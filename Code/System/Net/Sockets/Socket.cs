using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Configuration;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Net.Sockets
{
	/// <summary>Implements the Berkeley sockets interface.</summary>
	// Token: 0x020003F6 RID: 1014
	public class Socket : IDisposable
	{
		// Token: 0x0600230E RID: 8974 RVA: 0x00066AD0 File Offset: 0x00064CD0
		private Socket(AddressFamily family, SocketType type, ProtocolType proto, IntPtr sock)
		{
			this.readQ = new Queue(2);
			this.writeQ = new Queue(2);
			this.MinListenPort = 7100;
			this.MaxListenPort = 7150;
			base..ctor();
			this.address_family = family;
			this.socket_type = type;
			this.protocol_type = proto;
			this.socket = sock;
			this.connected = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.Socket" /> class using the specified value returned from <see cref="M:System.Net.Sockets.Socket.DuplicateAndClose(System.Int32)" />.</summary>
		/// <param name="socketInformation">The socket information returned by <see cref="M:System.Net.Sockets.Socket.DuplicateAndClose(System.Int32)" />.</param>
		// Token: 0x0600230F RID: 8975 RVA: 0x00066B44 File Offset: 0x00064D44
		[MonoTODO]
		public Socket(SocketInformation socketInformation)
		{
			this.readQ = new Queue(2);
			this.writeQ = new Queue(2);
			this.MinListenPort = 7100;
			this.MaxListenPort = 7150;
			base..ctor();
			throw new NotImplementedException("SocketInformation not figured out yet");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.Socket" /> class using the specified address family, socket type and protocol.</summary>
		/// <param name="addressFamily">One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values. </param>
		/// <param name="socketType">One of the <see cref="T:System.Net.Sockets.SocketType" /> values. </param>
		/// <param name="protocolType">One of the <see cref="T:System.Net.Sockets.ProtocolType" /> values. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">The combination of <paramref name="addressFamily" />, <paramref name="socketType" />, and <paramref name="protocolType" /> results in an invalid socket. </exception>
		// Token: 0x06002310 RID: 8976 RVA: 0x00066BA0 File Offset: 0x00064DA0
		public Socket(AddressFamily family, SocketType type, ProtocolType proto)
		{
			this.readQ = new Queue(2);
			this.writeQ = new Queue(2);
			this.MinListenPort = 7100;
			this.MaxListenPort = 7150;
			base..ctor();
			this.address_family = family;
			this.socket_type = type;
			this.protocol_type = proto;
			int num;
			this.socket = this.Socket_internal(family, type, proto, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			this.SocketDefaults();
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x00066C2C File Offset: 0x00064E2C
		static Socket()
		{
			Socket.CheckProtocolSupport();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x06002312 RID: 8978 RVA: 0x00066C40 File Offset: 0x00064E40
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x00066C50 File Offset: 0x00064E50
		private static void AddSockets(ArrayList sockets, IList list, string name)
		{
			if (list != null)
			{
				foreach (object obj in list)
				{
					Socket socket = (Socket)obj;
					if (socket == null)
					{
						throw new ArgumentNullException("name", "Contains a null element");
					}
					sockets.Add(socket);
				}
			}
			sockets.Add(null);
		}

		// Token: 0x06002314 RID: 8980
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Select_internal(ref Socket[] sockets, int microSeconds, out int error);

		/// <summary>Determines the status of one or more sockets.</summary>
		/// <param name="checkRead">An <see cref="T:System.Collections.IList" /> of <see cref="T:System.Net.Sockets.Socket" /> instances to check for readability. </param>
		/// <param name="checkWrite">An <see cref="T:System.Collections.IList" /> of <see cref="T:System.Net.Sockets.Socket" /> instances to check for writability. </param>
		/// <param name="checkError">An <see cref="T:System.Collections.IList" /> of <see cref="T:System.Net.Sockets.Socket" /> instances to check for errors. </param>
		/// <param name="microSeconds">The time-out value, in microseconds. A -1 value indicates an infinite time-out.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="checkRead" /> parameter is null or empty.-and- The <paramref name="checkWrite" /> parameter is null or empty -and- The <paramref name="checkError" /> parameter is null or empty. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002315 RID: 8981 RVA: 0x00066CE0 File Offset: 0x00064EE0
		public static void Select(IList checkRead, IList checkWrite, IList checkError, int microSeconds)
		{
			ArrayList arrayList = new ArrayList();
			Socket.AddSockets(arrayList, checkRead, "checkRead");
			Socket.AddSockets(arrayList, checkWrite, "checkWrite");
			Socket.AddSockets(arrayList, checkError, "checkError");
			if (arrayList.Count == 3)
			{
				throw new ArgumentNullException("checkRead, checkWrite, checkError", "All the lists are null or empty.");
			}
			Socket[] array = (Socket[])arrayList.ToArray(typeof(Socket));
			int num;
			Socket.Select_internal(ref array, microSeconds, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			if (array == null)
			{
				if (checkRead != null)
				{
					checkRead.Clear();
				}
				if (checkWrite != null)
				{
					checkWrite.Clear();
				}
				if (checkError != null)
				{
					checkError.Clear();
				}
				return;
			}
			int num2 = 0;
			int num3 = array.Length;
			IList list = checkRead;
			int num4 = 0;
			for (int i = 0; i < num3; i++)
			{
				Socket socket = array[i];
				if (socket == null)
				{
					if (list != null)
					{
						int num5 = list.Count - num4;
						for (int j = 0; j < num5; j++)
						{
							list.RemoveAt(num4);
						}
					}
					list = ((num2 != 0) ? checkError : checkWrite);
					num4 = 0;
					num2++;
				}
				else
				{
					if (num2 == 1 && list == checkWrite && !socket.connected && (int)socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Error) == 0)
					{
						socket.connected = true;
					}
					if (num4 < list.Count)
					{
						while ((Socket)list[num4] != socket)
						{
							list.RemoveAt(num4);
						}
					}
					num4++;
				}
			}
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00066E88 File Offset: 0x00065088
		private void SocketDefaults()
		{
			try
			{
				if (this.address_family == AddressFamily.InterNetwork)
				{
					this.DontFragment = false;
				}
			}
			catch (SocketException)
			{
			}
		}

		// Token: 0x06002317 RID: 8983
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Available_internal(IntPtr socket, out int error);

		/// <summary>Gets the amount of data that has been received from the network and is available to be read.</summary>
		/// <returns>The number of bytes of data received from the network and available to be read.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x00066ED0 File Offset: 0x000650D0
		public int Available
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				int num;
				int result = Socket.Available_internal(this.socket, out num);
				if (num != 0)
				{
					throw new SocketException(num);
				}
				return result;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.Socket" /> allows Internet Protocol (IP) datagrams to be fragmented.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> allows datagram fragmentation; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.NotSupportedException">This property can be set only for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x00066F20 File Offset: 0x00065120
		// (set) Token: 0x0600231A RID: 8986 RVA: 0x00066FB0 File Offset: 0x000651B0
		public bool DontFragment
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				bool result;
				if (this.address_family == AddressFamily.InterNetwork)
				{
					result = ((int)this.GetSocketOption(SocketOptionLevel.IP, SocketOptionName.DontFragment) != 0);
				}
				else
				{
					if (this.address_family != AddressFamily.InterNetworkV6)
					{
						throw new NotSupportedException("This property is only valid for InterNetwork and InterNetworkV6 sockets");
					}
					result = ((int)this.GetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DontFragment) != 0);
				}
				return result;
			}
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.address_family == AddressFamily.InterNetwork)
				{
					this.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DontFragment, (!value) ? 0 : 1);
				}
				else
				{
					if (this.address_family != AddressFamily.InterNetworkV6)
					{
						throw new NotSupportedException("This property is only valid for InterNetwork and InterNetworkV6 sockets");
					}
					this.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DontFragment, (!value) ? 0 : 1);
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.Socket" /> can send or receive broadcast packets.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> allows broadcast packets; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">This option is valid for a datagram socket only. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x00067040 File Offset: 0x00065240
		// (set) Token: 0x0600231C RID: 8988 RVA: 0x000670A4 File Offset: 0x000652A4
		public bool EnableBroadcast
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.protocol_type != ProtocolType.Udp)
				{
					throw new SocketException(10042);
				}
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast) != 0;
			}
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.protocol_type != ProtocolType.Udp)
				{
					throw new SocketException(10042);
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, (!value) ? 0 : 1);
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.Socket" /> allows only one process to bind to a port.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> allows only one socket to bind to a specific port; otherwise, false. The default is true for Windows Server 2003 and Windows XP Service Pack 2, and false for all other versions.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> has been called for this <see cref="T:System.Net.Sockets.Socket" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x0006710C File Offset: 0x0006530C
		// (set) Token: 0x0600231E RID: 8990 RVA: 0x00067158 File Offset: 0x00065358
		public bool ExclusiveAddressUse
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse) != 0;
			}
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.isbound)
				{
					throw new InvalidOperationException("Bind has already been called for this socket");
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse, (!value) ? 0 : 1);
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.Socket" /> is bound to a specific local port.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> is bound to a local port; otherwise, false.</returns>
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x000671BC File Offset: 0x000653BC
		public bool IsBound
		{
			get
			{
				return this.isbound;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the <see cref="T:System.Net.Sockets.Socket" /> will delay closing a socket in an attempt to send all pending data.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.LingerOption" /> that specifies how to linger while closing a socket.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x000671C4 File Offset: 0x000653C4
		// (set) Token: 0x06002321 RID: 8993 RVA: 0x00067210 File Offset: 0x00065410
		public LingerOption LingerState
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				return (LingerOption)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger);
			}
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, value);
			}
		}

		/// <summary>Gets or sets a value that specifies whether outgoing multicast packets are delivered to the sending application.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> receives outgoing multicast packets; otherwise, false.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x00067258 File Offset: 0x00065458
		// (set) Token: 0x06002323 RID: 8995 RVA: 0x00067300 File Offset: 0x00065500
		public bool MulticastLoopback
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.protocol_type == ProtocolType.Tcp)
				{
					throw new SocketException(10042);
				}
				bool result;
				if (this.address_family == AddressFamily.InterNetwork)
				{
					result = ((int)this.GetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastLoopback) != 0);
				}
				else
				{
					if (this.address_family != AddressFamily.InterNetworkV6)
					{
						throw new NotSupportedException("This property is only valid for InterNetwork and InterNetworkV6 sockets");
					}
					result = ((int)this.GetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.MulticastLoopback) != 0);
				}
				return result;
			}
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.protocol_type == ProtocolType.Tcp)
				{
					throw new SocketException(10042);
				}
				if (this.address_family == AddressFamily.InterNetwork)
				{
					this.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastLoopback, (!value) ? 0 : 1);
				}
				else
				{
					if (this.address_family != AddressFamily.InterNetworkV6)
					{
						throw new NotSupportedException("This property is only valid for InterNetwork and InterNetworkV6 sockets");
					}
					this.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.MulticastLoopback, (!value) ? 0 : 1);
				}
			}
		}

		/// <summary>Specifies whether the socket should only use Overlapped I/O mode.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> uses only overlapped I/O; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The socket has been bound to a completion port.</exception>
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x000673A8 File Offset: 0x000655A8
		// (set) Token: 0x06002325 RID: 8997 RVA: 0x000673B0 File Offset: 0x000655B0
		[MonoTODO("This doesn't do anything on Mono yet")]
		public bool UseOnlyOverlappedIO
		{
			get
			{
				return this.useoverlappedIO;
			}
			set
			{
				this.useoverlappedIO = value;
			}
		}

		/// <summary>Gets the operating system handle for the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that represents the operating system handle for the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x000673BC File Offset: 0x000655BC
		public IntPtr Handle
		{
			get
			{
				return this.socket;
			}
		}

		// Token: 0x06002327 RID: 8999
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SocketAddress LocalEndPoint_internal(IntPtr socket, out int error);

		/// <summary>Gets the local endpoint.</summary>
		/// <returns>The <see cref="T:System.Net.EndPoint" /> that the <see cref="T:System.Net.Sockets.Socket" /> is using for communications.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x000673C4 File Offset: 0x000655C4
		public EndPoint LocalEndPoint
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.seed_endpoint == null)
				{
					return null;
				}
				int num;
				SocketAddress address = Socket.LocalEndPoint_internal(this.socket, out num);
				if (num != 0)
				{
					throw new SocketException(num);
				}
				return this.seed_endpoint.Create(address);
			}
		}

		/// <summary>Gets the type of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.SocketType" /> values.</returns>
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x0006742C File Offset: 0x0006562C
		public SocketType SocketType
		{
			get
			{
				return this.socket_type;
			}
		}

		/// <summary>Gets or sets a value that specifies the amount of time after which a synchronous <see cref="Overload:System.Net.Sockets.Socket.Send" /> call will time out.</summary>
		/// <returns>The time-out value, in milliseconds. If you set the property with a value between 1 and 499, the value will be changed to 500. The default value is 0, which indicates an infinite time-out period. Specifying -1 also indicates an infinite time-out period.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than -1.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x00067434 File Offset: 0x00065634
		// (set) Token: 0x0600232B RID: 9003 RVA: 0x00067480 File Offset: 0x00065680
		public int SendTimeout
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
			}
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value", "The value specified for a set operation is less than -1");
				}
				if (value == -1)
				{
					value = 0;
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, value);
			}
		}

		/// <summary>Gets or sets a value that specifies the amount of time after which a synchronous <see cref="Overload:System.Net.Sockets.Socket.Receive" /> call will time out.</summary>
		/// <returns>The time-out value, in milliseconds. The default value is 0, which indicates an infinite time-out period. Specifying -1 also indicates an infinite time-out period.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than -1.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x000674E8 File Offset: 0x000656E8
		// (set) Token: 0x0600232D RID: 9005 RVA: 0x00067534 File Offset: 0x00065734
		public int ReceiveTimeout
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			}
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value", "The value specified for a set operation is less than -1");
				}
				if (value == -1)
				{
					value = 0;
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, value);
			}
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation.Returns false if the I/O operation completed synchronously. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentException">An argument is not valid. This exception occurs if the buffer provided is not large enough. The buffer must be at least 2 * (sizeof(SOCKADDR_STORAGE + 16) bytes. This exception also occurs if multiple buffers are specified, the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property is not null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An argument is out of range. The exception occurs if the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Count" /> is less than 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">An invalid operation was requested. This exception occurs if the accepting <see cref="T:System.Net.Sockets.Socket" /> is not listening for connections or the accepted socket is bound. You must call the <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> method before calling the <see cref="M:System.Net.Sockets.Socket.AcceptAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.This exception also occurs if the socket is already connected or a socket operation was already in progress using the specified <paramref name="e" /> parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x0600232E RID: 9006 RVA: 0x0006759C File Offset: 0x0006579C
		public bool AcceptAsync(SocketAsyncEventArgs e)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (!this.IsBound)
			{
				throw new InvalidOperationException("You must call the Bind method before performing this operation.");
			}
			if (!this.islistening)
			{
				throw new InvalidOperationException("You must call the Listen method before performing this operation.");
			}
			if (e.BufferList != null)
			{
				throw new ArgumentException("Multiple buffers cannot be used with this method.");
			}
			if (e.Count < 0)
			{
				throw new ArgumentOutOfRangeException("e.Count");
			}
			Socket acceptSocket = e.AcceptSocket;
			if (acceptSocket != null)
			{
				if (acceptSocket.IsBound || acceptSocket.Connected)
				{
					throw new InvalidOperationException("AcceptSocket: The socket must not be bound or connected.");
				}
			}
			else
			{
				e.AcceptSocket = new Socket(this.AddressFamily, this.SocketType, this.ProtocolType);
			}
			try
			{
				e.DoOperation(SocketAsyncOperation.Accept, this);
			}
			catch
			{
				((IDisposable)e).Dispose();
				throw;
			}
			return true;
		}

		// Token: 0x0600232F RID: 9007
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Accept_internal(IntPtr sock, out int error, bool blocking);

		/// <summary>Creates a new <see cref="T:System.Net.Sockets.Socket" /> for a newly created connection.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> for a newly created connection.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.InvalidOperationException">The accepting socket is not listening for connections. You must call <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> before calling <see cref="M:System.Net.Sockets.Socket.Accept" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002330 RID: 9008 RVA: 0x000676B4 File Offset: 0x000658B4
		public Socket Accept()
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			int num = 0;
			IntPtr sock = (IntPtr)(-1);
			this.blocking_thread = Thread.CurrentThread;
			try
			{
				sock = Socket.Accept_internal(this.socket, out num, this.blocking);
			}
			catch (ThreadAbortException)
			{
				if (this.disposed)
				{
					Thread.ResetAbort();
					num = 10004;
				}
			}
			finally
			{
				this.blocking_thread = null;
			}
			if (num != 0)
			{
				throw new SocketException(num);
			}
			return new Socket(this.AddressFamily, this.SocketType, this.ProtocolType, sock)
			{
				seed_endpoint = this.seed_endpoint,
				Blocking = this.Blocking
			};
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000677AC File Offset: 0x000659AC
		internal void Accept(Socket acceptSocket)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			int num = 0;
			IntPtr intPtr = (IntPtr)(-1);
			this.blocking_thread = Thread.CurrentThread;
			try
			{
				intPtr = Socket.Accept_internal(this.socket, out num, this.blocking);
			}
			catch (ThreadAbortException)
			{
				if (this.disposed)
				{
					Thread.ResetAbort();
					num = 10004;
				}
			}
			finally
			{
				this.blocking_thread = null;
			}
			if (num != 0)
			{
				throw new SocketException(num);
			}
			acceptSocket.address_family = this.AddressFamily;
			acceptSocket.socket_type = this.SocketType;
			acceptSocket.protocol_type = this.ProtocolType;
			acceptSocket.socket = intPtr;
			acceptSocket.connected = true;
			acceptSocket.seed_endpoint = this.seed_endpoint;
			acceptSocket.Blocking = this.Blocking;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous <see cref="T:System.Net.Sockets.Socket" /> creation.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method. </exception>
		/// <exception cref="T:System.InvalidOperationException">The accepting socket is not listening for connections. You must call <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> before calling <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />.-or- The accepted socket is bound. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="receiveSize" /> is less than 0. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002332 RID: 9010 RVA: 0x000678BC File Offset: 0x00065ABC
		public IAsyncResult BeginAccept(AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (!this.isbound || !this.islistening)
			{
				throw new InvalidOperationException();
			}
			Socket.SocketAsyncResult socketAsyncResult = new Socket.SocketAsyncResult(this, state, callback, Socket.SocketOperation.Accept);
			Socket.Worker @object = new Socket.Worker(socketAsyncResult);
			Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(@object.Accept);
			socketAsyncCall.BeginInvoke(null, socketAsyncResult);
			return socketAsyncResult;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt and receives the first block of data sent by the client application.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous <see cref="T:System.Net.Sockets.Socket" /> creation.</returns>
		/// <param name="receiveSize">The number of bytes to accept from the sender. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method. </exception>
		/// <exception cref="T:System.InvalidOperationException">The accepting socket is not listening for connections. You must call <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> before calling <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />.-or- The accepted socket is bound. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="receiveSize" /> is less than 0. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002333 RID: 9011 RVA: 0x00067934 File Offset: 0x00065B34
		public IAsyncResult BeginAccept(int receiveSize, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (receiveSize < 0)
			{
				throw new ArgumentOutOfRangeException("receiveSize", "receiveSize is less than zero");
			}
			Socket.SocketAsyncResult socketAsyncResult = new Socket.SocketAsyncResult(this, state, callback, Socket.SocketOperation.AcceptReceive);
			Socket.Worker @object = new Socket.Worker(socketAsyncResult);
			Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(@object.AcceptReceive);
			socketAsyncResult.Buffer = new byte[receiveSize];
			socketAsyncResult.Offset = 0;
			socketAsyncResult.Size = receiveSize;
			socketAsyncResult.SockFlags = SocketFlags.None;
			socketAsyncCall.BeginInvoke(null, socketAsyncResult);
			return socketAsyncResult;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt from a specified socket and receives the first block of data sent by the client application.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous <see cref="T:System.Net.Sockets.Socket" /> object creation.</returns>
		/// <param name="acceptSocket">The accepted <see cref="T:System.Net.Sockets.Socket" /> object. This value may be null. </param>
		/// <param name="receiveSize">The maximum number of bytes to receive. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method. </exception>
		/// <exception cref="T:System.InvalidOperationException">The accepting socket is not listening for connections. You must call <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> before calling <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />.-or- The accepted socket is bound. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="receiveSize" /> is less than 0. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002334 RID: 9012 RVA: 0x000679CC File Offset: 0x00065BCC
		public IAsyncResult BeginAccept(Socket acceptSocket, int receiveSize, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (receiveSize < 0)
			{
				throw new ArgumentOutOfRangeException("receiveSize", "receiveSize is less than zero");
			}
			if (acceptSocket != null)
			{
				if (acceptSocket.disposed && acceptSocket.closed)
				{
					throw new ObjectDisposedException(acceptSocket.GetType().ToString());
				}
				if (acceptSocket.IsBound)
				{
					throw new InvalidOperationException();
				}
				if (acceptSocket.ProtocolType != ProtocolType.Tcp)
				{
					throw new SocketException(10022);
				}
			}
			Socket.SocketAsyncResult socketAsyncResult = new Socket.SocketAsyncResult(this, state, callback, Socket.SocketOperation.AcceptReceive);
			Socket.Worker @object = new Socket.Worker(socketAsyncResult);
			Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(@object.AcceptReceive);
			socketAsyncResult.Buffer = new byte[receiveSize];
			socketAsyncResult.Offset = 0;
			socketAsyncResult.Size = receiveSize;
			socketAsyncResult.SockFlags = SocketFlags.None;
			socketAsyncResult.AcceptSocket = acceptSocket;
			socketAsyncCall.BeginInvoke(null, socketAsyncResult);
			return socketAsyncResult;
		}

		/// <summary>Begins an asynchronous request for a remote host connection.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous connection.</returns>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the remote host. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002335 RID: 9013 RVA: 0x00067AC0 File Offset: 0x00065CC0
		public IAsyncResult BeginConnect(EndPoint end_point, AsyncCallback callback, object state)
		{
			return this.BeginConnect(end_point, callback, state, false);
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00067ACC File Offset: 0x00065CCC
		internal IAsyncResult BeginConnect(EndPoint end_point, AsyncCallback callback, object state, bool bypassSocketSecurity)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (end_point == null)
			{
				throw new ArgumentNullException("end_point");
			}
			Socket.SocketAsyncResult socketAsyncResult = new Socket.SocketAsyncResult(this, state, callback, Socket.SocketOperation.Connect);
			socketAsyncResult.EndPoint = end_point;
			if (end_point is IPEndPoint)
			{
				IPEndPoint ipendPoint = (IPEndPoint)end_point;
				if (ipendPoint.Address.Equals(IPAddress.Any) || ipendPoint.Address.Equals(IPAddress.IPv6Any))
				{
					socketAsyncResult.Complete(new SocketException(10049), true);
					return socketAsyncResult;
				}
			}
			int num = 0;
			if (!this.blocking)
			{
				SocketAddress sa = end_point.Serialize();
				Socket.Connect_internal(this.socket, sa, out num);
				if (num == 0)
				{
					this.connected = true;
					socketAsyncResult.Complete(true);
				}
				else if (num != 10036 && num != 10035)
				{
					this.connected = false;
					socketAsyncResult.Complete(new SocketException(num), true);
				}
			}
			if (this.blocking || num == 10036 || num == 10035)
			{
				this.connected = false;
				Socket.Worker @object = new Socket.Worker(socketAsyncResult, bypassSocketSecurity);
				Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(@object.Connect);
				socketAsyncCall.BeginInvoke(null, socketAsyncResult);
			}
			return socketAsyncResult;
		}

		/// <summary>Begins an asynchronous request for a remote host connection. The host is specified by an <see cref="T:System.Net.IPAddress" /> and a port number.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous connection.</returns>
		/// <param name="address">The <see cref="T:System.Net.IPAddress" /> of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the connect operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the connect operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Net.Sockets.Socket" /> is not in the socket family.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="address" /> is zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002337 RID: 9015 RVA: 0x00067C24 File Offset: 0x00065E24
		public IAsyncResult BeginConnect(IPAddress address, int port, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.ToString().Length == 0)
			{
				throw new ArgumentException("The length of the IP address is zero");
			}
			if (this.islistening)
			{
				throw new InvalidOperationException();
			}
			IPEndPoint end_point = new IPEndPoint(address, port);
			return this.BeginConnect(end_point, callback, state);
		}

		/// <summary>Begins an asynchronous request for a remote host connection. The host is specified by an <see cref="T:System.Net.IPAddress" /> array and a port number.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous connections.</returns>
		/// <param name="addresses">At least one <see cref="T:System.Net.IPAddress" />, designating the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the connect operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the connect operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="addresses" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets that use <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="address" /> is zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002338 RID: 9016 RVA: 0x00067CA8 File Offset: 0x00065EA8
		public IAsyncResult BeginConnect(IPAddress[] addresses, int port, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (addresses == null)
			{
				throw new ArgumentNullException("addresses");
			}
			if (this.AddressFamily != AddressFamily.InterNetwork && this.AddressFamily != AddressFamily.InterNetworkV6)
			{
				throw new NotSupportedException("This method is only valid for addresses in the InterNetwork or InterNetworkV6 families");
			}
			if (this.islistening)
			{
				throw new InvalidOperationException();
			}
			Socket.SocketAsyncResult socketAsyncResult = new Socket.SocketAsyncResult(this, state, callback, Socket.SocketOperation.Connect);
			socketAsyncResult.Addresses = addresses;
			socketAsyncResult.Port = port;
			this.connected = false;
			Socket.Worker @object = new Socket.Worker(socketAsyncResult);
			Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(@object.Connect);
			socketAsyncCall.BeginInvoke(null, socketAsyncResult);
			return socketAsyncResult;
		}

		/// <summary>Begins an asynchronous request for a remote host connection. The host is specified by a host name and a port number.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous connection.</returns>
		/// <param name="host">The name of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the connect operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the connect operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002339 RID: 9017 RVA: 0x00067D60 File Offset: 0x00065F60
		public IAsyncResult BeginConnect(string host, int port, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (this.address_family != AddressFamily.InterNetwork && this.address_family != AddressFamily.InterNetworkV6)
			{
				throw new NotSupportedException("This method is valid only for sockets in the InterNetwork and InterNetworkV6 families");
			}
			if (this.islistening)
			{
				throw new InvalidOperationException();
			}
			IPAddress[] hostAddresses = Dns.GetHostAddresses(host);
			return this.BeginConnect(hostAddresses, port, callback, state);
		}

		/// <summary>Begins an asynchronous request to disconnect from a remote endpoint.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous operation.</returns>
		/// <param name="reuseSocket">true if this socket can be reused after the connection is closed; otherwise, false. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows 2000 or earlier, and this method requires Windows XP. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600233A RID: 9018 RVA: 0x00067DEC File Offset: 0x00065FEC
		public IAsyncResult BeginDisconnect(bool reuseSocket, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			Socket.SocketAsyncResult socketAsyncResult = new Socket.SocketAsyncResult(this, state, callback, Socket.SocketOperation.Disconnect);
			socketAsyncResult.ReuseSocket = reuseSocket;
			Socket.Worker @object = new Socket.Worker(socketAsyncResult);
			Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(@object.Disconnect);
			socketAsyncCall.BeginInvoke(null, socketAsyncResult);
			return socketAsyncResult;
		}

		/// <summary>Begins to asynchronously receive data from a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data. </param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to store the received data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600233B RID: 9019 RVA: 0x00067E50 File Offset: 0x00066050
		public IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags socket_flags, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Queue obj = this.readQ;
			Socket.SocketAsyncResult socketAsyncResult;
			lock (obj)
			{
				socketAsyncResult = new Socket.SocketAsyncResult(this, state, callback, Socket.SocketOperation.Receive);
				socketAsyncResult.Buffer = buffer;
				socketAsyncResult.Offset = offset;
				socketAsyncResult.Size = size;
				socketAsyncResult.SockFlags = socket_flags;
				this.readQ.Enqueue(socketAsyncResult);
				if (this.readQ.Count == 1)
				{
					Socket.Worker @object = new Socket.Worker(socketAsyncResult);
					Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(@object.Receive);
					socketAsyncCall.BeginInvoke(null, socketAsyncResult);
				}
			}
			return socketAsyncResult;
		}

		/// <summary>Begins to asynchronously receive data from a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="offset">The location in <paramref name="buffer" /> to store the received data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		// Token: 0x0600233C RID: 9020 RVA: 0x00067F68 File Offset: 0x00066168
		public IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags flags, out SocketError error, AsyncCallback callback, object state)
		{
			error = SocketError.Success;
			return this.BeginReceive(buffer, offset, size, flags, callback, state);
		}

		/// <summary>Begins to asynchronously receive data from a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <param name="buffers">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x0600233D RID: 9021 RVA: 0x00067F80 File Offset: 0x00066180
		[CLSCompliant(false)]
		public IAsyncResult BeginReceive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			Queue obj = this.readQ;
			Socket.SocketAsyncResult socketAsyncResult;
			lock (obj)
			{
				socketAsyncResult = new Socket.SocketAsyncResult(this, state, callback, Socket.SocketOperation.ReceiveGeneric);
				socketAsyncResult.Buffers = buffers;
				socketAsyncResult.SockFlags = socketFlags;
				this.readQ.Enqueue(socketAsyncResult);
				if (this.readQ.Count == 1)
				{
					Socket.Worker @object = new Socket.Worker(socketAsyncResult);
					Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(@object.ReceiveGeneric);
					socketAsyncCall.BeginInvoke(null, socketAsyncResult);
				}
			}
			return socketAsyncResult;
		}

		/// <summary>Begins to asynchronously receive data from a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <param name="buffers">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x0600233E RID: 9022 RVA: 0x00068050 File Offset: 0x00066250
		[CLSCompliant(false)]
		public IAsyncResult BeginReceive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state)
		{
			errorCode = SocketError.Success;
			return this.BeginReceive(buffers, socketFlags, callback, state);
		}

		/// <summary>Begins to asynchronously receive data from a specified network device.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data. </param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to store the data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the source of the data. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600233F RID: 9023 RVA: 0x00068064 File Offset: 0x00066264
		public IAsyncResult BeginReceiveFrom(byte[] buffer, int offset, int size, SocketFlags socket_flags, ref EndPoint remote_end, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "offset must be >= 0");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size", "size must be >= 0");
			}
			if (offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset, size", "offset + size exceeds the buffer length");
			}
			Queue obj = this.readQ;
			Socket.SocketAsyncResult socketAsyncResult;
			lock (obj)
			{
				socketAsyncResult = new Socket.SocketAsyncResult(this, state, callback, Socket.SocketOperation.ReceiveFrom);
				socketAsyncResult.Buffer = buffer;
				socketAsyncResult.Offset = offset;
				socketAsyncResult.Size = size;
				socketAsyncResult.SockFlags = socket_flags;
				socketAsyncResult.EndPoint = remote_end;
				this.readQ.Enqueue(socketAsyncResult);
				if (this.readQ.Count == 1)
				{
					Socket.Worker @object = new Socket.Worker(socketAsyncResult);
					Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(@object.ReceiveFrom);
					socketAsyncCall.BeginInvoke(null, socketAsyncResult);
				}
			}
			return socketAsyncResult;
		}

		/// <summary>Begins to asynchronously receive the specified number of bytes of data into the specified location of the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint and packet information..</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data. </param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to store the data.</param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the source of the data.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows 2000 or earlier, and this method requires Windows XP.</exception>
		// Token: 0x06002340 RID: 9024 RVA: 0x00068194 File Offset: 0x00066394
		[MonoTODO]
		public IAsyncResult BeginReceiveMessageFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref EndPoint remoteEP, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			throw new NotImplementedException();
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to send. </param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to begin sending data. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is less than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002341 RID: 9025 RVA: 0x0006822C File Offset: 0x0006642C
		public IAsyncResult BeginSend(byte[] buffer, int offset, int size, SocketFlags socket_flags, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "offset must be >= 0");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size", "size must be >= 0");
			}
			if (offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset, size", "offset + size exceeds the buffer length");
			}
			if (!this.connected)
			{
				throw new SocketException(10057);
			}
			Queue obj = this.writeQ;
			Socket.SocketAsyncResult socketAsyncResult;
			lock (obj)
			{
				socketAsyncResult = new Socket.SocketAsyncResult(this, state, callback, Socket.SocketOperation.Send);
				socketAsyncResult.Buffer = buffer;
				socketAsyncResult.Offset = offset;
				socketAsyncResult.Size = size;
				socketAsyncResult.SockFlags = socket_flags;
				this.writeQ.Enqueue(socketAsyncResult);
				if (this.writeQ.Count == 1)
				{
					Socket.Worker @object = new Socket.Worker(socketAsyncResult);
					Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(@object.Send);
					socketAsyncCall.BeginInvoke(null, socketAsyncResult);
				}
			}
			return socketAsyncResult;
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to send. </param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to begin sending data. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is less than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002342 RID: 9026 RVA: 0x0006836C File Offset: 0x0006656C
		public IAsyncResult BeginSend(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state)
		{
			if (!this.connected)
			{
				errorCode = SocketError.NotConnected;
				throw new SocketException((int)errorCode);
			}
			errorCode = SocketError.Success;
			return this.BeginSend(buffer, offset, size, socketFlags, callback, state);
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <param name="buffers">An array of type <see cref="T:System.Byte" /> that contains the data to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002343 RID: 9027 RVA: 0x000683A0 File Offset: 0x000665A0
		public IAsyncResult BeginSend(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			if (!this.connected)
			{
				throw new SocketException(10057);
			}
			Queue obj = this.writeQ;
			Socket.SocketAsyncResult socketAsyncResult;
			lock (obj)
			{
				socketAsyncResult = new Socket.SocketAsyncResult(this, state, callback, Socket.SocketOperation.SendGeneric);
				socketAsyncResult.Buffers = buffers;
				socketAsyncResult.SockFlags = socketFlags;
				this.writeQ.Enqueue(socketAsyncResult);
				if (this.writeQ.Count == 1)
				{
					Socket.Worker @object = new Socket.Worker(socketAsyncResult);
					Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(@object.SendGeneric);
					socketAsyncCall.BeginInvoke(null, socketAsyncResult);
				}
			}
			return socketAsyncResult;
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <param name="buffers">An array of type <see cref="T:System.Byte" /> that contains the data to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002344 RID: 9028 RVA: 0x00068488 File Offset: 0x00066688
		[CLSCompliant(false)]
		public IAsyncResult BeginSend(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state)
		{
			if (!this.connected)
			{
				errorCode = SocketError.NotConnected;
				throw new SocketException((int)errorCode);
			}
			errorCode = SocketError.Success;
			return this.BeginSend(buffers, socketFlags, callback, state);
		}

		/// <summary>Sends the file <paramref name="fileName" /> to a connected <see cref="T:System.Net.Sockets.Socket" /> object using the <see cref="F:System.Net.Sockets.TransmitFileOptions.UseDefaultWorkerThread" /> flag.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that represents the asynchronous send.</returns>
		/// <param name="fileName">A string that contains the path and name of the file to send. This parameter can be null. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The socket is not connected to a remote host. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> was not found. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002345 RID: 9029 RVA: 0x000684B4 File Offset: 0x000666B4
		public IAsyncResult BeginSendFile(string fileName, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (!this.connected)
			{
				throw new NotSupportedException();
			}
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException();
			}
			return this.BeginSendFile(fileName, null, null, TransmitFileOptions.UseDefaultWorkerThread, callback, state);
		}

		/// <summary>Sends a file and buffers of data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" /> object.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that represents the asynchronous operation.</returns>
		/// <param name="fileName">A string that contains the path and name of the file to be sent. This parameter can be null. </param>
		/// <param name="preBuffer">A <see cref="T:System.Byte" /> array that contains data to be sent before the file is sent. This parameter can be null. </param>
		/// <param name="postBuffer">A <see cref="T:System.Byte" /> array that contains data to be sent after the file is sent. This parameter can be null. </param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Net.Sockets.TransmitFileOptions" /> values. </param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate to be invoked when this operation completes. This parameter can be null. </param>
		/// <param name="state">A user-defined object that contains state information for this request. This parameter can be null. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <exception cref="T:System.NotSupportedException">The operating system is not Windows NT or later.- or - The socket is not connected to a remote host. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> was not found. </exception>
		// Token: 0x06002346 RID: 9030 RVA: 0x00068518 File Offset: 0x00066718
		public IAsyncResult BeginSendFile(string fileName, byte[] preBuffer, byte[] postBuffer, TransmitFileOptions flags, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (!this.connected)
			{
				throw new NotSupportedException();
			}
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException();
			}
			Socket.SendFileHandler sendFileHandler = new Socket.SendFileHandler(this.SendFile);
			return new Socket.SendFileAsyncResult(sendFileHandler, sendFileHandler.BeginInvoke(fileName, preBuffer, postBuffer, flags, callback, state));
		}

		/// <summary>Sends data asynchronously to a specific remote host.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to send. </param>
		/// <param name="offset">The zero-based position in <paramref name="buffer" /> at which to begin sending data. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the remote device. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002347 RID: 9031 RVA: 0x00068590 File Offset: 0x00066790
		public IAsyncResult BeginSendTo(byte[] buffer, int offset, int size, SocketFlags socket_flags, EndPoint remote_end, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "offset must be >= 0");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size", "size must be >= 0");
			}
			if (offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset, size", "offset + size exceeds the buffer length");
			}
			Queue obj = this.writeQ;
			Socket.SocketAsyncResult socketAsyncResult;
			lock (obj)
			{
				socketAsyncResult = new Socket.SocketAsyncResult(this, state, callback, Socket.SocketOperation.SendTo);
				socketAsyncResult.Buffer = buffer;
				socketAsyncResult.Offset = offset;
				socketAsyncResult.Size = size;
				socketAsyncResult.SockFlags = socket_flags;
				socketAsyncResult.EndPoint = remote_end;
				this.writeQ.Enqueue(socketAsyncResult);
				if (this.writeQ.Count == 1)
				{
					Socket.Worker @object = new Socket.Worker(socketAsyncResult);
					Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(@object.SendTo);
					socketAsyncCall.BeginInvoke(null, socketAsyncResult);
				}
			}
			return socketAsyncResult;
		}

		// Token: 0x06002348 RID: 9032
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Bind_internal(IntPtr sock, SocketAddress sa, out int error);

		/// <summary>Associates a <see cref="T:System.Net.Sockets.Socket" /> with a local endpoint.</summary>
		/// <param name="localEP">The local <see cref="T:System.Net.EndPoint" /> to associate with the <see cref="T:System.Net.Sockets.Socket" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002349 RID: 9033 RVA: 0x000686C0 File Offset: 0x000668C0
		public void Bind(EndPoint local_end)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (local_end == null)
			{
				throw new ArgumentNullException("local_end");
			}
			if (Environment.SocketSecurityEnabled && Socket.current_bind_count >= this.max_bind_count)
			{
				throw new SecurityException("Too many sockets are bound, maximum count in the webplayer is " + this.max_bind_count);
			}
			int num;
			Socket.Bind_internal(this.socket, local_end.Serialize(), out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			if (num == 0)
			{
				this.isbound = true;
			}
			if (Environment.SocketSecurityEnabled)
			{
				Socket.current_bind_count++;
			}
			this.seed_endpoint = local_end;
		}

		/// <summary>Begins an asynchronous request for a remote host connection.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation. </returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentException">An argument is not valid. This exception occurs if multiple buffers are specified, the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property is not null. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter cannot be null and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is listening or a socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method. This exception also occurs if the local endpoint and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> are not the same address family.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		// Token: 0x0600234A RID: 9034 RVA: 0x00068784 File Offset: 0x00066984
		public bool ConnectAsync(SocketAsyncEventArgs e)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (this.islistening)
			{
				throw new InvalidOperationException("You may not perform this operation after calling the Listen method.");
			}
			if (e.RemoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEP", "Value cannot be null.");
			}
			if (e.BufferList != null)
			{
				throw new ArgumentException("Multiple buffers cannot be used with this method.");
			}
			e.DoOperation(SocketAsyncOperation.Connect, this);
			return true;
		}

		/// <summary>Establishes a connection to a remote host. The host is specified by an IP address and a port number.</summary>
		/// <param name="address">The IP address of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="address" /> is zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600234B RID: 9035 RVA: 0x00068808 File Offset: 0x00066A08
		public void Connect(IPAddress address, int port)
		{
			this.Connect(new IPEndPoint(address, port));
		}

		/// <summary>Establishes a connection to a remote host. The host is specified by an array of IP addresses and a port number.</summary>
		/// <param name="addresses">The IP addresses of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="addresses" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="address" /> is zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600234C RID: 9036 RVA: 0x00068818 File Offset: 0x00066A18
		public void Connect(IPAddress[] addresses, int port)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (addresses == null)
			{
				throw new ArgumentNullException("addresses");
			}
			if (this.AddressFamily != AddressFamily.InterNetwork && this.AddressFamily != AddressFamily.InterNetworkV6)
			{
				throw new NotSupportedException("This method is only valid for addresses in the InterNetwork or InterNetworkV6 families");
			}
			if (this.islistening)
			{
				throw new InvalidOperationException();
			}
			int num = 0;
			foreach (IPAddress address in addresses)
			{
				IPEndPoint ipendPoint = new IPEndPoint(address, port);
				SocketAddress sa = ipendPoint.Serialize();
				Socket.Connect_internal(this.socket, sa, out num);
				if (num == 0)
				{
					this.connected = true;
					this.seed_endpoint = ipendPoint;
					return;
				}
				if (num == 10036 || num == 10035)
				{
					if (!this.blocking)
					{
						this.Poll(-1, SelectMode.SelectWrite);
						num = (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Error);
						if (num == 0)
						{
							this.connected = true;
							this.seed_endpoint = ipendPoint;
							return;
						}
					}
				}
			}
			if (num != 0)
			{
				throw new SocketException(num);
			}
		}

		/// <summary>Establishes a connection to a remote host. The host is specified by a host name and a port number.</summary>
		/// <param name="host">The name of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600234D RID: 9037 RVA: 0x00068950 File Offset: 0x00066B50
		public void Connect(string host, int port)
		{
			IPAddress[] hostAddresses = Dns.GetHostAddresses(host);
			this.Connect(hostAddresses, port);
		}

		/// <summary>Begins an asynchronous request to disconnect from a remote endpoint.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. </exception>
		// Token: 0x0600234E RID: 9038 RVA: 0x0006896C File Offset: 0x00066B6C
		public bool DisconnectAsync(SocketAsyncEventArgs e)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			e.DoOperation(SocketAsyncOperation.Disconnect, this);
			return true;
		}

		// Token: 0x0600234F RID: 9039
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Disconnect_internal(IntPtr sock, bool reuse, out int error);

		/// <summary>Closes the socket connection and allows reuse of the socket.</summary>
		/// <param name="reuseSocket">true if this socket can be reused after the current connection is closed; otherwise, false. </param>
		/// <exception cref="T:System.PlatformNotSupportedException">This method requires Windows 2000 or earlier, or the exception will be thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002350 RID: 9040 RVA: 0x000689AC File Offset: 0x00066BAC
		public void Disconnect(bool reuseSocket)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			int num = 0;
			Socket.Disconnect_internal(this.socket, reuseSocket, out num);
			if (num == 0)
			{
				this.connected = false;
				if (reuseSocket)
				{
				}
				return;
			}
			if (num == 50)
			{
				throw new PlatformNotSupportedException();
			}
			throw new SocketException(num);
		}

		/// <summary>Duplicates the socket reference for the target process, and closes the socket for this process.</summary>
		/// <returns>The socket reference to be passed to the target process.</returns>
		/// <param name="targetProcessId">The ID of the target process where a duplicate of the socket reference is created.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="targetProcessID" /> is not a valid process id.-or- Duplication of the socket reference failed. </exception>
		// Token: 0x06002351 RID: 9041 RVA: 0x00068A18 File Offset: 0x00066C18
		[MonoTODO("Not implemented")]
		public SocketInformation DuplicateAndClose(int targetProcessId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.Socket" /> to handle remote host communication.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> to handle communication with the remote host.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information for this asynchronous operation as well as any user defined data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndAccept(System.IAsyncResult)" /> method was previously called. </exception>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002352 RID: 9042 RVA: 0x00068A20 File Offset: 0x00066C20
		public Socket EndAccept(IAsyncResult result)
		{
			byte[] array;
			int num;
			return this.EndAccept(out array, out num, result);
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.Socket" /> object to handle remote host communication. This method returns a buffer that contains the initial data transferred.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> object to handle communication with the remote host.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the bytes transferred. </param>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that stores state information for this asynchronous operation as well as any user defined data. </param>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is empty. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndAccept(System.IAsyncResult)" /> method was previously called. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the <see cref="T:System.Net.Sockets.Socket" /> See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002353 RID: 9043 RVA: 0x00068A38 File Offset: 0x00066C38
		public Socket EndAccept(out byte[] buffer, IAsyncResult asyncResult)
		{
			int num;
			return this.EndAccept(out buffer, out num, asyncResult);
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.Socket" /> object to handle remote host communication. This method returns a buffer that contains the initial data and the number of bytes transferred.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> object to handle communication with the remote host.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the bytes transferred. </param>
		/// <param name="bytesTransferred">The number of bytes transferred. </param>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that stores state information for this asynchronous operation as well as any user defined data. </param>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is empty. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndAccept(System.IAsyncResult)" /> method was previously called. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002354 RID: 9044 RVA: 0x00068A50 File Offset: 0x00066C50
		public Socket EndAccept(out byte[] buffer, out int bytesTransferred, IAsyncResult asyncResult)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket.SocketAsyncResult socketAsyncResult = asyncResult as Socket.SocketAsyncResult;
			if (socketAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			if (Interlocked.CompareExchange(ref socketAsyncResult.EndCalled, 1, 0) == 1)
			{
				throw this.InvalidAsyncOp("EndAccept");
			}
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			socketAsyncResult.CheckIfThrowDelayedException();
			buffer = socketAsyncResult.Buffer;
			bytesTransferred = socketAsyncResult.Total;
			return socketAsyncResult.Socket;
		}

		/// <summary>Ends a pending asynchronous connection request.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginConnect(System.Net.EndPoint,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndConnect(System.IAsyncResult)" /> was previously called for the asynchronous connection. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002355 RID: 9045 RVA: 0x00068B04 File Offset: 0x00066D04
		public void EndConnect(IAsyncResult result)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			Socket.SocketAsyncResult socketAsyncResult = result as Socket.SocketAsyncResult;
			if (socketAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "result");
			}
			if (Interlocked.CompareExchange(ref socketAsyncResult.EndCalled, 1, 0) == 1)
			{
				throw this.InvalidAsyncOp("EndConnect");
			}
			if (!result.IsCompleted)
			{
				result.AsyncWaitHandle.WaitOne();
			}
			socketAsyncResult.CheckIfThrowDelayedException();
		}

		/// <summary>Ends a pending asynchronous disconnect request.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that stores state information and any user-defined data for this asynchronous operation. </param>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows 2000 or earlier, and this method requires Windows XP. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginDisconnect(System.Boolean,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndDisconnect(System.IAsyncResult)" /> was previously called for the asynchronous connection. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.Net.WebException">The disconnect request has timed out. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002356 RID: 9046 RVA: 0x00068BA4 File Offset: 0x00066DA4
		public void EndDisconnect(IAsyncResult asyncResult)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket.SocketAsyncResult socketAsyncResult = asyncResult as Socket.SocketAsyncResult;
			if (socketAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			if (Interlocked.CompareExchange(ref socketAsyncResult.EndCalled, 1, 0) == 1)
			{
				throw this.InvalidAsyncOp("EndDisconnect");
			}
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			socketAsyncResult.CheckIfThrowDelayedException();
		}

		/// <summary>Ends a pending asynchronous read.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginReceive(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> was previously called for the asynchronous read. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002357 RID: 9047 RVA: 0x00068C44 File Offset: 0x00066E44
		public int EndReceive(IAsyncResult result)
		{
			SocketError socketError;
			return this.EndReceive(result, out socketError);
		}

		/// <summary>Ends a pending asynchronous read.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginReceive(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> was previously called for the asynchronous read. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002358 RID: 9048 RVA: 0x00068C5C File Offset: 0x00066E5C
		public int EndReceive(IAsyncResult asyncResult, out SocketError errorCode)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket.SocketAsyncResult socketAsyncResult = asyncResult as Socket.SocketAsyncResult;
			if (socketAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			if (Interlocked.CompareExchange(ref socketAsyncResult.EndCalled, 1, 0) == 1)
			{
				throw this.InvalidAsyncOp("EndReceive");
			}
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			errorCode = socketAsyncResult.ErrorCode;
			socketAsyncResult.CheckIfThrowDelayedException();
			return socketAsyncResult.Total;
		}

		/// <summary>Ends a pending asynchronous read from a specific endpoint.</summary>
		/// <returns>If successful, the number of bytes received. If unsuccessful, returns 0.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation. </param>
		/// <param name="endPoint">The source <see cref="T:System.Net.EndPoint" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginReceiveFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.Net.EndPoint@,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndReceiveFrom(System.IAsyncResult,System.Net.EndPoint@)" /> was previously called for the asynchronous read. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002359 RID: 9049 RVA: 0x00068D08 File Offset: 0x00066F08
		public int EndReceiveFrom(IAsyncResult result, ref EndPoint end_point)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			Socket.SocketAsyncResult socketAsyncResult = result as Socket.SocketAsyncResult;
			if (socketAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "result");
			}
			if (Interlocked.CompareExchange(ref socketAsyncResult.EndCalled, 1, 0) == 1)
			{
				throw this.InvalidAsyncOp("EndReceiveFrom");
			}
			if (!result.IsCompleted)
			{
				result.AsyncWaitHandle.WaitOne();
			}
			socketAsyncResult.CheckIfThrowDelayedException();
			end_point = socketAsyncResult.EndPoint;
			return socketAsyncResult.Total;
		}

		/// <summary>Ends a pending asynchronous read from a specific endpoint. This method also reveals more information about the packet than <see cref="M:System.Net.Sockets.Socket.EndReceiveFrom(System.IAsyncResult,System.Net.EndPoint@)" />.</summary>
		/// <returns>If successful, the number of bytes received. If unsuccessful, returns 0.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values for the received packet.</param>
		/// <param name="endPoint">The source <see cref="T:System.Net.EndPoint" />.</param>
		/// <param name="ipPacketInformation">The <see cref="T:System.Net.IPAddress" /> and interface of the received packet.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null-or- <paramref name="endPoint" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.Net.EndPoint@,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> was previously called for the asynchronous read. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x0600235A RID: 9050 RVA: 0x00068DB4 File Offset: 0x00066FB4
		[MonoTODO]
		public int EndReceiveMessageFrom(IAsyncResult asyncResult, ref SocketFlags socketFlags, ref EndPoint endPoint, out IPPacketInformation ipPacketInformation)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (endPoint == null)
			{
				throw new ArgumentNullException("endPoint");
			}
			Socket.SocketAsyncResult socketAsyncResult = asyncResult as Socket.SocketAsyncResult;
			if (socketAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			if (Interlocked.CompareExchange(ref socketAsyncResult.EndCalled, 1, 0) == 1)
			{
				throw this.InvalidAsyncOp("EndReceiveMessageFrom");
			}
			throw new NotImplementedException();
		}

		/// <summary>Ends a pending asynchronous send.</summary>
		/// <returns>If successful, the number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />; otherwise, an invalid <see cref="T:System.Net.Sockets.Socket" /> error.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information for this asynchronous operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSend(System.IAsyncResult)" /> was previously called for the asynchronous send. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600235B RID: 9051 RVA: 0x00068E4C File Offset: 0x0006704C
		public int EndSend(IAsyncResult result)
		{
			SocketError socketError;
			return this.EndSend(result, out socketError);
		}

		/// <summary>Ends a pending asynchronous send.</summary>
		/// <returns>If successful, the number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />; otherwise, an invalid <see cref="T:System.Net.Sockets.Socket" /> error.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information for this asynchronous operation.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSend(System.IAsyncResult)" /> was previously called for the asynchronous send. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x0600235C RID: 9052 RVA: 0x00068E64 File Offset: 0x00067064
		public int EndSend(IAsyncResult asyncResult, out SocketError errorCode)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket.SocketAsyncResult socketAsyncResult = asyncResult as Socket.SocketAsyncResult;
			if (socketAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "result");
			}
			if (Interlocked.CompareExchange(ref socketAsyncResult.EndCalled, 1, 0) == 1)
			{
				throw this.InvalidAsyncOp("EndSend");
			}
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			errorCode = socketAsyncResult.ErrorCode;
			socketAsyncResult.CheckIfThrowDelayedException();
			return socketAsyncResult.Total;
		}

		/// <summary>Ends a pending asynchronous send of a file.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that stores state information for this asynchronous operation. </param>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is empty. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSendFile(System.String,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSendFile(System.IAsyncResult)" /> was previously called for the asynchronous <see cref="M:System.Net.Sockets.Socket.BeginSendFile(System.String,System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600235D RID: 9053 RVA: 0x00068F10 File Offset: 0x00067110
		public void EndSendFile(IAsyncResult asyncResult)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket.SendFileAsyncResult sendFileAsyncResult = asyncResult as Socket.SendFileAsyncResult;
			if (sendFileAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			sendFileAsyncResult.Delegate.EndInvoke(sendFileAsyncResult.Original);
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x00068F84 File Offset: 0x00067184
		private Exception InvalidAsyncOp(string method)
		{
			return new InvalidOperationException(method + " can only be called once per asynchronous operation");
		}

		/// <summary>Ends a pending asynchronous send to a specific location.</summary>
		/// <returns>If successful, the number of bytes sent; otherwise, an invalid <see cref="T:System.Net.Sockets.Socket" /> error.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSendTo(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.Net.EndPoint,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSendTo(System.IAsyncResult)" /> was previously called for the asynchronous send. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600235F RID: 9055 RVA: 0x00068F98 File Offset: 0x00067198
		public int EndSendTo(IAsyncResult result)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			Socket.SocketAsyncResult socketAsyncResult = result as Socket.SocketAsyncResult;
			if (socketAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "result");
			}
			if (Interlocked.CompareExchange(ref socketAsyncResult.EndCalled, 1, 0) == 1)
			{
				throw this.InvalidAsyncOp("EndSendTo");
			}
			if (!result.IsCompleted)
			{
				result.AsyncWaitHandle.WaitOne();
			}
			socketAsyncResult.CheckIfThrowDelayedException();
			return socketAsyncResult.Total;
		}

		// Token: 0x06002360 RID: 9056
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSocketOption_arr_internal(IntPtr socket, SocketOptionLevel level, SocketOptionName name, ref byte[] byte_val, out int error);

		/// <summary>Returns the specified <see cref="T:System.Net.Sockets.Socket" /> option setting, represented as a byte array.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values. </param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values. </param>
		/// <param name="optionValue">An array of type <see cref="T:System.Byte" /> that is to receive the option setting. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. - or -In .NET Compact Framework applications, the Windows CE default buffer space is set to 32768 bytes. You can change the per socket buffer space by calling <see cref="Overload:System.Net.Sockets.Socket.SetSocketOption" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002361 RID: 9057 RVA: 0x0006903C File Offset: 0x0006723C
		public void GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, byte[] optionValue)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (optionValue == null)
			{
				throw new SocketException(10014, "Error trying to dereference an invalid pointer");
			}
			int num;
			Socket.GetSocketOption_arr_internal(this.socket, optionLevel, optionName, ref optionValue, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
		}

		/// <summary>Returns the value of the specified <see cref="T:System.Net.Sockets.Socket" /> option in an array.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" /> that contains the value of the socket option.</returns>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values. </param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values. </param>
		/// <param name="optionLength">The length, in bytes, of the expected return value. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. - or -In .NET Compact Framework applications, the Windows CE default buffer space is set to 32768 bytes. You can change the per socket buffer space by calling <see cref="Overload:System.Net.Sockets.Socket.SetSocketOption" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002362 RID: 9058 RVA: 0x000690A4 File Offset: 0x000672A4
		public byte[] GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int length)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			byte[] result = new byte[length];
			int num;
			Socket.GetSocketOption_arr_internal(this.socket, optionLevel, optionName, ref result, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			return result;
		}

		// Token: 0x06002363 RID: 9059
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int WSAIoctl(IntPtr sock, int ioctl_code, byte[] input, byte[] output, out int error);

		/// <summary>Sets low-level operating modes for the <see cref="T:System.Net.Sockets.Socket" /> using numerical control codes.</summary>
		/// <returns>The number of bytes in the <paramref name="optionOutValue" /> parameter.</returns>
		/// <param name="ioControlCode">An <see cref="T:System.Int32" /> value that specifies the control code of the operation to perform. </param>
		/// <param name="optionInValue">A <see cref="T:System.Byte" /> array that contains the input data required by the operation. </param>
		/// <param name="optionOutValue">A <see cref="T:System.Byte" /> array that contains the output data returned by the operation. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to change the blocking mode without using the <see cref="P:System.Net.Sockets.Socket.Blocking" /> property. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002364 RID: 9060 RVA: 0x00069100 File Offset: 0x00067300
		public int IOControl(int ioctl_code, byte[] in_value, byte[] out_value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			int num2;
			int num = Socket.WSAIoctl(this.socket, ioctl_code, in_value, out_value, out num2);
			if (num2 != 0)
			{
				throw new SocketException(num2);
			}
			if (num == -1)
			{
				throw new InvalidOperationException("Must use Blocking property instead.");
			}
			return num;
		}

		/// <summary>Sets low-level operating modes for the <see cref="T:System.Net.Sockets.Socket" /> using the <see cref="T:System.Net.Sockets.IOControlCode" /> enumeration to specify control codes.</summary>
		/// <returns>The number of bytes in the <paramref name="optionOutValue" /> parameter.</returns>
		/// <param name="ioControlCode">A <see cref="T:System.Net.Sockets.IOControlCode" /> value that specifies the control code of the operation to perform. </param>
		/// <param name="optionInValue">An array of type <see cref="T:System.Byte" /> that contains the input data required by the operation. </param>
		/// <param name="optionOutValue">An array of type <see cref="T:System.Byte" /> that contains the output data returned by the operation. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to change the blocking mode without using the <see cref="P:System.Net.Sockets.Socket.Blocking" /> property. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002365 RID: 9061 RVA: 0x0006915C File Offset: 0x0006735C
		[MonoTODO]
		public int IOControl(IOControlCode ioControlCode, byte[] optionInValue, byte[] optionOutValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002366 RID: 9062
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Listen_internal(IntPtr sock, int backlog, out int error);

		/// <summary>Places a <see cref="T:System.Net.Sockets.Socket" /> in a listening state.</summary>
		/// <param name="backlog">The maximum length of the pending connections queue. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002367 RID: 9063 RVA: 0x00069164 File Offset: 0x00067364
		public void Listen(int backlog)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (!this.isbound)
			{
				throw new SocketException(10022);
			}
			if (Environment.SocketSecurityEnabled)
			{
				SecurityException ex = new SecurityException("Listening on TCP sockets is not allowed in the webplayer");
				Console.WriteLine("Throwing the following securityexception: " + ex);
				throw ex;
			}
			int num;
			Socket.Listen_internal(this.socket, backlog, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			this.islistening = true;
		}

		/// <summary>Determines the status of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The status of the <see cref="T:System.Net.Sockets.Socket" /> based on the polling mode value passed in the <paramref name="mode" /> parameter.Mode Return Value <see cref="F:System.Net.Sockets.SelectMode.SelectRead" />true if <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> has been called and a connection is pending; -or- true if data is available for reading; -or- true if the connection has been closed, reset, or terminated; otherwise, returns false. <see cref="F:System.Net.Sockets.SelectMode.SelectWrite" />true, if processing a <see cref="M:System.Net.Sockets.Socket.Connect(System.Net.EndPoint)" />, and the connection has succeeded; -or- true if data can be sent; otherwise, returns false. <see cref="F:System.Net.Sockets.SelectMode.SelectError" />true if processing a <see cref="M:System.Net.Sockets.Socket.Connect(System.Net.EndPoint)" /> that does not block, and the connection has failed; -or- true if <see cref="F:System.Net.Sockets.SocketOptionName.OutOfBandInline" /> is not set and out-of-band data is available; otherwise, returns false. </returns>
		/// <param name="microSeconds">The time to wait for a response, in microseconds. </param>
		/// <param name="mode">One of the <see cref="T:System.Net.Sockets.SelectMode" /> values. </param>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="mode" /> parameter is not one of the <see cref="T:System.Net.Sockets.SelectMode" /> values. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks below. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002368 RID: 9064 RVA: 0x000691F8 File Offset: 0x000673F8
		public bool Poll(int time_us, SelectMode mode)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (mode != SelectMode.SelectRead && mode != SelectMode.SelectWrite && mode != SelectMode.SelectError)
			{
				throw new NotSupportedException("'mode' parameter is not valid.");
			}
			int num;
			bool flag = Socket.Poll_internal(this.socket, mode, time_us, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			if (mode == SelectMode.SelectWrite && flag && !this.connected && (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Error) == 0)
			{
				this.connected = true;
			}
			return flag;
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into a receive buffer.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002369 RID: 9065 RVA: 0x000692A4 File Offset: 0x000674A4
		public int Receive(byte[] buffer)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			SocketError socketError;
			int result = this.Receive_nochecks(buffer, 0, buffer.Length, SocketFlags.None, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException((int)socketError);
			}
			return result;
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into a receive buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600236A RID: 9066 RVA: 0x00069308 File Offset: 0x00067508
		public int Receive(byte[] buffer, SocketFlags flags)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			SocketError socketError;
			int result = this.Receive_nochecks(buffer, 0, buffer.Length, flags, out socketError);
			if (socketError == SocketError.Success)
			{
				return result;
			}
			if (socketError == SocketError.WouldBlock && this.blocking)
			{
				throw new SocketException((int)socketError, "Operation timed out.");
			}
			throw new SocketException((int)socketError);
		}

		/// <summary>Receives the specified number of bytes of data from a bound <see cref="T:System.Net.Sockets.Socket" /> into a receive buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> exceeds the size of <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600236B RID: 9067 RVA: 0x0006938C File Offset: 0x0006758C
		public int Receive(byte[] buffer, int size, SocketFlags flags)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (size < 0 || size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			SocketError socketError;
			int result = this.Receive_nochecks(buffer, 0, size, flags, out socketError);
			if (socketError == SocketError.Success)
			{
				return result;
			}
			if (socketError == SocketError.WouldBlock && this.blocking)
			{
				throw new SocketException((int)socketError, "Operation timed out.");
			}
			throw new SocketException((int)socketError);
		}

		/// <summary>Receives the specified number of bytes from a bound <see cref="T:System.Net.Sockets.Socket" /> into the specified offset position of the receive buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data. </param>
		/// <param name="offset">The location in <paramref name="buffer" /> to store the received data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property was not set.-or- An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600236C RID: 9068 RVA: 0x0006942C File Offset: 0x0006762C
		public int Receive(byte[] buffer, int offset, int size, SocketFlags flags)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			SocketError socketError;
			int result = this.Receive_nochecks(buffer, offset, size, flags, out socketError);
			if (socketError == SocketError.Success)
			{
				return result;
			}
			if (socketError == SocketError.WouldBlock && this.blocking)
			{
				throw new SocketException((int)socketError, "Operation timed out.");
			}
			throw new SocketException((int)socketError);
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into a receive buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="offset">The position in the <paramref name="buffer" /> parameter to store the received data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property is not set.-or- An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		// Token: 0x0600236D RID: 9069 RVA: 0x000694E8 File Offset: 0x000676E8
		public int Receive(byte[] buffer, int offset, int size, SocketFlags flags, out SocketError error)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return this.Receive_nochecks(buffer, offset, size, flags, out error);
		}

		// Token: 0x0600236E RID: 9070
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Receive_internal(IntPtr sock, Socket.WSABUF[] bufarray, SocketFlags flags, out int error);

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into the list of receive buffers.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the received data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x0600236F RID: 9071 RVA: 0x00069574 File Offset: 0x00067774
		public int Receive(IList<ArraySegment<byte>> buffers)
		{
			SocketError socketError;
			int result = this.Receive(buffers, SocketFlags.None, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException((int)socketError);
			}
			return result;
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into the list of receive buffers, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is null.-or-<paramref name="buffers" />.Count is zero.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002370 RID: 9072 RVA: 0x0006959C File Offset: 0x0006779C
		[CLSCompliant(false)]
		public int Receive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			SocketError socketError;
			int result = this.Receive(buffers, socketFlags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException((int)socketError);
			}
			return result;
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into the list of receive buffers, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is null.-or-<paramref name="buffers" />.Count is zero.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002371 RID: 9073 RVA: 0x000695C4 File Offset: 0x000677C4
		[CLSCompliant(false)]
		public int Receive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffers == null || buffers.Count == 0)
			{
				throw new ArgumentNullException("buffers");
			}
			int count = buffers.Count;
			Socket.WSABUF[] array = new Socket.WSABUF[count];
			GCHandle[] array2 = new GCHandle[count];
			for (int i = 0; i < count; i++)
			{
				ArraySegment<byte> arraySegment = buffers[i];
				array2[i] = GCHandle.Alloc(arraySegment.Array, GCHandleType.Pinned);
				array[i].len = arraySegment.Count;
				array[i].buf = Marshal.UnsafeAddrOfPinnedArrayElement(arraySegment.Array, arraySegment.Offset);
			}
			int num;
			int result;
			try
			{
				result = Socket.Receive_internal(this.socket, array, socketFlags, out num);
			}
			finally
			{
				for (int j = 0; j < count; j++)
				{
					if (array2[j].IsAllocated)
					{
						array2[j].Free();
					}
				}
			}
			errorCode = (SocketError)num;
			return result;
		}

		/// <summary>Begins to asynchronously receive data from a specified network device.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. </exception>
		// Token: 0x06002372 RID: 9074 RVA: 0x00069708 File Offset: 0x00067908
		public bool ReceiveFromAsync(SocketAsyncEventArgs e)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (e.BufferList != null)
			{
				throw new NotSupportedException("Mono doesn't support using BufferList at this point.");
			}
			if (e.RemoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEP", "Value cannot be null.");
			}
			e.DoOperation(SocketAsyncOperation.ReceiveFrom, this);
			return true;
		}

		/// <summary>Receives a datagram into the data buffer and stores the endpoint.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data. </param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002373 RID: 9075 RVA: 0x00069778 File Offset: 0x00067978
		public int ReceiveFrom(byte[] buffer, ref EndPoint remoteEP)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			return this.ReceiveFrom_nochecks(buffer, 0, buffer.Length, SocketFlags.None, ref remoteEP);
		}

		/// <summary>Receives a datagram into the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002374 RID: 9076 RVA: 0x000697DC File Offset: 0x000679DC
		public int ReceiveFrom(byte[] buffer, SocketFlags flags, ref EndPoint remoteEP)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			return this.ReceiveFrom_nochecks(buffer, 0, buffer.Length, flags, ref remoteEP);
		}

		/// <summary>Receives the specified number of bytes into the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property was not set.-or- An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002375 RID: 9077 RVA: 0x00069840 File Offset: 0x00067A40
		public int ReceiveFrom(byte[] buffer, int size, SocketFlags flags, ref EndPoint remoteEP)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			if (size < 0 || size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return this.ReceiveFrom_nochecks(buffer, 0, size, flags, ref remoteEP);
		}

		// Token: 0x06002376 RID: 9078
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RecvFrom_internal(IntPtr sock, byte[] buffer, int offset, int count, SocketFlags flags, ref SocketAddress sockaddr, out int error);

		/// <summary>Receives the specified number of bytes of data into the specified location of the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data. </param>
		/// <param name="offset">The position in the <paramref name="buffer" /> parameter to store the received data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of the <paramref name="buffer" /> minus the value of the offset parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property was not set.-or- An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002377 RID: 9079 RVA: 0x000698C0 File Offset: 0x00067AC0
		public int ReceiveFrom(byte[] buffer, int offset, int size, SocketFlags flags, ref EndPoint remoteEP)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return this.ReceiveFrom_nochecks(buffer, offset, size, flags, ref remoteEP);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x00069960 File Offset: 0x00067B60
		internal int ReceiveFrom_nochecks(byte[] buf, int offset, int size, SocketFlags flags, ref EndPoint remote_end)
		{
			int num;
			return this.ReceiveFrom_nochecks_exc(buf, offset, size, flags, ref remote_end, true, out num);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x00069980 File Offset: 0x00067B80
		internal int ReceiveFrom_nochecks_exc(byte[] buf, int offset, int size, SocketFlags flags, ref EndPoint remote_end, bool throwOnError, out int error)
		{
			SocketAddress socketAddress = remote_end.Serialize();
			int result = Socket.RecvFrom_internal(this.socket, buf, offset, size, flags, ref socketAddress, out error);
			SocketError socketError = (SocketError)error;
			if (socketError != SocketError.Success)
			{
				if (socketError != SocketError.WouldBlock && socketError != SocketError.InProgress)
				{
					this.connected = false;
				}
				else if (socketError == SocketError.WouldBlock && this.blocking)
				{
					if (throwOnError)
					{
						throw new SocketException(10060, "Operation timed out");
					}
					error = 10060;
					return 0;
				}
				if (throwOnError)
				{
					throw new SocketException(error);
				}
				return 0;
			}
			else
			{
				if (Environment.SocketSecurityEnabled && !Socket.CheckEndPoint(socketAddress))
				{
					buf.Initialize();
					throw new SecurityException("Unable to connect, as no valid crossdomain policy was found");
				}
				this.connected = true;
				this.isbound = true;
				if (socketAddress != null)
				{
					remote_end = remote_end.Create(socketAddress);
				}
				this.seed_endpoint = remote_end;
				return result;
			}
		}

		/// <summary>Begins to asynchronously receive the specified number of bytes of data into the specified location in the data buffer, using the specified <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.SocketFlags" />, and stores the endpoint and packet information.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. </exception>
		// Token: 0x0600237A RID: 9082 RVA: 0x00069A70 File Offset: 0x00067C70
		[MonoTODO("Not implemented")]
		public bool ReceiveMessageFromAsync(SocketAsyncEventArgs e)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			throw new NotImplementedException();
		}

		/// <summary>Receives the specified number of bytes of data into the specified location of the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint and packet information.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data.</param>
		/// <param name="offset">The position in the <paramref name="buffer" /> parameter to store the received data.</param>
		/// <param name="size">The number of bytes to receive.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server.</param>
		/// <param name="ipPacketInformation">An <see cref="T:System.Net.Sockets.IPPacketInformation" /> holding address and interface information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.- or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of the <paramref name="buffer" /> minus the value of the offset parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property was not set.-or- The .NET Framework is running on an AMD 64-bit processor.-or- An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows 2000 or earlier, and this method requires Windows XP.</exception>
		// Token: 0x0600237B RID: 9083 RVA: 0x00069AAC File Offset: 0x00067CAC
		[MonoTODO("Not implemented")]
		public int ReceiveMessageFrom(byte[] buffer, int offset, int size, ref SocketFlags socketFlags, ref EndPoint remoteEP, out IPPacketInformation ipPacketInformation)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			throw new NotImplementedException();
		}

		/// <summary>Sends a collection of files or in memory data buffers asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" /> object.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in the <see cref="P:System.Net.Sockets.SendPacketsElement.FilePath" /> property was not found. </exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method. This exception also occurs if the <see cref="T:System.Net.Sockets.Socket" /> is not connected to a remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">A connectionless <see cref="T:System.Net.Sockets.Socket" /> is being used and the file being sent exceeds the maximum packet size of the underlying transport.</exception>
		// Token: 0x0600237C RID: 9084 RVA: 0x00069B44 File Offset: 0x00067D44
		[MonoTODO("Not implemented")]
		public bool SendPacketsAsync(SocketAsyncEventArgs e)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			throw new NotImplementedException();
		}

		/// <summary>Sends data to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600237D RID: 9085 RVA: 0x00069B80 File Offset: 0x00067D80
		public int Send(byte[] buf)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buf == null)
			{
				throw new ArgumentNullException("buf");
			}
			SocketError socketError;
			int result = this.Send_nochecks(buf, 0, buf.Length, SocketFlags.None, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException((int)socketError);
			}
			return result;
		}

		/// <summary>Sends data to a connected <see cref="T:System.Net.Sockets.Socket" /> using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600237E RID: 9086 RVA: 0x00069BE4 File Offset: 0x00067DE4
		public int Send(byte[] buf, SocketFlags flags)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buf == null)
			{
				throw new ArgumentNullException("buf");
			}
			SocketError socketError;
			int result = this.Send_nochecks(buf, 0, buf.Length, flags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException((int)socketError);
			}
			return result;
		}

		/// <summary>Sends the specified number of bytes of data to a connected <see cref="T:System.Net.Sockets.Socket" />, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is less than 0 or exceeds the size of the buffer. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- An operating system error occurs while accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600237F RID: 9087 RVA: 0x00069C48 File Offset: 0x00067E48
		public int Send(byte[] buf, int size, SocketFlags flags)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buf == null)
			{
				throw new ArgumentNullException("buf");
			}
			if (size < 0 || size > buf.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			SocketError socketError;
			int result = this.Send_nochecks(buf, 0, size, flags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException((int)socketError);
			}
			return result;
		}

		/// <summary>Sends the specified number of bytes of data to a connected <see cref="T:System.Net.Sockets.Socket" />, starting at the specified offset, and using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="offset">The position in the data buffer at which to begin sending data. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002380 RID: 9088 RVA: 0x00069CC4 File Offset: 0x00067EC4
		public int Send(byte[] buf, int offset, int size, SocketFlags flags)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buf == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buf.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buf.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			SocketError socketError;
			int result = this.Send_nochecks(buf, offset, size, flags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException((int)socketError);
			}
			return result;
		}

		/// <summary>Sends the specified number of bytes of data to a connected <see cref="T:System.Net.Sockets.Socket" />, starting at the specified offset, and using the specified <see cref="T:System.Net.Sockets.SocketFlags" /></summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="offset">The position in the data buffer at which to begin sending data. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002381 RID: 9089 RVA: 0x00069D60 File Offset: 0x00067F60
		public int Send(byte[] buf, int offset, int size, SocketFlags flags, out SocketError error)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buf == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buf.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buf.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return this.Send_nochecks(buf, offset, size, flags, out error);
		}

		// Token: 0x06002382 RID: 9090
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Send_internal(IntPtr sock, Socket.WSABUF[] bufarray, SocketFlags flags, out int error);

		/// <summary>Sends the set of buffers in the list to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002383 RID: 9091 RVA: 0x00069DEC File Offset: 0x00067FEC
		public int Send(IList<ArraySegment<byte>> buffers)
		{
			SocketError socketError;
			int result = this.Send(buffers, SocketFlags.None, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException((int)socketError);
			}
			return result;
		}

		/// <summary>Sends the set of buffers in the list to a connected <see cref="T:System.Net.Sockets.Socket" />, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002384 RID: 9092 RVA: 0x00069E14 File Offset: 0x00068014
		public int Send(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			SocketError socketError;
			int result = this.Send(buffers, socketFlags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException((int)socketError);
			}
			return result;
		}

		/// <summary>Sends the set of buffers in the list to a connected <see cref="T:System.Net.Sockets.Socket" />, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002385 RID: 9093 RVA: 0x00069E3C File Offset: 0x0006803C
		[CLSCompliant(false)]
		public int Send(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			if (buffers.Count == 0)
			{
				throw new ArgumentException("Buffer is empty", "buffers");
			}
			int count = buffers.Count;
			Socket.WSABUF[] array = new Socket.WSABUF[count];
			GCHandle[] array2 = new GCHandle[count];
			for (int i = 0; i < count; i++)
			{
				ArraySegment<byte> arraySegment = buffers[i];
				array2[i] = GCHandle.Alloc(arraySegment.Array, GCHandleType.Pinned);
				array[i].len = arraySegment.Count;
				array[i].buf = Marshal.UnsafeAddrOfPinnedArrayElement(arraySegment.Array, arraySegment.Offset);
			}
			int num;
			int result;
			try
			{
				result = Socket.Send_internal(this.socket, array, socketFlags, out num);
			}
			finally
			{
				for (int j = 0; j < count; j++)
				{
					if (array2[j].IsAllocated)
					{
						array2[j].Free();
					}
				}
			}
			errorCode = (SocketError)num;
			return result;
		}

		// Token: 0x06002386 RID: 9094
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SendFile(IntPtr sock, string filename, byte[] pre_buffer, byte[] post_buffer, TransmitFileOptions flags);

		/// <summary>Sends the file <paramref name="fileName" /> to a connected <see cref="T:System.Net.Sockets.Socket" /> object with the <see cref="F:System.Net.Sockets.TransmitFileOptions.UseDefaultWorkerThread" /> transmit flag.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the path and name of the file to be sent. This parameter can be null. </param>
		/// <exception cref="T:System.NotSupportedException">The socket is not connected to a remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> object is not in blocking mode and cannot accept this synchronous call. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> was not found. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002387 RID: 9095 RVA: 0x00069F90 File Offset: 0x00068190
		public void SendFile(string fileName)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (!this.connected)
			{
				throw new NotSupportedException();
			}
			if (!this.blocking)
			{
				throw new InvalidOperationException();
			}
			this.SendFile(fileName, null, null, TransmitFileOptions.UseDefaultWorkerThread);
		}

		/// <summary>Sends the file <paramref name="fileName" /> and buffers of data to a connected <see cref="T:System.Net.Sockets.Socket" /> object using the specified <see cref="T:System.Net.Sockets.TransmitFileOptions" /> value.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the path and name of the file to be sent. This parameter can be null. </param>
		/// <param name="preBuffer">A <see cref="T:System.Byte" /> array that contains data to be sent before the file is sent. This parameter can be null. </param>
		/// <param name="postBuffer">A <see cref="T:System.Byte" /> array that contains data to be sent after the file is sent. This parameter can be null. </param>
		/// <param name="flags">One or more of <see cref="T:System.Net.Sockets.TransmitFileOptions" /> values. </param>
		/// <exception cref="T:System.NotSupportedException">The operating system is not Windows NT or later.- or - The socket is not connected to a remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> object is not in blocking mode and cannot accept this synchronous call. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> was not found. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002388 RID: 9096 RVA: 0x00069FF0 File Offset: 0x000681F0
		public void SendFile(string fileName, byte[] preBuffer, byte[] postBuffer, TransmitFileOptions flags)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (!this.connected)
			{
				throw new NotSupportedException();
			}
			if (!this.blocking)
			{
				throw new InvalidOperationException();
			}
			if (Socket.SendFile(this.socket, fileName, preBuffer, postBuffer, flags))
			{
				return;
			}
			SocketException ex = new SocketException();
			if (ex.ErrorCode == 2 || ex.ErrorCode == 3)
			{
				throw new FileNotFoundException();
			}
			throw ex;
		}

		/// <summary>Sends data asynchronously to a specific remote host.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">The protocol specified is connection-oriented, but the <see cref="T:System.Net.Sockets.Socket" /> is not yet connected.</exception>
		// Token: 0x06002389 RID: 9097 RVA: 0x0006A084 File Offset: 0x00068284
		public bool SendToAsync(SocketAsyncEventArgs e)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (e.RemoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEP", "Value cannot be null.");
			}
			e.DoOperation(SocketAsyncOperation.SendTo, this);
			return true;
		}

		/// <summary>Sends data to the specified endpoint.</summary>
		/// <returns>The number of bytes sent.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="remoteEP">The <see cref="T:System.Net.EndPoint" /> that represents the destination for the data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600238A RID: 9098 RVA: 0x0006A0E0 File Offset: 0x000682E0
		public int SendTo(byte[] buffer, EndPoint remote_end)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remote_end == null)
			{
				throw new ArgumentNullException("remote_end");
			}
			return this.SendTo_nochecks(buffer, 0, buffer.Length, SocketFlags.None, remote_end);
		}

		/// <summary>Sends data to a specific endpoint using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">The <see cref="T:System.Net.EndPoint" /> that represents the destination location for the data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600238B RID: 9099 RVA: 0x0006A144 File Offset: 0x00068344
		public int SendTo(byte[] buffer, SocketFlags flags, EndPoint remote_end)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remote_end == null)
			{
				throw new ArgumentNullException("remote_end");
			}
			return this.SendTo_nochecks(buffer, 0, buffer.Length, flags, remote_end);
		}

		/// <summary>Sends the specified number of bytes of data to the specified endpoint using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">The <see cref="T:System.Net.EndPoint" /> that represents the destination location for the data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="size" /> exceeds the size of <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600238C RID: 9100 RVA: 0x0006A1A8 File Offset: 0x000683A8
		public int SendTo(byte[] buffer, int size, SocketFlags flags, EndPoint remote_end)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remote_end == null)
			{
				throw new ArgumentNullException("remote_end");
			}
			if (size < 0 || size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return this.SendTo_nochecks(buffer, 0, size, flags, remote_end);
		}

		// Token: 0x0600238D RID: 9101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SendTo_internal_real(IntPtr sock, byte[] buffer, int offset, int count, SocketFlags flags, SocketAddress sa, out int error);

		// Token: 0x0600238E RID: 9102 RVA: 0x0006A228 File Offset: 0x00068428
		private static int SendTo_internal(IntPtr sock, byte[] buffer, int offset, int count, SocketFlags flags, SocketAddress sa, out int error)
		{
			if (Environment.SocketSecurityEnabled && !Socket.CheckEndPoint(sa))
			{
				SecurityException ex = new SecurityException("SendTo request refused by Unity webplayer security model");
				Console.WriteLine("Throwing the following security exception: " + ex);
				throw ex;
			}
			return Socket.SendTo_internal_real(sock, buffer, offset, count, flags, sa, out error);
		}

		/// <summary>Sends the specified number of bytes of data to the specified endpoint, starting at the specified location in the buffer, and using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="offset">The position in the data buffer at which to begin sending data. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">The <see cref="T:System.Net.EndPoint" /> that represents the destination location for the data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600238F RID: 9103 RVA: 0x0006A27C File Offset: 0x0006847C
		public int SendTo(byte[] buffer, int offset, int size, SocketFlags flags, EndPoint remote_end)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remote_end == null)
			{
				throw new ArgumentNullException("remote_end");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return this.SendTo_nochecks(buffer, offset, size, flags, remote_end);
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x0006A318 File Offset: 0x00068518
		internal int SendTo_nochecks(byte[] buffer, int offset, int size, SocketFlags flags, EndPoint remote_end)
		{
			SocketAddress sa = remote_end.Serialize();
			int num;
			int result = Socket.SendTo_internal(this.socket, buffer, offset, size, flags, sa, out num);
			SocketError socketError = (SocketError)num;
			if (socketError != SocketError.Success)
			{
				if (socketError != SocketError.WouldBlock && socketError != SocketError.InProgress)
				{
					this.connected = false;
				}
				throw new SocketException(num);
			}
			this.connected = true;
			this.isbound = true;
			this.seed_endpoint = remote_end;
			return result;
		}

		/// <summary>Sets the specified <see cref="T:System.Net.Sockets.Socket" /> option to the specified value, represented as a byte array.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values. </param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values. </param>
		/// <param name="optionValue">An array of type <see cref="T:System.Byte" /> that represents the value of the option. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002391 RID: 9105 RVA: 0x0006A384 File Offset: 0x00068584
		public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, byte[] optionValue)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (optionValue == null)
			{
				throw new SocketException(10014, "Error trying to dereference an invalid pointer");
			}
			int num;
			Socket.SetSocketOption_internal(this.socket, optionLevel, optionName, null, optionValue, 0, out num);
			if (num == 0)
			{
				return;
			}
			if (num == 10022)
			{
				throw new ArgumentException();
			}
			throw new SocketException(num);
		}

		/// <summary>Sets the specified <see cref="T:System.Net.Sockets.Socket" /> option to the specified value, represented as an object.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values. </param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values. </param>
		/// <param name="optionValue">A <see cref="T:System.Net.Sockets.LingerOption" /> or <see cref="T:System.Net.Sockets.MulticastOption" /> that contains the value of the option. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="optionValue" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002392 RID: 9106 RVA: 0x0006A400 File Offset: 0x00068600
		public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, object optionValue)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (optionValue == null)
			{
				throw new ArgumentNullException("optionValue");
			}
			int num;
			if (optionLevel == SocketOptionLevel.Socket && optionName == SocketOptionName.Linger)
			{
				LingerOption lingerOption = optionValue as LingerOption;
				if (lingerOption == null)
				{
					throw new ArgumentException("A 'LingerOption' value must be specified.", "optionValue");
				}
				Socket.SetSocketOption_internal(this.socket, optionLevel, optionName, lingerOption, null, 0, out num);
			}
			else if (optionLevel == SocketOptionLevel.IP && (optionName == SocketOptionName.AddMembership || optionName == SocketOptionName.DropMembership))
			{
				MulticastOption multicastOption = optionValue as MulticastOption;
				if (multicastOption == null)
				{
					throw new ArgumentException("A 'MulticastOption' value must be specified.", "optionValue");
				}
				Socket.SetSocketOption_internal(this.socket, optionLevel, optionName, multicastOption, null, 0, out num);
			}
			else
			{
				if (optionLevel != SocketOptionLevel.IPv6 || (optionName != SocketOptionName.AddMembership && optionName != SocketOptionName.DropMembership))
				{
					throw new ArgumentException("Invalid value specified.", "optionValue");
				}
				IPv6MulticastOption pv6MulticastOption = optionValue as IPv6MulticastOption;
				if (pv6MulticastOption == null)
				{
					throw new ArgumentException("A 'IPv6MulticastOption' value must be specified.", "optionValue");
				}
				Socket.SetSocketOption_internal(this.socket, optionLevel, optionName, pv6MulticastOption, null, 0, out num);
			}
			if (num == 0)
			{
				return;
			}
			if (num == 10022)
			{
				throw new ArgumentException();
			}
			throw new SocketException(num);
		}

		/// <summary>Sets the specified <see cref="T:System.Net.Sockets.Socket" /> option to the specified <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values. </param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values. </param>
		/// <param name="optionValue">The value of the option, represented as a <see cref="T:System.Boolean" />. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002393 RID: 9107 RVA: 0x0006A554 File Offset: 0x00068754
		public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, bool optionValue)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			int int_val = (!optionValue) ? 0 : 1;
			int num;
			Socket.SetSocketOption_internal(this.socket, optionLevel, optionName, null, null, int_val, out num);
			if (num == 0)
			{
				return;
			}
			if (num == 10022)
			{
				throw new ArgumentException();
			}
			throw new SocketException(num);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x0006A5C8 File Offset: 0x000687C8
		internal static void CheckProtocolSupport()
		{
			if (Socket.ipv4Supported == -1)
			{
				try
				{
					Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					socket.Close();
					Socket.ipv4Supported = 1;
				}
				catch
				{
					Socket.ipv4Supported = 0;
				}
			}
			if (Socket.ipv6Supported == -1)
			{
				System.Net.Configuration.SettingsSection settingsSection = (System.Net.Configuration.SettingsSection)ConfigurationManager.GetSection("system.net/settings");
				if (settingsSection != null)
				{
					Socket.ipv6Supported = ((!settingsSection.Ipv6.Enabled) ? 0 : -1);
				}
				if (Socket.ipv6Supported != 0)
				{
					try
					{
						Socket socket2 = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
						socket2.Close();
						Socket.ipv6Supported = 1;
					}
					catch
					{
						Socket.ipv6Supported = 0;
					}
				}
			}
		}

		/// <summary>Gets a value indicating whether IPv4 support is available and enabled on the current host.</summary>
		/// <returns>true if the current host supports the IPv4 protocol; otherwise, false.</returns>
		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x0006A6A8 File Offset: 0x000688A8
		public static bool SupportsIPv4
		{
			get
			{
				Socket.CheckProtocolSupport();
				return Socket.ipv4Supported == 1;
			}
		}

		/// <summary>Gets a value that indicates whether the Framework supports IPv6 for certain obsolete <see cref="T:System.Net.Dns" /> members.</summary>
		/// <returns>true if the Framework supports IPv6 for certain obsolete <see cref="T:System.Net.Dns" /> methods; otherwise, false.</returns>
		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x0006A6B8 File Offset: 0x000688B8
		[Obsolete("Use OSSupportsIPv6 instead")]
		public static bool SupportsIPv6
		{
			get
			{
				Socket.CheckProtocolSupport();
				return Socket.ipv6Supported == 1;
			}
		}

		/// <summary>Indicates whether the underlying operating system and network adaptors support Internet Protocol version 6 (IPv6).</summary>
		/// <returns>true if the operating system and network adaptors support the IPv6 protocol; otherwise, false.</returns>
		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x0006A6C8 File Offset: 0x000688C8
		public static bool OSSupportsIPv6
		{
			get
			{
				System.Net.NetworkInformation.NetworkInterface[] allNetworkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
				foreach (System.Net.NetworkInformation.NetworkInterface networkInterface in allNetworkInterfaces)
				{
					if (networkInterface.Supports(System.Net.NetworkInformation.NetworkInterfaceComponent.IPv6))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06002398 RID: 9112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr Socket_internal(AddressFamily family, SocketType type, ProtocolType proto, out int error);

		/// <summary>Frees resources used by the <see cref="T:System.Net.Sockets.Socket" /> class.</summary>
		// Token: 0x06002399 RID: 9113 RVA: 0x0006A704 File Offset: 0x00068904
		~Socket()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the address family of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values.</returns>
		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x0006A740 File Offset: 0x00068940
		public AddressFamily AddressFamily
		{
			get
			{
				return this.address_family;
			}
		}

		// Token: 0x0600239B RID: 9115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blocking_internal(IntPtr socket, bool block, out int error);

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Net.Sockets.Socket" /> is in blocking mode.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> will block; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x0006A748 File Offset: 0x00068948
		// (set) Token: 0x0600239D RID: 9117 RVA: 0x0006A750 File Offset: 0x00068950
		public bool Blocking
		{
			get
			{
				return this.blocking;
			}
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				int num;
				Socket.Blocking_internal(this.socket, value, out num);
				if (num != 0)
				{
					throw new SocketException(num);
				}
				this.blocking = value;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Net.Sockets.Socket" /> is connected to a remote host as of the last <see cref="Overload:System.Net.Sockets.Socket.Send" /> or <see cref="Overload:System.Net.Sockets.Socket.Receive" /> operation.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> was connected to a remote resource as of the most recent operation; otherwise, false.</returns>
		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x0600239E RID: 9118 RVA: 0x0006A7A8 File Offset: 0x000689A8
		// (set) Token: 0x0600239F RID: 9119 RVA: 0x0006A7B0 File Offset: 0x000689B0
		public bool Connected
		{
			get
			{
				return this.connected;
			}
			internal set
			{
				this.connected = value;
			}
		}

		/// <summary>Gets the protocol type of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.ProtocolType" /> values.</returns>
		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x0006A7BC File Offset: 0x000689BC
		public ProtocolType ProtocolType
		{
			get
			{
				return this.protocol_type;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the stream <see cref="T:System.Net.Sockets.Socket" /> is using the Nagle algorithm.</summary>
		/// <returns>false if the <see cref="T:System.Net.Sockets.Socket" /> uses the Nagle algorithm; otherwise, true. The default is false.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x0006A7C4 File Offset: 0x000689C4
		// (set) Token: 0x060023A2 RID: 9122 RVA: 0x0006A814 File Offset: 0x00068A14
		public bool NoDelay
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				this.ThrowIfUpd();
				return (int)this.GetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.Debug) != 0;
			}
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				this.ThrowIfUpd();
				this.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.Debug, (!value) ? 0 : 1);
			}
		}

		/// <summary>Gets or sets a value that specifies the size of the receive buffer of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the size, in bytes, of the receive buffer. The default is 8192.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than 0.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x0006A864 File Offset: 0x00068A64
		// (set) Token: 0x060023A4 RID: 9124 RVA: 0x0006A8B0 File Offset: 0x00068AB0
		public int ReceiveBufferSize
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer);
			}
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "The value specified for a set operation is less than zero");
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, value);
			}
		}

		/// <summary>Gets or sets a value that specifies the size of the send buffer of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the size, in bytes, of the send buffer. The default is 8192.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than 0.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x0006A90C File Offset: 0x00068B0C
		// (set) Token: 0x060023A6 RID: 9126 RVA: 0x0006A958 File Offset: 0x00068B58
		public int SendBufferSize
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer);
			}
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "The value specified for a set operation is less than zero");
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, value);
			}
		}

		/// <summary>Gets or sets a value that specifies the Time To Live (TTL) value of Internet Protocol (IP) packets sent by the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The TTL value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The TTL value can't be set to a negative number.</exception>
		/// <exception cref="T:System.NotSupportedException">This property can only be retrieved or set for a socket in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> address family.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. This error is also returned when an attempt was made to set TTL to a value higher than 255.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x0006A9B4 File Offset: 0x00068BB4
		// (set) Token: 0x060023A8 RID: 9128 RVA: 0x0006AA38 File Offset: 0x00068C38
		public short Ttl
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				short result;
				if (this.address_family == AddressFamily.InterNetwork)
				{
					result = (short)((int)this.GetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress));
				}
				else
				{
					if (this.address_family != AddressFamily.InterNetworkV6)
					{
						throw new NotSupportedException("This property is only valid for InterNetwork and InterNetworkV6 sockets");
					}
					result = (short)((int)this.GetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.HopLimit));
				}
				return result;
			}
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.address_family == AddressFamily.InterNetwork)
				{
					this.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, (int)value);
				}
				else
				{
					if (this.address_family != AddressFamily.InterNetworkV6)
					{
						throw new NotSupportedException("This property is only valid for InterNetwork and InterNetworkV6 sockets");
					}
					this.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.HopLimit, (int)value);
				}
			}
		}

		// Token: 0x060023A9 RID: 9129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SocketAddress RemoteEndPoint_internal(IntPtr socket, out int error);

		/// <summary>Gets the remote endpoint.</summary>
		/// <returns>The <see cref="T:System.Net.EndPoint" /> with which the <see cref="T:System.Net.Sockets.Socket" /> is communicating.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x0006AAB0 File Offset: 0x00068CB0
		public EndPoint RemoteEndPoint
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.seed_endpoint == null)
				{
					return null;
				}
				int num;
				SocketAddress address = Socket.RemoteEndPoint_internal(this.socket, out num);
				if (num != 0)
				{
					throw new SocketException(num);
				}
				return this.seed_endpoint.Create(address);
			}
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x0006AB18 File Offset: 0x00068D18
		private void Linger(IntPtr handle)
		{
			if (!this.connected || this.linger_timeout <= 0)
			{
				return;
			}
			int num;
			Socket.Shutdown_internal(handle, SocketShutdown.Receive, out num);
			if (num != 0)
			{
				return;
			}
			int num2 = this.linger_timeout / 1000;
			int num3 = this.linger_timeout % 1000;
			if (num3 > 0)
			{
				Socket.Poll_internal(handle, SelectMode.SelectRead, num3 * 1000, out num);
				if (num != 0)
				{
					return;
				}
			}
			if (num2 > 0)
			{
				LingerOption obj_val = new LingerOption(true, num2);
				Socket.SetSocketOption_internal(handle, SocketOptionLevel.Socket, SocketOptionName.Linger, obj_val, null, 0, out num);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.Socket" />, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources. </param>
		// Token: 0x060023AC RID: 9132 RVA: 0x0006ABAC File Offset: 0x00068DAC
		protected virtual void Dispose(bool explicitDisposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			bool flag = this.connected;
			this.connected = false;
			if ((int)this.socket != -1)
			{
				if (Environment.SocketSecurityEnabled && Socket.current_bind_count > 0)
				{
					Socket.current_bind_count--;
				}
				this.closed = true;
				IntPtr handle = this.socket;
				this.socket = (IntPtr)(-1);
				Thread thread = this.blocking_thread;
				if (thread != null)
				{
					thread.Abort();
					this.blocking_thread = null;
				}
				if (flag)
				{
					this.Linger(handle);
				}
				int num;
				Socket.Close_internal(handle, out num);
				if (num != 0)
				{
					throw new SocketException(num);
				}
			}
		}

		// Token: 0x060023AD RID: 9133
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Close_internal(IntPtr socket, out int error);

		/// <summary>Closes the <see cref="T:System.Net.Sockets.Socket" /> connection and releases all associated resources.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060023AE RID: 9134 RVA: 0x0006AC64 File Offset: 0x00068E64
		public void Close()
		{
			this.linger_timeout = 0;
			((IDisposable)this).Dispose();
		}

		/// <summary>Closes the <see cref="T:System.Net.Sockets.Socket" /> connection and releases all associated resources with a specified timeout to allow queued data to be sent.</summary>
		/// <param name="timeout">Wait up to <paramref name="timeout" /> seconds to send any remaining data, then close the socket.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060023AF RID: 9135 RVA: 0x0006AC74 File Offset: 0x00068E74
		public void Close(int timeout)
		{
			this.linger_timeout = timeout;
			((IDisposable)this).Dispose();
		}

		// Token: 0x060023B0 RID: 9136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Connect_internal_real(IntPtr sock, SocketAddress sa, out int error);

		// Token: 0x060023B1 RID: 9137 RVA: 0x0006AC84 File Offset: 0x00068E84
		private static void Connect_internal(IntPtr sock, SocketAddress sa, out int error)
		{
			Socket.Connect_internal(sock, sa, out error, true);
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x0006AC90 File Offset: 0x00068E90
		private static void Connect_internal(IntPtr sock, SocketAddress sa, out int error, bool requireSocketPolicyFile)
		{
			if (requireSocketPolicyFile && !Socket.CheckEndPoint(sa))
			{
				throw new SecurityException("Unable to connect, as no valid crossdomain policy was found");
			}
			Socket.Connect_internal_real(sock, sa, out error);
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x0006ACC4 File Offset: 0x00068EC4
		internal static bool CheckEndPoint(SocketAddress sa)
		{
			if (!Environment.SocketSecurityEnabled)
			{
				return true;
			}
			bool result;
			try
			{
				IPEndPoint ipendPoint = new IPEndPoint(IPAddress.Loopback, 123);
				IPEndPoint ipendPoint2 = (IPEndPoint)ipendPoint.Create(sa);
				if (Socket.check_socket_policy == null)
				{
					Socket.check_socket_policy = Socket.GetUnityCrossDomainHelperMethod("CheckSocketEndPoint");
				}
				result = (bool)Socket.check_socket_policy.Invoke(null, new object[]
				{
					ipendPoint2.Address.ToString(),
					ipendPoint2.Port
				});
			}
			catch (Exception arg)
			{
				Console.WriteLine("Unexpected error while trying to CheckEndPoint() : " + arg);
				result = false;
			}
			return result;
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x0006AD88 File Offset: 0x00068F88
		private static MethodInfo GetUnityCrossDomainHelperMethod(string methodname)
		{
			Type type = Type.GetType("UnityEngine.UnityCrossDomainHelper, CrossDomainPolicyParser, Version=1.0.0.0, Culture=neutral");
			if (type == null)
			{
				throw new SecurityException("Cant find type UnityCrossDomainHelper");
			}
			MethodInfo method = type.GetMethod(methodname);
			if (method == null)
			{
				throw new SecurityException("Cant find " + methodname);
			}
			return method;
		}

		/// <summary>Establishes a connection to a remote host.</summary>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the remote device. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060023B5 RID: 9141 RVA: 0x0006ADD4 File Offset: 0x00068FD4
		public void Connect(EndPoint remoteEP)
		{
			this.Connect(remoteEP, true);
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x0006ADE0 File Offset: 0x00068FE0
		internal void Connect(EndPoint remoteEP, bool requireSocketPolicy)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			IPEndPoint ipendPoint = remoteEP as IPEndPoint;
			if (ipendPoint != null && (ipendPoint.Address.Equals(IPAddress.Any) || ipendPoint.Address.Equals(IPAddress.IPv6Any)))
			{
				throw new SocketException(10049);
			}
			if (this.islistening)
			{
				throw new InvalidOperationException();
			}
			SocketAddress sa = remoteEP.Serialize();
			int num = 0;
			this.blocking_thread = Thread.CurrentThread;
			try
			{
				Socket.Connect_internal(this.socket, sa, out num, requireSocketPolicy);
			}
			catch (ThreadAbortException)
			{
				if (this.disposed)
				{
					Thread.ResetAbort();
					num = 10004;
				}
			}
			finally
			{
				this.blocking_thread = null;
			}
			if (num != 0)
			{
				throw new SocketException(num);
			}
			this.connected = true;
			this.isbound = true;
			this.seed_endpoint = remoteEP;
		}

		/// <summary>Begins an asynchronous request to receive data from a connected <see cref="T:System.Net.Sockets.Socket" /> object.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentException">An argument was invalid. The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> or <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> properties on the <paramref name="e" /> parameter must reference valid buffers. One or the other of these properties may be set, but not both at the same time.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		// Token: 0x060023B7 RID: 9143 RVA: 0x0006AF1C File Offset: 0x0006911C
		public bool ReceiveAsync(SocketAsyncEventArgs e)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (e.BufferList != null)
			{
				throw new NotSupportedException("Mono doesn't support using BufferList at this point.");
			}
			e.DoOperation(SocketAsyncOperation.Receive, this);
			return true;
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" /> object.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> or <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> properties on the <paramref name="e" /> parameter must reference valid buffers. One or the other of these properties may be set, but not both at the same time.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">The <see cref="T:System.Net.Sockets.Socket" /> is not yet connected or was not obtained via an <see cref="M:System.Net.Sockets.Socket.Accept" />, <see cref="M:System.Net.Sockets.Socket.AcceptAsync(System.Net.Sockets.SocketAsyncEventArgs)" />,or <see cref="Overload:System.Net.Sockets.Socket.BeginAccept" />, method.</exception>
		// Token: 0x060023B8 RID: 9144 RVA: 0x0006AF70 File Offset: 0x00069170
		public bool SendAsync(SocketAsyncEventArgs e)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (e.Buffer == null && e.BufferList == null)
			{
				throw new ArgumentException("Either e.Buffer or e.BufferList must be valid buffers.");
			}
			e.DoOperation(SocketAsyncOperation.Send, this);
			return true;
		}

		// Token: 0x060023B9 RID: 9145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Poll_internal(IntPtr socket, SelectMode mode, int timeout, out int error);

		// Token: 0x060023BA RID: 9146 RVA: 0x0006AFD0 File Offset: 0x000691D0
		internal bool Poll(int time_us, SelectMode mode, out int socket_error)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (mode != SelectMode.SelectRead && mode != SelectMode.SelectWrite && mode != SelectMode.SelectError)
			{
				throw new NotSupportedException("'mode' parameter is not valid.");
			}
			int num;
			bool flag = Socket.Poll_internal(this.socket, mode, time_us, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			socket_error = (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Error);
			if (mode == SelectMode.SelectWrite && flag && socket_error == 0)
			{
				this.connected = true;
			}
			return flag;
		}

		// Token: 0x060023BB RID: 9147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Receive_internal(IntPtr sock, byte[] buffer, int offset, int count, SocketFlags flags, out int error);

		// Token: 0x060023BC RID: 9148 RVA: 0x0006B074 File Offset: 0x00069274
		internal int Receive_nochecks(byte[] buf, int offset, int size, SocketFlags flags, out SocketError error)
		{
			if (this.protocol_type == ProtocolType.Udp)
			{
				EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
				int num = 0;
				int result = this.ReceiveFrom_nochecks_exc(buf, offset, size, flags, ref endPoint, false, out num);
				error = (SocketError)num;
				return result;
			}
			int num2;
			int result2 = Socket.Receive_internal(this.socket, buf, offset, size, flags, out num2);
			error = (SocketError)num2;
			if (error != SocketError.Success && error != SocketError.WouldBlock && error != SocketError.InProgress)
			{
				this.connected = false;
			}
			else
			{
				this.connected = true;
			}
			return result2;
		}

		// Token: 0x060023BD RID: 9149
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSocketOption_obj_internal(IntPtr socket, SocketOptionLevel level, SocketOptionName name, out object obj_val, out int error);

		// Token: 0x060023BE RID: 9150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Send_internal(IntPtr sock, byte[] buf, int offset, int count, SocketFlags flags, out int error);

		// Token: 0x060023BF RID: 9151 RVA: 0x0006B104 File Offset: 0x00069304
		internal int Send_nochecks(byte[] buf, int offset, int size, SocketFlags flags, out SocketError error)
		{
			if (size == 0)
			{
				error = SocketError.Success;
				return 0;
			}
			int num;
			int result = Socket.Send_internal(this.socket, buf, offset, size, flags, out num);
			error = (SocketError)num;
			if (error != SocketError.Success && error != SocketError.WouldBlock && error != SocketError.InProgress)
			{
				this.connected = false;
			}
			else
			{
				this.connected = true;
			}
			return result;
		}

		/// <summary>Returns the value of a specified <see cref="T:System.Net.Sockets.Socket" /> option, represented as an object.</summary>
		/// <returns>An object that represents the value of the option. When the <paramref name="optionName" /> parameter is set to <see cref="F:System.Net.Sockets.SocketOptionName.Linger" /> the return value is an instance of the <see cref="T:System.Net.Sockets.LingerOption" /> class. When <paramref name="optionName" /> is set to <see cref="F:System.Net.Sockets.SocketOptionName.AddMembership" /> or <see cref="F:System.Net.Sockets.SocketOptionName.DropMembership" />, the return value is an instance of the <see cref="T:System.Net.Sockets.MulticastOption" /> class. When <paramref name="optionName" /> is any other value, the return value is an integer.</returns>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values. </param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information.-or-<paramref name="optionName" /> was set to the unsupported value <see cref="F:System.Net.Sockets.SocketOptionName.MaxConnections" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060023C0 RID: 9152 RVA: 0x0006B16C File Offset: 0x0006936C
		public object GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			object obj;
			int num;
			Socket.GetSocketOption_obj_internal(this.socket, optionLevel, optionName, out obj, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			if (optionName == SocketOptionName.Linger)
			{
				return (LingerOption)obj;
			}
			if (optionName == SocketOptionName.AddMembership || optionName == SocketOptionName.DropMembership)
			{
				return (MulticastOption)obj;
			}
			if (obj is int)
			{
				return (int)obj;
			}
			return obj;
		}

		// Token: 0x060023C1 RID: 9153
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Shutdown_internal(IntPtr socket, SocketShutdown how, out int error);

		/// <summary>Disables sends and receives on a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="how">One of the <see cref="T:System.Net.Sockets.SocketShutdown" /> values that specifies the operation that will no longer be allowed. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060023C2 RID: 9154 RVA: 0x0006B200 File Offset: 0x00069400
		public void Shutdown(SocketShutdown how)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (!this.connected)
			{
				throw new SocketException(10057);
			}
			int num;
			Socket.Shutdown_internal(this.socket, how, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
		}

		// Token: 0x060023C3 RID: 9155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSocketOption_internal(IntPtr socket, SocketOptionLevel level, SocketOptionName name, object obj_val, byte[] byte_val, int int_val, out int error);

		/// <summary>Sets the specified <see cref="T:System.Net.Sockets.Socket" /> option to the specified integer value.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values. </param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values. </param>
		/// <param name="optionValue">A value of the option. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060023C4 RID: 9156 RVA: 0x0006B268 File Offset: 0x00069468
		public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionValue)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			int num;
			Socket.SetSocketOption_internal(this.socket, optionLevel, optionName, null, null, optionValue, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x0006B2BC File Offset: 0x000694BC
		private void ThrowIfUpd()
		{
			if (this.protocol_type == ProtocolType.Udp)
			{
				throw new SocketException(10042);
			}
		}

		// Token: 0x040015CD RID: 5581
		private Queue readQ;

		// Token: 0x040015CE RID: 5582
		private Queue writeQ;

		// Token: 0x040015CF RID: 5583
		private bool islistening;

		// Token: 0x040015D0 RID: 5584
		private bool useoverlappedIO;

		// Token: 0x040015D1 RID: 5585
		private readonly int MinListenPort;

		// Token: 0x040015D2 RID: 5586
		private readonly int MaxListenPort;

		// Token: 0x040015D3 RID: 5587
		private static int ipv4Supported = -1;

		// Token: 0x040015D4 RID: 5588
		private static int ipv6Supported = -1;

		// Token: 0x040015D5 RID: 5589
		private int linger_timeout;

		// Token: 0x040015D6 RID: 5590
		private IntPtr socket;

		// Token: 0x040015D7 RID: 5591
		private AddressFamily address_family;

		// Token: 0x040015D8 RID: 5592
		private SocketType socket_type;

		// Token: 0x040015D9 RID: 5593
		private ProtocolType protocol_type;

		// Token: 0x040015DA RID: 5594
		internal bool blocking = true;

		// Token: 0x040015DB RID: 5595
		private Thread blocking_thread;

		// Token: 0x040015DC RID: 5596
		private bool isbound;

		// Token: 0x040015DD RID: 5597
		private static int current_bind_count;

		// Token: 0x040015DE RID: 5598
		private readonly int max_bind_count = 50;

		// Token: 0x040015DF RID: 5599
		private bool connected;

		// Token: 0x040015E0 RID: 5600
		private bool closed;

		// Token: 0x040015E1 RID: 5601
		internal bool disposed;

		// Token: 0x040015E2 RID: 5602
		internal EndPoint seed_endpoint;

		// Token: 0x040015E3 RID: 5603
		private static MethodInfo check_socket_policy;

		// Token: 0x020003F7 RID: 1015
		private enum SocketOperation
		{
			// Token: 0x040015E5 RID: 5605
			Accept,
			// Token: 0x040015E6 RID: 5606
			Connect,
			// Token: 0x040015E7 RID: 5607
			Receive,
			// Token: 0x040015E8 RID: 5608
			ReceiveFrom,
			// Token: 0x040015E9 RID: 5609
			Send,
			// Token: 0x040015EA RID: 5610
			SendTo,
			// Token: 0x040015EB RID: 5611
			UsedInManaged1,
			// Token: 0x040015EC RID: 5612
			UsedInManaged2,
			// Token: 0x040015ED RID: 5613
			UsedInProcess,
			// Token: 0x040015EE RID: 5614
			UsedInConsole2,
			// Token: 0x040015EF RID: 5615
			Disconnect,
			// Token: 0x040015F0 RID: 5616
			AcceptReceive,
			// Token: 0x040015F1 RID: 5617
			ReceiveGeneric,
			// Token: 0x040015F2 RID: 5618
			SendGeneric
		}

		// Token: 0x020003F8 RID: 1016
		private struct WSABUF
		{
			// Token: 0x040015F3 RID: 5619
			public int len;

			// Token: 0x040015F4 RID: 5620
			public IntPtr buf;
		}

		// Token: 0x020003F9 RID: 1017
		[StructLayout(LayoutKind.Sequential)]
		private sealed class SocketAsyncResult : IAsyncResult
		{
			// Token: 0x060023C6 RID: 9158 RVA: 0x0006B2D8 File Offset: 0x000694D8
			public SocketAsyncResult(Socket sock, object state, AsyncCallback callback, Socket.SocketOperation operation)
			{
				this.Sock = sock;
				this.blocking = sock.blocking;
				this.handle = sock.socket;
				this.state = state;
				this.callback = callback;
				this.operation = operation;
				this.SockFlags = SocketFlags.None;
			}

			// Token: 0x060023C7 RID: 9159 RVA: 0x0006B328 File Offset: 0x00069528
			public void CheckIfThrowDelayedException()
			{
				if (this.delayedException != null)
				{
					this.Sock.connected = false;
					throw this.delayedException;
				}
				if (this.error != 0)
				{
					this.Sock.connected = false;
					throw new SocketException(this.error);
				}
			}

			// Token: 0x060023C8 RID: 9160 RVA: 0x0006B378 File Offset: 0x00069578
			private void CompleteAllOnDispose(Queue queue)
			{
				object[] array = queue.ToArray();
				queue.Clear();
				foreach (Socket.SocketAsyncResult @object in array)
				{
					WaitCallback callBack = new WaitCallback(@object.CompleteDisposed);
					ThreadPool.QueueUserWorkItem(callBack, null);
				}
				if (array.Length == 0)
				{
					this.Buffer = null;
				}
			}

			// Token: 0x060023C9 RID: 9161 RVA: 0x0006B3D4 File Offset: 0x000695D4
			private void CompleteDisposed(object unused)
			{
				this.Complete();
			}

			// Token: 0x060023CA RID: 9162 RVA: 0x0006B3DC File Offset: 0x000695DC
			public void Complete()
			{
				if (this.operation != Socket.SocketOperation.Receive && this.Sock.disposed)
				{
					this.delayedException = new ObjectDisposedException(this.Sock.GetType().ToString());
				}
				this.IsCompleted = true;
				Queue queue = null;
				if (this.operation == Socket.SocketOperation.Receive || this.operation == Socket.SocketOperation.ReceiveFrom || this.operation == Socket.SocketOperation.ReceiveGeneric)
				{
					queue = this.Sock.readQ;
				}
				else if (this.operation == Socket.SocketOperation.Send || this.operation == Socket.SocketOperation.SendTo || this.operation == Socket.SocketOperation.SendGeneric)
				{
					queue = this.Sock.writeQ;
				}
				if (queue != null)
				{
					Socket.SocketAsyncCall socketAsyncCall = null;
					Socket.SocketAsyncResult socketAsyncResult = null;
					Queue obj = queue;
					lock (obj)
					{
						queue.Dequeue();
						if (queue.Count > 0)
						{
							socketAsyncResult = (Socket.SocketAsyncResult)queue.Peek();
							if (!this.Sock.disposed)
							{
								Socket.Worker worker = new Socket.Worker(socketAsyncResult);
								socketAsyncCall = this.GetDelegate(worker, socketAsyncResult.operation);
							}
							else
							{
								this.CompleteAllOnDispose(queue);
							}
						}
					}
					if (socketAsyncCall != null)
					{
						socketAsyncCall.BeginInvoke(null, socketAsyncResult);
					}
				}
				if (this.callback != null)
				{
					this.callback(this);
				}
				this.Buffer = null;
			}

			// Token: 0x060023CB RID: 9163 RVA: 0x0006B54C File Offset: 0x0006974C
			private Socket.SocketAsyncCall GetDelegate(Socket.Worker worker, Socket.SocketOperation op)
			{
				switch (op)
				{
				case Socket.SocketOperation.Receive:
					return new Socket.SocketAsyncCall(worker.Receive);
				case Socket.SocketOperation.ReceiveFrom:
					return new Socket.SocketAsyncCall(worker.ReceiveFrom);
				case Socket.SocketOperation.Send:
					return new Socket.SocketAsyncCall(worker.Send);
				case Socket.SocketOperation.SendTo:
					return new Socket.SocketAsyncCall(worker.SendTo);
				default:
					return null;
				}
			}

			// Token: 0x060023CC RID: 9164 RVA: 0x0006B5B0 File Offset: 0x000697B0
			public void Complete(bool synch)
			{
				this.completed_sync = synch;
				this.Complete();
			}

			// Token: 0x060023CD RID: 9165 RVA: 0x0006B5C0 File Offset: 0x000697C0
			public void Complete(int total)
			{
				this.total = total;
				this.Complete();
			}

			// Token: 0x060023CE RID: 9166 RVA: 0x0006B5D0 File Offset: 0x000697D0
			public void Complete(Exception e, bool synch)
			{
				this.completed_sync = synch;
				this.delayedException = e;
				this.Complete();
			}

			// Token: 0x060023CF RID: 9167 RVA: 0x0006B5E8 File Offset: 0x000697E8
			public void Complete(Exception e)
			{
				this.delayedException = e;
				this.Complete();
			}

			// Token: 0x060023D0 RID: 9168 RVA: 0x0006B5F8 File Offset: 0x000697F8
			public void Complete(Socket s)
			{
				this.acc_socket = s;
				this.Complete();
			}

			// Token: 0x060023D1 RID: 9169 RVA: 0x0006B608 File Offset: 0x00069808
			public void Complete(Socket s, int total)
			{
				this.acc_socket = s;
				this.total = total;
				this.Complete();
			}

			// Token: 0x17000A43 RID: 2627
			// (get) Token: 0x060023D2 RID: 9170 RVA: 0x0006B620 File Offset: 0x00069820
			public object AsyncState
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000A44 RID: 2628
			// (get) Token: 0x060023D3 RID: 9171 RVA: 0x0006B628 File Offset: 0x00069828
			// (set) Token: 0x060023D4 RID: 9172 RVA: 0x0006B688 File Offset: 0x00069888
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					lock (this)
					{
						if (this.waithandle == null)
						{
							this.waithandle = new ManualResetEvent(this.completed);
						}
					}
					return this.waithandle;
				}
				set
				{
					this.waithandle = value;
				}
			}

			// Token: 0x17000A45 RID: 2629
			// (get) Token: 0x060023D5 RID: 9173 RVA: 0x0006B694 File Offset: 0x00069894
			public bool CompletedSynchronously
			{
				get
				{
					return this.completed_sync;
				}
			}

			// Token: 0x17000A46 RID: 2630
			// (get) Token: 0x060023D6 RID: 9174 RVA: 0x0006B69C File Offset: 0x0006989C
			// (set) Token: 0x060023D7 RID: 9175 RVA: 0x0006B6A4 File Offset: 0x000698A4
			public bool IsCompleted
			{
				get
				{
					return this.completed;
				}
				set
				{
					this.completed = value;
					lock (this)
					{
						if (this.waithandle != null && value)
						{
							((ManualResetEvent)this.waithandle).Set();
						}
					}
				}
			}

			// Token: 0x17000A47 RID: 2631
			// (get) Token: 0x060023D8 RID: 9176 RVA: 0x0006B70C File Offset: 0x0006990C
			public Socket Socket
			{
				get
				{
					return this.acc_socket;
				}
			}

			// Token: 0x17000A48 RID: 2632
			// (get) Token: 0x060023D9 RID: 9177 RVA: 0x0006B714 File Offset: 0x00069914
			// (set) Token: 0x060023DA RID: 9178 RVA: 0x0006B71C File Offset: 0x0006991C
			public int Total
			{
				get
				{
					return this.total;
				}
				set
				{
					this.total = value;
				}
			}

			// Token: 0x17000A49 RID: 2633
			// (get) Token: 0x060023DB RID: 9179 RVA: 0x0006B728 File Offset: 0x00069928
			public SocketError ErrorCode
			{
				get
				{
					SocketException ex = this.delayedException as SocketException;
					if (ex != null)
					{
						return ex.SocketErrorCode;
					}
					if (this.error != 0)
					{
						return (SocketError)this.error;
					}
					return SocketError.Success;
				}
			}

			// Token: 0x040015F5 RID: 5621
			public Socket Sock;

			// Token: 0x040015F6 RID: 5622
			public IntPtr handle;

			// Token: 0x040015F7 RID: 5623
			private object state;

			// Token: 0x040015F8 RID: 5624
			private AsyncCallback callback;

			// Token: 0x040015F9 RID: 5625
			private WaitHandle waithandle;

			// Token: 0x040015FA RID: 5626
			private Exception delayedException;

			// Token: 0x040015FB RID: 5627
			public EndPoint EndPoint;

			// Token: 0x040015FC RID: 5628
			public byte[] Buffer;

			// Token: 0x040015FD RID: 5629
			public int Offset;

			// Token: 0x040015FE RID: 5630
			public int Size;

			// Token: 0x040015FF RID: 5631
			public SocketFlags SockFlags;

			// Token: 0x04001600 RID: 5632
			public Socket AcceptSocket;

			// Token: 0x04001601 RID: 5633
			public IPAddress[] Addresses;

			// Token: 0x04001602 RID: 5634
			public int Port;

			// Token: 0x04001603 RID: 5635
			public IList<ArraySegment<byte>> Buffers;

			// Token: 0x04001604 RID: 5636
			public bool ReuseSocket;

			// Token: 0x04001605 RID: 5637
			private Socket acc_socket;

			// Token: 0x04001606 RID: 5638
			private int total;

			// Token: 0x04001607 RID: 5639
			private bool completed_sync;

			// Token: 0x04001608 RID: 5640
			private bool completed;

			// Token: 0x04001609 RID: 5641
			public bool blocking;

			// Token: 0x0400160A RID: 5642
			internal int error;

			// Token: 0x0400160B RID: 5643
			private Socket.SocketOperation operation;

			// Token: 0x0400160C RID: 5644
			public object ares;

			// Token: 0x0400160D RID: 5645
			public int EndCalled;
		}

		// Token: 0x020003FA RID: 1018
		private sealed class Worker
		{
			// Token: 0x060023DC RID: 9180 RVA: 0x0006B764 File Offset: 0x00069964
			public Worker(Socket.SocketAsyncResult ares) : this(ares, true)
			{
			}

			// Token: 0x060023DD RID: 9181 RVA: 0x0006B770 File Offset: 0x00069970
			public Worker(Socket.SocketAsyncResult ares, bool requireSocketSecurity)
			{
				this.result = ares;
				this.requireSocketSecurity = requireSocketSecurity;
			}

			// Token: 0x060023DE RID: 9182 RVA: 0x0006B788 File Offset: 0x00069988
			public void Accept()
			{
				Socket s = null;
				try
				{
					s = this.result.Sock.Accept();
				}
				catch (Exception e)
				{
					this.result.Complete(e);
					return;
				}
				this.result.Complete(s);
			}

			// Token: 0x060023DF RID: 9183 RVA: 0x0006B7EC File Offset: 0x000699EC
			public void AcceptReceive()
			{
				Socket socket = null;
				try
				{
					if (this.result.AcceptSocket == null)
					{
						socket = this.result.Sock.Accept();
					}
					else
					{
						socket = this.result.AcceptSocket;
						this.result.Sock.Accept(socket);
					}
				}
				catch (Exception e)
				{
					this.result.Complete(e);
					return;
				}
				int total = 0;
				if (this.result.Size > 0)
				{
					try
					{
						SocketError socketError;
						total = socket.Receive_nochecks(this.result.Buffer, this.result.Offset, this.result.Size, this.result.SockFlags, out socketError);
					}
					catch (Exception e2)
					{
						this.result.Complete(e2);
						return;
					}
				}
				this.result.Complete(socket, total);
			}

			// Token: 0x060023E0 RID: 9184 RVA: 0x0006B904 File Offset: 0x00069B04
			public void Connect()
			{
				if (this.result.EndPoint != null)
				{
					try
					{
						if (!this.result.Sock.Blocking)
						{
							int num;
							this.result.Sock.Poll(-1, SelectMode.SelectWrite, out num);
							if (num != 0)
							{
								this.result.Complete(new SocketException(num));
								return;
							}
							this.result.Sock.connected = true;
						}
						else
						{
							this.result.Sock.seed_endpoint = this.result.EndPoint;
							this.result.Sock.Connect(this.result.EndPoint, this.requireSocketSecurity);
							this.result.Sock.connected = true;
						}
					}
					catch (Exception e)
					{
						this.result.Complete(e);
						return;
					}
					this.result.Complete();
				}
				else if (this.result.Addresses != null)
				{
					int num2 = 10036;
					foreach (IPAddress address in this.result.Addresses)
					{
						IPEndPoint ipendPoint = new IPEndPoint(address, this.result.Port);
						SocketAddress sa = ipendPoint.Serialize();
						try
						{
							Socket.Connect_internal(this.result.Sock.socket, sa, out num2, this.requireSocketSecurity);
						}
						catch (Exception e2)
						{
							this.result.Complete(e2);
							return;
						}
						if (num2 == 0)
						{
							this.result.Sock.connected = true;
							this.result.Sock.seed_endpoint = ipendPoint;
							this.result.Complete();
							return;
						}
						if (num2 == 10036 || num2 == 10035)
						{
							if (!this.result.Sock.Blocking)
							{
								int num3;
								this.result.Sock.Poll(-1, SelectMode.SelectWrite, out num3);
								if (num3 == 0)
								{
									this.result.Sock.connected = true;
									this.result.Sock.seed_endpoint = ipendPoint;
									this.result.Complete();
									return;
								}
							}
						}
					}
					this.result.Complete(new SocketException(num2));
				}
				else
				{
					this.result.Complete(new SocketException(10049));
				}
			}

			// Token: 0x060023E1 RID: 9185 RVA: 0x0006BBA8 File Offset: 0x00069DA8
			public void Disconnect()
			{
				try
				{
					this.result.Sock.Disconnect(this.result.ReuseSocket);
				}
				catch (Exception e)
				{
					this.result.Complete(e);
					return;
				}
				this.result.Complete();
			}

			// Token: 0x060023E2 RID: 9186 RVA: 0x0006BC14 File Offset: 0x00069E14
			public void Receive()
			{
				this.result.Complete();
			}

			// Token: 0x060023E3 RID: 9187 RVA: 0x0006BC24 File Offset: 0x00069E24
			public void ReceiveFrom()
			{
				int total = 0;
				try
				{
					total = this.result.Sock.ReceiveFrom_nochecks(this.result.Buffer, this.result.Offset, this.result.Size, this.result.SockFlags, ref this.result.EndPoint);
				}
				catch (Exception e)
				{
					this.result.Complete(e);
					return;
				}
				this.result.Complete(total);
			}

			// Token: 0x060023E4 RID: 9188 RVA: 0x0006BCC0 File Offset: 0x00069EC0
			public void ReceiveGeneric()
			{
				int total = 0;
				try
				{
					SocketError socketError;
					total = this.result.Sock.Receive(this.result.Buffers, this.result.SockFlags, out socketError);
				}
				catch (Exception e)
				{
					this.result.Complete(e);
					return;
				}
				this.result.Complete(total);
			}

			// Token: 0x060023E5 RID: 9189 RVA: 0x0006BD3C File Offset: 0x00069F3C
			private void UpdateSendValues(int last_sent)
			{
				if (this.result.error == 0)
				{
					this.send_so_far += last_sent;
					this.result.Offset += last_sent;
					this.result.Size -= last_sent;
				}
			}

			// Token: 0x060023E6 RID: 9190 RVA: 0x0006BD90 File Offset: 0x00069F90
			public void Send()
			{
				if (this.result.error == 0)
				{
					this.UpdateSendValues(this.result.Total);
					if (this.result.Sock.disposed)
					{
						this.result.Complete();
						return;
					}
					if (this.result.Size > 0)
					{
						Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(this.Send);
						socketAsyncCall.BeginInvoke(null, this.result);
						return;
					}
					this.result.Total = this.send_so_far;
				}
				this.result.Complete();
			}

			// Token: 0x060023E7 RID: 9191 RVA: 0x0006BE28 File Offset: 0x0006A028
			public void SendTo()
			{
				try
				{
					int last_sent = this.result.Sock.SendTo_nochecks(this.result.Buffer, this.result.Offset, this.result.Size, this.result.SockFlags, this.result.EndPoint);
					this.UpdateSendValues(last_sent);
					if (this.result.Size > 0)
					{
						Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(this.SendTo);
						socketAsyncCall.BeginInvoke(null, this.result);
						return;
					}
					this.result.Total = this.send_so_far;
				}
				catch (Exception e)
				{
					this.result.Complete(e);
					return;
				}
				this.result.Complete();
			}

			// Token: 0x060023E8 RID: 9192 RVA: 0x0006BF0C File Offset: 0x0006A10C
			public void SendGeneric()
			{
				int total = 0;
				try
				{
					SocketError socketError;
					total = this.result.Sock.Send(this.result.Buffers, this.result.SockFlags, out socketError);
				}
				catch (Exception e)
				{
					this.result.Complete(e);
					return;
				}
				this.result.Complete(total);
			}

			// Token: 0x0400160E RID: 5646
			private Socket.SocketAsyncResult result;

			// Token: 0x0400160F RID: 5647
			private bool requireSocketSecurity;

			// Token: 0x04001610 RID: 5648
			private int send_so_far;
		}

		// Token: 0x020003FB RID: 1019
		private sealed class SendFileAsyncResult : IAsyncResult
		{
			// Token: 0x060023E9 RID: 9193 RVA: 0x0006BF88 File Offset: 0x0006A188
			public SendFileAsyncResult(Socket.SendFileHandler d, IAsyncResult ares)
			{
				this.d = d;
				this.ares = ares;
			}

			// Token: 0x17000A4A RID: 2634
			// (get) Token: 0x060023EA RID: 9194 RVA: 0x0006BFA0 File Offset: 0x0006A1A0
			public object AsyncState
			{
				get
				{
					return this.ares.AsyncState;
				}
			}

			// Token: 0x17000A4B RID: 2635
			// (get) Token: 0x060023EB RID: 9195 RVA: 0x0006BFB0 File Offset: 0x0006A1B0
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return this.ares.AsyncWaitHandle;
				}
			}

			// Token: 0x17000A4C RID: 2636
			// (get) Token: 0x060023EC RID: 9196 RVA: 0x0006BFC0 File Offset: 0x0006A1C0
			public bool CompletedSynchronously
			{
				get
				{
					return this.ares.CompletedSynchronously;
				}
			}

			// Token: 0x17000A4D RID: 2637
			// (get) Token: 0x060023ED RID: 9197 RVA: 0x0006BFD0 File Offset: 0x0006A1D0
			public bool IsCompleted
			{
				get
				{
					return this.ares.IsCompleted;
				}
			}

			// Token: 0x17000A4E RID: 2638
			// (get) Token: 0x060023EE RID: 9198 RVA: 0x0006BFE0 File Offset: 0x0006A1E0
			public Socket.SendFileHandler Delegate
			{
				get
				{
					return this.d;
				}
			}

			// Token: 0x17000A4F RID: 2639
			// (get) Token: 0x060023EF RID: 9199 RVA: 0x0006BFE8 File Offset: 0x0006A1E8
			public IAsyncResult Original
			{
				get
				{
					return this.ares;
				}
			}

			// Token: 0x04001611 RID: 5649
			private IAsyncResult ares;

			// Token: 0x04001612 RID: 5650
			private Socket.SendFileHandler d;
		}

		// Token: 0x020004EA RID: 1258
		// (Invoke) Token: 0x06002C54 RID: 11348
		private delegate void SocketAsyncCall();

		// Token: 0x020004EB RID: 1259
		// (Invoke) Token: 0x06002C58 RID: 11352
		private delegate void SendFileHandler(string fileName, byte[] preBuffer, byte[] postBuffer, TransmitFileOptions flags);
	}
}
