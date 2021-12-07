using System;

namespace UnityEngine
{
	// Token: 0x02000279 RID: 633
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class CreateAssetMenuAttribute : Attribute
	{
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x00033704 File Offset: 0x00031904
		// (set) Token: 0x06002563 RID: 9571 RVA: 0x0003370C File Offset: 0x0003190C
		public string menuName { get; set; }

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x00033718 File Offset: 0x00031918
		// (set) Token: 0x06002565 RID: 9573 RVA: 0x00033720 File Offset: 0x00031920
		public string fileName { get; set; }

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x0003372C File Offset: 0x0003192C
		// (set) Token: 0x06002567 RID: 9575 RVA: 0x00033734 File Offset: 0x00031934
		public int order { get; set; }
	}
}
