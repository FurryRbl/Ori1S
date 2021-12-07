using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x020000B6 RID: 182
	internal class TlsClientKeyExchange : HandshakeMessage
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x00025614 File Offset: 0x00023814
		public TlsClientKeyExchange(Context context, byte[] buffer) : base(context, HandshakeType.ClientKeyExchange, buffer)
		{
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00025620 File Offset: 0x00023820
		protected override void ProcessAsSsl3()
		{
			ServerContext serverContext = (ServerContext)base.Context;
			AsymmetricAlgorithm asymmetricAlgorithm = serverContext.SslStream.RaisePrivateKeySelection(new X509Certificate(serverContext.ServerSettings.Certificates[0].RawData), null);
			if (asymmetricAlgorithm == null)
			{
				throw new TlsException(AlertDescription.UserCancelled, "Server certificate Private Key unavailable.");
			}
			byte[] rgbIn = base.ReadBytes((int)this.Length);
			RSAPKCS1KeyExchangeDeformatter rsapkcs1KeyExchangeDeformatter = new RSAPKCS1KeyExchangeDeformatter(asymmetricAlgorithm);
			byte[] preMasterSecret = rsapkcs1KeyExchangeDeformatter.DecryptKeyExchange(rgbIn);
			base.Context.Negotiating.Cipher.ComputeMasterSecret(preMasterSecret);
			base.Context.Negotiating.Cipher.ComputeKeys();
			base.Context.Negotiating.Cipher.InitializeCipher();
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000256D8 File Offset: 0x000238D8
		protected override void ProcessAsTls1()
		{
			ServerContext serverContext = (ServerContext)base.Context;
			AsymmetricAlgorithm asymmetricAlgorithm = serverContext.SslStream.RaisePrivateKeySelection(new X509Certificate(serverContext.ServerSettings.Certificates[0].RawData), null);
			if (asymmetricAlgorithm == null)
			{
				throw new TlsException(AlertDescription.UserCancelled, "Server certificate Private Key unavailable.");
			}
			byte[] rgbIn = base.ReadBytes((int)base.ReadInt16());
			RSAPKCS1KeyExchangeDeformatter rsapkcs1KeyExchangeDeformatter = new RSAPKCS1KeyExchangeDeformatter(asymmetricAlgorithm);
			byte[] preMasterSecret = rsapkcs1KeyExchangeDeformatter.DecryptKeyExchange(rgbIn);
			base.Context.Negotiating.Cipher.ComputeMasterSecret(preMasterSecret);
			base.Context.Negotiating.Cipher.ComputeKeys();
			base.Context.Negotiating.Cipher.InitializeCipher();
		}
	}
}
