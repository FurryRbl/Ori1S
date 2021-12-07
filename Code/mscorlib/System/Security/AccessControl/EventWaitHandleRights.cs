using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system event objects.</summary>
	// Token: 0x02000572 RID: 1394
	[Flags]
	public enum EventWaitHandleRights
	{
		/// <summary>The right to set or reset the signaled state of a named event.</summary>
		// Token: 0x040016F8 RID: 5880
		Modify = 2,
		/// <summary>The right to delete a named event.</summary>
		// Token: 0x040016F9 RID: 5881
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named event.</summary>
		// Token: 0x040016FA RID: 5882
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named event.</summary>
		// Token: 0x040016FB RID: 5883
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named event.</summary>
		// Token: 0x040016FC RID: 5884
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named event.</summary>
		// Token: 0x040016FD RID: 5885
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named event, and to modify its access rules and audit rules.</summary>
		// Token: 0x040016FE RID: 5886
		FullControl = 2031619
	}
}
