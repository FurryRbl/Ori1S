using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents a method to be called within a new context.  </summary>
	/// <param name="state">An object containing information to be used by the callback method each time it executes.</param>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006F8 RID: 1784
	// (Invoke) Token: 0x060043D4 RID: 17364
	[ComVisible(true)]
	public delegate void ContextCallback(object state);
}
