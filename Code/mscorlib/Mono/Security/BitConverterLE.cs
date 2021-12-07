using System;

namespace Mono.Security
{
	// Token: 0x0200009F RID: 159
	internal sealed class BitConverterLE
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x000234A4 File Offset: 0x000216A4
		private BitConverterLE()
		{
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x000234AC File Offset: 0x000216AC
		private unsafe static byte[] GetUShortBytes(byte* bytes)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					*bytes,
					bytes[1]
				};
			}
			return new byte[]
			{
				bytes[1],
				*bytes
			};
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000234E0 File Offset: 0x000216E0
		private unsafe static byte[] GetUIntBytes(byte* bytes)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					*bytes,
					bytes[1],
					bytes[2],
					bytes[3]
				};
			}
			return new byte[]
			{
				bytes[3],
				bytes[2],
				bytes[1],
				*bytes
			};
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00023538 File Offset: 0x00021738
		private unsafe static byte[] GetULongBytes(byte* bytes)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					*bytes,
					bytes[1],
					bytes[2],
					bytes[3],
					bytes[4],
					bytes[5],
					bytes[6],
					bytes[7]
				};
			}
			return new byte[]
			{
				bytes[7],
				bytes[6],
				bytes[5],
				bytes[4],
				bytes[3],
				bytes[2],
				bytes[1],
				*bytes
			};
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000235C8 File Offset: 0x000217C8
		internal static byte[] GetBytes(bool value)
		{
			return new byte[]
			{
				(!value) ? 0 : 1
			};
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000235E0 File Offset: 0x000217E0
		internal unsafe static byte[] GetBytes(char value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000235EC File Offset: 0x000217EC
		internal unsafe static byte[] GetBytes(short value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000235F8 File Offset: 0x000217F8
		internal unsafe static byte[] GetBytes(int value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00023604 File Offset: 0x00021804
		internal unsafe static byte[] GetBytes(long value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00023610 File Offset: 0x00021810
		internal unsafe static byte[] GetBytes(ushort value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0002361C File Offset: 0x0002181C
		internal unsafe static byte[] GetBytes(uint value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00023628 File Offset: 0x00021828
		internal unsafe static byte[] GetBytes(ulong value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00023634 File Offset: 0x00021834
		internal unsafe static byte[] GetBytes(float value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00023640 File Offset: 0x00021840
		internal unsafe static byte[] GetBytes(double value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0002364C File Offset: 0x0002184C
		private unsafe static void UShortFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				*dst = src[startIndex];
				dst[1] = src[startIndex + 1];
			}
			else
			{
				*dst = src[startIndex + 1];
				dst[1] = src[startIndex];
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0002367C File Offset: 0x0002187C
		private unsafe static void UIntFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				*dst = src[startIndex];
				dst[1] = src[startIndex + 1];
				dst[2] = src[startIndex + 2];
				dst[3] = src[startIndex + 3];
			}
			else
			{
				*dst = src[startIndex + 3];
				dst[1] = src[startIndex + 2];
				dst[2] = src[startIndex + 1];
				dst[3] = src[startIndex];
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000236D8 File Offset: 0x000218D8
		private unsafe static void ULongFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				for (int i = 0; i < 8; i++)
				{
					dst[i] = src[startIndex + i];
				}
			}
			else
			{
				for (int j = 0; j < 8; j++)
				{
					dst[j] = src[startIndex + (7 - j)];
				}
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0002372C File Offset: 0x0002192C
		internal static bool ToBoolean(byte[] value, int startIndex)
		{
			return value[startIndex] != 0;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00023738 File Offset: 0x00021938
		internal unsafe static char ToChar(byte[] value, int startIndex)
		{
			char result;
			BitConverterLE.UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00023750 File Offset: 0x00021950
		internal unsafe static short ToInt16(byte[] value, int startIndex)
		{
			short result;
			BitConverterLE.UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00023768 File Offset: 0x00021968
		internal unsafe static int ToInt32(byte[] value, int startIndex)
		{
			int result;
			BitConverterLE.UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00023780 File Offset: 0x00021980
		internal unsafe static long ToInt64(byte[] value, int startIndex)
		{
			long result;
			BitConverterLE.ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00023798 File Offset: 0x00021998
		internal unsafe static ushort ToUInt16(byte[] value, int startIndex)
		{
			ushort result;
			BitConverterLE.UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000237B0 File Offset: 0x000219B0
		internal unsafe static uint ToUInt32(byte[] value, int startIndex)
		{
			uint result;
			BitConverterLE.UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x000237C8 File Offset: 0x000219C8
		internal unsafe static ulong ToUInt64(byte[] value, int startIndex)
		{
			ulong result;
			BitConverterLE.ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000237E0 File Offset: 0x000219E0
		internal unsafe static float ToSingle(byte[] value, int startIndex)
		{
			float result;
			BitConverterLE.UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x000237F8 File Offset: 0x000219F8
		internal unsafe static double ToDouble(byte[] value, int startIndex)
		{
			double result;
			BitConverterLE.ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}
	}
}
