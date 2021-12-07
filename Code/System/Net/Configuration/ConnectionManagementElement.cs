using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the maximum number of connections to a remote computer. This class cannot be inherited.</summary>
	// Token: 0x020002CD RID: 717
	public sealed class ConnectionManagementElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> class. </summary>
		// Token: 0x060018AC RID: 6316 RVA: 0x00043C88 File Offset: 0x00041E88
		public ConnectionManagementElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> class with the specified address and connection limit information.</summary>
		/// <param name="address">A string that identifies the address of a remote computer.</param>
		/// <param name="maxConnection">An integer that identifies the maximum number of connections allowed to <paramref name="address" /> from the local computer.</param>
		// Token: 0x060018AD RID: 6317 RVA: 0x00043C90 File Offset: 0x00041E90
		public ConnectionManagementElement(string address, int maxConnection)
		{
			this.Address = address;
			this.MaxConnection = maxConnection;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00043CA8 File Offset: 0x00041EA8
		static ConnectionManagementElement()
		{
			ConnectionManagementElement.properties = new ConfigurationPropertyCollection();
			ConnectionManagementElement.properties.Add(ConnectionManagementElement.addressProp);
			ConnectionManagementElement.properties.Add(ConnectionManagementElement.maxConnectionProp);
		}

		/// <summary>Gets or sets the address for remote computers.</summary>
		/// <returns>A string that contains a regular expression describing an IP address or DNS name.</returns>
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x00043D18 File Offset: 0x00041F18
		// (set) Token: 0x060018B0 RID: 6320 RVA: 0x00043D2C File Offset: 0x00041F2C
		[ConfigurationProperty("address", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Address
		{
			get
			{
				return (string)base[ConnectionManagementElement.addressProp];
			}
			set
			{
				base[ConnectionManagementElement.addressProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of connections that can be made to a remote computer.</summary>
		/// <returns>An integer that specifies the maximum number of connections.</returns>
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x00043D3C File Offset: 0x00041F3C
		// (set) Token: 0x060018B2 RID: 6322 RVA: 0x00043D50 File Offset: 0x00041F50
		[ConfigurationProperty("maxconnection", DefaultValue = "1", Options = ConfigurationPropertyOptions.IsRequired)]
		public int MaxConnection
		{
			get
			{
				return (int)base[ConnectionManagementElement.maxConnectionProp];
			}
			set
			{
				base[ConnectionManagementElement.maxConnectionProp] = value;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x00043D64 File Offset: 0x00041F64
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionManagementElement.properties;
			}
		}

		// Token: 0x04000FB7 RID: 4023
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000FB8 RID: 4024
		private static ConfigurationProperty addressProp = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000FB9 RID: 4025
		private static ConfigurationProperty maxConnectionProp = new ConfigurationProperty("maxconnection", typeof(int), 1, ConfigurationPropertyOptions.IsRequired);
	}
}
