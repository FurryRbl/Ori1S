using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Protocol.Tls.Handshake;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200009C RID: 156
	public class SslServerStream : SslStreamBase
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x00020D5C File Offset: 0x0001EF5C
		public SslServerStream(Stream stream, System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate) : this(stream, serverCertificate, false, false, SecurityProtocolType.Default)
		{
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00020D70 File Offset: 0x0001EF70
		public SslServerStream(Stream stream, System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, bool clientCertificateRequired, bool ownsStream) : this(stream, serverCertificate, clientCertificateRequired, ownsStream, SecurityProtocolType.Default)
		{
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00020D84 File Offset: 0x0001EF84
		public SslServerStream(Stream stream, System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, bool clientCertificateRequired, bool requestClientCertificate, bool ownsStream) : this(stream, serverCertificate, clientCertificateRequired, requestClientCertificate, ownsStream, SecurityProtocolType.Default)
		{
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00020D98 File Offset: 0x0001EF98
		public SslServerStream(Stream stream, System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, bool clientCertificateRequired, bool ownsStream, SecurityProtocolType securityProtocolType) : this(stream, serverCertificate, clientCertificateRequired, false, ownsStream, securityProtocolType)
		{
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00020DA8 File Offset: 0x0001EFA8
		public SslServerStream(Stream stream, System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, bool clientCertificateRequired, bool requestClientCertificate, bool ownsStream, SecurityProtocolType securityProtocolType) : base(stream, ownsStream)
		{
			this.context = new ServerContext(this, securityProtocolType, serverCertificate, clientCertificateRequired, requestClientCertificate);
			this.protocol = new ServerRecordProtocol(this.innerStream, (ServerContext)this.context);
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060005B1 RID: 1457 RVA: 0x00020DEC File Offset: 0x0001EFEC
		// (remove) Token: 0x060005B2 RID: 1458 RVA: 0x00020E08 File Offset: 0x0001F008
		internal event CertificateValidationCallback ClientCertValidation;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060005B3 RID: 1459 RVA: 0x00020E24 File Offset: 0x0001F024
		// (remove) Token: 0x060005B4 RID: 1460 RVA: 0x00020E40 File Offset: 0x0001F040
		internal event PrivateKeySelectionCallback PrivateKeySelection;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060005B5 RID: 1461 RVA: 0x00020E5C File Offset: 0x0001F05C
		// (remove) Token: 0x060005B6 RID: 1462 RVA: 0x00020E78 File Offset: 0x0001F078
		public event CertificateValidationCallback2 ClientCertValidation2;

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00020E94 File Offset: 0x0001F094
		public System.Security.Cryptography.X509Certificates.X509Certificate ClientCertificate
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return this.context.ClientSettings.ClientCertificate;
				}
				return null;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00020EBC File Offset: 0x0001F0BC
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00020EC4 File Offset: 0x0001F0C4
		public CertificateValidationCallback ClientCertValidationDelegate
		{
			get
			{
				return this.ClientCertValidation;
			}
			set
			{
				this.ClientCertValidation = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00020ED0 File Offset: 0x0001F0D0
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00020ED8 File Offset: 0x0001F0D8
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

		// Token: 0x060005BC RID: 1468 RVA: 0x00020EE4 File Offset: 0x0001F0E4
		~SslServerStream()
		{
			this.Dispose(false);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00020F20 File Offset: 0x0001F120
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this.ClientCertValidation = null;
				this.PrivateKeySelection = null;
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00020F40 File Offset: 0x0001F140
		internal override IAsyncResult OnBeginNegotiateHandshake(AsyncCallback callback, object state)
		{
			if (this.context.HandshakeState != HandshakeState.None)
			{
				this.context.Clear();
			}
			this.context.SupportedCiphers = CipherSuiteFactory.GetSupportedCiphers(this.context.SecurityProtocol);
			this.context.HandshakeState = HandshakeState.Started;
			return this.protocol.BeginReceiveRecord(this.innerStream, callback, state);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00020FA4 File Offset: 0x0001F1A4
		internal override void OnNegotiateHandshakeCallback(IAsyncResult asyncResult)
		{
			this.protocol.EndReceiveRecord(asyncResult);
			if (this.context.LastHandshakeMsg != HandshakeType.ClientHello)
			{
				this.protocol.SendAlert(AlertDescription.UnexpectedMessage);
			}
			this.protocol.SendRecord(HandshakeType.ServerHello);
			this.protocol.SendRecord(HandshakeType.Certificate);
			if (this.context.Negotiating.Cipher.IsExportable)
			{
				this.protocol.SendRecord(HandshakeType.ServerKeyExchange);
			}
			bool flag = false;
			if (this.context.Negotiating.Cipher.IsExportable || ((ServerContext)this.context).ClientCertificateRequired || ((ServerContext)this.context).RequestClientCertificate)
			{
				this.protocol.SendRecord(HandshakeType.CertificateRequest);
				flag = true;
			}
			this.protocol.SendRecord(HandshakeType.ServerHelloDone);
			while (this.context.LastHandshakeMsg != HandshakeType.Finished)
			{
				byte[] array = this.protocol.ReceiveRecord(this.innerStream);
				if (array == null || array.Length == 0)
				{
					throw new TlsException(AlertDescription.HandshakeFailiure, "The client stopped the handshake.");
				}
			}
			if (flag)
			{
				System.Security.Cryptography.X509Certificates.X509Certificate clientCertificate = this.context.ClientSettings.ClientCertificate;
				if (clientCertificate == null && ((ServerContext)this.context).ClientCertificateRequired)
				{
					throw new TlsException(AlertDescription.BadCertificate, "No certificate received from client.");
				}
				if (!this.RaiseClientCertificateValidation(clientCertificate, new int[0]))
				{
					throw new TlsException(AlertDescription.BadCertificate, "Client certificate not accepted.");
				}
			}
			this.protocol.SendChangeCipherSpec();
			this.protocol.SendRecord(HandshakeType.Finished);
			this.context.HandshakeState = HandshakeState.Finished;
			this.context.HandshakeMessages.Reset();
			this.context.ClearKeyInfo();
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00021160 File Offset: 0x0001F360
		internal override System.Security.Cryptography.X509Certificates.X509Certificate OnLocalCertificateSelection(System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, string targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection serverRequestedCertificates)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00021168 File Offset: 0x0001F368
		internal override bool OnRemoteCertificateValidation(System.Security.Cryptography.X509Certificates.X509Certificate certificate, int[] errors)
		{
			if (this.ClientCertValidation != null)
			{
				return this.ClientCertValidation(certificate, errors);
			}
			return errors != null && errors.Length == 0;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x000211A0 File Offset: 0x0001F3A0
		internal override bool HaveRemoteValidation2Callback
		{
			get
			{
				return this.ClientCertValidation2 != null;
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000211B0 File Offset: 0x0001F3B0
		internal override ValidationResult OnRemoteCertificateValidation2(Mono.Security.X509.X509CertificateCollection collection)
		{
			CertificateValidationCallback2 clientCertValidation = this.ClientCertValidation2;
			if (clientCertValidation != null)
			{
				return clientCertValidation(collection);
			}
			return null;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000211D4 File Offset: 0x0001F3D4
		internal bool RaiseClientCertificateValidation(System.Security.Cryptography.X509Certificates.X509Certificate certificate, int[] certificateErrors)
		{
			return base.RaiseRemoteCertificateValidation(certificate, certificateErrors);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000211E0 File Offset: 0x0001F3E0
		internal override AsymmetricAlgorithm OnLocalPrivateKeySelection(System.Security.Cryptography.X509Certificates.X509Certificate certificate, string targetHost)
		{
			if (this.PrivateKeySelection != null)
			{
				return this.PrivateKeySelection(certificate, targetHost);
			}
			return null;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000211FC File Offset: 0x0001F3FC
		internal AsymmetricAlgorithm RaisePrivateKeySelection(System.Security.Cryptography.X509Certificates.X509Certificate certificate, string targetHost)
		{
			return base.RaiseLocalPrivateKeySelection(certificate, targetHost);
		}
	}
}
