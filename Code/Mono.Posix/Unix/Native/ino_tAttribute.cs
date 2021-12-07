using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000063 RID: 99
	[AttributeUsage(AttributeTargets.Field)]
	internal class ino_tAttribute : MapAttribute
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x0000D340 File Offset: 0x0000B540
		public ino_tAttribute() : base("ino_t")
		{
		}
	}
}
