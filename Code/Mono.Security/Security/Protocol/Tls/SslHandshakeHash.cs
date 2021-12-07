using System;
using System.Security.Cryptography;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200009B RID: 155
	internal class SslHandshakeHash : HashAlgorithm
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x000209C8 File Offset: 0x0001EBC8
		public SslHandshakeHash(byte[] secret)
		{
			this.md5 = HashAlgorithm.Create("MD5");
			this.sha = HashAlgorithm.Create("SHA1");
			this.HashSizeValue = this.md5.HashSize + this.sha.HashSize;
			this.secret = secret;
			this.Initialize();
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00020A28 File Offset: 0x0001EC28
		public override void Initialize()
		{
			this.md5.Initialize();
			this.sha.Initialize();
			this.initializePad();
			this.hashing = false;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00020A50 File Offset: 0x0001EC50
		protected override byte[] HashFinal()
		{
			if (!this.hashing)
			{
				this.hashing = true;
			}
			this.md5.TransformBlock(this.secret, 0, this.secret.Length, this.secret, 0);
			this.md5.TransformFinalBlock(this.innerPadMD5, 0, this.innerPadMD5.Length);
			byte[] hash = this.md5.Hash;
			this.md5.Initialize();
			this.md5.TransformBlock(this.secret, 0, this.secret.Length, this.secret, 0);
			this.md5.TransformBlock(this.outerPadMD5, 0, this.outerPadMD5.Length, this.outerPadMD5, 0);
			this.md5.TransformFinalBlock(hash, 0, hash.Length);
			this.sha.TransformBlock(this.secret, 0, this.secret.Length, this.secret, 0);
			this.sha.TransformFinalBlock(this.innerPadSHA, 0, this.innerPadSHA.Length);
			byte[] hash2 = this.sha.Hash;
			this.sha.Initialize();
			this.sha.TransformBlock(this.secret, 0, this.secret.Length, this.secret, 0);
			this.sha.TransformBlock(this.outerPadSHA, 0, this.outerPadSHA.Length, this.outerPadSHA, 0);
			this.sha.TransformFinalBlock(hash2, 0, hash2.Length);
			this.Initialize();
			byte[] array = new byte[36];
			Buffer.BlockCopy(this.md5.Hash, 0, array, 0, 16);
			Buffer.BlockCopy(this.sha.Hash, 0, array, 16, 20);
			return array;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00020BFC File Offset: 0x0001EDFC
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			if (!this.hashing)
			{
				this.hashing = true;
			}
			this.md5.TransformBlock(array, ibStart, cbSize, array, ibStart);
			this.sha.TransformBlock(array, ibStart, cbSize, array, ibStart);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00020C40 File Offset: 0x0001EE40
		public byte[] CreateSignature(RSA rsa)
		{
			if (rsa == null)
			{
				throw new CryptographicUnexpectedOperationException("missing key");
			}
			RSASslSignatureFormatter rsasslSignatureFormatter = new RSASslSignatureFormatter(rsa);
			rsasslSignatureFormatter.SetHashAlgorithm("MD5SHA1");
			return rsasslSignatureFormatter.CreateSignature(this.Hash);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00020C7C File Offset: 0x0001EE7C
		public bool VerifySignature(RSA rsa, byte[] rgbSignature)
		{
			if (rsa == null)
			{
				throw new CryptographicUnexpectedOperationException("missing key");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			RSASslSignatureDeformatter rsasslSignatureDeformatter = new RSASslSignatureDeformatter(rsa);
			rsasslSignatureDeformatter.SetHashAlgorithm("MD5SHA1");
			return rsasslSignatureDeformatter.VerifySignature(this.Hash, rgbSignature);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00020CCC File Offset: 0x0001EECC
		private void initializePad()
		{
			this.innerPadMD5 = new byte[48];
			this.outerPadMD5 = new byte[48];
			for (int i = 0; i < 48; i++)
			{
				this.innerPadMD5[i] = 54;
				this.outerPadMD5[i] = 92;
			}
			this.innerPadSHA = new byte[40];
			this.outerPadSHA = new byte[40];
			for (int j = 0; j < 40; j++)
			{
				this.innerPadSHA[j] = 54;
				this.outerPadSHA[j] = 92;
			}
		}

		// Token: 0x040002CA RID: 714
		private HashAlgorithm md5;

		// Token: 0x040002CB RID: 715
		private HashAlgorithm sha;

		// Token: 0x040002CC RID: 716
		private bool hashing;

		// Token: 0x040002CD RID: 717
		private byte[] secret;

		// Token: 0x040002CE RID: 718
		private byte[] innerPadMD5;

		// Token: 0x040002CF RID: 719
		private byte[] outerPadMD5;

		// Token: 0x040002D0 RID: 720
		private byte[] innerPadSHA;

		// Token: 0x040002D1 RID: 721
		private byte[] outerPadSHA;
	}
}
