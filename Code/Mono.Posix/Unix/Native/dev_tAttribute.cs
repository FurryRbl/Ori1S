using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200005F RID: 95
	[AttributeUsage(AttributeTargets.Field)]
	internal class dev_tAttribute : MapAttribute
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x0000D300 File Offset: 0x0000B500
		public dev_tAttribute() : base("dev_t")
		{
		}
	}
}
