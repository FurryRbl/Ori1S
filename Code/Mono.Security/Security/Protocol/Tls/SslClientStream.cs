using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Protocol.Tls.Handshake;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000099 RID: 153
	public class SslClientStream : SslStreamBase
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x0001FB88 File Offset: 0x0001DD88
		public SslClientStream(Stream stream, string targetHost, bool ownsStream) : this(stream, targetHost, ownsStream, SecurityProtocolType.Default, null)
		{
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001FB9C File Offset: 0x0001DD9C
		public SslClientStream(Stream stream, string targetHost, System.Security.Cryptography.X509Certificates.X509Certificate clientCertificate) : this(stream, targetHost, false, SecurityProtocolType.Default, new System.Security.Cryptography.X509Certificates.X509CertificateCollection(new System.Security.Cryptography.X509Certificates.X509Certificate[]
		{
			clientCertificate
		}))
		{
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
		public SslClientStream(Stream stream, string targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates) : this(stream, targetHost, false, SecurityProtocolType.Default, clientCertificates)
		{
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001FBDC File Offset: 0x0001DDDC
		public SslClientStream(Stream stream, string targetHost, bool ownsStream, SecurityProtocolType securityProtocolType) : this(stream, targetHost, ownsStream, securityProtocolType, new System.Security.Cryptography.X509Certificates.X509CertificateCollection())
		{
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001FBF0 File Offset: 0x0001DDF0
		public SslClientStream(Stream stream, string targetHost, bool ownsStream, SecurityProtocolType securityProtocolType, System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates) : base(stream, ownsStream)
		{
			if (targetHost == null || targetHost.Length == 0)
			{
				throw new ArgumentNullException("targetHost is null or an empty string.");
			}
			this.context = new ClientContext(this, securityProtocolType, targetHost, clientCertificates);
			this.protocol = new ClientRecordProtocol(this.innerStream, (ClientContext)this.context);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000580 RID: 1408 RVA: 0x0001FC50 File Offset: 0x0001DE50
		// (remove) Token: 0x06000581 RID: 1409 RVA: 0x0001FC6C File Offset: 0x0001DE6C
		internal event CertificateValidationCallback ServerCertValidation;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000582 RID: 1410 RVA: 0x0001FC88 File Offset: 0x0001DE88
		// (remove) Token: 0x06000583 RID: 1411 RVA: 0x0001FCA4 File Offset: 0x0001DEA4
		internal event CertificateSelectionCallback ClientCertSelection;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000584 RID: 1412 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
		// (remove) Token: 0x06000585 RID: 1413 RVA: 0x0001FCDC File Offset: 0x0001DEDC
		internal event PrivateKeySelectionCallback PrivateKeySelection;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000586 RID: 1414 RVA: 0x0001FCF8 File Offset: 0x0001DEF8
		// (remove) Token: 0x06000587 RID: 1415 RVA: 0x0001FD14 File Offset: 0x0001DF14
		public event CertificateValidationCallback2 ServerCertValidation2;

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001FD30 File Offset: 0x0001DF30
		internal Stream InputBuffer
		{
			get
			{
				return this.inputBuffer;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001FD38 File Offset: 0x0001DF38
		public System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.context.ClientSettings.Certificates;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0001FD4C File Offset: 0x0001DF4C
		public System.Security.Cryptography.X509Certificates.X509Certificate SelectedClientCertificate
		{
			get
			{
				return this.context.ClientSettings.ClientCertificate;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001FD60 File Offset: 0x0001DF60
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x0001FD68 File Offset: 0x0001DF68
		public CertificateValidationCallback ServerCertValidationDelegate
		{
			get
			{
				return this.ServerCertValidation;
			}
			set
			{
				this.ServerCertValidation = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001FD74 File Offset: 0x0001DF74
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0001FD7C File Offset: 0x0001DF7C
		public CertificateSelectionCallback ClientCertSelectionDelegate
		{
			get
			{
				return this.ClientCertSelection;
			}
			set
			{
				this.ClientCertSelection = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001FD88 File Offset: 0x0001DF88
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0001FD90 File Offset: 0x0001DF90
		public PrivateKeySelectionCallback PrivateKeyCertSelectionDelegate
		{
			get
			{
				return this.PrivateKeySelection;
			}
			set
			{
				this.PrivateKeySelection = value;
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001FD9C File Offset: 0x0001DF9C
		~SslClientStream()
		{
			base.Dispose(false);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001FDD8 File Offset: 0x0001DFD8
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this.ServerCertValidation = null;
				this.ClientCertSelection = null;
				this.PrivateKeySelection = null;
				this.ServerCertValidation2 = null;
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001FE04 File Offset: 0x0001E004
		internal override IAsyncResult OnBeginNegotiateHandshake(AsyncCallback callback, object state)
		{
			IAsyncResult result;
			try
			{
				if (this.context.HandshakeState != HandshakeState.None)
				{
					this.context.Clear();
				}
				this.context.SupportedCiphers = CipherSuiteFactory.GetSupportedCiphers(this.context.SecurityProtocol);
				this.context.HandshakeState = HandshakeState.Started;
				result = this.protocol.BeginSendRecord(HandshakeType.ClientHello, callback, state);
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
			return result;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001FEE4 File Offset: 0x0001E0E4
		private void SafeReceiveRecord(Stream s)
		{
			byte[] array = this.protocol.ReceiveRecord(s);
			if (array == null || array.Length == 0)
			{
				throw new TlsException(AlertDescription.HandshakeFailiure, "The server stopped the handshake.");
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001FF1C File Offset: 0x0001E11C
		internal override void OnNegotiateHandshakeCallback(IAsyncResult asyncResult)
		{
			this.protocol.EndSendRecord(asyncResult);
			while (this.context.LastHandshakeMsg != HandshakeType.ServerHelloDone)
			{
				this.SafeReceiveRecord(this.innerStream);
				if (this.context.AbbreviatedHandshake && this.context.LastHandshakeMsg == HandshakeType.ServerHello)
				{
					break;
				}
			}
			if (this.context.AbbreviatedHandshake)
			{
				ClientSessionCache.SetContextFromCache(this.context);
				this.context.Negotiating.Cipher.ComputeKeys();
				this.context.Negotiating.Cipher.InitializeCipher();
				this.protocol.SendChangeCipherSpec();
				while (this.context.HandshakeState != HandshakeState.Finished)
				{
					this.SafeReceiveRecord(this.innerStream);
				}
				this.protocol.SendRecord(HandshakeType.Finished);
			}
			else
			{
				bool flag = this.context.ServerSettings.CertificateRequest;
				if (this.context.SecurityProtocol == SecurityProtocolType.Ssl3)
				{
					flag = (this.context.ClientSettings.Certificates != null && this.context.ClientSettings.Certificates.Count > 0);
				}
				if (flag)
				{
					this.protocol.SendRecord(HandshakeType.Certificate);
				}
				this.protocol.SendRecord(HandshakeType.ClientKeyExchange);
				this.context.Negotiating.Cipher.InitializeCipher();
				if (flag && this.context.ClientSettings.ClientCertificate != null)
				{
					this.protocol.SendRecord(HandshakeType.CertificateVerify);
				}
				this.protocol.SendChangeCipherSpec();
				this.protocol.SendRecord(HandshakeType.Finished);
				while (this.context.HandshakeState != HandshakeState.Finished)
				{
					this.SafeReceiveRecord(this.innerStream);
				}
			}
			this.context.HandshakeMessages.Reset();
			this.context.ClearKeyInfo();
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0002010C File Offset: 0x0001E30C
		internal override System.Security.Cryptography.X509Certificates.X509Certificate OnLocalCertificateSelection(System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, string targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection serverRequestedCertificates)
		{
			if (this.ClientCertSelection != null)
			{
				return this.ClientCertSelection(clientCertificates, serverCertificate, targetHost, serverRequestedCertificates);
			}
			return null;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0002012C File Offset: 0x0001E32C
		internal override bool HaveRemoteValidation2Callback
		{
			get
			{
				return this.ServerCertValidation2 != null;
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0002013C File Offset: 0x0001E33C
		internal override ValidationResult OnRemoteCertificateValidation2(Mono.Security.X509.X509CertificateCollection collection)
		{
			CertificateValidationCallback2 serverCertValidation = this.ServerCertValidation2;
			if (serverCertValidation != null)
			{
				return serverCertValidation(collection);
			}
			return null;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00020160 File Offset: 0x0001E360
		internal override bool OnRemoteCertificateValidation(System.Security.Cryptography.X509Certificates.X509Certificate certificate, int[] errors)
		{
			if (this.ServerCertValidation != null)
			{
				return this.ServerCertValidation(certificate, errors);
			}
			return errors != null && errors.Length == 0;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00020198 File Offset: 0x0001E398
		internal virtual bool RaiseServerCertificateValidation(System.Security.Cryptography.X509Certificates.X509Certificate certificate, int[] certificateErrors)
		{
			return base.RaiseRemoteCertificateValidation(certificate, certificateErrors);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000201A4 File Offset: 0x0001E3A4
		internal virtual ValidationResult RaiseServerCertificateValidation2(Mono.Security.X509.X509CertificateCollection collection)
		{
			return base.RaiseRemoteCertificateValidation2(collection);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000201B0 File Offset: 0x0001E3B0
		internal System.Security.Cryptography.X509Certificates.X509Certificate RaiseClientCertificateSelection(System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, string targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection serverRequestedCertificates)
		{
			return base.RaiseLocalCertificateSelection(clientCertificates, serverCertificate, targetHost, serverRequestedCertificates);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000201C0 File Offset: 0x0001E3C0
		internal override AsymmetricAlgorithm OnLocalPrivateKeySelection(System.Security.Cryptography.X509Certificates.X509Certificate certificate, string targetHost)
		{
			if (this.PrivateKeySelection != null)
			{
				return this.PrivateKeySelection(certificate, targetHost);
			}
			return null;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000201DC File Offset: 0x0001E3DC
		internal AsymmetricAlgorithm RaisePrivateKeySelection(System.Security.Cryptography.X509Certificates.X509Certificate certificate, string targetHost)
		{
			return base.RaiseLocalPrivateKeySelection(certificate, targetHost);
		}
	}
}
