using System;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200007F RID: 127
	[Serializable]
	public enum CipherAlgorithmType
	{
		// Token: 0x04000235 RID: 565
		Des,
		// Token: 0x04000236 RID: 566
		None,
		// Token: 0x04000237 RID: 567
		Rc2,
		// Token: 0x04000238 RID: 568
		Rc4,
		// Token: 0x04000239 RID: 569
		Rijndael,
		// Token: 0x0400023A RID: 570
		SkipJack,
		// Token: 0x0400023B RID: 571
		TripleDes
	}
}
