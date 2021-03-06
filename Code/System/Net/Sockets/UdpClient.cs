using System;

namespace System.Net.Sockets
{
	/// <summary>Provides User Datagram Protocol (UDP) network services.</summary>
	// Token: 0x0200040C RID: 1036
	public class UdpClient : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class.</summary>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x0600246F RID: 9327 RVA: 0x0006D7E0 File Offset: 0x0006B9E0
		public UdpClient() : this(AddressFamily.InterNetwork)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class.</summary>
		/// <param name="family">One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values that specifies the addressing scheme of the socket. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="family" /> is not <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002470 RID: 9328 RVA: 0x0006D7EC File Offset: 0x0006B9EC
		public UdpClient(AddressFamily family)
		{
			this.family = AddressFamily.InterNetwork;
			base..ctor();
			if (family != AddressFamily.InterNetwork && family != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentException("Family must be InterNetwork or InterNetworkV6", "family");
			}
			this.family = family;
			this.InitSocket(null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class and binds it to the local port number provided.</summary>
		/// <param name="port">The local port number from which you intend to communicate. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="port" /> parameter is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" /> or less than <see cref="F:System.Net.IPEndPoint.MinPort" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002471 RID: 9329 RVA: 0x0006D834 File Offset: 0x0006BA34
		public UdpClient(int port)
		{
			this.family = AddressFamily.InterNetwork;
			base..ctor();
			if (port < 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.family = AddressFamily.InterNetwork;
			IPEndPoint localEP = new IPEndPoint(IPAddress.Any, port);
			this.InitSocket(localEP);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class and binds it to the specified local endpoint.</summary>
		/// <param name="localEP">An <see cref="T:System.Net.IPEndPoint" /> that respresents the local endpoint to which you bind the UDP connection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002472 RID: 9330 RVA: 0x0006D888 File Offset: 0x0006BA88
		public UdpClient(IPEndPoint localEP)
		{
			this.family = AddressFamily.InterNetwork;
			base..ctor();
			if (localEP == null)
			{
				throw new ArgumentNullException("localEP");
			}
			this.family = localEP.AddressFamily;
			this.InitSocket(localEP);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class and binds it to the local port number provided.</summary>
		/// <param name="port">The port on which to listen for incoming connection attempts. </param>
		/// <param name="family">One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values that specifies the addressing scheme of the socket. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="family" /> is not <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" /> or less than <see cref="F:System.Net.IPEndPoint.MinPort" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002473 RID: 9331 RVA: 0x0006D8BC File Offset: 0x0006BABC
		public UdpClient(int port, AddressFamily family)
		{
			this.family = AddressFamily.InterNetwork;
			base..ctor();
			if (family != AddressFamily.InterNetwork && family != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentException("Family must be InterNetwork or InterNetworkV6", "family");
			}
			if (port < 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.family = family;
			IPEndPoint localEP;
			if (family == AddressFamily.InterNetwork)
			{
				localEP = new IPEndPoint(IPAddress.Any, port);
			}
			else
			{
				localEP = new IPEndPoint(IPAddress.IPv6Any, port);
			}
			this.InitSocket(localEP);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class and establishes a default remote host.</summary>
		/// <param name="hostname">The name of the remote DNS host to which you intend to connect. </param>
		/// <param name="port">The remote port number to which you intend to connect. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostname" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002474 RID: 9332 RVA: 0x0006D944 File Offset: 0x0006BB44
		public UdpClient(string hostname, int port)
		{
			this.family = AddressFamily.InterNetwork;
			base..ctor();
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			if (port < 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.InitSocket(null);
			this.Connect(hostname, port);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Sockets.UdpClient" />.</summary>
		// Token: 0x06002475 RID: 9333 RVA: 0x0006D99C File Offset: 0x0006BB9C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x0006D9AC File Offset: 0x0006BBAC
		private void InitSocket(EndPoint localEP)
		{
			if (this.socket != null)
			{
				this.socket.Close();
				this.socket = null;
			}
			this.socket = new Socket(this.family, SocketType.Dgram, ProtocolType.Udp);
			if (localEP != null)
			{
				this.socket.Bind(localEP);
			}
		}

		/// <summary>Closes the UDP connection.</summary>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002477 RID: 9335 RVA: 0x0006D9FC File Offset: 0x0006BBFC
		public void Close()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x0006DA04 File Offset: 0x0006BC04
		private void DoConnect(IPEndPoint endPoint)
		{
			try
			{
				this.socket.Connect(endPoint);
			}
			catch (SocketException ex)
			{
				if (ex.ErrorCode != 10013)
				{
					throw;
				}
				this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
				this.socket.Connect(endPoint);
			}
		}

		/// <summary>Establishes a default remote host using the specified network endpoint.</summary>
		/// <param name="endPoint">An <see cref="T:System.Net.IPEndPoint" /> that specifies the network endpoint to which you intend to send data. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="endPoint" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002479 RID: 9337 RVA: 0x0006DA7C File Offset: 0x0006BC7C
		public void Connect(IPEndPoint endPoint)
		{
			this.CheckDisposed();
			if (endPoint == null)
			{
				throw new ArgumentNullException("endPoint");
			}
			this.DoConnect(endPoint);
			this.active = true;
		}

		/// <summary>Establishes a default remote host using the specified IP address and port number.</summary>
		/// <param name="addr">The <see cref="T:System.Net.IPAddress" /> of the remote host to which you intend to send data. </param>
		/// <param name="port">The port number to which you intend send data. </param>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="addr" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600247A RID: 9338 RVA: 0x0006DAA4 File Offset: 0x0006BCA4
		public void Connect(IPAddress addr, int port)
		{
			if (addr == null)
			{
				throw new ArgumentNullException("addr");
			}
			if (port < 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.Connect(new IPEndPoint(addr, port));
		}

		/// <summary>Establishes a default remote host using the specified host name and port number.</summary>
		/// <param name="hostname">The DNS name of the remote host to which you intend send data. </param>
		/// <param name="port">The port number on the remote host to which you intend to send data. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600247B RID: 9339 RVA: 0x0006DAE4 File Offset: 0x0006BCE4
		public void Connect(string hostname, int port)
		{
			if (port < 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostname);
			for (int i = 0; i < hostAddresses.Length; i++)
			{
				try
				{
					this.family = hostAddresses[i].AddressFamily;
					this.Connect(new IPEndPoint(hostAddresses[i], port));
					break;
				}
				catch (Exception ex)
				{
					if (i == hostAddresses.Length - 1)
					{
						if (this.socket != null)
						{
							this.socket.Close();
							this.socket = null;
						}
						throw ex;
					}
				}
			}
		}

		/// <summary>Leaves a multicast group.</summary>
		/// <param name="multicastAddr">The <see cref="T:System.Net.IPAddress" /> of the multicast group to leave. </param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentException">The IP address is not compatible with the <see cref="T:System.Net.Sockets.AddressFamily" /> value that defines the addressing scheme of the socket. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="multicastAddr" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600247C RID: 9340 RVA: 0x0006DB9C File Offset: 0x0006BD9C
		public void DropMulticastGroup(IPAddress multicastAddr)
		{
			this.CheckDisposed();
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (this.family == AddressFamily.InterNetwork)
			{
				this.socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DropMembership, new MulticastOption(multicastAddr));
			}
			else
			{
				this.socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership, new IPv6MulticastOption(multicastAddr));
			}
		}

		/// <summary>Leaves a multicast group.</summary>
		/// <param name="multicastAddr">The <see cref="T:System.Net.IPAddress" /> of the multicast group to leave. </param>
		/// <param name="ifindex">The local address of the multicast group to leave.</param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentException">The IP address is not compatible with the <see cref="T:System.Net.Sockets.AddressFamily" /> value that defines the addressing scheme of the socket. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="multicastAddr" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600247D RID: 9341 RVA: 0x0006DBFC File Offset: 0x0006BDFC
		public void DropMulticastGroup(IPAddress multicastAddr, int ifindex)
		{
			this.CheckDisposed();
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (this.family == AddressFamily.InterNetworkV6)
			{
				this.socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership, new IPv6MulticastOption(multicastAddr, (long)ifindex));
			}
		}

		/// <summary>Adds a <see cref="T:System.Net.Sockets.UdpClient" /> to a multicast group.</summary>
		/// <param name="multicastAddr">The multicast <see cref="T:System.Net.IPAddress" /> of the group you want to join. </param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentException">The IP address is not compatible with the <see cref="T:System.Net.Sockets.AddressFamily" /> value that defines the addressing scheme of the socket. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600247E RID: 9342 RVA: 0x0006DC3C File Offset: 0x0006BE3C
		public void JoinMulticastGroup(IPAddress multicastAddr)
		{
			this.CheckDisposed();
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (this.family == AddressFamily.InterNetwork)
			{
				this.socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastAddr));
			}
			else
			{
				this.socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership, new IPv6MulticastOption(multicastAddr));
			}
		}

		/// <summary>Adds a <see cref="T:System.Net.Sockets.UdpClient" /> to a multicast group.</summary>
		/// <param name="ifindex">The local address. </param>
		/// <param name="multicastAddr">The multicast <see cref="T:System.Net.IPAddress" /> of the group you want to join. </param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600247F RID: 9343 RVA: 0x0006DC9C File Offset: 0x0006BE9C
		public void JoinMulticastGroup(int ifindex, IPAddress multicastAddr)
		{
			this.CheckDisposed();
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (this.family == AddressFamily.InterNetworkV6)
			{
				this.socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership, new IPv6MulticastOption(multicastAddr, (long)ifindex));
				return;
			}
			throw new SocketException(10045);
		}

		/// <summary>Adds a <see cref="T:System.Net.Sockets.UdpClient" /> to a multicast group with the specified Time to Live (TTL).</summary>
		/// <param name="multicastAddr">The <see cref="T:System.Net.IPAddress" /> of the multicast group to join. </param>
		/// <param name="timeToLive">The Time to Live (TTL), measured in router hops. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The TTL provided is not between 0 and 255 </exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="multicastAddr" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The IP address is not compatible with the <see cref="T:System.Net.Sockets.AddressFamily" /> value that defines the addressing scheme of the socket. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002480 RID: 9344 RVA: 0x0006DCF4 File Offset: 0x0006BEF4
		public void JoinMulticastGroup(IPAddress multicastAddr, int timeToLive)
		{
			this.CheckDisposed();
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (timeToLive < 0 || timeToLive > 255)
			{
				throw new ArgumentOutOfRangeException("timeToLive");
			}
			this.JoinMulticastGroup(multicastAddr);
			if (this.family == AddressFamily.InterNetwork)
			{
				this.socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, timeToLive);
			}
			else
			{
				this.socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.MulticastTimeToLive, timeToLive);
			}
		}

		/// <summary>Adds a <see cref="T:System.Net.Sockets.UdpClient" /> to a multicast group.</summary>
		/// <param name="multicastAddr">The multicast <see cref="T:System.Net.IPAddress" /> of the group you want to join.</param>
		/// <param name="localAddress">The local <see cref="T:System.Net.IPAddress" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002481 RID: 9345 RVA: 0x0006DD6C File Offset: 0x0006BF6C
		public void JoinMulticastGroup(IPAddress multicastAddr, IPAddress localAddress)
		{
			this.CheckDisposed();
			if (this.family == AddressFamily.InterNetwork)
			{
				this.socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastAddr, localAddress));
				return;
			}
			throw new SocketException(10045);
		}

		/// <summary>Returns a UDP datagram that was sent by a remote host.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" /> that contains datagram data.</returns>
		/// <param name="remoteEP">An <see cref="T:System.Net.IPEndPoint" /> that represents the remote host from which the data was sent. </param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002482 RID: 9346 RVA: 0x0006DDA8 File Offset: 0x0006BFA8
		public byte[] Receive(ref IPEndPoint remoteEP)
		{
			this.CheckDisposed();
			byte[] array = new byte[65536];
			EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
			int num = this.socket.ReceiveFrom(array, ref endPoint);
			if (num < array.Length)
			{
				array = this.CutArray(array, num);
			}
			remoteEP = (IPEndPoint)endPoint;
			return array;
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x0006DDFC File Offset: 0x0006BFFC
		private int DoSend(byte[] dgram, int bytes, IPEndPoint endPoint)
		{
			int result;
			try
			{
				if (endPoint == null)
				{
					result = this.socket.Send(dgram, 0, bytes, SocketFlags.None);
				}
				else
				{
					result = this.socket.SendTo(dgram, 0, bytes, SocketFlags.None, endPoint);
				}
			}
			catch (SocketException ex)
			{
				if (ex.ErrorCode != 10013)
				{
					throw;
				}
				this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
				if (endPoint == null)
				{
					result = this.socket.Send(dgram, 0, bytes, SocketFlags.None);
				}
				else
				{
					result = this.socket.SendTo(dgram, 0, bytes, SocketFlags.None, endPoint);
				}
			}
			return result;
		}

		/// <summary>Sends a UDP datagram to a remote host.</summary>
		/// <returns>The number of bytes sent.</returns>
		/// <param name="dgram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send represented as an array of bytes. </param>
		/// <param name="bytes">The number of bytes in the datagram. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002484 RID: 9348 RVA: 0x0006DEB8 File Offset: 0x0006C0B8
		public int Send(byte[] dgram, int bytes)
		{
			this.CheckDisposed();
			if (dgram == null)
			{
				throw new ArgumentNullException("dgram");
			}
			if (!this.active)
			{
				throw new InvalidOperationException("Operation not allowed on non-connected sockets.");
			}
			return this.DoSend(dgram, bytes, null);
		}

		/// <summary>Sends a UDP datagram to the host at the specified remote endpoint.</summary>
		/// <returns>The number of bytes sent.</returns>
		/// <param name="dgram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send, represented as an array of bytes. </param>
		/// <param name="bytes">The number of bytes in the datagram. </param>
		/// <param name="endPoint">An <see cref="T:System.Net.IPEndPoint" /> that represents the host and port to which to send the datagram. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002485 RID: 9349 RVA: 0x0006DEFC File Offset: 0x0006C0FC
		public int Send(byte[] dgram, int bytes, IPEndPoint endPoint)
		{
			this.CheckDisposed();
			if (dgram == null)
			{
				throw new ArgumentNullException("dgram is null");
			}
			if (!this.active)
			{
				return this.DoSend(dgram, bytes, endPoint);
			}
			if (endPoint != null)
			{
				throw new InvalidOperationException("Cannot send packets to an arbitrary host while connected.");
			}
			return this.DoSend(dgram, bytes, null);
		}

		/// <summary>Sends a UDP datagram to a specified port on a specified remote host.</summary>
		/// <returns>The number of bytes sent.</returns>
		/// <param name="dgram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send represented as an array of bytes. </param>
		/// <param name="bytes">The number of bytes in the datagram. </param>
		/// <param name="hostname">The name of the remote host to which you intend to send the datagram. </param>
		/// <param name="port">The remote port number with which you intend to communicate. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002486 RID: 9350 RVA: 0x0006DF50 File Offset: 0x0006C150
		public int Send(byte[] dgram, int bytes, string hostname, int port)
		{
			return this.Send(dgram, bytes, new IPEndPoint(Dns.GetHostAddresses(hostname)[0], port));
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x0006DF6C File Offset: 0x0006C16C
		private byte[] CutArray(byte[] orig, int length)
		{
			byte[] array = new byte[length];
			Buffer.BlockCopy(orig, 0, array, 0, length);
			return array;
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x0006DF8C File Offset: 0x0006C18C
		private IAsyncResult DoBeginSend(byte[] datagram, int bytes, IPEndPoint endPoint, AsyncCallback requestCallback, object state)
		{
			IAsyncResult result;
			try
			{
				if (endPoint == null)
				{
					result = this.socket.BeginSend(datagram, 0, bytes, SocketFlags.None, requestCallback, state);
				}
				else
				{
					result = this.socket.BeginSendTo(datagram, 0, bytes, SocketFlags.None, endPoint, requestCallback, state);
				}
			}
			catch (SocketException ex)
			{
				if (ex.ErrorCode != 10013)
				{
					throw;
				}
				this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
				if (endPoint == null)
				{
					result = this.socket.BeginSend(datagram, 0, bytes, SocketFlags.None, requestCallback, state);
				}
				else
				{
					result = this.socket.BeginSendTo(datagram, 0, bytes, SocketFlags.None, endPoint, requestCallback, state);
				}
			}
			return result;
		}

		/// <summary>Sends a datagram to a remote host asynchronously. The destination was specified previously by a call to <see cref="Overload:System.Net.Sockets.UdpClient.Connect" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous send.</returns>
		/// <param name="datagram">A <see cref="T:System.Byte" /> array that contains the data to be sent.</param>
		/// <param name="bytes">The number of bytes to send.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the send operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		// Token: 0x06002489 RID: 9353 RVA: 0x0006E058 File Offset: 0x0006C258
		public IAsyncResult BeginSend(byte[] datagram, int bytes, AsyncCallback requestCallback, object state)
		{
			return this.BeginSend(datagram, bytes, null, requestCallback, state);
		}

		/// <summary>Sends a datagram to a destination asynchronously. The destination is specified by a <see cref="T:System.Net.EndPoint" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous send.</returns>
		/// <param name="datagram">A <see cref="T:System.Byte" /> array that contains the data to be sent.</param>
		/// <param name="bytes">The number of bytes to send.</param>
		/// <param name="endPoint">The <see cref="T:System.Net.EndPoint" /> that represents the destination for the data.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the send operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		// Token: 0x0600248A RID: 9354 RVA: 0x0006E068 File Offset: 0x0006C268
		public IAsyncResult BeginSend(byte[] datagram, int bytes, IPEndPoint endPoint, AsyncCallback requestCallback, object state)
		{
			this.CheckDisposed();
			if (datagram == null)
			{
				throw new ArgumentNullException("datagram");
			}
			return this.DoBeginSend(datagram, bytes, endPoint, requestCallback, state);
		}

		/// <summary>Sends a datagram to a destination asynchronously. The destination is specified by the host name and port number.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous send.</returns>
		/// <param name="datagram">A <see cref="T:System.Byte" /> array that contains the data to be sent.</param>
		/// <param name="bytes">The number of bytes to send.</param>
		/// <param name="hostname">The destination host.</param>
		/// <param name="port">The destination port number.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the send operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		// Token: 0x0600248B RID: 9355 RVA: 0x0006E09C File Offset: 0x0006C29C
		public IAsyncResult BeginSend(byte[] datagram, int bytes, string hostname, int port, AsyncCallback requestCallback, object state)
		{
			return this.BeginSend(datagram, bytes, new IPEndPoint(Dns.GetHostAddresses(hostname)[0], port), requestCallback, state);
		}

		/// <summary>Ends a pending asynchronous send.</summary>
		/// <returns>If successful, the number of bytes sent to the <see cref="T:System.Net.Sockets.UdpClient" />.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object returned by a call to <see cref="Overload:System.Net.Sockets.UdpClient.BeginSend" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSend(System.IAsyncResult)" /> was previously called for the asynchronous read. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the underlying socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x0600248C RID: 9356 RVA: 0x0006E0BC File Offset: 0x0006C2BC
		public int EndSend(IAsyncResult asyncResult)
		{
			this.CheckDisposed();
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult is a null reference");
			}
			return this.socket.EndSend(asyncResult);
		}

		/// <summary>Receives a datagram from a remote host asynchronously.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous receive.</returns>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		// Token: 0x0600248D RID: 9357 RVA: 0x0006E0E4 File Offset: 0x0006C2E4
		public IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			this.CheckDisposed();
			this.recvbuffer = new byte[8192];
			EndPoint endPoint;
			if (this.family == AddressFamily.InterNetwork)
			{
				endPoint = new IPEndPoint(IPAddress.Any, 0);
			}
			else
			{
				endPoint = new IPEndPoint(IPAddress.IPv6Any, 0);
			}
			return this.socket.BeginReceiveFrom(this.recvbuffer, 0, 8192, SocketFlags.None, ref endPoint, callback, state);
		}

		/// <summary>Ends a pending asynchronous receive.</summary>
		/// <returns>If successful, the number of bytes received. If unsuccessful, this method returns 0.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object returned by a call to <see cref="M:System.Net.Sockets.UdpClient.BeginReceive(System.AsyncCallback,System.Object)" />.</param>
		/// <param name="remoteEP">The specified remote endpoint.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.UdpClient.BeginReceive(System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.UdpClient.EndReceive(System.IAsyncResult,System.Net.IPEndPoint@)" /> was previously called for the asynchronous read. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the underlying <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x0600248E RID: 9358 RVA: 0x0006E14C File Offset: 0x0006C34C
		public byte[] EndReceive(IAsyncResult asyncResult, ref IPEndPoint remoteEP)
		{
			this.CheckDisposed();
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult is a null reference");
			}
			EndPoint endPoint;
			if (this.family == AddressFamily.InterNetwork)
			{
				endPoint = new IPEndPoint(IPAddress.Any, 0);
			}
			else
			{
				endPoint = new IPEndPoint(IPAddress.IPv6Any, 0);
			}
			int num = this.socket.EndReceiveFrom(asyncResult, ref endPoint);
			remoteEP = (IPEndPoint)endPoint;
			byte[] array = new byte[num];
			Array.Copy(this.recvbuffer, array, num);
			return array;
		}

		/// <summary>Gets or sets a value indicating whether a default remote host has been established.</summary>
		/// <returns>true if a connection is active; otherwise, false.</returns>
		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x0006E1C8 File Offset: 0x0006C3C8
		// (set) Token: 0x06002490 RID: 9360 RVA: 0x0006E1D0 File Offset: 0x0006C3D0
		protected bool Active
		{
			get
			{
				return this.active;
			}
			set
			{
				this.active = value;
			}
		}

		/// <summary>Gets or sets the underlying network <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The underlying Network <see cref="T:System.Net.Sockets.Socket" />.</returns>
		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x0006E1DC File Offset: 0x0006C3DC
		// (set) Token: 0x06002492 RID: 9362 RVA: 0x0006E1E4 File Offset: 0x0006C3E4
		public Socket Client
		{
			get
			{
				return this.socket;
			}
			set
			{
				this.socket = value;
			}
		}

		/// <summary>Gets the amount of data received from the network that is available to read.</summary>
		/// <returns>The number of bytes of data received from the network.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x0006E1F0 File Offset: 0x0006C3F0
		public int Available
		{
			get
			{
				return this.socket.Available;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.UdpClient" /> allows Internet Protocol (IP) datagrams to be fragmented.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.UdpClient" /> allows datagram fragmentation; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.NotSupportedException">This property can be set only for sockets that use the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> flag or the <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> flag. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x0006E200 File Offset: 0x0006C400
		// (set) Token: 0x06002495 RID: 9365 RVA: 0x0006E210 File Offset: 0x0006C410
		public bool DontFragment
		{
			get
			{
				return this.socket.DontFragment;
			}
			set
			{
				this.socket.DontFragment = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.UdpClient" /> may send or receive broadcast packets.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.UdpClient" /> allows broadcast packets; otherwise, false. The default is false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002496 RID: 9366 RVA: 0x0006E220 File Offset: 0x0006C420
		// (set) Token: 0x06002497 RID: 9367 RVA: 0x0006E230 File Offset: 0x0006C430
		public bool EnableBroadcast
		{
			get
			{
				return this.socket.EnableBroadcast;
			}
			set
			{
				this.socket.EnableBroadcast = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.UdpClient" /> allows only one client to use a port.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.UdpClient" /> allows only one client to use a specific port; otherwise, false. The default is true for Windows Server 2003 and Windows XP Service Pack 2 and later, and false for all other versions.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the underlying socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x0006E240 File Offset: 0x0006C440
		// (set) Token: 0x06002499 RID: 9369 RVA: 0x0006E250 File Offset: 0x0006C450
		public bool ExclusiveAddressUse
		{
			get
			{
				return this.socket.ExclusiveAddressUse;
			}
			set
			{
				this.socket.ExclusiveAddressUse = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether outgoing multicast packets are delivered to the sending application.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.UdpClient" /> receives outgoing multicast packets; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x0006E260 File Offset: 0x0006C460
		// (set) Token: 0x0600249B RID: 9371 RVA: 0x0006E270 File Offset: 0x0006C470
		public bool MulticastLoopback
		{
			get
			{
				return this.socket.MulticastLoopback;
			}
			set
			{
				this.socket.MulticastLoopback = value;
			}
		}

		/// <summary>Gets or sets a value that specifies the Time to Live (TTL) value of Internet Protocol (IP) packets sent by the <see cref="T:System.Net.Sockets.UdpClient" />.</summary>
		/// <returns>The TTL value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x0600249C RID: 9372 RVA: 0x0006E280 File Offset: 0x0006C480
		// (set) Token: 0x0600249D RID: 9373 RVA: 0x0006E290 File Offset: 0x0006C490
		public short Ttl
		{
			get
			{
				return this.socket.Ttl;
			}
			set
			{
				this.socket.Ttl = value;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.UdpClient" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600249E RID: 9374 RVA: 0x0006E2A0 File Offset: 0x0006C4A0
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (disposing)
			{
				if (this.socket != null)
				{
					this.socket.Close();
				}
				this.socket = null;
			}
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x0006E2E4 File Offset: 0x0006C4E4
		~UdpClient()
		{
			this.Dispose(false);
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x0006E320 File Offset: 0x0006C520
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x040016CD RID: 5837
		private bool disposed;

		// Token: 0x040016CE RID: 5838
		private bool active;

		// Token: 0x040016CF RID: 5839
		private Socket socket;

		// Token: 0x040016D0 RID: 5840
		private AddressFamily family;

		// Token: 0x040016D1 RID: 5841
		private byte[] recvbuffer;
	}
}
