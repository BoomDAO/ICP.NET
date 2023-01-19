// Used under MIT License
// Copyright(c) 2017-2018 NeoSmart Technologies<https://neosmart.net/>
// see https://github.com/neosmart/UrlBase64/
//
// we dont use the package directly as we want to operate on spans, not just byte arrays

using System;
using System.Collections.Generic;

namespace EdjCase.ICP.InternetIdentity
{
	internal enum PaddingPolicy
	{
		Discard,
		Preserve,
	}

	internal static class UrlBase64
	{
		private static readonly char[] TwoPads = { '=', '=' };

		public static string Encode(ReadOnlySpan<byte> bytes, PaddingPolicy padding = PaddingPolicy.Discard)
		{
			var encoded = Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_');
			if (padding == PaddingPolicy.Discard)
			{
				encoded = encoded.TrimEnd('=');
			}

			return encoded;
		}

		public static byte[] Decode(string encoded)
		{
			var chars = new List<char>(encoded.ToCharArray());

			for (int i = 0; i < chars.Count; ++i)
			{
				if (chars[i] == '_')
				{
					chars[i] = '/';
				}
				else if (chars[i] == '-')
				{
					chars[i] = '+';
				}
			}

			switch (encoded.Length % 4)
			{
				case 2:
					chars.AddRange(TwoPads);
					break;
				case 3:
					chars.Add('=');
					break;
			}

			var array = chars.ToArray();

			return Convert.FromBase64CharArray(array, 0, array.Length);
		}
	}
}
