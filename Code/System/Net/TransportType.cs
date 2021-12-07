using System;

namespace System.Net
{
	/// <summary>Defines transport types for the <see cref="T:System.Net.SocketPermission" /> and <see cref="T:System.Net.Sockets.Socket" /> classes.</summary>
	// Token: 0x0200040E RID: 1038
	public enum TransportType
	{
		/// <summary>UDP transport.</summary>
		// Token: 0x040016D3 RID: 5843
		Udp = 1,
		/// <summary>The transport type is connectionless, such as UDP. Specifying this value has the same effect as specifying <see cref="F:System.Net.TransportType.Udp" />.</summary>
		// Token: 0x040016D4 RID: 5844
		Connectionless = 1,
		/// <summary>TCP transport.</summary>
		// Token: 0x040016D5 RID: 5845
		Tcp,
		/// <summary>The transport is connection oriented, such as TCP. Specifying this value has the same effect as specifying <see cref="F:System.Net.TransportType.Tcp" />.</summary>
		// Token: 0x040016D6 RID: 5846
		ConnectionOriented = 2,
		/// <summary>All transport types.</summary>
		// Token: 0x040016D7 RID: 5847
		All
	}
}
