using System;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200005E RID: 94
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Mask", 13)]
	[DisallowMultipleComponent]
	public class Mask : UIBehaviour, ICanvasRaycastFilter, IMaterialModifier
	{
		// Token: 0x06000314 RID: 788 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		protected Mask()
		{
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
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

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000F6FC File Offset: 0x0000D8FC
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000F704 File Offset: 0x0000D904
		public bool showMaskGraphic
		{
			get
			{
				return this.m_ShowMaskGraphic;
			}
			set
			{
				if (this.m_ShowMaskGraphic == value)
				{
					return;
				}
				this.m_ShowMaskGraphic = value;
				if (this.graphic != null)
				{
					this.graphic.SetMaterialDirty();
				}
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000F744 File Offset: 0x0000D944
		public Graphic graphic
		{
			get
			{
				Graphic result;
				if ((result = this.m_Graphic) == null)
				{
					result = (this.m_Graphic = base.GetComponent<Graphic>());
				}
				return result;
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000F770 File Offset: 0x0000D970
		[Obsolete("use Mask.enabled instead", true)]
		public virtual bool MaskEnabled()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000F778 File Offset: 0x0000D978
		[Obsolete("Not used anymore.")]
		public virtual void OnSiblingGraphicEnabledDisabled()
		{
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000F77C File Offset: 0x0000D97C
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.graphic != null)
			{
				this.graphic.canvasRenderer.hasPopInstruction = true;
				this.graphic.SetMaterialDirty();
			}
			MaskUtilities.NotifyStencilStateChanged(this);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.graphic != null)
			{
				this.graphic.SetMaterialDirty();
				this.graphic.canvasRenderer.hasPopInstruction = false;
				this.graphic.canvasRenderer.popMaterialCount = 0;
			}
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
			StencilMaterial.Remove(this.m_UnmaskMaterial);
			this.m_UnmaskMaterial = null;
			MaskUtilities.NotifyStencilStateChanged(this);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000F840 File Offset: 0x0000DA40
		public virtual bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return !base.isActiveAndEnabled || RectTransformUtility.RectangleContainsScreenPoint(this.rectTransform, sp, eventCamera);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000F868 File Offset: 0x0000DA68
		public virtual Material GetModifiedMaterial(Material baseMaterial)
		{
			if (this.graphic == null)
			{
				return baseMaterial;
			}
			Transform stopAfter = MaskUtilities.FindRootSortOverrideCanvas(base.transform);
			int stencilDepth = MaskUtilities.GetStencilDepth(base.transform, stopAfter);
			if (stencilDepth >= 8)
			{
				Debug.LogError("Attempting to use a stencil mask with depth > 8", base.gameObject);
				return baseMaterial;
			}
			int num = 1 << stencilDepth;
			if (num == 1)
			{
				Material maskMaterial = StencilMaterial.Add(baseMaterial, 1, StencilOp.Replace, CompareFunction.Always, (!this.m_ShowMaskGraphic) ? ((ColorWriteMask)0) : ColorWriteMask.All);
				StencilMaterial.Remove(this.m_MaskMaterial);
				this.m_MaskMaterial = maskMaterial;
				Material unmaskMaterial = StencilMaterial.Add(baseMaterial, 1, StencilOp.Zero, CompareFunction.Always, (ColorWriteMask)0);
				StencilMaterial.Remove(this.m_UnmaskMaterial);
				this.m_UnmaskMaterial = unmaskMaterial;
				this.graphic.canvasRenderer.popMaterialCount = 1;
				this.graphic.canvasRenderer.SetPopMaterial(this.m_UnmaskMaterial, 0);
				return this.m_MaskMaterial;
			}
			Material maskMaterial2 = StencilMaterial.Add(baseMaterial, num | num - 1, StencilOp.Replace, CompareFunction.Equal, (!this.m_ShowMaskGraphic) ? ((ColorWriteMask)0) : ColorWriteMask.All, num - 1, num | num - 1);
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = maskMaterial2;
			this.graphic.canvasRenderer.hasPopInstruction = true;
			Material unmaskMaterial2 = StencilMaterial.Add(baseMaterial, num - 1, StencilOp.Replace, CompareFunction.Equal, (ColorWriteMask)0, num - 1, num | num - 1);
			StencilMaterial.Remove(this.m_UnmaskMaterial);
			this.m_UnmaskMaterial = unmaskMaterial2;
			this.graphic.canvasRenderer.popMaterialCount = 1;
			this.graphic.canvasRenderer.SetPopMaterial(this.m_UnmaskMaterial, 0);
			return this.m_MaskMaterial;
		}

		// Token: 0x0400018B RID: 395
		[NonSerialized]
		private RectTransform m_RectTransform;

		// Token: 0x0400018C RID: 396
		[FormerlySerializedAs("m_ShowGraphic")]
		[SerializeField]
		private bool m_ShowMaskGraphic = true;

		// Token: 0x0400018D RID: 397
		[NonSerialized]
		private Graphic m_Graphic;

		// Token: 0x0400018E RID: 398
		[NonSerialized]
		private Material m_MaskMaterial;

		// Token: 0x0400018F RID: 399
		[NonSerialized]
		private Material m_UnmaskMaterial;
	}
}
