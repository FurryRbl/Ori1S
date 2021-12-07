using System;

namespace System.Text
{
	/// <summary>Throws <see cref="T:System.Text.DecoderFallbackException" /> if an encoded input byte sequence cannot be converted to a decoded output character. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000672 RID: 1650
	[Serializable]
	public sealed class DecoderExceptionFallback : DecoderFallback
	{
		/// <summary>Gets the maximum number of characters this instance can return.</summary>
		/// <returns>The return value is always zero.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06003EB0 RID: 16048 RVA: 0x000D75E0 File Offset: 0x000D57E0
		public override int MaxCharCount
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderExceptionFallback" /> class. </summary>
		/// <returns>A <see cref="T:System.Text.DecoderFallbackBuffer" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003EB1 RID: 16049 RVA: 0x000D75E4 File Offset: 0x000D57E4
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderExceptionFallbackBuffer();
		}

		/// <summary>Indicates whether the current <see cref="T:System.Text.DecoderExceptionFallback" /> object and a specified object are equal.</summary>
		/// <returns>true if <paramref name="value" /> is not null and is a <see cref="T:System.Text.DecoderExceptionFallback" /> object; otherwise, false.</returns>
		/// <param name="value">An object that derives from the <see cref="T:System.Text.DecoderExceptionFallback" /> class.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003EB2 RID: 16050 RVA: 0x000D75EC File Offset: 0x000D57EC
		public override bool Equals(object value)
		{
			return value is DecoderExceptionFallback;
		}

		/// <summary>Retrieves the hash code for this instance.</summary>
		/// <returns>The return value is always the same arbitrary value, and has no special significance. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003EB3 RID: 16051 RVA: 0x000D75F8 File Offset: 0x000D57F8
		public override int GetHashCode()
		{
			return 0;
		}
	}
}
