using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x02000053 RID: 83
	internal class MachineConfigurationHost : InternalConfigurationHost
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x00008DC8 File Offset: 0x00006FC8
		public override void Init(IInternalConfigRoot root, params object[] hostInitParams)
		{
			this.map = (ConfigurationFileMap)hostInitParams[0];
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public override string GetStreamName(string configPath)
		{
			return this.map.MachineConfigFilename;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00008DE8 File Offset: 0x00006FE8
		public override void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams)
		{
			this.map = (ConfigurationFileMap)hostInitConfigurationParams[0];
			locationSubPath = null;
			configPath = null;
			locationConfigPath = null;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00008E04 File Offset: 0x00007004
		public override bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			return true;
		}

		// Token: 0x040000EA RID: 234
		private ConfigurationFileMap map;
	}
}
