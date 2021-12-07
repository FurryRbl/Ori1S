using System;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the MethodNameCompleted event of an asynchronous operation.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> that contains the event data. </param>
	// Token: 0x020004F6 RID: 1270
	// (Invoke) Token: 0x06002C84 RID: 11396
	public delegate void AsyncCompletedEventHandler(object sender, AsyncCompletedEventArgs e);
}
