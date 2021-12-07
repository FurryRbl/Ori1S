using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies socket send and receive behaviors.</summary>
	// Token: 0x02000401 RID: 1025
	[Flags]
	public enum SocketFlags
	{
		/// <summary>Use no flags for this call.</summary>
		// Token: 0x04001663 RID: 5731
		None = 0,
		/// <summary>Process out-of-band data.</summary>
		// Token: 0x04001664 RID: 5732
		OutOfBand = 1,
		/// <summary>Peek at the incoming message.</summary>
		// Token: 0x04001665 RID: 5733
		Peek = 2,
		/// <summary>Send without using routing tables.</summary>
		// Token: 0x04001666 RID: 5734
		DontRoute = 4,
		/// <summary>Provides a standard value for the number of WSABUF structures that are used to send and receive data.</summary>
		// Token: 0x04001667 RID: 5735
		MaxIOVectorLength = 16,
		/// <summary>The message was too large to fit into the specified buffer and was truncated.</summary>
		// Token: 0x04001668 RID: 5736
		Truncated = 256,
		/// <summary>Indicates that the control data did not fit into an internal 64-KB buffer and was truncated.</summary>
		// Token: 0x04001669 RID: 5737
		ControlDataTruncated = 512,
		/// <summary>Indicates a broadcast packet.</summary>
		// Token: 0x0400166A RID: 5738
		Broadcast = 1024,
		/// <summary>Indicates a multicast packet.</summary>
		// Token: 0x0400166B RID: 5739
		Multicast = 2048,
		/// <summary>Partial send or receive for message.</summary>
		// Token: 0x0400166C RID: 5740
		Partial = 32768
	}
}
