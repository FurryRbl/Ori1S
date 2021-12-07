using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000069 RID: 105
	[AttributeUsage(AttributeTargets.Field)]
	internal class time_tAttribute : MapAttribute
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		public time_tAttribute() : base("time_t")
		{
		}
	}
}
