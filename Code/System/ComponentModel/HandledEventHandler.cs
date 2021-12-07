using System;

namespace System.ComponentModel
{
	/// <summary>Represents a method that can handle events which may or may not require further processing after the event handler has returned.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.HandledEventArgs" /> that contains the event data.</param>
	// Token: 0x02000503 RID: 1283
	// (Invoke) Token: 0x06002CB8 RID: 11448
	public delegate void HandledEventHandler(object sender, HandledEventArgs e);
}
