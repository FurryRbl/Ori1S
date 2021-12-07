using System;

namespace Microsoft.Win32
{
	/// <summary>Represents the method that will handle the <see cref="E:Microsoft.Win32.SystemEvents.SessionEnded" /> event.</summary>
	/// <param name="sender">The source of the event. When this event is raised by the <see cref="T:Microsoft.Win32.SystemEvents" /> class, this object is always null. </param>
	/// <param name="e">A <see cref="T:Microsoft.Win32.SessionEndedEventArgs" /> that contains the event data. </param>
	// Token: 0x020004EF RID: 1263
	// (Invoke) Token: 0x06002C68 RID: 11368
	public delegate void SessionEndedEventHandler(object sender, SessionEndedEventArgs e);
}
