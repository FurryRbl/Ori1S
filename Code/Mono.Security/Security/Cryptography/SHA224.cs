using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000038 RID: 56
	public abstract class SHA224 : HashAlgorithm
	{
		// Token: 0x06000253 RID: 595 RVA: 0x0000F220 File Offset: 0x0000D420
		public SHA224()
		{
			this.HashSizeValue = 224;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000F234 File Offset: 0x0000D434
		public new static SHA224 Create()
		{
			return SHA224.Create("SHA224");
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000F240 File Offset: 0x0000D440
		public new static SHA224 Create(string hashName)
		{
			object obj = CryptoConfig.CreateFromName(hashName);
			if (obj == null)
			{
				obj = new SHA224Managed();
			}
			return (SHA224)obj;
		}
	}
}
