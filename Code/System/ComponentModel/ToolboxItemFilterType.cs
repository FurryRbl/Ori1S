using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers used to indicate the type of filter that a <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> uses.</summary>
	// Token: 0x020001AD RID: 429
	public enum ToolboxItemFilterType
	{
		/// <summary>Indicates that a toolbox item filter string is allowed, but not required.</summary>
		// Token: 0x0400043E RID: 1086
		Allow,
		/// <summary>Indicates that custom processing is required to determine whether to use a toolbox item filter string. </summary>
		// Token: 0x0400043F RID: 1087
		Custom,
		/// <summary>Indicates that a toolbox item filter string is not allowed. </summary>
		// Token: 0x04000440 RID: 1088
		Prevent,
		/// <summary>Indicates that a toolbox item filter string must be present for a toolbox item to be enabled. </summary>
		// Token: 0x04000441 RID: 1089
		Require
	}
}
