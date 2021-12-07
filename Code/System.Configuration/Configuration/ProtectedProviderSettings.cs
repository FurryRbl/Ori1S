using System;

namespace System.Configuration
{
	/// <summary>Represents a group of configuration elements that configure the providers for the &lt;configProtectedData&gt; configuration section.</summary>
	// Token: 0x02000066 RID: 102
	public class ProtectedProviderSettings : ConfigurationElement
	{
		// Token: 0x0600037A RID: 890 RVA: 0x00009AA4 File Offset: 0x00007CA4
		static ProtectedProviderSettings()
		{
			ProtectedProviderSettings.properties.Add(ProtectedProviderSettings.providersProp);
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> collection that represents the properties of the providers for the protected configuration data.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> that represents the properties of the providers for the protected configuration data.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00009AE8 File Offset: 0x00007CE8
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProtectedProviderSettings.properties;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Configuration.ProviderSettings" /> objects that represent the properties of the providers for the protected configuration data.</summary>
		/// <returns>A collection of <see cref="T:System.Configuration.ProviderSettings" /> objects that represent the properties of the providers for the protected configuration data.</returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00009AF0 File Offset: 0x00007CF0
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[ProtectedProviderSettings.providersProp];
			}
		}

		// Token: 0x04000115 RID: 277
		private static ConfigurationProperty providersProp = new ConfigurationProperty(string.Empty, typeof(ProviderSettingsCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04000116 RID: 278
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
