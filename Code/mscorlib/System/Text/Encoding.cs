using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Text
{
	/// <summary>Represents a character encoding.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000681 RID: 1665
	[ComVisible(true)]
	[Serializable]
	public abstract class Encoding : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.Encoding" /> class.</summary>
		// Token: 0x06003F17 RID: 16151 RVA: 0x000D7E9C File Offset: 0x000D609C
		protected Encoding()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.Encoding" /> class that corresponds to the specified code page.</summary>
		/// <param name="codePage">The code page identifier of the preferred encoding.-or- 0, to use the default encoding. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codePage" /> is less than zero. </exception>
		// Token: 0x06003F18 RID: 16152 RVA: 0x000D7EAC File Offset: 0x000D60AC
		protected Encoding(int codePage)
		{
			this.windows_code_page = codePage;
			this.codePage = codePage;
			if (codePage != 1200 && codePage != 1201 && codePage != 12000 && codePage != 12001 && codePage != 65000 && codePage != 65001)
			{
				if (codePage != 20127 && codePage != 54936)
				{
					this.decoder_fallback = DecoderFallback.ReplacementFallback;
					this.encoder_fallback = EncoderFallback.ReplacementFallback;
				}
				else
				{
					this.decoder_fallback = DecoderFallback.ReplacementFallback;
					this.encoder_fallback = EncoderFallback.ReplacementFallback;
				}
			}
			else
			{
				this.decoder_fallback = DecoderFallback.StandardSafeFallback;
				this.encoder_fallback = EncoderFallback.StandardSafeFallback;
			}
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x000D8150 File Offset: 0x000D6350
		internal static string _(string arg)
		{
			return arg;
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding is read-only.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> is read-only; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06003F1B RID: 16155 RVA: 0x000D8154 File Offset: 0x000D6354
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.is_readonly;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding uses single-byte code points.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> uses single-byte code points; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06003F1C RID: 16156 RVA: 0x000D815C File Offset: 0x000D635C
		[ComVisible(false)]
		public virtual bool IsSingleByte
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.DecoderFallback" /> object for the current <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <returns>The <see cref="T:System.Text.DecoderFallback" /> object for the current <see cref="T:System.Text.Encoding" /> object. </returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A value cannot be assigned in a set operation because the current <see cref="T:System.Text.Encoding" /> object is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06003F1D RID: 16157 RVA: 0x000D8160 File Offset: 0x000D6360
		// (set) Token: 0x06003F1E RID: 16158 RVA: 0x000D8168 File Offset: 0x000D6368
		[ComVisible(false)]
		public DecoderFallback DecoderFallback
		{
			get
			{
				return this.decoder_fallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("This Encoding is readonly.");
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.decoder_fallback = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.EncoderFallback" /> object for the current <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <returns>The <see cref="T:System.Text.EncoderFallback" /> object for the current <see cref="T:System.Text.Encoding" /> object. </returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A value cannot be assigned in a set operation because the current <see cref="T:System.Text.Encoding" /> object is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06003F1F RID: 16159 RVA: 0x000D8194 File Offset: 0x000D6394
		// (set) Token: 0x06003F20 RID: 16160 RVA: 0x000D819C File Offset: 0x000D639C
		[ComVisible(false)]
		public EncoderFallback EncoderFallback
		{
			get
			{
				return this.encoder_fallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("This Encoding is readonly.");
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.encoder_fallback = value;
			}
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x000D81C8 File Offset: 0x000D63C8
		internal void SetFallbackInternal(EncoderFallback e, DecoderFallback d)
		{
			if (e != null)
			{
				this.encoder_fallback = e;
			}
			if (d != null)
			{
				this.decoder_fallback = d;
			}
		}

		/// <summary>Converts an entire byte array from one encoding to another.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" /> containing the results of converting <paramref name="bytes" /> from <paramref name="srcEncoding" /> to <paramref name="dstEncoding" />.</returns>
		/// <param name="srcEncoding">The encoding format of <paramref name="bytes" />. </param>
		/// <param name="dstEncoding">The target encoding format. </param>
		/// <param name="bytes"></param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="srcEncoding" /> is null.-or- <paramref name="dstEncoding" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-srcEncoding.<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-dstEncoding.<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F22 RID: 16162 RVA: 0x000D81E4 File Offset: 0x000D63E4
		public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes)
		{
			if (srcEncoding == null)
			{
				throw new ArgumentNullException("srcEncoding");
			}
			if (dstEncoding == null)
			{
				throw new ArgumentNullException("dstEncoding");
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			return dstEncoding.GetBytes(srcEncoding.GetChars(bytes, 0, bytes.Length));
		}

		/// <summary>Converts a range of bytes in a byte array from one encoding to another.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" /> containing the result of converting a range of bytes in <paramref name="bytes" /> from <paramref name="srcEncoding" /> to <paramref name="dstEncoding" />.</returns>
		/// <param name="srcEncoding">The encoding of the source array, <paramref name="bytes" />. </param>
		/// <param name="dstEncoding">The encoding of the output array. </param>
		/// <param name="bytes">The array of bytes to convert. </param>
		/// <param name="index">The index of the first element of <paramref name="bytes" /> to convert. </param>
		/// <param name="count">The number of bytes to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="srcEncoding" /> is null.-or- <paramref name="dstEncoding" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the byte array. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-srcEncoding.<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-dstEncoding.<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F23 RID: 16163 RVA: 0x000D8238 File Offset: 0x000D6438
		public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes, int index, int count)
		{
			if (srcEncoding == null)
			{
				throw new ArgumentNullException("srcEncoding");
			}
			if (dstEncoding == null)
			{
				throw new ArgumentNullException("dstEncoding");
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (index < 0 || index > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("index", Encoding._("ArgRange_Array"));
			}
			if (count < 0 || bytes.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count", Encoding._("ArgRange_Array"));
			}
			return dstEncoding.GetBytes(srcEncoding.GetChars(bytes, index, count));
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current instance.</summary>
		/// <returns>true if <paramref name="value" /> is an instance of <see cref="T:System.Text.Encoding" /> and is equal to the current instance; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to compare with the current instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003F24 RID: 16164 RVA: 0x000D82D8 File Offset: 0x000D64D8
		public override bool Equals(object value)
		{
			Encoding encoding = value as Encoding;
			return encoding != null && (this.codePage == encoding.codePage && this.DecoderFallback.Equals(encoding.DecoderFallback)) && this.EncoderFallback.Equals(encoding.EncoderFallback);
		}

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding a set of characters from the specified character array.</summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="chars">The character array containing the set of characters to encode. </param>
		/// <param name="index">The index of the first character to encode. </param>
		/// <param name="count">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="chars" />. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F25 RID: 16165
		public abstract int GetByteCount(char[] chars, int index, int count);

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding the characters in the specified string.</summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="s">The string containing the set of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F26 RID: 16166 RVA: 0x000D8330 File Offset: 0x000D6530
		public unsafe virtual int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length == 0)
			{
				return 0;
			}
			fixed (char* chars = s + RuntimeHelpers.OffsetToStringData / 2)
			{
				return this.GetByteCount(chars, s.Length);
			}
		}

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding all the characters in the specified character array.</summary>
		/// <returns>The number of bytes produced by encoding all the characters in the specified character array.</returns>
		/// <param name="chars">The character array containing the characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F27 RID: 16167 RVA: 0x000D8374 File Offset: 0x000D6574
		public virtual int GetByteCount(char[] chars)
		{
			if (chars != null)
			{
				return this.GetByteCount(chars, 0, chars.Length);
			}
			throw new ArgumentNullException("chars");
		}

		/// <summary>When overridden in a derived class, encodes a set of characters from the specified character array into the specified byte array.</summary>
		/// <returns>The actual number of bytes written into <paramref name="bytes" />.</returns>
		/// <param name="chars">The character array containing the set of characters to encode. </param>
		/// <param name="charIndex">The index of the first character to encode. </param>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <param name="bytes">The byte array to contain the resulting sequence of bytes. </param>
		/// <param name="byteIndex">The index at which to start writing the resulting sequence of bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" /> or <paramref name="charCount" /> or <paramref name="byteIndex" /> is less than zero.-or- <paramref name="charIndex" /> and <paramref name="charCount" /> do not denote a valid range in <paramref name="chars" />.-or- <paramref name="byteIndex" /> is not a valid index in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F28 RID: 16168
		public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex);

		/// <summary>When overridden in a derived class, encodes a set of characters from the specified string into the specified byte array.</summary>
		/// <returns>The actual number of bytes written into <paramref name="bytes" />.</returns>
		/// <param name="s">The string containing the set of characters to encode. </param>
		/// <param name="charIndex">The index of the first character to encode. </param>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <param name="bytes">The byte array to contain the resulting sequence of bytes. </param>
		/// <param name="byteIndex">The index at which to start writing the resulting sequence of bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" /> or <paramref name="charCount" /> or <paramref name="byteIndex" /> is less than zero.-or- <paramref name="charIndex" /> and <paramref name="charCount" /> do not denote a valid range in <paramref name="chars" />.-or- <paramref name="byteIndex" /> is not a valid index in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F29 RID: 16169 RVA: 0x000D8394 File Offset: 0x000D6594
		public unsafe virtual int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (charIndex < 0 || charIndex > s.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", Encoding._("ArgRange_Array"));
			}
			if (charCount < 0 || charIndex > s.Length - charCount)
			{
				throw new ArgumentOutOfRangeException("charCount", Encoding._("ArgRange_Array"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Encoding._("ArgRange_Array"));
			}
			if (charCount == 0 || bytes.Length == byteIndex)
			{
				return 0;
			}
			fixed (char* ptr = s + RuntimeHelpers.OffsetToStringData / 2)
			{
				fixed (byte* ptr2 = ref (bytes != null && bytes.Length != 0) ? ref bytes[0] : ref *null)
				{
					return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, bytes.Length - byteIndex);
				}
			}
		}

		/// <summary>When overridden in a derived class, encodes all the characters in the specified string into a sequence of bytes.</summary>
		/// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
		/// <param name="s"></param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F2A RID: 16170 RVA: 0x000D8484 File Offset: 0x000D6684
		public unsafe virtual byte[] GetBytes(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length == 0)
			{
				return new byte[0];
			}
			int byteCount = this.GetByteCount(s);
			if (byteCount == 0)
			{
				return new byte[0];
			}
			fixed (char* chars = s + RuntimeHelpers.OffsetToStringData / 2)
			{
				byte[] array = new byte[byteCount];
				fixed (byte* bytes = ref (array != null && array.Length != 0) ? ref array[0] : ref *null)
				{
					this.GetBytes(chars, s.Length, bytes, byteCount);
					return array;
				}
			}
		}

		/// <summary>When overridden in a derived class, encodes a set of characters from the specified character array into a sequence of bytes.</summary>
		/// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
		/// <param name="chars">The character array containing the set of characters to encode. </param>
		/// <param name="index">The index of the first character to encode. </param>
		/// <param name="count">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="chars" />. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F2B RID: 16171 RVA: 0x000D850C File Offset: 0x000D670C
		public virtual byte[] GetBytes(char[] chars, int index, int count)
		{
			int byteCount = this.GetByteCount(chars, index, count);
			byte[] array = new byte[byteCount];
			this.GetBytes(chars, index, count, array, 0);
			return array;
		}

		/// <summary>When overridden in a derived class, encodes all the characters in the specified character array into a sequence of bytes.</summary>
		/// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
		/// <param name="chars">The character array containing the characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F2C RID: 16172 RVA: 0x000D8538 File Offset: 0x000D6738
		public virtual byte[] GetBytes(char[] chars)
		{
			int byteCount = this.GetByteCount(chars, 0, chars.Length);
			byte[] array = new byte[byteCount];
			this.GetBytes(chars, 0, chars.Length, array, 0);
			return array;
		}

		/// <summary>When overridden in a derived class, calculates the number of characters produced by decoding a sequence of bytes from the specified byte array.</summary>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="index">The index of the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F2D RID: 16173
		public abstract int GetCharCount(byte[] bytes, int index, int count);

		/// <summary>When overridden in a derived class, calculates the number of characters produced by decoding all the bytes in the specified byte array.</summary>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F2E RID: 16174 RVA: 0x000D8568 File Offset: 0x000D6768
		public virtual int GetCharCount(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			return this.GetCharCount(bytes, 0, bytes.Length);
		}

		/// <summary>When overridden in a derived class, decodes a sequence of bytes from the specified byte array into the specified character array.</summary>
		/// <returns>The actual number of characters written into <paramref name="chars" />.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="byteIndex">The index of the first byte to decode. </param>
		/// <param name="byteCount">The number of bytes to decode. </param>
		/// <param name="chars">The character array to contain the resulting set of characters. </param>
		/// <param name="charIndex">The index at which to start writing the resulting set of characters. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null.-or- <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteIndex" /> or <paramref name="byteCount" /> or <paramref name="charIndex" /> is less than zero.-or- <paramref name="byteindex" /> and <paramref name="byteCount" /> do not denote a valid range in <paramref name="bytes" />.-or- <paramref name="charIndex" /> is not a valid index in <paramref name="chars" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="chars" /> does not have enough capacity from <paramref name="charIndex" /> to the end of the array to accommodate the resulting characters. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F2F RID: 16175
		public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

		/// <summary>When overridden in a derived class, decodes a sequence of bytes from the specified byte array into a set of characters.</summary>
		/// <returns>A character array containing the results of decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="index">The index of the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F30 RID: 16176 RVA: 0x000D8588 File Offset: 0x000D6788
		public virtual char[] GetChars(byte[] bytes, int index, int count)
		{
			int charCount = this.GetCharCount(bytes, index, count);
			char[] array = new char[charCount];
			this.GetChars(bytes, index, count, array, 0);
			return array;
		}

		/// <summary>When overridden in a derived class, decodes all the bytes in the specified byte array into a set of characters.</summary>
		/// <returns>A character array containing the results of decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F31 RID: 16177 RVA: 0x000D85B4 File Offset: 0x000D67B4
		public virtual char[] GetChars(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			int charCount = this.GetCharCount(bytes, 0, bytes.Length);
			char[] array = new char[charCount];
			this.GetChars(bytes, 0, bytes.Length, array, 0);
			return array;
		}

		/// <summary>When overridden in a derived class, obtains a decoder that converts an encoded sequence of bytes into a sequence of characters.</summary>
		/// <returns>A <see cref="T:System.Text.Decoder" /> that converts an encoded sequence of bytes into a sequence of characters.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F32 RID: 16178 RVA: 0x000D85F4 File Offset: 0x000D67F4
		public virtual Decoder GetDecoder()
		{
			return new Encoding.ForwardingDecoder(this);
		}

		/// <summary>When overridden in a derived class, obtains an encoder that converts a sequence of Unicode characters into an encoded sequence of bytes.</summary>
		/// <returns>A <see cref="T:System.Text.Encoder" /> that converts a sequence of Unicode characters into an encoded sequence of bytes.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F33 RID: 16179 RVA: 0x000D85FC File Offset: 0x000D67FC
		public virtual Encoder GetEncoder()
		{
			return new Encoding.ForwardingEncoder(this);
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x000D8604 File Offset: 0x000D6804
		private static object InvokeI18N(string name, params object[] args)
		{
			object obj = Encoding.lockobj;
			object result;
			lock (obj)
			{
				if (Encoding.i18nDisabled)
				{
					result = null;
				}
				else
				{
					if (Encoding.i18nAssembly == null)
					{
						try
						{
							try
							{
								Encoding.i18nAssembly = Assembly.Load("I18N, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756");
							}
							catch (NotImplementedException)
							{
								Encoding.i18nDisabled = true;
								return null;
							}
							if (Encoding.i18nAssembly == null)
							{
								return null;
							}
						}
						catch (SystemException)
						{
							return null;
						}
					}
					Type type;
					try
					{
						type = Encoding.i18nAssembly.GetType("I18N.Common.Manager");
					}
					catch (NotImplementedException)
					{
						Encoding.i18nDisabled = true;
						return null;
					}
					if (type == null)
					{
						result = null;
					}
					else
					{
						object obj2;
						try
						{
							obj2 = type.InvokeMember("PrimaryManager", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, null, null, null, null);
							if (obj2 == null)
							{
								return null;
							}
						}
						catch (MissingMethodException)
						{
							return null;
						}
						catch (SecurityException)
						{
							return null;
						}
						catch (NotImplementedException)
						{
							Encoding.i18nDisabled = true;
							return null;
						}
						try
						{
							result = type.InvokeMember(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, obj2, args, null, null, null);
						}
						catch (MissingMethodException)
						{
							result = null;
						}
						catch (SecurityException)
						{
							result = null;
						}
					}
				}
			}
			return result;
		}

		/// <summary>Returns the encoding associated with the specified code page identifier.</summary>
		/// <returns>The <see cref="T:System.Text.Encoding" /> associated with the specified code page.</returns>
		/// <param name="codepage">The code page identifier of the preferred encoding.-or- 0, to use the default encoding. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codepage" /> is less than zero or greater than 65535. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codepage" /> is not supported by the underlying platform. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="codepage" /> is not supported by the underlying platform. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F35 RID: 16181 RVA: 0x000D8830 File Offset: 0x000D6A30
		public static Encoding GetEncoding(int codepage)
		{
			if (codepage < 0 || codepage > 65535)
			{
				throw new ArgumentOutOfRangeException("codepage", "Valid values are between 0 and 65535, inclusive.");
			}
			int num = codepage;
			if (num == 1200)
			{
				return Encoding.Unicode;
			}
			if (num == 1201)
			{
				return Encoding.BigEndianUnicode;
			}
			if (num == 12000)
			{
				return Encoding.UTF32;
			}
			if (num == 12001)
			{
				return Encoding.BigEndianUTF32;
			}
			if (num == 65000)
			{
				return Encoding.UTF7;
			}
			if (num == 65001)
			{
				return Encoding.UTF8;
			}
			if (num == 0)
			{
				return Encoding.Default;
			}
			if (num == 20127)
			{
				return Encoding.ASCII;
			}
			if (num == 28591)
			{
				return Encoding.ISOLatin1;
			}
			Encoding encoding = (Encoding)Encoding.InvokeI18N("GetEncoding", new object[]
			{
				codepage
			});
			if (encoding != null)
			{
				encoding.is_readonly = true;
				return encoding;
			}
			string text = "System.Text.CP" + codepage.ToString();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Type type = executingAssembly.GetType(text);
			if (type != null)
			{
				encoding = (Encoding)Activator.CreateInstance(type);
				encoding.is_readonly = true;
				return encoding;
			}
			type = Type.GetType(text);
			if (type != null)
			{
				encoding = (Encoding)Activator.CreateInstance(type);
				encoding.is_readonly = true;
				return encoding;
			}
			throw new NotSupportedException(string.Format("CodePage {0} not supported", codepage.ToString()));
		}

		/// <summary>When overridden in a derived class, creates a shallow copy of the current <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <returns>A copy of the current <see cref="T:System.Text.Encoding" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003F36 RID: 16182 RVA: 0x000D89AC File Offset: 0x000D6BAC
		[ComVisible(false)]
		public virtual object Clone()
		{
			Encoding encoding = (Encoding)base.MemberwiseClone();
			encoding.is_readonly = false;
			return encoding;
		}

		/// <summary>Returns the encoding associated with the specified code page identifier. Parameters specify an error handler for characters that cannot be encoded and byte sequences that cannot be decoded.</summary>
		/// <returns>The <see cref="T:System.Text.Encoding" /> object associated with the specified code page.</returns>
		/// <param name="codepage">The code page identifier of the preferred encoding.-or- 0, to use the default encoding. </param>
		/// <param name="encoderFallback">A <see cref="T:System.Text.EncoderFallback" /> object that provides an error handling procedure when a character cannot be encoded with the current encoding. </param>
		/// <param name="decoderFallback">A <see cref="T:System.Text.DecoderFallback" /> object that provides an error handling procedure when a byte sequence cannot be decoded with the current encoding. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codepage" /> is less than zero or greater than 65535. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codepage" /> is not supported by the underlying platform. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="codepage" /> is not supported by the underlying platform. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F37 RID: 16183 RVA: 0x000D89D0 File Offset: 0x000D6BD0
		public static Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			if (encoderFallback == null)
			{
				throw new ArgumentNullException("encoderFallback");
			}
			if (decoderFallback == null)
			{
				throw new ArgumentNullException("decoderFallback");
			}
			Encoding encoding = Encoding.GetEncoding(codepage).Clone() as Encoding;
			encoding.is_readonly = false;
			encoding.encoder_fallback = encoderFallback;
			encoding.decoder_fallback = decoderFallback;
			return encoding;
		}

		/// <summary>Returns the encoding associated with the specified code page name. Parameters specify an error handler for characters that cannot be encoded and byte sequences that cannot be decoded.</summary>
		/// <returns>The <see cref="T:System.Text.Encoding" /> object associated with the specified code page.</returns>
		/// <param name="name">The code page name of the preferred encoding. </param>
		/// <param name="encoderFallback">A <see cref="T:System.Text.EncoderFallback" /> object that provides an error handling procedure when a character cannot be encoded with the current encoding. </param>
		/// <param name="decoderFallback">A <see cref="T:System.Text.DecoderFallback" /> object that provides an error handling procedure when a byte sequence cannot be decoded with the current encoding. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid code page name.-or- The code page indicated by <paramref name="name" /> is not supported by the underlying platform. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F38 RID: 16184 RVA: 0x000D8A28 File Offset: 0x000D6C28
		public static Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			if (encoderFallback == null)
			{
				throw new ArgumentNullException("encoderFallback");
			}
			if (decoderFallback == null)
			{
				throw new ArgumentNullException("decoderFallback");
			}
			Encoding encoding = Encoding.GetEncoding(name).Clone() as Encoding;
			encoding.is_readonly = false;
			encoding.encoder_fallback = encoderFallback;
			encoding.decoder_fallback = decoderFallback;
			return encoding;
		}

		/// <summary>Returns an array containing all encodings.</summary>
		/// <returns>An array of type <see cref="T:System.Text.EncodingInfo" /> containing all encodings.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F39 RID: 16185 RVA: 0x000D8A80 File Offset: 0x000D6C80
		public static EncodingInfo[] GetEncodings()
		{
			if (Encoding.encoding_infos == null)
			{
				int[] array = new int[]
				{
					37,
					437,
					500,
					708,
					850,
					852,
					855,
					857,
					858,
					860,
					861,
					862,
					863,
					864,
					865,
					866,
					869,
					870,
					874,
					875,
					932,
					936,
					949,
					950,
					1026,
					1047,
					1140,
					1141,
					1142,
					1143,
					1144,
					1145,
					1146,
					1147,
					1148,
					1149,
					1200,
					1201,
					1250,
					1251,
					1252,
					1253,
					1254,
					1255,
					1256,
					1257,
					1258,
					10000,
					10079,
					12000,
					12001,
					20127,
					20273,
					20277,
					20278,
					20280,
					20284,
					20285,
					20290,
					20297,
					20420,
					20424,
					20866,
					20871,
					21025,
					21866,
					28591,
					28592,
					28593,
					28594,
					28595,
					28596,
					28597,
					28598,
					28599,
					28605,
					38598,
					50220,
					50221,
					50222,
					51932,
					51949,
					54936,
					57002,
					57003,
					57004,
					57005,
					57006,
					57007,
					57008,
					57009,
					57010,
					57011,
					65000,
					65001
				};
				Encoding.encoding_infos = new EncodingInfo[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					Encoding.encoding_infos[i] = new EncodingInfo(array[i]);
				}
			}
			return Encoding.encoding_infos;
		}

		/// <summary>Gets a value indicating whether the current encoding is always normalized, using the default normalization form.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> is always normalized; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003F3A RID: 16186 RVA: 0x000D8AE0 File Offset: 0x000D6CE0
		[ComVisible(false)]
		public bool IsAlwaysNormalized()
		{
			return this.IsAlwaysNormalized(NormalizationForm.FormC);
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding is always normalized, using the specified normalization form.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> object is always normalized using the specified <see cref="T:System.Text.NormalizationForm" /> value; otherwise, false. The default is false.</returns>
		/// <param name="form">One of the <see cref="T:System.Text.NormalizationForm" /> values. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003F3B RID: 16187 RVA: 0x000D8AEC File Offset: 0x000D6CEC
		[ComVisible(false)]
		public virtual bool IsAlwaysNormalized(NormalizationForm form)
		{
			return form == NormalizationForm.FormC && this is ASCIIEncoding;
		}

		/// <summary>Returns an encoding associated with the specified code page name.</summary>
		/// <returns>The <see cref="T:System.Text.Encoding" /> associated with the specified code page.</returns>
		/// <param name="name">The code page name of the preferred encoding. Any value returned by <see cref="P:System.Text.Encoding.WebName" /> is a valid input. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid code page name.-or- The code page indicated by <paramref name="name" /> is not supported by the underlying platform. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F3C RID: 16188 RVA: 0x000D8B04 File Offset: 0x000D6D04
		public static Encoding GetEncoding(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			string text = name.ToLowerInvariant().Replace('-', '_');
			int codepage = 0;
			for (int i = 0; i < Encoding.encodings.Length; i++)
			{
				object obj = Encoding.encodings[i];
				if (obj is int)
				{
					codepage = (int)obj;
				}
				else if (text == (string)Encoding.encodings[i])
				{
					return Encoding.GetEncoding(codepage);
				}
			}
			Encoding encoding = (Encoding)Encoding.InvokeI18N("GetEncoding", new object[]
			{
				name
			});
			if (encoding != null)
			{
				return encoding;
			}
			string text2 = "System.Text.ENC" + text;
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Type type = executingAssembly.GetType(text2);
			if (type != null)
			{
				return (Encoding)Activator.CreateInstance(type);
			}
			type = Type.GetType(text2);
			if (type != null)
			{
				return (Encoding)Activator.CreateInstance(type);
			}
			throw new ArgumentException(string.Format("Encoding name '{0}' not supported", name), "name");
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>The hash code for the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F3D RID: 16189 RVA: 0x000D8C14 File Offset: 0x000D6E14
		public override int GetHashCode()
		{
			return this.DecoderFallback.GetHashCode() << 24 + this.EncoderFallback.GetHashCode() << 16 + this.codePage;
		}

		/// <summary>When overridden in a derived class, calculates the maximum number of bytes produced by encoding the specified number of characters.</summary>
		/// <returns>The maximum number of bytes produced by encoding the specified number of characters.</returns>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> is less than zero. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F3E RID: 16190
		public abstract int GetMaxByteCount(int charCount);

		/// <summary>When overridden in a derived class, calculates the maximum number of characters produced by decoding the specified number of bytes.</summary>
		/// <returns>The maximum number of characters produced by decoding the specified number of bytes.</returns>
		/// <param name="byteCount">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> is less than zero. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F3F RID: 16191
		public abstract int GetMaxCharCount(int byteCount);

		/// <summary>When overridden in a derived class, returns a sequence of bytes that specifies the encoding used.</summary>
		/// <returns>A byte array containing a sequence of bytes that specifies the encoding used.-or- A byte array of length zero, if a preamble is not required.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F40 RID: 16192 RVA: 0x000D8C4C File Offset: 0x000D6E4C
		public virtual byte[] GetPreamble()
		{
			return new byte[0];
		}

		/// <summary>When overridden in a derived class, decodes a sequence of bytes from the specified byte array into a string.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the results of decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="index">The index of the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F41 RID: 16193 RVA: 0x000D8C54 File Offset: 0x000D6E54
		public virtual string GetString(byte[] bytes, int index, int count)
		{
			return new string(this.GetChars(bytes, index, count));
		}

		/// <summary>When overridden in a derived class, decodes all the bytes in the specified byte array into a string.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the results of decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F42 RID: 16194 RVA: 0x000D8C64 File Offset: 0x000D6E64
		public virtual string GetString(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			return this.GetString(bytes, 0, bytes.Length);
		}

		/// <summary>When overridden in a derived class, gets a name for the current encoding that can be used with mail agent body tags.</summary>
		/// <returns>A name for the current <see cref="T:System.Text.Encoding" /> that can be used with mail agent body tags.-or- An empty string (""), if the current <see cref="T:System.Text.Encoding" /> cannot be used.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06003F43 RID: 16195 RVA: 0x000D8C84 File Offset: 0x000D6E84
		public virtual string BodyName
		{
			get
			{
				return this.body_name;
			}
		}

		/// <summary>When overridden in a derived class, gets the code page identifier of the current <see cref="T:System.Text.Encoding" />.</summary>
		/// <returns>The code page identifier of the current <see cref="T:System.Text.Encoding" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06003F44 RID: 16196 RVA: 0x000D8C8C File Offset: 0x000D6E8C
		public virtual int CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		/// <summary>When overridden in a derived class, gets the human-readable description of the current encoding.</summary>
		/// <returns>The human-readable description of the current <see cref="T:System.Text.Encoding" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06003F45 RID: 16197 RVA: 0x000D8C94 File Offset: 0x000D6E94
		public virtual string EncodingName
		{
			get
			{
				return this.encoding_name;
			}
		}

		/// <summary>When overridden in a derived class, gets a name for the current encoding that can be used with mail agent header tags.</summary>
		/// <returns>A name for the current <see cref="T:System.Text.Encoding" /> to use with mail agent header tags.-or- An empty string (""), if the current <see cref="T:System.Text.Encoding" /> cannot be used.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06003F46 RID: 16198 RVA: 0x000D8C9C File Offset: 0x000D6E9C
		public virtual string HeaderName
		{
			get
			{
				return this.header_name;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding can be used by browser clients for displaying content.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> can be used by browser clients for displaying content; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06003F47 RID: 16199 RVA: 0x000D8CA4 File Offset: 0x000D6EA4
		public virtual bool IsBrowserDisplay
		{
			get
			{
				return this.is_browser_display;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding can be used by browser clients for saving content.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> can be used by browser clients for saving content; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06003F48 RID: 16200 RVA: 0x000D8CAC File Offset: 0x000D6EAC
		public virtual bool IsBrowserSave
		{
			get
			{
				return this.is_browser_save;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding can be used by mail and news clients for displaying content.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> can be used by mail and news clients for displaying content; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06003F49 RID: 16201 RVA: 0x000D8CB4 File Offset: 0x000D6EB4
		public virtual bool IsMailNewsDisplay
		{
			get
			{
				return this.is_mail_news_display;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding can be used by mail and news clients for saving content.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> can be used by mail and news clients for saving content; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06003F4A RID: 16202 RVA: 0x000D8CBC File Offset: 0x000D6EBC
		public virtual bool IsMailNewsSave
		{
			get
			{
				return this.is_mail_news_save;
			}
		}

		/// <summary>When overridden in a derived class, gets the name registered with the Internet Assigned Numbers Authority (IANA) for the current encoding.</summary>
		/// <returns>The IANA name for the current <see cref="T:System.Text.Encoding" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x000D8CC4 File Offset: 0x000D6EC4
		public virtual string WebName
		{
			get
			{
				return this.web_name;
			}
		}

		/// <summary>When overridden in a derived class, gets the Windows operating system code page that most closely corresponds to the current encoding.</summary>
		/// <returns>The Windows operating system code page that most closely corresponds to the current <see cref="T:System.Text.Encoding" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06003F4C RID: 16204 RVA: 0x000D8CCC File Offset: 0x000D6ECC
		public virtual int WindowsCodePage
		{
			get
			{
				return this.windows_code_page;
			}
		}

		/// <summary>Gets an encoding for the ASCII (7-bit) character set.</summary>
		/// <returns>An encoding for the ASCII (7-bit) character set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06003F4D RID: 16205 RVA: 0x000D8CD4 File Offset: 0x000D6ED4
		public static Encoding ASCII
		{
			get
			{
				if (Encoding.asciiEncoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.asciiEncoding == null)
						{
							Encoding.asciiEncoding = new ASCIIEncoding();
						}
					}
				}
				return Encoding.asciiEncoding;
			}
		}

		/// <summary>Gets an encoding for the UTF-16 format using the big endian byte order.</summary>
		/// <returns>An encoding object for the UTF-16 format using the big endian byte order.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06003F4E RID: 16206 RVA: 0x000D8D40 File Offset: 0x000D6F40
		public static Encoding BigEndianUnicode
		{
			get
			{
				if (Encoding.bigEndianEncoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.bigEndianEncoding == null)
						{
							Encoding.bigEndianEncoding = new UnicodeEncoding(true, true);
						}
					}
				}
				return Encoding.bigEndianEncoding;
			}
		}

		// Token: 0x06003F4F RID: 16207
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string InternalCodePage(ref int code_page);

		/// <summary>Gets an encoding for the operating system's current ANSI code page.</summary>
		/// <returns>An encoding for the operating system's current ANSI code page.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06003F50 RID: 16208 RVA: 0x000D8DB0 File Offset: 0x000D6FB0
		public static Encoding Default
		{
			get
			{
				if (Encoding.defaultEncoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.defaultEncoding == null)
						{
							int num = 1;
							string name = Encoding.InternalCodePage(ref num);
							try
							{
								if (num == -1)
								{
									Encoding.defaultEncoding = Encoding.GetEncoding(name);
								}
								else
								{
									num &= 268435455;
									switch (num)
									{
									case 1:
										num = 20127;
										break;
									case 2:
										num = 65000;
										break;
									case 3:
										num = 65001;
										break;
									case 4:
										num = 1200;
										break;
									case 5:
										num = 1201;
										break;
									case 6:
										num = 28591;
										break;
									}
									Encoding.defaultEncoding = Encoding.GetEncoding(num);
								}
							}
							catch (NotSupportedException)
							{
								Encoding.defaultEncoding = Encoding.UTF8Unmarked;
							}
							catch (ArgumentException)
							{
								Encoding.defaultEncoding = Encoding.UTF8Unmarked;
							}
							Encoding.defaultEncoding.is_readonly = true;
						}
					}
				}
				return Encoding.defaultEncoding;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06003F51 RID: 16209 RVA: 0x000D8F18 File Offset: 0x000D7118
		private static Encoding ISOLatin1
		{
			get
			{
				if (Encoding.isoLatin1Encoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.isoLatin1Encoding == null)
						{
							Encoding.isoLatin1Encoding = new Latin1Encoding();
						}
					}
				}
				return Encoding.isoLatin1Encoding;
			}
		}

		/// <summary>Gets an encoding for the UTF-7 format.</summary>
		/// <returns>A <see cref="T:System.Text.Encoding" /> for the UTF-7 format.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06003F52 RID: 16210 RVA: 0x000D8F84 File Offset: 0x000D7184
		public static Encoding UTF7
		{
			get
			{
				if (Encoding.utf7Encoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.utf7Encoding == null)
						{
							Encoding.utf7Encoding = new UTF7Encoding();
						}
					}
				}
				return Encoding.utf7Encoding;
			}
		}

		/// <summary>Gets an encoding for the UTF-8 format.</summary>
		/// <returns>An encoding for the UTF-8 format.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06003F53 RID: 16211 RVA: 0x000D8FF0 File Offset: 0x000D71F0
		public static Encoding UTF8
		{
			get
			{
				if (Encoding.utf8EncodingWithMarkers == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.utf8EncodingWithMarkers == null)
						{
							Encoding.utf8EncodingWithMarkers = new UTF8Encoding(true);
						}
					}
				}
				return Encoding.utf8EncodingWithMarkers;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06003F54 RID: 16212 RVA: 0x000D9060 File Offset: 0x000D7260
		internal static Encoding UTF8Unmarked
		{
			get
			{
				if (Encoding.utf8EncodingWithoutMarkers == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.utf8EncodingWithoutMarkers == null)
						{
							Encoding.utf8EncodingWithoutMarkers = new UTF8Encoding(false, false);
						}
					}
				}
				return Encoding.utf8EncodingWithoutMarkers;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06003F55 RID: 16213 RVA: 0x000D90D0 File Offset: 0x000D72D0
		internal static Encoding UTF8UnmarkedUnsafe
		{
			get
			{
				if (Encoding.utf8EncodingUnsafe == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.utf8EncodingUnsafe == null)
						{
							Encoding.utf8EncodingUnsafe = new UTF8Encoding(false, false);
							Encoding.utf8EncodingUnsafe.is_readonly = false;
							Encoding.utf8EncodingUnsafe.DecoderFallback = new DecoderReplacementFallback(string.Empty);
							Encoding.utf8EncodingUnsafe.is_readonly = true;
						}
					}
				}
				return Encoding.utf8EncodingUnsafe;
			}
		}

		/// <summary>Gets an encoding for the UTF-16 format using the little endian byte order.</summary>
		/// <returns>An encoding for the UTF-16 format using the little endian byte order.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06003F56 RID: 16214 RVA: 0x000D9170 File Offset: 0x000D7370
		public static Encoding Unicode
		{
			get
			{
				if (Encoding.unicodeEncoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.unicodeEncoding == null)
						{
							Encoding.unicodeEncoding = new UnicodeEncoding(false, true);
						}
					}
				}
				return Encoding.unicodeEncoding;
			}
		}

		/// <summary>Gets an encoding for the UTF-32 format using the little endian byte order.</summary>
		/// <returns>An encoding object for the UTF-32 format using the little endian byte order.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06003F57 RID: 16215 RVA: 0x000D91E0 File Offset: 0x000D73E0
		public static Encoding UTF32
		{
			get
			{
				if (Encoding.utf32Encoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.utf32Encoding == null)
						{
							Encoding.utf32Encoding = new UTF32Encoding(false, true);
						}
					}
				}
				return Encoding.utf32Encoding;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06003F58 RID: 16216 RVA: 0x000D9250 File Offset: 0x000D7450
		internal static Encoding BigEndianUTF32
		{
			get
			{
				if (Encoding.bigEndianUTF32Encoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.bigEndianUTF32Encoding == null)
						{
							Encoding.bigEndianUTF32Encoding = new UTF32Encoding(true, true);
						}
					}
				}
				return Encoding.bigEndianUTF32Encoding;
			}
		}

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding a set of characters starting at the specified character pointer.</summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="chars">A pointer to the first character to encode. </param>
		/// <param name="count">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F59 RID: 16217 RVA: 0x000D92C0 File Offset: 0x000D74C0
		[ComVisible(false)]
		[CLSCompliant(false)]
		public unsafe virtual int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = chars[i];
			}
			return this.GetByteCount(array);
		}

		/// <summary>When overridden in a derived class, calculates the number of characters produced by decoding a sequence of bytes starting at the specified byte pointer.</summary>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">A pointer to the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F5A RID: 16218 RVA: 0x000D931C File Offset: 0x000D751C
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = bytes[i];
			}
			return this.GetCharCount(array, 0, count);
		}

		/// <summary>When overridden in a derived class, decodes a sequence of bytes starting at the specified byte pointer into a set of characters that are stored starting at the specified character pointer.</summary>
		/// <returns>The actual number of characters written at the location indicated by the <paramref name="chars" /> parameter.</returns>
		/// <param name="bytes">A pointer to the first byte to decode. </param>
		/// <param name="byteCount">The number of bytes to decode. </param>
		/// <param name="chars">A pointer to the location at which to start writing the resulting set of characters. </param>
		/// <param name="charCount">The maximum number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null.-or- <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> or <paramref name="charCount" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="charCount" /> is less than the resulting number of characters. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F5B RID: 16219 RVA: 0x000D9378 File Offset: 0x000D7578
		[ComVisible(false)]
		[CLSCompliant(false)]
		public unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount");
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			byte[] array = new byte[byteCount];
			for (int i = 0; i < byteCount; i++)
			{
				array[i] = bytes[i];
			}
			char[] chars2 = this.GetChars(array, 0, byteCount);
			int num = chars2.Length;
			if (num > charCount)
			{
				throw new ArgumentException("charCount is less than the number of characters produced", "charCount");
			}
			for (int j = 0; j < num; j++)
			{
				chars[j] = chars2[j];
			}
			return num;
		}

		/// <summary>When overridden in a derived class, encodes a set of characters starting at the specified character pointer into a sequence of bytes that are stored starting at the specified byte pointer.</summary>
		/// <returns>The actual number of bytes written at the location indicated by the <paramref name="bytes" /> parameter.</returns>
		/// <param name="chars">A pointer to the first character to encode. </param>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <param name="bytes">A pointer to the location at which to start writing the resulting sequence of bytes. </param>
		/// <param name="byteCount">The maximum number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> or <paramref name="byteCount" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="byteCount" /> is less than the resulting number of bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Understanding Encodings for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F5C RID: 16220 RVA: 0x000D9434 File Offset: 0x000D7634
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount");
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			char[] array = new char[charCount];
			for (int i = 0; i < charCount; i++)
			{
				array[i] = chars[i];
			}
			byte[] bytes2 = this.GetBytes(array, 0, charCount);
			int num = bytes2.Length;
			if (num > byteCount)
			{
				throw new ArgumentException("byteCount is less that the number of bytes produced", "byteCount");
			}
			for (int j = 0; j < num; j++)
			{
				bytes[j] = bytes2[j];
			}
			return bytes2.Length;
		}

		// Token: 0x04001B4C RID: 6988
		internal int codePage;

		// Token: 0x04001B4D RID: 6989
		internal int windows_code_page;

		// Token: 0x04001B4E RID: 6990
		private bool is_readonly = true;

		// Token: 0x04001B4F RID: 6991
		private DecoderFallback decoder_fallback;

		// Token: 0x04001B50 RID: 6992
		private EncoderFallback encoder_fallback;

		// Token: 0x04001B51 RID: 6993
		private static Assembly i18nAssembly;

		// Token: 0x04001B52 RID: 6994
		private static bool i18nDisabled;

		// Token: 0x04001B53 RID: 6995
		private static EncodingInfo[] encoding_infos;

		// Token: 0x04001B54 RID: 6996
		private static readonly object[] encodings = new object[]
		{
			20127,
			"ascii",
			"us_ascii",
			"us",
			"ansi_x3.4_1968",
			"ansi_x3.4_1986",
			"cp367",
			"csascii",
			"ibm367",
			"iso_ir_6",
			"iso646_us",
			"iso_646.irv:1991",
			65000,
			"utf_7",
			"csunicode11utf7",
			"unicode_1_1_utf_7",
			"unicode_2_0_utf_7",
			"x_unicode_1_1_utf_7",
			"x_unicode_2_0_utf_7",
			65001,
			"utf_8",
			"unicode_1_1_utf_8",
			"unicode_2_0_utf_8",
			"x_unicode_1_1_utf_8",
			"x_unicode_2_0_utf_8",
			1200,
			"utf_16",
			"UTF_16LE",
			"ucs_2",
			"unicode",
			"iso_10646_ucs2",
			1201,
			"unicodefffe",
			"utf_16be",
			12000,
			"utf_32",
			"UTF_32LE",
			"ucs_4",
			12001,
			"UTF_32BE",
			28591,
			"iso_8859_1",
			"latin1"
		};

		// Token: 0x04001B55 RID: 6997
		internal string body_name;

		// Token: 0x04001B56 RID: 6998
		internal string encoding_name;

		// Token: 0x04001B57 RID: 6999
		internal string header_name;

		// Token: 0x04001B58 RID: 7000
		internal bool is_mail_news_display;

		// Token: 0x04001B59 RID: 7001
		internal bool is_mail_news_save;

		// Token: 0x04001B5A RID: 7002
		internal bool is_browser_save;

		// Token: 0x04001B5B RID: 7003
		internal bool is_browser_display;

		// Token: 0x04001B5C RID: 7004
		internal string web_name;

		// Token: 0x04001B5D RID: 7005
		private static volatile Encoding asciiEncoding;

		// Token: 0x04001B5E RID: 7006
		private static volatile Encoding bigEndianEncoding;

		// Token: 0x04001B5F RID: 7007
		private static volatile Encoding defaultEncoding;

		// Token: 0x04001B60 RID: 7008
		private static volatile Encoding utf7Encoding;

		// Token: 0x04001B61 RID: 7009
		private static volatile Encoding utf8EncodingWithMarkers;

		// Token: 0x04001B62 RID: 7010
		private static volatile Encoding utf8EncodingWithoutMarkers;

		// Token: 0x04001B63 RID: 7011
		private static volatile Encoding unicodeEncoding;

		// Token: 0x04001B64 RID: 7012
		private static volatile Encoding isoLatin1Encoding;

		// Token: 0x04001B65 RID: 7013
		private static volatile Encoding utf8EncodingUnsafe;

		// Token: 0x04001B66 RID: 7014
		private static volatile Encoding utf32Encoding;

		// Token: 0x04001B67 RID: 7015
		private static volatile Encoding bigEndianUTF32Encoding;

		// Token: 0x04001B68 RID: 7016
		private static readonly object lockobj = new object();

		// Token: 0x02000682 RID: 1666
		private sealed class ForwardingDecoder : Decoder
		{
			// Token: 0x06003F5D RID: 16221 RVA: 0x000D94F4 File Offset: 0x000D76F4
			public ForwardingDecoder(Encoding enc)
			{
				this.encoding = enc;
				DecoderFallback decoderFallback = this.encoding.DecoderFallback;
				if (decoderFallback != null)
				{
					base.Fallback = decoderFallback;
				}
			}

			// Token: 0x06003F5E RID: 16222 RVA: 0x000D9528 File Offset: 0x000D7728
			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return this.encoding.GetCharCount(bytes, index, count);
			}

			// Token: 0x06003F5F RID: 16223 RVA: 0x000D9538 File Offset: 0x000D7738
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				return this.encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
			}

			// Token: 0x04001B69 RID: 7017
			private Encoding encoding;
		}

		// Token: 0x02000683 RID: 1667
		private sealed class ForwardingEncoder : Encoder
		{
			// Token: 0x06003F60 RID: 16224 RVA: 0x000D954C File Offset: 0x000D774C
			public ForwardingEncoder(Encoding enc)
			{
				this.encoding = enc;
				EncoderFallback encoderFallback = this.encoding.EncoderFallback;
				if (encoderFallback != null)
				{
					base.Fallback = encoderFallback;
				}
			}

			// Token: 0x06003F61 RID: 16225 RVA: 0x000D9580 File Offset: 0x000D7780
			public override int GetByteCount(char[] chars, int index, int count, bool flush)
			{
				return this.encoding.GetByteCount(chars, index, count);
			}

			// Token: 0x06003F62 RID: 16226 RVA: 0x000D9590 File Offset: 0x000D7790
			public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteCount, bool flush)
			{
				return this.encoding.GetBytes(chars, charIndex, charCount, bytes, byteCount);
			}

			// Token: 0x04001B6A RID: 7018
			private Encoding encoding;
		}
	}
}
