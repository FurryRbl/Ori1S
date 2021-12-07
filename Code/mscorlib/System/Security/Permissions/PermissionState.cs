using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies whether a permission should have all or no access to resources at creation.</summary>
	// Token: 0x0200060E RID: 1550
	[ComVisible(true)]
	[Serializable]
	public enum PermissionState
	{
		/// <summary>Full access to the resource protected by the permission.</summary>
		// Token: 0x040019B5 RID: 6581
		Unrestricted = 1,
		/// <summary>No access to the resource protected by the permission.</summary>
		// Token: 0x040019B6 RID: 6582
		None = 0
	}
}
