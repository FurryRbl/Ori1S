using System;

namespace System.Net.Sockets
{
	/// <summary>Describes states for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
	// Token: 0x02000403 RID: 1027
	[Flags]
	public enum SocketInformationOptions
	{
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is nonblocking.</summary>
		// Token: 0x04001670 RID: 5744
		NonBlocking = 1,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is connected.</summary>
		// Token: 0x04001671 RID: 5745
		Connected = 2,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is listening for new connections.</summary>
		// Token: 0x04001672 RID: 5746
		Listening = 4,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> uses overlapped I/O.</summary>
		// Token: 0x04001673 RID: 5747
		UseOnlyOverlappedIO = 8
	}
}
