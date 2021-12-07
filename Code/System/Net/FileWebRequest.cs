using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Net
{
	/// <summary>Provides a file system implementation of the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x02000304 RID: 772
	[Serializable]
	public class FileWebRequest : WebRequest, ISerializable
	{
		// Token: 0x06001A79 RID: 6777 RVA: 0x0004A904 File Offset: 0x00048B04
		internal FileWebRequest(System.Uri uri)
		{
			this.uri = uri;
			this.webHeaders = new WebHeaderCollection();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.FileWebRequest" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the new <see cref="T:System.Net.FileWebRequest" /> object. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.FileWebRequest" /> object. </param>
		// Token: 0x06001A7A RID: 6778 RVA: 0x0004A93C File Offset: 0x00048B3C
		[Obsolete("Serialization is obsoleted for this type", false)]
		protected FileWebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.webHeaders = (WebHeaderCollection)serializationInfo.GetValue("headers", typeof(WebHeaderCollection));
			this.proxy = (IWebProxy)serializationInfo.GetValue("proxy", typeof(IWebProxy));
			this.uri = (System.Uri)serializationInfo.GetValue("uri", typeof(System.Uri));
			this.connectionGroup = serializationInfo.GetString("connectionGroupName");
			this.method = serializationInfo.GetString("method");
			this.contentLength = serializationInfo.GetInt64("contentLength");
			this.timeout = serializationInfo.GetInt32("timeout");
			this.fileAccess = (FileAccess)((int)serializationInfo.GetValue("fileAccess", typeof(FileAccess)));
			this.preAuthenticate = serializationInfo.GetBoolean("preauthenticate");
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the required data to serialize the <see cref="T:System.Net.FileWebRequest" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized data for the <see cref="T:System.Net.FileWebRequest" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream that is associated with the new <see cref="T:System.Net.FileWebRequest" />. </param>
		// Token: 0x06001A7B RID: 6779 RVA: 0x0004AA44 File Offset: 0x00048C44
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Gets or sets the name of the connection group for the request. This property is reserved for future use.</summary>
		/// <returns>The name of the connection group for the request.</returns>
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x0004AA50 File Offset: 0x00048C50
		// (set) Token: 0x06001A7D RID: 6781 RVA: 0x0004AA58 File Offset: 0x00048C58
		public override string ConnectionGroupName
		{
			get
			{
				return this.connectionGroup;
			}
			set
			{
				this.connectionGroup = value;
			}
		}

		/// <summary>Gets or sets the content length of the data being sent.</summary>
		/// <returns>The number of bytes of request data being sent.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.FileWebRequest.ContentLength" /> is less than 0. </exception>
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x0004AA64 File Offset: 0x00048C64
		// (set) Token: 0x06001A7F RID: 6783 RVA: 0x0004AA6C File Offset: 0x00048C6C
		public override long ContentLength
		{
			get
			{
				return this.contentLength;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentException("The Content-Length value must be greater than or equal to zero.", "value");
				}
				this.contentLength = value;
			}
		}

		/// <summary>Gets or sets the content type of the data being sent. This property is reserved for future use.</summary>
		/// <returns>The content type of the data being sent.</returns>
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x0004AA90 File Offset: 0x00048C90
		// (set) Token: 0x06001A81 RID: 6785 RVA: 0x0004AAA4 File Offset: 0x00048CA4
		public override string ContentType
		{
			get
			{
				return this.webHeaders["Content-Type"];
			}
			set
			{
				this.webHeaders["Content-Type"] = value;
			}
		}

		/// <summary>Gets or sets the credentials that are associated with this request. This property is reserved for future use.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> that contains the authentication credentials that are associated with this request. The default is null.</returns>
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x0004AAB8 File Offset: 0x00048CB8
		// (set) Token: 0x06001A83 RID: 6787 RVA: 0x0004AAC0 File Offset: 0x00048CC0
		public override ICredentials Credentials
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

		/// <summary>Gets a collection of the name/value pairs that are associated with the request. This property is reserved for future use.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains header name/value pairs associated with this request.</returns>
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x0004AACC File Offset: 0x00048CCC
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.webHeaders;
			}
		}

		/// <summary>Gets or sets the protocol method used for the request. This property is reserved for future use.</summary>
		/// <returns>The protocol method to use in this request.</returns>
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x0004AAD4 File Offset: 0x00048CD4
		// (set) Token: 0x06001A86 RID: 6790 RVA: 0x0004AADC File Offset: 0x00048CDC
		public override string Method
		{
			get
			{
				return this.method;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException("Cannot set null or blank methods on request.", "value");
				}
				this.method = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to preauthenticate a request. This property is reserved for future use.</summary>
		/// <returns>true to preauthenticate; otherwise, false.</returns>
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0004AB14 File Offset: 0x00048D14
		// (set) Token: 0x06001A88 RID: 6792 RVA: 0x0004AB1C File Offset: 0x00048D1C
		public override bool PreAuthenticate
		{
			get
			{
				return this.preAuthenticate;
			}
			set
			{
				this.preAuthenticate = value;
			}
		}

		/// <summary>Gets or sets the network proxy to use for this request. This property is reserved for future use.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that indicates the network proxy to use for this request.</returns>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x0004AB28 File Offset: 0x00048D28
		// (set) Token: 0x06001A8A RID: 6794 RVA: 0x0004AB30 File Offset: 0x00048D30
		public override IWebProxy Proxy
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

		/// <summary>Gets the Uniform Resource Identifier (URI) of the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the request.</returns>
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x0004AB3C File Offset: 0x00048D3C
		public override System.Uri RequestUri
		{
			get
			{
				return this.uri;
			}
		}

		/// <summary>Gets or sets the length of time until the request times out.</summary>
		/// <returns>The time, in milliseconds, until the request times out, or the value <see cref="F:System.Threading.Timeout.Infinite" /> to indicate that the request does not time out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x0004AB44 File Offset: 0x00048D44
		// (set) Token: 0x06001A8D RID: 6797 RVA: 0x0004AB4C File Offset: 0x00048D4C
		public override int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("Timeout can be only set to 'System.Threading.Timeout.Infinite' or a value >= 0.");
				}
				this.timeout = value;
			}
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Default credentials are not supported for file Uniform Resource Identifiers (URIs).</exception>
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x0004AB68 File Offset: 0x00048D68
		// (set) Token: 0x06001A8F RID: 6799 RVA: 0x0004AB70 File Offset: 0x00048D70
		public override bool UseDefaultCredentials
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0004AB78 File Offset: 0x00048D78
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Cancels a request to an Internet resource.</summary>
		// Token: 0x06001A91 RID: 6801 RVA: 0x0004AB80 File Offset: 0x00048D80
		[MonoTODO]
		public override void Abort()
		{
			throw FileWebRequest.GetMustImplement();
		}

		/// <summary>Begins an asynchronous request for a <see cref="T:System.IO.Stream" /> object to use to write data.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.FileWebRequest.Method" /> property is GET and the application writes to the stream. </exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is being used by a previous call to <see cref="M:System.Net.FileWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.ApplicationException">No write stream is available. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001A92 RID: 6802 RVA: 0x0004AB88 File Offset: 0x00048D88
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			if (string.Compare("GET", this.method, true) == 0 || string.Compare("HEAD", this.method, true) == 0 || string.Compare("CONNECT", this.method, true) == 0)
			{
				throw new ProtocolViolationException("Cannot send a content-body with this verb-type.");
			}
			lock (this)
			{
				if (this.asyncResponding || this.webResponse != null)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				if (this.requesting)
				{
					throw new InvalidOperationException("Cannot re-call start of asynchronous method while a previous call is still in progress.");
				}
				this.requesting = true;
			}
			FileWebRequest.GetRequestStreamCallback getRequestStreamCallback = new FileWebRequest.GetRequestStreamCallback(this.GetRequestStreamInternal);
			return getRequestStreamCallback.BeginInvoke(callback, state);
		}

		/// <summary>Ends an asynchronous request for a <see cref="T:System.IO.Stream" /> instance that the application uses to write data.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that the application uses to write data.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references the pending request for a stream. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		// Token: 0x06001A93 RID: 6803 RVA: 0x0004AC68 File Offset: 0x00048E68
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			FileWebRequest.GetRequestStreamCallback getRequestStreamCallback = (FileWebRequest.GetRequestStreamCallback)asyncResult2.AsyncDelegate;
			return getRequestStreamCallback.EndInvoke(asyncResult);
		}

		/// <summary>Returns a <see cref="T:System.IO.Stream" /> object for writing data to the file system resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> for writing data to the file system resource.</returns>
		/// <exception cref="T:System.Net.WebException">The request times out. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001A94 RID: 6804 RVA: 0x0004ACB8 File Offset: 0x00048EB8
		public override Stream GetRequestStream()
		{
			IAsyncResult asyncResult = this.BeginGetRequestStream(null, null);
			if (!asyncResult.AsyncWaitHandle.WaitOne(this.timeout, false))
			{
				throw new WebException("The request timed out", WebExceptionStatus.Timeout);
			}
			return this.EndGetRequestStream(asyncResult);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0004ACFC File Offset: 0x00048EFC
		internal Stream GetRequestStreamInternal()
		{
			this.requestStream = new FileWebRequest.FileWebStream(this, FileMode.Create, FileAccess.Write, FileShare.Read);
			return this.requestStream;
		}

		/// <summary>Begins an asynchronous request for a file system resource.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.InvalidOperationException">The stream is already in use by a previous call to <see cref="M:System.Net.FileWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001A96 RID: 6806 RVA: 0x0004AD14 File Offset: 0x00048F14
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			lock (this)
			{
				if (this.asyncResponding)
				{
					throw new InvalidOperationException("Cannot re-call start of asynchronous method while a previous call is still in progress.");
				}
				this.asyncResponding = true;
			}
			FileWebRequest.GetResponseCallback getResponseCallback = new FileWebRequest.GetResponseCallback(this.GetResponseInternal);
			return getResponseCallback.BeginInvoke(callback, state);
		}

		/// <summary>Ends an asynchronous request for a file system resource.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the file system resource.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references the pending request for a response. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		// Token: 0x06001A97 RID: 6807 RVA: 0x0004AD84 File Offset: 0x00048F84
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			FileWebRequest.GetResponseCallback getResponseCallback = (FileWebRequest.GetResponseCallback)asyncResult2.AsyncDelegate;
			WebResponse result = getResponseCallback.EndInvoke(asyncResult);
			this.asyncResponding = false;
			return result;
		}

		/// <summary>Returns a response to a file system request.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the file system resource.</returns>
		/// <exception cref="T:System.Net.WebException">The request timed out. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001A98 RID: 6808 RVA: 0x0004ADDC File Offset: 0x00048FDC
		public override WebResponse GetResponse()
		{
			IAsyncResult asyncResult = this.BeginGetResponse(null, null);
			if (!asyncResult.AsyncWaitHandle.WaitOne(this.timeout, false))
			{
				throw new WebException("The request timed out", WebExceptionStatus.Timeout);
			}
			return this.EndGetResponse(asyncResult);
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x0004AE20 File Offset: 0x00049020
		private WebResponse GetResponseInternal()
		{
			if (this.webResponse != null)
			{
				return this.webResponse;
			}
			lock (this)
			{
				if (this.requesting)
				{
					this.requestEndEvent = new AutoResetEvent(false);
				}
			}
			if (this.requestEndEvent != null)
			{
				this.requestEndEvent.WaitOne();
			}
			FileStream fileStream = null;
			try
			{
				fileStream = new FileWebRequest.FileWebStream(this, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			catch (Exception ex)
			{
				throw new WebException(ex.Message, ex);
			}
			this.webResponse = new FileWebResponse(this.uri, fileStream);
			return this.webResponse;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" />  that specifies the destination for this serialization. </param>
		// Token: 0x06001A9A RID: 6810 RVA: 0x0004AEF4 File Offset: 0x000490F4
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("headers", this.webHeaders, typeof(WebHeaderCollection));
			serializationInfo.AddValue("proxy", this.proxy, typeof(IWebProxy));
			serializationInfo.AddValue("uri", this.uri, typeof(System.Uri));
			serializationInfo.AddValue("connectionGroupName", this.connectionGroup);
			serializationInfo.AddValue("method", this.method);
			serializationInfo.AddValue("contentLength", this.contentLength);
			serializationInfo.AddValue("timeout", this.timeout);
			serializationInfo.AddValue("fileAccess", this.fileAccess);
			serializationInfo.AddValue("preauthenticate", false);
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x0004AFBC File Offset: 0x000491BC
		internal void Close()
		{
			lock (this)
			{
				this.requesting = false;
				if (this.requestEndEvent != null)
				{
					this.requestEndEvent.Set();
				}
			}
		}

		// Token: 0x04001058 RID: 4184
		private System.Uri uri;

		// Token: 0x04001059 RID: 4185
		private WebHeaderCollection webHeaders;

		// Token: 0x0400105A RID: 4186
		private ICredentials credentials;

		// Token: 0x0400105B RID: 4187
		private string connectionGroup;

		// Token: 0x0400105C RID: 4188
		private long contentLength;

		// Token: 0x0400105D RID: 4189
		private FileAccess fileAccess = FileAccess.Read;

		// Token: 0x0400105E RID: 4190
		private string method = "GET";

		// Token: 0x0400105F RID: 4191
		private IWebProxy proxy;

		// Token: 0x04001060 RID: 4192
		private bool preAuthenticate;

		// Token: 0x04001061 RID: 4193
		private int timeout = 100000;

		// Token: 0x04001062 RID: 4194
		private Stream requestStream;

		// Token: 0x04001063 RID: 4195
		private FileWebResponse webResponse;

		// Token: 0x04001064 RID: 4196
		private AutoResetEvent requestEndEvent;

		// Token: 0x04001065 RID: 4197
		private bool requesting;

		// Token: 0x04001066 RID: 4198
		private bool asyncResponding;

		// Token: 0x02000305 RID: 773
		internal class FileWebStream : FileStream
		{
			// Token: 0x06001A9C RID: 6812 RVA: 0x0004B018 File Offset: 0x00049218
			internal FileWebStream(FileWebRequest webRequest, FileMode mode, FileAccess access, FileShare share) : base(webRequest.RequestUri.LocalPath, mode, access, share)
			{
				this.webRequest = webRequest;
			}

			// Token: 0x06001A9D RID: 6813 RVA: 0x0004B044 File Offset: 0x00049244
			public override void Close()
			{
				base.Close();
				FileWebRequest fileWebRequest = this.webRequest;
				this.webRequest = null;
				if (fileWebRequest != null)
				{
					fileWebRequest.Close();
				}
			}

			// Token: 0x04001067 RID: 4199
			private FileWebRequest webRequest;
		}

		// Token: 0x020004E6 RID: 1254
		// (Invoke) Token: 0x06002C44 RID: 11332
		private delegate Stream GetRequestStreamCallback();

		// Token: 0x020004E7 RID: 1255
		// (Invoke) Token: 0x06002C48 RID: 11336
		private delegate WebResponse GetResponseCallback();
	}
}
