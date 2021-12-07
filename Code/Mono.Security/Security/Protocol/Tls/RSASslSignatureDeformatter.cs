using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000091 RID: 145
	internal class RSASslSignatureDeformatter : AsymmetricSignatureDeformatter
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x0001F57C File Offset: 0x0001D77C
		public RSASslSignatureDeformatter()
		{
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001F584 File Offset: 0x0001D784
		public RSASslSignatureDeformatter(AsymmetricAlgorithm key)
		{
			this.SetKey(key);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001F594 File Offset: 0x0001D794
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
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
			return PKCS1.Verify_v15(this.key, this.hash, rgbHash, rgbSignature);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001F5F4 File Offset: 0x0001D7F4
		public override void SetHashAlgorithm(string strName)
		{
			if (strName != null)
			{
				if (RSASslSignatureDeformatter.<>f__switch$map15 == null)
				{
					RSASslSignatureDeformatter.<>f__switch$map15 = new Dictionary<string, int>(1)
					{
						{
							"MD5SHA1",
							0
						}
					};
				}
				int num;
				if (RSASslSignatureDeformatter.<>f__switch$map15.TryGetValue(strName, out num))
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

		// Token: 0x06000560 RID: 1376 RVA: 0x0001F66C File Offset: 0x0001D86C
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (!(key is RSA))
			{
				throw new ArgumentException("Specfied key is not an RSA key");
			}
			this.key = (key as RSA);
		}

		// Token: 0x040002AB RID: 683
		private RSA key;

		// Token: 0x040002AC RID: 684
		private HashAlgorithm hash;
	}
}
