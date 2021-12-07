using System;
using System.Text.RegularExpressions;

namespace System.Configuration
{
	/// <summary>Provides validation of a string based on the rules provided by a regular expression.</summary>
	// Token: 0x02000069 RID: 105
	public class RegexStringValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.RegexStringValidator" /> class. </summary>
		/// <param name="regex">A string that specifies a regular expression.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="regex" /> is null or an empty string ("").</exception>
		// Token: 0x06000395 RID: 917 RVA: 0x00009D40 File Offset: 0x00007F40
		public RegexStringValidator(string regex)
		{
			this.regex = regex;
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <returns>true if the <paramref name="type" /> parameter matches a string; otherwise, false. </returns>
		/// <param name="type">The type of object.</param>
		// Token: 0x06000396 RID: 918 RVA: 0x00009D50 File Offset: 0x00007F50
		public override bool CanValidate(Type type)
		{
			return type == typeof(string);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> does not conform to the parameters of the <see cref="T:System.Text.RegularExpressions.Regex" /> class.</exception>
		// Token: 0x06000397 RID: 919 RVA: 0x00009D60 File Offset: 0x00007F60
		public override void Validate(object value)
		{
			if (!Regex.IsMatch((string)value, this.regex))
			{
				throw new ArgumentException("The string must match the regexp `{0}'", this.regex);
			}
		}

		// Token: 0x0400011C RID: 284
		private string regex;
	}
}
