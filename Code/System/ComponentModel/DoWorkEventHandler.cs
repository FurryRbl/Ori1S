using System;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event. This class cannot be inherited.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.DoWorkEventArgs" />    that contains the event data.</param>
	// Token: 0x02000502 RID: 1282
	// (Invoke) Token: 0x06002CB4 RID: 11444
	public delegate void DoWorkEventHandler(object sender, DoWorkEventArgs e);
}
