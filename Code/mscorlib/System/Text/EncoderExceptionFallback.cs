using System;

namespace System.Text
{
	/// <summary>Throws a <see cref="T:System.Text.EncoderFallbackException" /> if an input character cannot be converted to an encoded output byte sequence. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200067A RID: 1658
	[Serializable]
	public sealed class EncoderExceptionFallback : EncoderFallback
	{
		/// <summary>Gets the maximum number of characters this instance can return.</summary>
		/// <returns>The return value is always zero.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06003EE6 RID: 16102 RVA: 0x000D7B94 File Offset: 0x000D5D94
		public override int MaxCharCount
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderExceptionFallback" /> class.</summary>
		/// <returns>A <see cref="T:System.Text.EncoderFallbackBuffer" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003EE7 RID: 16103 RVA: 0x000D7B98 File Offset: 0x000D5D98
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderExceptionFallbackBuffer();
		}

		/// <summary>Indicates whether the current <see cref="T:System.Text.EncoderExceptionFallback" /> object and a specified object are equal.</summary>
		/// <returns>true if <paramref name="value" /> is not null (Nothing in Visual Basic .NET) and is a <see cref="T:System.Text.EncoderExceptionFallback" /> object; otherwise, false.</returns>
		/// <param name="value">An object that derives from the <see cref="T:System.Text.EncoderExceptionFallback" /> class.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003EE8 RID: 16104 RVA: 0x000D7BA0 File Offset: 0x000D5DA0
		public override bool Equals(object value)
		{
			return value is EncoderExceptionFallback;
		}

		/// <summary>Retrieves the hash code for this instance.</summary>
		/// <returns>The return value is always the same arbitrary value, and has no special significance. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003EE9 RID: 16105 RVA: 0x000D7BAC File Offset: 0x000D5DAC
		public override int GetHashCode()
		{
			return 0;
		}
	}
}
