using System;
using System.Runtime.InteropServices;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system semaphore objects.</summary>
	// Token: 0x02000428 RID: 1064
	[ComVisible(false)]
	[Flags]
	public enum SemaphoreRights
	{
		/// <summary>The right to release a named semaphore.</summary>
		// Token: 0x04001798 RID: 6040
		Modify = 2,
		/// <summary>The right to delete a named semaphore.</summary>
		// Token: 0x04001799 RID: 6041
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named semaphore.</summary>
		// Token: 0x0400179A RID: 6042
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named semaphore.</summary>
		// Token: 0x0400179B RID: 6043
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named semaphore.</summary>
		// Token: 0x0400179C RID: 6044
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named semaphore.</summary>
		// Token: 0x0400179D RID: 6045
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named semaphore, and to modify its access rules and audit rules.</summary>
		// Token: 0x0400179E RID: 6046
		FullControl = 2031619
	}
}
