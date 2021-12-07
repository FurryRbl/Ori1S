using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers that indicate the type of a refresh of the Properties window.</summary>
	// Token: 0x020001A2 RID: 418
	public enum RefreshProperties
	{
		/// <summary>The properties should be requeried and the view should be refreshed.</summary>
		// Token: 0x0400042B RID: 1067
		All = 1,
		/// <summary>No refresh is necessary.</summary>
		// Token: 0x0400042C RID: 1068
		None = 0,
		/// <summary>The view should be refreshed.</summary>
		// Token: 0x0400042D RID: 1069
		Repaint = 2
	}
}
