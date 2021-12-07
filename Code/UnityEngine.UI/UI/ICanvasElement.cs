using System;

namespace UnityEngine.UI
{
	// Token: 0x0200003B RID: 59
	public interface ICanvasElement
	{
		// Token: 0x0600016C RID: 364
		void Rebuild(CanvasUpdate executing);

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600016D RID: 365
		Transform transform { get; }

		// Token: 0x0600016E RID: 366
		void LayoutComplete();

		// Token: 0x0600016F RID: 367
		void GraphicUpdateComplete();

		// Token: 0x06000170 RID: 368
		bool IsDestroyed();
	}
}
