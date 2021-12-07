using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Provides display instructions for the debugger.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DD RID: 477
	[ComVisible(true)]
	public enum DebuggerBrowsableState
	{
		/// <summary>Never show the element.</summary>
		// Token: 0x040008E0 RID: 2272
		Never,
		/// <summary>Show the element as collapsed.</summary>
		// Token: 0x040008E1 RID: 2273
		Collapsed = 2,
		/// <summary>Do not display the root element; display the child elements if the element is a collection or array of items.</summary>
		// Token: 0x040008E2 RID: 2274
		RootHidden
	}
}
