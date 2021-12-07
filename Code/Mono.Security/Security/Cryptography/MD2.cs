using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200002C RID: 44
	public abstract class MD2 : HashAlgorithm
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x0000C5A4 File Offset: 0x0000A7A4
		protected MD2()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
		public new static MD2 Create()
		{
			return MD2.Create("MD2");
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		public new static MD2 Create(string hashName)
		{
			object obj = CryptoConfig.CreateFromName(hashName);
			if (obj == null)
			{
				obj = new MD2Managed();
			}
			return (MD2)obj;
		}
	}
}
