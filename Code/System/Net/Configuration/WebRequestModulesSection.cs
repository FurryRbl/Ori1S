using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for Web request modules. This class cannot be inherited.</summary>
	// Token: 0x020002ED RID: 749
	public sealed class WebRequestModulesSection : ConfigurationSection
	{
		// Token: 0x06001988 RID: 6536 RVA: 0x00046094 File Offset: 0x00044294
		static WebRequestModulesSection()
		{
			WebRequestModulesSection.properties = new ConfigurationPropertyCollection();
			WebRequestModulesSection.properties.Add(WebRequestModulesSection.webRequestModulesProp);
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x000460D8 File Offset: 0x000442D8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebRequestModulesSection.properties;
			}
		}

		/// <summary>Gets the collection of Web request modules in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.WebRequestModuleElementCollection" /> containing the registered Web request modules. </returns>
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x000460E0 File Offset: 0x000442E0
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public WebRequestModuleElementCollection WebRequestModules
		{
			get
			{
				return (WebRequestModuleElementCollection)base[WebRequestModulesSection.webRequestModulesProp];
			}
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x000460F4 File Offset: 0x000442F4
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x000460F8 File Offset: 0x000442F8
		[MonoTODO]
		protected override void InitializeDefault()
		{
		}

		// Token: 0x04001007 RID: 4103
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04001008 RID: 4104
		private static ConfigurationProperty webRequestModulesProp = new ConfigurationProperty(string.Empty, typeof(WebRequestModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
