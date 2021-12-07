﻿using System;

namespace System.IO.Ports
{
	/// <summary>Represents the method that will handle the <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event of a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	/// <param name="sender">The sender of the event, which is the <see cref="T:System.IO.Ports.SerialPort" /> object. </param>
	/// <param name="e">A <see cref="T:System.IO.Ports.SerialDataReceivedEventArgs" /> object that contains the event data. </param>
	// Token: 0x02000510 RID: 1296
	// (Invoke) Token: 0x06002CEC RID: 11500
	public delegate void SerialDataReceivedEventHandler(object sender, SerialDataReceivedEventArgs e);
}
