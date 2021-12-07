using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents the method that will handle an event that has no event data.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains no event data. </param>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006EC RID: 1772
	// (Invoke) Token: 0x060043A4 RID: 17316
	[ComVisible(true)]
	[Serializable]
	public delegate void EventHandler(object sender, EventArgs e);
}
