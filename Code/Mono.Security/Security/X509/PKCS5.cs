using System;

namespace Mono.Security.X509
{
	// Token: 0x0200003B RID: 59
	public class PKCS5
	{
		// Token: 0x04000112 RID: 274
		public const string pbeWithMD2AndDESCBC = "1.2.840.113549.1.5.1";

		// Token: 0x04000113 RID: 275
		public const string pbeWithMD5AndDESCBC = "1.2.840.113549.1.5.3";

		// Token: 0x04000114 RID: 276
		public const string pbeWithMD2AndRC2CBC = "1.2.840.113549.1.5.4";

		// Token: 0x04000115 RID: 277
		public const string pbeWithMD5AndRC2CBC = "1.2.840.113549.1.5.6";

		// Token: 0x04000116 RID: 278
		public const string pbeWithSHA1AndDESCBC = "1.2.840.113549.1.5.10";

		// Token: 0x04000117 RID: 279
		public const string pbeWithSHA1AndRC2CBC = "1.2.840.113549.1.5.11";
	}
}
