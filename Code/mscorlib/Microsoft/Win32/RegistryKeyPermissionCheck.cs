using System;

namespace Microsoft.Win32
{
	/// <summary>Specifies whether security checks are performed when opening registry keys and accessing their name/value pairs.</summary>
	// Token: 0x02000068 RID: 104
	public enum RegistryKeyPermissionCheck
	{
		/// <summary>The registry key inherits the mode of its parent. Security checks are performed when trying to access subkeys or values, unless the parent was opened with <see cref="F:Microsoft.Win32.RegistryKeyPermissionCheck.ReadSubTree" /> or <see cref="F:Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree" /> mode.</summary>
		// Token: 0x040000E2 RID: 226
		Default,
		/// <summary>Security checks are not performed when accessing subkeys or values. A security check is performed when trying to open the current key, unless the parent was opened with <see cref="F:Microsoft.Win32.RegistryKeyPermissionCheck.ReadSubTree" /> or <see cref="F:Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree" />.</summary>
		// Token: 0x040000E3 RID: 227
		ReadSubTree,
		/// <summary>Security checks are not performed when accessing subkeys or values. A security check is performed when trying to open the current key, unless the parent was opened with <see cref="F:Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree" />.</summary>
		// Token: 0x040000E4 RID: 228
		ReadWriteSubTree
	}
}
