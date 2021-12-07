using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines an interface used by the .NET Framework to initialize configuration properties.</summary>
	// Token: 0x02000008 RID: 8
	[ComVisible(false)]
	public interface IConfigurationManagerInternal
	{
		/// <summary>Gets the configuration file name related to the application path.</summary>
		/// <returns>A string value representing a configuration file name.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000036 RID: 54
		string ApplicationConfigUri { get; }

		/// <summary>Gets the local configuration directory of the application based on the entry assembly.</summary>
		/// <returns>A string representing the local configuration directory.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000037 RID: 55
		string ExeLocalConfigDirectory { get; }

		/// <summary>Gets the local configuration path of the application based on the entry assembly.</summary>
		/// <returns>A string value representing the local configuration path of the application.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000038 RID: 56
		string ExeLocalConfigPath { get; }

		/// <summary>Gets the product name of the application based on the entry assembly.</summary>
		/// <returns>A string value representing the product name of the application.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000039 RID: 57
		string ExeProductName { get; }

		/// <summary>Gets the product version of the application based on the entry assembly.</summary>
		/// <returns>A string value representing the product version of the application.</returns>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003A RID: 58
		string ExeProductVersion { get; }

		/// <summary>Gets the roaming configuration directory of the application based on the entry assembly.</summary>
		/// <returns>A string value representing the roaming configuration directory of the application.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003B RID: 59
		string ExeRoamingConfigDirectory { get; }

		/// <summary>Gets the roaming user's configuration path based on the application's entry assembly.</summary>
		/// <returns>A string value representing the roaming user's configuration path.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003C RID: 60
		string ExeRoamingConfigPath { get; }

		/// <summary>Gets the configuration path for the Machine.config file.</summary>
		/// <returns>A string value representing the path of the Machine.config file.</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003D RID: 61
		string MachineConfigPath { get; }

		/// <summary>Gets a value representing the configuration system's status.</summary>
		/// <returns>true if the configuration system is in the process of being initialized; otherwise, false.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003E RID: 62
		bool SetConfigurationSystemInProgress { get; }

		/// <summary>Gets a value that specifies whether user configuration settings are supported.</summary>
		/// <returns>true if the configuration system supports user configuration settings; otherwise, false.</returns>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003F RID: 63
		bool SupportsUserConfig { get; }

		/// <summary>Gets the name of the file used to store user configuration settings.</summary>
		/// <returns>A string specifying the name of the file used to store user configuration.</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000040 RID: 64
		string UserConfigFilename { get; }
	}
}
