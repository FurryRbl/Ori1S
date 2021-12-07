using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200002E RID: 46
	public abstract class MD4 : HashAlgorithm
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x0000C8C8 File Offset: 0x0000AAC8
		protected MD4()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000C8DC File Offset: 0x0000AADC
		public new static MD4 Create()
		{
			return MD4.Create("MD4");
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		public new static MD4 Create(string hashName)
		{
			object obj = CryptoConfig.CreateFromName(hashName);
			if (obj == null)
			{
				obj = new MD4Managed();
			}
			return (MD4)obj;
		}
	}
}
