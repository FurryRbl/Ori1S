using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the number of stop bits used on the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x020002A1 RID: 673
	public enum StopBits
	{
		/// <summary>No stop bits are used. This value is not supported. Setting the <see cref="P:System.IO.Ports.SerialPort.StopBits" /> property to <see cref="F:System.IO.Ports.StopBits.None" /> raises an <see cref="T:System.ArgumentOutOfRangeException" />.</summary>
		// Token: 0x04000ECC RID: 3788
		None,
		/// <summary>One stop bit is used.</summary>
		// Token: 0x04000ECD RID: 3789
		One,
		/// <summary>Two stop bits are used.</summary>
		// Token: 0x04000ECE RID: 3790
		Two,
		/// <summary>1.5 stop bits are used.</summary>
		// Token: 0x04000ECF RID: 3791
		OnePointFive
	}
}
