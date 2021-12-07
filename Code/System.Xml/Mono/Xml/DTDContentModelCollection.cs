using System;
using System.Collections;

namespace Mono.Xml
{
	// Token: 0x020000C6 RID: 198
	internal class DTDContentModelCollection
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00026428 File Offset: 0x00024628
		public IList Items
		{
			get
			{
				return this.contentModel;
			}
		}

		// Token: 0x170001C1 RID: 449
		public DTDContentModel this[int i]
		{
			get
			{
				return this.contentModel[i] as DTDContentModel;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00026444 File Offset: 0x00024644
		public int Count
		{
			get
			{
				return this.contentModel.Count;
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00026454 File Offset: 0x00024654
		public void Add(DTDContentModel model)
		{
			this.contentModel.Add(model);
		}

		// Token: 0x040003FA RID: 1018
		private ArrayList contentModel = new ArrayList();
	}
}
