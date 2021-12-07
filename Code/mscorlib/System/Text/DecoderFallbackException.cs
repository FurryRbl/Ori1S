using System;

namespace System.Text
{
	/// <summary>The exception that is thrown when a decoder fallback operation fails. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000676 RID: 1654
	[Serializable]
	public sealed class DecoderFallbackException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. </summary>
		// Token: 0x06003EC6 RID: 16070 RVA: 0x000D7670 File Offset: 0x000D5870
		public DecoderFallbackException() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. A parameter specifies the error message.</summary>
		/// <param name="message">An error message.</param>
		// Token: 0x06003EC7 RID: 16071 RVA: 0x000D767C File Offset: 0x000D587C
		public DecoderFallbackException(string message)
		{
			this.index = -1;
			base..ctor(message);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. Parameters specify the error message and the inner exception that is the cause of this exception.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="innerException">The exception that caused this exception.</param>
		// Token: 0x06003EC8 RID: 16072 RVA: 0x000D768C File Offset: 0x000D588C
		public DecoderFallbackException(string message, Exception innerException)
		{
			this.index = -1;
			base..ctor(message, innerException);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. Parameters specify the error message, the array of bytes being decoded, and the index of the byte that cannot be decoded.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="bytesUnknown">The input byte array.</param>
		/// <param name="index">The index position in <paramref name="bytesUnknown" /> of the byte that cannot be decoded.</param>
		// Token: 0x06003EC9 RID: 16073 RVA: 0x000D76A0 File Offset: 0x000D58A0
		public DecoderFallbackException(string message, byte[] bytesUnknown, int index)
		{
			this.index = -1;
			base..ctor(message);
			this.bytes_unknown = bytesUnknown;
			this.index = index;
		}

		/// <summary>Gets the input byte sequence that caused the exception.</summary>
		/// <returns>The input byte array that cannot be decoded. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06003ECA RID: 16074 RVA: 0x000D76C0 File Offset: 0x000D58C0
		[MonoTODO]
		public byte[] BytesUnknown
		{
			get
			{
				return this.bytes_unknown;
			}
		}

		/// <summary>Gets the index position in the input byte sequence of the byte that caused the exception.</summary>
		/// <returns>The index position in the input byte array of the byte that cannot be decoded. The index position is zero-based. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x000D76C8 File Offset: 0x000D58C8
		[MonoTODO]
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x04001B37 RID: 6967
		private const string defaultMessage = "Failed to decode the input byte sequence to Unicode characters.";

		// Token: 0x04001B38 RID: 6968
		private byte[] bytes_unknown;

		// Token: 0x04001B39 RID: 6969
		private int index;
	}
}
