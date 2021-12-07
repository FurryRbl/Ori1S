using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x020000B1 RID: 177
	internal class TlsServerKeyExchange : HandshakeMessage
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x00024B5C File Offset: 0x00022D5C
		public TlsServerKeyExchange(Context context, byte[] buffer) : base(context, HandshakeType.ServerKeyExchange, buffer)
		{
			this.verifySignature();
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00024B70 File Offset: 0x00022D70
		public override void Update()
		{
			base.Update();
			base.Context.ServerSettings.ServerKeyExchange = true;
			base.Context.ServerSettings.RsaParameters = this.rsaParams;
			base.Context.ServerSettings.SignedParams = this.signedParams;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00024BC0 File Offset: 0x00022DC0
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00024BC8 File Offset: 0x00022DC8
		protected override void ProcessAsTls1()
		{
			this.rsaParams = default(RSAParameters);
			this.rsaParams.Modulus = base.ReadBytes((int)base.ReadInt16());
			this.rsaParams.Exponent = base.ReadBytes((int)base.ReadInt16());
			this.signedParams = base.ReadBytes((int)base.ReadInt16());
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00024C24 File Offset: 0x00022E24
		private void verifySignature()
		{
			MD5SHA1 md5SHA = new MD5SHA1();
			int count = this.rsaParams.Modulus.Length + this.rsaParams.Exponent.Length + 4;
			TlsStream tlsStream = new TlsStream();
			tlsStream.Write(base.Context.RandomCS);
			tlsStream.Write(base.ToArray(), 0, count);
			md5SHA.ComputeHash(tlsStream.ToArray());
			tlsStream.Reset();
			if (!md5SHA.VerifySignature(base.Context.ServerSettings.CertificateRSA, this.signedParams))
			{
				throw new TlsException(AlertDescription.DecodeError, "Data was not signed with the server certificate.");
			}
		}

		// Token: 0x04000327 RID: 807
		private RSAParameters rsaParams;

		// Token: 0x04000328 RID: 808
		private byte[] signedParams;
	}
}
