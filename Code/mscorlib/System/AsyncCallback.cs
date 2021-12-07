using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>References a method to be called when a corresponding asynchronous operation completes.</summary>
	/// <param name="ar">The result of the asynchronous operation. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005B RID: 91
	// (Invoke) Token: 0x06000690 RID: 1680
	[ComVisible(true)]
	[Serializable]
	public delegate void AsyncCallback(IAsyncResult ar);
}
