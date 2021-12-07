using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform string validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x02000077 RID: 119
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class StringValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Gets or sets the invalid characters for the property.</summary>
		/// <returns>The string that contains the set of characters that are not allowed for the property.</returns>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000B8C0 File Offset: 0x00009AC0
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		public string InvalidCharacters
		{
			get
			{
				return this.invalidCharacters;
			}
			set
			{
				this.invalidCharacters = value;
				this.instance = null;
			}
		}

		/// <summary>Gets or sets the maximum length allowed for the string to assign to the property.</summary>
		/// <returns>An integer that indicates the maximum allowed length for the string to assign to the property.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see cref="P:System.Configuration.StringValidatorAttribute.MinLength" />.</exception>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000B8D8 File Offset: 0x00009AD8
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
			set
			{
				this.maxLength = value;
				this.instance = null;
			}
		}

		/// <summary>Gets or sets the minimum allowed value for the string to assign to the property.</summary>
		/// <returns>An integer that indicates the allowed minimum length for the string to assign to the property.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is greater than <see cref="P:System.Configuration.StringValidatorAttribute.MaxLength" />.</exception>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000B8F0 File Offset: 0x00009AF0
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x0000B8F8 File Offset: 0x00009AF8
		public int MinLength
		{
			get
			{
				return this.minLength;
			}
			set
			{
				this.minLength = value;
				this.instance = null;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.StringValidator" /> class.</summary>
		/// <returns>A current <see cref="T:System.Configuration.StringValidator" /> settings in a <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000B908 File Offset: 0x00009B08
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new StringValidator(this.minLength, this.maxLength, this.invalidCharacters);
				}
				return this.instance;
			}
		}

		// Token: 0x04000142 RID: 322
		private string invalidCharacters;

		// Token: 0x04000143 RID: 323
		private int maxLength = int.MaxValue;

		// Token: 0x04000144 RID: 324
		private int minLength;

		// Token: 0x04000145 RID: 325
		private ConfigurationValidatorBase instance;
	}
}
