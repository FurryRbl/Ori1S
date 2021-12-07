using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for connection management. This class cannot be inherited.</summary>
	// Token: 0x020002D1 RID: 721
	public sealed class ConnectionManagementSection : ConfigurationSection
	{
		// Token: 0x060018C2 RID: 6338 RVA: 0x000441B8 File Offset: 0x000423B8
		static ConnectionManagementSection()
		{
			ConnectionManagementSection.properties.Add(ConnectionManagementSection.connectionManagementProp);
		}

		/// <summary>Gets the collection of connection management objects in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ConnectionManagementElementCollection" /> that contains the connection management information for the local computer. </returns>
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x000441FC File Offset: 0x000423FC
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ConnectionManagementElementCollection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementElementCollection)base[ConnectionManagementSection.connectionManagementProp];
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x00044210 File Offset: 0x00042410
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionManagementSection.properties;
			}
		}

		// Token: 0x04000FBC RID: 4028
		private static ConfigurationProperty connectionManagementProp = new ConfigurationProperty("ConnectionManagement", typeof(ConnectionManagementElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04000FBD RID: 4029
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
