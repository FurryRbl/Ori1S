using System;

namespace System.Configuration
{
	/// <summary>Represents a group of user-scoped application settings in a configuration file.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CA RID: 458
	public sealed class ClientSettingsSection : ConfigurationSection
	{
		// Token: 0x0600100B RID: 4107 RVA: 0x0002A684 File Offset: 0x00028884
		static ClientSettingsSection()
		{
			ClientSettingsSection.properties = new ConfigurationPropertyCollection();
			ClientSettingsSection.properties.Add(ClientSettingsSection.settings_prop);
		}

		/// <summary>Gets the collection of client settings for the section.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingElementCollection" /> containing all the client settings found in the current configuration section.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0002A6C8 File Offset: 0x000288C8
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public SettingElementCollection Settings
		{
			get
			{
				return (SettingElementCollection)base[ClientSettingsSection.settings_prop];
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x0002A6DC File Offset: 0x000288DC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ClientSettingsSection.properties;
			}
		}

		// Token: 0x04000478 RID: 1144
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000479 RID: 1145
		private static ConfigurationProperty settings_prop = new ConfigurationProperty(string.Empty, typeof(SettingElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
