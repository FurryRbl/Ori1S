using System;
using System.IO;
using System.Security.Principal;

namespace System.Net.Security
{
	/// <summary>Provides a stream that uses the Negotiate security protocol to authenticate the client, and optionally the server, in client-server communication.</summary>
	// Token: 0x020003DF RID: 991
	public class NegotiateStream : AuthenticatedStream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.NegotiateStream" /> class using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.NegotiateStream" /> for sending and receiving data.</param>
		// Token: 0x060021E3 RID: 8675 RVA: 0x00063288 File Offset: 0x00061488
		[MonoTODO]
		public NegotiateStream(Stream innerStream) : base(innerStream, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.NegotiateStream" /> class using the specified <see cref="T:System.IO.Stream" /> and stream closure behavior.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.NegotiateStream" /> for sending and receiving data.</param>
		/// <param name="leaveInnerStreamOpen">true to indicate that closing this <see cref="T:System.Net.Security.NegotiateStream" /> has no effect on <paramref name="innerstream" />; false to indicate that closing this <see cref="T:System.Net.Security.NegotiateStream" /> also closes <paramref name="innerStream" />. See the Remarks section for more information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is null.- or -<paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x060021E4 RID: 8676 RVA: 0x00063294 File Offset: 0x00061494
		[MonoTODO]
		public NegotiateStream(Stream innerStream, bool leaveStreamOpen) : base(innerStream, leaveStreamOpen)
		{
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is readable.</summary>
		/// <returns>true if authentication has occurred and the underlying stream is readable; otherwise, false.</returns>
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x000632A0 File Offset: 0x000614A0
		public override bool CanRead
		{
			get
			{
				return base.InnerStream.CanRead;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is seekable.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x000632B0 File Offset: 0x000614B0
		public override bool CanSeek
		{
			get
			{
				return base.InnerStream.CanSeek;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream supports time-outs.</summary>
		/// <returns>true if the underlying stream supports time-outs; otherwise, false.</returns>
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x000632C0 File Offset: 0x000614C0
		[MonoTODO]
		public override bool CanTimeout
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is writable.</summary>
		/// <returns>true if authentication has occurred and the underlying stream is writable; otherwise, false.</returns>
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x000632C8 File Offset: 0x000614C8
		public override bool CanWrite
		{
			get
			{
				return base.InnerStream.CanWrite;
			}
		}

		/// <summary>Gets a value that indicates how the server can use the client's credentials.</summary>
		/// <returns>One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values.</returns>
		/// <exception cref="T:System.InvalidOperationException">Authentication failed or has not occurred.</exception>
		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x000632D8 File Offset: 0x000614D8
		[MonoTODO]
		public virtual TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether authentication was successful.</summary>
		/// <returns>true if successful authentication occurred; otherwise, false. </returns>
		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x000632E0 File Offset: 0x000614E0
		[MonoTODO]
		public override bool IsAuthenticated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this <see cref="T:System.Net.Security.NegotiateStream" /> uses data encryption.</summary>
		/// <returns>true if data is encrypted before being transmitted over the network and decrypted when it reaches the remote endpoint; otherwise, false.</returns>
		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x000632E8 File Offset: 0x000614E8
		[MonoTODO]
		public override bool IsEncrypted
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether both the server and the client have been authenticated.</summary>
		/// <returns>true if the server has been authenticated; otherwise, false.</returns>
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x000632F0 File Offset: 0x000614F0
		[MonoTODO]
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the local side of the connection used by this <see cref="T:System.Net.Security.NegotiateStream" /> was authenticated as the server.</summary>
		/// <returns>true if the local endpoint was successfully authenticated as the server side of the authenticated connection; otherwise, false.</returns>
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x000632F8 File Offset: 0x000614F8
		[MonoTODO]
		public override bool IsServer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the data sent using this stream is signed.</summary>
		/// <returns>true if the data is signed before being transmitted; otherwise, false.</returns>
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x00063300 File Offset: 0x00061500
		[MonoTODO]
		public override bool IsSigned
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the length of the underlying stream.</summary>
		/// <returns>A <see cref="T:System.Int64" /> that specifies the length of the underlying stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Getting the value of this property is not supported when the underlying stream is a <see cref="T:System.Net.Sockets.NetworkStream" />.</exception>
		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x00063308 File Offset: 0x00061508
		public override long Length
		{
			get
			{
				return base.InnerStream.Length;
			}
		}

		/// <summary>Gets or sets the current position in the underlying stream.</summary>
		/// <returns>A <see cref="T:System.Int64" /> that specifies the current position in the underlying stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Setting this property is not supported.- or -Getting the value of this property is not supported when the underlying stream is a <see cref="T:System.Net.Sockets.NetworkStream" />.</exception>
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x00063318 File Offset: 0x00061518
		// (set) Token: 0x060021F1 RID: 8689 RVA: 0x00063328 File Offset: 0x00061528
		public override long Position
		{
			get
			{
				return base.InnerStream.Position;
			}
			set
			{
				base.InnerStream.Position = value;
			}
		}

		/// <summary>Gets or sets the amount of time a read operation blocks waiting for data.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time that will elapse before a read operation fails. </returns>
		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x00063338 File Offset: 0x00061538
		// (set) Token: 0x060021F3 RID: 8691 RVA: 0x00063340 File Offset: 0x00061540
		public override int ReadTimeout
		{
			get
			{
				return this.readTimeout;
			}
			set
			{
				this.readTimeout = value;
			}
		}

		/// <summary>Gets information about the identity of the remote party sharing this authenticated stream.</summary>
		/// <returns>An <see cref="T:System.Security.Principal.IIdentity" /> object that describes the identity of the remote endpoint.</returns>
		/// <exception cref="T:System.InvalidOperationException">Authentication failed or has not occurred.</exception>
		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x0006334C File Offset: 0x0006154C
		[MonoTODO]
		public virtual IIdentity RemoteIdentity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the amount of time a write operation blocks waiting for data.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time that will elapse before a write operation fails. </returns>
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x00063354 File Offset: 0x00061554
		// (set) Token: 0x060021F6 RID: 8694 RVA: 0x0006335C File Offset: 0x0006155C
		public override int WriteTimeout
		{
			get
			{
				return this.writeTimeout;
			}
			set
			{
				this.writeTimeout = value;
			}
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. This method does not block.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation. </returns>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.- or -This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x060021F7 RID: 8695 RVA: 0x00063368 File Offset: 0x00061568
		[MonoTODO]
		public virtual IAsyncResult BeginAuthenticateAsClient(AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credentials. This method does not block.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation. </returns>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is null.- or -<paramref name="targetName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.- or -This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x060021F8 RID: 8696 RVA: 0x00063370 File Offset: 0x00061570
		[MonoTODO]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, string targetName, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credentials and authentication options. This method does not block.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation. </returns>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="allowedImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete. </param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is null.- or -<paramref name="targetName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.- or -This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x060021F9 RID: 8697 RVA: 0x00063378 File Offset: 0x00061578
		[MonoTODO]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Begins an asynchronous read operation that reads data from the stream and stores it in the specified array. </summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation. </returns>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that receives the bytes read from the stream.</param>
		/// <param name="offset">The zero-based location in <paramref name="buffer" /> at which to begin storing the data read from this stream.</param>
		/// <param name="count">The maximum number of bytes to read from the stream.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the read operation is complete. </param>
		/// <param name="asyncState">A user-defined object containing information about the read operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> is less than 0.- or -<paramref name="offset" /> is greater than the length of <paramref name="buffer" />.- or -<paramref name="offset" /> plus <paramref name="count" /> is greater than the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The read operation failed.- or -Encryption is in use, but the data could not be decrypted.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a read operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x060021FA RID: 8698 RVA: 0x00063380 File Offset: 0x00061580
		[MonoTODO]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. This method does not block.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation. </returns>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x060021FB RID: 8699 RVA: 0x00063388 File Offset: 0x00061588
		[MonoTODO]
		public virtual IAsyncResult BeginAuthenticateAsServer(AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified server credentials and authentication options. This method does not block.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation. </returns>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="requiredImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="requiredImpersonationLevel" /> must be <see cref="F:System.Security.Principal.TokenImpersonationLevel.Identification" />, <see cref="F:System.Security.Principal.TokenImpersonationLevel.Impersonation" />, or <see cref="F:System.Security.Principal.TokenImpersonationLevel.Delegation" />,</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.- or -This stream was used previously to attempt authentication as the client. You cannot use the stream to retry authentication as the server.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x060021FC RID: 8700 RVA: 0x00063390 File Offset: 0x00061590
		[MonoTODO]
		public virtual IAsyncResult BeginAuthenticateAsServer(NetworkCredential credential, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Begins an asynchronous write operation that writes <see cref="T:System.Byte" />s from the specified buffer to the stream.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation. </returns>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that supplies the bytes to be written to the stream.</param>
		/// <param name="offset">The zero-based location in<paramref name=" buffer" /> at which to begin reading bytes to be written to the stream.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> value that specifies the number of bytes to read from <paramref name="buffer" />.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the write operation is complete. </param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset is less than 0" />.- or -<paramref name="offset" /> is greater than the length of <paramref name="buffer" />.- or -<paramref name="offset" /> plus count is greater than the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.- or -Encryption is in use, but the data could not be encrypted.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a write operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x060021FD RID: 8701 RVA: 0x00063398 File Offset: 0x00061598
		[MonoTODO]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection.</summary>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.- or -This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x060021FE RID: 8702 RVA: 0x000633A0 File Offset: 0x000615A0
		[MonoTODO]
		public virtual void AuthenticateAsClient()
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified client credential. </summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.- or -This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is null.</exception>
		// Token: 0x060021FF RID: 8703 RVA: 0x000633A8 File Offset: 0x000615A8
		[MonoTODO]
		public virtual void AuthenticateAsClient(NetworkCredential credential, string targetName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credentials and authentication options.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="allowedImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowedImpersonationLevel" /> is not a valid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.- or -This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06002200 RID: 8704 RVA: 0x000633B0 File Offset: 0x000615B0
		[MonoTODO]
		public virtual void AuthenticateAsClient(NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection.</summary>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x06002201 RID: 8705 RVA: 0x000633B8 File Offset: 0x000615B8
		[MonoTODO]
		public virtual void AuthenticateAsServer()
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified server credentials and authentication options.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the server.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="requiredImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential " />is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to try to r-authenticate.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.- or -This stream was used previously to attempt authentication as the client. You cannot use the stream to retry authentication as the server.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="requiredImpersonationLevel" /> must be <see cref="F:System.Security.Principal.TokenImpersonationLevel.Identification" />, <see cref="F:System.Security.Principal.TokenImpersonationLevel.Impersonation" />, or <see cref="F:System.Security.Principal.TokenImpersonationLevel.Delegation" />,</exception>
		// Token: 0x06002202 RID: 8706 RVA: 0x000633C0 File Offset: 0x000615C0
		[MonoTODO]
		public virtual void AuthenticateAsServer(NetworkCredential credential, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Security.NegotiateStream" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002203 RID: 8707 RVA: 0x000633C8 File Offset: 0x000615C8
		[MonoTODO]
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
		}

		/// <summary>Ends a pending asynchronous client authentication operation that was started with a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsClient" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsClient" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsClient" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending client authentication to complete.</exception>
		// Token: 0x06002204 RID: 8708 RVA: 0x000633D0 File Offset: 0x000615D0
		[MonoTODO]
		public virtual void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		/// <summary>Ends an asynchronous read operation that was started with a call to <see cref="M:System.Net.Security.NegotiateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</summary>
		/// <returns>A <see cref="T:System.Int32" /> value that specifies the number of bytes read from the underlying stream.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="M:System.Net.Security.NegotiateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /></param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The asyncResult was not created by a call to <see cref="M:System.Net.Security.NegotiateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending read operation to complete.</exception>
		/// <exception cref="T:System.IO.IOException">The read operation failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x06002205 RID: 8709 RVA: 0x000633D8 File Offset: 0x000615D8
		[MonoTODO]
		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		/// <summary>Ends a pending asynchronous client authentication operation that was started with a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsServer" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsServer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsServer" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending authentication to complete.</exception>
		// Token: 0x06002206 RID: 8710 RVA: 0x000633E0 File Offset: 0x000615E0
		[MonoTODO]
		public virtual void EndAuthenticateAsServer(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		/// <summary>Ends an asynchronous write operation that was started with a call to <see cref="M:System.Net.Security.NegotiateStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="M:System.Net.Security.NegotiateStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /></param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The asyncResult was not created by a call to <see cref="M:System.Net.Security.NegotiateStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending write operation to complete.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x06002207 RID: 8711 RVA: 0x000633E8 File Offset: 0x000615E8
		[MonoTODO]
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		/// <summary>Causes any buffered data to be written to the underlying device.</summary>
		// Token: 0x06002208 RID: 8712 RVA: 0x000633F0 File Offset: 0x000615F0
		[MonoTODO]
		public override void Flush()
		{
			base.InnerStream.Flush();
		}

		/// <summary>Reads data from this stream and stores it in the specified array.</summary>
		/// <returns>A <see cref="T:System.Int32" /> value that specifies the number of bytes read from the underlying stream. When there is no more data to be read, returns 0.</returns>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that receives the bytes read from the stream.</param>
		/// <param name="offset">A <see cref="T:System.Int32" /> containing the zero-based location in <paramref name="buffer" /> at which to begin storing the data read from this stream.</param>
		/// <param name="count">A <see cref="T:System.Int32" /> containing the maximum number of bytes to read from the stream.</param>
		/// <exception cref="T:System.IO.IOException">The read operation failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		/// <exception cref="T:System.NotSupportedException">A <see cref="M:System.Net.Security.NegotiateStream.Read(System.Byte[],System.Int32,System.Int32)" /> operation is already in progress.</exception>
		// Token: 0x06002209 RID: 8713 RVA: 0x00063400 File Offset: 0x00061600
		[MonoTODO]
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>Throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="offset">This value is ignored.</param>
		/// <param name="origin">This value is ignored.</param>
		/// <exception cref="T:System.NotSupportedException">Seeking is not supported on <see cref="T:System.Net.Security.NegotiateStream" />.</exception>
		// Token: 0x0600220A RID: 8714 RVA: 0x00063408 File Offset: 0x00061608
		[MonoTODO]
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the length of the underlying stream.</summary>
		/// <param name="value">An <see cref="T:System.Int64" /> value that specifies the length of the stream.</param>
		// Token: 0x0600220B RID: 8715 RVA: 0x00063410 File Offset: 0x00061610
		[MonoTODO]
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Write the specified number of <see cref="T:System.Byte" />s to the underlying stream using the specified buffer and offset.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that supplies the bytes written to the stream.</param>
		/// <param name="offset">An <see cref="T:System.Int32" /> containing the zero-based location in<paramref name=" buffer" /> at which to begin reading bytes to be written to the stream.</param>
		/// <param name="count">A <see cref="T:System.Int32" /> containing the number of bytes to read from <paramref name="buffer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset is less than 0" />.- or -<paramref name="offset" /> is greater than the length of <paramref name="buffer" />.- or -<paramref name="offset" /> plus count is greater than the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.- or -Encryption is in use, but the data could not be encrypted.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a write operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x0600220C RID: 8716 RVA: 0x00063418 File Offset: 0x00061618
		[MonoTODO]
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040014F9 RID: 5369
		private int readTimeout;

		// Token: 0x040014FA RID: 5370
		private int writeTimeout;
	}
}
