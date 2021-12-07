using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Net
{
	/// <summary>Represents a response to a request being handled by an <see cref="T:System.Net.HttpListener" /> object.</summary>
	// Token: 0x0200031A RID: 794
	public sealed class HttpListenerResponse : IDisposable
	{
		// Token: 0x06001BCB RID: 7115 RVA: 0x0004FBEC File Offset: 0x0004DDEC
		internal HttpListenerResponse(HttpListenerContext context)
		{
			this.context = context;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.HttpListenerResponse" />.</summary>
		// Token: 0x06001BCC RID: 7116 RVA: 0x0004FC3C File Offset: 0x0004DE3C
		void IDisposable.Dispose()
		{
			this.Close(true);
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x0004FC48 File Offset: 0x0004DE48
		internal bool ForceCloseChunked
		{
			get
			{
				return this.force_close_chunked;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.Encoding" /> for this response's <see cref="P:System.Net.HttpListenerResponse.OutputStream" />.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object suitable for use with the data in the <see cref="P:System.Net.HttpListenerResponse.OutputStream" /> property, or null if no encoding is specified.</returns>
		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x0004FC50 File Offset: 0x0004DE50
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x0004FC70 File Offset: 0x0004DE70
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
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.content_encoding = value;
			}
		}

		/// <summary>Gets or sets the number of bytes in the body data included in the response.</summary>
		/// <returns>The value of the response's Content-Length header.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The response is already being sent.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x0004FCAC File Offset: 0x0004DEAC
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x0004FCB4 File Offset: 0x0004DEB4
		public long ContentLength64
		{
			get
			{
				return this.content_length;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("Must be >= 0", "value");
				}
				this.cl_set = true;
				this.content_length = value;
			}
		}

		/// <summary>Gets or sets the MIME type of the content returned.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the text of the response's Content-Type header.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is an empty string ("").</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x0004FD1C File Offset: 0x0004DF1C
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x0004FD24 File Offset: 0x0004DF24
		public string ContentType
		{
			get
			{
				return this.content_type;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.content_type = value;
			}
		}

		/// <summary>Gets or sets the collection of cookies returned with the response.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains cookies to accompany the response. The collection is empty if no cookies have been added to the response.</returns>
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x0004FD60 File Offset: 0x0004DF60
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x0004FD80 File Offset: 0x0004DF80
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
			set
			{
				this.cookies = value;
			}
		}

		/// <summary>Gets or sets the collection of header name/value pairs returned by the server.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> instance that contains all the explicitly set HTTP headers to be included in the response.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.WebHeaderCollection" /> instance specified for a set operation is not valid for a response.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0004FD8C File Offset: 0x0004DF8C
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x0004FD94 File Offset: 0x0004DF94
		public WebHeaderCollection Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the server requests a persistent connection.</summary>
		/// <returns>true if the server requests a persistent connection; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x0004FDA0 File Offset: 0x0004DFA0
		// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x0004FDA8 File Offset: 0x0004DFA8
		public bool KeepAlive
		{
			get
			{
				return this.keep_alive;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.keep_alive = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object to which a response can be written.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object to which a response can be written.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x0004FDE4 File Offset: 0x0004DFE4
		public Stream OutputStream
		{
			get
			{
				if (this.output_stream == null)
				{
					this.output_stream = this.context.Connection.GetResponseStream();
				}
				return this.output_stream;
			}
		}

		/// <summary>Gets or sets the HTTP version used for the response.</summary>
		/// <returns>A <see cref="T:System.Version" /> object indicating the version of HTTP used when responding to the client. Note that this property is now obsolete.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation does not have its <see cref="P:System.Version.Major" /> property set to 1 or does not have its <see cref="P:System.Version.Minor" /> property set to either 0 or 1.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0004FE10 File Offset: 0x0004E010
		// (set) Token: 0x06001BDC RID: 7132 RVA: 0x0004FE18 File Offset: 0x0004E018
		public Version ProtocolVersion
		{
			get
			{
				return this.version;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Major != 1 || (value.Minor != 0 && value.Minor != 1))
				{
					throw new ArgumentException("Must be 1.0 or 1.1", "value");
				}
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				this.version = value;
			}
		}

		/// <summary>Gets or sets the value of the HTTP Location header in this response.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the absolute URL to be sent to the client in the Location header. </returns>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is an empty string ("").</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x0004FEC4 File Offset: 0x0004E0C4
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x0004FECC File Offset: 0x0004E0CC
		public string RedirectLocation
		{
			get
			{
				return this.location;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.location = value;
			}
		}

		/// <summary>Gets or sets whether the response uses chunked transfer encoding.</summary>
		/// <returns>true if the response is set to use chunked transfer encoding; otherwise, false. The default is false.</returns>
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x0004FF08 File Offset: 0x0004E108
		// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x0004FF10 File Offset: 0x0004E110
		public bool SendChunked
		{
			get
			{
				return this.chunked;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.chunked = value;
			}
		}

		/// <summary>Gets or sets the HTTP status code to be returned to the client.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the HTTP status code for the requested resource. The default is <see cref="F:System.Net.HttpStatusCode.OK" />, indicating that the server successfully processed the client's request and included the requested resource in the response body.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The value specified for a set operation is not valid. Valid values are between 100 and 999 inclusive.</exception>
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x0004FF4C File Offset: 0x0004E14C
		// (set) Token: 0x06001BE2 RID: 7138 RVA: 0x0004FF54 File Offset: 0x0004E154
		public int StatusCode
		{
			get
			{
				return this.status_code;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				if (value < 100 || value > 999)
				{
					throw new ProtocolViolationException("StatusCode must be between 100 and 999.");
				}
				this.status_code = value;
				this.status_description = HttpListenerResponse.GetStatusDescription(value);
			}
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x0004FFC4 File Offset: 0x0004E1C4
		internal static string GetStatusDescription(int code)
		{
			switch (code)
			{
			case 400:
				return "Bad Request";
			case 401:
				return "Unauthorized";
			case 402:
				return "Payment Required";
			case 403:
				return "Forbidden";
			case 404:
				return "Not Found";
			case 405:
				return "Method Not Allowed";
			case 406:
				return "Not Acceptable";
			case 407:
				return "Proxy Authentication Required";
			case 408:
				return "Request Timeout";
			case 409:
				return "Conflict";
			case 410:
				return "Gone";
			case 411:
				return "Length Required";
			case 412:
				return "Precondition Failed";
			case 413:
				return "Request Entity Too Large";
			case 414:
				return "Request-Uri Too Long";
			case 415:
				return "Unsupported Media Type";
			case 416:
				return "Requested Range Not Satisfiable";
			case 417:
				return "Expectation Failed";
			default:
				switch (code)
				{
				case 200:
					return "OK";
				case 201:
					return "Created";
				case 202:
					return "Accepted";
				case 203:
					return "Non-Authoritative Information";
				case 204:
					return "No Content";
				case 205:
					return "Reset Content";
				case 206:
					return "Partial Content";
				case 207:
					return "Multi-Status";
				default:
					switch (code)
					{
					case 300:
						return "Multiple Choices";
					case 301:
						return "Moved Permanently";
					case 302:
						return "Found";
					case 303:
						return "See Other";
					case 304:
						return "Not Modified";
					case 305:
						return "Use Proxy";
					default:
						switch (code)
						{
						case 500:
							return "Internal Server Error";
						case 501:
							return "Not Implemented";
						case 502:
							return "Bad Gateway";
						case 503:
							return "Service Unavailable";
						case 504:
							return "Gateway Timeout";
						case 505:
							return "Http Version Not Supported";
						default:
							switch (code)
							{
							case 100:
								return "Continue";
							case 101:
								return "Switching Protocols";
							case 102:
								return "Processing";
							default:
								return string.Empty;
							}
							break;
						case 507:
							return "Insufficient Storage";
						}
						break;
					case 307:
						return "Temporary Redirect";
					}
					break;
				}
				break;
			case 422:
				return "Unprocessable Entity";
			case 423:
				return "Locked";
			case 424:
				return "Failed Dependency";
			}
		}

		/// <summary>Gets or sets a text description of the HTTP status code returned to the client.</summary>
		/// <returns>The text description of the HTTP status code returned to the client. The default is the RFC 2616 description for the <see cref="P:System.Net.HttpListenerResponse.StatusCode" /> property value, or an empty string ("") if an RFC 2616 description does not exist.</returns>
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x000501FC File Offset: 0x0004E3FC
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x00050204 File Offset: 0x0004E404
		public string StatusDescription
		{
			get
			{
				return this.status_description;
			}
			set
			{
				this.status_description = value;
			}
		}

		/// <summary>Closes the connection to the client without sending a response.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001BE6 RID: 7142 RVA: 0x00050210 File Offset: 0x0004E410
		public void Abort()
		{
			if (this.disposed)
			{
				return;
			}
			this.Close(true);
		}

		/// <summary>Adds the specified header and value to the HTTP headers for this response.</summary>
		/// <param name="name">The name of the HTTP header to set.</param>
		/// <param name="value">The value for the <paramref name="name" /> header.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null or an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentException">You are not allowed to specify a value for the specified header.-or-<paramref name="name" /> or <paramref name="value" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65,535 characters.</exception>
		// Token: 0x06001BE7 RID: 7143 RVA: 0x00050228 File Offset: 0x0004E428
		public void AddHeader(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException("'name' cannot be empty", "name");
			}
			if (value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			this.headers.Set(name, value);
		}

		/// <summary>Adds the specified <see cref="T:System.Net.Cookie" /> to the collection of cookies for this response.</summary>
		/// <param name="cookie">The <see cref="T:System.Net.Cookie" /> to add to the collection to be sent with this response</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is null.</exception>
		// Token: 0x06001BE8 RID: 7144 RVA: 0x00050290 File Offset: 0x0004E490
		public void AppendCookie(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			this.Cookies.Add(cookie);
		}

		/// <summary>Appends a value to the specified HTTP header to be sent with this response.</summary>
		/// <param name="name">The name of the HTTP header to append <paramref name="value" /> to.</param>
		/// <param name="value">The value to append to the <paramref name="name" /> header.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null or an empty string ("").-or-You are not allowed to specify a value for the specified header.-or-<paramref name="name" /> or <paramref name="value" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65,535 characters.</exception>
		// Token: 0x06001BE9 RID: 7145 RVA: 0x000502B0 File Offset: 0x0004E4B0
		public void AppendHeader(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException("'name' cannot be empty", "name");
			}
			if (value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			this.headers.Add(name, value);
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00050318 File Offset: 0x0004E518
		private void Close(bool force)
		{
			this.disposed = true;
			this.context.Connection.Close(force);
		}

		/// <summary>Sends the response to the client and releases the resources held by this <see cref="T:System.Net.HttpListenerResponse" /> instance.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001BEB RID: 7147 RVA: 0x00050334 File Offset: 0x0004E534
		public void Close()
		{
			if (this.disposed)
			{
				return;
			}
			this.Close(false);
		}

		/// <summary>Returns the specified byte array to the client and releases the resources held by this <see cref="T:System.Net.HttpListenerResponse" /> instance.</summary>
		/// <param name="responseEntity">A <see cref="T:System.Byte" /> array that contains the response to send to the client.</param>
		/// <param name="willBlock">true to block execution while flushing the stream to the client; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="responseEntity" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001BEC RID: 7148 RVA: 0x0005034C File Offset: 0x0004E54C
		public void Close(byte[] responseEntity, bool willBlock)
		{
			if (this.disposed)
			{
				return;
			}
			if (responseEntity == null)
			{
				throw new ArgumentNullException("responseEntity");
			}
			this.ContentLength64 = (long)responseEntity.Length;
			this.OutputStream.Write(responseEntity, 0, (int)this.content_length);
			this.Close(false);
		}

		/// <summary>Copies properties from the specified <see cref="T:System.Net.HttpListenerResponse" /> to this response.</summary>
		/// <param name="templateResponse">The <see cref="T:System.Net.HttpListenerResponse" /> instance to copy.</param>
		// Token: 0x06001BED RID: 7149 RVA: 0x0005039C File Offset: 0x0004E59C
		public void CopyFrom(HttpListenerResponse templateResponse)
		{
			this.headers.Clear();
			this.headers.Add(templateResponse.headers);
			this.content_length = templateResponse.content_length;
			this.status_code = templateResponse.status_code;
			this.status_description = templateResponse.status_description;
			this.keep_alive = templateResponse.keep_alive;
			this.version = templateResponse.version;
		}

		/// <summary>Configures the response to redirect the client to the specified URL.</summary>
		/// <param name="url">The URL that the client should use to locate the requested resource.</param>
		// Token: 0x06001BEE RID: 7150 RVA: 0x00050404 File Offset: 0x0004E604
		public void Redirect(string url)
		{
			this.StatusCode = 302;
			this.location = url;
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00050418 File Offset: 0x0004E618
		private bool FindCookie(Cookie cookie)
		{
			string name = cookie.Name;
			string domain = cookie.Domain;
			string path = cookie.Path;
			foreach (object obj in this.cookies)
			{
				Cookie cookie2 = (Cookie)obj;
				if (!(name != cookie2.Name))
				{
					if (!(domain != cookie2.Domain))
					{
						if (path == cookie2.Path)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x000504E8 File Offset: 0x0004E6E8
		internal void SendHeaders(bool closing, MemoryStream ms)
		{
			Encoding @default = this.content_encoding;
			if (@default == null)
			{
				@default = Encoding.Default;
			}
			if (this.content_type != null)
			{
				if (this.content_encoding != null && this.content_type.IndexOf("charset=") == -1)
				{
					string webName = this.content_encoding.WebName;
					this.headers.SetInternal("Content-Type", this.content_type + "; charset=" + webName);
				}
				else
				{
					this.headers.SetInternal("Content-Type", this.content_type);
				}
			}
			if (this.headers["Server"] == null)
			{
				this.headers.SetInternal("Server", "Mono-HTTPAPI/1.0");
			}
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			if (this.headers["Date"] == null)
			{
				this.headers.SetInternal("Date", DateTime.UtcNow.ToString("r", invariantCulture));
			}
			if (!this.chunked)
			{
				if (!this.cl_set && closing)
				{
					this.cl_set = true;
					this.content_length = 0L;
				}
				if (this.cl_set)
				{
					this.headers.SetInternal("Content-Length", this.content_length.ToString(invariantCulture));
				}
			}
			Version protocolVersion = this.context.Request.ProtocolVersion;
			if (!this.cl_set && !this.chunked && protocolVersion >= HttpVersion.Version11)
			{
				this.chunked = true;
			}
			bool flag = this.status_code == 400 || this.status_code == 408 || this.status_code == 411 || this.status_code == 413 || this.status_code == 414 || this.status_code == 500 || this.status_code == 503;
			if (!flag)
			{
				flag = (this.context.Request.Headers["connection"] == "close");
				flag |= (protocolVersion <= HttpVersion.Version10);
			}
			if (!this.keep_alive || flag)
			{
				this.headers.SetInternal("Connection", "close");
			}
			if (this.chunked)
			{
				this.headers.SetInternal("Transfer-Encoding", "chunked");
			}
			int chunkedUses = this.context.Connection.ChunkedUses;
			if (chunkedUses >= 100)
			{
				this.force_close_chunked = true;
				if (!flag)
				{
					this.headers.SetInternal("Connection", "close");
				}
			}
			if (this.location != null)
			{
				this.headers.SetInternal("Location", this.location);
			}
			if (this.cookies != null)
			{
				foreach (object obj in this.cookies)
				{
					Cookie cookie = (Cookie)obj;
					this.headers.SetInternal("Set-Cookie", cookie.ToClientString());
				}
			}
			StreamWriter streamWriter = new StreamWriter(ms, @default);
			streamWriter.Write("HTTP/{0} {1} {2}\r\n", this.version, this.status_code, this.status_description);
			string value = this.headers.ToStringMultiValue();
			streamWriter.Write(value);
			streamWriter.Flush();
			int num = @default.GetPreamble().Length;
			if (this.output_stream == null)
			{
				this.output_stream = this.context.Connection.GetResponseStream();
			}
			ms.Position = (long)num;
			this.HeadersSent = true;
		}

		/// <summary>Adds or updates a <see cref="T:System.Net.Cookie" /> in the collection of cookies sent with this response. </summary>
		/// <param name="cookie">A <see cref="T:System.Net.Cookie" /> for this response.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The cookie already exists in the collection and could not be replaced.</exception>
		// Token: 0x06001BF1 RID: 7153 RVA: 0x000508D4 File Offset: 0x0004EAD4
		public void SetCookie(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			if (this.cookies != null)
			{
				if (this.FindCookie(cookie))
				{
					throw new ArgumentException("The cookie already exists.");
				}
			}
			else
			{
				this.cookies = new CookieCollection();
			}
			this.cookies.Add(cookie);
		}

		// Token: 0x0400112A RID: 4394
		private bool disposed;

		// Token: 0x0400112B RID: 4395
		private Encoding content_encoding;

		// Token: 0x0400112C RID: 4396
		private long content_length;

		// Token: 0x0400112D RID: 4397
		private bool cl_set;

		// Token: 0x0400112E RID: 4398
		private string content_type;

		// Token: 0x0400112F RID: 4399
		private CookieCollection cookies;

		// Token: 0x04001130 RID: 4400
		private WebHeaderCollection headers = new WebHeaderCollection();

		// Token: 0x04001131 RID: 4401
		private bool keep_alive = true;

		// Token: 0x04001132 RID: 4402
		private ResponseStream output_stream;

		// Token: 0x04001133 RID: 4403
		private Version version = HttpVersion.Version11;

		// Token: 0x04001134 RID: 4404
		private string location;

		// Token: 0x04001135 RID: 4405
		private int status_code = 200;

		// Token: 0x04001136 RID: 4406
		private string status_description = "OK";

		// Token: 0x04001137 RID: 4407
		private bool chunked;

		// Token: 0x04001138 RID: 4408
		private HttpListenerContext context;

		// Token: 0x04001139 RID: 4409
		internal bool HeadersSent;

		// Token: 0x0400113A RID: 4410
		private bool force_close_chunked;
	}
}
