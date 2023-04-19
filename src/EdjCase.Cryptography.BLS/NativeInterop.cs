using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace EdjCase.Cryptography.BLS
{
	internal static class NativeInterop
	{
		private const string LibDl = "libdl";
		private const int RtldNow = 2;

		[DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
		private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

		[DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

		[DllImport(LibDl, SetLastError = true)]
		private static extern IntPtr dlopen(string fileName, int flag);

		[DllImport(LibDl, SetLastError = true)]
		private static extern IntPtr dlsym(IntPtr handle, string symbol);

		[DllImport(LibDl, SetLastError = true)]
		private static extern string dlerror();

		public static IntPtr LoadNativeLibrary(string libraryName)
		{
			IntPtr libraryHandle;

			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				libraryHandle = LoadLibrary(libraryName);

				if (libraryHandle == IntPtr.Zero)
				{
					throw new Exception($"Unable to load native library '{libraryName}'");
				}
			}
			else
			{
				dlerror(); // Clear any existing error
				libraryHandle = dlopen(libraryName, RtldNow);
				if (libraryHandle == IntPtr.Zero)
				{
					string error = dlerror();
					if (!string.IsNullOrEmpty(error))
					{
						throw new Exception($"Error loading native library: {error}");
					}
				}
			}

			return libraryHandle;
		}

		public static IntPtr GetFunctionPointer(IntPtr libraryHandle, string functionName)
		{
			IntPtr functionPointer;

			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				functionPointer = GetProcAddress(libraryHandle, functionName);
			}
			else
			{
				functionPointer = dlsym(libraryHandle, functionName);
			}

			if (functionPointer == IntPtr.Zero)
			{
				throw new Exception($"Unable to load native library function '{functionName}'");
			}

			return functionPointer;
		}
	}

}
