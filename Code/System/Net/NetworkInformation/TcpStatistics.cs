using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Transmission Control Protocol (TCP) statistical data.</summary>
	// Token: 0x020003BE RID: 958
	public abstract class TcpStatistics
	{
		/// <summary>Gets the number of accepted Transmission Control Protocol (TCP) connection requests.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connection requests accepted.</returns>
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x0600212D RID: 8493
		public abstract long ConnectionsAccepted { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) connection requests made by clients.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connections initiated by clients.</returns>
		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600212E RID: 8494
		public abstract long ConnectionsInitiated { get; }

		/// <summary>Specifies the total number of Transmission Control Protocol (TCP) connections established.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of connections established.</returns>
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x0600212F RID: 8495
		public abstract long CumulativeConnections { get; }

		/// <summary>Gets the number of current Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of current TCP connections.</returns>
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002130 RID: 8496
		public abstract long CurrentConnections { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) errors received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP errors received.</returns>
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002131 RID: 8497
		public abstract long ErrorsReceived { get; }

		/// <summary>Gets the number of failed Transmission Control Protocol (TCP) connection attempts.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of failed TCP connection attempts.</returns>
		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002132 RID: 8498
		public abstract long FailedConnectionAttempts { get; }

		/// <summary>Gets the maximum number of supported Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connections that can be supported.</returns>
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002133 RID: 8499
		public abstract long MaximumConnections { get; }

		/// <summary>Gets the maximum retransmission time-out value for Transmission Control Protocol (TCP) segments.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the maximum number of milliseconds permitted by a TCP implementation for the retransmission time-out value.</returns>
		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002134 RID: 8500
		public abstract long MaximumTransmissionTimeout { get; }

		/// <summary>Gets the minimum retransmission time-out value for Transmission Control Protocol (TCP) segments.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the minimum number of milliseconds permitted by a TCP implementation for the retransmission time-out value.</returns>
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002135 RID: 8501
		public abstract long MinimumTransmissionTimeout { get; }

		/// <summary>Gets the number of RST packets received by Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of reset TCP connections.</returns>
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002136 RID: 8502
		public abstract long ResetConnections { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments sent with the reset flag set.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments sent with the reset flag set.</returns>
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002137 RID: 8503
		public abstract long ResetsSent { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments received.</returns>
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002138 RID: 8504
		public abstract long SegmentsReceived { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments re-sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments retransmitted.</returns>
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002139 RID: 8505
		public abstract long SegmentsResent { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments sent.</returns>
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x0600213A RID: 8506
		public abstract long SegmentsSent { get; }
	}
}
