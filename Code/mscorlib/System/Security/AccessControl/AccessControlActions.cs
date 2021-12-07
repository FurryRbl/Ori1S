using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the actions that are permitted for securable objects.</summary>
	// Token: 0x02000554 RID: 1364
	[Flags]
	public enum AccessControlActions
	{
		/// <summary>Specifies no access.</summary>
		// Token: 0x04001679 RID: 5753
		None = 0,
		/// <summary>Specifies read-only access.</summary>
		// Token: 0x0400167A RID: 5754
		View = 1,
		/// <summary>Specifies write-only access.</summary>
		// Token: 0x0400167B RID: 5755
		Change = 2
	}
}
