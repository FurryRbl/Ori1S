using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200008A RID: 138
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("Layout/Content Size Fitter", 141)]
	[ExecuteInEditMode]
	public class ContentSizeFitter : UIBehaviour, ILayoutController, ILayoutSelfController
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x000166EC File Offset: 0x000148EC
		protected ContentSizeFitter()
		{
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x000166F4 File Offset: 0x000148F4
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x000166FC File Offset: 0x000148FC
		public ContentSizeFitter.FitMode horizontalFit
		{
			get
			{
				return this.m_HorizontalFit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ContentSizeFitter.FitMode>(ref this.m_HorizontalFit, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00016718 File Offset: 0x00014918
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x00016720 File Offset: 0x00014920
		public ContentSizeFitter.FitMode verticalFit
		{
			get
			{
				return this.m_VerticalFit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ContentSizeFitter.FitMode>(ref this.m_VerticalFit, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0001673C File Offset: 0x0001493C
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

		// Token: 0x060004ED RID: 1261 RVA: 0x00016764 File Offset: 0x00014964
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00016774 File Offset: 0x00014974
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00016794 File Offset: 0x00014994
		protected override void OnRectTransformDimensionsChange()
		{
			this.SetDirty();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001679C File Offset: 0x0001499C
		private void HandleSelfFittingAlongAxis(int axis)
		{
			ContentSizeFitter.FitMode fitMode = (axis != 0) ? this.verticalFit : this.horizontalFit;
			if (fitMode == ContentSizeFitter.FitMode.Unconstrained)
			{
				return;
			}
			this.m_Tracker.Add(this, this.rectTransform, (axis != 0) ? DrivenTransformProperties.SizeDeltaY : DrivenTransformProperties.SizeDeltaX);
			if (fitMode == ContentSizeFitter.FitMode.MinSize)
			{
				this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetMinSize(this.m_Rect, axis));
			}
			else
			{
				this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetPreferredSize(this.m_Rect, axis));
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001682C File Offset: 0x00014A2C
		public virtual void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
			this.HandleSelfFittingAlongAxis(0);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00016840 File Offset: 0x00014A40
		public virtual void SetLayoutVertical()
		{
			this.HandleSelfFittingAlongAxis(1);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001684C File Offset: 0x00014A4C
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x04000266 RID: 614
		[SerializeField]
		protected ContentSizeFitter.FitMode m_HorizontalFit;

		// Token: 0x04000267 RID: 615
		[SerializeField]
		protected ContentSizeFitter.FitMode m_VerticalFit;

		// Token: 0x04000268 RID: 616
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x04000269 RID: 617
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x0200008B RID: 139
		public enum FitMode
		{
			// Token: 0x0400026B RID: 619
			Unconstrained,
			// Token: 0x0400026C RID: 620
			MinSize,
			// Token: 0x0400026D RID: 621
			PreferredSize
		}
	}
}
