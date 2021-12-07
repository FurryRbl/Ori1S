using System;
using System.Runtime.InteropServices;

namespace System.Configuration
{
	/// <summary>Defines the configuration file mapping for the machine configuration file. </summary>
	// Token: 0x02000028 RID: 40
	public class ConfigurationFileMap : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationFileMap" /> class. </summary>
		// Token: 0x0600019D RID: 413 RVA: 0x00005F58 File Offset: 0x00004158
		public ConfigurationFileMap()
		{
			this.machineConfigFilename = RuntimeEnvironment.SystemConfigurationFile;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationFileMap" /> class based on the supplied parameter.</summary>
		/// <param name="machineConfigFilename">The name of the machine configuration file.</param>
		// Token: 0x0600019E RID: 414 RVA: 0x00005F6C File Offset: 0x0000416C
		public ConfigurationFileMap(string machineConfigFilename)
		{
			this.machineConfigFilename = machineConfigFilename;
		}

		/// <summary>Gets or sets the name of the machine configuration file name.</summary>
		/// <returns>The machine configuration file name.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00005F7C File Offset: 0x0000417C
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00005F84 File Offset: 0x00004184
		public string MachineConfigFilename
		{
			get
			{
				return this.machineConfigFilename;
			}
			set
			{
				this.machineConfigFilename = value;
			}
		}

		/// <summary>Creates a copy of the existing <see cref="T:System.Configuration.ConfigurationFileMap" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationFileMap" /> object.</returns>
		// Token: 0x060001A1 RID: 417 RVA: 0x00005F90 File Offset: 0x00004190
		public virtual object Clone()
		{
			return new ConfigurationFileMap(this.machineConfigFilename);
		}

		// Token: 0x0400007C RID: 124
		private string machineConfigFilename;
	}
}
