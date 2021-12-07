using System;

namespace System.Net.Sockets
{
	/// <summary>Listens for connections from TCP network clients.</summary>
	// Token: 0x0200040A RID: 1034
	public class TcpListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener" /> class that listens on the specified port.</summary>
		/// <param name="port">The port on which to listen for incoming connection attempts. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		// Token: 0x0600245B RID: 9307 RVA: 0x0006D448 File Offset: 0x0006B648
		[Obsolete("Use TcpListener (IPAddress address, int port) instead")]
		public TcpListener(int port)
		{
			if (port < 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.Init(AddressFamily.InterNetwork, new IPEndPoint(IPAddress.Any, port));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener" /> class with the specified local endpoint.</summary>
		/// <param name="localEP">An <see cref="T:System.Net.IPEndPoint" /> that represents the local endpoint to which to bind the listener <see cref="T:System.Net.Sockets.Socket" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localEP" /> is null. </exception>
		// Token: 0x0600245C RID: 9308 RVA: 0x0006D480 File Offset: 0x0006B680
		public TcpListener(IPEndPoint local_end_point)
		{
			if (local_end_point == null)
			{
				throw new ArgumentNullException("local_end_point");
			}
			this.Init(local_end_point.AddressFamily, local_end_point);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener" /> class that listens for incoming connection attempts on the specified local IP address and port number.</summary>
		/// <param name="localaddr">An <see cref="T:System.Net.IPAddress" /> that represents the local IP address. </param>
		/// <param name="port">The port on which to listen for incoming connection attempts. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localaddr" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		// Token: 0x0600245D RID: 9309 RVA: 0x0006D4B4 File Offset: 0x0006B6B4
		public TcpListener(IPAddress listen_ip, int port)
		{
			if (listen_ip == null)
			{
				throw new ArgumentNullException("listen_ip");
			}
			if (port < 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.Init(listen_ip.AddressFamily, new IPEndPoint(listen_ip, port));
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x0006D508 File Offset: 0x0006B708
		private void Init(AddressFamily family, EndPoint ep)
		{
			this.active = false;
			this.server = new Socket(family, SocketType.Stream, ProtocolType.Tcp);
			this.savedEP = ep;
		}

		/// <summary>Gets a value that indicates whether <see cref="T:System.Net.Sockets.TcpListener" /> is actively listening for client connections.</summary>
		/// <returns>true if <see cref="T:System.Net.Sockets.TcpListener" /> is actively listening; otherwise, false.</returns>
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x0006D528 File Offset: 0x0006B728
		protected bool Active
		{
			get
			{
				return this.active;
			}
		}

		/// <summary>Gets the underlying <see cref="T:System.Net.EndPoint" /> of the current <see cref="T:System.Net.Sockets.TcpListener" />.</summary>
		/// <returns>The <see cref="T:System.Net.EndPoint" /> to which the <see cref="T:System.Net.Sockets.Socket" /> is bound.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x0006D530 File Offset: 0x0006B730
		public EndPoint LocalEndpoint
		{
			get
			{
				if (this.active)
				{
					return this.server.LocalEndPoint;
				}
				return this.savedEP;
			}
		}

		/// <summary>Gets the underlying network <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The underlying <see cref="T:System.Net.Sockets.Socket" />.</returns>
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x0006D550 File Offset: 0x0006B750
		public Socket Server
		{
			get
			{
				return this.server;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.TcpListener" /> allows only one underlying socket to listen to a specific port.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.TcpListener" /> allows only one <see cref="T:System.Net.Sockets.TcpListener" /> to listen to a specific port; otherwise, false. . The default is true for Windows Server 2003 and Windows XP Service Pack 2 and later, and false for all other versions.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.TcpListener" /> has been started. Call the <see cref="M:System.Net.Sockets.TcpListener.Stop" /> method and then set the <see cref="P:System.Net.Sockets.Socket.ExclusiveAddressUse" /> property.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the underlying socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x0006D558 File Offset: 0x0006B758
		// (set) Token: 0x06002463 RID: 9315 RVA: 0x0006D598 File Offset: 0x0006B798
		public bool ExclusiveAddressUse
		{
			get
			{
				if (this.server == null)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.active)
				{
					throw new InvalidOperationException("The TcpListener has been started");
				}
				return this.server.ExclusiveAddressUse;
			}
			set
			{
				if (this.server == null)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.active)
				{
					throw new InvalidOperationException("The TcpListener has been started");
				}
				this.server.ExclusiveAddressUse = value;
			}
		}

		/// <summary>Accepts a pending connection request.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> used to send and receive data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002464 RID: 9316 RVA: 0x0006D5E4 File Offset: 0x0006B7E4
		public Socket AcceptSocket()
		{
			if (!this.active)
			{
				throw new InvalidOperationException("Socket is not listening");
			}
			return this.server.Accept();
		}

		/// <summary>Accepts a pending connection request </summary>
		/// <returns>A <see cref="T:System.Net.Sockets.TcpClient" /> used to send and receive data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation in MSDN for a detailed description of the error. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002465 RID: 9317 RVA: 0x0006D608 File Offset: 0x0006B808
		public TcpClient AcceptTcpClient()
		{
			if (!this.active)
			{
				throw new InvalidOperationException("Socket is not listening");
			}
			Socket tcpClient = this.server.Accept();
			TcpClient tcpClient2 = new TcpClient();
			tcpClient2.SetTcpClient(tcpClient);
			return tcpClient2;
		}

		/// <summary>Frees resources used by the <see cref="T:System.Net.Sockets.TcpListener" /> class.</summary>
		// Token: 0x06002466 RID: 9318 RVA: 0x0006D648 File Offset: 0x0006B848
		~TcpListener()
		{
			if (this.active)
			{
				this.Stop();
			}
		}

		/// <summary>Determines if there are pending connection requests.</summary>
		/// <returns>true if connections are pending; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002467 RID: 9319 RVA: 0x0006D690 File Offset: 0x0006B890
		public bool Pending()
		{
			if (!this.active)
			{
				throw new InvalidOperationException("Socket is not listening");
			}
			return this.server.Poll(0, SelectMode.SelectRead);
		}

		/// <summary>Starts listening for incoming connection requests.</summary>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation in MSDN for a detailed description of the error. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002468 RID: 9320 RVA: 0x0006D6B8 File Offset: 0x0006B8B8
		public void Start()
		{
			this.Start(5);
		}

		/// <summary>Starts listening for incoming connection requests with a maximum number of pending connection.</summary>
		/// <param name="backlog">The maximum length of the pending connections queue.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The<paramref name=" backlog" /> parameter is less than zero or exceeds the maximum number of permitted connections.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002469 RID: 9321 RVA: 0x0006D6C4 File Offset: 0x0006B8C4
		public void Start(int backlog)
		{
			if (this.active)
			{
				return;
			}
			if (this.server == null)
			{
				throw new InvalidOperationException("Invalid server socket");
			}
			this.server.Bind(this.savedEP);
			this.server.Listen(backlog);
			this.active = true;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous creation of the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object containing information about the accept operation. This object is passed to the <paramref name="callback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600246A RID: 9322 RVA: 0x0006D718 File Offset: 0x0006B918
		public IAsyncResult BeginAcceptSocket(AsyncCallback callback, object state)
		{
			if (this.server == null)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			return this.server.BeginAccept(callback, state);
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous creation of the <see cref="T:System.Net.Sockets.TcpClient" />.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object containing information about the accept operation. This object is passed to the <paramref name="callback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600246B RID: 9323 RVA: 0x0006D744 File Offset: 0x0006B944
		public IAsyncResult BeginAcceptTcpClient(AsyncCallback callback, object state)
		{
			if (this.server == null)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			return this.server.BeginAccept(callback, state);
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.Socket" /> to handle remote host communication.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> returned by a call to the <see cref="M:System.Net.Sockets.TcpListener.BeginAcceptSocket(System.AsyncCallback,System.Object)" />  method.</param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> parameter was not created by a call to the <see cref="M:System.Net.Sockets.TcpListener.BeginAcceptSocket(System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.Sockets.TcpListener.EndAcceptSocket(System.IAsyncResult)" /> method was previously called. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600246C RID: 9324 RVA: 0x0006D770 File Offset: 0x0006B970
		public Socket EndAcceptSocket(IAsyncResult asyncResult)
		{
			return this.server.EndAccept(asyncResult);
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.TcpClient" /> to handle remote host communication.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.TcpClient" />.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> returned by a call to the <see cref="M:System.Net.Sockets.TcpListener.BeginAcceptTcpClient(System.AsyncCallback,System.Object)" /> method.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600246D RID: 9325 RVA: 0x0006D780 File Offset: 0x0006B980
		public TcpClient EndAcceptTcpClient(IAsyncResult asyncResult)
		{
			Socket tcpClient = this.server.EndAccept(asyncResult);
			TcpClient tcpClient2 = new TcpClient();
			tcpClient2.SetTcpClient(tcpClient);
			return tcpClient2;
		}

		/// <summary>Closes the listener.</summary>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation in MSDN for a detailed description of the error. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600246E RID: 9326 RVA: 0x0006D7A8 File Offset: 0x0006B9A8
		public void Stop()
		{
			if (this.active)
			{
				this.server.Close();
				this.server = null;
			}
			this.Init(AddressFamily.InterNetwork, this.savedEP);
		}

		// Token: 0x040016C3 RID: 5827
		private bool active;

		// Token: 0x040016C4 RID: 5828
		private Socket server;

		// Token: 0x040016C5 RID: 5829
		private EndPoint savedEP;
	}
}
