using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies which sections of a security descriptor to save or load.</summary>
	// Token: 0x02000556 RID: 1366
	[Flags]
	public enum AccessControlSections
	{
		/// <summary>No sections.</summary>
		// Token: 0x04001684 RID: 5764
		None = 0,
		/// <summary>The system access control list (SACL).</summary>
		// Token: 0x04001685 RID: 5765
		Audit = 1,
		/// <summary>The discretionary access control list (DACL).</summary>
		// Token: 0x04001686 RID: 5766
		Access = 2,
		/// <summary>The owner.</summary>
		// Token: 0x04001687 RID: 5767
		Owner = 4,
		/// <summary>The primary group.</summary>
		// Token: 0x04001688 RID: 5768
		Group = 8,
		/// <summary>The entire security descriptor.</summary>
		// Token: 0x04001689 RID: 5769
		All = 15
	}
}
