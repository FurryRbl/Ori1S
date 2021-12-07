using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000068 RID: 104
	[AttributeUsage(AttributeTargets.Field)]
	internal class uid_tAttribute : MapAttribute
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x0000D390 File Offset: 0x0000B590
		public uid_tAttribute() : base("uid_t")
		{
		}
	}
}
