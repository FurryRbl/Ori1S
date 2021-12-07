using System;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public struct DHParameters
	{
		// Token: 0x040000B6 RID: 182
		public byte[] P;

		// Token: 0x040000B7 RID: 183
		public byte[] G;

		// Token: 0x040000B8 RID: 184
		[NonSerialized]
		public byte[] X;
	}
}
