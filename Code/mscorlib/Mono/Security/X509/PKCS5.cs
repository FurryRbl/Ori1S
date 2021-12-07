using System;

namespace Mono.Security.X509
{
	// Token: 0x020000C1 RID: 193
	internal class PKCS5
	{
		// Token: 0x0400029E RID: 670
		public const string pbeWithMD2AndDESCBC = "1.2.840.113549.1.5.1";

		// Token: 0x0400029F RID: 671
		public const string pbeWithMD5AndDESCBC = "1.2.840.113549.1.5.3";

		// Token: 0x040002A0 RID: 672
		public const string pbeWithMD2AndRC2CBC = "1.2.840.113549.1.5.4";

		// Token: 0x040002A1 RID: 673
		public const string pbeWithMD5AndRC2CBC = "1.2.840.113549.1.5.6";

		// Token: 0x040002A2 RID: 674
		public const string pbeWithSHA1AndDESCBC = "1.2.840.113549.1.5.10";

		// Token: 0x040002A3 RID: 675
		public const string pbeWithSHA1AndRC2CBC = "1.2.840.113549.1.5.11";
	}
}
