﻿using System;

namespace System.Xml
{
	/// <summary>Specifies the state of the reader.</summary>
	// Token: 0x020000E2 RID: 226
	public enum ReadState
	{
		/// <summary>The Read method has not been called.</summary>
		// Token: 0x04000487 RID: 1159
		Initial,
		/// <summary>The Read method has been called. Additional methods may be called on the reader.</summary>
		// Token: 0x04000488 RID: 1160
		Interactive,
		/// <summary>An error occurred that prevents the read operation from continuing.</summary>
		// Token: 0x04000489 RID: 1161
		Error,
		/// <summary>The end of the file has been reached successfully.</summary>
		// Token: 0x0400048A RID: 1162
		EndOfFile,
		/// <summary>The <see cref="M:System.Xml.XmlReader.Close" /> method has been called.</summary>
		// Token: 0x0400048B RID: 1163
		Closed
	}
}
