using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000067 RID: 103
	[AttributeUsage(AttributeTargets.Field)]
	internal class suseconds_tAttribute : MapAttribute
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x0000D380 File Offset: 0x0000B580
		public suseconds_tAttribute() : base("suseconds_t")
		{
		}
	}
}
