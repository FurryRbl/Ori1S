using System;

namespace System.Text
{
	/// <summary>Throws <see cref="T:System.Text.DecoderFallbackException" /> when an encoded input byte sequence cannot be converted to a decoded output character. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000673 RID: 1651
	public sealed class DecoderExceptionFallbackBuffer : DecoderFallbackBuffer
	{
		/// <summary>Gets the number of characters in the current <see cref="T:System.Text.DecoderExceptionFallbackBuffer" /> object that remain to be processed.</summary>
		/// <returns>The return value is always zero. A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06003EB5 RID: 16053 RVA: 0x000D7604 File Offset: 0x000D5804
		public override int Remaining
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Throws <see cref="T:System.Text.DecoderFallbackException" /> when the input byte sequence cannot be decoded. The nominal return value is not used. </summary>
		/// <returns>None. No value is returned because the <see cref="M:System.Text.DecoderExceptionFallbackBuffer.Fallback(System.Byte[],System.Int32)" /> method always throws an exception. The nominal return value is true. A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		/// <param name="bytesUnknown">An input array of bytes.</param>
		/// <param name="index">The index position of a byte in the input.</param>
		/// <exception cref="T:System.Text.DecoderFallbackException">This method always throws an exception that reports the value and index position of the input byte that cannot be decoded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003EB6 RID: 16054 RVA: 0x000D7608 File Offset: 0x000D5808
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			throw new DecoderFallbackException(null, bytesUnknown, index);
		}

		/// <summary>Retrieves the next character in the exception data buffer.</summary>
		/// <returns>The return value is always the Unicode character NULL (U+0000). A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003EB7 RID: 16055 RVA: 0x000D7614 File Offset: 0x000D5814
		public override char GetNextChar()
		{
			return '\0';
		}

		/// <summary>Causes the next call to <see cref="M:System.Text.DecoderExceptionFallbackBuffer.GetNextChar" /> to access the exception data buffer character position that is prior to the current position.</summary>
		/// <returns>The return value is always false. A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003EB8 RID: 16056 RVA: 0x000D7618 File Offset: 0x000D5818
		public override bool MovePrevious()
		{
			return false;
		}
	}
}
