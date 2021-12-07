using System;

namespace System.Text
{
	/// <summary>The exception that is thrown when an encoder fallback operation fails. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200067E RID: 1662
	[Serializable]
	public sealed class EncoderFallbackException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallbackException" /> class.</summary>
		// Token: 0x06003EFE RID: 16126 RVA: 0x000D7C40 File Offset: 0x000D5E40
		public EncoderFallbackException() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallbackException" /> class. A parameter specifies the error message.</summary>
		/// <param name="message">An error message.</param>
		// Token: 0x06003EFF RID: 16127 RVA: 0x000D7C4C File Offset: 0x000D5E4C
		public EncoderFallbackException(string message)
		{
			this.index = -1;
			base..ctor(message);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallbackException" /> class. Parameters specify the error message and the inner exception that is the cause of this exception.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="innerException">The exception that caused this exception.</param>
		// Token: 0x06003F00 RID: 16128 RVA: 0x000D7C5C File Offset: 0x000D5E5C
		public EncoderFallbackException(string message, Exception innerException)
		{
			this.index = -1;
			base..ctor(message, innerException);
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x000D7C70 File Offset: 0x000D5E70
		internal EncoderFallbackException(char charUnknown, int index)
		{
			this.index = -1;
			base..ctor(null);
			this.char_unknown = charUnknown;
			this.index = index;
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x000D7C90 File Offset: 0x000D5E90
		internal EncoderFallbackException(char charUnknownHigh, char charUnknownLow, int index)
		{
			this.index = -1;
			base..ctor(null);
			this.char_unknown_high = charUnknownHigh;
			this.char_unknown_low = charUnknownLow;
			this.index = index;
		}

		/// <summary>Gets the input character that caused the exception.</summary>
		/// <returns>The character that cannot be encoded.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06003F03 RID: 16131 RVA: 0x000D7CB8 File Offset: 0x000D5EB8
		public char CharUnknown
		{
			get
			{
				return this.char_unknown;
			}
		}

		/// <summary>Gets the high component character of the surrogate pair that caused the exception.</summary>
		/// <returns>The high component character of the surrogate pair that cannot be encoded.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06003F04 RID: 16132 RVA: 0x000D7CC0 File Offset: 0x000D5EC0
		public char CharUnknownHigh
		{
			get
			{
				return this.char_unknown_high;
			}
		}

		/// <summary>Gets the low component character of the surrogate pair that caused the exception.</summary>
		/// <returns>The low component character of the surrogate pair that cannot be encoded.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06003F05 RID: 16133 RVA: 0x000D7CC8 File Offset: 0x000D5EC8
		public char CharUnknownLow
		{
			get
			{
				return this.char_unknown_low;
			}
		}

		/// <summary>Gets the index position in the input buffer of the character that caused the exception.</summary>
		/// <returns>The index position in the input buffer of the character that cannot be encoded.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06003F06 RID: 16134 RVA: 0x000D7CD0 File Offset: 0x000D5ED0
		[MonoTODO]
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Indicates whether the input that caused the exception is a surrogate pair.</summary>
		/// <returns>true if the input was a surrogate pair; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003F07 RID: 16135 RVA: 0x000D7CD8 File Offset: 0x000D5ED8
		[MonoTODO]
		public bool IsUnknownSurrogate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001B43 RID: 6979
		private const string defaultMessage = "Failed to decode the input byte sequence to Unicode characters.";

		// Token: 0x04001B44 RID: 6980
		private char char_unknown;

		// Token: 0x04001B45 RID: 6981
		private char char_unknown_high;

		// Token: 0x04001B46 RID: 6982
		private char char_unknown_low;

		// Token: 0x04001B47 RID: 6983
		private int index;
	}
}
