using Dahomey.Cbor;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using static EdjCase.ICP.Candid.Mapping.Mappers.VariantMapper;

namespace EdjCase.ICP.Agent.Cbor
{
	internal static class CborUtil
	{
		public delegate void SetProperty<T>(string field, ref CborReader reader, ref T context);
		public delegate T GetValue<T>(ref CborReader reader);
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

		public static List<T>? ReadArrayOrNull<T>(ref CborReader reader, CborOptions options)
		{
			return ReadArrayOrNull(ref reader, GetValueFunc);

			T GetValueFunc(ref CborReader reader)
			{
				return Read<T>(ref reader, options);
			}
		}

		public static List<T>? ReadArrayOrNull<T>(ref CborReader reader, GetValue<T> getValueFunc)
		{
			if(reader.GetCurrentDataItemType() == CborDataItemType.Null)
			{
				return null;
			}
			return ReadArray<T>(ref reader, getValueFunc);
		}

		public static List<T> ReadArray<T>(ref CborReader reader, CborOptions options)
		{
			return ReadArray(ref reader, GetValueFunc);

			T GetValueFunc(ref CborReader reader)
			{
				return Read<T>(ref reader, options);
			}
		}

		public static List<T> ReadArray<T>(ref CborReader reader, GetValue<T> getValueFunc)
		{
			var list = new List<T>();
			reader.ReadBeginArray();
			var size = reader.ReadSize();
			for (int i = 0; i < size; i++)
			{
				T value = getValueFunc(ref reader);
				list.Add(value);
			}
			return list;
		}

		public static T Read<T>(ref CborReader reader, CborOptions cborOptions)
		{
			ICborConverter<T> converter = cborOptions
				.Registry
				.ConverterRegistry
				.Lookup<T>();
			return converter.Read(ref reader);
		}

		public static void Write<T>(ref CborWriter writer, T value, CborOptions cborOptions)
		{
			ICborConverter<T> converter = cborOptions
				.Registry
				.ConverterRegistry
				.Lookup<T>();
			converter.Write(ref writer, value);
		}
	}
}
