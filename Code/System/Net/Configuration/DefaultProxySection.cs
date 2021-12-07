using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for Web proxy server usage. This class cannot be inherited.</summary>
	// Token: 0x020002D3 RID: 723
	public sealed class DefaultProxySection : ConfigurationSection
	{
		// Token: 0x060018C9 RID: 6345 RVA: 0x000446F8 File Offset: 0x000428F8
		static DefaultProxySection()
		{
			DefaultProxySection.properties = new ConfigurationPropertyCollection();
			DefaultProxySection.properties.Add(DefaultProxySection.bypassListProp);
			DefaultProxySection.properties.Add(DefaultProxySection.moduleProp);
			DefaultProxySection.properties.Add(DefaultProxySection.proxyProp);
		}

		/// <summary>Gets the collection of resources that are not obtained using the Web proxy server.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.BypassElementCollection" /> that contains the addresses of resources that bypass the Web proxy server. </returns>
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x000447C8 File Offset: 0x000429C8
		[ConfigurationProperty("bypasslist")]
		public BypassElementCollection BypassList
		{
			get
			{
				return (BypassElementCollection)base[DefaultProxySection.bypassListProp];
			}
		}

		/// <summary>Gets or sets whether a Web proxy is used.</summary>
		/// <returns>true if a Web proxy will be used; otherwise, false.</returns>
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x000447DC File Offset: 0x000429DC
		// (set) Token: 0x060018CC RID: 6348 RVA: 0x000447F0 File Offset: 0x000429F0
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool Enabled
		{
			get
			{
				return (bool)base[DefaultProxySection.enabledProp];
			}
			set
			{
				base[DefaultProxySection.enabledProp] = value;
			}
		}

		/// <summary>Gets the type information for a custom Web proxy implementation.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ModuleElement" />. </returns>
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x00044804 File Offset: 0x00042A04
		[ConfigurationProperty("module")]
		public ModuleElement Module
		{
			get
			{
				return (ModuleElement)base[DefaultProxySection.moduleProp];
			}
		}

		/// <summary>Gets the URI that identifies the Web proxy server to use.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ProxyElement" />. </returns>
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x00044818 File Offset: 0x00042A18
		[ConfigurationProperty("proxy")]
		public ProxyElement Proxy
		{
			get
			{
				return (ProxyElement)base[DefaultProxySection.proxyProp];
			}
		}

		/// <summary>Gets or sets whether default credentials are to be used to access a Web proxy server.</summary>
		/// <returns>true if default credentials are to be used; otherwise, false.</returns>
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x0004482C File Offset: 0x00042A2C
		// (set) Token: 0x060018D0 RID: 6352 RVA: 0x00044840 File Offset: 0x00042A40
		[ConfigurationProperty("useDefaultCredentials", DefaultValue = "False")]
		public bool UseDefaultCredentials
		{
			get
			{
				return (bool)base[DefaultProxySection.useDefaultCredentialsProp];
			}
			set
			{
				base[DefaultProxySection.useDefaultCredentialsProp] = value;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x00044854 File Offset: 0x00042A54
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DefaultProxySection.properties;
			}
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0004485C File Offset: 0x00042A5C
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00044860 File Offset: 0x00042A60
		[MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
		}

		// Token: 0x04000FBE RID: 4030
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000FBF RID: 4031
		private static ConfigurationProperty bypassListProp = new ConfigurationProperty("bypasslist", typeof(BypassElementCollection), null);

		// Token: 0x04000FC0 RID: 4032
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x04000FC1 RID: 4033
		private static ConfigurationProperty moduleProp = new ConfigurationProperty("module", typeof(ModuleElement), null);

		// Token: 0x04000FC2 RID: 4034
		private static ConfigurationProperty proxyProp = new ConfigurationProperty("proxy", typeof(ProxyElement), null);

		// Token: 0x04000FC3 RID: 4035
		private static ConfigurationProperty useDefaultCredentialsProp = new ConfigurationProperty("useDefaultCredentials", typeof(bool), false);
	}
}
