using System;

namespace System.Configuration
{
	/// <summary>Specifies the property of a configuration element. This class cannot be inherited.</summary>
	// Token: 0x02000026 RID: 38
	public sealed class ConfigurationElementProperty
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationElementProperty" /> class, based on a supplied parameter.</summary>
		/// <param name="validator">A <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="validator" /> is null.</exception>
		// Token: 0x06000187 RID: 391 RVA: 0x00005C84 File Offset: 0x00003E84
		public ConfigurationElementProperty(ConfigurationValidatorBase validator)
		{
			this.validator = validator;
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object used to validate the <see cref="T:System.Configuration.ConfigurationElementProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00005C94 File Offset: 0x00003E94
		public ConfigurationValidatorBase Validator
		{
			get
			{
				return this.validator;
			}
		}

		// Token: 0x04000079 RID: 121
		private ConfigurationValidatorBase validator;
	}
}
