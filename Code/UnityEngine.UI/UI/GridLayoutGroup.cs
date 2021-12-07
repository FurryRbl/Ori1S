using System;

namespace UnityEngine.UI
{
	// Token: 0x0200008C RID: 140
	[AddComponentMenu("Layout/Grid Layout Group", 152)]
	public class GridLayoutGroup : LayoutGroup
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x00016868 File Offset: 0x00014A68
		protected GridLayoutGroup()
		{
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00016898 File Offset: 0x00014A98
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x000168A0 File Offset: 0x00014AA0
		public GridLayoutGroup.Corner startCorner
		{
			get
			{
				return this.m_StartCorner;
			}
			set
			{
				base.SetProperty<GridLayoutGroup.Corner>(ref this.m_StartCorner, value);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x000168B0 File Offset: 0x00014AB0
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x000168B8 File Offset: 0x00014AB8
		public GridLayoutGroup.Axis startAxis
		{
			get
			{
				return this.m_StartAxis;
			}
			set
			{
				base.SetProperty<GridLayoutGroup.Axis>(ref this.m_StartAxis, value);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000168C8 File Offset: 0x00014AC8
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x000168D0 File Offset: 0x00014AD0
		public Vector2 cellSize
		{
			get
			{
				return this.m_CellSize;
			}
			set
			{
				base.SetProperty<Vector2>(ref this.m_CellSize, value);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x000168E0 File Offset: 0x00014AE0
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x000168E8 File Offset: 0x00014AE8
		public Vector2 spacing
		{
			get
			{
				return this.m_Spacing;
			}
			set
			{
				base.SetProperty<Vector2>(ref this.m_Spacing, value);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x000168F8 File Offset: 0x00014AF8
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00016900 File Offset: 0x00014B00
		public GridLayoutGroup.Constraint constraint
		{
			get
			{
				return this.m_Constraint;
			}
			set
			{
				base.SetProperty<GridLayoutGroup.Constraint>(ref this.m_Constraint, value);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00016910 File Offset: 0x00014B10
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x00016918 File Offset: 0x00014B18
		public int constraintCount
		{
			get
			{
				return this.m_ConstraintCount;
			}
			set
			{
				base.SetProperty<int>(ref this.m_ConstraintCount, Mathf.Max(1, value));
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00016930 File Offset: 0x00014B30
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			int num2;
			int num;
			if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = (num2 = this.m_ConstraintCount);
			}
			else if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount)
			{
				num = (num2 = Mathf.CeilToInt((float)base.rectChildren.Count / (float)this.m_ConstraintCount - 0.001f));
			}
			else
			{
				num2 = 1;
				num = Mathf.CeilToInt(Mathf.Sqrt((float)base.rectChildren.Count));
			}
			base.SetLayoutInputForAxis((float)base.padding.horizontal + (this.cellSize.x + this.spacing.x) * (float)num2 - this.spacing.x, (float)base.padding.horizontal + (this.cellSize.x + this.spacing.x) * (float)num - this.spacing.x, -1f, 0);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00016A38 File Offset: 0x00014C38
		public override void CalculateLayoutInputVertical()
		{
			int num;
			if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = Mathf.CeilToInt((float)base.rectChildren.Count / (float)this.m_ConstraintCount - 0.001f);
			}
			else if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount)
			{
				num = this.m_ConstraintCount;
			}
			else
			{
				float x = base.rectTransform.rect.size.x;
				int num2 = Mathf.Max(1, Mathf.FloorToInt((x - (float)base.padding.horizontal + this.spacing.x + 0.001f) / (this.cellSize.x + this.spacing.x)));
				num = Mathf.CeilToInt((float)base.rectChildren.Count / (float)num2);
			}
			float num3 = (float)base.padding.vertical + (this.cellSize.y + this.spacing.y) * (float)num - this.spacing.y;
			base.SetLayoutInputForAxis(num3, num3, -1f, 1);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00016B64 File Offset: 0x00014D64
		public override void SetLayoutHorizontal()
		{
			this.SetCellsAlongAxis(0);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00016B70 File Offset: 0x00014D70
		public override void SetLayoutVertical()
		{
			this.SetCellsAlongAxis(1);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00016B7C File Offset: 0x00014D7C
		private void SetCellsAlongAxis(int axis)
		{
			if (axis == 0)
			{
				for (int i = 0; i < base.rectChildren.Count; i++)
				{
					RectTransform rectTransform = base.rectChildren[i];
					this.m_Tracker.Add(this, rectTransform, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
					rectTransform.anchorMin = Vector2.up;
					rectTransform.anchorMax = Vector2.up;
					rectTransform.sizeDelta = this.cellSize;
				}
				return;
			}
			float x = base.rectTransform.rect.size.x;
			float y = base.rectTransform.rect.size.y;
			int num;
			int num2;
			if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = this.m_ConstraintCount;
				num2 = Mathf.CeilToInt((float)base.rectChildren.Count / (float)num - 0.001f);
			}
			else if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount)
			{
				num2 = this.m_ConstraintCount;
				num = Mathf.CeilToInt((float)base.rectChildren.Count / (float)num2 - 0.001f);
			}
			else
			{
				if (this.cellSize.x + this.spacing.x <= 0f)
				{
					num = int.MaxValue;
				}
				else
				{
					num = Mathf.Max(1, Mathf.FloorToInt((x - (float)base.padding.horizontal + this.spacing.x + 0.001f) / (this.cellSize.x + this.spacing.x)));
				}
				if (this.cellSize.y + this.spacing.y <= 0f)
				{
					num2 = int.MaxValue;
				}
				else
				{
					num2 = Mathf.Max(1, Mathf.FloorToInt((y - (float)base.padding.vertical + this.spacing.y + 0.001f) / (this.cellSize.y + this.spacing.y)));
				}
			}
			int num3 = (int)(this.startCorner % GridLayoutGroup.Corner.LowerLeft);
			int num4 = (int)(this.startCorner / GridLayoutGroup.Corner.LowerLeft);
			int num5;
			int num6;
			int num7;
			if (this.startAxis == GridLayoutGroup.Axis.Horizontal)
			{
				num5 = num;
				num6 = Mathf.Clamp(num, 1, base.rectChildren.Count);
				num7 = Mathf.Clamp(num2, 1, Mathf.CeilToInt((float)base.rectChildren.Count / (float)num5));
			}
			else
			{
				num5 = num2;
				num7 = Mathf.Clamp(num2, 1, base.rectChildren.Count);
				num6 = Mathf.Clamp(num, 1, Mathf.CeilToInt((float)base.rectChildren.Count / (float)num5));
			}
			Vector2 vector = new Vector2((float)num6 * this.cellSize.x + (float)(num6 - 1) * this.spacing.x, (float)num7 * this.cellSize.y + (float)(num7 - 1) * this.spacing.y);
			Vector2 vector2 = new Vector2(base.GetStartOffset(0, vector.x), base.GetStartOffset(1, vector.y));
			for (int j = 0; j < base.rectChildren.Count; j++)
			{
				int num8;
				int num9;
				if (this.startAxis == GridLayoutGroup.Axis.Horizontal)
				{
					num8 = j % num5;
					num9 = j / num5;
				}
				else
				{
					num8 = j / num5;
					num9 = j % num5;
				}
				if (num3 == 1)
				{
					num8 = num6 - 1 - num8;
				}
				if (num4 == 1)
				{
					num9 = num7 - 1 - num9;
				}
				base.SetChildAlongAxis(base.rectChildren[j], 0, vector2.x + (this.cellSize[0] + this.spacing[0]) * (float)num8, this.cellSize[0]);
				base.SetChildAlongAxis(base.rectChildren[j], 1, vector2.y + (this.cellSize[1] + this.spacing[1]) * (float)num9, this.cellSize[1]);
			}
		}

		// Token: 0x0400026E RID: 622
		[SerializeField]
		protected GridLayoutGroup.Corner m_StartCorner;

		// Token: 0x0400026F RID: 623
		[SerializeField]
		protected GridLayoutGroup.Axis m_StartAxis;

		// Token: 0x04000270 RID: 624
		[SerializeField]
		protected Vector2 m_CellSize = new Vector2(100f, 100f);

		// Token: 0x04000271 RID: 625
		[SerializeField]
		protected Vector2 m_Spacing = Vector2.zero;

		// Token: 0x04000272 RID: 626
		[SerializeField]
		protected GridLayoutGroup.Constraint m_Constraint;

		// Token: 0x04000273 RID: 627
		[SerializeField]
		protected int m_ConstraintCount = 2;

		// Token: 0x0200008D RID: 141
		public enum Corner
		{
			// Token: 0x04000275 RID: 629
			UpperLeft,
			// Token: 0x04000276 RID: 630
			UpperRight,
			// Token: 0x04000277 RID: 631
			LowerLeft,
			// Token: 0x04000278 RID: 632
			LowerRight
		}

		// Token: 0x0200008E RID: 142
		public enum Axis
		{
			// Token: 0x0400027A RID: 634
			Horizontal,
			// Token: 0x0400027B RID: 635
			Vertical
		}

		// Token: 0x0200008F RID: 143
		public enum Constraint
		{
			// Token: 0x0400027D RID: 637
			Flexible,
			// Token: 0x0400027E RID: 638
			FixedColumnCount,
			// Token: 0x0400027F RID: 639
			FixedRowCount
		}
	}
}
