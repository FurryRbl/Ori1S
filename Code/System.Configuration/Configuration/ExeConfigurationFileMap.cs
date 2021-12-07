using System;

namespace System.Configuration
{
	/// <summary>Defines the configuration file mapping for an .exe application. This class cannot be inherited.</summary>
	// Token: 0x02000046 RID: 70
	public sealed class ExeConfigurationFileMap : ConfigurationFileMap
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ExeConfigurationFileMap" /> class.</summary>
		// Token: 0x06000295 RID: 661 RVA: 0x0000838C File Offset: 0x0000658C
		public ExeConfigurationFileMap()
		{
			this.exeConfigFilename = string.Empty;
			this.localUserConfigFilename = string.Empty;
			this.roamingUserConfigFilename = string.Empty;
		}

		/// <summary>Gets or sets the name of the configuration file.</summary>
		/// <returns>The configuration file name.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000083B8 File Offset: 0x000065B8
		// (set) Token: 0x06000297 RID: 663 RVA: 0x000083C0 File Offset: 0x000065C0
		public string ExeConfigFilename
		{
			get
			{
				return this.exeConfigFilename;
			}
			set
			{
				this.exeConfigFilename = value;
			}
		}

		/// <summary>Gets or sets the name of the configuration file for the local user.</summary>
		/// <returns>The configuration file name.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000083CC File Offset: 0x000065CC
		// (set) Token: 0x06000299 RID: 665 RVA: 0x000083D4 File Offset: 0x000065D4
		public string LocalUserConfigFilename
		{
			get
			{
				return this.localUserConfigFilename;
			}
			set
			{
				this.localUserConfigFilename = value;
			}
		}

		/// <summary>Gets or sets the name of the configuration file for the roaming user.</summary>
		/// <returns>The configuration file name.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600029A RID: 666 RVA: 0x000083E0 File Offset: 0x000065E0
		// (set) Token: 0x0600029B RID: 667 RVA: 0x000083E8 File Offset: 0x000065E8
		public string RoamingUserConfigFilename
		{
			get
			{
				return this.roamingUserConfigFilename;
			}
			set
			{
				this.roamingUserConfigFilename = value;
			}
		}

		/// <summary>Creates a copy of the existing <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object.</summary>
		/// <returns>An <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object.</returns>
		// Token: 0x0600029C RID: 668 RVA: 0x000083F4 File Offset: 0x000065F4
		public override object Clone()
		{
			return new ExeConfigurationFileMap
			{
				exeConfigFilename = this.exeConfigFilename,
				localUserConfigFilename = this.localUserConfigFilename,
				roamingUserConfigFilename = this.roamingUserConfigFilename,
				MachineConfigFilename = base.MachineConfigFilename
			};
		}

		// Token: 0x040000D4 RID: 212
		private string exeConfigFilename;

		// Token: 0x040000D5 RID: 213
		private string localUserConfigFilename;

		// Token: 0x040000D6 RID: 214
		private string roamingUserConfigFilename;
	}
}
