using System;

namespace System.Net.Sockets
{
	/// <summary>Defines configuration option names.</summary>
	// Token: 0x02000405 RID: 1029
	public enum SocketOptionName
	{
		/// <summary>Record debugging information.</summary>
		// Token: 0x0400167B RID: 5755
		Debug = 1,
		/// <summary>The socket is listening.</summary>
		// Token: 0x0400167C RID: 5756
		AcceptConnection,
		/// <summary>Allows the socket to be bound to an address that is already in use.</summary>
		// Token: 0x0400167D RID: 5757
		ReuseAddress = 4,
		/// <summary>Use keep-alives.</summary>
		// Token: 0x0400167E RID: 5758
		KeepAlive = 8,
		/// <summary>Do not route; send the packet directly to the interface addresses.</summary>
		// Token: 0x0400167F RID: 5759
		DontRoute = 16,
		/// <summary>Permit sending broadcast messages on the socket.</summary>
		// Token: 0x04001680 RID: 5760
		Broadcast = 32,
		/// <summary>Bypass hardware when possible.</summary>
		// Token: 0x04001681 RID: 5761
		UseLoopback = 64,
		/// <summary>Linger on close if unsent data is present.</summary>
		// Token: 0x04001682 RID: 5762
		Linger = 128,
		/// <summary>Receives out-of-band data in the normal data stream.</summary>
		// Token: 0x04001683 RID: 5763
		OutOfBandInline = 256,
		/// <summary>Close the socket gracefully without lingering.</summary>
		// Token: 0x04001684 RID: 5764
		DontLinger = -129,
		/// <summary>Enables a socket to be bound for exclusive access.</summary>
		// Token: 0x04001685 RID: 5765
		ExclusiveAddressUse = -5,
		/// <summary>Specifies the total per-socket buffer space reserved for sends. This is unrelated to the maximum message size or the size of a TCP window.</summary>
		// Token: 0x04001686 RID: 5766
		SendBuffer = 4097,
		/// <summary>Specifies the total per-socket buffer space reserved for receives. This is unrelated to the maximum message size or the size of a TCP window.</summary>
		// Token: 0x04001687 RID: 5767
		ReceiveBuffer,
		/// <summary>Specifies the low water mark for <see cref="Overload:System.Net.Sockets.Socket.Send" /> operations.</summary>
		// Token: 0x04001688 RID: 5768
		SendLowWater,
		/// <summary>Specifies the low water mark for <see cref="Overload:System.Net.Sockets.Socket.Receive" /> operations.</summary>
		// Token: 0x04001689 RID: 5769
		ReceiveLowWater,
		/// <summary>Send a time-out. This option applies only to synchronous methods; it has no effect on asynchronous methods such as the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</summary>
		// Token: 0x0400168A RID: 5770
		SendTimeout,
		/// <summary>Receive a time-out. This option applies only to synchronous methods; it has no effect on asynchronous methods such as the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</summary>
		// Token: 0x0400168B RID: 5771
		ReceiveTimeout,
		/// <summary>Get the error status and clear.</summary>
		// Token: 0x0400168C RID: 5772
		Error,
		/// <summary>Get the socket type.</summary>
		// Token: 0x0400168D RID: 5773
		Type,
		/// <summary>Not supported; will throw a <see cref="T:System.Net.Sockets.SocketException" /> if used.</summary>
		// Token: 0x0400168E RID: 5774
		MaxConnections = 2147483647,
		/// <summary>Specifies the IP options to be inserted into outgoing datagrams.</summary>
		// Token: 0x0400168F RID: 5775
		IPOptions = 1,
		/// <summary>Indicates that the application provides the IP header for outgoing datagrams.</summary>
		// Token: 0x04001690 RID: 5776
		HeaderIncluded,
		/// <summary>Change the IP header type of the service field.</summary>
		// Token: 0x04001691 RID: 5777
		TypeOfService,
		/// <summary>Set the IP header Time-to-Live field.</summary>
		// Token: 0x04001692 RID: 5778
		IpTimeToLive,
		/// <summary>Set the interface for outgoing multicast packets.</summary>
		// Token: 0x04001693 RID: 5779
		MulticastInterface = 9,
		/// <summary>An IP multicast Time to Live.</summary>
		// Token: 0x04001694 RID: 5780
		MulticastTimeToLive,
		/// <summary>An IP multicast loopback.</summary>
		// Token: 0x04001695 RID: 5781
		MulticastLoopback,
		/// <summary>Add an IP group membership.</summary>
		// Token: 0x04001696 RID: 5782
		AddMembership,
		/// <summary>Drop an IP group membership.</summary>
		// Token: 0x04001697 RID: 5783
		DropMembership,
		/// <summary>Do not fragment IP datagrams.</summary>
		// Token: 0x04001698 RID: 5784
		DontFragment,
		/// <summary>Join a source group.</summary>
		// Token: 0x04001699 RID: 5785
		AddSourceMembership,
		/// <summary>Drop a source group.</summary>
		// Token: 0x0400169A RID: 5786
		DropSourceMembership,
		/// <summary>Block data from a source.</summary>
		// Token: 0x0400169B RID: 5787
		BlockSource,
		/// <summary>Unblock a previously blocked source.</summary>
		// Token: 0x0400169C RID: 5788
		UnblockSource,
		/// <summary>Return information about received packets.</summary>
		// Token: 0x0400169D RID: 5789
		PacketInformation,
		/// <summary>Disables the Nagle algorithm for send coalescing.</summary>
		// Token: 0x0400169E RID: 5790
		NoDelay = 1,
		/// <summary>Use urgent data as defined in RFC-1222. This option can be set only once; after it is set, it cannot be turned off.</summary>
		// Token: 0x0400169F RID: 5791
		BsdUrgent,
		/// <summary>Use expedited data as defined in RFC-1222. This option can be set only once; after it is set, it cannot be turned off.</summary>
		// Token: 0x040016A0 RID: 5792
		Expedited = 2,
		/// <summary>Send UDP datagrams with checksum set to zero.</summary>
		// Token: 0x040016A1 RID: 5793
		NoChecksum = 1,
		/// <summary>Set or get the UDP checksum coverage.</summary>
		// Token: 0x040016A2 RID: 5794
		ChecksumCoverage = 20,
		/// <summary>Specifies the maximum number of router hops for an Internet Protocol version 6 (IPv6) packet. This is similar to Time to Live (TTL) for Internet Protocol version 4.</summary>
		// Token: 0x040016A3 RID: 5795
		HopLimit,
		/// <summary>Updates an accepted socket's properties by using those of an existing socket. This is equivalent to using the Winsock2 SO_UPDATE_ACCEPT_CONTEXT socket option and is supported only on connection-oriented sockets.</summary>
		// Token: 0x040016A4 RID: 5796
		UpdateAcceptContext = 28683,
		/// <summary>Updates a connected socket's properties by using those of an existing socket. This is equivalent to using the Winsock2 SO_UPDATE_CONNECT_CONTEXT socket option and is supported only on connection-oriented sockets.</summary>
		// Token: 0x040016A5 RID: 5797
		UpdateConnectContext = 28688
	}
}
