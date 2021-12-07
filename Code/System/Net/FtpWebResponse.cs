using System;
using System.IO;

namespace System.Net
{
	/// <summary>Encapsulates a File Transfer Protocol (FTP) server's response to a request.</summary>
	// Token: 0x0200030E RID: 782
	public class FtpWebResponse : WebResponse
	{
		// Token: 0x06001B33 RID: 6963 RVA: 0x0004D7EC File Offset: 0x0004B9EC
		internal FtpWebResponse(FtpWebRequest request, System.Uri uri, string method, bool keepAlive)
		{
			this.request = request;
			this.uri = uri;
			this.method = method;
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0004D848 File Offset: 0x0004BA48
		internal FtpWebResponse(FtpWebRequest request, System.Uri uri, string method, FtpStatusCode statusCode, string statusDescription)
		{
			this.request = request;
			this.uri = uri;
			this.method = method;
			this.statusCode = statusCode;
			this.statusDescription = statusDescription;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0004D8B4 File Offset: 0x0004BAB4
		internal FtpWebResponse(FtpWebRequest request, System.Uri uri, string method, FtpStatus status) : this(request, uri, method, status.StatusCode, status.StatusDescription)
		{
		}

		/// <summary>Gets the length of the data received from the FTP server.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that contains the number of bytes of data received from the FTP server. </returns>
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0004D8D8 File Offset: 0x0004BAD8
		public override long ContentLength
		{
			get
			{
				return this.contentLength;
			}
		}

		/// <summary>Gets an empty <see cref="T:System.Net.WebHeaderCollection" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Net.WebHeaderCollection" /> object.</returns>
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x0004D8E0 File Offset: 0x0004BAE0
		public override WebHeaderCollection Headers
		{
			get
			{
				return new WebHeaderCollection();
			}
		}

		/// <summary>Gets the URI that sent the response to the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that identifies the resource associated with this response.</returns>
		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x0004D8E8 File Offset: 0x0004BAE8
		public override System.Uri ResponseUri
		{
			get
			{
				return this.uri;
			}
		}

		/// <summary>Gets the date and time that a file on an FTP server was last modified.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the last modified date and time for a file.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x0004D8F0 File Offset: 0x0004BAF0
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x0004D8F8 File Offset: 0x0004BAF8
		public DateTime LastModified
		{
			get
			{
				return this.lastModified;
			}
			internal set
			{
				this.lastModified = value;
			}
		}

		/// <summary>Gets the message sent by the FTP server when a connection is established prior to logon.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the banner message sent by the server; otherwise, <see cref="F:System.String.Empty" /> if no message is sent.</returns>
		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x0004D904 File Offset: 0x0004BB04
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x0004D90C File Offset: 0x0004BB0C
		public string BannerMessage
		{
			get
			{
				return this.bannerMessage;
			}
			internal set
			{
				this.bannerMessage = value;
			}
		}

		/// <summary>Gets the message sent by the FTP server when authentication is complete.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the welcome message sent by the server; otherwise, <see cref="F:System.String.Empty" /> if no message is sent.</returns>
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x0004D918 File Offset: 0x0004BB18
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x0004D920 File Offset: 0x0004BB20
		public string WelcomeMessage
		{
			get
			{
				return this.welcomeMessage;
			}
			internal set
			{
				this.welcomeMessage = value;
			}
		}

		/// <summary>Gets the message sent by the server when the FTP session is ending.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the exit message sent by the server; otherwise, <see cref="F:System.String.Empty" /> if no message is sent.</returns>
		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x0004D92C File Offset: 0x0004BB2C
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x0004D934 File Offset: 0x0004BB34
		public string ExitMessage
		{
			get
			{
				return this.exitMessage;
			}
			internal set
			{
				this.exitMessage = value;
			}
		}

		/// <summary>Gets the most recent status code sent from the FTP server.</summary>
		/// <returns>An <see cref="T:System.Net.FtpStatusCode" /> value that indicates the most recent status code returned with this response.</returns>
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x0004D940 File Offset: 0x0004BB40
		// (set) Token: 0x06001B42 RID: 6978 RVA: 0x0004D948 File Offset: 0x0004BB48
		public FtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
			private set
			{
				this.statusCode = value;
			}
		}

		/// <summary>Gets text that describes a status code sent from the FTP server.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the status code and message returned with this response.</returns>
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x0004D954 File Offset: 0x0004BB54
		// (set) Token: 0x06001B44 RID: 6980 RVA: 0x0004D95C File Offset: 0x0004BB5C
		public string StatusDescription
		{
			get
			{
				return this.statusDescription;
			}
			private set
			{
				this.statusDescription = value;
			}
		}

		/// <summary>Frees the resources held by the response.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001B45 RID: 6981 RVA: 0x0004D968 File Offset: 0x0004BB68
		public override void Close()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (this.stream != null)
			{
				this.stream.Close();
				if (this.stream == Stream.Null)
				{
					this.request.OperationCompleted();
				}
			}
			this.stream = null;
		}

		/// <summary>Retrieves the stream that contains response data sent from an FTP server.</summary>
		/// <returns>A readable <see cref="T:System.IO.Stream" /> instance that contains data returned with the response; otherwise, <see cref="F:System.IO.Stream.Null" /> if no response data was returned by the server.</returns>
		/// <exception cref="T:System.InvalidOperationException">The response did not return a data stream. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001B46 RID: 6982 RVA: 0x0004D9C0 File Offset: 0x0004BBC0
		public override Stream GetResponseStream()
		{
			if (this.stream == null)
			{
				return Stream.Null;
			}
			if (this.method != "RETR" && this.method != "NLST")
			{
				this.CheckDisposed();
			}
			return this.stream;
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x0004DA20 File Offset: 0x0004BC20
		// (set) Token: 0x06001B47 RID: 6983 RVA: 0x0004DA14 File Offset: 0x0004BC14
		internal Stream Stream
		{
			get
			{
				return this.stream;
			}
			set
			{
				this.stream = value;
			}
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0004DA28 File Offset: 0x0004BC28
		internal void UpdateStatus(FtpStatus status)
		{
			this.statusCode = status.StatusCode;
			this.statusDescription = status.StatusDescription;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0004DA44 File Offset: 0x0004BC44
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0004DA64 File Offset: 0x0004BC64
		internal bool IsFinal()
		{
			return this.statusCode >= FtpStatusCode.CommandOK;
		}

		// Token: 0x040010D9 RID: 4313
		private Stream stream;

		// Token: 0x040010DA RID: 4314
		private System.Uri uri;

		// Token: 0x040010DB RID: 4315
		private FtpStatusCode statusCode;

		// Token: 0x040010DC RID: 4316
		private DateTime lastModified = DateTime.MinValue;

		// Token: 0x040010DD RID: 4317
		private string bannerMessage = string.Empty;

		// Token: 0x040010DE RID: 4318
		private string welcomeMessage = string.Empty;

		// Token: 0x040010DF RID: 4319
		private string exitMessage = string.Empty;

		// Token: 0x040010E0 RID: 4320
		private string statusDescription;

		// Token: 0x040010E1 RID: 4321
		private string method;

		// Token: 0x040010E2 RID: 4322
		private bool disposed;

		// Token: 0x040010E3 RID: 4323
		private FtpWebRequest request;

		// Token: 0x040010E4 RID: 4324
		internal long contentLength = -1L;
	}
}
