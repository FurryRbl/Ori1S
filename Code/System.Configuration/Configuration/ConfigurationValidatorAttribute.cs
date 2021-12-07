using System;

namespace System.Configuration
{
	/// <summary>Serves as the base class for the <see cref="N:System.Configuration" /> validator attribute types.</summary>
	// Token: 0x0200003B RID: 59
	[AttributeUsage(AttributeTargets.Property)]
	public class ConfigurationValidatorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationValidatorAttribute" /> class.</summary>
		// Token: 0x0600024D RID: 589 RVA: 0x00007C20 File Offset: 0x00005E20
		protected ConfigurationValidatorAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationValidatorAttribute" /> class using the specified validator type.</summary>
		/// <param name="validator">The validator type to use when creating an instance of <see cref="T:System.Configuration.ConfigurationValidatorAttribute" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="validator" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="validator" /> is not derived from <see cref="T:System.Configuration.ConfigurationValidatorBase" />.</exception>
		// Token: 0x0600024E RID: 590 RVA: 0x00007C28 File Offset: 0x00005E28
		public ConfigurationValidatorAttribute(Type validator)
		{
			this.validatorType = validator;
		}

		/// <summary>Gets the validator attribute instance.</summary>
		/// <returns>The current <see cref="T:System.Configuration.ConfigurationValidatorBase" />.</returns>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00007C38 File Offset: 0x00005E38
		public virtual ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = (ConfigurationValidatorBase)Activator.CreateInstance(this.validatorType);
				}
				return this.instance;
			}
		}

		/// <summary>Gets the type of the validator attribute.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the current validator attribute instance.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00007C64 File Offset: 0x00005E64
		public Type ValidatorType
		{
			get
			{
				return this.validatorType;
			}
		}

		// Token: 0x040000C3 RID: 195
		private Type validatorType;

		// Token: 0x040000C4 RID: 196
		private ConfigurationValidatorBase instance;
	}
}
