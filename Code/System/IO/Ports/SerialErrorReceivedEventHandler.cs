using System;

namespace System.IO.Ports
{
	/// <summary>Represents the method that will handle the <see cref="E:System.IO.Ports.SerialPort.ErrorReceived" /> event of a <see cref="T:System.IO.Ports.SerialPort" /> object. </summary>
	/// <param name="sender">The sender of the event, which is the <see cref="T:System.IO.Ports.SerialPort" /> object. </param>
	/// <param name="e">A <see cref="T:System.IO.Ports.SerialErrorReceivedEventArgs" /> object that contains the event data. </param>
	// Token: 0x02000512 RID: 1298
	// (Invoke) Token: 0x06002CF4 RID: 11508
	public delegate void SerialErrorReceivedEventHandler(object sender, SerialErrorReceivedEventArgs e);
}
