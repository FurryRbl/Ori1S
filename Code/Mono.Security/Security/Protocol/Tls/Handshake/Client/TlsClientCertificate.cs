using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x020000A7 RID: 167
	internal class TlsClientCertificate : HandshakeMessage
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x0002366C File Offset: 0x0002186C
		public TlsClientCertificate(Context context) : base(context, HandshakeType.Certificate)
		{
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00023678 File Offset: 0x00021878
		public X509Certificate ClientCertificate
		{
			get
			{
				if (!this.clientCertSelected)
				{
					this.GetClientCertificate();
					this.clientCertSelected = true;
				}
				return this.clientCert;
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00023698 File Offset: 0x00021898
		public override void Update()
		{
			base.Update();
			base.Reset();
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000236A8 File Offset: 0x000218A8
		private void GetClientCertificate()
		{
			ClientContext clientContext = (ClientContext)base.Context;
			if (clientContext.ClientSettings.Certificates != null && clientContext.ClientSettings.Certificates.Count > 0)
			{
				this.clientCert = clientContext.SslStream.RaiseClientCertificateSelection(base.Context.ClientSettings.Certificates, new X509Certificate(base.Context.ServerSettings.Certificates[0].RawData), base.Context.ClientSettings.TargetHost, null);
			}
			clientContext.ClientSettings.ClientCertificate = this.clientCert;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0002374C File Offset: 0x0002194C
		private void SendCertificates()
		{
			TlsStream tlsStream = new TlsStream();
			for (X509Certificate x509Certificate = this.ClientCertificate; x509Certificate != null; x509Certificate = this.FindParentCertificate(x509Certificate))
			{
				byte[] rawCertData = x509Certificate.GetRawCertData();
				tlsStream.WriteInt24(rawCertData.Length);
				tlsStream.Write(rawCertData);
			}
			base.WriteInt24((int)tlsStream.Length);
			base.Write(tlsStream.ToArray());
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000237AC File Offset: 0x000219AC
		protected override void ProcessAsSsl3()
		{
			if (this.ClientCertificate != null)
			{
				this.SendCertificates();
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000237C4 File Offset: 0x000219C4
		protected override void ProcessAsTls1()
		{
			if (this.ClientCertificate != null)
			{
				this.SendCertificates();
			}
			else
			{
				base.WriteInt24(0);
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000237E4 File Offset: 0x000219E4
		private X509Certificate FindParentCertificate(X509Certificate cert)
		{
			if (cert.GetName() == cert.GetIssuerName())
			{
				return null;
			}
			foreach (X509Certificate result in base.Context.ClientSettings.Certificates)
			{
				if (cert.GetName() == cert.GetIssuerName())
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x0400031B RID: 795
		private bool clientCertSelected;

		// Token: 0x0400031C RID: 796
		private X509Certificate clientCert;
	}
}
