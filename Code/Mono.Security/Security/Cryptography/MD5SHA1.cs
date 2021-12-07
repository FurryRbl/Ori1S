using System;
using System.Security.Cryptography;
using Mono.Security.Protocol.Tls;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000075 RID: 117
	internal class MD5SHA1 : HashAlgorithm
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x0001B0B0 File Offset: 0x000192B0
		public MD5SHA1()
		{
			this.md5 = MD5.Create();
			this.sha = SHA1.Create();
			this.HashSizeValue = this.md5.HashSize + this.sha.HashSize;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001B0F8 File Offset: 0x000192F8
		public override void Initialize()
		{
			this.md5.Initialize();
			this.sha.Initialize();
			this.hashing = false;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001B118 File Offset: 0x00019318
		protected override byte[] HashFinal()
		{
			if (!this.hashing)
			{
				this.hashing = true;
			}
			this.md5.TransformFinalBlock(new byte[0], 0, 0);
			this.sha.TransformFinalBlock(new byte[0], 0, 0);
			byte[] array = new byte[36];
			Buffer.BlockCopy(this.md5.Hash, 0, array, 0, 16);
			Buffer.BlockCopy(this.sha.Hash, 0, array, 16, 20);
			return array;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001B194 File Offset: 0x00019394
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			if (!this.hashing)
			{
				this.hashing = true;
			}
			this.md5.TransformBlock(array, ibStart, cbSize, array, ibStart);
			this.sha.TransformBlock(array, ibStart, cbSize, array, ibStart);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001B1D8 File Offset: 0x000193D8
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

		// Token: 0x06000436 RID: 1078 RVA: 0x0001B214 File Offset: 0x00019414
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

		// Token: 0x040001F5 RID: 501
		private HashAlgorithm md5;

		// Token: 0x040001F6 RID: 502
		private HashAlgorithm sha;

		// Token: 0x040001F7 RID: 503
		private bool hashing;
	}
}
