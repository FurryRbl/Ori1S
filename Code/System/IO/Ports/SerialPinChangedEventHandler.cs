﻿using System;

namespace System.IO.Ports
{
	/// <summary>Represents the method that will handle the <see cref="E:System.IO.Ports.SerialPort.PinChanged" /> event of a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	/// <param name="sender">The source of the event, which is the <see cref="T:System.IO.Ports.SerialPort" /> object. </param>
	/// <param name="e">A <see cref="T:System.IO.Ports.SerialPinChangedEventArgs" /> object that contains the event data. </param>
	// Token: 0x02000511 RID: 1297
	// (Invoke) Token: 0x06002CF0 RID: 11504
	public delegate void SerialPinChangedEventHandler(object sender, SerialPinChangedEventArgs e);
}
