using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000092 RID: 146
	internal class RSASslSignatureFormatter : AsymmetricSignatureFormatter
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x0001F69C File Offset: 0x0001D89C
		public RSASslSignatureFormatter()
		{
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001F6A4 File Offset: 0x0001D8A4
		public RSASslSignatureFormatter(AsymmetricAlgorithm key)
		{
			this.SetKey(key);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001F6B4 File Offset: 0x0001D8B4
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			if (this.key == null)
			{
				throw new CryptographicUnexpectedOperationException("The key is a null reference");
			}
			if (this.hash == null)
			{
				throw new CryptographicUnexpectedOperationException("The hash algorithm is a null reference.");
			}
			if (rgbHash == null)
			{
				throw new ArgumentNullException("The rgbHash parameter is a null reference.");
			}
			return PKCS1.Sign_v15(this.key, this.hash, rgbHash);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001F710 File Offset: 0x0001D910
		public override void SetHashAlgorithm(string strName)
		{
			if (strName != null)
			{
				if (RSASslSignatureFormatter.<>f__switch$map16 == null)
				{
					RSASslSignatureFormatter.<>f__switch$map16 = new Dictionary<string, int>(1)
					{
						{
							"MD5SHA1",
							0
						}
					};
				}
				int num;
				if (RSASslSignatureFormatter.<>f__switch$map16.TryGetValue(strName, out num))
				{
					if (num == 0)
					{
						this.hash = new MD5SHA1();
						return;
					}
				}
			}
			this.hash = HashAlgorithm.Create(strName);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001F788 File Offset: 0x0001D988
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (!(key is RSA))
			{
				throw new ArgumentException("Specfied key is not an RSA key");
			}
			this.key = (key as RSA);
		}

		// Token: 0x040002AE RID: 686
		private RSA key;

		// Token: 0x040002AF RID: 687
		private HashAlgorithm hash;
	}
}
