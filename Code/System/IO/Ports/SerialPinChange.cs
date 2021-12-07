using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the type of change that occurred on the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x0200029B RID: 667
	public enum SerialPinChange
	{
		/// <summary>The Clear to Send (CTS) signal changed state. This signal is used to indicate whether data can be sent over the serial port.</summary>
		// Token: 0x04000E9E RID: 3742
		CtsChanged = 8,
		/// <summary>The Data Set Ready (DSR) signal changed state. This signal is used to indicate whether the device on the serial port is ready to operate.</summary>
		// Token: 0x04000E9F RID: 3743
		DsrChanged = 16,
		/// <summary>The Carrier Detect (CD) signal changed state. This signal is used to indicate whether a modem is connected to a working phone line and a data carrier signal is detected.</summary>
		// Token: 0x04000EA0 RID: 3744
		CDChanged = 32,
		/// <summary>A break was detected on input.</summary>
		// Token: 0x04000EA1 RID: 3745
		Break = 64,
		/// <summary>A ring indicator was detected.</summary>
		// Token: 0x04000EA2 RID: 3746
		Ring = 256
	}
}
