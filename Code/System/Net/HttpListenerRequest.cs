using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace System.Net
{
	/// <summary>Describes an incoming HTTP request to an <see cref="T:System.Net.HttpListener" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000319 RID: 793
	public sealed class HttpListenerRequest
	{
		// Token: 0x06001BA6 RID: 7078 RVA: 0x0004EFEC File Offset: 0x0004D1EC
		internal HttpListenerRequest(HttpListenerContext context)
		{
			this.context = context;
			this.headers = new WebHeaderCollection();
			this.input_stream = Stream.Null;
			this.version = HttpVersion.Version10;
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0004F07C File Offset: 0x0004D27C
		internal void SetRequestLine(string req)
		{
			string[] array = req.Split(HttpListenerRequest.separators, 3);
			if (array.Length != 3)
			{
				this.context.ErrorMessage = "Invalid request line (parts).";
				return;
			}
			this.method = array[0];
			foreach (char c in this.method)
			{
				int num = (int)c;
				if ((num < 65 || num > 90) && (num <= 32 || c >= '\u007f' || c == '(' || c == ')' || c == '<' || c == '<' || c == '>' || c == '@' || c == ',' || c == ';' || c == ':' || c == '\\' || c == '"' || c == '/' || c == '[' || c == ']' || c == '?' || c == '=' || c == '{' || c == '}'))
				{
					this.context.ErrorMessage = "(Invalid verb)";
					return;
				}
			}
			this.raw_url = array[1];
			if (array[2].Length != 8 || !array[2].StartsWith("HTTP/"))
			{
				this.context.ErrorMessage = "Invalid request line (version).";
				return;
			}
			try
			{
				this.version = new Version(array[2].Substring(5));
				if (this.version.Major < 1)
				{
					throw new Exception();
				}
			}
			catch
			{
				this.context.ErrorMessage = "Invalid request line (version).";
			}
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0004F24C File Offset: 0x0004D44C
		private void CreateQueryString(string query)
		{
			this.query_string = new System.Collections.Specialized.NameValueCollection();
			if (query == null || query.Length == 0)
			{
				return;
			}
			if (query[0] == '?')
			{
				query = query.Substring(1);
			}
			string[] array = query.Split(new char[]
			{
				'&'
			});
			foreach (string text in array)
			{
				int num = text.IndexOf('=');
				if (num == -1)
				{
					this.query_string.Add(null, HttpUtility.UrlDecode(text));
				}
				else
				{
					string name = HttpUtility.UrlDecode(text.Substring(0, num));
					string val = HttpUtility.UrlDecode(text.Substring(num + 1));
					this.query_string.Add(name, val);
				}
			}
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0004F314 File Offset: 0x0004D514
		internal void FinishInitialization()
		{
			string text = this.UserHostName;
			if (this.version > HttpVersion.Version10 && (text == null || text.Length == 0))
			{
				this.context.ErrorMessage = "Invalid host name";
				return;
			}
			System.Uri uri;
			string pathAndQuery;
			if (System.Uri.MaybeUri(this.raw_url) && System.Uri.TryCreate(this.raw_url, System.UriKind.Absolute, out uri))
			{
				pathAndQuery = uri.PathAndQuery;
			}
			else
			{
				pathAndQuery = this.raw_url;
			}
			if (text == null || text.Length == 0)
			{
				text = this.UserHostAddress;
			}
			if (uri != null)
			{
				text = uri.Host;
			}
			int num = text.IndexOf(':');
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			string text2 = string.Format("{0}://{1}:{2}", (!this.IsSecureConnection) ? "http" : "https", text, this.LocalEndPoint.Port);
			if (!System.Uri.TryCreate(text2 + pathAndQuery, System.UriKind.Absolute, out this.url))
			{
				this.context.ErrorMessage = "Invalid url: " + text2 + pathAndQuery;
				return;
			}
			this.CreateQueryString(this.url.Query);
			string text3 = null;
			if (this.version >= HttpVersion.Version11)
			{
				text3 = this.Headers["Transfer-Encoding"];
				if (text3 != null && text3 != "chunked")
				{
					this.context.Connection.SendError(null, 501);
					return;
				}
			}
			this.is_chunked = (text3 == "chunked");
			foreach (string strB in HttpListenerRequest.no_body_methods)
			{
				if (string.Compare(this.method, strB, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return;
				}
			}
			if (!this.is_chunked && !this.cl_set)
			{
				this.context.Connection.SendError(null, 411);
				return;
			}
			if (this.is_chunked || this.content_length > 0L)
			{
				this.input_stream = this.context.Connection.GetRequestStream(this.is_chunked, this.content_length);
			}
			if (this.Headers["Expect"] == "100-continue")
			{
				ResponseStream responseStream = this.context.Connection.GetResponseStream();
				responseStream.InternalWrite(HttpListenerRequest._100continue, 0, HttpListenerRequest._100continue.Length);
			}
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0004F5A8 File Offset: 0x0004D7A8
		internal static string Unquote(string str)
		{
			int num = str.IndexOf('"');
			int num2 = str.LastIndexOf('"');
			if (num >= 0 && num2 >= 0)
			{
				str = str.Substring(num + 1, num2 - 1);
			}
			return str.Trim();
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0004F5EC File Offset: 0x0004D7EC
		internal void AddHeader(string header)
		{
			int num = header.IndexOf(':');
			if (num == -1 || num == 0)
			{
				this.context.ErrorMessage = "Bad Request";
				this.context.ErrorStatus = 400;
				return;
			}
			string text = header.Substring(0, num).Trim();
			string text2 = header.Substring(num + 1).Trim();
			string text3 = text.ToLower(CultureInfo.InvariantCulture);
			this.headers.SetInternal(text, text2);
			string text4 = text3;
			switch (text4)
			{
			case "accept-language":
				this.user_languages = text2.Split(new char[]
				{
					','
				});
				break;
			case "accept":
				this.accept_types = text2.Split(new char[]
				{
					','
				});
				break;
			case "content-length":
				try
				{
					this.content_length = long.Parse(text2.Trim());
					if (this.content_length < 0L)
					{
						this.context.ErrorMessage = "Invalid Content-Length.";
					}
					this.cl_set = true;
				}
				catch
				{
					this.context.ErrorMessage = "Invalid Content-Length.";
				}
				break;
			case "referer":
				try
				{
					this.referrer = new System.Uri(text2);
				}
				catch
				{
					this.referrer = new System.Uri("http://someone.is.screwing.with.the.headers.com/");
				}
				break;
			case "cookie":
			{
				if (this.cookies == null)
				{
					this.cookies = new CookieCollection();
				}
				string[] array = text2.Split(new char[]
				{
					',',
					';'
				});
				Cookie cookie = null;
				int num3 = 0;
				foreach (string text5 in array)
				{
					string text6 = text5.Trim();
					if (text6.Length != 0)
					{
						if (text6.StartsWith("$Version"))
						{
							num3 = int.Parse(HttpListenerRequest.Unquote(text6.Substring(text6.IndexOf("=") + 1)));
						}
						else if (text6.StartsWith("$Path"))
						{
							if (cookie != null)
							{
								cookie.Path = text6.Substring(text6.IndexOf("=") + 1).Trim();
							}
						}
						else if (text6.StartsWith("$Domain"))
						{
							if (cookie != null)
							{
								cookie.Domain = text6.Substring(text6.IndexOf("=") + 1).Trim();
							}
						}
						else if (text6.StartsWith("$Port"))
						{
							if (cookie != null)
							{
								cookie.Port = text6.Substring(text6.IndexOf("=") + 1).Trim();
							}
						}
						else
						{
							if (cookie != null)
							{
								this.cookies.Add(cookie);
							}
							cookie = new Cookie();
							int num4 = text6.IndexOf("=");
							if (num4 > 0)
							{
								cookie.Name = text6.Substring(0, num4).Trim();
								cookie.Value = text6.Substring(num4 + 1).Trim();
							}
							else
							{
								cookie.Name = text6.Trim();
								cookie.Value = string.Empty;
							}
							cookie.Version = num3;
						}
					}
				}
				if (cookie != null)
				{
					this.cookies.Add(cookie);
				}
				break;
			}
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0004F9EC File Offset: 0x0004DBEC
		internal bool FlushInput()
		{
			if (!this.HasEntityBody)
			{
				return true;
			}
			int num = 2048;
			if (this.content_length > 0L)
			{
				num = (int)Math.Min(this.content_length, (long)num);
			}
			byte[] buffer = new byte[num];
			bool result;
			for (;;)
			{
				try
				{
					if (this.InputStream.Read(buffer, 0, num) <= 0)
					{
						result = true;
						break;
					}
				}
				catch
				{
					result = false;
					break;
				}
			}
			return result;
		}

		/// <summary>Gets the MIME types accepted by the client. </summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the type names specified in the request's Accept header or null if the client request did not include an Accept header.</returns>
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x0004FA80 File Offset: 0x0004DC80
		public string[] AcceptTypes
		{
			get
			{
				return this.accept_types;
			}
		}

		/// <summary>Gets an error code that identifies a problem with the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> provided by the client.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains a Windows error code.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x0004FA88 File Offset: 0x0004DC88
		[MonoTODO("Always returns 0")]
		public int ClientCertificateError
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the content encoding that can be used with data sent with the request</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object suitable for use with the data in the <see cref="P:System.Net.HttpListenerRequest.InputStream" /> property.</returns>
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x0004FA8C File Offset: 0x0004DC8C
		public Encoding ContentEncoding
		{
			get
			{
				if (this.content_encoding == null)
				{
					this.content_encoding = Encoding.Default;
				}
				return this.content_encoding;
			}
		}

		/// <summary>Gets the length of the body data included in the request.</summary>
		/// <returns>The value from the request's Content-Length header. This value is -1 if the content length is not known.</returns>
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x0004FAAC File Offset: 0x0004DCAC
		public long ContentLength64
		{
			get
			{
				return this.content_length;
			}
		}

		/// <summary>Gets the MIME type of the body data included in the request.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the text of the request's Content-Type header.</returns>
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x0004FAB4 File Offset: 0x0004DCB4
		public string ContentType
		{
			get
			{
				return this.headers["content-type"];
			}
		}

		/// <summary>Gets the cookies sent with the request.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains cookies that accompany the request. This property returns an empty collection if the request does not contain cookies.</returns>
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0004FAC8 File Offset: 0x0004DCC8
		public CookieCollection Cookies
		{
			get
			{
				if (this.cookies == null)
				{
					this.cookies = new CookieCollection();
				}
				return this.cookies;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the request has associated body data.</summary>
		/// <returns>true if the request has associated body data; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x0004FAE8 File Offset: 0x0004DCE8
		public bool HasEntityBody
		{
			get
			{
				return this.content_length > 0L || this.is_chunked;
			}
		}

		/// <summary>Gets the collection of header name/value pairs sent in the request.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the HTTP headers included in the request.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x0004FB00 File Offset: 0x0004DD00
		public System.Collections.Specialized.NameValueCollection Headers
		{
			get
			{
				return this.headers;
			}
		}

		/// <summary>Gets the HTTP method specified by the client. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the method used in the request.</returns>
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x0004FB08 File Offset: 0x0004DD08
		public string HttpMethod
		{
			get
			{
				return this.method;
			}
		}

		/// <summary>Gets a stream that contains the body data sent by the client.</summary>
		/// <returns>A readable <see cref="T:System.IO.Stream" /> object that contains the bytes sent by the client in the body of the request. This property returns <see cref="F:System.IO.Stream.Null" /> if no data is sent with the request.</returns>
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x0004FB10 File Offset: 0x0004DD10
		public Stream InputStream
		{
			get
			{
				return this.input_stream;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the client sending this request is authenticated.</summary>
		/// <returns>true if the client was authenticated; otherwise, false.</returns>
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0004FB18 File Offset: 0x0004DD18
		[MonoTODO("Always returns false")]
		public bool IsAuthenticated
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the request is sent from the local computer.</summary>
		/// <returns>true if the request originated on the same computer as the <see cref="T:System.Net.HttpListener" /> object that provided the request; otherwise, false.</returns>
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x0004FB1C File Offset: 0x0004DD1C
		public bool IsLocal
		{
			get
			{
				return IPAddress.IsLoopback(this.RemoteEndPoint.Address);
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the TCP connection used to send the request is using the Secure Sockets Layer (SSL) protocol.</summary>
		/// <returns>true if the TCP connection is using SSL; otherwise, false.</returns>
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x0004FB30 File Offset: 0x0004DD30
		public bool IsSecureConnection
		{
			get
			{
				return this.context.Connection.IsSecure;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the client requests a persistent connection.</summary>
		/// <returns>true if the connection should be kept open; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x0004FB44 File Offset: 0x0004DD44
		public bool KeepAlive
		{
			get
			{
				return false;
			}
		}

		/// <summary>Get the server IP address and port number to which the request is directed.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> that represents the IP address that the request is sent to.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0004FB48 File Offset: 0x0004DD48
		public IPEndPoint LocalEndPoint
		{
			get
			{
				return this.context.Connection.LocalEndPoint;
			}
		}

		/// <summary>Gets the HTTP version used by the requesting client.</summary>
		/// <returns>A <see cref="T:System.Version" /> that identifies the client's version of HTTP.</returns>
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x0004FB5C File Offset: 0x0004DD5C
		public Version ProtocolVersion
		{
			get
			{
				return this.version;
			}
		}

		/// <summary>Gets the query string included in the request.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains the query data included in the request <see cref="P:System.Net.HttpListenerRequest.Url" />.</returns>
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x0004FB64 File Offset: 0x0004DD64
		public System.Collections.Specialized.NameValueCollection QueryString
		{
			get
			{
				return this.query_string;
			}
		}

		/// <summary>Gets the URL information (without the host and port) requested by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the raw URL for this request.</returns>
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x0004FB6C File Offset: 0x0004DD6C
		public string RawUrl
		{
			get
			{
				return this.raw_url;
			}
		}

		/// <summary>Gets the client IP address and port number from which the request originated.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> that represents the IP address and port number from which the request originated.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x0004FB74 File Offset: 0x0004DD74
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.context.Connection.RemoteEndPoint;
			}
		}

		/// <summary>Gets the request identifier of the incoming HTTP request.</summary>
		/// <returns>A <see cref="T:System.Guid" /> object that contains the identifier of the HTTP request.</returns>
		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x0004FB88 File Offset: 0x0004DD88
		public Guid RequestTraceIdentifier
		{
			get
			{
				return this.identifier;
			}
		}

		/// <summary>Gets the <see cref="T:System.Uri" /> object requested by the client.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object that identifies the resource requested by the client.</returns>
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x0004FB90 File Offset: 0x0004DD90
		public System.Uri Url
		{
			get
			{
				return this.url;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the resource that referred the client to the server.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object that contains the text of the request's <see cref="F:System.Net.HttpRequestHeader.Referer" /> header, or null if the header was not included in the request.</returns>
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x0004FB98 File Offset: 0x0004DD98
		public System.Uri UrlReferrer
		{
			get
			{
				return this.referrer;
			}
		}

		/// <summary>Gets the user agent presented by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the text of the request's User-Agent header.</returns>
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x0004FBA0 File Offset: 0x0004DDA0
		public string UserAgent
		{
			get
			{
				return this.headers["user-agent"];
			}
		}

		/// <summary>Gets the server IP address and port number to which the request is directed.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the host address information.</returns>
		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x0004FBB4 File Offset: 0x0004DDB4
		public string UserHostAddress
		{
			get
			{
				return this.LocalEndPoint.ToString();
			}
		}

		/// <summary>Gets the DNS name and, if provided, the port number specified by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the text of the request's Host header.</returns>
		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x0004FBC4 File Offset: 0x0004DDC4
		public string UserHostName
		{
			get
			{
				return this.headers["host"];
			}
		}

		/// <summary>Gets the natural languages that are preferred for the response.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the languages specified in the request's <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header or null if the client request did not include an <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header.</returns>
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x0004FBD8 File Offset: 0x0004DDD8
		public string[] UserLanguages
		{
			get
			{
				return this.user_languages;
			}
		}

		/// <summary>Begins an asynchronous request for the client's X.509 v.3 certificate.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that indicates the status of the operation.</returns>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the callback delegate when the operation completes.</param>
		// Token: 0x06001BC8 RID: 7112 RVA: 0x0004FBE0 File Offset: 0x0004DDE0
		public IAsyncResult BeginGetClientCertificate(AsyncCallback requestCallback, object state)
		{
			return null;
		}

		/// <summary>Ends an asynchronous request for the client's X.509 v.3 certificate.</summary>
		/// <returns>The <see cref="T:System.IAsyncResult" /> object that is returned when the operation started.</returns>
		/// <param name="asyncResult">The pending request for the certificate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling <see cref="M:System.Net.HttpListenerRequest.BeginGetClientCertificate(System.AsyncCallback,System.Object)" /><paramref name="e." /></exception>
		/// <exception cref="T:System.InvalidOperationException">This method was already called for the operation identified by <paramref name="asyncResult" />. </exception>
		// Token: 0x06001BC9 RID: 7113 RVA: 0x0004FBE4 File Offset: 0x0004DDE4
		public System.Security.Cryptography.X509Certificates.X509Certificate2 EndGetClientCertificate(IAsyncResult asyncResult)
		{
			return null;
		}

		/// <summary>Retrieves the client's X.509 v.3 certificate.</summary>
		/// <returns>A <see cref="N:System.Security.Cryptography.X509Certificates" /> object that contains the client's X.509 v.3 certificate.</returns>
		/// <exception cref="T:System.InvalidOperationException">A call to this method to retrieve the client's X.509 v.3 certificate is in progress and therefore another call to this method cannot be made.</exception>
		// Token: 0x06001BCA RID: 7114 RVA: 0x0004FBE8 File Offset: 0x0004DDE8
		public System.Security.Cryptography.X509Certificates.X509Certificate2 GetClientCertificate()
		{
			return null;
		}

		// Token: 0x04001115 RID: 4373
		private string[] accept_types;

		// Token: 0x04001116 RID: 4374
		private Encoding content_encoding;

		// Token: 0x04001117 RID: 4375
		private long content_length;

		// Token: 0x04001118 RID: 4376
		private bool cl_set;

		// Token: 0x04001119 RID: 4377
		private CookieCollection cookies;

		// Token: 0x0400111A RID: 4378
		private WebHeaderCollection headers;

		// Token: 0x0400111B RID: 4379
		private string method;

		// Token: 0x0400111C RID: 4380
		private Stream input_stream;

		// Token: 0x0400111D RID: 4381
		private Version version;

		// Token: 0x0400111E RID: 4382
		private System.Collections.Specialized.NameValueCollection query_string;

		// Token: 0x0400111F RID: 4383
		private string raw_url;

		// Token: 0x04001120 RID: 4384
		private Guid identifier;

		// Token: 0x04001121 RID: 4385
		private System.Uri url;

		// Token: 0x04001122 RID: 4386
		private System.Uri referrer;

		// Token: 0x04001123 RID: 4387
		private string[] user_languages;

		// Token: 0x04001124 RID: 4388
		private HttpListenerContext context;

		// Token: 0x04001125 RID: 4389
		private bool is_chunked;

		// Token: 0x04001126 RID: 4390
		private static byte[] _100continue = Encoding.ASCII.GetBytes("HTTP/1.1 100 Continue\r\n\r\n");

		// Token: 0x04001127 RID: 4391
		private static readonly string[] no_body_methods = new string[]
		{
			"GET",
			"HEAD",
			"DELETE"
		};

		// Token: 0x04001128 RID: 4392
		private static char[] separators = new char[]
		{
			' '
		};
	}
}
