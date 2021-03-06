using System;

namespace System.Text
{
	/// <summary>Represents a substitute output string that is emitted when the original input byte sequence cannot be decoded. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000678 RID: 1656
	public sealed class DecoderReplacementFallbackBuffer : DecoderFallbackBuffer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderReplacementFallbackBuffer" /> class using the value of a <see cref="T:System.Text.DecoderReplacementFallback" /> object.</summary>
		/// <param name="fallback">A <see cref="T:System.Text.DecoderReplacementFallback" /> object that contains a replacement string. </param>
		// Token: 0x06003ED3 RID: 16083 RVA: 0x000D775C File Offset: 0x000D595C
		public DecoderReplacementFallbackBuffer(DecoderReplacementFallback fallback)
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
		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06003ED4 RID: 16084 RVA: 0x000D7794 File Offset: 0x000D5994
		public override int Remaining
		{
			get
			{
				return (!this.fallback_assigned) ? 0 : (this.replacement.Length - this.current);
			}
		}

		/// <summary>Prepares the replacement fallback buffer to use the current replacement string.</summary>
		/// <returns>true if the replacement string is not empty; false if the replacement string is empty.</returns>
		/// <param name="bytesUnknown">An input byte sequence. This parameter is ignored unless an exception is thrown.</param>
		/// <param name="index">The index position of the byte in <paramref name="bytesUnknown" />. This parameter is ignored in this operation.</param>
		/// <exception cref="T:System.ArgumentException">This method is called again before the <see cref="M:System.Text.DecoderReplacementFallbackBuffer.GetNextChar" /> method has read all the characters in the replacement fallback buffer.  </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003ED5 RID: 16085 RVA: 0x000D77BC File Offset: 0x000D59BC
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			if (bytesUnknown == null)
			{
				throw new ArgumentNullException("bytesUnknown");
			}
			if (this.fallback_assigned && this.Remaining != 0)
			{
				throw new ArgumentException("Reentrant Fallback method invocation occured. It might be because either this FallbackBuffer is incorrectly shared by multiple threads, invoked inside Encoding recursively, or Reset invocation is forgotten.");
			}
			if (index < 0 || bytesUnknown.Length < index)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.fallback_assigned = true;
			this.current = 0;
			return this.replacement.Length > 0;
		}

		/// <summary>Retrieves the next character in the replacement fallback buffer.</summary>
		/// <returns>The next character in the replacement fallback buffer.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003ED6 RID: 16086 RVA: 0x000D7834 File Offset: 0x000D5A34
		public override char GetNextChar()
		{
			if (!this.fallback_assigned)
			{
				return '\0';
			}
			if (this.current >= this.replacement.Length)
			{
				return '\0';
			}
			return this.replacement[this.current++];
		}

		/// <summary>Causes the next call to <see cref="M:System.Text.DecoderReplacementFallbackBuffer.GetNextChar" /> to access the character position in the replacement fallback buffer prior to the current character position.</summary>
		/// <returns>true if the <see cref="M:System.Text.DecoderReplacementFallbackBuffer.MovePrevious" /> operation was successful; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003ED7 RID: 16087 RVA: 0x000D7884 File Offset: 0x000D5A84
		public override bool MovePrevious()
		{
			if (this.current == 0)
			{
				return false;
			}
			this.current--;
			return true;
		}

		/// <summary>Initializes all internal state information and data in the <see cref="T:System.Text.DecoderReplacementFallbackBuffer" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003ED8 RID: 16088 RVA: 0x000D78A4 File Offset: 0x000D5AA4
		public override void Reset()
		{
			this.fallback_assigned = false;
			this.current = 0;
		}

		// Token: 0x04001B3B RID: 6971
		private bool fallback_assigned;

		// Token: 0x04001B3C RID: 6972
		private int current;

		// Token: 0x04001B3D RID: 6973
		private string replacement;
	}
}
