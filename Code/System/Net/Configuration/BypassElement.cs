using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the address information for resources that are not retrieved using a proxy server. This class cannot be inherited.</summary>
	// Token: 0x020002CB RID: 715
	public sealed class BypassElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.BypassElement" /> class. </summary>
		// Token: 0x06001899 RID: 6297 RVA: 0x00043B5C File Offset: 0x00041D5C
		public BypassElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.BypassElement" /> class with the specified type information.</summary>
		/// <param name="address">A string that identifies the address of a resource.</param>
		// Token: 0x0600189A RID: 6298 RVA: 0x00043B64 File Offset: 0x00041D64
		public BypassElement(string address)
		{
			this.Address = address;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00043B74 File Offset: 0x00041D74
		static BypassElement()
		{
			BypassElement.properties = new ConfigurationPropertyCollection();
			BypassElement.properties.Add(BypassElement.addressProp);
		}

		/// <summary>Gets or sets the addresses of resources that bypass the proxy server.</summary>
		/// <returns>A string that identifies a resource.</returns>
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x00043BB8 File Offset: 0x00041DB8
		// (set) Token: 0x0600189D RID: 6301 RVA: 0x00043BCC File Offset: 0x00041DCC
		[ConfigurationProperty("address", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Address
		{
			get
			{
				return (string)base[BypassElement.addressProp];
			}
			set
			{
				base[BypassElement.addressProp] = value;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x00043BDC File Offset: 0x00041DDC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BypassElement.properties;
			}
		}

		// Token: 0x04000FB5 RID: 4021
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000FB6 RID: 4022
		private static ConfigurationProperty addressProp = new ConfigurationProperty("Address", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
