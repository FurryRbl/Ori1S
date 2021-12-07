using System;
using System.Text;

namespace Mono.Unix
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class UnixEncoding : Encoding
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00003B68 File Offset: 0x00001D68
		private static int InternalGetByteCount(char[] chars, int index, int count, uint leftOver, bool flush)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (index < 0 || index > chars.Length)
			{
				throw new ArgumentOutOfRangeException("index", UnixEncoding._("ArgRange_Array"));
			}
			if (count < 0 || count > chars.Length - index)
			{
				throw new ArgumentOutOfRangeException("count", UnixEncoding._("ArgRange_Array"));
			}
			int num = 0;
			uint num2 = leftOver;
			while (count > 0)
			{
				char c = chars[index];
				if (num2 == 0U)
				{
					if (c == UnixEncoding.EscapeByte && count > 1)
					{
						num++;
						index++;
						count--;
					}
					else if (c < '\u0080')
					{
						num++;
					}
					else if (c < 'ࠀ')
					{
						num += 2;
					}
					else if (c >= '\ud800' && c <= '\udbff')
					{
						num2 = (uint)c;
					}
					else
					{
						num += 3;
					}
				}
				else
				{
					if (c < '\udc00' || c > '\udfff')
					{
						num += 3;
						num2 = 0U;
						continue;
					}
					num += 4;
					num2 = 0U;
				}
				index++;
				count--;
			}
			if (flush && num2 != 0U)
			{
				num += 3;
			}
			return num;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return UnixEncoding.InternalGetByteCount(chars, index, count, 0U, true);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public override int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			int num = 0;
			int i = s.Length;
			int num2 = 0;
			while (i > 0)
			{
				char c = s[num++];
				if (c == UnixEncoding.EscapeByte && i > 1)
				{
					num2++;
					num++;
					i--;
				}
				else if (c < '\u0080')
				{
					num2++;
				}
				else if (c < 'ࠀ')
				{
					num2 += 2;
				}
				else if (c >= '\ud800' && c <= '\udbff' && i > 1)
				{
					uint num3 = (uint)s[num];
					if (num3 >= 56320U && num3 <= 57343U)
					{
						num2 += 4;
						num++;
						i--;
					}
					else
					{
						num2 += 3;
					}
				}
				else
				{
					num2 += 3;
				}
				i--;
			}
			return num2;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003DA4 File Offset: 0x00001FA4
		private static int InternalGetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, ref uint leftOver, bool flush)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", UnixEncoding._("ArgRange_Array"));
			}
			if (charCount < 0 || charCount > chars.Length - charIndex)
			{
				throw new ArgumentOutOfRangeException("charCount", UnixEncoding._("ArgRange_Array"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", UnixEncoding._("ArgRange_Array"));
			}
			int num = bytes.Length;
			uint num2 = leftOver;
			int num3 = byteIndex;
			while (charCount > 0)
			{
				char c = chars[charIndex++];
				charCount--;
				uint num4;
				if (num2 == 0U)
				{
					if (c >= '\ud800' && c <= '\udbff')
					{
						num2 = (uint)c;
						continue;
					}
					if (c == UnixEncoding.EscapeByte)
					{
						if (num3 >= num)
						{
							throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "bytes");
						}
						if (--charCount >= 0)
						{
							bytes[num3++] = (byte)chars[charIndex++];
						}
						continue;
					}
					else
					{
						num4 = (uint)c;
					}
				}
				else if (c >= '\udc00' && c <= '\udfff')
				{
					num4 = (num2 - 55296U << 10) + (uint)(c - '\udc00') + 65536U;
					num2 = 0U;
				}
				else
				{
					num4 = num2;
					num2 = 0U;
					charIndex--;
					charCount++;
				}
				if (num4 < 128U)
				{
					if (num3 >= num)
					{
						throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "bytes");
					}
					bytes[num3++] = (byte)num4;
				}
				else if (num4 < 2048U)
				{
					if (num3 + 2 > num)
					{
						throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "bytes");
					}
					bytes[num3++] = (byte)(192U | num4 >> 6);
					bytes[num3++] = (byte)(128U | (num4 & 63U));
				}
				else if (num4 < 65536U)
				{
					if (num3 + 3 > num)
					{
						throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "bytes");
					}
					bytes[num3++] = (byte)(224U | num4 >> 12);
					bytes[num3++] = (byte)(128U | (num4 >> 6 & 63U));
					bytes[num3++] = (byte)(128U | (num4 & 63U));
				}
				else
				{
					if (num3 + 4 > num)
					{
						throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "bytes");
					}
					bytes[num3++] = (byte)(240U | num4 >> 18);
					bytes[num3++] = (byte)(128U | (num4 >> 12 & 63U));
					bytes[num3++] = (byte)(128U | (num4 >> 6 & 63U));
					bytes[num3++] = (byte)(128U | (num4 & 63U));
				}
			}
			if (flush && num2 != 0U)
			{
				if (num3 + 3 > num)
				{
					throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "bytes");
				}
				bytes[num3++] = (byte)(224U | num2 >> 12);
				bytes[num3++] = (byte)(128U | (num2 >> 6 & 63U));
				bytes[num3++] = (byte)(128U | (num2 & 63U));
				num2 = 0U;
			}
			leftOver = num2;
			return num3 - byteIndex;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000410C File Offset: 0x0000230C
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			uint num = 0U;
			return UnixEncoding.InternalGetBytes(chars, charIndex, charCount, bytes, byteIndex, ref num, true);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000412C File Offset: 0x0000232C
		public override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (charIndex < 0 || charIndex > s.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", UnixEncoding._("ArgRange_StringIndex"));
			}
			if (charCount < 0 || charCount > s.Length - charIndex)
			{
				throw new ArgumentOutOfRangeException("charCount", UnixEncoding._("ArgRange_StringRange"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", UnixEncoding._("ArgRange_Array"));
			}
			int num = bytes.Length;
			int num2 = byteIndex;
			while (charCount > 0)
			{
				char c = s[charIndex++];
				uint num3;
				if (c >= '\ud800' && c <= '\udbff' && charCount > 1)
				{
					num3 = (uint)s[charIndex];
					if (num3 >= 56320U && num3 <= 57343U)
					{
						num3 = num3 - 56320U + (uint)((uint)(c - '\ud800') << 10) + 65536U;
						charIndex++;
						charCount--;
					}
					else
					{
						num3 = (uint)c;
					}
				}
				else if (c == UnixEncoding.EscapeByte && charCount > 1)
				{
					if (num2 >= num)
					{
						throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "bytes");
					}
					charCount -= 2;
					if (charCount >= 0)
					{
						bytes[num2++] = (byte)s[charIndex++];
					}
					continue;
				}
				else
				{
					num3 = (uint)c;
				}
				charCount--;
				if (num3 < 128U)
				{
					if (num2 >= num)
					{
						throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "bytes");
					}
					bytes[num2++] = (byte)num3;
				}
				else if (num3 < 2048U)
				{
					if (num2 + 2 > num)
					{
						throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "bytes");
					}
					bytes[num2++] = (byte)(192U | num3 >> 6);
					bytes[num2++] = (byte)(128U | (num3 & 63U));
				}
				else if (num3 < 65536U)
				{
					if (num2 + 3 > num)
					{
						throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "bytes");
					}
					bytes[num2++] = (byte)(224U | num3 >> 12);
					bytes[num2++] = (byte)(128U | (num3 >> 6 & 63U));
					bytes[num2++] = (byte)(128U | (num3 & 63U));
				}
				else
				{
					if (num2 + 4 > num)
					{
						throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "bytes");
					}
					bytes[num2++] = (byte)(240U | num3 >> 18);
					bytes[num2++] = (byte)(128U | (num3 >> 12 & 63U));
					bytes[num2++] = (byte)(128U | (num3 >> 6 & 63U));
					bytes[num2++] = (byte)(128U | (num3 & 63U));
				}
			}
			return num2 - byteIndex;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004424 File Offset: 0x00002624
		private static int InternalGetCharCount(byte[] bytes, int index, int count, uint leftOverBits, uint leftOverCount, bool throwOnInvalid, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (index < 0 || index > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("index", UnixEncoding._("ArgRange_Array"));
			}
			if (count < 0 || count > bytes.Length - index)
			{
				throw new ArgumentOutOfRangeException("count", UnixEncoding._("ArgRange_Array"));
			}
			int num = 0;
			int num2 = 0;
			uint num3 = leftOverBits;
			uint num4 = leftOverCount & 15U;
			uint num5 = leftOverCount >> 4 & 15U;
			while (count > 0)
			{
				uint num6 = (uint)bytes[index++];
				num++;
				count--;
				if (num5 == 0U)
				{
					if (num6 < 128U)
					{
						num2++;
						num = 0;
					}
					else if ((num6 & 224U) == 192U)
					{
						num3 = (num6 & 31U);
						num4 = 1U;
						num5 = 2U;
					}
					else if ((num6 & 240U) == 224U)
					{
						num3 = (num6 & 15U);
						num4 = 1U;
						num5 = 3U;
					}
					else if ((num6 & 248U) == 240U)
					{
						num3 = (num6 & 7U);
						num4 = 1U;
						num5 = 4U;
					}
					else if ((num6 & 252U) == 248U)
					{
						num3 = (num6 & 3U);
						num4 = 1U;
						num5 = 5U;
					}
					else if ((num6 & 254U) == 252U)
					{
						num3 = (num6 & 3U);
						num4 = 1U;
						num5 = 6U;
					}
					else
					{
						if (throwOnInvalid)
						{
						}
						num2 += num * 2;
						num = 0;
					}
				}
				else if ((num6 & 192U) == 128U)
				{
					num3 = (num3 << 6 | (num6 & 63U));
					if ((num4 += 1U) >= num5)
					{
						if (num3 < 65536U)
						{
							bool flag = false;
							switch (num5)
							{
							case 2U:
								flag = (num3 <= 127U);
								break;
							case 3U:
								flag = (num3 <= 2047U);
								break;
							case 4U:
								flag = (num3 <= 65535U);
								break;
							case 5U:
								flag = (num3 <= 2097151U);
								break;
							case 6U:
								flag = (num3 <= 67108863U);
								break;
							}
							if (flag)
							{
								num2 += num * 2;
							}
							else
							{
								num2++;
							}
						}
						else if (num3 < 1114112U)
						{
							num2 += 2;
						}
						else if (throwOnInvalid)
						{
							num2 += num * 2;
						}
						num5 = 0U;
						num = 0;
					}
				}
				else
				{
					if (throwOnInvalid)
					{
					}
					if (num6 < 128U)
					{
						index--;
						count++;
						num--;
					}
					num2 += num * 2;
					num5 = 0U;
					num = 0;
				}
			}
			if (flush && num5 != 0U && throwOnInvalid)
			{
				num2 += num * 2;
			}
			return num2;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000046D8 File Offset: 0x000028D8
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return UnixEncoding.InternalGetCharCount(bytes, index, count, 0U, 0U, true, true);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000046E8 File Offset: 0x000028E8
		private static int InternalGetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, ref uint leftOverBits, ref uint leftOverCount, bool throwOnInvalid, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", UnixEncoding._("ArgRange_Array"));
			}
			if (byteCount < 0 || byteCount > bytes.Length - byteIndex)
			{
				throw new ArgumentOutOfRangeException("byteCount", UnixEncoding._("ArgRange_Array"));
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", UnixEncoding._("ArgRange_Array"));
			}
			if (charIndex == chars.Length)
			{
				return 0;
			}
			byte[] array = new byte[6];
			int num = 0;
			int num2 = chars.Length;
			int num3 = charIndex;
			uint num4 = leftOverBits;
			uint num5 = leftOverCount & 15U;
			uint num6 = leftOverCount >> 4 & 15U;
			while (byteCount > 0)
			{
				uint num7 = (uint)bytes[byteIndex++];
				array[num++] = (byte)num7;
				byteCount--;
				if (num6 == 0U)
				{
					if (num7 < 128U)
					{
						if (num3 >= num2)
						{
							throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "chars");
						}
						num = 0;
						chars[num3++] = (char)num7;
					}
					else if ((num7 & 224U) == 192U)
					{
						num4 = (num7 & 31U);
						num5 = 1U;
						num6 = 2U;
					}
					else if ((num7 & 240U) == 224U)
					{
						num4 = (num7 & 15U);
						num5 = 1U;
						num6 = 3U;
					}
					else if ((num7 & 248U) == 240U)
					{
						num4 = (num7 & 7U);
						num5 = 1U;
						num6 = 4U;
					}
					else if ((num7 & 252U) == 248U)
					{
						num4 = (num7 & 3U);
						num5 = 1U;
						num6 = 5U;
					}
					else if ((num7 & 254U) == 252U)
					{
						num4 = (num7 & 3U);
						num5 = 1U;
						num6 = 6U;
					}
					else
					{
						if (throwOnInvalid)
						{
						}
						num = 0;
						chars[num3++] = UnixEncoding.EscapeByte;
						chars[num3++] = (char)num7;
					}
				}
				else if ((num7 & 192U) == 128U)
				{
					num4 = (num4 << 6 | (num7 & 63U));
					if ((num5 += 1U) >= num6)
					{
						if (num4 < 65536U)
						{
							bool flag = false;
							switch (num6)
							{
							case 2U:
								flag = (num4 <= 127U);
								break;
							case 3U:
								flag = (num4 <= 2047U);
								break;
							case 4U:
								flag = (num4 <= 65535U);
								break;
							case 5U:
								flag = (num4 <= 2097151U);
								break;
							case 6U:
								flag = (num4 <= 67108863U);
								break;
							}
							if (flag)
							{
								UnixEncoding.CopyRaw(array, ref num, chars, ref num3, num2);
							}
							else
							{
								if (num3 >= num2)
								{
									throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "chars");
								}
								chars[num3++] = (char)num4;
							}
						}
						else if (num4 < 1114112U)
						{
							if (num3 + 2 > num2)
							{
								throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "chars");
							}
							num4 -= 65536U;
							chars[num3++] = (char)((num4 >> 10) + 55296U);
							chars[num3++] = (char)((num4 & 1023U) + 56320U);
						}
						else if (throwOnInvalid)
						{
							UnixEncoding.CopyRaw(array, ref num, chars, ref num3, num2);
						}
						num6 = 0U;
						num = 0;
					}
				}
				else
				{
					if (throwOnInvalid)
					{
					}
					if (num7 < 128U)
					{
						byteIndex--;
						byteCount++;
						num--;
					}
					UnixEncoding.CopyRaw(array, ref num, chars, ref num3, num2);
					num6 = 0U;
					num = 0;
				}
			}
			if (flush && num6 != 0U && throwOnInvalid)
			{
				UnixEncoding.CopyRaw(array, ref num, chars, ref num3, num2);
			}
			leftOverBits = num4;
			leftOverCount = (num5 | num6 << 4);
			return num3 - charIndex;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004ADC File Offset: 0x00002CDC
		private static void CopyRaw(byte[] raw, ref int next_raw, char[] chars, ref int posn, int length)
		{
			if (posn + next_raw * 2 > length)
			{
				throw new ArgumentException(UnixEncoding._("Arg_InsufficientSpace"), "chars");
			}
			for (int i = 0; i < next_raw; i++)
			{
				chars[posn++] = UnixEncoding.EscapeByte;
				chars[posn++] = (char)raw[i];
			}
			next_raw = 0;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004B44 File Offset: 0x00002D44
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			uint num = 0U;
			uint num2 = 0U;
			return UnixEncoding.InternalGetChars(bytes, byteIndex, byteCount, chars, charIndex, ref num, ref num2, true, true);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004B68 File Offset: 0x00002D68
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", UnixEncoding._("ArgRange_NonNegative"));
			}
			return charCount * 4;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004B8C File Offset: 0x00002D8C
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", UnixEncoding._("ArgRange_NonNegative"));
			}
			return byteCount;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004BAC File Offset: 0x00002DAC
		public override Decoder GetDecoder()
		{
			return new UnixEncoding.UnixDecoder();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public override Encoder GetEncoder()
		{
			return new UnixEncoding.UnixEncoder();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004BBC File Offset: 0x00002DBC
		public override byte[] GetPreamble()
		{
			return new byte[0];
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004BC4 File Offset: 0x00002DC4
		public override bool Equals(object value)
		{
			return value is UnixEncoding;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004BE4 File Offset: 0x00002DE4
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004BEC File Offset: 0x00002DEC
		public override byte[] GetBytes(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			int byteCount = this.GetByteCount(s);
			byte[] array = new byte[byteCount];
			this.GetBytes(s, 0, s.Length, array, 0);
			return array;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004C2C File Offset: 0x00002E2C
		private static string _(string arg)
		{
			return arg;
		}

		// Token: 0x0400005B RID: 91
		public static readonly Encoding Instance = new UnixEncoding();

		// Token: 0x0400005C RID: 92
		public static readonly char EscapeByte = '\0';

		// Token: 0x02000012 RID: 18
		[Serializable]
		private class UnixDecoder : Decoder
		{
			// Token: 0x0600009D RID: 157 RVA: 0x00004C30 File Offset: 0x00002E30
			public UnixDecoder()
			{
				this.leftOverBits = 0U;
				this.leftOverCount = 0U;
			}

			// Token: 0x0600009E RID: 158 RVA: 0x00004C48 File Offset: 0x00002E48
			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return UnixEncoding.InternalGetCharCount(bytes, index, count, this.leftOverBits, this.leftOverCount, true, false);
			}

			// Token: 0x0600009F RID: 159 RVA: 0x00004C60 File Offset: 0x00002E60
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				return UnixEncoding.InternalGetChars(bytes, byteIndex, byteCount, chars, charIndex, ref this.leftOverBits, ref this.leftOverCount, true, false);
			}

			// Token: 0x0400005D RID: 93
			private uint leftOverBits;

			// Token: 0x0400005E RID: 94
			private uint leftOverCount;
		}

		// Token: 0x02000013 RID: 19
		[Serializable]
		private class UnixEncoder : Encoder
		{
			// Token: 0x060000A0 RID: 160 RVA: 0x00004C88 File Offset: 0x00002E88
			public UnixEncoder()
			{
				this.leftOver = 0U;
			}

			// Token: 0x060000A1 RID: 161 RVA: 0x00004C98 File Offset: 0x00002E98
			public override int GetByteCount(char[] chars, int index, int count, bool flush)
			{
				return UnixEncoding.InternalGetByteCount(chars, index, count, this.leftOver, flush);
			}

			// Token: 0x060000A2 RID: 162 RVA: 0x00004CAC File Offset: 0x00002EAC
			public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteCount, bool flush)
			{
				return UnixEncoding.InternalGetBytes(chars, charIndex, charCount, bytes, byteCount, ref this.leftOver, flush);
			}

			// Token: 0x0400005F RID: 95
			private uint leftOver;
		}
	}
}
