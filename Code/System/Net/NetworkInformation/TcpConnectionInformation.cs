using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the Transmission Control Protocol (TCP) connections on the local computer.</summary>
	// Token: 0x020003BB RID: 955
	public abstract class TcpConnectionInformation
	{
		/// <summary>Gets the local endpoint of a Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> instance that contains the IP address and port on the local computer.</returns>
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002125 RID: 8485
		public abstract IPEndPoint LocalEndPoint { get; }

		/// <summary>Gets the remote endpoint of a Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> instance that contains the IP address and port on the remote computer.</returns>
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002126 RID: 8486
		public abstract IPEndPoint RemoteEndPoint { get; }

		/// <summary>Gets the state of this Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.TcpState" /> enumeration values.</returns>
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002127 RID: 8487
		public abstract TcpState State { get; }
	}
}
