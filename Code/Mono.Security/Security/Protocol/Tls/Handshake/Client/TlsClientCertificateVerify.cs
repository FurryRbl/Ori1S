using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x020000A8 RID: 168
	internal class TlsClientCertificateVerify : HandshakeMessage
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x0002388C File Offset: 0x00021A8C
		public TlsClientCertificateVerify(Context context) : base(context, HandshakeType.CertificateVerify)
		{
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00023898 File Offset: 0x00021A98
		public override void Update()
		{
			base.Update();
			base.Reset();
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000238A8 File Offset: 0x00021AA8
		protected override void ProcessAsSsl3()
		{
			AsymmetricAlgorithm asymmetricAlgorithm = null;
			ClientContext clientContext = (ClientContext)base.Context;
			asymmetricAlgorithm = clientContext.SslStream.RaisePrivateKeySelection(clientContext.ClientSettings.ClientCertificate, clientContext.ClientSettings.TargetHost);
			if (asymmetricAlgorithm == null)
			{
				throw new TlsException(AlertDescription.UserCancelled, "Client certificate Private Key unavailable.");
			}
			SslHandshakeHash sslHandshakeHash = new SslHandshakeHash(clientContext.MasterSecret);
			sslHandshakeHash.TransformFinalBlock(clientContext.HandshakeMessages.ToArray(), 0, (int)clientContext.HandshakeMessages.Length);
			byte[] array = null;
			if (!(asymmetricAlgorithm is RSACryptoServiceProvider))
			{
				try
				{
					array = sslHandshakeHash.CreateSignature((RSA)asymmetricAlgorithm);
				}
				catch (NotImplementedException)
				{
				}
			}
			if (array == null)
			{
				RSA clientCertRSA = this.getClientCertRSA((RSA)asymmetricAlgorithm);
				array = sslHandshakeHash.CreateSignature(clientCertRSA);
			}
			base.Write((short)array.Length);
			this.Write(array, 0, array.Length);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00023998 File Offset: 0x00021B98
		protected override void ProcessAsTls1()
		{
			AsymmetricAlgorithm asymmetricAlgorithm = null;
			ClientContext clientContext = (ClientContext)base.Context;
			asymmetricAlgorithm = clientContext.SslStream.RaisePrivateKeySelection(clientContext.ClientSettings.ClientCertificate, clientContext.ClientSettings.TargetHost);
			if (asymmetricAlgorithm == null)
			{
				throw new TlsException(AlertDescription.UserCancelled, "Client certificate Private Key unavailable.");
			}
			MD5SHA1 md5SHA = new MD5SHA1();
			md5SHA.ComputeHash(clientContext.HandshakeMessages.ToArray(), 0, (int)clientContext.HandshakeMessages.Length);
			byte[] array = null;
			if (!(asymmetricAlgorithm is RSACryptoServiceProvider))
			{
				try
				{
					array = md5SHA.CreateSignature((RSA)asymmetricAlgorithm);
				}
				catch (NotImplementedException)
				{
				}
			}
			if (array == null)
			{
				RSA clientCertRSA = this.getClientCertRSA((RSA)asymmetricAlgorithm);
				array = md5SHA.CreateSignature(clientCertRSA);
			}
			base.Write((short)array.Length);
			this.Write(array, 0, array.Length);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00023A80 File Offset: 0x00021C80
		private RSA getClientCertRSA(RSA privKey)
		{
			RSAParameters parameters = default(RSAParameters);
			RSAParameters rsaparameters = privKey.ExportParameters(true);
			ASN1 asn = new ASN1(base.Context.ClientSettings.Certificates[0].GetPublicKey());
			ASN1 asn2 = asn[0];
			if (asn2 == null || asn2.Tag != 2)
			{
				return null;
			}
			ASN1 asn3 = asn[1];
			if (asn3.Tag != 2)
			{
				return null;
			}
			parameters.Modulus = this.getUnsignedBigInteger(asn2.Value);
			parameters.Exponent = asn3.Value;
			parameters.D = rsaparameters.D;
			parameters.DP = rsaparameters.DP;
			parameters.DQ = rsaparameters.DQ;
			parameters.InverseQ = rsaparameters.InverseQ;
			parameters.P = rsaparameters.P;
			parameters.Q = rsaparameters.Q;
			int keySize = parameters.Modulus.Length << 3;
			RSAManaged rsamanaged = new RSAManaged(keySize);
			rsamanaged.ImportParameters(parameters);
			return rsamanaged;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00023B88 File Offset: 0x00021D88
		private byte[] getUnsignedBigInteger(byte[] integer)
		{
			if (integer[0] == 0)
			{
				int num = integer.Length - 1;
				byte[] array = new byte[num];
				Buffer.BlockCopy(integer, 1, array, 0, num);
				return array;
			}
			return integer;
		}
	}
}
