using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200001E RID: 30
	public abstract class UIBehaviour : MonoBehaviour
	{
		// Token: 0x0600007B RID: 123 RVA: 0x000031E8 File Offset: 0x000013E8
		protected virtual void Awake()
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000031EC File Offset: 0x000013EC
		protected virtual void OnEnable()
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000031F0 File Offset: 0x000013F0
		protected virtual void Start()
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000031F4 File Offset: 0x000013F4
		protected virtual void OnDisable()
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000031F8 File Offset: 0x000013F8
		protected virtual void OnDestroy()
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000031FC File Offset: 0x000013FC
		public virtual bool IsActive()
		{
			return base.isActiveAndEnabled;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003204 File Offset: 0x00001404
		protected virtual void OnRectTransformDimensionsChange()
		{
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003208 File Offset: 0x00001408
		protected virtual void OnBeforeTransformParentChanged()
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000320C File Offset: 0x0000140C
		protected virtual void OnTransformParentChanged()
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003210 File Offset: 0x00001410
		protected virtual void OnDidApplyAnimationProperties()
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003214 File Offset: 0x00001414
		protected virtual void OnCanvasGroupChanged()
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003218 File Offset: 0x00001418
		protected virtual void OnCanvasHierarchyChanged()
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000321C File Offset: 0x0000141C
		public bool IsDestroyed()
		{
			return this == null;
		}
	}
}
