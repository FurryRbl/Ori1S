using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies how Access Control Entries (ACEs) are propagated to child objects.  These flags are significant only if inheritance flags are present. </summary>
	// Token: 0x02000589 RID: 1417
	[Flags]
	public enum PropagationFlags
	{
		/// <summary>Specifies that no inheritance flags are set.</summary>
		// Token: 0x04001742 RID: 5954
		None = 0,
		/// <summary>Specifies that the ACE is not propagated to child objects.</summary>
		// Token: 0x04001743 RID: 5955
		NoPropagateInherit = 1,
		/// <summary>Specifies that the ACE is propagated only to child objects. This includes both container and leaf child objects. </summary>
		// Token: 0x04001744 RID: 5956
		InheritOnly = 2
	}
}
