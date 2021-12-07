using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>Provides an HTTP-specific implementation of the <see cref="T:System.Net.WebResponse" /> class.</summary>
	// Token: 0x02000323 RID: 803
	[Serializable]
	public class HttpWebResponse : WebResponse, IDisposable, ISerializable
	{
		// Token: 0x06001C77 RID: 7287 RVA: 0x00053628 File Offset: 0x00051828
		internal HttpWebResponse(System.Uri uri, string method, WebConnectionData data, CookieContainer container)
		{
			this.uri = uri;
			this.method = method;
			this.webHeaders = data.Headers;
			this.version = data.Version;
			this.statusCode = (HttpStatusCode)data.StatusCode;
			this.statusDescription = data.StatusDescription;
			this.stream = data.stream;
			this.contentLength = -1L;
			try
			{
				string text = this.webHeaders["Content-Length"];
				if (string.IsNullOrEmpty(text) || !long.TryParse(text, out this.contentLength))
				{
					this.contentLength = -1L;
				}
			}
			catch (Exception)
			{
				this.contentLength = -1L;
			}
			if (container != null)
			{
				this.cookie_container = container;
				this.FillCookies();
			}
			string a = this.webHeaders["Content-Encoding"];
			if (a == "gzip" && (data.request.AutomaticDecompression & DecompressionMethods.GZip) != DecompressionMethods.None)
			{
				this.stream = new System.IO.Compression.GZipStream(this.stream, System.IO.Compression.CompressionMode.Decompress);
			}
			else if (a == "deflate" && (data.request.AutomaticDecompression & DecompressionMethods.Deflate) != DecompressionMethods.None)
			{
				this.stream = new System.IO.Compression.DeflateStream(this.stream, System.IO.Compression.CompressionMode.Decompress);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebResponse" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.HttpWebRequest" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.HttpWebRequest" />. </param>
		// Token: 0x06001C78 RID: 7288 RVA: 0x000537A8 File Offset: 0x000519A8
		[Obsolete("Serialization is obsoleted for this type", false)]
		protected HttpWebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.uri = (System.Uri)serializationInfo.GetValue("uri", typeof(System.Uri));
			this.contentLength = serializationInfo.GetInt64("contentLength");
			this.contentType = serializationInfo.GetString("contentType");
			this.method = serializationInfo.GetString("method");
			this.statusDescription = serializationInfo.GetString("statusDescription");
			this.cookieCollection = (CookieCollection)serializationInfo.GetValue("cookieCollection", typeof(CookieCollection));
			this.version = (Version)serializationInfo.GetValue("version", typeof(Version));
			this.statusCode = (HttpStatusCode)((int)serializationInfo.GetValue("statusCode", typeof(HttpStatusCode)));
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.HttpWebResponse" /> will be serialized. </param>
		/// <param name="streamingContext">The destination of the serialization. </param>
		// Token: 0x06001C79 RID: 7289 RVA: 0x000538A8 File Offset: 0x00051AA8
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.HttpWebResponse" />.</summary>
		// Token: 0x06001C7A RID: 7290 RVA: 0x000538B4 File Offset: 0x00051AB4
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets the character set of the response.</summary>
		/// <returns>A string that contains the character set of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x000538C4 File Offset: 0x00051AC4
		public string CharacterSet
		{
			get
			{
				string text = this.ContentType;
				if (text == null)
				{
					return "ISO-8859-1";
				}
				string text2 = text.ToLower();
				int num = text2.IndexOf("charset=");
				if (num == -1)
				{
					return "ISO-8859-1";
				}
				num += 8;
				int num2 = text2.IndexOf(';', num);
				return (num2 != -1) ? text.Substring(num, num2 - num) : text.Substring(num);
			}
		}

		/// <summary>Gets the method that is used to encode the body of the response.</summary>
		/// <returns>A string that describes the method that is used to encode the body of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x00053930 File Offset: 0x00051B30
		public string ContentEncoding
		{
			get
			{
				this.CheckDisposed();
				string text = this.webHeaders["Content-Encoding"];
				return (text == null) ? string.Empty : text;
			}
		}

		/// <summary>Gets the length of the content returned by the request.</summary>
		/// <returns>The number of bytes returned by the request. Content length does not include header information.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x00053968 File Offset: 0x00051B68
		public override long ContentLength
		{
			get
			{
				return this.contentLength;
			}
		}

		/// <summary>Gets the content type of the response.</summary>
		/// <returns>A string that contains the content type of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x00053970 File Offset: 0x00051B70
		public override string ContentType
		{
			get
			{
				this.CheckDisposed();
				if (this.contentType == null)
				{
					this.contentType = this.webHeaders["Content-Type"];
				}
				return this.contentType;
			}
		}

		/// <summary>Gets or sets the cookies that are associated with this response.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains the cookies that are associated with this response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x000539A0 File Offset: 0x00051BA0
		// (set) Token: 0x06001C80 RID: 7296 RVA: 0x000539D0 File Offset: 0x00051BD0
		public CookieCollection Cookies
		{
			get
			{
				this.CheckDisposed();
				if (this.cookieCollection == null)
				{
					this.cookieCollection = new CookieCollection();
				}
				return this.cookieCollection;
			}
			set
			{
				this.CheckDisposed();
				this.cookieCollection = value;
			}
		}

		/// <summary>Gets the headers that are associated with this response from the server.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the header information returned with the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x000539E0 File Offset: 0x00051BE0
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.webHeaders;
			}
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x000539E8 File Offset: 0x00051BE8
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether both client and server were authenticated.</summary>
		/// <returns>true if mutual authentication occurred; otherwise, false.</returns>
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x000539F0 File Offset: 0x00051BF0
		[MonoTODO]
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				throw HttpWebResponse.GetMustImplement();
			}
		}

		/// <summary>Gets the last date and time that the contents of the response were modified.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the date and time that the contents of the response were modified.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x000539F8 File Offset: 0x00051BF8
		public DateTime LastModified
		{
			get
			{
				this.CheckDisposed();
				DateTime result;
				try
				{
					string dateStr = this.webHeaders["Last-Modified"];
					result = MonoHttpDate.Parse(dateStr);
				}
				catch (Exception)
				{
					result = DateTime.Now;
				}
				return result;
			}
		}

		/// <summary>Gets the method that is used to return the response.</summary>
		/// <returns>A string that contains the HTTP method that is used to return the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x00053A5C File Offset: 0x00051C5C
		public string Method
		{
			get
			{
				this.CheckDisposed();
				return this.method;
			}
		}

		/// <summary>Gets the version of the HTTP protocol that is used in the response.</summary>
		/// <returns>A <see cref="T:System.Version" /> that contains the HTTP protocol version of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x00053A6C File Offset: 0x00051C6C
		public Version ProtocolVersion
		{
			get
			{
				this.CheckDisposed();
				return this.version;
			}
		}

		/// <summary>Gets the URI of the Internet resource that responded to the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the Internet resource that responded to the request.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x00053A7C File Offset: 0x00051C7C
		public override System.Uri ResponseUri
		{
			get
			{
				this.CheckDisposed();
				return this.uri;
			}
		}

		/// <summary>Gets the name of the server that sent the response.</summary>
		/// <returns>A string that contains the name of the server that sent the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x00053A8C File Offset: 0x00051C8C
		public string Server
		{
			get
			{
				this.CheckDisposed();
				return this.webHeaders["Server"];
			}
		}

		/// <summary>Gets the status of the response.</summary>
		/// <returns>One of the <see cref="T:System.Net.HttpStatusCode" /> values.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x00053AA4 File Offset: 0x00051CA4
		public HttpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		/// <summary>Gets the status description returned with the response.</summary>
		/// <returns>A string that describes the status of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x00053AAC File Offset: 0x00051CAC
		public string StatusDescription
		{
			get
			{
				this.CheckDisposed();
				return this.statusDescription;
			}
		}

		/// <summary>Gets the contents of a header that was returned with the response.</summary>
		/// <returns>The contents of the specified header.</returns>
		/// <param name="headerName">The header value to return. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x06001C8B RID: 7307 RVA: 0x00053ABC File Offset: 0x00051CBC
		public string GetResponseHeader(string headerName)
		{
			this.CheckDisposed();
			string text = this.webHeaders[headerName];
			return (text == null) ? string.Empty : text;
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00053AF0 File Offset: 0x00051CF0
		internal void ReadAll()
		{
			WebConnectionStream webConnectionStream = this.stream as WebConnectionStream;
			if (webConnectionStream == null)
			{
				return;
			}
			try
			{
				webConnectionStream.ReadAll();
			}
			catch
			{
			}
		}

		/// <summary>Gets the stream that is used to read the body of the response from the server.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> containing the body of the response.</returns>
		/// <exception cref="T:System.Net.ProtocolViolationException">There is no response stream. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001C8D RID: 7309 RVA: 0x00053B40 File Offset: 0x00051D40
		public override Stream GetResponseStream()
		{
			this.CheckDisposed();
			if (this.stream == null)
			{
				return Stream.Null;
			}
			if (string.Compare(this.method, "HEAD", true) == 0)
			{
				return Stream.Null;
			}
			return this.stream;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06001C8E RID: 7310 RVA: 0x00053B7C File Offset: 0x00051D7C
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("uri", this.uri);
			serializationInfo.AddValue("contentLength", this.contentLength);
			serializationInfo.AddValue("contentType", this.contentType);
			serializationInfo.AddValue("method", this.method);
			serializationInfo.AddValue("statusDescription", this.statusDescription);
			serializationInfo.AddValue("cookieCollection", this.cookieCollection);
			serializationInfo.AddValue("version", this.version);
			serializationInfo.AddValue("statusCode", this.statusCode);
		}

		/// <summary>Closes the response stream.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001C8F RID: 7311 RVA: 0x00053C18 File Offset: 0x00051E18
		public override void Close()
		{
			((IDisposable)this).Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.HttpWebResponse" />, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources. </param>
		// Token: 0x06001C90 RID: 7312 RVA: 0x00053C20 File Offset: 0x00051E20
		private void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (disposing)
			{
				this.uri = null;
				this.cookieCollection = null;
				this.method = null;
				this.version = null;
				this.statusDescription = null;
			}
			Stream stream = this.stream;
			this.stream = null;
			if (stream != null)
			{
				stream.Close();
			}
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00053C84 File Offset: 0x00051E84
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00053CA4 File Offset: 0x00051EA4
		private void FillCookies()
		{
			if (this.webHeaders == null)
			{
				return;
			}
			string[] values = this.webHeaders.GetValues("Set-Cookie");
			if (values != null)
			{
				foreach (string cookie in values)
				{
					this.SetCookie(cookie);
				}
			}
			values = this.webHeaders.GetValues("Set-Cookie2");
			if (values != null)
			{
				foreach (string cookie2 in values)
				{
					this.SetCookie2(cookie2);
				}
			}
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00053D38 File Offset: 0x00051F38
		private void SetCookie(string header)
		{
			Cookie cookie = null;
			CookieParser cookieParser = new CookieParser(header);
			string text;
			string text2;
			while (cookieParser.GetNextNameValue(out text, out text2))
			{
				if ((text != null && !(text == string.Empty)) || cookie != null)
				{
					if (cookie == null)
					{
						cookie = new Cookie(text, text2);
					}
					else
					{
						text = text.ToUpper();
						string text3 = text;
						switch (text3)
						{
						case "COMMENT":
							if (cookie.Comment == null)
							{
								cookie.Comment = text2;
							}
							break;
						case "COMMENTURL":
							if (cookie.CommentUri == null)
							{
								cookie.CommentUri = new System.Uri(text2);
							}
							break;
						case "DISCARD":
							cookie.Discard = true;
							break;
						case "DOMAIN":
							if (cookie.Domain == string.Empty)
							{
								cookie.Domain = text2;
							}
							break;
						case "HTTPONLY":
							cookie.HttpOnly = true;
							break;
						case "MAX-AGE":
							if (cookie.Expires == DateTime.MinValue)
							{
								try
								{
									cookie.Expires = cookie.TimeStamp.AddSeconds(uint.Parse(text2));
								}
								catch
								{
								}
							}
							break;
						case "EXPIRES":
							if (!(cookie.Expires != DateTime.MinValue))
							{
								cookie.Expires = this.TryParseCookieExpires(text2);
							}
							break;
						case "PATH":
							cookie.Path = text2;
							break;
						case "PORT":
							if (cookie.Port == null)
							{
								cookie.Port = text2;
							}
							break;
						case "SECURE":
							cookie.Secure = true;
							break;
						case "VERSION":
							try
							{
								cookie.Version = (int)uint.Parse(text2);
							}
							catch
							{
							}
							break;
						}
					}
				}
			}
			if (cookie == null)
			{
				return;
			}
			if (this.cookieCollection == null)
			{
				this.cookieCollection = new CookieCollection();
			}
			if (cookie.Domain == string.Empty)
			{
				cookie.Domain = this.uri.Host;
			}
			this.cookieCollection.Add(cookie);
			if (this.cookie_container != null)
			{
				this.cookie_container.Add(this.uri, cookie);
			}
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00054068 File Offset: 0x00052268
		private void SetCookie2(string cookies_str)
		{
			string[] array = cookies_str.Split(new char[]
			{
				','
			});
			foreach (string cookie in array)
			{
				this.SetCookie(cookie);
			}
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000540A8 File Offset: 0x000522A8
		private DateTime TryParseCookieExpires(string value)
		{
			if (value == null || value.Length == 0)
			{
				return DateTime.MinValue;
			}
			for (int i = 0; i < this.cookieExpiresFormats.Length; i++)
			{
				try
				{
					DateTime dateTime = DateTime.ParseExact(value, this.cookieExpiresFormats[i], CultureInfo.InvariantCulture);
					dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
					return TimeZone.CurrentTimeZone.ToLocalTime(dateTime);
				}
				catch
				{
				}
			}
			return DateTime.MinValue;
		}

		// Token: 0x040011F3 RID: 4595
		private System.Uri uri;

		// Token: 0x040011F4 RID: 4596
		private WebHeaderCollection webHeaders;

		// Token: 0x040011F5 RID: 4597
		private CookieCollection cookieCollection;

		// Token: 0x040011F6 RID: 4598
		private string method;

		// Token: 0x040011F7 RID: 4599
		private Version version;

		// Token: 0x040011F8 RID: 4600
		private HttpStatusCode statusCode;

		// Token: 0x040011F9 RID: 4601
		private string statusDescription;

		// Token: 0x040011FA RID: 4602
		private long contentLength;

		// Token: 0x040011FB RID: 4603
		private string contentType;

		// Token: 0x040011FC RID: 4604
		private CookieContainer cookie_container;

		// Token: 0x040011FD RID: 4605
		private bool disposed;

		// Token: 0x040011FE RID: 4606
		private Stream stream;

		// Token: 0x040011FF RID: 4607
		private string[] cookieExpiresFormats = new string[]
		{
			"r",
			"ddd, dd'-'MMM'-'yyyy HH':'mm':'ss 'GMT'",
			"ddd, dd'-'MMM'-'yy HH':'mm':'ss 'GMT'"
		};
	}
}
