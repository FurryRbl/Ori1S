using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000479 RID: 1145
	internal struct Mark
	{
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x00085878 File Offset: 0x00083A78
		public bool IsDefined
		{
			get
			{
				return this.Start >= 0 && this.End >= 0;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060028ED RID: 10477 RVA: 0x00085898 File Offset: 0x00083A98
		public int Index
		{
			get
			{
				return (this.Start >= this.End) ? this.End : this.Start;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x000858C8 File Offset: 0x00083AC8
		public int Length
		{
			get
			{
				return (this.Start >= this.End) ? (this.Start - this.End) : (this.End - this.Start);
			}
		}

		// Token: 0x040019C8 RID: 6600
		public int Start;

		// Token: 0x040019C9 RID: 6601
		public int End;

		// Token: 0x040019CA RID: 6602
		public int Previous;
	}
}
