using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for authentication modules. This class cannot be inherited.</summary>
	// Token: 0x020002C9 RID: 713
	public sealed class AuthenticationModulesSection : ConfigurationSection
	{
		// Token: 0x06001886 RID: 6278 RVA: 0x00043A4C File Offset: 0x00041C4C
		static AuthenticationModulesSection()
		{
			AuthenticationModulesSection.properties = new ConfigurationPropertyCollection();
			AuthenticationModulesSection.properties.Add(AuthenticationModulesSection.authenticationModulesProp);
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x00043A90 File Offset: 0x00041C90
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthenticationModulesSection.properties;
			}
		}

		/// <summary>Gets the collection of authentication modules in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.AuthenticationModuleElementCollection" /> that contains the registered authentication modules. </returns>
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x00043A98 File Offset: 0x00041C98
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public AuthenticationModuleElementCollection AuthenticationModules
		{
			get
			{
				return (AuthenticationModuleElementCollection)base[AuthenticationModulesSection.authenticationModulesProp];
			}
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x00043AAC File Offset: 0x00041CAC
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00043AB0 File Offset: 0x00041CB0
		[MonoTODO]
		protected override void InitializeDefault()
		{
		}

		// Token: 0x04000FB3 RID: 4019
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000FB4 RID: 4020
		private static ConfigurationProperty authenticationModulesProp = new ConfigurationProperty(string.Empty, typeof(AuthenticationModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
