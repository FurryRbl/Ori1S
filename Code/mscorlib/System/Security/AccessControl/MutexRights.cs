using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system mutex objects.</summary>
	// Token: 0x02000580 RID: 1408
	[Flags]
	public enum MutexRights
	{
		/// <summary>The right to release a named mutex.</summary>
		// Token: 0x04001729 RID: 5929
		Modify = 1,
		/// <summary>The right to delete a named mutex.</summary>
		// Token: 0x0400172A RID: 5930
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named mutex.</summary>
		// Token: 0x0400172B RID: 5931
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named mutex.</summary>
		// Token: 0x0400172C RID: 5932
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named mutex.</summary>
		// Token: 0x0400172D RID: 5933
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named mutex.</summary>
		// Token: 0x0400172E RID: 5934
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named mutex, and to modify its access rules and audit rules.</summary>
		// Token: 0x0400172F RID: 5935
		FullControl = 2031617
	}
}
