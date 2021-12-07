using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x020000A7 RID: 167
	[AddComponentMenu("UI/Effects/Shadow", 14)]
	public class Shadow : BaseMeshEffect
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x00019090 File Offset: 0x00017290
		protected Shadow()
		{
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x000190E0 File Offset: 0x000172E0
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x000190E8 File Offset: 0x000172E8
		public Color effectColor
		{
			get
			{
				return this.m_EffectColor;
			}
			set
			{
				this.m_EffectColor = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00019110 File Offset: 0x00017310
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x00019118 File Offset: 0x00017318
		public Vector2 effectDistance
		{
			get
			{
				return this.m_EffectDistance;
			}
			set
			{
				if (value.x > 600f)
				{
					value.x = 600f;
				}
				if (value.x < -600f)
				{
					value.x = -600f;
				}
				if (value.y > 600f)
				{
					value.y = 600f;
				}
				if (value.y < -600f)
				{
					value.y = -600f;
				}
				if (this.m_EffectDistance == value)
				{
					return;
				}
				this.m_EffectDistance = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x000191D0 File Offset: 0x000173D0
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x000191D8 File Offset: 0x000173D8
		public bool useGraphicAlpha
		{
			get
			{
				return this.m_UseGraphicAlpha;
			}
			set
			{
				this.m_UseGraphicAlpha = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00019200 File Offset: 0x00017400
		protected void ApplyShadowZeroAlloc(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			int num = verts.Count * 2;
			if (verts.Capacity < num)
			{
				verts.Capacity = num;
			}
			for (int i = start; i < end; i++)
			{
				UIVertex uivertex = verts[i];
				verts.Add(uivertex);
				Vector3 position = uivertex.position;
				position.x += x;
				position.y += y;
				uivertex.position = position;
				Color32 color2 = color;
				if (this.m_UseGraphicAlpha)
				{
					color2.a = color2.a * verts[i].color.a / byte.MaxValue;
				}
				uivertex.color = color2;
				verts[i] = uivertex;
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000192C8 File Offset: 0x000174C8
		protected void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			int num = verts.Count * 2;
			if (verts.Capacity < num)
			{
				verts.Capacity = num;
			}
			this.ApplyShadowZeroAlloc(verts, color, start, end, x, y);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00019300 File Offset: 0x00017500
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> list = ListPool<UIVertex>.Get();
			vh.GetUIVertexStream(list);
			this.ApplyShadow(list, this.effectColor, 0, list.Count, this.effectDistance.x, this.effectDistance.y);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
			ListPool<UIVertex>.Release(list);
		}

		// Token: 0x040002B5 RID: 693
		private const float kMaxEffectDistance = 600f;

		// Token: 0x040002B6 RID: 694
		[SerializeField]
		private Color m_EffectColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x040002B7 RID: 695
		[SerializeField]
		private Vector2 m_EffectDistance = new Vector2(1f, -1f);

		// Token: 0x040002B8 RID: 696
		[SerializeField]
		private bool m_UseGraphicAlpha = true;
	}
}
