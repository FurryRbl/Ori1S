using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event of a <see cref="T:System.Net.NetworkInformation.Ping" /> object.</summary>
	/// <param name="sender">The source of the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event.</param>
	/// <param name="e">A <see cref="T:System.Net.NetworkInformation.PingCompletedEventArgs" />  object that contains the event data.</param>
	// Token: 0x0200051A RID: 1306
	// (Invoke) Token: 0x06002D14 RID: 11540
	public delegate void PingCompletedEventHandler(object sender, PingCompletedEventArgs e);
}
