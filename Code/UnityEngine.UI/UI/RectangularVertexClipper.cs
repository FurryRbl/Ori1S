using System;

namespace UnityEngine.UI
{
	// Token: 0x02000083 RID: 131
	internal class RectangularVertexClipper
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x00015E20 File Offset: 0x00014020
		public Rect GetCanvasRect(RectTransform t, Canvas c)
		{
			t.GetWorldCorners(this.m_WorldCorners);
			Transform component = c.GetComponent<Transform>();
			for (int i = 0; i < 4; i++)
			{
				this.m_CanvasCorners[i] = component.InverseTransformPoint(this.m_WorldCorners[i]);
			}
			return new Rect(this.m_CanvasCorners[0].x, this.m_CanvasCorners[0].y, this.m_CanvasCorners[2].x - this.m_CanvasCorners[0].x, this.m_CanvasCorners[2].y - this.m_CanvasCorners[0].y);
		}

		// Token: 0x0400023E RID: 574
		private readonly Vector3[] m_WorldCorners = new Vector3[4];

		// Token: 0x0400023F RID: 575
		private readonly Vector3[] m_CanvasCorners = new Vector3[4];
	}
}
