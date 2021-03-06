using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the IO control codes supported by the <see cref="M:System.Net.Sockets.Socket.IOControl(System.Int32,System.Byte[],System.Byte[])" /> method.</summary>
	// Token: 0x020003EC RID: 1004
	public enum IOControlCode : long
	{
		/// <summary>This value is equal to the Winsock 2 SIO_ABSORB_RTRALERT constant.</summary>
		// Token: 0x04001559 RID: 5465
		AbsorbRouterAlert = 2550136837L,
		/// <summary>Join a multicast group using an interface identified by its index. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_INDEX_ADD_MCAST constant.</summary>
		// Token: 0x0400155A RID: 5466
		AddMulticastGroupOnInterface = 2550136842L,
		/// <summary>Enable receiving notification when the list of local interfaces for the socket's protocol family changes. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_ADDRESS_LIST_CHANGE constant.</summary>
		// Token: 0x0400155B RID: 5467
		AddressListChange = 671088663L,
		/// <summary>Return the list of local interfaces that the socket can bind to. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_ADDRESS_LIST_QUERY constant.</summary>
		// Token: 0x0400155C RID: 5468
		AddressListQuery = 1207959574L,
		/// <summary>Sort the structure returned by the <see cref="F:System.Net.Sockets.IOControlCode.AddressListQuery" /> field and add scope ID information for IPv6 addresses. This control code is supported on Windows XP and later operating systems. This value is equal to the Winsock 2 SIO_ADDRESS_LIST_SORT constant.</summary>
		// Token: 0x0400155D RID: 5469
		AddressListSort = 3355443225L,
		/// <summary>Associate this socket with the specified handle of a companion interface. Refer to the appropriate  protocol-specific annex in the Winsock 2 reference or documentation for the particular companion interface for additional details. It is recommended that the Component Object Model (COM) be used instead of this IOCTL to discover and track other interfaces that might be supported by a socket. This control code is present for backward compatibility with systems where COM is not available or cannot be used for some other reason. This value is equal to the Winsock 2 SIO_ASSOCIATE_HANDLE constant. </summary>
		// Token: 0x0400155E RID: 5470
		AssociateHandle = 2281701377L,
		/// <summary>Enable notification for when data is waiting to be received. This value is equal to the Winsock 2 FIOASYNC constant.</summary>
		// Token: 0x0400155F RID: 5471
		AsyncIO = 2147772029L,
		/// <summary>Bind the socket to a specified interface index. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_INDEX_BIND constant.</summary>
		// Token: 0x04001560 RID: 5472
		BindToInterface = 2550136840L,
		/// <summary>Return the number of bytes available for reading. This value is equal to the Winsock 2 FIONREAD constant.</summary>
		// Token: 0x04001561 RID: 5473
		DataToRead = 1074030207L,
		/// <summary>Remove the socket from a multicast group. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_INDEX_ADD_MCAST constant.</summary>
		// Token: 0x04001562 RID: 5474
		DeleteMulticastGroupFromInterface = 2550136843L,
		/// <summary>Replace the oldest queued datagram with an incoming datagram when the incoming message queues are full. This value is equal to the Winsock 2 SIO_ENABLE_CIRCULAR_QUEUEING constant.</summary>
		// Token: 0x04001563 RID: 5475
		EnableCircularQueuing = 671088642L,
		/// <summary>Discard the contents of the sending queue. This value is equal to the Winsock 2 SIO_FLUSH constant.</summary>
		// Token: 0x04001564 RID: 5476
		Flush = 671088644L,
		/// <summary>Return a SOCKADDR structure that contains the broadcast address for the address family of the current socket. The returned address can be used with the <see cref="Overload:System.Net.Sockets.Socket.SendTo" /> method. This value is equal to the Winsock 2 SIO_GET_BROADCAST_ADDRESS constant. This value can be used on User Datagram Protocol (UDP) sockets only.</summary>
		// Token: 0x04001565 RID: 5477
		GetBroadcastAddress = 1207959557L,
		/// <summary>Obtain provider-specific functions that are not part of the Winsock specification. Functions are specified using their provider-assigned GUID. This value is equal to the Winsock 2 SIO_GET_EXTENSION_FUNCTION_POINTER constant.</summary>
		// Token: 0x04001566 RID: 5478
		GetExtensionFunctionPointer = 3355443206L,
		/// <summary>Return the Quality of Service (QOS) attributes for the socket group. This value is reserved for future use, and is equal to the Winsock 2 SIO_GET_GROUP_QOS constant. </summary>
		// Token: 0x04001567 RID: 5479
		GetGroupQos = 3355443208L,
		/// <summary>Retrieve the QOS structure associated with the socket. This control is only supported on platforms that provide a QOS capable transport (Windows Me, Windows 2000, and later.) This value is equal to the Winsock 2 SIO_GET_QOS constant.</summary>
		// Token: 0x04001568 RID: 5480
		GetQos = 3355443207L,
		/// <summary>Control sending TCP keep-alive packets and the interval at which they are sent. This control code is supported on Windows 2000 and later operating systems. For additional information, see RFC 1122 section 4.2.3.6. This value is equal to the Winsock 2 SIO_KEEPALIVE_VALS constant.</summary>
		// Token: 0x04001569 RID: 5481
		KeepAliveValues = 2550136836L,
		/// <summary>This value is equal to the Winsock 2 SIO_LIMIT_BROADCASTS constant.</summary>
		// Token: 0x0400156A RID: 5482
		LimitBroadcasts = 2550136839L,
		/// <summary>Set the interface used for outgoing multicast packets. The interface is identified by its index. This control code is supported on Windows 2000 and later operating systems.  This value is equal to the Winsock 2 SIO_INDEX_MCASTIF constant.</summary>
		// Token: 0x0400156B RID: 5483
		MulticastInterface = 2550136841L,
		/// <summary>Control the number of times a multicast packet can be forwarded by a router, also known as the Time to Live (TTL), or hop count. This value is equal to the Winsock 2 SIO_MULTICAST_SCOPE constant.</summary>
		// Token: 0x0400156C RID: 5484
		MulticastScope = 2281701386L,
		/// <summary>Control whether multicast data sent by the socket appears as incoming data in the sockets receive queue. This value is equal to the Winsock 2 SIO_MULTIPOINT_LOOPBACK constant.</summary>
		// Token: 0x0400156D RID: 5485
		MultipointLoopback = 2281701385L,
		/// <summary>Control whether the socket receives notification when a namespace query becomes invalid. This control code is supported on Windows XP and later operating systems. This value is equal to the Winsock 2 SIO_NSP_NOTIFY_CHANGE constant.</summary>
		// Token: 0x0400156E RID: 5486
		NamespaceChange = 2281701401L,
		/// <summary>Control the blocking behavior of the socket. If the argument specified with this control code is zero, the socket is placed in blocking mode. If the argument is nonzero, the socket is placed in nonblocking mode. This value is equal to the Winsock 2 FIONBIO constant.</summary>
		// Token: 0x0400156F RID: 5487
		NonBlockingIO = 2147772030L,
		/// <summary>Return information about out-of-band data waiting to be received. When using this control code on stream sockets, the return value indicates the number of bytes available.</summary>
		// Token: 0x04001570 RID: 5488
		OobDataRead = 1074033415L,
		/// <summary>Retrieve the underlying provider's SOCKET handle. This handle can be used to receive plug-and-play event notification. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_QUERY_TARGET_PNP_HANDLE constant.</summary>
		// Token: 0x04001571 RID: 5489
		QueryTargetPnpHandle = 1207959576L,
		/// <summary>Enable receiving all IPv4 packets on the network. The socket must have address family <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" />, the socket type must be <see cref="F:System.Net.Sockets.SocketType.Raw" />, and the protocol type must be <see cref="F:System.Net.Sockets.ProtocolType.IP" />. The current user must belong to the Administrators group on the local computer, and the socket must be bound to a specific port. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_RCVALL constant.</summary>
		// Token: 0x04001572 RID: 5490
		ReceiveAll = 2550136833L,
		/// <summary>Enable receiving all Internet Group Management Protocol (IGMP) packets on the network. The socket must have address family <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" />, the socket type must be <see cref="F:System.Net.Sockets.SocketType.Raw" />, and the protocol type must be <see cref="F:System.Net.Sockets.ProtocolType.Igmp" />. The current user must belong to the Administrators group on the local computer, and the socket must be bound to a specific port. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_RCVALL_IGMPMCAST constant.</summary>
		// Token: 0x04001573 RID: 5491
		ReceiveAllIgmpMulticast = 2550136835L,
		/// <summary>Enable receiving all multicast IPv4 packets on the network. These are packets with destination addresses in the range 224.0.0.0 through 239.255.255.255. The socket must have address family <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" />, the socket type must be <see cref="F:System.Net.Sockets.SocketType.Raw" />, and the protocol type must be <see cref="F:System.Net.Sockets.ProtocolType.Udp" />. The current user must belong to the Administrators group on the local computer, and the socket must be bound to a specific port. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_RCVALL_MCAST constant.</summary>
		// Token: 0x04001574 RID: 5492
		ReceiveAllMulticast = 2550136834L,
		/// <summary>Enable receiving notification when the local interface used to access a remote endpoint changes. This value is equal to the Winsock 2 SIO_ROUTING_INTERFACE_CHANGE constant.</summary>
		// Token: 0x04001575 RID: 5493
		RoutingInterfaceChange = 2281701397L,
		/// <summary>Return the interface addresses that can be used to connect to the specified remote address. This value is equal to the Winsock 2 SIO_ROUTING_INTERFACE_QUERY constant.</summary>
		// Token: 0x04001576 RID: 5494
		RoutingInterfaceQuery = 3355443220L,
		/// <summary>Set the Quality of Service (QOS) attributes for the socket group. This value is reserved for future use and is equal to the Winsock 2 SIO_SET_GROUP_QOS constant.</summary>
		// Token: 0x04001577 RID: 5495
		SetGroupQos = 2281701388L,
		/// <summary>Set the Quality of Service (QOS) attributes for the socket. QOS defines the bandwidth requirements for the socket. This control code is supported on Windows Me, Windows 2000, and later operating systems. This value is equal to the Winsock 2 SIO_SET_QOS constant.</summary>
		// Token: 0x04001578 RID: 5496
		SetQos = 2281701387L,
		/// <summary>Return a handle for the socket that is valid in the context of a companion interface. This value is equal to the Winsock 2 SIO_TRANSLATE_HANDLE constant.</summary>
		// Token: 0x04001579 RID: 5497
		TranslateHandle = 3355443213L,
		/// <summary>Set the interface used for outgoing unicast packets. This value is equal to the Winsock 2 SIO_UCAST_IF constant.</summary>
		// Token: 0x0400157A RID: 5498
		UnicastInterface = 2550136838L
	}
}
