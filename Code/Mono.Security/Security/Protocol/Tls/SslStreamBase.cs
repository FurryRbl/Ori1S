using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200009D RID: 157
	public abstract class SslStreamBase : Stream, IDisposable
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x00021208 File Offset: 0x0001F408
		protected SslStreamBase(Stream stream, bool ownsStream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream is null.");
			}
			if (!stream.CanRead || !stream.CanWrite)
			{
				throw new ArgumentNullException("stream is not both readable and writable.");
			}
			this.inputBuffer = new MemoryStream();
			this.innerStream = stream;
			this.ownsStream = ownsStream;
			this.negotiate = new object();
			this.read = new object();
			this.write = new object();
			this.negotiationComplete = new ManualResetEvent(false);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000212C0 File Offset: 0x0001F4C0
		private void AsyncHandshakeCallback(IAsyncResult asyncResult)
		{
			SslStreamBase.InternalAsyncResult internalAsyncResult = asyncResult.AsyncState as SslStreamBase.InternalAsyncResult;
			try
			{
				try
				{
					this.OnNegotiateHandshakeCallback(asyncResult);
				}
				catch (TlsException ex)
				{
					this.protocol.SendAlert(ex.Alert);
					throw new IOException("The authentication or decryption has failed.", ex);
				}
				catch (Exception innerException)
				{
					this.protocol.SendAlert(AlertDescription.InternalError);
					throw new IOException("The authentication or decryption has failed.", innerException);
				}
				if (internalAsyncResult.ProceedAfterHandshake)
				{
					if (internalAsyncResult.FromWrite)
					{
						this.InternalBeginWrite(internalAsyncResult);
					}
					else
					{
						this.InternalBeginRead(internalAsyncResult);
					}
					this.negotiationComplete.Set();
				}
				else
				{
					this.negotiationComplete.Set();
					internalAsyncResult.SetComplete();
				}
			}
			catch (Exception complete)
			{
				this.negotiationComplete.Set();
				internalAsyncResult.SetComplete(complete);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x000213E0 File Offset: 0x0001F5E0
		internal bool MightNeedHandshake
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return false;
				}
				object obj = this.negotiate;
				bool result;
				lock (obj)
				{
					result = (this.context.HandshakeState != HandshakeState.Finished);
				}
				return result;
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00021450 File Offset: 0x0001F650
		internal void NegotiateHandshake()
		{
			if (this.MightNeedHandshake)
			{
				SslStreamBase.InternalAsyncResult asyncResult = new SslStreamBase.InternalAsyncResult(null, null, null, 0, 0, false, false);
				if (!this.BeginNegotiateHandshake(asyncResult))
				{
					this.negotiationComplete.WaitOne();
				}
				else
				{
					this.EndNegotiateHandshake(asyncResult);
				}
			}
		}

		// Token: 0x060005CC RID: 1484
		internal abstract IAsyncResult OnBeginNegotiateHandshake(AsyncCallback callback, object state);

		// Token: 0x060005CD RID: 1485
		internal abstract void OnNegotiateHandshakeCallback(IAsyncResult asyncResult);

		// Token: 0x060005CE RID: 1486
		internal abstract System.Security.Cryptography.X509Certificates.X509Certificate OnLocalCertificateSelection(System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, string targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection serverRequestedCertificates);

		// Token: 0x060005CF RID: 1487
		internal abstract bool OnRemoteCertificateValidation(System.Security.Cryptography.X509Certificates.X509Certificate certificate, int[] errors);

		// Token: 0x060005D0 RID: 1488
		internal abstract ValidationResult OnRemoteCertificateValidation2(Mono.Security.X509.X509CertificateCollection collection);

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005D1 RID: 1489
		internal abstract bool HaveRemoteValidation2Callback { get; }

		// Token: 0x060005D2 RID: 1490
		internal abstract AsymmetricAlgorithm OnLocalPrivateKeySelection(System.Security.Cryptography.X509Certificates.X509Certificate certificate, string targetHost);

		// Token: 0x060005D3 RID: 1491 RVA: 0x0002149C File Offset: 0x0001F69C
		internal System.Security.Cryptography.X509Certificates.X509Certificate RaiseLocalCertificateSelection(System.Security.Cryptography.X509Certificates.X509CertificateCollection certificates, System.Security.Cryptography.X509Certificates.X509Certificate remoteCertificate, string targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection requestedCertificates)
		{
			return this.OnLocalCertificateSelection(certificates, remoteCertificate, targetHost, requestedCertificates);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000214AC File Offset: 0x0001F6AC
		internal bool RaiseRemoteCertificateValidation(System.Security.Cryptography.X509Certificates.X509Certificate certificate, int[] errors)
		{
			return this.OnRemoteCertificateValidation(certificate, errors);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000214B8 File Offset: 0x0001F6B8
		internal ValidationResult RaiseRemoteCertificateValidation2(Mono.Security.X509.X509CertificateCollection collection)
		{
			return this.OnRemoteCertificateValidation2(collection);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000214C4 File Offset: 0x0001F6C4
		internal AsymmetricAlgorithm RaiseLocalPrivateKeySelection(System.Security.Cryptography.X509Certificates.X509Certificate certificate, string targetHost)
		{
			return this.OnLocalPrivateKeySelection(certificate, targetHost);
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x000214D0 File Offset: 0x0001F6D0
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x000214D8 File Offset: 0x0001F6D8
		public bool CheckCertRevocationStatus
		{
			get
			{
				return this.checkCertRevocationStatus;
			}
			set
			{
				this.checkCertRevocationStatus = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x000214E4 File Offset: 0x0001F6E4
		public CipherAlgorithmType CipherAlgorithm
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return this.context.Current.Cipher.CipherAlgorithmType;
				}
				return CipherAlgorithmType.None;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0002151C File Offset: 0x0001F71C
		public int CipherStrength
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return (int)this.context.Current.Cipher.EffectiveKeyBits;
				}
				return 0;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00021554 File Offset: 0x0001F754
		public HashAlgorithmType HashAlgorithm
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return this.context.Current.Cipher.HashAlgorithmType;
				}
				return HashAlgorithmType.None;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0002158C File Offset: 0x0001F78C
		public int HashStrength
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return this.context.Current.Cipher.HashSize * 8;
				}
				return 0;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x000215C4 File Offset: 0x0001F7C4
		public int KeyExchangeStrength
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return this.context.ServerSettings.Certificates[0].RSA.KeySize;
				}
				return 0;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00021604 File Offset: 0x0001F804
		public ExchangeAlgorithmType KeyExchangeAlgorithm
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return this.context.Current.Cipher.ExchangeAlgorithmType;
				}
				return ExchangeAlgorithmType.None;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0002163C File Offset: 0x0001F83C
		public SecurityProtocolType SecurityProtocol
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return this.context.SecurityProtocol;
				}
				return (SecurityProtocolType)0;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0002165C File Offset: 0x0001F85C
		public System.Security.Cryptography.X509Certificates.X509Certificate ServerCertificate
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished && this.context.ServerSettings.Certificates != null && this.context.ServerSettings.Certificates.Count > 0)
				{
					return new System.Security.Cryptography.X509Certificates.X509Certificate(this.context.ServerSettings.Certificates[0].RawData);
				}
				return null;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x000216CC File Offset: 0x0001F8CC
		internal Mono.Security.X509.X509CertificateCollection ServerCertificates
		{
			get
			{
				return this.context.ServerSettings.Certificates;
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000216E0 File Offset: 0x0001F8E0
		private bool BeginNegotiateHandshake(SslStreamBase.InternalAsyncResult asyncResult)
		{
			bool result;
			try
			{
				object obj = this.negotiate;
				lock (obj)
				{
					if (this.context.HandshakeState == HandshakeState.None)
					{
						this.OnBeginNegotiateHandshake(new AsyncCallback(this.AsyncHandshakeCallback), asyncResult);
						result = true;
					}
					else
					{
						result = false;
					}
				}
			}
			catch (TlsException ex)
			{
				this.negotiationComplete.Set();
				this.protocol.SendAlert(ex.Alert);
				throw new IOException("The authentication or decryption has failed.", ex);
			}
			catch (Exception innerException)
			{
				this.negotiationComplete.Set();
				this.protocol.SendAlert(AlertDescription.InternalError);
				throw new IOException("The authentication or decryption has failed.", innerException);
			}
			return result;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000217E4 File Offset: 0x0001F9E4
		private void EndNegotiateHandshake(SslStreamBase.InternalAsyncResult asyncResult)
		{
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			if (asyncResult.CompletedWithError)
			{
				throw asyncResult.AsyncException;
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0002181C File Offset: 0x0001FA1C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.checkDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer is a null reference.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset is less than 0.");
			}
			if (offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset is greater than the length of buffer.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count is less than 0.");
			}
			if (count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count is less than the length of buffer minus the value of the offset parameter.");
			}
			SslStreamBase.InternalAsyncResult internalAsyncResult = new SslStreamBase.InternalAsyncResult(callback, state, buffer, offset, count, false, true);
			if (this.MightNeedHandshake)
			{
				if (!this.BeginNegotiateHandshake(internalAsyncResult))
				{
					this.negotiationComplete.WaitOne();
					this.InternalBeginRead(internalAsyncResult);
				}
			}
			else
			{
				this.InternalBeginRead(internalAsyncResult);
			}
			return internalAsyncResult;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000218D4 File Offset: 0x0001FAD4
		private void InternalBeginRead(SslStreamBase.InternalAsyncResult asyncResult)
		{
			try
			{
				int num = 0;
				object obj = this.read;
				lock (obj)
				{
					bool flag = this.inputBuffer.Position == this.inputBuffer.Length && this.inputBuffer.Length > 0L;
					bool flag2 = this.inputBuffer.Length > 0L && asyncResult.Count > 0;
					if (flag)
					{
						this.resetBuffer();
					}
					else if (flag2)
					{
						num = this.inputBuffer.Read(asyncResult.Buffer, asyncResult.Offset, asyncResult.Count);
					}
				}
				if (0 < num)
				{
					asyncResult.SetComplete(num);
				}
				else if (!this.context.ReceivedConnectionEnd)
				{
					this.innerStream.BeginRead(this.recbuf, 0, this.recbuf.Length, new AsyncCallback(this.InternalReadCallback), new object[]
					{
						this.recbuf,
						asyncResult
					});
				}
				else
				{
					asyncResult.SetComplete(0);
				}
			}
			catch (TlsException ex)
			{
				this.protocol.SendAlert(ex.Alert);
				throw new IOException("The authentication or decryption has failed.", ex);
			}
			catch (Exception innerException)
			{
				throw new IOException("IO exception during read.", innerException);
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00021A74 File Offset: 0x0001FC74
		private void InternalReadCallback(IAsyncResult result)
		{
			if (this.disposed)
			{
				return;
			}
			object[] array = (object[])result.AsyncState;
			byte[] array2 = (byte[])array[0];
			SslStreamBase.InternalAsyncResult internalAsyncResult = (SslStreamBase.InternalAsyncResult)array[1];
			try
			{
				int num = this.innerStream.EndRead(result);
				if (num > 0)
				{
					this.recordStream.Write(array2, 0, num);
					bool flag = false;
					long position = this.recordStream.Position;
					this.recordStream.Position = 0L;
					byte[] array3 = null;
					if (this.recordStream.Length >= 5L)
					{
						array3 = this.protocol.ReceiveRecord(this.recordStream);
					}
					while (array3 != null)
					{
						long num2 = this.recordStream.Length - this.recordStream.Position;
						byte[] array4 = null;
						if (num2 > 0L)
						{
							array4 = new byte[num2];
							this.recordStream.Read(array4, 0, array4.Length);
						}
						object obj = this.read;
						lock (obj)
						{
							long position2 = this.inputBuffer.Position;
							if (array3.Length > 0)
							{
								this.inputBuffer.Seek(0L, SeekOrigin.End);
								this.inputBuffer.Write(array3, 0, array3.Length);
								this.inputBuffer.Seek(position2, SeekOrigin.Begin);
								flag = true;
							}
						}
						this.recordStream.SetLength(0L);
						array3 = null;
						if (num2 > 0L)
						{
							this.recordStream.Write(array4, 0, array4.Length);
							if (this.recordStream.Length >= 5L)
							{
								this.recordStream.Position = 0L;
								array3 = this.protocol.ReceiveRecord(this.recordStream);
								if (array3 == null)
								{
									position = this.recordStream.Length;
								}
							}
							else
							{
								position = num2;
							}
						}
						else
						{
							position = 0L;
						}
					}
					if (!flag && num > 0)
					{
						if (this.context.ReceivedConnectionEnd)
						{
							internalAsyncResult.SetComplete(0);
						}
						else
						{
							this.recordStream.Position = this.recordStream.Length;
							this.innerStream.BeginRead(array2, 0, array2.Length, new AsyncCallback(this.InternalReadCallback), array);
						}
					}
					else
					{
						this.recordStream.Position = position;
						int complete = 0;
						object obj2 = this.read;
						lock (obj2)
						{
							complete = this.inputBuffer.Read(internalAsyncResult.Buffer, internalAsyncResult.Offset, internalAsyncResult.Count);
						}
						internalAsyncResult.SetComplete(complete);
					}
				}
				else
				{
					internalAsyncResult.SetComplete(0);
				}
			}
			catch (Exception complete2)
			{
				internalAsyncResult.SetComplete(complete2);
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00021D74 File Offset: 0x0001FF74
		private void InternalBeginWrite(SslStreamBase.InternalAsyncResult asyncResult)
		{
			try
			{
				object obj = this.write;
				lock (obj)
				{
					byte[] array = this.protocol.EncodeRecord(ContentType.ApplicationData, asyncResult.Buffer, asyncResult.Offset, asyncResult.Count);
					this.innerStream.BeginWrite(array, 0, array.Length, new AsyncCallback(this.InternalWriteCallback), asyncResult);
				}
			}
			catch (TlsException ex)
			{
				this.protocol.SendAlert(ex.Alert);
				this.Close();
				throw new IOException("The authentication or decryption has failed.", ex);
			}
			catch (Exception innerException)
			{
				throw new IOException("IO exception during Write.", innerException);
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00021E64 File Offset: 0x00020064
		private void InternalWriteCallback(IAsyncResult ar)
		{
			if (this.disposed)
			{
				return;
			}
			SslStreamBase.InternalAsyncResult internalAsyncResult = (SslStreamBase.InternalAsyncResult)ar.AsyncState;
			try
			{
				this.innerStream.EndWrite(ar);
				internalAsyncResult.SetComplete();
			}
			catch (Exception complete)
			{
				internalAsyncResult.SetComplete(complete);
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00021ECC File Offset: 0x000200CC
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.checkDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer is a null reference.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset is less than 0.");
			}
			if (offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset is greater than the length of buffer.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count is less than 0.");
			}
			if (count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count is less than the length of buffer minus the value of the offset parameter.");
			}
			SslStreamBase.InternalAsyncResult internalAsyncResult = new SslStreamBase.InternalAsyncResult(callback, state, buffer, offset, count, true, true);
			if (this.MightNeedHandshake)
			{
				if (!this.BeginNegotiateHandshake(internalAsyncResult))
				{
					this.negotiationComplete.WaitOne();
					this.InternalBeginWrite(internalAsyncResult);
				}
			}
			else
			{
				this.InternalBeginWrite(internalAsyncResult);
			}
			return internalAsyncResult;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00021F84 File Offset: 0x00020184
		public override int EndRead(IAsyncResult asyncResult)
		{
			this.checkDisposed();
			SslStreamBase.InternalAsyncResult internalAsyncResult = asyncResult as SslStreamBase.InternalAsyncResult;
			if (internalAsyncResult == null)
			{
				throw new ArgumentNullException("asyncResult is null or was not obtained by calling BeginRead.");
			}
			if (!asyncResult.IsCompleted && !asyncResult.AsyncWaitHandle.WaitOne(300000, false))
			{
				throw new TlsException(AlertDescription.InternalError, "Couldn't complete EndRead");
			}
			if (internalAsyncResult.CompletedWithError)
			{
				throw internalAsyncResult.AsyncException;
			}
			return internalAsyncResult.BytesRead;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00021FF8 File Offset: 0x000201F8
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.checkDisposed();
			SslStreamBase.InternalAsyncResult internalAsyncResult = asyncResult as SslStreamBase.InternalAsyncResult;
			if (internalAsyncResult == null)
			{
				throw new ArgumentNullException("asyncResult is null or was not obtained by calling BeginWrite.");
			}
			if (!asyncResult.IsCompleted && !internalAsyncResult.AsyncWaitHandle.WaitOne(300000, false))
			{
				throw new TlsException(AlertDescription.InternalError, "Couldn't complete EndWrite");
			}
			if (internalAsyncResult.CompletedWithError)
			{
				throw internalAsyncResult.AsyncException;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00022064 File Offset: 0x00020264
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0002206C File Offset: 0x0002026C
		public override void Flush()
		{
			this.checkDisposed();
			this.innerStream.Flush();
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00022080 File Offset: 0x00020280
		public int Read(byte[] buffer)
		{
			return this.Read(buffer, 0, buffer.Length);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00022090 File Offset: 0x00020290
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.checkDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset is less than 0.");
			}
			if (offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset is greater than the length of buffer.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count is less than 0.");
			}
			if (count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count is less than the length of buffer minus the value of the offset parameter.");
			}
			if (this.context.HandshakeState != HandshakeState.Finished)
			{
				this.NegotiateHandshake();
			}
			object obj = this.read;
			int result;
			lock (obj)
			{
				try
				{
					SslStreamBase.record_processing.Reset();
					if (this.inputBuffer.Position > 0L)
					{
						if (this.inputBuffer.Position == this.inputBuffer.Length)
						{
							this.inputBuffer.SetLength(0L);
						}
						else
						{
							int num = this.inputBuffer.Read(buffer, offset, count);
							if (num > 0)
							{
								SslStreamBase.record_processing.Set();
								return num;
							}
						}
					}
					bool flag = false;
					for (;;)
					{
						if (this.recordStream.Position == 0L || flag)
						{
							flag = false;
							byte[] array = new byte[16384];
							int num2 = 0;
							if (count == 1)
							{
								int num3 = this.innerStream.ReadByte();
								if (num3 >= 0)
								{
									array[0] = (byte)num3;
									num2 = 1;
								}
							}
							else
							{
								num2 = this.innerStream.Read(array, 0, array.Length);
							}
							if (num2 <= 0)
							{
								break;
							}
							if (this.recordStream.Length > 0L && this.recordStream.Position != this.recordStream.Length)
							{
								this.recordStream.Seek(0L, SeekOrigin.End);
							}
							this.recordStream.Write(array, 0, num2);
						}
						bool flag2 = false;
						this.recordStream.Position = 0L;
						byte[] array2 = null;
						if (this.recordStream.Length >= 5L)
						{
							array2 = this.protocol.ReceiveRecord(this.recordStream);
							flag = (array2 == null);
						}
						while (array2 != null)
						{
							long num4 = this.recordStream.Length - this.recordStream.Position;
							byte[] array3 = null;
							if (num4 > 0L)
							{
								array3 = new byte[num4];
								this.recordStream.Read(array3, 0, array3.Length);
							}
							long position = this.inputBuffer.Position;
							if (array2.Length > 0)
							{
								this.inputBuffer.Seek(0L, SeekOrigin.End);
								this.inputBuffer.Write(array2, 0, array2.Length);
								this.inputBuffer.Seek(position, SeekOrigin.Begin);
								flag2 = true;
							}
							this.recordStream.SetLength(0L);
							array2 = null;
							if (num4 > 0L)
							{
								this.recordStream.Write(array3, 0, array3.Length);
							}
							if (flag2)
							{
								goto Block_23;
							}
						}
					}
					SslStreamBase.record_processing.Set();
					return 0;
					Block_23:
					int num5 = this.inputBuffer.Read(buffer, offset, count);
					SslStreamBase.record_processing.Set();
					result = num5;
				}
				catch (TlsException innerException)
				{
					throw new IOException("The authentication or decryption has failed.", innerException);
				}
				catch (Exception innerException2)
				{
					throw new IOException("IO exception during read.", innerException2);
				}
			}
			return result;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00022420 File Offset: 0x00020620
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00022428 File Offset: 0x00020628
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00022430 File Offset: 0x00020630
		public void Write(byte[] buffer)
		{
			this.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00022440 File Offset: 0x00020640
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.checkDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset is less than 0.");
			}
			if (offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset is greater than the length of buffer.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count is less than 0.");
			}
			if (count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count is less than the length of buffer minus the value of the offset parameter.");
			}
			if (this.context.HandshakeState != HandshakeState.Finished)
			{
				this.NegotiateHandshake();
			}
			object obj = this.write;
			lock (obj)
			{
				try
				{
					byte[] array = this.protocol.EncodeRecord(ContentType.ApplicationData, buffer, offset, count);
					this.innerStream.Write(array, 0, array.Length);
				}
				catch (TlsException ex)
				{
					this.protocol.SendAlert(ex.Alert);
					this.Close();
					throw new IOException("The authentication or decryption has failed.", ex);
				}
				catch (Exception innerException)
				{
					throw new IOException("IO exception during Write.", innerException);
				}
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00022590 File Offset: 0x00020790
		public override bool CanRead
		{
			get
			{
				return this.innerStream.CanRead;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x000225A0 File Offset: 0x000207A0
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x000225A4 File Offset: 0x000207A4
		public override bool CanWrite
		{
			get
			{
				return this.innerStream.CanWrite;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x000225B4 File Offset: 0x000207B4
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x000225BC File Offset: 0x000207BC
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x000225C4 File Offset: 0x000207C4
		public override long Position
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

		// Token: 0x060005FA RID: 1530 RVA: 0x000225CC File Offset: 0x000207CC
		~SslStreamBase()
		{
			this.Dispose(false);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00022608 File Offset: 0x00020808
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (this.innerStream != null)
					{
						if (this.context.HandshakeState == HandshakeState.Finished && !this.context.SentConnectionEnd)
						{
							try
							{
								this.protocol.SendAlert(AlertDescription.CloseNotify);
							}
							catch
							{
							}
						}
						if (this.ownsStream)
						{
							this.innerStream.Close();
						}
					}
					this.ownsStream = false;
					this.innerStream = null;
				}
				this.disposed = true;
				base.Dispose(disposing);
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000226BC File Offset: 0x000208BC
		private void resetBuffer()
		{
			this.inputBuffer.SetLength(0L);
			this.inputBuffer.Position = 0L;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000226D8 File Offset: 0x000208D8
		internal void checkDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("The Stream is closed.");
			}
		}

		// Token: 0x040002D5 RID: 725
		private const int WaitTimeOut = 300000;

		// Token: 0x040002D6 RID: 726
		private static ManualResetEvent record_processing = new ManualResetEvent(true);

		// Token: 0x040002D7 RID: 727
		internal Stream innerStream;

		// Token: 0x040002D8 RID: 728
		internal MemoryStream inputBuffer;

		// Token: 0x040002D9 RID: 729
		internal Context context;

		// Token: 0x040002DA RID: 730
		internal RecordProtocol protocol;

		// Token: 0x040002DB RID: 731
		internal bool ownsStream;

		// Token: 0x040002DC RID: 732
		private volatile bool disposed;

		// Token: 0x040002DD RID: 733
		private bool checkCertRevocationStatus;

		// Token: 0x040002DE RID: 734
		private object negotiate;

		// Token: 0x040002DF RID: 735
		private object read;

		// Token: 0x040002E0 RID: 736
		private object write;

		// Token: 0x040002E1 RID: 737
		private ManualResetEvent negotiationComplete;

		// Token: 0x040002E2 RID: 738
		private byte[] recbuf = new byte[16384];

		// Token: 0x040002E3 RID: 739
		private MemoryStream recordStream = new MemoryStream();

		// Token: 0x0200009E RID: 158
		private class InternalAsyncResult : IAsyncResult
		{
			// Token: 0x060005FE RID: 1534 RVA: 0x000226F4 File Offset: 0x000208F4
			public InternalAsyncResult(AsyncCallback userCallback, object userState, byte[] buffer, int offset, int count, bool fromWrite, bool proceedAfterHandshake)
			{
				this._userCallback = userCallback;
				this._userState = userState;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
				this._fromWrite = fromWrite;
				this._proceedAfterHandshake = proceedAfterHandshake;
			}

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x060005FF RID: 1535 RVA: 0x00022748 File Offset: 0x00020948
			public bool ProceedAfterHandshake
			{
				get
				{
					return this._proceedAfterHandshake;
				}
			}

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x06000600 RID: 1536 RVA: 0x00022750 File Offset: 0x00020950
			public bool FromWrite
			{
				get
				{
					return this._fromWrite;
				}
			}

			// Token: 0x17000183 RID: 387
			// (get) Token: 0x06000601 RID: 1537 RVA: 0x00022758 File Offset: 0x00020958
			public byte[] Buffer
			{
				get
				{
					return this._buffer;
				}
			}

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x06000602 RID: 1538 RVA: 0x00022760 File Offset: 0x00020960
			public int Offset
			{
				get
				{
					return this._offset;
				}
			}

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x06000603 RID: 1539 RVA: 0x00022768 File Offset: 0x00020968
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x06000604 RID: 1540 RVA: 0x00022770 File Offset: 0x00020970
			public int BytesRead
			{
				get
				{
					return this._bytesRead;
				}
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x06000605 RID: 1541 RVA: 0x00022778 File Offset: 0x00020978
			public object AsyncState
			{
				get
				{
					return this._userState;
				}
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x06000606 RID: 1542 RVA: 0x00022780 File Offset: 0x00020980
			public Exception AsyncException
			{
				get
				{
					return this._asyncException;
				}
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x06000607 RID: 1543 RVA: 0x00022788 File Offset: 0x00020988
			public bool CompletedWithError
			{
				get
				{
					return this.IsCompleted && null != this._asyncException;
				}
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x06000608 RID: 1544 RVA: 0x000227A4 File Offset: 0x000209A4
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					object obj = this.locker;
					lock (obj)
					{
						if (this.handle == null)
						{
							this.handle = new ManualResetEvent(this.completed);
						}
					}
					return this.handle;
				}
			}

			// Token: 0x1700018B RID: 395
			// (get) Token: 0x06000609 RID: 1545 RVA: 0x00022808 File Offset: 0x00020A08
			public bool CompletedSynchronously
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x0600060A RID: 1546 RVA: 0x0002280C File Offset: 0x00020A0C
			public bool IsCompleted
			{
				get
				{
					object obj = this.locker;
					bool result;
					lock (obj)
					{
						result = this.completed;
					}
					return result;
				}
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x0002285C File Offset: 0x00020A5C
			private void SetComplete(Exception ex, int bytesRead)
			{
				object obj = this.locker;
				lock (obj)
				{
					if (this.completed)
					{
						return;
					}
					this.completed = true;
					this._asyncException = ex;
					this._bytesRead = bytesRead;
					if (this.handle != null)
					{
						this.handle.Set();
					}
				}
				if (this._userCallback != null)
				{
					this._userCallback.BeginInvoke(this, null, null);
				}
			}

			// Token: 0x0600060C RID: 1548 RVA: 0x000228F4 File Offset: 0x00020AF4
			public void SetComplete(Exception ex)
			{
				this.SetComplete(ex, 0);
			}

			// Token: 0x0600060D RID: 1549 RVA: 0x00022900 File Offset: 0x00020B00
			public void SetComplete(int bytesRead)
			{
				this.SetComplete(null, bytesRead);
			}

			// Token: 0x0600060E RID: 1550 RVA: 0x0002290C File Offset: 0x00020B0C
			public void SetComplete()
			{
				this.SetComplete(null, 0);
			}

			// Token: 0x040002E4 RID: 740
			private object locker = new object();

			// Token: 0x040002E5 RID: 741
			private AsyncCallback _userCallback;

			// Token: 0x040002E6 RID: 742
			private object _userState;

			// Token: 0x040002E7 RID: 743
			private Exception _asyncException;

			// Token: 0x040002E8 RID: 744
			private ManualResetEvent handle;

			// Token: 0x040002E9 RID: 745
			private bool completed;

			// Token: 0x040002EA RID: 746
			private int _bytesRead;

			// Token: 0x040002EB RID: 747
			private bool _fromWrite;

			// Token: 0x040002EC RID: 748
			private bool _proceedAfterHandshake;

			// Token: 0x040002ED RID: 749
			private byte[] _buffer;

			// Token: 0x040002EE RID: 750
			private int _offset;

			// Token: 0x040002EF RID: 751
			private int _count;
		}

		// Token: 0x020000C9 RID: 201
		// (Invoke) Token: 0x06000709 RID: 1801
		private delegate void AsyncHandshakeDelegate(SslStreamBase.InternalAsyncResult asyncResult, bool fromWrite);
	}
}
