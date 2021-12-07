using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000066 RID: 102
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	[ExecuteInEditMode]
	[AddComponentMenu("UI/2D Rect Mask", 13)]
	public class RectMask2D : UIBehaviour, ICanvasRaycastFilter, IClipper
	{
		// Token: 0x0600034E RID: 846 RVA: 0x0001055C File Offset: 0x0000E75C
		protected RectMask2D()
		{
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00010588 File Offset: 0x0000E788
		public Rect canvasRect
		{
			get
			{
				Canvas c = null;
				List<Canvas> list = ListPool<Canvas>.Get();
				base.gameObject.GetComponentsInParent<Canvas>(false, list);
				if (list.Count > 0)
				{
					c = list[0];
				}
				ListPool<Canvas>.Release(list);
				return this.m_VertexClipper.GetCanvasRect(this.rectTransform, c);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000105D8 File Offset: 0x0000E7D8
		public RectTransform rectTransform
		{
			get
			{
				RectTransform result;
				if ((result = this.m_RectTransform) == null)
				{
					result = (this.m_RectTransform = base.GetComponent<RectTransform>());
				}
				return result;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00010604 File Offset: 0x0000E804
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_ShouldRecalculateClipRects = true;
			ClipperRegistry.Register(this);
			MaskUtilities.Notify2DMaskStateChanged(this);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00010620 File Offset: 0x0000E820
		protected override void OnDisable()
		{
			base.OnDisable();
			this.m_ClipTargets.Clear();
			this.m_Clippers.Clear();
			ClipperRegistry.Unregister(this);
			MaskUtilities.Notify2DMaskStateChanged(this);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00010658 File Offset: 0x0000E858
		public virtual bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return !base.isActiveAndEnabled || RectTransformUtility.RectangleContainsScreenPoint(this.rectTransform, sp, eventCamera);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00010680 File Offset: 0x0000E880
		public virtual void PerformClipping()
		{
			if (this.m_ShouldRecalculateClipRects)
			{
				MaskUtilities.GetRectMasksForClip(this, this.m_Clippers);
				this.m_ShouldRecalculateClipRects = false;
			}
			bool flag = true;
			Rect rect = Clipping.FindCullAndClipWorldRect(this.m_Clippers, out flag);
			if (rect != this.m_LastClipRectCanvasSpace)
			{
				for (int i = 0; i < this.m_ClipTargets.Count; i++)
				{
					this.m_ClipTargets[i].SetClipRect(rect, flag);
				}
				this.m_LastClipRectCanvasSpace = rect;
				this.m_LastClipRectValid = flag;
			}
			for (int j = 0; j < this.m_ClipTargets.Count; j++)
			{
				this.m_ClipTargets[j].Cull(this.m_LastClipRectCanvasSpace, this.m_LastClipRectValid);
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00010744 File Offset: 0x0000E944
		public void AddClippable(IClippable clippable)
		{
			if (clippable == null)
			{
				return;
			}
			if (!this.m_ClipTargets.Contains(clippable))
			{
				this.m_ClipTargets.Add(clippable);
			}
			clippable.SetClipRect(this.m_LastClipRectCanvasSpace, this.m_LastClipRectValid);
			clippable.Cull(this.m_LastClipRectCanvasSpace, this.m_LastClipRectValid);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001079C File Offset: 0x0000E99C
		public void RemoveClippable(IClippable clippable)
		{
			if (clippable == null)
			{
				return;
			}
			clippable.SetClipRect(default(Rect), false);
			this.m_ClipTargets.Remove(clippable);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000107D0 File Offset: 0x0000E9D0
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.m_ShouldRecalculateClipRects = true;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000107E0 File Offset: 0x0000E9E0
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			this.m_ShouldRecalculateClipRects = true;
		}

		// Token: 0x040001A6 RID: 422
		[NonSerialized]
		private readonly RectangularVertexClipper m_VertexClipper = new RectangularVertexClipper();

		// Token: 0x040001A7 RID: 423
		[NonSerialized]
		private RectTransform m_RectTransform;

		// Token: 0x040001A8 RID: 424
		[NonSerialized]
		private List<IClippable> m_ClipTargets = new List<IClippable>();

		// Token: 0x040001A9 RID: 425
		[NonSerialized]
		private bool m_ShouldRecalculateClipRects;

		// Token: 0x040001AA RID: 426
		[NonSerialized]
		private List<RectMask2D> m_Clippers = new List<RectMask2D>();

		// Token: 0x040001AB RID: 427
		[NonSerialized]
		private Rect m_LastClipRectCanvasSpace;

		// Token: 0x040001AC RID: 428
		[NonSerialized]
		private bool m_LastClipRectValid;
	}
}
