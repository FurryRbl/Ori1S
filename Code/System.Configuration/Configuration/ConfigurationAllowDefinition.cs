using System;

namespace System.Configuration
{
	/// <summary>Specifies the locations within the configuration-file hierarchy that can set or override the properties contained within a <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
	// Token: 0x0200001D RID: 29
	public enum ConfigurationAllowDefinition
	{
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined only in the Machine.config file.</summary>
		// Token: 0x04000048 RID: 72
		MachineOnly,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined in either the Machine.config file or the machine-level Web.config file found in the same directory as Machine.config, but not in application Web.config files.</summary>
		// Token: 0x04000049 RID: 73
		MachineToWebRoot = 100,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined in either the Machine.config file, the machine-level Web.config file found in the same directory as Machine.config, or the top-level application Web.config file found in the virtual-directory root, but not in subdirectories of a virtual root.</summary>
		// Token: 0x0400004A RID: 74
		MachineToApplication = 200,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined anywhere.</summary>
		// Token: 0x0400004B RID: 75
		Everywhere = 300
	}
}
