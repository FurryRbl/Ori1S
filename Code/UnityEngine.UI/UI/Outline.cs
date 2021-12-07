using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x020000A5 RID: 165
	[AddComponentMenu("UI/Effects/Outline", 15)]
	public class Outline : Shadow
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x00018EC0 File Offset: 0x000170C0
		protected Outline()
		{
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00018EC8 File Offset: 0x000170C8
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> list = ListPool<UIVertex>.Get();
			vh.GetUIVertexStream(list);
			int num = list.Count * 5;
			if (list.Capacity < num)
			{
				list.Capacity = num;
			}
			int start = 0;
			int count = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, base.effectDistance.x, base.effectDistance.y);
			start = count;
			count = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, base.effectDistance.x, -base.effectDistance.y);
			start = count;
			count = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, -base.effectDistance.x, base.effectDistance.y);
			start = count;
			count = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, -base.effectDistance.x, -base.effectDistance.y);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
			ListPool<UIVertex>.Release(list);
		}
	}
}
