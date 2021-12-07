using System;

namespace System.Security.Cryptography
{
	// Token: 0x020005E5 RID: 1509
	internal class DSASignatureDescription : SignatureDescription
	{
		// Token: 0x06003994 RID: 14740 RVA: 0x000C59A0 File Offset: 0x000C3BA0
		public DSASignatureDescription()
		{
			base.DeformatterAlgorithm = "System.Security.Cryptography.DSASignatureDeformatter";
			base.DigestAlgorithm = "System.Security.Cryptography.SHA1CryptoServiceProvider";
			base.FormatterAlgorithm = "System.Security.Cryptography.DSASignatureFormatter";
			base.KeyAlgorithm = "System.Security.Cryptography.DSACryptoServiceProvider";
		}
	}
}
