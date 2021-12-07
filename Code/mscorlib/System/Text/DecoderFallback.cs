using System;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an encoded input byte sequence that cannot be converted to an output character. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000674 RID: 1652
	[Serializable]
	public abstract class DecoderFallback
	{
		/// <summary>Gets an object that throws an exception when an input byte sequence cannot be decoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.DecoderFallback" /> class. The default value is a <see cref="T:System.Text.DecoderExceptionFallback" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06003EBB RID: 16059 RVA: 0x000D764C File Offset: 0x000D584C
		public static DecoderFallback ExceptionFallback
		{
			get
			{
				return DecoderFallback.exception_fallback;
			}
		}

		/// <summary>When overridden in a derived class, gets the maximum number of characters the current <see cref="T:System.Text.DecoderFallback" /> object can return.</summary>
		/// <returns>The maximum number of characters the current <see cref="T:System.Text.DecoderFallback" /> object can return.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06003EBC RID: 16060
		public abstract int MaxCharCount { get; }

		/// <summary>Gets an object that outputs a substitute string in place of an input byte sequence that cannot be decoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.DecoderFallback" /> class. The default value is a <see cref="T:System.Text.DecoderReplacementFallback" /> object that emits the QUESTION MARK character ("?", U+003F) in place of unknown byte sequences. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06003EBD RID: 16061 RVA: 0x000D7654 File Offset: 0x000D5854
		public static DecoderFallback ReplacementFallback
		{
			get
			{
				return DecoderFallback.replacement_fallback;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06003EBE RID: 16062 RVA: 0x000D765C File Offset: 0x000D585C
		internal static DecoderFallback StandardSafeFallback
		{
			get
			{
				return DecoderFallback.standard_safe_fallback;
			}
		}

		/// <summary>When overridden in a derived class, initializes a new instance of the <see cref="T:System.Text.DecoderFallbackBuffer" /> class. </summary>
		/// <returns>An object that provides a fallback buffer for a decoder.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003EBF RID: 16063
		public abstract DecoderFallbackBuffer CreateFallbackBuffer();

		// Token: 0x04001B34 RID: 6964
		private static DecoderFallback exception_fallback = new DecoderExceptionFallback();

		// Token: 0x04001B35 RID: 6965
		private static DecoderFallback replacement_fallback = new DecoderReplacementFallback();

		// Token: 0x04001B36 RID: 6966
		private static DecoderFallback standard_safe_fallback = new DecoderReplacementFallback("�");
	}
}
