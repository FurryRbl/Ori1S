﻿using System;

namespace System.IO.Ports
{
	/// <summary>Specifies errors that occur on the <see cref="T:System.IO.Ports.SerialPort" /> object</summary>
	// Token: 0x02000299 RID: 665
	public enum SerialError
	{
		/// <summary>An input buffer overflow has occurred. There is either no room in the input buffer, or a character was received after the end-of-file (EOF) character.</summary>
		// Token: 0x04000E97 RID: 3735
		RXOver = 1,
		/// <summary>A character-buffer overrun has occurred. The next character is lost.</summary>
		// Token: 0x04000E98 RID: 3736
		Overrun,
		/// <summary>The hardware detected a parity error.</summary>
		// Token: 0x04000E99 RID: 3737
		RXParity = 4,
		/// <summary>The hardware detected a framing error.</summary>
		// Token: 0x04000E9A RID: 3738
		Frame = 8,
		/// <summary>The application tried to transmit a character, but the output buffer was full.</summary>
		// Token: 0x04000E9B RID: 3739
		TXFull = 256
	}
}
