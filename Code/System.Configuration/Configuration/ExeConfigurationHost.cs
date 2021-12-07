﻿using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x02000052 RID: 82
	internal class ExeConfigurationHost : InternalConfigurationHost
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x00008AEC File Offset: 0x00006CEC
		public override void Init(IInternalConfigRoot root, params object[] hostInitParams)
		{
			this.map = (ExeConfigurationFileMap)hostInitParams[0];
			this.level = (ConfigurationUserLevel)((int)hostInitParams[1]);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00008B0C File Offset: 0x00006D0C
		public override string GetStreamName(string configPath)
		{
			switch (configPath)
			{
			case "exe":
				return this.map.ExeConfigFilename;
			case "local":
				return this.map.LocalUserConfigFilename;
			case "roaming":
				return this.map.RoamingUserConfigFilename;
			case "machine":
				return this.map.MachineConfigFilename;
			}
			ConfigurationUserLevel configurationUserLevel = this.level;
			if (configurationUserLevel == ConfigurationUserLevel.None)
			{
				return this.map.ExeConfigFilename;
			}
			if (configurationUserLevel == ConfigurationUserLevel.PerUserRoaming)
			{
				return this.map.RoamingUserConfigFilename;
			}
			if (configurationUserLevel != ConfigurationUserLevel.PerUserRoamingAndLocal)
			{
				return this.map.MachineConfigFilename;
			}
			return this.map.LocalUserConfigFilename;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00008C18 File Offset: 0x00006E18
		public override void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams)
		{
			this.map = (ExeConfigurationFileMap)hostInitConfigurationParams[0];
			if (hostInitConfigurationParams.Length > 1 && hostInitConfigurationParams[1] is ConfigurationUserLevel)
			{
				this.level = (ConfigurationUserLevel)((int)hostInitConfigurationParams[1]);
			}
			if (locationSubPath == null)
			{
				ConfigurationUserLevel configurationUserLevel = this.level;
				if (configurationUserLevel != ConfigurationUserLevel.PerUserRoaming)
				{
					if (configurationUserLevel == ConfigurationUserLevel.PerUserRoamingAndLocal)
					{
						if (this.map.LocalUserConfigFilename == null)
						{
							throw new ArgumentException("LocalUserConfigFilename must be set correctly");
						}
						locationSubPath = "local";
					}
				}
				else
				{
					if (this.map.RoamingUserConfigFilename == null)
					{
						throw new ArgumentException("RoamingUserConfigFilename must be set correctly");
					}
					locationSubPath = "roaming";
				}
			}
			configPath = null;
			string text = null;
			locationConfigPath = null;
			if (locationSubPath == "exe" || (locationSubPath == null && this.map.ExeConfigFilename != null))
			{
				configPath = "exe";
				text = "local";
				locationConfigPath = this.map.ExeConfigFilename;
			}
			if (locationSubPath == "local" && this.map.LocalUserConfigFilename != null)
			{
				configPath = "local";
				text = "roaming";
				locationConfigPath = this.map.LocalUserConfigFilename;
			}
			if (locationSubPath == "roaming" && this.map.RoamingUserConfigFilename != null)
			{
				configPath = "roaming";
				text = "machine";
				locationConfigPath = this.map.RoamingUserConfigFilename;
			}
			if ((locationSubPath == "machine" || configPath == null) && this.map.MachineConfigFilename != null)
			{
				configPath = "machine";
				text = null;
			}
			locationSubPath = text;
		}

		// Token: 0x040000E7 RID: 231
		private ExeConfigurationFileMap map;

		// Token: 0x040000E8 RID: 232
		private ConfigurationUserLevel level;
	}
}
