using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.BLS.Models
{
	public class G1Affine
	{
		public Fp X { get; }
		public Fp Y { get; }
		public bool IsInfinity { get; }

		public G1Affine(Fp x, Fp y, bool isInfinity)
		{
			this.X = x;
			this.Y = y;
			this.IsInfinity = isInfinity;
		}

		private static G1Affine Infinity()
		{

		}


		internal static G1Affine FromCompressed(byte[] bytes)
		{
			if (bytes.Length != 48) throw new ArgumentException("Invalid byte array length for G1Affine compression.");

			// Extract flags and reconstruct the x-coordinate
			bool compressionFlag = (bytes[0] & 0x80) != 0;
			bool infinityFlag = (bytes[0] & 0x40) != 0;
			bool sortFlag = (bytes[0] & 0x20) != 0;

			bytes[0] &= 0x1F; // Clear flag bits
			Fp x = Fp.FromBytes(bytes);

			if (infinityFlag)
			{
				return G1Affine.Infinity();
			}
			else
			{
				Fp ySquared = x.Square().Multiply(x).Add(B); // y^2 = x^3 + B
				Fp y = ySquared.Sqrt(); // Attempt to compute sqrt(y^2)

				if (y != null)
				{
					bool yFlag = y.ToBytes()[0] >> 7 != 0; // Assuming ToBytes() gives the big-endian representation and we check the lexicographic order
					if (sortFlag != yFlag)
					{
						y = y.Negate(); // Switch y if necessary based on the sort flag
					}

					return new G1Affine { X = x, Y = y, IsInfinity = infinityFlag };
				}
			}

			throw new ArgumentException("Invalid compressed bytes for G1Affine point.");
		}
	}

}
