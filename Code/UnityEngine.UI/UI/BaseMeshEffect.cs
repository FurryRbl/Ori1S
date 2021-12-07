using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x020000A2 RID: 162
	[ExecuteInEditMode]
	public abstract class BaseMeshEffect : UIBehaviour, IMeshModifier
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00018DB8 File Offset: 0x00016FB8
		protected Graphic graphic
		{
			get
			{
				if (this.m_Graphic == null)
				{
					this.m_Graphic = base.GetComponent<Graphic>();
				}
				return this.m_Graphic;
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00018DE0 File Offset: 0x00016FE0
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00018E10 File Offset: 0x00017010
		protected override void OnDisable()
		{
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
			base.OnDisable();
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00018E40 File Offset: 0x00017040
		protected override void OnDidApplyAnimationProperties()
		{
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
			base.OnDidApplyAnimationProperties();
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00018E70 File Offset: 0x00017070
		public virtual void ModifyMesh(Mesh mesh)
		{
			using (VertexHelper vertexHelper = new VertexHelper(mesh))
			{
				this.ModifyMesh(vertexHelper);
				vertexHelper.FillMesh(mesh);
			}
		}

		// Token: 0x060005C3 RID: 1475
		public abstract void ModifyMesh(VertexHelper vh);

		// Token: 0x040002B4 RID: 692
		[NonSerialized]
		private Graphic m_Graphic;
	}
}
