using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Gets the section group information for the networking namespaces. This class cannot be inherited.</summary>
	// Token: 0x020002DC RID: 732
	public sealed class NetSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.NetSectionGroup" /> class. </summary>
		// Token: 0x06001905 RID: 6405 RVA: 0x0004513C File Offset: 0x0004333C
		[MonoTODO]
		public NetSectionGroup()
		{
		}

		/// <summary>Gets the configuration section containing the authentication modules registered for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.AuthenticationModulesSection" /> object.</returns>
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x00045144 File Offset: 0x00043344
		[ConfigurationProperty("authenticationModules")]
		public AuthenticationModulesSection AuthenticationModules
		{
			get
			{
				return (AuthenticationModulesSection)base.Sections["authenticationModules"];
			}
		}

		/// <summary>Gets the configuration section containing the connection management settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ConnectionManagementSection" /> object.</returns>
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x0004515C File Offset: 0x0004335C
		[ConfigurationProperty("connectionManagement")]
		public ConnectionManagementSection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementSection)base.Sections["connectionManagement"];
			}
		}

		/// <summary>Gets the configuration section containing the default Web proxy server settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.DefaultProxySection" /> object.</returns>
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x00045174 File Offset: 0x00043374
		[ConfigurationProperty("defaultProxy")]
		public DefaultProxySection DefaultProxy
		{
			get
			{
				return (DefaultProxySection)base.Sections["defaultProxy"];
			}
		}

		/// <summary>Gets the configuration section containing the SMTP client e-mail settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.MailSettingsSectionGroup" /> object.</returns>
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0004518C File Offset: 0x0004338C
		public MailSettingsSectionGroup MailSettings
		{
			get
			{
				return (MailSettingsSectionGroup)base.SectionGroups["mailSettings"];
			}
		}

		/// <summary>Gets the configuration section containing the cache configuration settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.RequestCachingSection" /> object.</returns>
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x000451A4 File Offset: 0x000433A4
		[ConfigurationProperty("requestCaching")]
		public RequestCachingSection RequestCaching
		{
			get
			{
				return (RequestCachingSection)base.Sections["requestCaching"];
			}
		}

		/// <summary>Gets the configuration section containing the network settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SettingsSection" /> object.</returns>
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x000451BC File Offset: 0x000433BC
		[ConfigurationProperty("settings")]
		public SettingsSection Settings
		{
			get
			{
				return (SettingsSection)base.Sections["settings"];
			}
		}

		/// <summary>Gets the configuration section containing the modules registered for use with the <see cref="T:System.Net.WebRequest" /> class.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.WebRequestModulesSection" /> object.</returns>
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x000451D4 File Offset: 0x000433D4
		[ConfigurationProperty("webRequestModules")]
		public WebRequestModulesSection WebRequestModules
		{
			get
			{
				return (WebRequestModulesSection)base.Sections["webRequestModules"];
			}
		}

		/// <summary>Gets the System.Net configuration section group from the specified configuration file.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.NetSectionGroup" /> that represents the System.Net settings in <paramref name="config" />.</returns>
		/// <param name="config">A <see cref="T:System.Configuration.Configuration" /> that represents a configuration file.</param>
		// Token: 0x0600190D RID: 6413 RVA: 0x000451EC File Offset: 0x000433EC
		[MonoTODO]
		public static NetSectionGroup GetSectionGroup(Configuration config)
		{
			throw new NotImplementedException();
		}
	}
}
