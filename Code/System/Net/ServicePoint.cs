using System;
using System.Collections;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	/// <summary>Provides connection management for HTTP connections.</summary>
	// Token: 0x020003E4 RID: 996
	public class ServicePoint
	{
		// Token: 0x06002243 RID: 8771 RVA: 0x00063C74 File Offset: 0x00061E74
		internal ServicePoint(System.Uri uri, int connectionLimit, int maxIdleTime)
		{
			this.uri = uri;
			this.connectionLimit = connectionLimit;
			this.maxIdleTime = maxIdleTime;
			this.currentConnections = 0;
			this.idleSince = DateTime.Now;
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the server that this <see cref="T:System.Net.ServicePoint" /> object connects to.</summary>
		/// <returns>An instance of the <see cref="T:System.Uri" /> class that contains the URI of the Internet server that this <see cref="T:System.Net.ServicePoint" /> object connects to.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Net.ServicePoint" /> is in host mode.</exception>
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x00063CCC File Offset: 0x00061ECC
		public System.Uri Address
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x00063CD4 File Offset: 0x00061ED4
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Specifies the delegate to associate a local <see cref="T:System.Net.IPEndPoint" /> with a <see cref="T:System.Net.ServicePoint" />.</summary>
		/// <returns>A delegate that forces a <see cref="T:System.Net.ServicePoint" /> to use a particular local Internet Protocol (IP) address and port number. The default value is null.</returns>
		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x00063CDC File Offset: 0x00061EDC
		// (set) Token: 0x06002247 RID: 8775 RVA: 0x00063CE4 File Offset: 0x00061EE4
		public BindIPEndPoint BindIPEndPointDelegate
		{
			get
			{
				return this.endPointCallback;
			}
			set
			{
				this.endPointCallback = value;
			}
		}

		/// <summary>Gets the certificate received for this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>An instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class that contains the security certificate received for this <see cref="T:System.Net.ServicePoint" /> object.</returns>
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x00063CF0 File Offset: 0x00061EF0
		public X509Certificate Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		/// <summary>Gets the last client certificate sent to the server.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object that contains the public values of the last client certificate sent to the server.</returns>
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x00063CF8 File Offset: 0x00061EF8
		public X509Certificate ClientCertificate
		{
			get
			{
				return this.clientCertificate;
			}
		}

		/// <summary>Gets or sets the number of milliseconds after which an active <see cref="T:System.Net.ServicePoint" /> connection is closed.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the number of milliseconds that an active <see cref="T:System.Net.ServicePoint" /> connection remains open. The default is -1, which allows an active <see cref="T:System.Net.ServicePoint" /> connection to stay connected indefinitely. Set this property to 0 to force <see cref="T:System.Net.ServicePoint" /> connections to close after servicing a request.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is a negative number less than -1.</exception>
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x00063D00 File Offset: 0x00061F00
		// (set) Token: 0x0600224B RID: 8779 RVA: 0x00063D08 File Offset: 0x00061F08
		[MonoTODO]
		public int ConnectionLeaseTimeout
		{
			get
			{
				throw ServicePoint.GetMustImplement();
			}
			set
			{
				throw ServicePoint.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the maximum number of connections allowed on this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum number of connections allowed on this <see cref="T:System.Net.ServicePoint" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The connection limit is equal to or less than 0. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x00063D10 File Offset: 0x00061F10
		// (set) Token: 0x0600224D RID: 8781 RVA: 0x00063D18 File Offset: 0x00061F18
		public int ConnectionLimit
		{
			get
			{
				return this.connectionLimit;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.connectionLimit = value;
			}
		}

		/// <summary>Gets the connection name. </summary>
		/// <returns>A <see cref="T:System.String" /> that represents the connection name. </returns>
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x00063D30 File Offset: 0x00061F30
		public string ConnectionName
		{
			get
			{
				return this.uri.Scheme;
			}
		}

		/// <summary>Gets the number of open connections associated with this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The number of open connections associated with this <see cref="T:System.Net.ServicePoint" /> object.</returns>
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x00063D40 File Offset: 0x00061F40
		public int CurrentConnections
		{
			get
			{
				return this.currentConnections;
			}
		}

		/// <summary>Gets the date and time that the <see cref="T:System.Net.ServicePoint" /> object was last connected to a host.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that contains the date and time at which the <see cref="T:System.Net.ServicePoint" /> object was last connected.</returns>
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x00063D48 File Offset: 0x00061F48
		// (set) Token: 0x06002251 RID: 8785 RVA: 0x00063D50 File Offset: 0x00061F50
		public DateTime IdleSince
		{
			get
			{
				return this.idleSince;
			}
			internal set
			{
				object obj = this.locker;
				lock (obj)
				{
					this.idleSince = value;
				}
			}
		}

		/// <summary>Gets or sets the amount of time a connection associated with the <see cref="T:System.Net.ServicePoint" /> object can remain idle before the connection is closed.</summary>
		/// <returns>The length of time, in milliseconds, that a connection associated with the <see cref="T:System.Net.ServicePoint" /> object can remain idle before it is closed and reused for another connection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePoint.MaxIdleTime" /> is set to less than <see cref="F:System.Threading.Timeout.Infinite" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x00063D9C File Offset: 0x00061F9C
		// (set) Token: 0x06002253 RID: 8787 RVA: 0x00063DA4 File Offset: 0x00061FA4
		public int MaxIdleTime
		{
			get
			{
				return this.maxIdleTime;
			}
			set
			{
				if (value < -1 || value > 2147483647)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.maxIdleTime = value;
			}
		}

		/// <summary>Gets the version of the HTTP protocol that the <see cref="T:System.Net.ServicePoint" /> object uses.</summary>
		/// <returns>A <see cref="T:System.Version" /> object that contains the HTTP protocol version that the <see cref="T:System.Net.ServicePoint" /> object uses.</returns>
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x00063DC8 File Offset: 0x00061FC8
		public virtual Version ProtocolVersion
		{
			get
			{
				return this.protocolVersion;
			}
		}

		/// <summary>Gets or sets the size of the receiving buffer for the socket used by this <see cref="T:System.Net.ServicePoint" />.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that contains the size, in bytes, of the receive buffer. The default is 8192.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x00063DD0 File Offset: 0x00061FD0
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x00063DD8 File Offset: 0x00061FD8
		[MonoTODO]
		public int ReceiveBufferSize
		{
			get
			{
				throw ServicePoint.GetMustImplement();
			}
			set
			{
				throw ServicePoint.GetMustImplement();
			}
		}

		/// <summary>Indicates whether the <see cref="T:System.Net.ServicePoint" /> object supports pipelined connections.</summary>
		/// <returns>true if the <see cref="T:System.Net.ServicePoint" /> object supports pipelined connections; otherwise, false.</returns>
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x00063DE0 File Offset: 0x00061FE0
		public bool SupportsPipelining
		{
			get
			{
				return HttpVersion.Version11.Equals(this.protocolVersion);
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>true to expect 100-Continue responses for POST requests; otherwise, false. The default value is true.</returns>
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x00063DF4 File Offset: 0x00061FF4
		// (set) Token: 0x06002259 RID: 8793 RVA: 0x00063DFC File Offset: 0x00061FFC
		public bool Expect100Continue
		{
			get
			{
				return this.SendContinue;
			}
			set
			{
				this.SendContinue = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether the Nagle algorithm is used on connections managed by this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>true to use the Nagle algorithm; otherwise, false. The default value is true.</returns>
		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x00063E08 File Offset: 0x00062008
		// (set) Token: 0x0600225B RID: 8795 RVA: 0x00063E10 File Offset: 0x00062010
		public bool UseNagleAlgorithm
		{
			get
			{
				return this.useNagle;
			}
			set
			{
				this.useNagle = value;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x00063E1C File Offset: 0x0006201C
		// (set) Token: 0x0600225D RID: 8797 RVA: 0x00063E5C File Offset: 0x0006205C
		internal bool SendContinue
		{
			get
			{
				return this.sendContinue && (this.protocolVersion == null || this.protocolVersion == HttpVersion.Version11);
			}
			set
			{
				this.sendContinue = value;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x00063E68 File Offset: 0x00062068
		// (set) Token: 0x0600225F RID: 8799 RVA: 0x00063E70 File Offset: 0x00062070
		internal bool UsesProxy
		{
			get
			{
				return this.usesProxy;
			}
			set
			{
				this.usesProxy = value;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x00063E7C File Offset: 0x0006207C
		// (set) Token: 0x06002261 RID: 8801 RVA: 0x00063E84 File Offset: 0x00062084
		internal bool UseConnect
		{
			get
			{
				return this.useConnect;
			}
			set
			{
				this.useConnect = value;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x00063E90 File Offset: 0x00062090
		internal bool AvailableForRecycling
		{
			get
			{
				return this.CurrentConnections == 0 && this.maxIdleTime != -1 && DateTime.Now >= this.IdleSince.AddMilliseconds((double)this.maxIdleTime);
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x00063ED8 File Offset: 0x000620D8
		internal Hashtable Groups
		{
			get
			{
				if (this.groups == null)
				{
					this.groups = new Hashtable();
				}
				return this.groups;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x00063EF8 File Offset: 0x000620F8
		internal IPHostEntry HostEntry
		{
			get
			{
				object obj = this.hostE;
				lock (obj)
				{
					if (this.host != null)
					{
						return this.host;
					}
					string text = this.uri.Host;
					if (this.uri.HostNameType == System.UriHostNameType.IPv6 || this.uri.HostNameType == System.UriHostNameType.IPv4)
					{
						if (this.uri.HostNameType == System.UriHostNameType.IPv6)
						{
							text = text.Substring(1, text.Length - 2);
						}
						this.host = new IPHostEntry();
						this.host.AddressList = new IPAddress[]
						{
							IPAddress.Parse(text)
						};
						return this.host;
					}
					try
					{
						this.host = Dns.GetHostByName(text);
					}
					catch
					{
						return null;
					}
				}
				return this.host;
			}
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x00064010 File Offset: 0x00062210
		internal void SetVersion(Version version)
		{
			this.protocolVersion = version;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x0006401C File Offset: 0x0006221C
		private WebConnectionGroup GetConnectionGroup(string name)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			WebConnectionGroup webConnectionGroup = this.Groups[name] as WebConnectionGroup;
			if (webConnectionGroup != null)
			{
				return webConnectionGroup;
			}
			webConnectionGroup = new WebConnectionGroup(this, name);
			this.Groups[name] = webConnectionGroup;
			return webConnectionGroup;
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x00064068 File Offset: 0x00062268
		internal EventHandler SendRequest(HttpWebRequest request, string groupName)
		{
			object obj = this.locker;
			WebConnection connection;
			lock (obj)
			{
				WebConnectionGroup connectionGroup = this.GetConnectionGroup(groupName);
				connection = connectionGroup.GetConnection(request);
			}
			return connection.SendRequest(request);
		}

		/// <summary>Removes the specified connection group from this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates whether the connection group was closed.</returns>
		/// <param name="connectionGroupName">The name of the connection group that contains the connections to close and remove from this service point. </param>
		// Token: 0x06002268 RID: 8808 RVA: 0x000640C4 File Offset: 0x000622C4
		public bool CloseConnectionGroup(string connectionGroupName)
		{
			object obj = this.locker;
			lock (obj)
			{
				WebConnectionGroup connectionGroup = this.GetConnectionGroup(connectionGroupName);
				if (connectionGroup != null)
				{
					connectionGroup.Close();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x00064124 File Offset: 0x00062324
		internal void IncrementConnection()
		{
			object obj = this.locker;
			lock (obj)
			{
				this.currentConnections++;
				this.idleSince = DateTime.Now.AddMilliseconds(1000000.0);
			}
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x00064190 File Offset: 0x00062390
		internal void DecrementConnection()
		{
			object obj = this.locker;
			lock (obj)
			{
				this.currentConnections--;
				if (this.currentConnections == 0)
				{
					this.idleSince = DateTime.Now;
				}
			}
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000641F8 File Offset: 0x000623F8
		internal void SetCertificates(X509Certificate client, X509Certificate server)
		{
			this.certificate = server;
			this.clientCertificate = client;
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x00064208 File Offset: 0x00062408
		internal bool CallEndPointDelegate(System.Net.Sockets.Socket sock, IPEndPoint remote)
		{
			if (this.endPointCallback == null)
			{
				return true;
			}
			int num = 0;
			checked
			{
				for (;;)
				{
					IPEndPoint ipendPoint = null;
					try
					{
						ipendPoint = this.endPointCallback(this, remote, num);
					}
					catch
					{
						return false;
					}
					if (ipendPoint == null)
					{
						break;
					}
					try
					{
						sock.Bind(ipendPoint);
					}
					catch (System.Net.Sockets.SocketException)
					{
						num++;
						continue;
					}
					return true;
				}
				return true;
			}
		}

		// Token: 0x0400150A RID: 5386
		private System.Uri uri;

		// Token: 0x0400150B RID: 5387
		private int connectionLimit;

		// Token: 0x0400150C RID: 5388
		private int maxIdleTime;

		// Token: 0x0400150D RID: 5389
		private int currentConnections;

		// Token: 0x0400150E RID: 5390
		private DateTime idleSince;

		// Token: 0x0400150F RID: 5391
		private Version protocolVersion;

		// Token: 0x04001510 RID: 5392
		private X509Certificate certificate;

		// Token: 0x04001511 RID: 5393
		private X509Certificate clientCertificate;

		// Token: 0x04001512 RID: 5394
		private IPHostEntry host;

		// Token: 0x04001513 RID: 5395
		private bool usesProxy;

		// Token: 0x04001514 RID: 5396
		private Hashtable groups;

		// Token: 0x04001515 RID: 5397
		private bool sendContinue = true;

		// Token: 0x04001516 RID: 5398
		private bool useConnect;

		// Token: 0x04001517 RID: 5399
		private object locker = new object();

		// Token: 0x04001518 RID: 5400
		private object hostE = new object();

		// Token: 0x04001519 RID: 5401
		private bool useNagle;

		// Token: 0x0400151A RID: 5402
		private BindIPEndPoint endPointCallback;
	}
}
