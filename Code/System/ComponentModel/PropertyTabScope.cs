using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers that indicate the persistence scope of a tab in the Properties window.</summary>
	// Token: 0x02000199 RID: 409
	public enum PropertyTabScope
	{
		/// <summary>This tab is added to the Properties window and cannot be removed.</summary>
		// Token: 0x0400040A RID: 1034
		Static,
		/// <summary>This tab is added to the Properties window and can only be removed explicitly by a parent component.</summary>
		// Token: 0x0400040B RID: 1035
		Global,
		/// <summary>This tab is specific to the current document. This tab is added to the Properties window and is removed when the currently selected document changes.</summary>
		// Token: 0x0400040C RID: 1036
		Document,
		/// <summary>This tab is specific to the current component. This tab is added to the Properties window for the current component only and is removed when the component is no longer selected.</summary>
		// Token: 0x0400040D RID: 1037
		Component
	}
}
