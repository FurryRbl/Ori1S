using System;

namespace System.Configuration
{
	/// <summary>Specifies a <see cref="T:System.Configuration.CallbackValidator" /> object to use for code validation. This class cannot be inherited.</summary>
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class CallbackValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Gets or sets the name of the callback method.</summary>
		/// <returns>The name of the method to call.</returns>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00002A1C File Offset: 0x00000C1C
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00002A24 File Offset: 0x00000C24
		public string CallbackMethodName
		{
			get
			{
				return this.callbackMethodName;
			}
			set
			{
				this.callbackMethodName = value;
				this.instance = null;
			}
		}

		/// <summary>Gets or sets the type of the validator.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the current validator attribute instance.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002A34 File Offset: 0x00000C34
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00002A3C File Offset: 0x00000C3C
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
				this.instance = null;
			}
		}

		/// <summary>Gets the validator instance.</summary>
		/// <returns>The current <see cref="T:System.Configuration.ConfigurationValidatorBase" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <see cref="P:System.Configuration.CallbackValidatorAttribute.Type" /> property is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Configuration.CallbackValidatorAttribute.CallbackMethodName" /> property is not set to a public static void method with one object parameter.</exception>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002A4C File Offset: 0x00000C4C
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return this.instance;
			}
		}

		// Token: 0x0400002B RID: 43
		private string callbackMethodName = string.Empty;

		// Token: 0x0400002C RID: 44
		private Type type;

		// Token: 0x0400002D RID: 45
		private ConfigurationValidatorBase instance;
	}
}
