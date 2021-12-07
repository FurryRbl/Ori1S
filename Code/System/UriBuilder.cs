using System;
using System.Text;

namespace System
{
	/// <summary>Provides a custom constructor for uniform resource identifiers (URIs) and modifies URIs for the <see cref="T:System.Uri" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004B0 RID: 1200
	public class UriBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class.</summary>
		// Token: 0x06002B10 RID: 11024 RVA: 0x00093E08 File Offset: 0x00092008
		public UriBuilder() : this(System.Uri.UriSchemeHttp, "localhost")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified URI.</summary>
		/// <param name="uri">A URI string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is null. </exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="uri" /> is a zero length string or contains only spaces.-or- The parsing routine detected a scheme in an invalid form.-or- The parser detected more than two consecutive slashes in a URI that does not use the "file" scheme.-or- <paramref name="uri" /> is not a valid URI. </exception>
		// Token: 0x06002B11 RID: 11025 RVA: 0x00093E1C File Offset: 0x0009201C
		public UriBuilder(string uri) : this(new System.Uri(uri))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified <see cref="T:System.Uri" /> instance.</summary>
		/// <param name="uri">An instance of the <see cref="T:System.Uri" /> class. </param>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="uri" /> is null. </exception>
		// Token: 0x06002B12 RID: 11026 RVA: 0x00093E2C File Offset: 0x0009202C
		public UriBuilder(System.Uri uri)
		{
			this.scheme = uri.Scheme;
			this.host = uri.Host;
			this.port = uri.Port;
			this.path = uri.AbsolutePath;
			this.query = uri.Query;
			this.fragment = uri.Fragment;
			this.username = uri.UserInfo;
			int num = this.username.IndexOf(':');
			if (num != -1)
			{
				this.password = this.username.Substring(num + 1);
				this.username = this.username.Substring(0, num);
			}
			else
			{
				this.password = string.Empty;
			}
			this.modified = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme and host.</summary>
		/// <param name="schemeName">An Internet access protocol. </param>
		/// <param name="hostName">A DNS-style domain name or IP address. </param>
		// Token: 0x06002B13 RID: 11027 RVA: 0x00093EE8 File Offset: 0x000920E8
		public UriBuilder(string schemeName, string hostName)
		{
			this.Scheme = schemeName;
			this.Host = hostName;
			this.port = -1;
			this.Path = string.Empty;
			this.query = string.Empty;
			this.fragment = string.Empty;
			this.username = string.Empty;
			this.password = string.Empty;
			this.modified = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, and port.</summary>
		/// <param name="scheme">An Internet access protocol. </param>
		/// <param name="host">A DNS-style domain name or IP address. </param>
		/// <param name="portNumber">An IP port number for the service. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="portNumber" /> is less than 0 or greater than 65,535. </exception>
		// Token: 0x06002B14 RID: 11028 RVA: 0x00093F50 File Offset: 0x00092150
		public UriBuilder(string scheme, string host, int portNumber) : this(scheme, host)
		{
			this.Port = portNumber;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, port number, and path.</summary>
		/// <param name="scheme">An Internet access protocol. </param>
		/// <param name="host">A DNS-style domain name or IP address. </param>
		/// <param name="port">An IP port number for the service. </param>
		/// <param name="pathValue">The path to the Internet resource. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than 0 or greater than 65,535. </exception>
		// Token: 0x06002B15 RID: 11029 RVA: 0x00093F64 File Offset: 0x00092164
		public UriBuilder(string scheme, string host, int port, string pathValue) : this(scheme, host, port)
		{
			this.Path = pathValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, port number, path and query string or fragment identifier.</summary>
		/// <param name="scheme">An Internet access protocol. </param>
		/// <param name="host">A DNS-style domain name or IP address. </param>
		/// <param name="port">An IP port number for the service. </param>
		/// <param name="path">The path to the Internet resource. </param>
		/// <param name="extraValue">A query string or fragment identifier. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="extraValue" /> is neither null nor <see cref="F:System.String.Empty" />, nor does a valid fragment identifier begin with a number sign (#), nor a valid query string begin with a question mark (?). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than 0 or greater than 65,535. </exception>
		// Token: 0x06002B16 RID: 11030 RVA: 0x00093F78 File Offset: 0x00092178
		public UriBuilder(string scheme, string host, int port, string pathValue, string extraValue) : this(scheme, host, port, pathValue)
		{
			if (extraValue == null || extraValue.Length == 0)
			{
				return;
			}
			if (extraValue[0] == '#')
			{
				this.Fragment = extraValue.Remove(0, 1);
			}
			else
			{
				if (extraValue[0] != '?')
				{
					throw new ArgumentException("extraValue");
				}
				this.Query = extraValue.Remove(0, 1);
			}
		}

		/// <summary>Gets or sets the fragment portion of the URI.</summary>
		/// <returns>The fragment portion of the URI. The fragment identifier ("#") is added to the beginning of the fragment.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06002B17 RID: 11031 RVA: 0x00093FF8 File Offset: 0x000921F8
		// (set) Token: 0x06002B18 RID: 11032 RVA: 0x00094000 File Offset: 0x00092200
		public string Fragment
		{
			get
			{
				return this.fragment;
			}
			set
			{
				this.fragment = value;
				if (this.fragment == null)
				{
					this.fragment = string.Empty;
				}
				else if (this.fragment.Length > 0)
				{
					this.fragment = "#" + value.Replace("%23", "#");
				}
				this.modified = true;
			}
		}

		/// <summary>Gets or sets the Domain Name System (DNS) host name or IP address of a server.</summary>
		/// <returns>The DNS host name or IP address of the server.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x00094068 File Offset: 0x00092268
		// (set) Token: 0x06002B1A RID: 11034 RVA: 0x00094070 File Offset: 0x00092270
		public string Host
		{
			get
			{
				return this.host;
			}
			set
			{
				this.host = ((value != null) ? value : string.Empty);
				this.modified = true;
			}
		}

		/// <summary>Gets or sets the password associated with the user that accesses the URI.</summary>
		/// <returns>The password of the user that accesses the URI.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x00094090 File Offset: 0x00092290
		// (set) Token: 0x06002B1C RID: 11036 RVA: 0x00094098 File Offset: 0x00092298
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = ((value != null) ? value : string.Empty);
				this.modified = true;
			}
		}

		/// <summary>Gets or sets the path to the resource referenced by the URI.</summary>
		/// <returns>The path to the resource referenced by the URI.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06002B1D RID: 11037 RVA: 0x000940B8 File Offset: 0x000922B8
		// (set) Token: 0x06002B1E RID: 11038 RVA: 0x000940C0 File Offset: 0x000922C0
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.path = "/";
				}
				else
				{
					this.path = System.Uri.EscapeString(value.Replace('\\', '/'), false, true, true);
				}
				this.modified = true;
			}
		}

		/// <summary>Gets or sets the port number of the URI.</summary>
		/// <returns>The port number of the URI.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port cannot be set to a value less than 0 or greater than 65,535. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x00094110 File Offset: 0x00092310
		// (set) Token: 0x06002B20 RID: 11040 RVA: 0x00094118 File Offset: 0x00092318
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.port = value;
				this.modified = true;
			}
		}

		/// <summary>Gets or sets any query information included in the URI.</summary>
		/// <returns>The query information included in the URI.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x00094148 File Offset: 0x00092348
		// (set) Token: 0x06002B22 RID: 11042 RVA: 0x00094150 File Offset: 0x00092350
		public string Query
		{
			get
			{
				return this.query;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.query = string.Empty;
				}
				else
				{
					this.query = "?" + value;
				}
				this.modified = true;
			}
		}

		/// <summary>Gets or sets the scheme name of the URI.</summary>
		/// <returns>The scheme of the URI.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The scheme cannot be set to an invalid scheme name. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x0009418C File Offset: 0x0009238C
		// (set) Token: 0x06002B24 RID: 11044 RVA: 0x00094194 File Offset: 0x00092394
		public string Scheme
		{
			get
			{
				return this.scheme;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				int num = value.IndexOf(':');
				if (num != -1)
				{
					value = value.Substring(0, num);
				}
				this.scheme = value.ToLower();
				this.modified = true;
			}
		}

		/// <summary>Gets the <see cref="T:System.Uri" /> instance constructed by the specified <see cref="T:System.UriBuilder" /> instance.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI constructed by the <see cref="T:System.UriBuilder" />.</returns>
		/// <exception cref="T:System.UriFormatException">The URI constructed by the <see cref="T:System.UriBuilder" /> properties is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x000941DC File Offset: 0x000923DC
		public System.Uri Uri
		{
			get
			{
				if (!this.modified)
				{
					return this.uri;
				}
				this.uri = new System.Uri(this.ToString(), true);
				this.modified = false;
				return this.uri;
			}
		}

		/// <summary>The user name associated with the user that accesses the URI.</summary>
		/// <returns>The user name of the user that accesses the URI.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x00094210 File Offset: 0x00092410
		// (set) Token: 0x06002B27 RID: 11047 RVA: 0x00094218 File Offset: 0x00092418
		public string UserName
		{
			get
			{
				return this.username;
			}
			set
			{
				this.username = ((value != null) ? value : string.Empty);
				this.modified = true;
			}
		}

		/// <summary>Compares an existing <see cref="T:System.Uri" /> instance with the contents of the <see cref="T:System.UriBuilder" /> for equality.</summary>
		/// <returns>true if <paramref name="rparam" /> represents the same <see cref="T:System.Uri" /> as the <see cref="T:System.Uri" /> constructed by this <see cref="T:System.UriBuilder" /> instance; otherwise, false.</returns>
		/// <param name="rparam">The object to compare with the current instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B28 RID: 11048 RVA: 0x00094238 File Offset: 0x00092438
		public override bool Equals(object rparam)
		{
			return rparam != null && this.Uri.Equals(rparam.ToString());
		}

		/// <summary>Returns the hash code for the URI.</summary>
		/// <returns>The hash code generated for the URI.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002B29 RID: 11049 RVA: 0x00094258 File Offset: 0x00092458
		public override int GetHashCode()
		{
			return this.Uri.GetHashCode();
		}

		/// <summary>Returns the display string for the specified <see cref="T:System.UriBuilder" /> instance.</summary>
		/// <returns>The string that contains the unescaped display string of the <see cref="T:System.UriBuilder" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002B2A RID: 11050 RVA: 0x00094268 File Offset: 0x00092468
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.scheme);
			stringBuilder.Append("://");
			if (this.username != string.Empty)
			{
				stringBuilder.Append(this.username);
				if (this.password != string.Empty)
				{
					stringBuilder.Append(":" + this.password);
				}
				stringBuilder.Append('@');
			}
			stringBuilder.Append(this.host);
			if (this.port > 0)
			{
				stringBuilder.Append(":" + this.port);
			}
			if (this.path != string.Empty && stringBuilder[stringBuilder.Length - 1] != '/' && this.path.Length > 0 && this.path[0] != '/')
			{
				stringBuilder.Append('/');
			}
			stringBuilder.Append(this.path);
			stringBuilder.Append(this.query);
			stringBuilder.Append(this.fragment);
			return stringBuilder.ToString();
		}

		// Token: 0x04001B25 RID: 6949
		private string scheme;

		// Token: 0x04001B26 RID: 6950
		private string host;

		// Token: 0x04001B27 RID: 6951
		private int port;

		// Token: 0x04001B28 RID: 6952
		private string path;

		// Token: 0x04001B29 RID: 6953
		private string query;

		// Token: 0x04001B2A RID: 6954
		private string fragment;

		// Token: 0x04001B2B RID: 6955
		private string username;

		// Token: 0x04001B2C RID: 6956
		private string password;

		// Token: 0x04001B2D RID: 6957
		private System.Uri uri;

		// Token: 0x04001B2E RID: 6958
		private bool modified;
	}
}
