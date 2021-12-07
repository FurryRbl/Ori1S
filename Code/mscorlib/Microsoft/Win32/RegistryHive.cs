using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	/// <summary>Represents the possible values for a top-level node on a foreign machine.</summary>
	// Token: 0x0200006A RID: 106
	[ComVisible(true)]
	[Serializable]
	public enum RegistryHive
	{
		/// <summary>Represents the HKEY_CLASSES_ROOT base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x040000EE RID: 238
		ClassesRoot = -2147483648,
		/// <summary>Represents the HKEY_CURRENT_CONFIG base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x040000EF RID: 239
		CurrentConfig = -2147483643,
		/// <summary>Represents the HKEY_CURRENT_USER base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x040000F0 RID: 240
		CurrentUser = -2147483647,
		/// <summary>Represents the HKEY_DYN_DATA base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x040000F1 RID: 241
		DynData = -2147483642,
		/// <summary>Represents the HKEY_LOCAL_MACHINE base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x040000F2 RID: 242
		LocalMachine = -2147483646,
		/// <summary>Represents the HKEY_PERFORMANCE_DATA base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x040000F3 RID: 243
		PerformanceData = -2147483644,
		/// <summary>Represents the HKEY_USERS base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x040000F4 RID: 244
		Users = -2147483645
	}
}
