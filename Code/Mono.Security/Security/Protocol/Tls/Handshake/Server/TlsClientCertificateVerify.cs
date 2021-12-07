using System;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x020000B3 RID: 179
	internal class TlsClientCertificateVerify : HandshakeMessage
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x000251B8 File Offset: 0x000233B8
		public TlsClientCertificateVerify(Context context, byte[] buffer) : base(context, HandshakeType.CertificateVerify, buffer)
		{
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000251C4 File Offset: 0x000233C4
		protected override void ProcessAsSsl3()
		{
			ServerContext serverContext = (ServerContext)base.Context;
			int count = (int)base.ReadInt16();
			byte[] rgbSignature = base.ReadBytes(count);
			SslHandshakeHash sslHandshakeHash = new SslHandshakeHash(serverContext.MasterSecret);
			sslHandshakeHash.TransformFinalBlock(serverContext.HandshakeMessages.ToArray(), 0, (int)serverContext.HandshakeMessages.Length);
			if (!sslHandshakeHash.VerifySignature(serverContext.ClientSettings.CertificateRSA, rgbSignature))
			{
				throw new TlsException(AlertDescription.HandshakeFailiure, "Handshake Failure.");
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0002523C File Offset: 0x0002343C
		protected override void ProcessAsTls1()
		{
			ServerContext serverContext = (ServerContext)base.Context;
			int count = (int)base.ReadInt16();
			byte[] rgbSignature = base.ReadBytes(count);
			MD5SHA1 md5SHA = new MD5SHA1();
			md5SHA.ComputeHash(serverContext.HandshakeMessages.ToArray(), 0, (int)serverContext.HandshakeMessages.Length);
			if (!md5SHA.VerifySignature(serverContext.ClientSettings.CertificateRSA, rgbSignature))
			{
				throw new TlsException(AlertDescription.HandshakeFailiure, "Handshake Failure.");
			}
		}
	}
}
