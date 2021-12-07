using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x020000BC RID: 188
	internal class TlsServerKeyExchange : HandshakeMessage
	{
		// Token: 0x060006C0 RID: 1728 RVA: 0x00025BF0 File Offset: 0x00023DF0
		public TlsServerKeyExchange(Context context) : base(context, HandshakeType.ServerKeyExchange)
		{
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00025BFC File Offset: 0x00023DFC
		public override void Update()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00025C04 File Offset: 0x00023E04
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00025C0C File Offset: 0x00023E0C
		protected override void ProcessAsTls1()
		{
			ServerContext serverContext = (ServerContext)base.Context;
			RSA rsa = (RSA)serverContext.SslStream.PrivateKeyCertSelectionDelegate(new X509Certificate(serverContext.ServerSettings.Certificates[0].RawData), null);
			RSAParameters rsaparameters = rsa.ExportParameters(false);
			base.WriteInt24(rsaparameters.Modulus.Length);
			this.Write(rsaparameters.Modulus, 0, rsaparameters.Modulus.Length);
			base.WriteInt24(rsaparameters.Exponent.Length);
			this.Write(rsaparameters.Exponent, 0, rsaparameters.Exponent.Length);
			byte[] array = this.createSignature(rsa, base.ToArray());
			base.WriteInt24(array.Length);
			base.Write(array);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00025CCC File Offset: 0x00023ECC
		private byte[] createSignature(RSA rsa, byte[] buffer)
		{
			MD5SHA1 md5SHA = new MD5SHA1();
			TlsStream tlsStream = new TlsStream();
			tlsStream.Write(base.Context.RandomCS);
			tlsStream.Write(buffer, 0, buffer.Length);
			md5SHA.ComputeHash(tlsStream.ToArray());
			tlsStream.Reset();
			return md5SHA.CreateSignature(rsa);
		}
	}
}
