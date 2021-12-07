using System;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event of the <see cref="T:System.ComponentModel.BackgroundWorker" /> class. This class cannot be inherited.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.ComponentModel.ProgressChangedEventArgs" />   that contains the event data. </param>
	// Token: 0x02000521 RID: 1313
	// (Invoke) Token: 0x06002D30 RID: 11568
	public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);
}
