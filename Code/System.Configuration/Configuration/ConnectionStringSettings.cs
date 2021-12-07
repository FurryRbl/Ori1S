using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Represents a single, named connection string in the connection strings configuration file section.</summary>
	// Token: 0x02000040 RID: 64
	public sealed class ConnectionStringSettings : ConfigurationElement
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Configuration.ConnectionStringSettings" /> class.</summary>
		// Token: 0x0600026A RID: 618 RVA: 0x00007ED4 File Offset: 0x000060D4
		public ConnectionStringSettings()
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Configuration.ConnectionStringSettings" /> class.</summary>
		/// <param name="name">The name of the connection string.</param>
		/// <param name="connectionString">The connection string.</param>
		// Token: 0x0600026B RID: 619 RVA: 0x00007EDC File Offset: 0x000060DC
		public ConnectionStringSettings(string name, string connectionString) : this(name, connectionString, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Configuration.ConnectionStringSettings" /> object.</summary>
		/// <param name="name">The name of the connection string.</param>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="providerName">The name of the provider to use with the connection string.</param>
		// Token: 0x0600026C RID: 620 RVA: 0x00007EEC File Offset: 0x000060EC
		public ConnectionStringSettings(string name, string connectionString, string providerName)
		{
			this.Name = name;
			this.ConnectionString = connectionString;
			this.ProviderName = providerName;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00007F14 File Offset: 0x00006114
		static ConnectionStringSettings()
		{
			ConnectionStringSettings._propConnectionString = new ConfigurationProperty("connectionString", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);
			ConnectionStringSettings._properties.Add(ConnectionStringSettings._propName);
			ConnectionStringSettings._properties.Add(ConnectionStringSettings._propProviderName);
			ConnectionStringSettings._properties.Add(ConnectionStringSettings._propConnectionString);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00007FC4 File Offset: 0x000061C4
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionStringSettings._properties;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Configuration.ConnectionStringSettings" /> name.</summary>
		/// <returns>The string value assigned to the <see cref="P:System.Configuration.ConnectionStringSettings.Name" /> property.</returns>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00007FCC File Offset: 0x000061CC
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00007FE0 File Offset: 0x000061E0
		[ConfigurationProperty("name", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Name
		{
			get
			{
				return (string)base[ConnectionStringSettings._propName];
			}
			set
			{
				base[ConnectionStringSettings._propName] = value;
			}
		}

		/// <summary>Gets or sets the provider name property.</summary>
		/// <returns>Gets or sets the <see cref="P:System.Configuration.ConnectionStringSettings.ProviderName" /> property.</returns>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00007FF0 File Offset: 0x000061F0
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00008004 File Offset: 0x00006204
		[ConfigurationProperty("providerName", DefaultValue = "System.Data.SqlClient")]
		public string ProviderName
		{
			get
			{
				return (string)base[ConnectionStringSettings._propProviderName];
			}
			set
			{
				base[ConnectionStringSettings._propProviderName] = value;
			}
		}

		/// <summary>Gets or sets the connection string.</summary>
		/// <returns>The string value assigned to the <see cref="P:System.Configuration.ConnectionStringSettings.ConnectionString" /> property.</returns>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00008014 File Offset: 0x00006214
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00008028 File Offset: 0x00006228
		[ConfigurationProperty("connectionString", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string ConnectionString
		{
			get
			{
				return (string)base[ConnectionStringSettings._propConnectionString];
			}
			set
			{
				base[ConnectionStringSettings._propConnectionString] = value;
			}
		}

		/// <summary>Returns a string representation of the object.</summary>
		/// <returns>A string representation of the object.</returns>
		// Token: 0x06000275 RID: 629 RVA: 0x00008038 File Offset: 0x00006238
		public override string ToString()
		{
			return this.ConnectionString;
		}

		// Token: 0x040000C8 RID: 200
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x040000C9 RID: 201
		private static readonly ConfigurationProperty _propConnectionString;

		// Token: 0x040000CA RID: 202
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040000CB RID: 203
		private static readonly ConfigurationProperty _propProviderName = new ConfigurationProperty("providerName", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
	}
}
