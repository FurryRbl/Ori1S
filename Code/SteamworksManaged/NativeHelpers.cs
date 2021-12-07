using System;
using System.Runtime.InteropServices;
using ManagedSteam.Utility;

namespace ManagedSteam
{
	// Token: 0x02000168 RID: 360
	internal static class NativeHelpers
	{
		// Token: 0x06000BE8 RID: 3048 RVA: 0x000102B2 File Offset: 0x0000E4B2
		public static LoadStatus Services_GetSteamLoadStatus()
		{
			return (LoadStatus)NativeMethods.Services_GetSteamLoadStatus();
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000102B9 File Offset: 0x0000E4B9
		public static ErrorCodes Services_GetErrorCode()
		{
			return (ErrorCodes)NativeMethods.Services_GetErrorCode();
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x000102C0 File Offset: 0x0000E4C0
		public static LoadStatus ServicesGameServer_GetSteamLoadStatus()
		{
			return (LoadStatus)NativeMethods.ServicesGameServer_GetSteamLoadStatus();
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x000102C7 File Offset: 0x0000E4C7
		public static ErrorCodes ServicesGameServer_GetErrorCode()
		{
			return (ErrorCodes)NativeMethods.ServicesGameServer_GetErrorCode();
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x000102D0 File Offset: 0x0000E4D0
		public static T ConvertStruct<T>(IntPtr dataPointer, int dataSize) where T : struct
		{
			if (dataSize != Marshal.SizeOf(typeof(T)))
			{
				Error.ThrowError(ErrorCodes.CallbackStructSizeMissmatch, new object[]
				{
					string.Concat(new object[]
					{
						typeof(T).Name,
						". Ours is: ",
						Marshal.SizeOf(typeof(T)),
						", should be: ",
						dataSize
					})
				});
			}
			return (T)((object)Marshal.PtrToStructure(dataPointer, typeof(T)));
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00010368 File Offset: 0x0000E568
		public static T ConvertStructToClass<T>(IntPtr dataPointer, int dataSize) where T : class
		{
			if (dataSize != Marshal.SizeOf(typeof(T)))
			{
				Error.ThrowError(ErrorCodes.CallbackStructSizeMissmatch, new object[]
				{
					string.Concat(new object[]
					{
						typeof(T).Name,
						". Ours is ",
						Marshal.SizeOf(typeof(T)),
						", should be: ",
						dataSize
					})
				});
			}
			return (T)((object)Marshal.PtrToStructure(dataPointer, typeof(T)));
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00010400 File Offset: 0x0000E600
		public static string ToStringAnsi(IntPtr pointer)
		{
			return Marshal.PtrToStringAnsi(pointer);
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00010408 File Offset: 0x0000E608
		public static string ToStringUtf8(IntPtr pointer)
		{
			string result;
			using (NativeString nativeString = new NativeString(pointer))
			{
				result = nativeString.ToStringFromUtf8();
			}
			return result;
		}

		// Token: 0x04000649 RID: 1609
		public const int SteamStructPacking = 8;
	}
}
