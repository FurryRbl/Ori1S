using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Net
{
	/// <summary>Provides common methods for sending data to and receiving data from a resource identified by a URI.</summary>
	// Token: 0x02000410 RID: 1040
	[ComVisible(true)]
	public class WebClient : System.ComponentModel.Component
	{
		// Token: 0x060024BD RID: 9405 RVA: 0x0006E7E0 File Offset: 0x0006C9E0
		static WebClient()
		{
			int num = 0;
			int i = 48;
			while (i <= 57)
			{
				WebClient.hexBytes[num] = (byte)i;
				i++;
				num++;
			}
			int j = 97;
			while (j <= 102)
			{
				WebClient.hexBytes[num] = (byte)j;
				j++;
				num++;
			}
		}

		/// <summary>Occurs when an asynchronous data download operation completes.</summary>
		// Token: 0x14000053 RID: 83
		// (add) Token: 0x060024BE RID: 9406 RVA: 0x0006E848 File Offset: 0x0006CA48
		// (remove) Token: 0x060024BF RID: 9407 RVA: 0x0006E864 File Offset: 0x0006CA64
		public event DownloadDataCompletedEventHandler DownloadDataCompleted;

		/// <summary>Occurs when an asynchronous file download operation completes.</summary>
		// Token: 0x14000054 RID: 84
		// (add) Token: 0x060024C0 RID: 9408 RVA: 0x0006E880 File Offset: 0x0006CA80
		// (remove) Token: 0x060024C1 RID: 9409 RVA: 0x0006E89C File Offset: 0x0006CA9C
		public event System.ComponentModel.AsyncCompletedEventHandler DownloadFileCompleted;

		/// <summary>Occurs when an asynchronous download operation successfully transfers some or all of the data.</summary>
		// Token: 0x14000055 RID: 85
		// (add) Token: 0x060024C2 RID: 9410 RVA: 0x0006E8B8 File Offset: 0x0006CAB8
		// (remove) Token: 0x060024C3 RID: 9411 RVA: 0x0006E8D4 File Offset: 0x0006CAD4
		public event DownloadProgressChangedEventHandler DownloadProgressChanged;

		/// <summary>Occurs when an asynchronous resource-download operation completes.</summary>
		// Token: 0x14000056 RID: 86
		// (add) Token: 0x060024C4 RID: 9412 RVA: 0x0006E8F0 File Offset: 0x0006CAF0
		// (remove) Token: 0x060024C5 RID: 9413 RVA: 0x0006E90C File Offset: 0x0006CB0C
		public event DownloadStringCompletedEventHandler DownloadStringCompleted;

		/// <summary>Occurs when an asynchronous operation to open a stream containing a resource completes.</summary>
		// Token: 0x14000057 RID: 87
		// (add) Token: 0x060024C6 RID: 9414 RVA: 0x0006E928 File Offset: 0x0006CB28
		// (remove) Token: 0x060024C7 RID: 9415 RVA: 0x0006E944 File Offset: 0x0006CB44
		public event OpenReadCompletedEventHandler OpenReadCompleted;

		/// <summary>Occurs when an asynchronous operation to open a stream to write data to a resource completes.</summary>
		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060024C8 RID: 9416 RVA: 0x0006E960 File Offset: 0x0006CB60
		// (remove) Token: 0x060024C9 RID: 9417 RVA: 0x0006E97C File Offset: 0x0006CB7C
		public event OpenWriteCompletedEventHandler OpenWriteCompleted;

		/// <summary>Occurs when an asynchronous data-upload operation completes.</summary>
		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060024CA RID: 9418 RVA: 0x0006E998 File Offset: 0x0006CB98
		// (remove) Token: 0x060024CB RID: 9419 RVA: 0x0006E9B4 File Offset: 0x0006CBB4
		public event UploadDataCompletedEventHandler UploadDataCompleted;

		/// <summary>Occurs when an asynchronous file-upload operation completes.</summary>
		// Token: 0x1400005A RID: 90
		// (add) Token: 0x060024CC RID: 9420 RVA: 0x0006E9D0 File Offset: 0x0006CBD0
		// (remove) Token: 0x060024CD RID: 9421 RVA: 0x0006E9EC File Offset: 0x0006CBEC
		public event UploadFileCompletedEventHandler UploadFileCompleted;

		/// <summary>Occurs when an asynchronous upload operation successfully transfers some or all of the data.</summary>
		// Token: 0x1400005B RID: 91
		// (add) Token: 0x060024CE RID: 9422 RVA: 0x0006EA08 File Offset: 0x0006CC08
		// (remove) Token: 0x060024CF RID: 9423 RVA: 0x0006EA24 File Offset: 0x0006CC24
		public event UploadProgressChangedEventHandler UploadProgressChanged;

		/// <summary>Occurs when an asynchronous string-upload operation completes.</summary>
		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060024D0 RID: 9424 RVA: 0x0006EA40 File Offset: 0x0006CC40
		// (remove) Token: 0x060024D1 RID: 9425 RVA: 0x0006EA5C File Offset: 0x0006CC5C
		public event UploadStringCompletedEventHandler UploadStringCompleted;

		/// <summary>Occurs when an asynchronous upload of a name/value collection completes.</summary>
		// Token: 0x1400005D RID: 93
		// (add) Token: 0x060024D2 RID: 9426 RVA: 0x0006EA78 File Offset: 0x0006CC78
		// (remove) Token: 0x060024D3 RID: 9427 RVA: 0x0006EA94 File Offset: 0x0006CC94
		public event UploadValuesCompletedEventHandler UploadValuesCompleted;

		/// <summary>Gets or sets the base URI for requests made by a <see cref="T:System.Net.WebClient" />.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the base URI for requests made by a <see cref="T:System.Net.WebClient" /> or <see cref="F:System.String.Empty" /> if no base address has been specified.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.WebClient.BaseAddress" /> is set to an invalid URI. The inner exception may contain information that will help you locate the error.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x0006EAB0 File Offset: 0x0006CCB0
		// (set) Token: 0x060024D5 RID: 9429 RVA: 0x0006EAEC File Offset: 0x0006CCEC
		public string BaseAddress
		{
			get
			{
				if (this.baseString == null && this.baseAddress == null)
				{
					return string.Empty;
				}
				this.baseString = this.baseAddress.ToString();
				return this.baseString;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.baseAddress = null;
				}
				else
				{
					this.baseAddress = new System.Uri(value);
				}
			}
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x0006EB18 File Offset: 0x0006CD18
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets or sets the application's cache policy for any resources obtained by this WebClient instance using <see cref="T:System.Net.WebRequest" /> objects.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCachePolicy" /> object that represents the application's caching requirements.</returns>
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060024D7 RID: 9431 RVA: 0x0006EB20 File Offset: 0x0006CD20
		// (set) Token: 0x060024D8 RID: 9432 RVA: 0x0006EB28 File Offset: 0x0006CD28
		[MonoTODO]
		public System.Net.Cache.RequestCachePolicy CachePolicy
		{
			get
			{
				throw WebClient.GetMustImplement();
			}
			set
			{
				throw WebClient.GetMustImplement();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>true if the default credentials are used; otherwise false. The default value is false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="USERNAME" />
		/// </PermissionSet>
		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x0006EB30 File Offset: 0x0006CD30
		// (set) Token: 0x060024DA RID: 9434 RVA: 0x0006EB38 File Offset: 0x0006CD38
		[MonoTODO]
		public bool UseDefaultCredentials
		{
			get
			{
				throw WebClient.GetMustImplement();
			}
			set
			{
				throw WebClient.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the network credentials that are sent to the host and used to authenticate the request.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> containing the authentication credentials for the request. The default is null.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060024DB RID: 9435 RVA: 0x0006EB40 File Offset: 0x0006CD40
		// (set) Token: 0x060024DC RID: 9436 RVA: 0x0006EB48 File Offset: 0x0006CD48
		public ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.credentials = value;
			}
		}

		/// <summary>Gets or sets a collection of header name/value pairs associated with the request.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> containing header name/value pairs associated with this request.</returns>
		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060024DD RID: 9437 RVA: 0x0006EB54 File Offset: 0x0006CD54
		// (set) Token: 0x060024DE RID: 9438 RVA: 0x0006EB74 File Offset: 0x0006CD74
		public WebHeaderCollection Headers
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = new WebHeaderCollection();
				}
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		/// <summary>Gets or sets a collection of query name/value pairs associated with the request.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains query name/value pairs associated with the request. If no pairs are associated with the request, the value is an empty <see cref="T:System.Collections.Specialized.NameValueCollection" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060024DF RID: 9439 RVA: 0x0006EB80 File Offset: 0x0006CD80
		// (set) Token: 0x060024E0 RID: 9440 RVA: 0x0006EBA0 File Offset: 0x0006CDA0
		public System.Collections.Specialized.NameValueCollection QueryString
		{
			get
			{
				if (this.queryString == null)
				{
					this.queryString = new System.Collections.Specialized.NameValueCollection();
				}
				return this.queryString;
			}
			set
			{
				this.queryString = value;
			}
		}

		/// <summary>Gets a collection of header name/value pairs associated with the response.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> containing header name/value pairs associated with the response, or null if no response has been received.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x0006EBAC File Offset: 0x0006CDAC
		public WebHeaderCollection ResponseHeaders
		{
			get
			{
				return this.responseHeaders;
			}
		}

		/// <summary>Gets and sets the <see cref="T:System.Text.Encoding" /> used to upload and download strings.</summary>
		/// <returns>A <see cref="T:System.Text.Encoding" /> that is used to encode strings. The default value of this property is the encoding returned by <see cref="P:System.Text.Encoding.Default" />.</returns>
		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x0006EBB4 File Offset: 0x0006CDB4
		// (set) Token: 0x060024E3 RID: 9443 RVA: 0x0006EBBC File Offset: 0x0006CDBC
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Encoding");
				}
				this.encoding = value;
			}
		}

		/// <summary>Gets or sets the proxy used by this <see cref="T:System.Net.WebClient" /> object.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> instance used to send requests.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Net.WebClient.Proxy" /> is set to null. </exception>
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x0006EBD8 File Offset: 0x0006CDD8
		// (set) Token: 0x060024E5 RID: 9445 RVA: 0x0006EBE0 File Offset: 0x0006CDE0
		public IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
			set
			{
				this.proxy = value;
			}
		}

		/// <summary>Gets whether a Web request is in progress.</summary>
		/// <returns>true if the Web request is still in progress; otherwise false.</returns>
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060024E6 RID: 9446 RVA: 0x0006EBEC File Offset: 0x0006CDEC
		public bool IsBusy
		{
			get
			{
				return this.is_busy;
			}
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x0006EBF4 File Offset: 0x0006CDF4
		private void CheckBusy()
		{
			if (this.IsBusy)
			{
				throw new NotSupportedException("WebClient does not support conccurent I/O operations.");
			}
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x0006EC0C File Offset: 0x0006CE0C
		private void SetBusy()
		{
			lock (this)
			{
				this.CheckBusy();
				this.is_busy = true;
			}
		}

		/// <summary>Downloads the resource with the specified URI as a <see cref="T:System.Byte" /> array.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <param name="address">The URI from which to download data. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading data. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024E9 RID: 9449 RVA: 0x0006EC58 File Offset: 0x0006CE58
		public byte[] DownloadData(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.DownloadData(this.CreateUri(address));
		}

		/// <summary>Downloads the resource with the specified URI as a <see cref="T:System.Byte" /> array.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <param name="address">The URI represented by the <see cref="T:System.Uri" />  object, from which to download data.</param>
		// Token: 0x060024EA RID: 9450 RVA: 0x0006EC78 File Offset: 0x0006CE78
		public byte[] DownloadData(System.Uri address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			byte[] result;
			try
			{
				this.SetBusy();
				this.async = false;
				result = this.DownloadDataCore(address, null);
			}
			finally
			{
				this.is_busy = false;
			}
			return result;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x0006ECE4 File Offset: 0x0006CEE4
		private byte[] DownloadDataCore(System.Uri address, object userToken)
		{
			WebRequest webRequest = null;
			byte[] result;
			try
			{
				webRequest = this.SetupRequest(address);
				WebResponse webResponse = this.GetWebResponse(webRequest);
				Stream responseStream = webResponse.GetResponseStream();
				result = this.ReadAll(responseStream, (int)webResponse.ContentLength, userToken);
			}
			catch (ThreadInterruptedException)
			{
				if (webRequest != null)
				{
					webRequest.Abort();
				}
				throw;
			}
			catch (WebException ex)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new WebException("An error occurred performing a WebClient request.", innerException);
			}
			return result;
		}

		/// <summary>Downloads the resource with the specified URI to a local file.</summary>
		/// <param name="address">The URI from which to download data. </param>
		/// <param name="fileName">The name of the local file that is to receive the data. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="filename" /> is null or <see cref="F:System.String.Empty" />.-or-The file does not exist.-or- An error occurred while downloading data. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024EC RID: 9452 RVA: 0x0006EDA4 File Offset: 0x0006CFA4
		public void DownloadFile(string address, string fileName)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.DownloadFile(this.CreateUri(address), fileName);
		}

		/// <summary>Downloads the resource with the specified URI to a local file.</summary>
		/// <param name="address">The URI specified as a <see cref="T:System.String" />, from which to download data. </param>
		/// <param name="fileName">The name of the local file that is to receive the data. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="filename" /> is null or <see cref="F:System.String.Empty" />.-or- The file does not exist. -or- An error occurred while downloading data. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		// Token: 0x060024ED RID: 9453 RVA: 0x0006EDC8 File Offset: 0x0006CFC8
		public void DownloadFile(System.Uri address, string fileName)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			try
			{
				this.SetBusy();
				this.async = false;
				this.DownloadFileCore(address, fileName, null);
			}
			catch (WebException ex)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new WebException("An error occurred performing a WebClient request.", innerException);
			}
			finally
			{
				this.is_busy = false;
			}
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x0006EE88 File Offset: 0x0006D088
		private void DownloadFileCore(System.Uri address, string fileName, object userToken)
		{
			WebRequest webRequest = null;
			using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
			{
				try
				{
					webRequest = this.SetupRequest(address);
					WebResponse webResponse = this.GetWebResponse(webRequest);
					Stream responseStream = webResponse.GetResponseStream();
					int num = (int)webResponse.ContentLength;
					int num2 = (num > -1 && num <= 32768) ? num : 32768;
					byte[] array = new byte[num2];
					long num3 = 0L;
					int num4;
					while ((num4 = responseStream.Read(array, 0, num2)) != 0)
					{
						if (this.async)
						{
							num3 += (long)num4;
							this.OnDownloadProgressChanged(new DownloadProgressChangedEventArgs(num3, webResponse.ContentLength, userToken));
						}
						fileStream.Write(array, 0, num4);
					}
				}
				catch (ThreadInterruptedException)
				{
					if (webRequest != null)
					{
						webRequest.Abort();
					}
					throw;
				}
			}
		}

		/// <summary>Opens a readable stream for the data downloaded from a resource with the URI specified as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to read data from a resource.</returns>
		/// <param name="address">The URI specified as a <see cref="T:System.String" /> from which to download data. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, <paramref name="address" /> is invalid.-or- An error occurred while downloading data. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024EF RID: 9455 RVA: 0x0006EF9C File Offset: 0x0006D19C
		public Stream OpenRead(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenRead(this.CreateUri(address));
		}

		/// <summary>Opens a readable stream for the data downloaded from a resource with the URI specified as a <see cref="T:System.Uri" /></summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to read data from a resource.</returns>
		/// <param name="address">The URI specified as a <see cref="T:System.Uri" /> from which to download data. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, <paramref name="address" /> is invalid.-or- An error occurred while downloading data. </exception>
		// Token: 0x060024F0 RID: 9456 RVA: 0x0006EFBC File Offset: 0x0006D1BC
		public Stream OpenRead(System.Uri address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			Stream responseStream;
			try
			{
				this.SetBusy();
				this.async = false;
				WebRequest request = this.SetupRequest(address);
				WebResponse webResponse = this.GetWebResponse(request);
				responseStream = webResponse.GetResponseStream();
			}
			catch (WebException ex)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new WebException("An error occurred performing a WebClient request.", innerException);
			}
			finally
			{
				this.is_busy = false;
			}
			return responseStream;
		}

		/// <summary>Opens a stream for writing data to the specified resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024F1 RID: 9457 RVA: 0x0006F084 File Offset: 0x0006D284
		public Stream OpenWrite(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenWrite(this.CreateUri(address));
		}

		/// <summary>Opens a stream for writing data to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024F2 RID: 9458 RVA: 0x0006F0A4 File Offset: 0x0006D2A4
		public Stream OpenWrite(string address, string method)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenWrite(this.CreateUri(address), method);
		}

		/// <summary>Opens a stream for writing data to the specified resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		// Token: 0x060024F3 RID: 9459 RVA: 0x0006F0C8 File Offset: 0x0006D2C8
		public Stream OpenWrite(System.Uri address)
		{
			return this.OpenWrite(address, null);
		}

		/// <summary>Opens a stream for writing data to the specified resource, by using the specified method.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		// Token: 0x060024F4 RID: 9460 RVA: 0x0006F0D4 File Offset: 0x0006D2D4
		public Stream OpenWrite(System.Uri address, string method)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			Stream requestStream;
			try
			{
				this.SetBusy();
				this.async = false;
				WebRequest webRequest = this.SetupRequest(address, method, true);
				requestStream = webRequest.GetRequestStream();
			}
			catch (WebException ex)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new WebException("An error occurred performing a WebClient request.", innerException);
			}
			finally
			{
				this.is_busy = false;
			}
			return requestStream;
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x0006F190 File Offset: 0x0006D390
		private string DetermineMethod(System.Uri address, string method, bool is_upload)
		{
			if (method != null)
			{
				return method;
			}
			if (address.Scheme == System.Uri.UriSchemeFtp)
			{
				return (!is_upload) ? "RETR" : "STOR";
			}
			return (!is_upload) ? "GET" : "POST";
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="data">The data buffer to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null. -or-An error occurred while sending the data.-or- There was no response from the server hosting the resource. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024F6 RID: 9462 RVA: 0x0006F1E8 File Offset: 0x0006D3E8
		public byte[] UploadData(string address, byte[] data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadData(this.CreateUri(address), data);
		}

		/// <summary>Uploads a data buffer to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="method">The HTTP method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- An error occurred while uploading the data.-or- There was no response from the server hosting the resource. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024F7 RID: 9463 RVA: 0x0006F20C File Offset: 0x0006D40C
		public byte[] UploadData(string address, string method, byte[] data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadData(this.CreateUri(address), method, data);
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="data">The data buffer to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null. -or-An error occurred while sending the data.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x060024F8 RID: 9464 RVA: 0x0006F23C File Offset: 0x0006D43C
		public byte[] UploadData(System.Uri address, byte[] data)
		{
			return this.UploadData(address, null, data);
		}

		/// <summary>Uploads a data buffer to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="method">The HTTP method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- An error occurred while uploading the data.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x060024F9 RID: 9465 RVA: 0x0006F248 File Offset: 0x0006D448
		public byte[] UploadData(System.Uri address, string method, byte[] data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] result;
			try
			{
				this.SetBusy();
				this.async = false;
				result = this.UploadDataCore(address, method, data, null);
			}
			catch (WebException)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new WebException("An error occurred performing a WebClient request.", innerException);
			}
			finally
			{
				this.is_busy = false;
			}
			return result;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x0006F310 File Offset: 0x0006D510
		private byte[] UploadDataCore(System.Uri address, string method, byte[] data, object userToken)
		{
			WebRequest webRequest = this.SetupRequest(address, method, true);
			byte[] result;
			try
			{
				int num = data.Length;
				webRequest.ContentLength = (long)num;
				using (Stream requestStream = webRequest.GetRequestStream())
				{
					requestStream.Write(data, 0, num);
				}
				WebResponse webResponse = this.GetWebResponse(webRequest);
				Stream responseStream = webResponse.GetResponseStream();
				result = this.ReadAll(responseStream, (int)webResponse.ContentLength, userToken);
			}
			catch (ThreadInterruptedException)
			{
				if (webRequest != null)
				{
					webRequest.Abort();
				}
				throw;
			}
			return result;
		}

		/// <summary>Uploads the specified local file to a resource with the specified URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the file. For example, ftp://localhost/samplefile.txt.</param>
		/// <param name="fileName">The file to send to the resource. For example, "samplefile.txt".</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.-or- An error occurred while uploading the file.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024FB RID: 9467 RVA: 0x0006F3CC File Offset: 0x0006D5CC
		public byte[] UploadFile(string address, string fileName)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadFile(this.CreateUri(address), fileName);
		}

		/// <summary>Uploads the specified local file to a resource with the specified URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the file. For example, ftp://localhost/samplefile.txt.</param>
		/// <param name="fileName">The file to send to the resource. For example, "samplefile.txt".</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.-or- An error occurred while uploading the file.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x060024FC RID: 9468 RVA: 0x0006F3F0 File Offset: 0x0006D5F0
		public byte[] UploadFile(System.Uri address, string fileName)
		{
			return this.UploadFile(address, null, fileName);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the file.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.-or- An error occurred while uploading the file.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024FD RID: 9469 RVA: 0x0006F3FC File Offset: 0x0006D5FC
		public byte[] UploadFile(string address, string method, string fileName)
		{
			return this.UploadFile(this.CreateUri(address), method, fileName);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the file.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.-or- An error occurred while uploading the file.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x060024FE RID: 9470 RVA: 0x0006F410 File Offset: 0x0006D610
		public byte[] UploadFile(System.Uri address, string method, string fileName)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			byte[] result;
			try
			{
				this.SetBusy();
				this.async = false;
				result = this.UploadFileCore(address, method, fileName, null);
			}
			catch (WebException ex)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new WebException("An error occurred performing a WebClient request.", innerException);
			}
			finally
			{
				this.is_busy = false;
			}
			return result;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x0006F4D8 File Offset: 0x0006D6D8
		private byte[] UploadFileCore(System.Uri address, string method, string fileName, object userToken)
		{
			string text = this.Headers["Content-Type"];
			if (text != null)
			{
				string text2 = text.ToLower();
				if (text2.StartsWith("multipart/"))
				{
					throw new WebException("Content-Type cannot be set to a multipart type for this request.");
				}
			}
			else
			{
				text = "application/octet-stream";
			}
			string text3 = "------------" + DateTime.Now.Ticks.ToString("x");
			this.Headers["Content-Type"] = string.Format("multipart/form-data; boundary={0}", text3);
			Stream stream = null;
			Stream stream2 = null;
			byte[] result = null;
			fileName = Path.GetFullPath(fileName);
			WebRequest webRequest = null;
			try
			{
				stream2 = File.OpenRead(fileName);
				webRequest = this.SetupRequest(address, method, true);
				stream = webRequest.GetRequestStream();
				byte[] bytes = Encoding.ASCII.GetBytes("--" + text3 + "\r\n");
				stream.Write(bytes, 0, bytes.Length);
				string s = string.Format("Content-Disposition: form-data; name=\"file\"; filename=\"{0}\"\r\nContent-Type: {1}\r\n\r\n", Path.GetFileName(fileName), text);
				byte[] bytes2 = Encoding.UTF8.GetBytes(s);
				stream.Write(bytes2, 0, bytes2.Length);
				byte[] buffer = new byte[4096];
				int count;
				while ((count = stream2.Read(buffer, 0, 4096)) != 0)
				{
					stream.Write(buffer, 0, count);
				}
				stream.WriteByte(13);
				stream.WriteByte(10);
				stream.Write(bytes, 0, bytes.Length);
				stream.Close();
				stream = null;
				WebResponse webResponse = this.GetWebResponse(webRequest);
				Stream responseStream = webResponse.GetResponseStream();
				result = this.ReadAll(responseStream, (int)webResponse.ContentLength, userToken);
			}
			catch (ThreadInterruptedException)
			{
				if (webRequest != null)
				{
					webRequest.Abort();
				}
				throw;
			}
			finally
			{
				if (stream2 != null)
				{
					stream2.Close();
				}
				if (stream != null)
				{
					stream.Close();
				}
			}
			return result;
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the collection. </param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- There was no response from the server hosting the resource.-or- An error occurred while opening the stream.-or- The Content-type header is not null or "application/x-www-form-urlencoded". </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002500 RID: 9472 RVA: 0x0006F6DC File Offset: 0x0006D8DC
		public byte[] UploadValues(string address, System.Collections.Specialized.NameValueCollection data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadValues(this.CreateUri(address), data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the collection. </param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header value is not null and is not application/x-www-form-urlencoded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002501 RID: 9473 RVA: 0x0006F700 File Offset: 0x0006D900
		public byte[] UploadValues(string address, string method, System.Collections.Specialized.NameValueCollection data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadValues(this.CreateUri(address), method, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the collection. </param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- There was no response from the server hosting the resource.-or- An error occurred while opening the stream.-or- The Content-type header is not null or "application/x-www-form-urlencoded". </exception>
		// Token: 0x06002502 RID: 9474 RVA: 0x0006F730 File Offset: 0x0006D930
		public byte[] UploadValues(System.Uri address, System.Collections.Specialized.NameValueCollection data)
		{
			return this.UploadValues(address, null, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the collection. </param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header value is not null and is not application/x-www-form-urlencoded. </exception>
		// Token: 0x06002503 RID: 9475 RVA: 0x0006F73C File Offset: 0x0006D93C
		public byte[] UploadValues(System.Uri address, string method, System.Collections.Specialized.NameValueCollection data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] result;
			try
			{
				this.SetBusy();
				this.async = false;
				result = this.UploadValuesCore(address, method, data, null);
			}
			catch (WebException ex)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new WebException("An error occurred performing a WebClient request.", innerException);
			}
			finally
			{
				this.is_busy = false;
			}
			return result;
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x0006F804 File Offset: 0x0006DA04
		private byte[] UploadValuesCore(System.Uri uri, string method, System.Collections.Specialized.NameValueCollection data, object userToken)
		{
			string text = this.Headers["Content-Type"];
			if (text != null && string.Compare(text, WebClient.urlEncodedCType, true) != 0)
			{
				throw new WebException("Content-Type header cannot be changed from its default value for this request.");
			}
			this.Headers["Content-Type"] = WebClient.urlEncodedCType;
			WebRequest webRequest = this.SetupRequest(uri, method, true);
			byte[] result;
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				foreach (object obj in data)
				{
					string text2 = (string)obj;
					byte[] bytes = Encoding.UTF8.GetBytes(text2);
					WebClient.UrlEncodeAndWrite(memoryStream, bytes);
					memoryStream.WriteByte(61);
					bytes = Encoding.UTF8.GetBytes(data[text2]);
					WebClient.UrlEncodeAndWrite(memoryStream, bytes);
					memoryStream.WriteByte(38);
				}
				int num = (int)memoryStream.Length;
				if (num > 0)
				{
					memoryStream.SetLength((long)(--num));
				}
				byte[] buffer = memoryStream.GetBuffer();
				webRequest.ContentLength = (long)num;
				using (Stream requestStream = webRequest.GetRequestStream())
				{
					requestStream.Write(buffer, 0, num);
				}
				memoryStream.Close();
				WebResponse webResponse = this.GetWebResponse(webRequest);
				Stream responseStream = webResponse.GetResponseStream();
				result = this.ReadAll(responseStream, (int)webResponse.ContentLength, userToken);
			}
			catch (ThreadInterruptedException)
			{
				webRequest.Abort();
				throw;
			}
			return result;
		}

		/// <summary>Downloads the requested resource as a <see cref="T:System.String" />. The resource to download is specified as a <see cref="T:System.String" /> containing the URI.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the requested resource.</returns>
		/// <param name="address">A <see cref="T:System.String" /> containing the URI to download.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002505 RID: 9477 RVA: 0x0006F9D4 File Offset: 0x0006DBD4
		public string DownloadString(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.encoding.GetString(this.DownloadData(this.CreateUri(address)));
		}

		/// <summary>Downloads the requested resource as a <see cref="T:System.String" />. The resource to download is specified as a <see cref="T:System.Uri" />.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the requested resource.</returns>
		/// <param name="address">A <see cref="T:System.Uri" /> object containing the URI to download.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		// Token: 0x06002506 RID: 9478 RVA: 0x0006FA00 File Offset: 0x0006DC00
		public string DownloadString(System.Uri address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.encoding.GetString(this.DownloadData(this.CreateUri(address)));
		}

		/// <summary>Uploads the specified string to the specified resource, using the POST method.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the string. For Http resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002507 RID: 9479 RVA: 0x0006FA34 File Offset: 0x0006DC34
		public string UploadString(string address, string data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] bytes = this.UploadData(address, this.encoding.GetBytes(data));
			return this.encoding.GetString(bytes);
		}

		/// <summary>Uploads the specified string to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the file. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method. </param>
		/// <param name="method">The HTTP method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.-or-<paramref name="method" /> cannot be used to send content.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002508 RID: 9480 RVA: 0x0006FA84 File Offset: 0x0006DC84
		public string UploadString(string address, string method, string data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] bytes = this.UploadData(address, method, this.encoding.GetBytes(data));
			return this.encoding.GetString(bytes);
		}

		/// <summary>Uploads the specified string to the specified resource, using the POST method.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the string. For Http resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x06002509 RID: 9481 RVA: 0x0006FAD4 File Offset: 0x0006DCD4
		public string UploadString(System.Uri address, string data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] bytes = this.UploadData(address, this.encoding.GetBytes(data));
			return this.encoding.GetString(bytes);
		}

		/// <summary>Uploads the specified string to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the file. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method. </param>
		/// <param name="method">The HTTP method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.-or-<paramref name="method" /> cannot be used to send content.</exception>
		// Token: 0x0600250A RID: 9482 RVA: 0x0006FB2C File Offset: 0x0006DD2C
		public string UploadString(System.Uri address, string method, string data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] bytes = this.UploadData(address, method, this.encoding.GetBytes(data));
			return this.encoding.GetString(bytes);
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x0006FB84 File Offset: 0x0006DD84
		private System.Uri CreateUri(string address)
		{
			return this.MakeUri(address);
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x0006FB90 File Offset: 0x0006DD90
		private System.Uri CreateUri(System.Uri address)
		{
			string query = address.Query;
			if (string.IsNullOrEmpty(query))
			{
				query = this.GetQueryString(true);
			}
			if (this.baseAddress == null && query == null)
			{
				return address;
			}
			if (this.baseAddress == null)
			{
				return new System.Uri(address.ToString() + query, query != null);
			}
			if (query == null)
			{
				return new System.Uri(this.baseAddress, address.ToString());
			}
			return new System.Uri(this.baseAddress, address.ToString() + query, query != null);
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x0006FC30 File Offset: 0x0006DE30
		private string GetQueryString(bool add_qmark)
		{
			if (this.queryString == null || this.queryString.Count == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (add_qmark)
			{
				stringBuilder.Append('?');
			}
			foreach (object obj in this.queryString)
			{
				string text = (string)obj;
				stringBuilder.AppendFormat("{0}={1}&", text, this.UrlEncode(this.queryString[text]));
			}
			if (stringBuilder.Length != 0)
			{
				stringBuilder.Length--;
			}
			if (stringBuilder.Length == 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x0006FD18 File Offset: 0x0006DF18
		private System.Uri MakeUri(string path)
		{
			string text = this.GetQueryString(true);
			if (this.baseAddress == null && text == null)
			{
				try
				{
					return new System.Uri(path);
				}
				catch (ArgumentNullException)
				{
					if (Environment.UnityWebSecurityEnabled)
					{
						throw;
					}
					path = Path.GetFullPath(path);
					return new System.Uri("file://" + path);
				}
				catch (System.UriFormatException)
				{
					if (Environment.UnityWebSecurityEnabled)
					{
						throw;
					}
					path = Path.GetFullPath(path);
					return new System.Uri("file://" + path);
				}
			}
			if (this.baseAddress == null)
			{
				return new System.Uri(path + text, text != null);
			}
			if (text == null)
			{
				return new System.Uri(this.baseAddress, path);
			}
			return new System.Uri(this.baseAddress, path + text, text != null);
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x0006FE3C File Offset: 0x0006E03C
		private WebRequest SetupRequest(System.Uri uri)
		{
			WebRequest webRequest = this.GetWebRequest(uri);
			if (this.Proxy != null)
			{
				webRequest.Proxy = this.Proxy;
			}
			webRequest.Credentials = this.credentials;
			if (this.headers != null && this.headers.Count != 0 && webRequest is HttpWebRequest)
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)webRequest;
				string text = this.headers["Expect"];
				string text2 = this.headers["Content-Type"];
				string text3 = this.headers["Accept"];
				string text4 = this.headers["Connection"];
				string text5 = this.headers["User-Agent"];
				string text6 = this.headers["Referer"];
				this.headers.RemoveInternal("Expect");
				this.headers.RemoveInternal("Content-Type");
				this.headers.RemoveInternal("Accept");
				this.headers.RemoveInternal("Connection");
				this.headers.RemoveInternal("Referer");
				this.headers.RemoveInternal("User-Agent");
				webRequest.Headers = this.headers;
				if (text != null && text.Length > 0)
				{
					httpWebRequest.Expect = text;
				}
				if (text3 != null && text3.Length > 0)
				{
					httpWebRequest.Accept = text3;
				}
				if (text2 != null && text2.Length > 0)
				{
					httpWebRequest.ContentType = text2;
				}
				if (text4 != null && text4.Length > 0)
				{
					httpWebRequest.Connection = text4;
				}
				if (text5 != null && text5.Length > 0)
				{
					httpWebRequest.UserAgent = text5;
				}
				if (text6 != null && text6.Length > 0)
				{
					httpWebRequest.Referer = text6;
				}
			}
			this.responseHeaders = null;
			return webRequest;
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x00070024 File Offset: 0x0006E224
		private WebRequest SetupRequest(System.Uri uri, string method, bool is_upload)
		{
			WebRequest webRequest = this.SetupRequest(uri);
			webRequest.Method = this.DetermineMethod(uri, method, is_upload);
			return webRequest;
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x0007004C File Offset: 0x0006E24C
		private byte[] ReadAll(Stream stream, int length, object userToken)
		{
			MemoryStream memoryStream = null;
			bool flag = length == -1;
			int num = (!flag) ? length : 8192;
			if (flag)
			{
				memoryStream = new MemoryStream();
			}
			int num2 = 0;
			byte[] array = new byte[num];
			int num3;
			while ((num3 = stream.Read(array, num2, num)) != 0)
			{
				if (flag)
				{
					memoryStream.Write(array, 0, num3);
				}
				else
				{
					num2 += num3;
					num -= num3;
				}
				if (this.async)
				{
					this.OnDownloadProgressChanged(new DownloadProgressChangedEventArgs((long)num3, (long)length, userToken));
				}
			}
			if (flag)
			{
				return memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000700EC File Offset: 0x0006E2EC
		private string UrlEncode(string str)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				char c = str[i];
				if (c == ' ')
				{
					stringBuilder.Append('+');
				}
				else if ((c < '0' && c != '-' && c != '.') || (c < 'A' && c > '9') || (c > 'Z' && c < 'a' && c != '_') || c > 'z')
				{
					stringBuilder.Append('%');
					int num = (int)(c >> 4);
					stringBuilder.Append((char)WebClient.hexBytes[num]);
					num = (int)(c & '\u000f');
					stringBuilder.Append((char)WebClient.hexBytes[num]);
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000701C8 File Offset: 0x0006E3C8
		private static void UrlEncodeAndWrite(Stream stream, byte[] bytes)
		{
			if (bytes == null)
			{
				return;
			}
			int num = bytes.Length;
			if (num == 0)
			{
				return;
			}
			for (int i = 0; i < num; i++)
			{
				char c = (char)bytes[i];
				if (c == ' ')
				{
					stream.WriteByte(43);
				}
				else if ((c < '0' && c != '-' && c != '.') || (c < 'A' && c > '9') || (c > 'Z' && c < 'a' && c != '_') || c > 'z')
				{
					stream.WriteByte(37);
					int num2 = (int)(c >> 4);
					stream.WriteByte(WebClient.hexBytes[num2]);
					num2 = (int)(c & '\u000f');
					stream.WriteByte(WebClient.hexBytes[num2]);
				}
				else
				{
					stream.WriteByte((byte)c);
				}
			}
		}

		/// <summary>Cancels a pending asynchronous operation.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002514 RID: 9492 RVA: 0x00070294 File Offset: 0x0006E494
		public void CancelAsync()
		{
			lock (this)
			{
				if (this.async_thread != null)
				{
					Thread thread = this.async_thread;
					this.CompleteAsync();
					thread.Interrupt();
				}
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000702F4 File Offset: 0x0006E4F4
		private void CompleteAsync()
		{
			lock (this)
			{
				this.is_busy = false;
				this.async_thread = null;
			}
		}

		/// <summary>Downloads the specified resource as a <see cref="T:System.Byte" /> array. This method does not block the calling thread.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x06002516 RID: 9494 RVA: 0x00070340 File Offset: 0x0006E540
		public void DownloadDataAsync(System.Uri address)
		{
			this.DownloadDataAsync(address, null);
		}

		/// <summary>Downloads the specified resource as a <see cref="T:System.Byte" /> array. This method does not block the calling thread.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x06002517 RID: 9495 RVA: 0x0007034C File Offset: 0x0006E54C
		public void DownloadDataAsync(System.Uri address, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			lock (this)
			{
				this.SetBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array = (object[])state;
					try
					{
						byte[] result = this.DownloadDataCore((System.Uri)array[0], array[1]);
						this.OnDownloadDataCompleted(new DownloadDataCompletedEventArgs(result, null, false, array[1]));
					}
					catch (ThreadInterruptedException)
					{
						this.OnDownloadDataCompleted(new DownloadDataCompletedEventArgs(null, null, true, array[1]));
						throw;
					}
					catch (Exception error)
					{
						this.OnDownloadDataCompleted(new DownloadDataCompletedEventArgs(null, error, false, array[1]));
					}
				});
				object[] parameter = new object[]
				{
					address,
					userToken
				};
				this.async_thread.Start(parameter);
			}
		}

		/// <summary>Downloads, to a local file, the resource with the specified URI. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to download. </param>
		/// <param name="fileName">The name of the file to be placed on the local computer. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		/// <exception cref="T:System.InvalidOperationException">The local file specified by <paramref name="fileName" /> is in use by another thread.</exception>
		// Token: 0x06002518 RID: 9496 RVA: 0x000703E0 File Offset: 0x0006E5E0
		public void DownloadFileAsync(System.Uri address, string fileName)
		{
			this.DownloadFileAsync(address, fileName, null);
		}

		/// <summary>Downloads, to a local file, the resource with the specified URI. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to download. </param>
		/// <param name="fileName">The name of the file to be placed on the local computer. </param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		/// <exception cref="T:System.InvalidOperationException">The local file specified by <paramref name="fileName" /> is in use by another thread.</exception>
		// Token: 0x06002519 RID: 9497 RVA: 0x000703EC File Offset: 0x0006E5EC
		public void DownloadFileAsync(System.Uri address, string fileName, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			lock (this)
			{
				this.SetBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array = (object[])state;
					try
					{
						this.DownloadFileCore((System.Uri)array[0], (string)array[1], array[2]);
						this.OnDownloadFileCompleted(new System.ComponentModel.AsyncCompletedEventArgs(null, false, array[2]));
					}
					catch (ThreadInterruptedException)
					{
						this.OnDownloadFileCompleted(new System.ComponentModel.AsyncCompletedEventArgs(null, true, array[2]));
					}
					catch (Exception error)
					{
						this.OnDownloadFileCompleted(new System.ComponentModel.AsyncCompletedEventArgs(error, false, array[2]));
					}
				});
				object[] parameter = new object[]
				{
					address,
					fileName,
					userToken
				};
				this.async_thread.Start(parameter);
			}
		}

		/// <summary>Downloads the resource specified as a <see cref="T:System.Uri" />. This method does not block the calling thread.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x0600251A RID: 9498 RVA: 0x00070494 File Offset: 0x0006E694
		public void DownloadStringAsync(System.Uri address)
		{
			this.DownloadStringAsync(address, null);
		}

		/// <summary>Downloads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x0600251B RID: 9499 RVA: 0x000704A0 File Offset: 0x0006E6A0
		public void DownloadStringAsync(System.Uri address, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			lock (this)
			{
				this.SetBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array = (object[])state;
					try
					{
						string @string = this.encoding.GetString(this.DownloadDataCore((System.Uri)array[0], array[1]));
						this.OnDownloadStringCompleted(new DownloadStringCompletedEventArgs(@string, null, false, array[1]));
					}
					catch (ThreadInterruptedException)
					{
						this.OnDownloadStringCompleted(new DownloadStringCompletedEventArgs(null, null, true, array[1]));
					}
					catch (Exception error)
					{
						this.OnDownloadStringCompleted(new DownloadStringCompletedEventArgs(null, error, false, array[1]));
					}
				});
				object[] parameter = new object[]
				{
					address,
					userToken
				};
				this.async_thread.Start(parameter);
			}
		}

		/// <summary>Opens a readable stream containing the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to retrieve.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and address is invalid.-or- An error occurred while downloading the resource. -or- An error occurred while opening the stream.</exception>
		// Token: 0x0600251C RID: 9500 RVA: 0x00070534 File Offset: 0x0006E734
		public void OpenReadAsync(System.Uri address)
		{
			this.OpenReadAsync(address, null);
		}

		/// <summary>Opens a readable stream containing the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to retrieve.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and address is invalid.-or- An error occurred while downloading the resource. -or- An error occurred while opening the stream.</exception>
		// Token: 0x0600251D RID: 9501 RVA: 0x00070540 File Offset: 0x0006E740
		public void OpenReadAsync(System.Uri address, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			lock (this)
			{
				this.SetBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array = (object[])state;
					WebRequest webRequest = null;
					try
					{
						webRequest = this.SetupRequest((System.Uri)array[0]);
						WebResponse webResponse = this.GetWebResponse(webRequest);
						Stream responseStream = webResponse.GetResponseStream();
						this.OnOpenReadCompleted(new OpenReadCompletedEventArgs(responseStream, null, false, array[1]));
					}
					catch (ThreadInterruptedException)
					{
						if (webRequest != null)
						{
							webRequest.Abort();
						}
						this.OnOpenReadCompleted(new OpenReadCompletedEventArgs(null, null, true, array[1]));
					}
					catch (Exception error)
					{
						this.OnOpenReadCompleted(new OpenReadCompletedEventArgs(null, error, false, array[1]));
					}
				});
				object[] parameter = new object[]
				{
					address,
					userToken
				};
				this.async_thread.Start(parameter);
			}
		}

		/// <summary>Opens a stream for writing data to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data. </param>
		// Token: 0x0600251E RID: 9502 RVA: 0x000705D4 File Offset: 0x0006E7D4
		public void OpenWriteAsync(System.Uri address)
		{
			this.OpenWriteAsync(address, null);
		}

		/// <summary>Opens a stream for writing data to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		// Token: 0x0600251F RID: 9503 RVA: 0x000705E0 File Offset: 0x0006E7E0
		public void OpenWriteAsync(System.Uri address, string method)
		{
			this.OpenWriteAsync(address, method, null);
		}

		/// <summary>Opens a stream for writing data to the specified resource, using the specified method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		// Token: 0x06002520 RID: 9504 RVA: 0x000705EC File Offset: 0x0006E7EC
		public void OpenWriteAsync(System.Uri address, string method, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			lock (this)
			{
				this.SetBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array = (object[])state;
					WebRequest webRequest = null;
					try
					{
						webRequest = this.SetupRequest((System.Uri)array[0], (string)array[1], true);
						Stream requestStream = webRequest.GetRequestStream();
						this.OnOpenWriteCompleted(new OpenWriteCompletedEventArgs(requestStream, null, false, array[2]));
					}
					catch (ThreadInterruptedException)
					{
						if (webRequest != null)
						{
							webRequest.Abort();
						}
						this.OnOpenWriteCompleted(new OpenWriteCompletedEventArgs(null, null, true, array[2]));
					}
					catch (Exception error)
					{
						this.OnOpenWriteCompleted(new OpenWriteCompletedEventArgs(null, error, false, array[2]));
					}
				});
				object[] parameter = new object[]
				{
					address,
					method,
					userToken
				};
				this.async_thread.Start(parameter);
			}
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="data">The data buffer to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x06002521 RID: 9505 RVA: 0x00070684 File Offset: 0x0006E884
		public void UploadDataAsync(System.Uri address, byte[] data)
		{
			this.UploadDataAsync(address, null, data);
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI, using the specified method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x06002522 RID: 9506 RVA: 0x00070690 File Offset: 0x0006E890
		public void UploadDataAsync(System.Uri address, string method, byte[] data)
		{
			this.UploadDataAsync(address, method, data, null);
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI, using the specified method and identifying token.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x06002523 RID: 9507 RVA: 0x0007069C File Offset: 0x0006E89C
		public void UploadDataAsync(System.Uri address, string method, byte[] data, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			lock (this)
			{
				this.SetBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array = (object[])state;
					try
					{
						byte[] result = this.UploadDataCore((System.Uri)array[0], (string)array[1], (byte[])array[2], array[3]);
						this.OnUploadDataCompleted(new UploadDataCompletedEventArgs(result, null, false, array[3]));
					}
					catch (ThreadInterruptedException)
					{
						this.OnUploadDataCompleted(new UploadDataCompletedEventArgs(null, null, true, array[3]));
					}
					catch (Exception error)
					{
						this.OnUploadDataCompleted(new UploadDataCompletedEventArgs(null, error, false, array[3]));
					}
				});
				object[] parameter = new object[]
				{
					address,
					method,
					data,
					userToken
				};
				this.async_thread.Start(parameter);
			}
		}

		/// <summary>Uploads the specified local file to the specified resource, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="fileName">The file to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x06002524 RID: 9508 RVA: 0x0007074C File Offset: 0x0006E94C
		public void UploadFileAsync(System.Uri address, string fileName)
		{
			this.UploadFileAsync(address, null, fileName);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="method">The HTTP method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x06002525 RID: 9509 RVA: 0x00070758 File Offset: 0x0006E958
		public void UploadFileAsync(System.Uri address, string method, string fileName)
		{
			this.UploadFileAsync(address, method, fileName, null);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The HTTP method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x06002526 RID: 9510 RVA: 0x00070764 File Offset: 0x0006E964
		public void UploadFileAsync(System.Uri address, string method, string fileName, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			lock (this)
			{
				this.SetBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array = (object[])state;
					try
					{
						byte[] result = this.UploadFileCore((System.Uri)array[0], (string)array[1], (string)array[2], array[3]);
						this.OnUploadFileCompleted(new UploadFileCompletedEventArgs(result, null, false, array[3]));
					}
					catch (ThreadInterruptedException)
					{
						this.OnUploadFileCompleted(new UploadFileCompletedEventArgs(null, null, true, array[3]));
					}
					catch (Exception error)
					{
						this.OnUploadFileCompleted(new UploadFileCompletedEventArgs(null, error, false, array[3]));
					}
				});
				object[] parameter = new object[]
				{
					address,
					method,
					fileName,
					userToken
				};
				this.async_thread.Start(parameter);
			}
		}

		/// <summary>Uploads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x06002527 RID: 9511 RVA: 0x00070814 File Offset: 0x0006EA14
		public void UploadStringAsync(System.Uri address, string data)
		{
			this.UploadStringAsync(address, null, data);
		}

		/// <summary>Uploads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x06002528 RID: 9512 RVA: 0x00070820 File Offset: 0x0006EA20
		public void UploadStringAsync(System.Uri address, string method, string data)
		{
			this.UploadStringAsync(address, method, data, null);
		}

		/// <summary>Uploads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x06002529 RID: 9513 RVA: 0x0007082C File Offset: 0x0006EA2C
		public void UploadStringAsync(System.Uri address, string method, string data, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			lock (this)
			{
				this.CheckBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array = (object[])state;
					try
					{
						string result = this.UploadString((System.Uri)array[0], (string)array[1], (string)array[2]);
						this.OnUploadStringCompleted(new UploadStringCompletedEventArgs(result, null, false, array[3]));
					}
					catch (ThreadInterruptedException)
					{
						this.OnUploadStringCompleted(new UploadStringCompletedEventArgs(null, null, true, array[3]));
					}
					catch (Exception error)
					{
						this.OnUploadStringCompleted(new UploadStringCompletedEventArgs(null, error, false, array[3]));
					}
				});
				object[] parameter = new object[]
				{
					address,
					method,
					data,
					userToken
				};
				this.async_thread.Start(parameter);
			}
		}

		/// <summary>Uploads the data in the specified name/value collection to the resource identified by the specified URI. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the collection. This URI must identify a resource that can accept a request sent with the default method. See remarks.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x0600252A RID: 9514 RVA: 0x000708DC File Offset: 0x0006EADC
		public void UploadValuesAsync(System.Uri address, System.Collections.Specialized.NameValueCollection values)
		{
			this.UploadValuesAsync(address, null, values);
		}

		/// <summary>Uploads the data in the specified name/value collection to the resource identified by the specified URI, using the specified method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the collection. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method.</param>
		/// <param name="method">The method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null. -or- <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.-or-<paramref name="method" /> cannot be used to send content.</exception>
		// Token: 0x0600252B RID: 9515 RVA: 0x000708E8 File Offset: 0x0006EAE8
		public void UploadValuesAsync(System.Uri address, string method, System.Collections.Specialized.NameValueCollection values)
		{
			this.UploadValuesAsync(address, method, values, null);
		}

		/// <summary>Uploads the data in the specified name/value collection to the resource identified by the specified URI, using the specified method. This method does not block the calling thread, and allows the caller to pass an object to the method that is invoked when the operation completes.</summary>
		/// <param name="address">The URI of the resource to receive the collection. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method.</param>
		/// <param name="method">The HTTP method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null. -or- <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.-or-<paramref name="method" /> cannot be used to send content.</exception>
		// Token: 0x0600252C RID: 9516 RVA: 0x000708F4 File Offset: 0x0006EAF4
		public void UploadValuesAsync(System.Uri address, string method, System.Collections.Specialized.NameValueCollection values, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			lock (this)
			{
				this.CheckBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array = (object[])state;
					try
					{
						byte[] result = this.UploadValuesCore((System.Uri)array[0], (string)array[1], (System.Collections.Specialized.NameValueCollection)array[2], array[3]);
						this.OnUploadValuesCompleted(new UploadValuesCompletedEventArgs(result, null, false, array[3]));
					}
					catch (ThreadInterruptedException)
					{
						this.OnUploadValuesCompleted(new UploadValuesCompletedEventArgs(null, null, true, array[3]));
					}
					catch (Exception error)
					{
						this.OnUploadValuesCompleted(new UploadValuesCompletedEventArgs(null, error, false, array[3]));
					}
				});
				object[] parameter = new object[]
				{
					address,
					method,
					values,
					userToken
				};
				this.async_thread.Start(parameter);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadDataCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.DownloadDataCompletedEventArgs" /> object that contains event data.</param>
		// Token: 0x0600252D RID: 9517 RVA: 0x000709A4 File Offset: 0x0006EBA4
		protected virtual void OnDownloadDataCompleted(DownloadDataCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.DownloadDataCompleted != null)
			{
				this.DownloadDataCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadFileCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x0600252E RID: 9518 RVA: 0x000709C4 File Offset: 0x0006EBC4
		protected virtual void OnDownloadFileCompleted(System.ComponentModel.AsyncCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.DownloadFileCompleted != null)
			{
				this.DownloadFileCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadProgressChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.DownloadProgressChangedEventArgs" /> object containing event data.</param>
		// Token: 0x0600252F RID: 9519 RVA: 0x000709E4 File Offset: 0x0006EBE4
		protected virtual void OnDownloadProgressChanged(DownloadProgressChangedEventArgs e)
		{
			if (this.DownloadProgressChanged != null)
			{
				this.DownloadProgressChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadStringCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.DownloadStringCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06002530 RID: 9520 RVA: 0x00070A00 File Offset: 0x0006EC00
		protected virtual void OnDownloadStringCompleted(DownloadStringCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.DownloadStringCompleted != null)
			{
				this.DownloadStringCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.OpenReadCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.OpenReadCompletedEventArgs" />  object containing event data.</param>
		// Token: 0x06002531 RID: 9521 RVA: 0x00070A20 File Offset: 0x0006EC20
		protected virtual void OnOpenReadCompleted(OpenReadCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.OpenReadCompleted != null)
			{
				this.OpenReadCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.OpenWriteCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.OpenWriteCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06002532 RID: 9522 RVA: 0x00070A40 File Offset: 0x0006EC40
		protected virtual void OnOpenWriteCompleted(OpenWriteCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.OpenWriteCompleted != null)
			{
				this.OpenWriteCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadDataCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.UploadDataCompletedEventArgs" />  object containing event data.</param>
		// Token: 0x06002533 RID: 9523 RVA: 0x00070A60 File Offset: 0x0006EC60
		protected virtual void OnUploadDataCompleted(UploadDataCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.UploadDataCompleted != null)
			{
				this.UploadDataCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadFileCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Net.UploadFileCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06002534 RID: 9524 RVA: 0x00070A80 File Offset: 0x0006EC80
		protected virtual void OnUploadFileCompleted(UploadFileCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.UploadFileCompleted != null)
			{
				this.UploadFileCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadProgressChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Net.UploadProgressChangedEventArgs" /> object containing event data.</param>
		// Token: 0x06002535 RID: 9525 RVA: 0x00070AA0 File Offset: 0x0006ECA0
		protected virtual void OnUploadProgressChanged(UploadProgressChangedEventArgs e)
		{
			if (this.UploadProgressChanged != null)
			{
				this.UploadProgressChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadStringCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Net.UploadStringCompletedEventArgs" />  object containing event data.</param>
		// Token: 0x06002536 RID: 9526 RVA: 0x00070ABC File Offset: 0x0006ECBC
		protected virtual void OnUploadStringCompleted(UploadStringCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.UploadStringCompleted != null)
			{
				this.UploadStringCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadValuesCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.UploadValuesCompletedEventArgs" />  object containing event data.</param>
		// Token: 0x06002537 RID: 9527 RVA: 0x00070ADC File Offset: 0x0006ECDC
		protected virtual void OnUploadValuesCompleted(UploadValuesCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.UploadValuesCompleted != null)
			{
				this.UploadValuesCompleted(this, args);
			}
		}

		/// <summary>Returns the <see cref="T:System.Net.WebResponse" /> for the specified <see cref="T:System.Net.WebRequest" /> using the specified <see cref="T:System.IAsyncResult" />.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> containing the response for the specified <see cref="T:System.Net.WebRequest" />.</returns>
		/// <param name="request">A <see cref="T:System.Net.WebRequest" /> that is used to obtain the response.</param>
		/// <param name="result">An <see cref="T:System.IAsyncResult" /> object obtained from a previous call to <see cref="M:System.Net.WebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> .</param>
		// Token: 0x06002538 RID: 9528 RVA: 0x00070AFC File Offset: 0x0006ECFC
		protected virtual WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
		{
			WebResponse webResponse = request.EndGetResponse(result);
			this.responseHeaders = webResponse.Headers;
			return webResponse;
		}

		/// <summary>Returns a <see cref="T:System.Net.WebRequest" /> object for the specified resource.</summary>
		/// <returns>A new <see cref="T:System.Net.WebRequest" /> object for the specified resource.</returns>
		/// <param name="address">A <see cref="T:System.Uri" /> that identifies the resource to request.</param>
		// Token: 0x06002539 RID: 9529 RVA: 0x00070B20 File Offset: 0x0006ED20
		protected virtual WebRequest GetWebRequest(System.Uri address)
		{
			return WebRequest.Create(address);
		}

		/// <summary>Returns the <see cref="T:System.Net.WebResponse" /> for the specified <see cref="T:System.Net.WebRequest" />.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> containing the response for the specified <see cref="T:System.Net.WebRequest" />.</returns>
		/// <param name="request">A <see cref="T:System.Net.WebRequest" /> that is used to obtain the response. </param>
		// Token: 0x0600253A RID: 9530 RVA: 0x00070B28 File Offset: 0x0006ED28
		protected virtual WebResponse GetWebResponse(WebRequest request)
		{
			WebResponse response = request.GetResponse();
			this.responseHeaders = response.Headers;
			return response;
		}

		// Token: 0x040016E9 RID: 5865
		private static readonly string urlEncodedCType = "application/x-www-form-urlencoded";

		// Token: 0x040016EA RID: 5866
		private static byte[] hexBytes = new byte[16];

		// Token: 0x040016EB RID: 5867
		private ICredentials credentials;

		// Token: 0x040016EC RID: 5868
		private WebHeaderCollection headers;

		// Token: 0x040016ED RID: 5869
		private WebHeaderCollection responseHeaders;

		// Token: 0x040016EE RID: 5870
		private System.Uri baseAddress;

		// Token: 0x040016EF RID: 5871
		private string baseString;

		// Token: 0x040016F0 RID: 5872
		private System.Collections.Specialized.NameValueCollection queryString;

		// Token: 0x040016F1 RID: 5873
		private bool is_busy;

		// Token: 0x040016F2 RID: 5874
		private bool async;

		// Token: 0x040016F3 RID: 5875
		private Thread async_thread;

		// Token: 0x040016F4 RID: 5876
		private Encoding encoding = Encoding.Default;

		// Token: 0x040016F5 RID: 5877
		private IWebProxy proxy;
	}
}
