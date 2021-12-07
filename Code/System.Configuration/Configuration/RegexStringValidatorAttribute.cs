using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform string validation on a configuration property using a regular expression. This class cannot be inherited.</summary>
	// Token: 0x0200006A RID: 106
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class RegexStringValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.RegexStringValidatorAttribute" /> object.</summary>
		/// <param name="regex">The string to use for regular expression validation.</param>
		// Token: 0x06000398 RID: 920 RVA: 0x00009D8C File Offset: 0x00007F8C
		public RegexStringValidatorAttribute(string regex)
		{
			this.regex = regex;
		}

		/// <summary>Gets the string used to perform regular-expression validation.</summary>
		/// <returns>The string containing the regular expression used to filter the string assigned to the decorated configuration-element property.</returns>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00009D9C File Offset: 0x00007F9C
		public string Regex
		{
			get
			{
				return this.regex;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.RegexStringValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00009DA4 File Offset: 0x00007FA4
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new RegexStringValidator(this.regex);
				}
				return this.instance;
			}
		}

		// Token: 0x0400011D RID: 285
		private string regex;

		// Token: 0x0400011E RID: 286
		private ConfigurationValidatorBase instance;
	}
}
