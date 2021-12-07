using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000055 RID: 85
	public abstract class Aes : SymmetricAlgorithm
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x00015CD4 File Offset: 0x00013ED4
		protected Aes()
		{
			this.KeySizeValue = 256;
			this.BlockSizeValue = 128;
			this.FeedbackSizeValue = 128;
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(128, 256, 64);
			this.LegalBlockSizesValue = new KeySizes[1];
			this.LegalBlockSizesValue[0] = new KeySizes(128, 128, 0);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00015D54 File Offset: 0x00013F54
		public new static Aes Create()
		{
			return Aes.Create("System.Security.Cryptography.AesManaged, System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00015D60 File Offset: 0x00013F60
		public new static Aes Create(string algName)
		{
			return (Aes)CryptoConfig.CreateFromName(algName);
		}
	}
}
