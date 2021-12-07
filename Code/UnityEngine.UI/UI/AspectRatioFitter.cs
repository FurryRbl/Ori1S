using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000084 RID: 132
	[RequireComponent(typeof(RectTransform))]
	[ExecuteInEditMode]
	[AddComponentMenu("Layout/Aspect Ratio Fitter", 142)]
	public class AspectRatioFitter : UIBehaviour, ILayoutController, ILayoutSelfController
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x00015EE8 File Offset: 0x000140E8
		protected AspectRatioFitter()
		{
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00015EFC File Offset: 0x000140FC
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x00015F04 File Offset: 0x00014104
		public AspectRatioFitter.AspectMode aspectMode
		{
			get
			{
				return this.m_AspectMode;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<AspectRatioFitter.AspectMode>(ref this.m_AspectMode, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00015F20 File Offset: 0x00014120
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x00015F28 File Offset: 0x00014128
		public float aspectRatio
		{
			get
			{
				return this.m_AspectRatio;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_AspectRatio, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00015F44 File Offset: 0x00014144
		private RectTransform rectTransform
		{
			get
			{
				if (this.m_Rect == null)
				{
					this.m_Rect = base.GetComponent<RectTransform>();
				}
				return this.m_Rect;
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00015F6C File Offset: 0x0001416C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00015F7C File Offset: 0x0001417C
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00015F9C File Offset: 0x0001419C
		protected override void OnRectTransformDimensionsChange()
		{
			this.UpdateRect();
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00015FA4 File Offset: 0x000141A4
		private void UpdateRect()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_Tracker.Clear();
			switch (this.m_AspectMode)
			{
			case AspectRatioFitter.AspectMode.WidthControlsHeight:
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.SizeDeltaY);
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.rectTransform.rect.width / this.m_AspectRatio);
				break;
			case AspectRatioFitter.AspectMode.HeightControlsWidth:
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.SizeDeltaX);
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.rectTransform.rect.height * this.m_AspectRatio);
				break;
			case AspectRatioFitter.AspectMode.FitInParent:
			case AspectRatioFitter.AspectMode.EnvelopeParent:
			{
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
				this.rectTransform.anchorMin = Vector2.zero;
				this.rectTransform.anchorMax = Vector2.one;
				this.rectTransform.anchoredPosition = Vector2.zero;
				Vector2 zero = Vector2.zero;
				Vector2 parentSize = this.GetParentSize();
				if (parentSize.y * this.aspectRatio < parentSize.x ^ this.m_AspectMode == AspectRatioFitter.AspectMode.FitInParent)
				{
					zero.y = this.GetSizeDeltaToProduceSize(parentSize.x / this.aspectRatio, 1);
				}
				else
				{
					zero.x = this.GetSizeDeltaToProduceSize(parentSize.y * this.aspectRatio, 0);
				}
				this.rectTransform.sizeDelta = zero;
				break;
			}
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001613C File Offset: 0x0001433C
		private float GetSizeDeltaToProduceSize(float size, int axis)
		{
			return size - this.GetParentSize()[axis] * (this.rectTransform.anchorMax[axis] - this.rectTransform.anchorMin[axis]);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00016184 File Offset: 0x00014384
		private Vector2 GetParentSize()
		{
			RectTransform rectTransform = this.rectTransform.parent as RectTransform;
			if (!rectTransform)
			{
				return Vector2.zero;
			}
			return rectTransform.rect.size;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000161C4 File Offset: 0x000143C4
		public virtual void SetLayoutHorizontal()
		{
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000161C8 File Offset: 0x000143C8
		public virtual void SetLayoutVertical()
		{
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000161CC File Offset: 0x000143CC
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateRect();
		}

		// Token: 0x04000240 RID: 576
		[SerializeField]
		private AspectRatioFitter.AspectMode m_AspectMode;

		// Token: 0x04000241 RID: 577
		[SerializeField]
		private float m_AspectRatio = 1f;

		// Token: 0x04000242 RID: 578
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x04000243 RID: 579
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x02000085 RID: 133
		public enum AspectMode
		{
			// Token: 0x04000245 RID: 581
			None,
			// Token: 0x04000246 RID: 582
			WidthControlsHeight,
			// Token: 0x04000247 RID: 583
			HeightControlsWidth,
			// Token: 0x04000248 RID: 584
			FitInParent,
			// Token: 0x04000249 RID: 585
			EnvelopeParent
		}
	}
}
