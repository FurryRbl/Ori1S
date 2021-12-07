using System;

namespace UnityEngine.UI
{
	// Token: 0x02000082 RID: 130
	public interface IClippable
	{
		// Token: 0x060004B3 RID: 1203
		void RecalculateClipping();

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004B4 RID: 1204
		RectTransform rectTransform { get; }

		// Token: 0x060004B5 RID: 1205
		void Cull(Rect clipRect, bool validRect);

		// Token: 0x060004B6 RID: 1206
		void SetClipRect(Rect value, bool validRect);
	}
}
