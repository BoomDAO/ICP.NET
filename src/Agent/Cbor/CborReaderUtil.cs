using Agent.Cbor;
using Dahomey.Cbor.Serialization;
using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static EdjCase.ICP.Agent.Cbor.CborReaderUtil;

namespace EdjCase.ICP.Agent.Cbor
{
	internal static class CborReaderUtil
	{
		public delegate void SetProperty<T>(string field, ref CborReader reader, ref T context);
		public static void ReadMap<T>(ref CborReader reader, ref T context, SetProperty<T> setFunc)
		{
			reader.ReadBeginMap();
			var size = reader.ReadSize();
			for (int i = 0; i < size; i++)
			{
				string field = reader.ReadString()!;
				reader.MoveNextMapItem();
				setFunc(field, ref reader, ref context);
			}
		}
	}
}
