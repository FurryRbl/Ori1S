using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the conditions for auditing attempts to access a securable object.</summary>
	// Token: 0x0200055D RID: 1373
	[Flags]
	public enum AuditFlags
	{
		/// <summary>No access attempts are to be audited.</summary>
		// Token: 0x040016B4 RID: 5812
		None = 0,
		/// <summary>Successful access attempts are to be audited.</summary>
		// Token: 0x040016B5 RID: 5813
		Success = 1,
		/// <summary>Failed access attempts are to be audited.</summary>
		// Token: 0x040016B6 RID: 5814
		Failure = 2
	}
}
