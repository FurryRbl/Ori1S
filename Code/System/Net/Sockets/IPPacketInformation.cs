using System;

namespace System.Net.Sockets
{
	/// <summary>Presents the packet information from a call to <see cref="M:System.Net.Sockets.Socket.ReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> or <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" />.</summary>
	// Token: 0x020003EE RID: 1006
	public struct IPPacketInformation
	{
		// Token: 0x060022C5 RID: 8901 RVA: 0x000660F8 File Offset: 0x000642F8
		internal IPPacketInformation(IPAddress address, int iface)
		{
			this.address = address;
			this.iface = iface;
		}

		/// <summary>Gets the origin information of the packet that was received as a result of calling the <see cref="M:System.Net.Sockets.Socket.ReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> method or <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> method.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that indicates the origin information of the packet that was received as a result of calling the <see cref="M:System.Net.Sockets.Socket.ReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> method or <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> method. For packets that were sent from a unicast address, the <see cref="P:System.Net.Sockets.IPPacketInformation.Address" /> property will return the <see cref="T:System.Net.IPAddress" /> of the sender; for multicast or broadcast packets, the <see cref="P:System.Net.Sockets.IPPacketInformation.Address" /> property will return the multicast or broadcast <see cref="T:System.Net.IPAddress" />.</returns>
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x060022C6 RID: 8902 RVA: 0x00066108 File Offset: 0x00064308
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		/// <summary>Gets the network interface information that is associated with a call to <see cref="M:System.Net.Sockets.Socket.ReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> or <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value, which represents the index of the network interface. You can use this index with <see cref="M:System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces" /> to get more information about the relevant interface.</returns>
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x00066110 File Offset: 0x00064310
		public int Interface
		{
			get
			{
				return this.iface;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="comparand" /> is an instance of <see cref="T:System.Net.Sockets.IPPacketInformation" /> and equals the value of the instance; otherwise, false.</returns>
		/// <param name="comparand">The object to compare with this instance.</param>
		// Token: 0x060022C8 RID: 8904 RVA: 0x00066118 File Offset: 0x00064318
		public override bool Equals(object comparand)
		{
			if (!(comparand is IPPacketInformation))
			{
				return false;
			}
			IPPacketInformation ippacketInformation = (IPPacketInformation)comparand;
			return ippacketInformation.iface == this.iface && ippacketInformation.address.Equals(this.address);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>An Int32 hash code.</returns>
		// Token: 0x060022C9 RID: 8905 RVA: 0x00066160 File Offset: 0x00064360
		public override int GetHashCode()
		{
			return this.address.GetHashCode() + this.iface;
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.IPPacketInformation" /> instances are equivalent.</summary>
		/// <returns>true if <paramref name="packetInformation1" /> and <paramref name="packetInformation2" /> are equal; otherwise, false.</returns>
		/// <param name="packetInformation1">The <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that is to the left of the equality operator.</param>
		/// <param name="packetInformation2">The <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that is to the right of the equality operator.</param>
		// Token: 0x060022CA RID: 8906 RVA: 0x00066174 File Offset: 0x00064374
		public static bool operator ==(IPPacketInformation p1, IPPacketInformation p2)
		{
			return p1.Equals(p2);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.IPPacketInformation" /> instances are not equal.</summary>
		/// <returns>true if <paramref name="packetInformation1" /> and <paramref name="packetInformation2" /> are unequal; otherwise, false.</returns>
		/// <param name="packetInformation1">The <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that is to the left of the inequality operator.</param>
		/// <param name="packetInformation2">The <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that is to the right of the inequality operator.</param>
		// Token: 0x060022CB RID: 8907 RVA: 0x00066184 File Offset: 0x00064384
		public static bool operator !=(IPPacketInformation p1, IPPacketInformation p2)
		{
			return !p1.Equals(p2);
		}

		// Token: 0x0400157D RID: 5501
		private IPAddress address;

		// Token: 0x0400157E RID: 5502
		private int iface;
	}
}
