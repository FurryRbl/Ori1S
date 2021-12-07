using System;

namespace Mono.Security
{
	// Token: 0x02000010 RID: 16
	internal sealed class BitConverterLE
	{
		// Token: 0x06000094 RID: 148 RVA: 0x000058A0 File Offset: 0x00003AA0
		private BitConverterLE()
		{
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000058A8 File Offset: 0x00003AA8
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

		// Token: 0x06000096 RID: 150 RVA: 0x000058DC File Offset: 0x00003ADC
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

		// Token: 0x06000097 RID: 151 RVA: 0x00005934 File Offset: 0x00003B34
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

		// Token: 0x06000098 RID: 152 RVA: 0x000059C4 File Offset: 0x00003BC4
		internal static byte[] GetBytes(bool value)
		{
			return new byte[]
			{
				(!value) ? 0 : 1
			};
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000059DC File Offset: 0x00003BDC
		internal unsafe static byte[] GetBytes(char value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000059E8 File Offset: 0x00003BE8
		internal unsafe static byte[] GetBytes(short value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000059F4 File Offset: 0x00003BF4
		internal unsafe static byte[] GetBytes(int value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005A00 File Offset: 0x00003C00
		internal unsafe static byte[] GetBytes(long value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005A0C File Offset: 0x00003C0C
		internal unsafe static byte[] GetBytes(ushort value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005A18 File Offset: 0x00003C18
		internal unsafe static byte[] GetBytes(uint value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005A24 File Offset: 0x00003C24
		internal unsafe static byte[] GetBytes(ulong value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005A30 File Offset: 0x00003C30
		internal unsafe static byte[] GetBytes(float value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005A3C File Offset: 0x00003C3C
		internal unsafe static byte[] GetBytes(double value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005A48 File Offset: 0x00003C48
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

		// Token: 0x060000A3 RID: 163 RVA: 0x00005A78 File Offset: 0x00003C78
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

		// Token: 0x060000A4 RID: 164 RVA: 0x00005AD4 File Offset: 0x00003CD4
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

		// Token: 0x060000A5 RID: 165 RVA: 0x00005B28 File Offset: 0x00003D28
		internal static bool ToBoolean(byte[] value, int startIndex)
		{
			return value[startIndex] != 0;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005B34 File Offset: 0x00003D34
		internal unsafe static char ToChar(byte[] value, int startIndex)
		{
			char result;
			BitConverterLE.UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005B4C File Offset: 0x00003D4C
		internal unsafe static short ToInt16(byte[] value, int startIndex)
		{
			short result;
			BitConverterLE.UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005B64 File Offset: 0x00003D64
		internal unsafe static int ToInt32(byte[] value, int startIndex)
		{
			int result;
			BitConverterLE.UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005B7C File Offset: 0x00003D7C
		internal unsafe static long ToInt64(byte[] value, int startIndex)
		{
			long result;
			BitConverterLE.ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005B94 File Offset: 0x00003D94
		internal unsafe static ushort ToUInt16(byte[] value, int startIndex)
		{
			ushort result;
			BitConverterLE.UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005BAC File Offset: 0x00003DAC
		internal unsafe static uint ToUInt32(byte[] value, int startIndex)
		{
			uint result;
			BitConverterLE.UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005BC4 File Offset: 0x00003DC4
		internal unsafe static ulong ToUInt64(byte[] value, int startIndex)
		{
			ulong result;
			BitConverterLE.ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005BDC File Offset: 0x00003DDC
		internal unsafe static float ToSingle(byte[] value, int startIndex)
		{
			float result;
			BitConverterLE.UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005BF4 File Offset: 0x00003DF4
		internal unsafe static double ToDouble(byte[] value, int startIndex)
		{
			double result;
			BitConverterLE.ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}
	}
}
