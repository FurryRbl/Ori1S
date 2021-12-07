using System;

namespace System.Configuration
{
	/// <summary>Provides validation of a string.</summary>
	// Token: 0x02000076 RID: 118
	public class StringValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.StringValidator" /> class, based on a supplied parameter.</summary>
		/// <param name="minLength">An integer that specifies the minimum length of the string value.</param>
		// Token: 0x06000406 RID: 1030 RVA: 0x0000B770 File Offset: 0x00009970
		public StringValidator(int minLength)
		{
			this.minLength = minLength;
			this.maxLength = int.MaxValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.StringValidator" /> class, based on supplied parameters.</summary>
		/// <param name="minLength">An integer that specifies the minimum length of the string value.</param>
		/// <param name="maxLength">An integer that specifies the maximum length of the string value.</param>
		// Token: 0x06000407 RID: 1031 RVA: 0x0000B78C File Offset: 0x0000998C
		public StringValidator(int minLength, int maxLength)
		{
			this.minLength = minLength;
			this.maxLength = maxLength;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.StringValidator" /> class, based on supplied parameters.</summary>
		/// <param name="minLength">An integer that specifies the minimum length of the string value.</param>
		/// <param name="maxLength">An integer that specifies the maximum length of the string value.</param>
		/// <param name="invalidCharacters">A string that represents invalid characters. </param>
		// Token: 0x06000408 RID: 1032 RVA: 0x0000B7A4 File Offset: 0x000099A4
		public StringValidator(int minLength, int maxLength, string invalidCharacters)
		{
			this.minLength = minLength;
			this.maxLength = maxLength;
			if (invalidCharacters != null)
			{
				this.invalidCharacters = invalidCharacters.ToCharArray();
			}
		}

		/// <summary>Determines whether an object can be validated based on type.</summary>
		/// <returns>true if the <paramref name="type" /> parameter matches a string; otherwise, false. </returns>
		/// <param name="type">The object type.</param>
		// Token: 0x06000409 RID: 1033 RVA: 0x0000B7D8 File Offset: 0x000099D8
		public override bool CanValidate(Type type)
		{
			return type == typeof(string);
		}

		/// <summary>Determines whether the value of an object is valid. </summary>
		/// <param name="value">The object value.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is less than <paramref name="minValue" /> or greater than <paramref name="maxValue" /> as defined in the constructor.- or -<paramref name="value" /> contains invalid characters.</exception>
		// Token: 0x0600040A RID: 1034 RVA: 0x0000B7E8 File Offset: 0x000099E8
		public override void Validate(object value)
		{
			if (value == null && this.minLength <= 0)
			{
				return;
			}
			string text = (string)value;
			if (text == null || text.Length < this.minLength)
			{
				throw new ArgumentException("The string must be at least " + this.minLength + " characters long.");
			}
			if (text.Length > this.maxLength)
			{
				throw new ArgumentException("The string must be no more than " + this.maxLength + " characters long.");
			}
			if (this.invalidCharacters != null)
			{
				int num = text.IndexOfAny(this.invalidCharacters);
				if (num != -1)
				{
					throw new ArgumentException(string.Format("The string cannot contain any of the following characters: '{0}'.", this.invalidCharacters));
				}
			}
		}

		// Token: 0x0400013F RID: 319
		private char[] invalidCharacters;

		// Token: 0x04000140 RID: 320
		private int maxLength;

		// Token: 0x04000141 RID: 321
		private int minLength;
	}
}
