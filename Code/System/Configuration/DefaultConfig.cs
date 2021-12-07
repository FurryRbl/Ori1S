using System;
using System.Runtime.CompilerServices;

namespace System.Configuration
{
	// Token: 0x020001D0 RID: 464
	internal class DefaultConfig : IConfigurationSystem
	{
		// Token: 0x06001032 RID: 4146 RVA: 0x0002AE88 File Offset: 0x00029088
		private DefaultConfig()
		{
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0002AE9C File Offset: 0x0002909C
		public static DefaultConfig GetInstance()
		{
			return DefaultConfig.instance;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0002AEA4 File Offset: 0x000290A4
		[Obsolete("This method is obsolete.  Please use System.Configuration.ConfigurationManager.GetConfig")]
		public object GetConfig(string sectionName)
		{
			this.Init();
			return this.config.GetConfig(sectionName);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0002AEB8 File Offset: 0x000290B8
		public void Init()
		{
			lock (this)
			{
				if (this.config == null)
				{
					ConfigurationData configurationData = new ConfigurationData();
					if (!configurationData.LoadString(DefaultConfig.GetBundledMachineConfig()))
					{
						if (!configurationData.Load(DefaultConfig.GetMachineConfigPath()))
						{
							throw new ConfigurationException("Cannot find " + DefaultConfig.GetMachineConfigPath());
						}
					}
					string appConfigPath = DefaultConfig.GetAppConfigPath();
					if (appConfigPath == null)
					{
						this.config = configurationData;
					}
					else
					{
						ConfigurationData configurationData2 = new ConfigurationData(configurationData);
						if (configurationData2.Load(appConfigPath))
						{
							this.config = configurationData2;
						}
						else
						{
							this.config = configurationData;
						}
					}
				}
			}
		}

		// Token: 0x06001037 RID: 4151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_bundled_machine_config();

		// Token: 0x06001038 RID: 4152 RVA: 0x0002AF84 File Offset: 0x00029184
		internal static string GetBundledMachineConfig()
		{
			return DefaultConfig.get_bundled_machine_config();
		}

		// Token: 0x06001039 RID: 4153
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_machine_config_path();

		// Token: 0x0600103A RID: 4154 RVA: 0x0002AF8C File Offset: 0x0002918C
		internal static string GetMachineConfigPath()
		{
			return DefaultConfig.get_machine_config_path();
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0002AF94 File Offset: 0x00029194
		private static string GetAppConfigPath()
		{
			AppDomainSetup setupInformation = AppDomain.CurrentDomain.SetupInformation;
			string configurationFile = setupInformation.ConfigurationFile;
			if (configurationFile == null || configurationFile.Length == 0)
			{
				return null;
			}
			return configurationFile;
		}

		// Token: 0x04000482 RID: 1154
		private static readonly DefaultConfig instance = new DefaultConfig();

		// Token: 0x04000483 RID: 1155
		private ConfigurationData config;
	}
}
