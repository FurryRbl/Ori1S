using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents a callback method to be executed by a thread pool thread.</summary>
	/// <param name="state">An object containing information to be used by the callback method. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006FE RID: 1790
	// (Invoke) Token: 0x060043EC RID: 17388
	[ComVisible(true)]
	public delegate void WaitCallback(object state);
}
