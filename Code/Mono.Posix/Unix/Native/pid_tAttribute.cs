using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000066 RID: 102
	[AttributeUsage(AttributeTargets.Field)]
	internal class pid_tAttribute : MapAttribute
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0000D370 File Offset: 0x0000B570
		public pid_tAttribute() : base("pid_t")
		{
		}
	}
}
