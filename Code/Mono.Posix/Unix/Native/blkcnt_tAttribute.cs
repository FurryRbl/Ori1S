using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200005D RID: 93
	[AttributeUsage(AttributeTargets.Field)]
	internal class blkcnt_tAttribute : MapAttribute
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
		public blkcnt_tAttribute() : base("blkcnt_t")
		{
		}
	}
}
