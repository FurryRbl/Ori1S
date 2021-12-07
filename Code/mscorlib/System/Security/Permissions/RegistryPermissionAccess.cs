﻿using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the permitted access to registry keys and values.</summary>
	// Token: 0x02000618 RID: 1560
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum RegistryPermissionAccess
	{
		/// <summary>No access to registry variables. <see cref="F:System.Security.Permissions.RegistryPermissionAccess.NoAccess" /> represents no valid <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values and causes an <see cref="T:System.ArgumentException" /> when used as the parameter for <see cref="M:System.Security.Permissions.RegistryPermission.GetPathList(System.Security.Permissions.RegistryPermissionAccess)" />, which expects a single value.</summary>
		// Token: 0x040019D7 RID: 6615
		NoAccess = 0,
		/// <summary>Read access to registry variables.</summary>
		// Token: 0x040019D8 RID: 6616
		Read = 1,
		/// <summary>Write access to registry variables.</summary>
		// Token: 0x040019D9 RID: 6617
		Write = 2,
		/// <summary>Create access to registry variables.</summary>
		// Token: 0x040019DA RID: 6618
		Create = 4,
		/// <summary>
		///   <see cref="F:System.Security.Permissions.RegistryPermissionAccess.Create" />, <see cref="F:System.Security.Permissions.RegistryPermissionAccess.Read" />, and <see cref="F:System.Security.Permissions.RegistryPermissionAccess.Write" /> access to registry variables. <see cref="F:System.Security.Permissions.RegistryPermissionAccess.AllAccess" /> represents multiple <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values and causes an <see cref="T:System.ArgumentException" /> when used as the <paramref name="access" /> parameter for the <see cref="M:System.Security.Permissions.RegistryPermission.GetPathList(System.Security.Permissions.RegistryPermissionAccess)" /> method, which expects a single value.</summary>
		// Token: 0x040019DB RID: 6619
		AllAccess = 7
	}
}
