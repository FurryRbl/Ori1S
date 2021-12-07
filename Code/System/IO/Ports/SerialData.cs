using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the type of character that was received on the serial port of the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x02000298 RID: 664
	public enum SerialData
	{
		/// <summary>A character was received and placed in the input buffer.</summary>
		// Token: 0x04000E94 RID: 3732
		Chars = 1,
		/// <summary>The end of file character was received and placed in the input buffer.</summary>
		// Token: 0x04000E95 RID: 3733
		Eof
	}
}
