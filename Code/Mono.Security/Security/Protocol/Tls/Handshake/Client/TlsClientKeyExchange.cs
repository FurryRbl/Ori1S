using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x020000AB RID: 171
	internal class TlsClientKeyExchange : HandshakeMessage
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x00023E64 File Offset: 0x00022064
		public TlsClientKeyExchange(Context context) : base(context, HandshakeType.ClientKeyExchange)
		{
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00023E70 File Offset: 0x00022070
		protected override void ProcessAsSsl3()
		{
			this.ProcessCommon(false);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00023E7C File Offset: 0x0002207C
		protected override void ProcessAsTls1()
		{
			this.ProcessCommon(true);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00023E88 File Offset: 0x00022088
		public void ProcessCommon(bool sendLength)
		{
			byte[] array = base.Context.Negotiating.Cipher.CreatePremasterSecret();
			RSA rsa;
			if (base.Context.ServerSettings.ServerKeyExchange)
			{
				rsa = new RSAManaged();
				rsa.ImportParameters(base.Context.ServerSettings.RsaParameters);
			}
			else
			{
				rsa = base.Context.ServerSettings.CertificateRSA;
			}
			RSAPKCS1KeyExchangeFormatter rsapkcs1KeyExchangeFormatter = new RSAPKCS1KeyExchangeFormatter(rsa);
			byte[] array2 = rsapkcs1KeyExchangeFormatter.CreateKeyExchange(array);
			if (sendLength)
			{
				base.Write((short)array2.Length);
			}
			base.Write(array2);
			base.Context.Negotiating.Cipher.ComputeMasterSecret(array);
			base.Context.Negotiating.Cipher.ComputeKeys();
			rsa.Clear();
		}
	}
}
