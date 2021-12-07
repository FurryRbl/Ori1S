using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the function of an access control entry (ACE).</summary>
	// Token: 0x0200055B RID: 1371
	public enum AceQualifier
	{
		/// <summary>Allow access.</summary>
		// Token: 0x0400169C RID: 5788
		AccessAllowed,
		/// <summary>Deny access.</summary>
		// Token: 0x0400169D RID: 5789
		AccessDenied,
		/// <summary>Cause a system audit.</summary>
		// Token: 0x0400169E RID: 5790
		SystemAudit,
		/// <summary>Cause a system alarm.</summary>
		// Token: 0x0400169F RID: 5791
		SystemAlarm
	}
}
