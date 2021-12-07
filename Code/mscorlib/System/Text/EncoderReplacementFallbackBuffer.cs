using System;

namespace System.Text
{
	/// <summary>Represents a substitute input string that is used when the original input character cannot be encoded. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000680 RID: 1664
	public sealed class EncoderReplacementFallbackBuffer : EncoderFallbackBuffer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderReplacementFallbackBuffer" /> class using the value of a <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <param name="fallback">A <see cref="T:System.Text.EncoderReplacementFallback" /> object. </param>
		// Token: 0x06003F0F RID: 16143 RVA: 0x000D7D6C File Offset: 0x000D5F6C
		public EncoderReplacementFallbackBuffer(EncoderReplacementFallback fallback)
		{
			if (fallback == null)
			{
				throw new ArgumentNullException("fallback");
			}
			this.replacement = fallback.DefaultString;
			this.current = 0;
		}

		/// <summary>Gets the number of characters in the replacement fallback buffer that remain to be processed.</summary>
		/// <returns>The number of characters in the replacement fallback buffer that have not yet been processed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06003F10 RID: 16144 RVA: 0x000D7DA4 File Offset: 0x000D5FA4
		public override int Remaining
		{
			get
			{
				return this.replacement.Length - this.current;
			}
		}

		/// <summary>Prepares the replacement fallback buffer to use the current replacement string.</summary>
		/// <returns>true if the replacement string is not empty; false if the replacement string is empty.</returns>
		/// <param name="charUnknown">An input character. This parameter is ignored in this operation unless an exception is thrown.</param>
		/// <param name="index">The index position of the character in the input buffer. This parameter is ignored in this operation.</param>
		/// <exception cref="T:System.ArgumentException">This method is called again before the <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> method has read all the characters in the replacement fallback buffer.  </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F11 RID: 16145 RVA: 0x000D7DB8 File Offset: 0x000D5FB8
		public override bool Fallback(char charUnknown, int index)
		{
			return this.Fallback(index);
		}

		/// <summary>Indicates whether a replacement string can be used when an input surrogate pair cannot be encoded, or whether the surrogate pair can be ignored. Parameters specify the surrogate pair and the index position of the pair in the input.</summary>
		/// <returns>true if the replacement string is not empty; false if the replacement string is empty.</returns>
		/// <param name="charUnknownHigh">The high surrogate of the input pair.</param>
		/// <param name="charUnknownLow">The low surrogate of the input pair.</param>
		/// <param name="index">The index position of the surrogate pair in the input buffer.</param>
		/// <exception cref="T:System.ArgumentException">This method is called again before the <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> method has read all the replacement string characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="charUnknownHigh" /> is less than U+D800 or greater than U+D8FF.-or-The value of <paramref name="charUnknownLow" /> is less than U+DC00 or greater than U+DFFF.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F12 RID: 16146 RVA: 0x000D7DC4 File Offset: 0x000D5FC4
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			return this.Fallback(index);
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x000D7DD0 File Offset: 0x000D5FD0
		private bool Fallback(int index)
		{
			if (this.fallback_assigned && this.Remaining != 0)
			{
				throw new ArgumentException("Reentrant Fallback method invocation occured. It might be because either this FallbackBuffer is incorrectly shared by multiple threads, invoked inside Encoding recursively, or Reset invocation is forgotten.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.fallback_assigned = true;
			this.current = 0;
			return this.replacement.Length > 0;
		}

		/// <summary>Retrieves the next character in the replacement fallback buffer.</summary>
		/// <returns>The next Unicode character in the replacement fallback buffer that the application can encode.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003F14 RID: 16148 RVA: 0x000D7E2C File Offset: 0x000D602C
		public override char GetNextChar()
		{
			if (this.current >= this.replacement.Length)
			{
				return '\0';
			}
			return this.replacement[this.current++];
		}

		/// <summary>Causes the next call to the <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> method to access the character position in the replacement fallback buffer prior to the current character position. </summary>
		/// <returns>true if the <see cref="M:System.Text.EncoderReplacementFallbackBuffer.MovePrevious" /> operation was successful; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F15 RID: 16149 RVA: 0x000D7E70 File Offset: 0x000D6070
		public override bool MovePrevious()
		{
			if (this.current == 0)
			{
				return false;
			}
			this.current--;
			return true;
		}

		/// <summary>Initializes all internal state information and data in this instance of <see cref="T:System.Text.EncoderReplacementFallbackBuffer" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F16 RID: 16150 RVA: 0x000D7E90 File Offset: 0x000D6090
		public override void Reset()
		{
			this.current = 0;
		}

		// Token: 0x04001B49 RID: 6985
		private string replacement;

		// Token: 0x04001B4A RID: 6986
		private int current;

		// Token: 0x04001B4B RID: 6987
		private bool fallback_assigned;
	}
}
