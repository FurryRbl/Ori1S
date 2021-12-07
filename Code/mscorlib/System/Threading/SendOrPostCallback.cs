using System;

namespace System.Threading
{
	/// <summary>Represents a method to be called when a message is to be dispatched to a synchronization context.  </summary>
	/// <param name="state">The object passed to the delegate.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006FB RID: 1787
	// (Invoke) Token: 0x060043E0 RID: 17376
	public delegate void SendOrPostCallback(object state);
}
