using System;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an input character that cannot be converted to an encoded output byte sequence. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200067C RID: 1660
	[Serializable]
	public abstract class EncoderFallback
	{
		/// <summary>Gets an object that throws an exception when an input character cannot be encoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.EncoderFallback" /> class. The default value is a <see cref="T:System.Text.EncoderExceptionFallback" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06003EF2 RID: 16114 RVA: 0x000D7C0C File Offset: 0x000D5E0C
		public static EncoderFallback ExceptionFallback
		{
			get
			{
				return EncoderFallback.exception_fallback;
			}
		}

		/// <summary>When overridden in a derived class, gets the maximum number of characters the current <see cref="T:System.Text.EncoderFallback" /> object can return.</summary>
		/// <returns>The maximum number of characters the current <see cref="T:System.Text.EncoderFallback" /> object can return.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06003EF3 RID: 16115
		public abstract int MaxCharCount { get; }

		/// <summary>Gets an object that outputs a substitute string in place of an input character that cannot be encoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.EncoderFallback" /> class. The default value is a <see cref="T:System.Text.EncoderReplacementFallback" /> object that replaces unknown input characters with the QUESTION MARK character ("?", U+003F).</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06003EF4 RID: 16116 RVA: 0x000D7C14 File Offset: 0x000D5E14
		public static EncoderFallback ReplacementFallback
		{
			get
			{
				return EncoderFallback.replacement_fallback;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06003EF5 RID: 16117 RVA: 0x000D7C1C File Offset: 0x000D5E1C
		internal static EncoderFallback StandardSafeFallback
		{
			get
			{
				return EncoderFallback.standard_safe_fallback;
			}
		}

		/// <summary>When overridden in a derived class, initializes a new instance of the <see cref="T:System.Text.EncoderFallbackBuffer" /> class. </summary>
		/// <returns>An object that provides a fallback buffer for an encoder.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003EF6 RID: 16118
		public abstract EncoderFallbackBuffer CreateFallbackBuffer();

		// Token: 0x04001B40 RID: 6976
		private static EncoderFallback exception_fallback = new EncoderExceptionFallback();

		// Token: 0x04001B41 RID: 6977
		private static EncoderFallback replacement_fallback = new EncoderReplacementFallback();

		// Token: 0x04001B42 RID: 6978
		private static EncoderFallback standard_safe_fallback = new EncoderReplacementFallback("�");
	}
}
