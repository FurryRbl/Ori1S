using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000097 RID: 151
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("Layout/Layout Element", 140)]
	public class LayoutElement : UIBehaviour, ILayoutElement, ILayoutIgnorer
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x00017440 File Offset: 0x00015640
		protected LayoutElement()
		{
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00017498 File Offset: 0x00015698
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x000174A0 File Offset: 0x000156A0
		public virtual bool ignoreLayout
		{
			get
			{
				return this.m_IgnoreLayout;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_IgnoreLayout, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000174BC File Offset: 0x000156BC
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000174C0 File Offset: 0x000156C0
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x000174C4 File Offset: 0x000156C4
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x000174CC File Offset: 0x000156CC
		public virtual float minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x000174E8 File Offset: 0x000156E8
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x000174F0 File Offset: 0x000156F0
		public virtual float minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0001750C File Offset: 0x0001570C
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00017514 File Offset: 0x00015714
		public virtual float preferredWidth
		{
			get
			{
				return this.m_PreferredWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00017530 File Offset: 0x00015730
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00017538 File Offset: 0x00015738
		public virtual float preferredHeight
		{
			get
			{
				return this.m_PreferredHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00017554 File Offset: 0x00015754
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0001755C File Offset: 0x0001575C
		public virtual float flexibleWidth
		{
			get
			{
				return this.m_FlexibleWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FlexibleWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00017578 File Offset: 0x00015778
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x00017580 File Offset: 0x00015780
		public virtual float flexibleHeight
		{
			get
			{
				return this.m_FlexibleHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FlexibleHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001759C File Offset: 0x0001579C
		public virtual int layoutPriority
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000175A0 File Offset: 0x000157A0
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000175B0 File Offset: 0x000157B0
		protected override void OnTransformParentChanged()
		{
			this.SetDirty();
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000175B8 File Offset: 0x000157B8
		protected override void OnDisable()
		{
			this.SetDirty();
			base.OnDisable();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000175C8 File Offset: 0x000157C8
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x000175D0 File Offset: 0x000157D0
		protected override void OnBeforeTransformParentChanged()
		{
			this.SetDirty();
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000175D8 File Offset: 0x000157D8
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(base.transform as RectTransform);
		}

		// Token: 0x04000283 RID: 643
		[SerializeField]
		private bool m_IgnoreLayout;

		// Token: 0x04000284 RID: 644
		[SerializeField]
		private float m_MinWidth = -1f;

		// Token: 0x04000285 RID: 645
		[SerializeField]
		private float m_MinHeight = -1f;

		// Token: 0x04000286 RID: 646
		[SerializeField]
		private float m_PreferredWidth = -1f;

		// Token: 0x04000287 RID: 647
		[SerializeField]
		private float m_PreferredHeight = -1f;

		// Token: 0x04000288 RID: 648
		[SerializeField]
		private float m_FlexibleWidth = -1f;

		// Token: 0x04000289 RID: 649
		[SerializeField]
		private float m_FlexibleHeight = -1f;
	}
}
