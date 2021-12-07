using System;

namespace UnityEngine.UI
{
	// Token: 0x02000092 RID: 146
	public interface ILayoutElement
	{
		// Token: 0x06000514 RID: 1300
		void CalculateLayoutInputHorizontal();

		// Token: 0x06000515 RID: 1301
		void CalculateLayoutInputVertical();

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000516 RID: 1302
		float minWidth { get; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000517 RID: 1303
		float preferredWidth { get; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000518 RID: 1304
		float flexibleWidth { get; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000519 RID: 1305
		float minHeight { get; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600051A RID: 1306
		float preferredHeight { get; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600051B RID: 1307
		float flexibleHeight { get; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600051C RID: 1308
		int layoutPriority { get; }
	}
}
