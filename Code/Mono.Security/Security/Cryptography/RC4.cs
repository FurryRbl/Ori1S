using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000035 RID: 53
	public abstract class RC4 : SymmetricAlgorithm
	{
		// Token: 0x06000237 RID: 567 RVA: 0x0000E374 File Offset: 0x0000C574
		public RC4()
		{
			this.KeySizeValue = 128;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = RC4.s_legalBlockSizes;
			this.LegalKeySizesValue = RC4.s_legalKeySizes;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000E3EC File Offset: 0x0000C5EC
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		public override byte[] IV
		{
			get
			{
				return new byte[0];
			}
			set
			{
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000E3F8 File Offset: 0x0000C5F8
		public new static RC4 Create()
		{
			return RC4.Create("RC4");
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000E404 File Offset: 0x0000C604
		public new static RC4 Create(string algName)
		{
			object obj = CryptoConfig.CreateFromName(algName);
			if (obj == null)
			{
				obj = new ARC4Managed();
			}
			return (RC4)obj;
		}

		// Token: 0x040000F0 RID: 240
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x040000F1 RID: 241
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 2048, 8)
		};
	}
}
