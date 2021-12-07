using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000064 RID: 100
	[AttributeUsage(AttributeTargets.Field)]
	internal class nlink_tAttribute : MapAttribute
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x0000D350 File Offset: 0x0000B550
		public nlink_tAttribute() : base("nlink_t")
		{
		}
	}
}
