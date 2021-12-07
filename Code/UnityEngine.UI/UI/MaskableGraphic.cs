using System;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace UnityEngine.UI
{
	// Token: 0x0200005F RID: 95
	public abstract class MaskableGraphic : Graphic, IMaskable, IClippable, IMaterialModifier
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000FA2C File Offset: 0x0000DC2C
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000FA34 File Offset: 0x0000DC34
		public MaskableGraphic.CullStateChangedEvent onCullStateChanged
		{
			get
			{
				return this.m_OnCullStateChanged;
			}
			set
			{
				this.m_OnCullStateChanged = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000FA40 File Offset: 0x0000DC40
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000FA48 File Offset: 0x0000DC48
		public bool maskable
		{
			get
			{
				return this.m_Maskable;
			}
			set
			{
				if (value == this.m_Maskable)
				{
					return;
				}
				this.m_Maskable = value;
				this.m_ShouldRecalculateStencil = true;
				this.SetMaterialDirty();
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000FA6C File Offset: 0x0000DC6C
		public virtual Material GetModifiedMaterial(Material baseMaterial)
		{
			Material material = baseMaterial;
			if (this.m_ShouldRecalculateStencil)
			{
				Transform stopAfter = MaskUtilities.FindRootSortOverrideCanvas(base.transform);
				this.m_StencilValue = ((!this.maskable) ? 0 : MaskUtilities.GetStencilDepth(base.transform, stopAfter));
				this.m_ShouldRecalculateStencil = false;
			}
			if (this.m_StencilValue > 0 && base.GetComponent<Mask>() == null)
			{
				Material maskMaterial = StencilMaterial.Add(material, (1 << this.m_StencilValue) - 1, StencilOp.Keep, CompareFunction.Equal, ColorWriteMask.All, (1 << this.m_StencilValue) - 1, 0);
				StencilMaterial.Remove(this.m_MaskMaterial);
				this.m_MaskMaterial = maskMaterial;
				material = this.m_MaskMaterial;
			}
			return material;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000FB1C File Offset: 0x0000DD1C
		public virtual void Cull(Rect clipRect, bool validRect)
		{
			if (!base.canvasRenderer.hasMoved)
			{
				return;
			}
			bool flag = !validRect || !clipRect.Overlaps(this.canvasRect, true);
			bool flag2 = base.canvasRenderer.cull != flag;
			base.canvasRenderer.cull = flag;
			if (flag2)
			{
				this.m_OnCullStateChanged.Invoke(flag);
				this.SetVerticesDirty();
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000FB8C File Offset: 0x0000DD8C
		public virtual void SetClipRect(Rect clipRect, bool validRect)
		{
			if (validRect)
			{
				base.canvasRenderer.EnableRectClipping(clipRect);
			}
			else
			{
				base.canvasRenderer.DisableRectClipping();
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000FBBC File Offset: 0x0000DDBC
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_ShouldRecalculateStencil = true;
			this.UpdateClipParent();
			this.SetMaterialDirty();
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000FBD8 File Offset: 0x0000DDD8
		protected override void OnDisable()
		{
			base.OnDisable();
			this.m_ShouldRecalculateStencil = true;
			this.SetMaterialDirty();
			this.UpdateClipParent();
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000FC10 File Offset: 0x0000DE10
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.m_ShouldRecalculateStencil = true;
			this.UpdateClipParent();
			this.SetMaterialDirty();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		[Obsolete("Not used anymore.", true)]
		public virtual void ParentMaskStateChanged()
		{
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000FC30 File Offset: 0x0000DE30
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			this.m_ShouldRecalculateStencil = true;
			this.UpdateClipParent();
			this.SetMaterialDirty();
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000FC4C File Offset: 0x0000DE4C
		private Rect canvasRect
		{
			get
			{
				base.rectTransform.GetWorldCorners(this.m_Corners);
				if (base.canvas)
				{
					for (int i = 0; i < 4; i++)
					{
						this.m_Corners[i] = base.canvas.transform.InverseTransformPoint(this.m_Corners[i]);
					}
				}
				return new Rect(this.m_Corners[0].x, this.m_Corners[0].y, this.m_Corners[2].x - this.m_Corners[0].x, this.m_Corners[2].y - this.m_Corners[0].y);
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000FD2C File Offset: 0x0000DF2C
		private void UpdateClipParent()
		{
			RectMask2D rectMask2D = (!this.maskable || !this.IsActive()) ? null : MaskUtilities.GetRectMaskForClippable(this);
			if (rectMask2D != this.m_ParentMask && this.m_ParentMask != null)
			{
				this.m_ParentMask.RemoveClippable(this);
			}
			if (rectMask2D != null)
			{
				rectMask2D.AddClippable(this);
			}
			this.m_ParentMask = rectMask2D;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000FDA4 File Offset: 0x0000DFA4
		public virtual void RecalculateClipping()
		{
			this.UpdateClipParent();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		public virtual void RecalculateMasking()
		{
			this.m_ShouldRecalculateStencil = true;
			this.SetMaterialDirty();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		virtual RectTransform get_rectTransform()
		{
			return base.rectTransform;
		}

		// Token: 0x04000190 RID: 400
		[NonSerialized]
		protected bool m_ShouldRecalculateStencil = true;

		// Token: 0x04000191 RID: 401
		[NonSerialized]
		protected Material m_MaskMaterial;

		// Token: 0x04000192 RID: 402
		[NonSerialized]
		private RectMask2D m_ParentMask;

		// Token: 0x04000193 RID: 403
		[NonSerialized]
		private bool m_Maskable = true;

		// Token: 0x04000194 RID: 404
		[Obsolete("Not used anymore.", true)]
		[NonSerialized]
		protected bool m_IncludeForMasking;

		// Token: 0x04000195 RID: 405
		[SerializeField]
		private MaskableGraphic.CullStateChangedEvent m_OnCullStateChanged = new MaskableGraphic.CullStateChangedEvent();

		// Token: 0x04000196 RID: 406
		[Obsolete("Not used anymore", true)]
		[NonSerialized]
		protected bool m_ShouldRecalculate = true;

		// Token: 0x04000197 RID: 407
		[NonSerialized]
		protected int m_StencilValue;

		// Token: 0x04000198 RID: 408
		private readonly Vector3[] m_Corners = new Vector3[4];

		// Token: 0x02000060 RID: 96
		[Serializable]
		public class CullStateChangedEvent : UnityEvent<bool>
		{
		}
	}
}
