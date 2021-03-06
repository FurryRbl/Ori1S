using System;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Represents a network endpoint as an IP address and a port number.</summary>
	// Token: 0x0200032B RID: 811
	[Serializable]
	public class IPEndPoint : EndPoint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPEndPoint" /> class with the specified address and port number.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" />. </param>
		/// <param name="port">The port number associated with the <paramref name="address" />, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.-or- <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.-or- <paramref name="address" /> is less than 0 or greater than 0x00000000FFFFFFFF. </exception>
		// Token: 0x06001CC5 RID: 7365 RVA: 0x00054E94 File Offset: 0x00053094
		public IPEndPoint(IPAddress address, int port)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.Address = address;
			this.Port = port;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPEndPoint" /> class with the specified address and port number.</summary>
		/// <param name="address">The IP address of the Internet host. </param>
		/// <param name="port">The port number associated with the <paramref name="address" />, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.-or- <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.-or- <paramref name="address" /> is less than 0 or greater than 0x00000000FFFFFFFF. </exception>
		// Token: 0x06001CC6 RID: 7366 RVA: 0x00054EBC File Offset: 0x000530BC
		public IPEndPoint(long iaddr, int port)
		{
			this.Address = new IPAddress(iaddr);
			this.Port = port;
		}

		/// <summary>Gets or sets the IP address of the endpoint.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance containing the IP address of the endpoint.</returns>
		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x00054ED8 File Offset: 0x000530D8
		// (set) Token: 0x06001CC8 RID: 7368 RVA: 0x00054EE0 File Offset: 0x000530E0
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
			}
		}

		/// <summary>Gets the Internet Protocol (IP) address family.</summary>
		/// <returns>Returns <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" />.</returns>
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x00054EEC File Offset: 0x000530EC
		public override System.Net.Sockets.AddressFamily AddressFamily
		{
			get
			{
				return this.address.AddressFamily;
			}
		}

		/// <summary>Gets or sets the port number of the endpoint.</summary>
		/// <returns>An integer value in the range <see cref="F:System.Net.IPEndPoint.MinPort" /> to <see cref="F:System.Net.IPEndPoint.MaxPort" /> indicating the port number of the endpoint.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value that was specified for a set operation is less than <see cref="F:System.Net.IPEndPoint.MinPort" /> or greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x00054EFC File Offset: 0x000530FC
		// (set) Token: 0x06001CCB RID: 7371 RVA: 0x00054F04 File Offset: 0x00053104
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new ArgumentOutOfRangeException("Invalid port");
				}
				this.port = value;
			}
		}

		/// <summary>Creates an endpoint from a socket address.</summary>
		/// <returns>An <see cref="T:System.Net.EndPoint" /> instance using the specified socket address.</returns>
		/// <param name="socketAddress">The <see cref="T:System.Net.SocketAddress" /> to use for the endpoint. </param>
		/// <exception cref="T:System.ArgumentException">The AddressFamily of <paramref name="socketAddress" /> is not equal to the AddressFamily of the current instance.-or- <paramref name="socketAddress" />.Size &lt; 8. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001CCC RID: 7372 RVA: 0x00054F38 File Offset: 0x00053138
		public override EndPoint Create(SocketAddress socketAddress)
		{
			if (socketAddress == null)
			{
				throw new ArgumentNullException("socketAddress");
			}
			if (socketAddress.Family != this.AddressFamily)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"The IPEndPoint was created using ",
					this.AddressFamily,
					" AddressFamily but SocketAddress contains ",
					socketAddress.Family,
					" instead, please use the same type."
				}));
			}
			int size = socketAddress.Size;
			System.Net.Sockets.AddressFamily family = socketAddress.Family;
			System.Net.Sockets.AddressFamily addressFamily = family;
			IPEndPoint result;
			if (addressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
			{
				if (addressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6)
				{
					return null;
				}
				if (size < 28)
				{
					return null;
				}
				int num = ((int)socketAddress[2] << 8) + (int)socketAddress[3];
				int num2 = (int)socketAddress[24] + ((int)socketAddress[25] << 8) + ((int)socketAddress[26] << 16) + ((int)socketAddress[27] << 24);
				ushort[] array = new ushort[8];
				for (int i = 0; i < 8; i++)
				{
					array[i] = (ushort)(((int)socketAddress[8 + i * 2] << 8) + (int)socketAddress[8 + i * 2 + 1]);
				}
				result = new IPEndPoint(new IPAddress(array, (long)num2), num);
			}
			else
			{
				if (size < 8)
				{
					return null;
				}
				int num = ((int)socketAddress[2] << 8) + (int)socketAddress[3];
				long iaddr = ((long)socketAddress[7] << 24) + ((long)socketAddress[6] << 16) + ((long)socketAddress[5] << 8) + (long)socketAddress[4];
				result = new IPEndPoint(iaddr, num);
			}
			return result;
		}

		/// <summary>Serializes endpoint information into a <see cref="T:System.Net.SocketAddress" /> instance.</summary>
		/// <returns>A <see cref="T:System.Net.SocketAddress" /> instance containing the socket address for the endpoint.</returns>
		// Token: 0x06001CCD RID: 7373 RVA: 0x000550D4 File Offset: 0x000532D4
		public override SocketAddress Serialize()
		{
			SocketAddress socketAddress = null;
			System.Net.Sockets.AddressFamily addressFamily = this.address.AddressFamily;
			if (addressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
			{
				if (addressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
				{
					socketAddress = new SocketAddress(System.Net.Sockets.AddressFamily.InterNetworkV6, 28);
					socketAddress[2] = (byte)(this.port >> 8 & 255);
					socketAddress[3] = (byte)(this.port & 255);
					byte[] addressBytes = this.address.GetAddressBytes();
					for (int i = 0; i < 16; i++)
					{
						socketAddress[8 + i] = addressBytes[i];
					}
					socketAddress[24] = (byte)(this.address.ScopeId & 255L);
					socketAddress[25] = (byte)(this.address.ScopeId >> 8 & 255L);
					socketAddress[26] = (byte)(this.address.ScopeId >> 16 & 255L);
					socketAddress[27] = (byte)(this.address.ScopeId >> 24 & 255L);
				}
			}
			else
			{
				socketAddress = new SocketAddress(System.Net.Sockets.AddressFamily.InterNetwork, 16);
				socketAddress[2] = (byte)(this.port >> 8 & 255);
				socketAddress[3] = (byte)(this.port & 255);
				long internalIPv4Address = this.address.InternalIPv4Address;
				socketAddress[4] = (byte)(internalIPv4Address & 255L);
				socketAddress[5] = (byte)(internalIPv4Address >> 8 & 255L);
				socketAddress[6] = (byte)(internalIPv4Address >> 16 & 255L);
				socketAddress[7] = (byte)(internalIPv4Address >> 24 & 255L);
			}
			return socketAddress;
		}

		/// <summary>Returns the IP address and port number of the specified endpoint.</summary>
		/// <returns>A string containing the IP address and the port number of the specified endpoint (for example, 192.168.1.2:80).</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CCE RID: 7374 RVA: 0x0005526C File Offset: 0x0005346C
		public override string ToString()
		{
			return this.address.ToString() + ":" + this.port;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.IPEndPoint" /> instance.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="comparand">The specified <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Net.IPEndPoint" /> instance.</param>
		// Token: 0x06001CCF RID: 7375 RVA: 0x0005529C File Offset: 0x0005349C
		public override bool Equals(object obj)
		{
			IPEndPoint ipendPoint = obj as IPEndPoint;
			return ipendPoint != null && ipendPoint.port == this.port && ipendPoint.address.Equals(this.address);
		}

		/// <summary>Returns a hash value for a <see cref="T:System.Net.IPEndPoint" /> instance.</summary>
		/// <returns>An integer hash value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CD0 RID: 7376 RVA: 0x000552DC File Offset: 0x000534DC
		public override int GetHashCode()
		{
			return this.address.GetHashCode() + this.port;
		}

		/// <summary>Specifies the maximum value that can be assigned to the <see cref="P:System.Net.IPEndPoint.Port" /> property. The MaxPort value is set to 0x0000FFFF. This field is read-only.</summary>
		// Token: 0x04001210 RID: 4624
		public const int MaxPort = 65535;

		/// <summary>Specifies the minimum value that can be assigned to the <see cref="P:System.Net.IPEndPoint.Port" /> property. This field is read-only.</summary>
		// Token: 0x04001211 RID: 4625
		public const int MinPort = 0;

		// Token: 0x04001212 RID: 4626
		private IPAddress address;

		// Token: 0x04001213 RID: 4627
		private int port;
	}
}
