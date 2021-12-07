using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides User Datagram Protocol (UDP) statistical data.</summary>
	// Token: 0x020003C2 RID: 962
	public abstract class UdpStatistics
	{
		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of datagrams that were delivered to UDP users.</returns>
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600215B RID: 8539
		public abstract long DatagramsReceived { get; }

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of datagrams that were sent.</returns>
		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x0600215C RID: 8540
		public abstract long DatagramsSent { get; }

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received and discarded because of port errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of received UDP datagrams that were discarded because there was no listening application at the destination port.</returns>
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600215D RID: 8541
		public abstract long IncomingDatagramsDiscarded { get; }

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received and discarded because of errors other than bad port information.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of received UDP datagrams that could not be delivered for reasons other than the lack of an application at the destination port.</returns>
		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x0600215E RID: 8542
		public abstract long IncomingDatagramsWithErrors { get; }

		/// <summary>Gets the number of local endpoints that are listening for User Datagram Protocol (UDP) datagrams.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of sockets that are listening for UDP datagrams.</returns>
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x0600215F RID: 8543
		public abstract int UdpListeners { get; }
	}
}
