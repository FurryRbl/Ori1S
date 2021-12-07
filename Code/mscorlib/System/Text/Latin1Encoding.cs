using System;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x02000685 RID: 1669
	[Serializable]
	internal class Latin1Encoding : Encoding
	{
		// Token: 0x06003F6A RID: 16234 RVA: 0x000D9638 File Offset: 0x000D7838
		public Latin1Encoding() : base(28591)
		{
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06003F6B RID: 16235 RVA: 0x000D9648 File Offset: 0x000D7848
		public override bool IsSingleByte
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x000D964C File Offset: 0x000D784C
		public override bool IsAlwaysNormalized(NormalizationForm form)
		{
			return form == NormalizationForm.FormC;
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x000D9654 File Offset: 0x000D7854
		public override int GetByteCount(char[] chars, int index, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (index < 0 || index > chars.Length)
			{
				throw new ArgumentOutOfRangeException("index", Encoding._("ArgRange_Array"));
			}
			if (count < 0 || count > chars.Length - index)
			{
				throw new ArgumentOutOfRangeException("count", Encoding._("ArgRange_Array"));
			}
			return count;
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x000D96C0 File Offset: 0x000D78C0
		public override int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return s.Length;
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x000D96DC File Offset: 0x000D78DC
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			char[] array = null;
			return this.GetBytes(chars, charIndex, charCount, bytes, byteIndex, ref encoderFallbackBuffer, ref array);
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x000D9700 File Offset: 0x000D7900
		private int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, ref EncoderFallbackBuffer buffer, ref char[] fallback_chars)
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
				throw new ArgumentOutOfRangeException("charIndex", Encoding._("ArgRange_Array"));
			}
			if (charCount < 0 || charCount > chars.Length - charIndex)
			{
				throw new ArgumentOutOfRangeException("charCount", Encoding._("ArgRange_Array"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Encoding._("ArgRange_Array"));
			}
			if (bytes.Length - byteIndex < charCount)
			{
				throw new ArgumentException(Encoding._("Arg_InsufficientSpace"));
			}
			int num = charCount;
			while (num-- > 0)
			{
				char c = chars[charIndex++];
				if (c < 'Ā')
				{
					bytes[byteIndex++] = (byte)c;
				}
				else if (c >= '！' && c <= '～')
				{
					bytes[byteIndex++] = (byte)(c - 'ﻠ');
				}
				else
				{
					if (buffer == null)
					{
						buffer = base.EncoderFallback.CreateFallbackBuffer();
					}
					if (char.IsSurrogate(c) && num > 1 && char.IsSurrogate(chars[charIndex]))
					{
						buffer.Fallback(c, chars[charIndex], charIndex++ - 1);
					}
					else
					{
						buffer.Fallback(c, charIndex - 1);
					}
					if (fallback_chars == null || fallback_chars.Length < buffer.Remaining)
					{
						fallback_chars = new char[buffer.Remaining];
					}
					for (int i = 0; i < fallback_chars.Length; i++)
					{
						fallback_chars[i] = buffer.GetNextChar();
					}
					byteIndex += this.GetBytes(fallback_chars, 0, fallback_chars.Length, bytes, byteIndex, ref buffer, ref fallback_chars);
				}
			}
			return charCount;
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x000D98EC File Offset: 0x000D7AEC
		public override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			char[] array = null;
			return this.GetBytes(s, charIndex, charCount, bytes, byteIndex, ref encoderFallbackBuffer, ref array);
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x000D9910 File Offset: 0x000D7B10
		private int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex, ref EncoderFallbackBuffer buffer, ref char[] fallback_chars)
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
				throw new ArgumentOutOfRangeException("charIndex", Encoding._("ArgRange_StringIndex"));
			}
			if (charCount < 0 || charCount > s.Length - charIndex)
			{
				throw new ArgumentOutOfRangeException("charCount", Encoding._("ArgRange_StringRange"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Encoding._("ArgRange_Array"));
			}
			if (bytes.Length - byteIndex < charCount)
			{
				throw new ArgumentException(Encoding._("Arg_InsufficientSpace"));
			}
			int num = charCount;
			while (num-- > 0)
			{
				char c = s[charIndex++];
				if (c < 'Ā')
				{
					bytes[byteIndex++] = (byte)c;
				}
				else if (c >= '！' && c <= '～')
				{
					bytes[byteIndex++] = (byte)(c - 'ﻠ');
				}
				else
				{
					if (buffer == null)
					{
						buffer = base.EncoderFallback.CreateFallbackBuffer();
					}
					if (char.IsSurrogate(c) && num > 1 && char.IsSurrogate(s[charIndex]))
					{
						buffer.Fallback(c, s[charIndex], charIndex++ - 1);
					}
					else
					{
						buffer.Fallback(c, charIndex - 1);
					}
					if (fallback_chars == null || fallback_chars.Length < buffer.Remaining)
					{
						fallback_chars = new char[buffer.Remaining];
					}
					for (int i = 0; i < fallback_chars.Length; i++)
					{
						fallback_chars[i] = buffer.GetNextChar();
					}
					byteIndex += this.GetBytes(fallback_chars, 0, fallback_chars.Length, bytes, byteIndex, ref buffer, ref fallback_chars);
				}
			}
			return charCount;
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x000D9B10 File Offset: 0x000D7D10
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (index < 0 || index > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("index", Encoding._("ArgRange_Array"));
			}
			if (count < 0 || count > bytes.Length - index)
			{
				throw new ArgumentOutOfRangeException("count", Encoding._("ArgRange_Array"));
			}
			return count;
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x000D9B7C File Offset: 0x000D7D7C
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
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
				throw new ArgumentOutOfRangeException("byteIndex", Encoding._("ArgRange_Array"));
			}
			if (byteCount < 0 || byteCount > bytes.Length - byteIndex)
			{
				throw new ArgumentOutOfRangeException("byteCount", Encoding._("ArgRange_Array"));
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", Encoding._("ArgRange_Array"));
			}
			if (chars.Length - charIndex < byteCount)
			{
				throw new ArgumentException(Encoding._("Arg_InsufficientSpace"));
			}
			int num = byteCount;
			while (num-- > 0)
			{
				chars[charIndex++] = (char)bytes[byteIndex++];
			}
			return byteCount;
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x000D9C64 File Offset: 0x000D7E64
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", Encoding._("ArgRange_NonNegative"));
			}
			return charCount;
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x000D9C84 File Offset: 0x000D7E84
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Encoding._("ArgRange_NonNegative"));
			}
			return byteCount;
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x000D9CA4 File Offset: 0x000D7EA4
		public unsafe override string GetString(byte[] bytes, int index, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (index < 0 || index > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("index", Encoding._("ArgRange_Array"));
			}
			if (count < 0 || count > bytes.Length - index)
			{
				throw new ArgumentOutOfRangeException("count", Encoding._("ArgRange_Array"));
			}
			if (count == 0)
			{
				return string.Empty;
			}
			fixed (byte* ptr = ref (bytes != null && bytes.Length != 0) ? ref bytes[0] : ref *null)
			{
				string text = string.InternalAllocateStr(count);
				fixed (string text2 = text)
				{
					fixed (char* ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						byte* ptr3 = ptr + index;
						byte* ptr4 = ptr3 + count;
						char* ptr5 = ptr2;
						while (ptr3 < ptr4)
						{
							*(ptr5++) = (char)(*(ptr3++));
						}
						text2 = null;
						return text;
					}
				}
			}
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x000D9D7C File Offset: 0x000D7F7C
		public override string GetString(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			return this.GetString(bytes, 0, bytes.Length);
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06003F79 RID: 16249 RVA: 0x000D9D9C File Offset: 0x000D7F9C
		public override string BodyName
		{
			get
			{
				return "iso-8859-1";
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06003F7A RID: 16250 RVA: 0x000D9DA4 File Offset: 0x000D7FA4
		public override string EncodingName
		{
			get
			{
				return "Western European (ISO)";
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06003F7B RID: 16251 RVA: 0x000D9DAC File Offset: 0x000D7FAC
		public override string HeaderName
		{
			get
			{
				return "iso-8859-1";
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x000D9DB4 File Offset: 0x000D7FB4
		public override bool IsBrowserDisplay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06003F7D RID: 16253 RVA: 0x000D9DB8 File Offset: 0x000D7FB8
		public override bool IsBrowserSave
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06003F7E RID: 16254 RVA: 0x000D9DBC File Offset: 0x000D7FBC
		public override bool IsMailNewsDisplay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06003F7F RID: 16255 RVA: 0x000D9DC0 File Offset: 0x000D7FC0
		public override bool IsMailNewsSave
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06003F80 RID: 16256 RVA: 0x000D9DC4 File Offset: 0x000D7FC4
		public override string WebName
		{
			get
			{
				return "iso-8859-1";
			}
		}

		// Token: 0x04001B6D RID: 7021
		internal const int ISOLATIN_CODE_PAGE = 28591;
	}
}
