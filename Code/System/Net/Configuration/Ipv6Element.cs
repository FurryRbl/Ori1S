using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Determines whether Internet Protocol version 6 is enabled on the local computer. This class cannot be inherited.</summary>
	// Token: 0x020002D7 RID: 727
	public sealed class Ipv6Element : ConfigurationElement
	{
		// Token: 0x060018F5 RID: 6389 RVA: 0x00044C24 File Offset: 0x00042E24
		static Ipv6Element()
		{
			Ipv6Element.properties = new ConfigurationPropertyCollection();
			Ipv6Element.properties.Add(Ipv6Element.enabledProp);
		}

		/// <summary>Gets or sets a Boolean value that indicates whether Internet Protocol version 6 is enabled on the local computer.</summary>
		/// <returns>true if IPv6 is enabled; otherwise, false.</returns>
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x00044C6C File Offset: 0x00042E6C
		// (set) Token: 0x060018F7 RID: 6391 RVA: 0x00044C80 File Offset: 0x00042E80
		[ConfigurationProperty("enabled", DefaultValue = "False")]
		public bool Enabled
		{
			get
			{
				return (bool)base[Ipv6Element.enabledProp];
			}
			set
			{
				base[Ipv6Element.enabledProp] = value;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x00044C94 File Offset: 0x00042E94
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return Ipv6Element.properties;
			}
		}

		// Token: 0x04000FD0 RID: 4048
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000FD1 RID: 4049
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);
	}
}
