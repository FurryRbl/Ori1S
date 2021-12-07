using System;

namespace System.Net.Sockets
{
	/// <summary>Defines error codes for the <see cref="T:System.Net.Sockets.Socket" /> class.</summary>
	// Token: 0x020003FF RID: 1023
	public enum SocketError
	{
		/// <summary>An attempt was made to access a <see cref="T:System.Net.Sockets.Socket" /> in a way that is forbidden by its access permissions.</summary>
		// Token: 0x04001633 RID: 5683
		AccessDenied = 10013,
		/// <summary>Only one use of an address is normally permitted.</summary>
		// Token: 0x04001634 RID: 5684
		AddressAlreadyInUse = 10048,
		/// <summary>The address family specified is not supported. This error is returned if the IPv6 address family was specified and the IPv6 stack is not installed on the local machine. This error is returned if the IPv4 address family was specified and the IPv4 stack is not installed on the local machine.</summary>
		// Token: 0x04001635 RID: 5685
		AddressFamilyNotSupported = 10047,
		/// <summary>The selected IP address is not valid in this context.</summary>
		// Token: 0x04001636 RID: 5686
		AddressNotAvailable = 10049,
		/// <summary>The nonblocking <see cref="T:System.Net.Sockets.Socket" /> already has an operation in progress.</summary>
		// Token: 0x04001637 RID: 5687
		AlreadyInProgress = 10037,
		/// <summary>The connection was aborted by the .NET Framework or the underlying socket provider.</summary>
		// Token: 0x04001638 RID: 5688
		ConnectionAborted = 10053,
		/// <summary>The remote host is actively refusing a connection.</summary>
		// Token: 0x04001639 RID: 5689
		ConnectionRefused = 10061,
		/// <summary>The connection was reset by the remote peer.</summary>
		// Token: 0x0400163A RID: 5690
		ConnectionReset = 10054,
		/// <summary>A required address was omitted from an operation on a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x0400163B RID: 5691
		DestinationAddressRequired = 10039,
		/// <summary>A graceful shutdown is in progress.</summary>
		// Token: 0x0400163C RID: 5692
		Disconnecting = 10101,
		/// <summary>An invalid pointer address was detected by the underlying socket provider.</summary>
		// Token: 0x0400163D RID: 5693
		Fault = 10014,
		/// <summary>The operation failed because the remote host is down.</summary>
		// Token: 0x0400163E RID: 5694
		HostDown = 10064,
		/// <summary>No such host is known. The name is not an official host name or alias.</summary>
		// Token: 0x0400163F RID: 5695
		HostNotFound = 11001,
		/// <summary>There is no network route to the specified host.</summary>
		// Token: 0x04001640 RID: 5696
		HostUnreachable = 10065,
		/// <summary>A blocking operation is in progress.</summary>
		// Token: 0x04001641 RID: 5697
		InProgress = 10036,
		/// <summary>A blocking <see cref="T:System.Net.Sockets.Socket" /> call was canceled.</summary>
		// Token: 0x04001642 RID: 5698
		Interrupted = 10004,
		/// <summary>An invalid argument was supplied to a <see cref="T:System.Net.Sockets.Socket" /> member.</summary>
		// Token: 0x04001643 RID: 5699
		InvalidArgument = 10022,
		/// <summary>The application has initiated an overlapped operation that cannot be completed immediately.</summary>
		// Token: 0x04001644 RID: 5700
		IOPending = 997,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is already connected.</summary>
		// Token: 0x04001645 RID: 5701
		IsConnected = 10056,
		/// <summary>The datagram is too long.</summary>
		// Token: 0x04001646 RID: 5702
		MessageSize = 10040,
		/// <summary>The network is not available.</summary>
		// Token: 0x04001647 RID: 5703
		NetworkDown = 10050,
		/// <summary>The application tried to set <see cref="F:System.Net.Sockets.SocketOptionName.KeepAlive" /> on a connection that has already timed out.</summary>
		// Token: 0x04001648 RID: 5704
		NetworkReset = 10052,
		/// <summary>No route to the remote host exists.</summary>
		// Token: 0x04001649 RID: 5705
		NetworkUnreachable = 10051,
		/// <summary>No free buffer space is available for a <see cref="T:System.Net.Sockets.Socket" /> operation.</summary>
		// Token: 0x0400164A RID: 5706
		NoBufferSpaceAvailable = 10055,
		/// <summary>The requested name or IP address was not found on the name server.</summary>
		// Token: 0x0400164B RID: 5707
		NoData = 11004,
		/// <summary>The error is unrecoverable or the requested database cannot be located.</summary>
		// Token: 0x0400164C RID: 5708
		NoRecovery = 11003,
		/// <summary>The application tried to send or receive data, and the <see cref="T:System.Net.Sockets.Socket" /> is not connected.</summary>
		// Token: 0x0400164D RID: 5709
		NotConnected = 10057,
		/// <summary>The underlying socket provider has not been initialized.</summary>
		// Token: 0x0400164E RID: 5710
		NotInitialized = 10093,
		/// <summary>A <see cref="T:System.Net.Sockets.Socket" /> operation was attempted on a non-socket.</summary>
		// Token: 0x0400164F RID: 5711
		NotSocket = 10038,
		/// <summary>The overlapped operation was aborted due to the closure of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04001650 RID: 5712
		OperationAborted = 995,
		/// <summary>The address family is not supported by the protocol family.</summary>
		// Token: 0x04001651 RID: 5713
		OperationNotSupported = 10045,
		/// <summary>Too many processes are using the underlying socket provider.</summary>
		// Token: 0x04001652 RID: 5714
		ProcessLimit = 10067,
		/// <summary>The protocol family is not implemented or has not been configured.</summary>
		// Token: 0x04001653 RID: 5715
		ProtocolFamilyNotSupported = 10046,
		/// <summary>The protocol is not implemented or has not been configured.</summary>
		// Token: 0x04001654 RID: 5716
		ProtocolNotSupported = 10043,
		/// <summary>An unknown, invalid, or unsupported option or level was used with a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04001655 RID: 5717
		ProtocolOption = 10042,
		/// <summary>The protocol type is incorrect for this <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04001656 RID: 5718
		ProtocolType = 10041,
		/// <summary>A request to send or receive data was disallowed because the <see cref="T:System.Net.Sockets.Socket" /> has already been closed.</summary>
		// Token: 0x04001657 RID: 5719
		Shutdown = 10058,
		/// <summary>An unspecified <see cref="T:System.Net.Sockets.Socket" /> error has occurred.</summary>
		// Token: 0x04001658 RID: 5720
		SocketError = -1,
		/// <summary>The support for the specified socket type does not exist in this address family.</summary>
		// Token: 0x04001659 RID: 5721
		SocketNotSupported = 10044,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> operation succeeded.</summary>
		// Token: 0x0400165A RID: 5722
		Success = 0,
		/// <summary>The network subsystem is unavailable.</summary>
		// Token: 0x0400165B RID: 5723
		SystemNotReady = 10091,
		/// <summary>The connection attempt timed out, or the connected host has failed to respond.</summary>
		// Token: 0x0400165C RID: 5724
		TimedOut = 10060,
		/// <summary>There are too many open sockets in the underlying socket provider.</summary>
		// Token: 0x0400165D RID: 5725
		TooManyOpenSockets = 10024,
		/// <summary>The name of the host could not be resolved. Try again later.</summary>
		// Token: 0x0400165E RID: 5726
		TryAgain = 11002,
		/// <summary>The specified class was not found.</summary>
		// Token: 0x0400165F RID: 5727
		TypeNotFound = 10109,
		/// <summary>The version of the underlying socket provider is out of range.</summary>
		// Token: 0x04001660 RID: 5728
		VersionNotSupported = 10092,
		/// <summary>An operation on a nonblocking socket cannot be completed immediately.</summary>
		// Token: 0x04001661 RID: 5729
		WouldBlock = 10035
	}
}
