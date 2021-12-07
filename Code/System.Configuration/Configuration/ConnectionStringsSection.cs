using System;

namespace System.Configuration
{
	/// <summary>Provides programmatic access to the connection strings configuration-file section.</summary>
	// Token: 0x0200003E RID: 62
	public sealed class ConnectionStringsSection : ConfigurationSection
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00007CD0 File Offset: 0x00005ED0
		static ConnectionStringsSection()
		{
			ConnectionStringsSection._properties.Add(ConnectionStringsSection._propConnectionStrings);
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> collection of <see cref="T:System.Configuration.ConnectionStringSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> collection of <see cref="T:System.Configuration.ConnectionStringSettings" /> objects.</returns>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00007D14 File Offset: 0x00005F14
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return (ConnectionStringSettingsCollection)base[ConnectionStringsSection._propConnectionStrings];
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00007D28 File Offset: 0x00005F28
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionStringsSection._properties;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007D30 File Offset: 0x00005F30
		protected internal override object GetRuntimeObject()
		{
			return base.GetRuntimeObject();
		}

		// Token: 0x040000C6 RID: 198
		private static readonly ConfigurationProperty _propConnectionStrings = new ConfigurationProperty(string.Empty, typeof(ConnectionStringSettingsCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x040000C7 RID: 199
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
