using System;
using System.IO;
using System.Text;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000A4 RID: 164
	internal sealed class WWWTranscoder
	{
		// Token: 0x06000976 RID: 2422 RVA: 0x0000DA18 File Offset: 0x0000BC18
		private static byte Hex2Byte(byte[] b, int offset)
		{
			byte b2 = 0;
			for (int i = offset; i < offset + 2; i++)
			{
				b2 *= 16;
				int num = (int)b[i];
				if (num >= 48 && num <= 57)
				{
					num -= 48;
				}
				else if (num >= 65 && num <= 75)
				{
					num -= 55;
				}
				else if (num >= 97 && num <= 102)
				{
					num -= 87;
				}
				if (num > 15)
				{
					return 63;
				}
				b2 += (byte)num;
			}
			return b2;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0000DAA0 File Offset: 0x0000BCA0
		private static byte[] Byte2Hex(byte b, byte[] hexChars)
		{
			return new byte[]
			{
				hexChars[b >> 4],
				hexChars[(int)(b & 15)]
			};
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
		[ExcludeFromDocs]
		public static string URLEncode(string toEncode)
		{
			Encoding utf = Encoding.UTF8;
			return WWWTranscoder.URLEncode(toEncode, utf);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		public static string URLEncode(string toEncode, [DefaultValue("Encoding.UTF8")] Encoding e)
		{
			byte[] array = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace, WWWTranscoder.urlForbidden, false);
			return WWW.DefaultEncoding.GetString(array, 0, array.Length);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0000DB20 File Offset: 0x0000BD20
		public static byte[] URLEncode(byte[] toEncode)
		{
			return WWWTranscoder.Encode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace, WWWTranscoder.urlForbidden, false);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0000DB38 File Offset: 0x0000BD38
		[ExcludeFromDocs]
		public static string QPEncode(string toEncode)
		{
			Encoding utf = Encoding.UTF8;
			return WWWTranscoder.QPEncode(toEncode, utf);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0000DB54 File Offset: 0x0000BD54
		public static string QPEncode(string toEncode, [DefaultValue("Encoding.UTF8")] Encoding e)
		{
			byte[] array = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace, WWWTranscoder.qpForbidden, true);
			return WWW.DefaultEncoding.GetString(array, 0, array.Length);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0000DB90 File Offset: 0x0000BD90
		public static byte[] QPEncode(byte[] toEncode)
		{
			return WWWTranscoder.Encode(toEncode, WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace, WWWTranscoder.qpForbidden, true);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		public static byte[] Encode(byte[] input, byte escapeChar, byte space, byte[] forbidden, bool uppercase)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(input.Length * 2))
			{
				for (int i = 0; i < input.Length; i++)
				{
					if (input[i] == 32)
					{
						memoryStream.WriteByte(space);
					}
					else if (input[i] < 32 || input[i] > 126 || WWWTranscoder.ByteArrayContains(forbidden, input[i]))
					{
						memoryStream.WriteByte(escapeChar);
						memoryStream.Write(WWWTranscoder.Byte2Hex(input[i], (!uppercase) ? WWWTranscoder.lcHexChars : WWWTranscoder.ucHexChars), 0, 2);
					}
					else
					{
						memoryStream.WriteByte(input[i]);
					}
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0000DC84 File Offset: 0x0000BE84
		private static bool ByteArrayContains(byte[] array, byte b)
		{
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				if (array[i] == b)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0000DCB4 File Offset: 0x0000BEB4
		[ExcludeFromDocs]
		public static string URLDecode(string toEncode)
		{
			Encoding utf = Encoding.UTF8;
			return WWWTranscoder.URLDecode(toEncode, utf);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0000DCD0 File Offset: 0x0000BED0
		public static string URLDecode(string toEncode, [DefaultValue("Encoding.UTF8")] Encoding e)
		{
			byte[] array = WWWTranscoder.Decode(WWW.DefaultEncoding.GetBytes(toEncode), WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace);
			return e.GetString(array, 0, array.Length);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0000DD04 File Offset: 0x0000BF04
		public static byte[] URLDecode(byte[] toEncode)
		{
			return WWWTranscoder.Decode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0000DD18 File Offset: 0x0000BF18
		[ExcludeFromDocs]
		public static string QPDecode(string toEncode)
		{
			Encoding utf = Encoding.UTF8;
			return WWWTranscoder.QPDecode(toEncode, utf);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0000DD34 File Offset: 0x0000BF34
		public static string QPDecode(string toEncode, [DefaultValue("Encoding.UTF8")] Encoding e)
		{
			byte[] array = WWWTranscoder.Decode(WWW.DefaultEncoding.GetBytes(toEncode), WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace);
			return e.GetString(array, 0, array.Length);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0000DD68 File Offset: 0x0000BF68
		public static byte[] QPDecode(byte[] toEncode)
		{
			return WWWTranscoder.Decode(toEncode, WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		public static byte[] Decode(byte[] input, byte escapeChar, byte space)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(input.Length))
			{
				for (int i = 0; i < input.Length; i++)
				{
					if (input[i] == space)
					{
						memoryStream.WriteByte(32);
					}
					else if (input[i] == escapeChar && i + 2 < input.Length)
					{
						i++;
						memoryStream.WriteByte(WWWTranscoder.Hex2Byte(input, i++));
					}
					else
					{
						memoryStream.WriteByte(input[i]);
					}
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0000DE30 File Offset: 0x0000C030
		[ExcludeFromDocs]
		public static bool SevenBitClean(string s)
		{
			Encoding utf = Encoding.UTF8;
			return WWWTranscoder.SevenBitClean(s, utf);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0000DE4C File Offset: 0x0000C04C
		public static bool SevenBitClean(string s, [DefaultValue("Encoding.UTF8")] Encoding e)
		{
			return WWWTranscoder.SevenBitClean(e.GetBytes(s));
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0000DE5C File Offset: 0x0000C05C
		public static bool SevenBitClean(byte[] input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] < 32 || input[i] > 126)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040001F9 RID: 505
		private static byte[] ucHexChars = WWW.DefaultEncoding.GetBytes("0123456789ABCDEF");

		// Token: 0x040001FA RID: 506
		private static byte[] lcHexChars = WWW.DefaultEncoding.GetBytes("0123456789abcdef");

		// Token: 0x040001FB RID: 507
		private static byte urlEscapeChar = 37;

		// Token: 0x040001FC RID: 508
		private static byte urlSpace = 43;

		// Token: 0x040001FD RID: 509
		private static byte[] urlForbidden = WWW.DefaultEncoding.GetBytes("@&;:<>=?\"'/\\!#%+$,{}|^[]`");

		// Token: 0x040001FE RID: 510
		private static byte qpEscapeChar = 61;

		// Token: 0x040001FF RID: 511
		private static byte qpSpace = 95;

		// Token: 0x04000200 RID: 512
		private static byte[] qpForbidden = WWW.DefaultEncoding.GetBytes("&;=?\"'%+_");
	}
}
