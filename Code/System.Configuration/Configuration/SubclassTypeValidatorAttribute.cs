using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x02000079 RID: 121
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SubclassTypeValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SubclassTypeValidatorAttribute" /> class. </summary>
		/// <param name="baseClass">The base class type.</param>
		// Token: 0x06000416 RID: 1046 RVA: 0x0000B994 File Offset: 0x00009B94
		public SubclassTypeValidatorAttribute(Type baseClass)
		{
			this.baseClass = baseClass;
		}

		/// <summary>Gets the base type of the object being validated.</summary>
		/// <returns>The base type of the object being validated.</returns>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		public Type BaseClass
		{
			get
			{
				return this.baseClass;
			}
		}

		/// <summary>Gets the validator attribute instance.</summary>
		/// <returns>The current <see cref="T:System.Configuration.ConfigurationValidatorBase" /> instance.</returns>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000B9AC File Offset: 0x00009BAC
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new SubclassTypeValidator(this.baseClass);
				}
				return this.instance;
			}
		}

		// Token: 0x04000147 RID: 327
		private Type baseClass;

		// Token: 0x04000148 RID: 328
		private ConfigurationValidatorBase instance;
	}
}
