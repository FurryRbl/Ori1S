using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Identifies the configuration settings for Web proxy server. This class cannot be inherited.</summary>
	// Token: 0x020002DE RID: 734
	public sealed class ProxyElement : ConfigurationElement
	{
		// Token: 0x06001914 RID: 6420 RVA: 0x0004527C File Offset: 0x0004347C
		static ProxyElement()
		{
			ProxyElement.properties = new ConfigurationPropertyCollection();
			ProxyElement.properties.Add(ProxyElement.bypassOnLocalProp);
			ProxyElement.properties.Add(ProxyElement.proxyAddressProp);
			ProxyElement.properties.Add(ProxyElement.scriptLocationProp);
			ProxyElement.properties.Add(ProxyElement.useSystemDefaultProp);
		}

		/// <summary>Gets and sets an <see cref="T:System.Net.Configuration.ProxyElement.AutoDetectValues" /> value that controls whether the Web proxy is automatically detected.</summary>
		/// <returns>
		///   <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.True" /> if the <see cref="T:System.Net.WebProxy" /> is automatically detected; <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.False" /> if the <see cref="T:System.Net.WebProxy" /> is not automatically detected; or <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.Unspecified" />.</returns>
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x00045360 File Offset: 0x00043560
		// (set) Token: 0x06001916 RID: 6422 RVA: 0x00045374 File Offset: 0x00043574
		[ConfigurationProperty("autoDetect", DefaultValue = "Unspecified")]
		public ProxyElement.AutoDetectValues AutoDetect
		{
			get
			{
				return (ProxyElement.AutoDetectValues)((int)base[ProxyElement.autoDetectProp]);
			}
			set
			{
				base[ProxyElement.autoDetectProp] = value;
			}
		}

		/// <summary>Gets and sets a value that indicates whether local resources are retrieved by using a Web proxy server.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ProxyElement.BypassOnLocalValues" />.</returns>
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x00045388 File Offset: 0x00043588
		// (set) Token: 0x06001918 RID: 6424 RVA: 0x0004539C File Offset: 0x0004359C
		[ConfigurationProperty("bypassonlocal", DefaultValue = "Unspecified")]
		public ProxyElement.BypassOnLocalValues BypassOnLocal
		{
			get
			{
				return (ProxyElement.BypassOnLocalValues)((int)base[ProxyElement.bypassOnLocalProp]);
			}
			set
			{
				base[ProxyElement.bypassOnLocalProp] = value;
			}
		}

		/// <summary>Gets and sets the URI that identifies the Web proxy server to use.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a URI.</returns>
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x000453B0 File Offset: 0x000435B0
		// (set) Token: 0x0600191A RID: 6426 RVA: 0x000453C4 File Offset: 0x000435C4
		[ConfigurationProperty("proxyaddress")]
		public System.Uri ProxyAddress
		{
			get
			{
				return (System.Uri)base[ProxyElement.proxyAddressProp];
			}
			set
			{
				base[ProxyElement.proxyAddressProp] = value;
			}
		}

		/// <summary>Gets and sets an <see cref="T:System.Uri" /> value that specifies the location of the automatic proxy detection script.</summary>
		/// <returns>A <see cref="T:System.Uri" /> specifying the location of the automatic proxy detection script.</returns>
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x000453D4 File Offset: 0x000435D4
		// (set) Token: 0x0600191C RID: 6428 RVA: 0x000453E8 File Offset: 0x000435E8
		[ConfigurationProperty("scriptLocation")]
		public System.Uri ScriptLocation
		{
			get
			{
				return (System.Uri)base[ProxyElement.scriptLocationProp];
			}
			set
			{
				base[ProxyElement.scriptLocationProp] = value;
			}
		}

		/// <summary>Gets and sets a <see cref="T:System.Boolean" /> value that controls whether the Internet Explorer Web proxy settings are used.</summary>
		/// <returns>true if the Internet Explorer LAN settings are used to detect and configure the default <see cref="T:System.Net.WebProxy" /> used for requests; otherwise, false.</returns>
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x000453F8 File Offset: 0x000435F8
		// (set) Token: 0x0600191E RID: 6430 RVA: 0x0004540C File Offset: 0x0004360C
		[ConfigurationProperty("usesystemdefault", DefaultValue = "Unspecified")]
		public ProxyElement.UseSystemDefaultValues UseSystemDefault
		{
			get
			{
				return (ProxyElement.UseSystemDefaultValues)((int)base[ProxyElement.useSystemDefaultProp]);
			}
			set
			{
				base[ProxyElement.useSystemDefaultProp] = value;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x00045420 File Offset: 0x00043620
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProxyElement.properties;
			}
		}

		// Token: 0x04000FD6 RID: 4054
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000FD7 RID: 4055
		private static ConfigurationProperty autoDetectProp = new ConfigurationProperty("autoDetect", typeof(ProxyElement.AutoDetectValues), ProxyElement.AutoDetectValues.Unspecified);

		// Token: 0x04000FD8 RID: 4056
		private static ConfigurationProperty bypassOnLocalProp = new ConfigurationProperty("bypassonlocal", typeof(ProxyElement.BypassOnLocalValues), ProxyElement.BypassOnLocalValues.Unspecified);

		// Token: 0x04000FD9 RID: 4057
		private static ConfigurationProperty proxyAddressProp = new ConfigurationProperty("proxyaddress", typeof(System.Uri), null);

		// Token: 0x04000FDA RID: 4058
		private static ConfigurationProperty scriptLocationProp = new ConfigurationProperty("scriptLocation", typeof(System.Uri), null);

		// Token: 0x04000FDB RID: 4059
		private static ConfigurationProperty useSystemDefaultProp = new ConfigurationProperty("UseSystemDefault", typeof(ProxyElement.UseSystemDefaultValues), ProxyElement.UseSystemDefaultValues.Unspecified);

		/// <summary>Specifies whether the proxy is bypassed for local resources.</summary>
		// Token: 0x020002DF RID: 735
		public enum BypassOnLocalValues
		{
			/// <summary>Unspecified.</summary>
			// Token: 0x04000FDD RID: 4061
			Unspecified = -1,
			/// <summary>Access local resources directly.</summary>
			// Token: 0x04000FDE RID: 4062
			True = 1,
			/// <summary>All requests for local resources should go through the proxy</summary>
			// Token: 0x04000FDF RID: 4063
			False = 0
		}

		/// <summary>Specifies whether to use the local system proxy settings to determine whether the proxy is bypassed for local resources.</summary>
		// Token: 0x020002E0 RID: 736
		public enum UseSystemDefaultValues
		{
			/// <summary>The system default proxy setting is unspecified.</summary>
			// Token: 0x04000FE1 RID: 4065
			Unspecified = -1,
			/// <summary>Use system default proxy setting values.</summary>
			// Token: 0x04000FE2 RID: 4066
			True = 1,
			/// <summary>Do not use system default proxy setting values</summary>
			// Token: 0x04000FE3 RID: 4067
			False = 0
		}

		/// <summary>Specifies whether the proxy is automatically detected.</summary>
		// Token: 0x020002E1 RID: 737
		public enum AutoDetectValues
		{
			/// <summary>Unspecified.</summary>
			// Token: 0x04000FE5 RID: 4069
			Unspecified = -1,
			/// <summary>The proxy is automatically detected.</summary>
			// Token: 0x04000FE6 RID: 4070
			True = 1,
			/// <summary>The proxy is not automatically detected.</summary>
			// Token: 0x04000FE7 RID: 4071
			False = 0
		}
	}
}
