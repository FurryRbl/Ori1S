using System;
using System.Configuration.Internal;
using System.Reflection;

namespace System.Configuration
{
	// Token: 0x02000017 RID: 23
	internal class ClientConfigurationSystem : IInternalConfigSystem
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00002A5C File Offset: 0x00000C5C
		object IInternalConfigSystem.GetSection(string configKey)
		{
			ConfigurationSection section = this.Configuration.GetSection(configKey);
			return (section == null) ? null : section.GetRuntimeObject();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002A88 File Offset: 0x00000C88
		void IInternalConfigSystem.RefreshConfig(string sectionName)
		{
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002A8C File Offset: 0x00000C8C
		bool IInternalConfigSystem.SupportsUserConfig
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002A90 File Offset: 0x00000C90
		private Configuration Configuration
		{
			get
			{
				if (this.cfg == null)
				{
					Assembly entryAssembly = Assembly.GetEntryAssembly();
					try
					{
						this.cfg = ConfigurationManager.OpenExeConfigurationInternal(ConfigurationUserLevel.None, entryAssembly, null);
					}
					catch (Exception inner)
					{
						throw new ConfigurationErrorsException("Error Initializing the configuration system.", inner);
					}
				}
				return this.cfg;
			}
		}

		// Token: 0x0400002E RID: 46
		private Configuration cfg;
	}
}
