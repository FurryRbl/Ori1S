using System;

namespace System.Timers
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Timers.Timer.Elapsed" /> event of a <see cref="T:System.Timers.Timer" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.Timers.ElapsedEventArgs" /> object that contains the event data. </param>
	// Token: 0x02000520 RID: 1312
	// (Invoke) Token: 0x06002D2C RID: 11564
	public delegate void ElapsedEventHandler(object sender, ElapsedEventArgs e);
}
