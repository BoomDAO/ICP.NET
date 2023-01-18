using System;
using System.Diagnostics;

namespace EdjCase.ICP.Candid.Crypto
{
	internal class HashBuffer
	{
		private readonly byte[] data;
		private int position;

		public HashBuffer(int length)
		{
			Debug.Assert(length > 0);

			this.data = new byte[length];
			this.position = 0;
		}

		public void Reset()
		{
			this.position = 0;
		}

		public byte[] GetBytes()
		{
			Debug.Assert(this.IsFull);

			this.position = 0;
			return this.data;
		}

		public byte[] GetBytesZeroPadded()
		{
			Array.Clear(this.data, this.position, this.data.Length - this.position);
			this.position = 0;
			return this.data;
		}

		public bool Feed(byte[] a_data, ref int a_start_index, ref int a_length, ref ulong a_processed_bytes)
		{
			Debug.Assert(a_start_index >= 0);
			Debug.Assert(a_length >= 0);
			Debug.Assert(a_start_index + a_length <= a_data.Length);
			Debug.Assert(!this.IsFull);

			if (a_data.Length == 0)
				return false;

			if (a_length == 0)
				return false;

			int length = this.data.Length - this.position;
			if (length > a_length)
				length = a_length;

			Array.Copy(a_data, a_start_index, this.data, this.position, length);

			this.position += length;
			a_start_index += length;
			a_length -= length;
			a_processed_bytes += (ulong)length;

			return this.IsFull;
		}

		public bool Feed(byte[] a_data, int a_length)
		{
			Debug.Assert(a_length >= 0);
			Debug.Assert(a_length <= a_data.Length);
			Debug.Assert(!this.IsFull);

			if (a_data.Length == 0)
				return false;

			if (a_length == 0)
				return false;

			int length = this.data.Length - this.position;
			if (length > a_length)
				length = a_length;

			Array.Copy(a_data, 0, this.data, this.position, length);

			this.position += length;

			return this.IsFull;
		}

		public bool IsEmpty
		{
			get
			{
				return this.position == 0;
			}
		}

		public int Pos
		{
			get
			{
				return this.position;
			}
		}

		public int Length
		{
			get
			{
				return this.data.Length;
			}
		}

		public bool IsFull
		{
			get
			{
				return this.position == this.data.Length;
			}
		}

		public override string ToString()
		{
			return $"HashBuffer, Legth: {this.Length}, Pos: {this.Pos}, IsEmpty: {this.IsEmpty}";
		}
	}
}
