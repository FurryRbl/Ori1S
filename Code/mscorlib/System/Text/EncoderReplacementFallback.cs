using System;

namespace System.Text
{
	/// <summary>Provides a failure handling mechanism, called a fallback, for an input character that cannot be converted to an output byte sequence. The fallback uses a user-specified replacement string instead of the original input character. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200067F RID: 1663
	[Serializable]
	public sealed class EncoderReplacementFallback : EncoderFallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderReplacementFallback" /> class.</summary>
		// Token: 0x06003F08 RID: 16136 RVA: 0x000D7CE0 File Offset: 0x000D5EE0
		public EncoderReplacementFallback() : this("?")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderReplacementFallback" /> class using a specified replacement string.</summary>
		/// <param name="replacement">A string that is converted in an encoding operation in place of an input character that cannot be encoded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="replacement" /> contains an invalid surrogate pair. In other words, the surrogate does not consist of one high surrogate component followed by one low surrogate component.</exception>
		// Token: 0x06003F09 RID: 16137 RVA: 0x000D7CF0 File Offset: 0x000D5EF0
		[MonoTODO]
		public EncoderReplacementFallback(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException();
			}
			this.replacement = replacement;
		}

		/// <summary>Gets the replacement string that is the value of the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>A substitute string that is used in place of an input character that cannot be encoded.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06003F0A RID: 16138 RVA: 0x000D7D0C File Offset: 0x000D5F0C
		public string DefaultString
		{
			get
			{
				return this.replacement;
			}
		}

		/// <summary>Gets the number of characters in the replacement string for the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>The number of characters in the string used in place of an input character that cannot be encoded.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06003F0B RID: 16139 RVA: 0x000D7D14 File Offset: 0x000D5F14
		public override int MaxCharCount
		{
			get
			{
				return this.replacement.Length;
			}
		}

		/// <summary>Creates a <see cref="T:System.Text.EncoderFallbackBuffer" /> object that is initialized with the replacement string of this <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>A <see cref="T:System.Text.EncoderFallbackBuffer" /> object equal to this <see cref="T:System.Text.EncoderReplacementFallback" /> object. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003F0C RID: 16140 RVA: 0x000D7D24 File Offset: 0x000D5F24
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderReplacementFallbackBuffer(this);
		}

		/// <summary>Indicates whether the value of a specified object is equal to the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>true if the <paramref name="value" /> parameter specifies an <see cref="T:System.Text.EncoderReplacementFallback" /> object and the replacement string of that object is equal to the replacement string of this <see cref="T:System.Text.EncoderReplacementFallback" /> object; otherwise, false. </returns>
		/// <param name="value">A <see cref="T:System.Text.EncoderReplacementFallback" /> object.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003F0D RID: 16141 RVA: 0x000D7D2C File Offset: 0x000D5F2C
		public override bool Equals(object value)
		{
			EncoderReplacementFallback encoderReplacementFallback = value as EncoderReplacementFallback;
			return encoderReplacementFallback != null && this.replacement == encoderReplacementFallback.replacement;
		}

		/// <summary>Retrieves the hash code for the value of the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>The hash code of the value of the object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003F0E RID: 16142 RVA: 0x000D7D5C File Offset: 0x000D5F5C
		public override int GetHashCode()
		{
			return this.replacement.GetHashCode();
		}

		// Token: 0x04001B48 RID: 6984
		private string replacement;
	}
}
