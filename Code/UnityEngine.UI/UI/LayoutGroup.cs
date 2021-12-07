using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000098 RID: 152
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
	[ExecuteInEditMode]
	public abstract class LayoutGroup : UIBehaviour, ILayoutElement, ILayoutController, ILayoutGroup
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x000175F8 File Offset: 0x000157F8
		protected LayoutGroup()
		{
			if (this.m_Padding == null)
			{
				this.m_Padding = new RectOffset();
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00017658 File Offset: 0x00015858
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00017660 File Offset: 0x00015860
		public RectOffset padding
		{
			get
			{
				return this.m_Padding;
			}
			set
			{
				this.SetProperty<RectOffset>(ref this.m_Padding, value);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00017670 File Offset: 0x00015870
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x00017678 File Offset: 0x00015878
		public TextAnchor childAlignment
		{
			get
			{
				return this.m_ChildAlignment;
			}
			set
			{
				this.SetProperty<TextAnchor>(ref this.m_ChildAlignment, value);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00017688 File Offset: 0x00015888
		protected RectTransform rectTransform
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

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x000176B0 File Offset: 0x000158B0
		protected List<RectTransform> rectChildren
		{
			get
			{
				return this.m_RectChildren;
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000176B8 File Offset: 0x000158B8
		public virtual void CalculateLayoutInputHorizontal()
		{
			this.m_RectChildren.Clear();
			List<Component> list = ListPool<Component>.Get();
			for (int i = 0; i < this.rectTransform.childCount; i++)
			{
				RectTransform rectTransform = this.rectTransform.GetChild(i) as RectTransform;
				if (!(rectTransform == null) && rectTransform.gameObject.activeInHierarchy)
				{
					rectTransform.GetComponents(typeof(ILayoutIgnorer), list);
					if (list.Count == 0)
					{
						this.m_RectChildren.Add(rectTransform);
					}
					else
					{
						for (int j = 0; j < list.Count; j++)
						{
							ILayoutIgnorer layoutIgnorer = (ILayoutIgnorer)list[j];
							if (!layoutIgnorer.ignoreLayout)
							{
								this.m_RectChildren.Add(rectTransform);
								break;
							}
						}
					}
				}
			}
			ListPool<Component>.Release(list);
			this.m_Tracker.Clear();
		}

		// Token: 0x06000540 RID: 1344
		public abstract void CalculateLayoutInputVertical();

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x000177A8 File Offset: 0x000159A8
		public virtual float minWidth
		{
			get
			{
				return this.GetTotalMinSize(0);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x000177B4 File Offset: 0x000159B4
		public virtual float preferredWidth
		{
			get
			{
				return this.GetTotalPreferredSize(0);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x000177C0 File Offset: 0x000159C0
		public virtual float flexibleWidth
		{
			get
			{
				return this.GetTotalFlexibleSize(0);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x000177CC File Offset: 0x000159CC
		public virtual float minHeight
		{
			get
			{
				return this.GetTotalMinSize(1);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x000177D8 File Offset: 0x000159D8
		public virtual float preferredHeight
		{
			get
			{
				return this.GetTotalPreferredSize(1);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x000177E4 File Offset: 0x000159E4
		public virtual float flexibleHeight
		{
			get
			{
				return this.GetTotalFlexibleSize(1);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x000177F0 File Offset: 0x000159F0
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000548 RID: 1352
		public abstract void SetLayoutHorizontal();

		// Token: 0x06000549 RID: 1353
		public abstract void SetLayoutVertical();

		// Token: 0x0600054A RID: 1354 RVA: 0x000177F4 File Offset: 0x000159F4
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00017804 File Offset: 0x00015A04
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00017824 File Offset: 0x00015A24
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001782C File Offset: 0x00015A2C
		protected float GetTotalMinSize(int axis)
		{
			return this.m_TotalMinSize[axis];
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001783C File Offset: 0x00015A3C
		protected float GetTotalPreferredSize(int axis)
		{
			return this.m_TotalPreferredSize[axis];
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001784C File Offset: 0x00015A4C
		protected float GetTotalFlexibleSize(int axis)
		{
			return this.m_TotalFlexibleSize[axis];
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001785C File Offset: 0x00015A5C
		protected float GetStartOffset(int axis, float requiredSpaceWithoutPadding)
		{
			float num = requiredSpaceWithoutPadding + (float)((axis != 0) ? this.padding.vertical : this.padding.horizontal);
			float num2 = this.rectTransform.rect.size[axis];
			float num3 = num2 - num;
			float num4;
			if (axis == 0)
			{
				num4 = (float)(this.childAlignment % TextAnchor.MiddleLeft) * 0.5f;
			}
			else
			{
				num4 = (float)(this.childAlignment / TextAnchor.MiddleLeft) * 0.5f;
			}
			return (float)((axis != 0) ? this.padding.top : this.padding.left) + num3 * num4;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00017908 File Offset: 0x00015B08
		protected void SetLayoutInputForAxis(float totalMin, float totalPreferred, float totalFlexible, int axis)
		{
			this.m_TotalMinSize[axis] = totalMin;
			this.m_TotalPreferredSize[axis] = totalPreferred;
			this.m_TotalFlexibleSize[axis] = totalFlexible;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00017940 File Offset: 0x00015B40
		protected void SetChildAlongAxis(RectTransform rect, int axis, float pos, float size)
		{
			if (rect == null)
			{
				return;
			}
			this.m_Tracker.Add(this, rect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
			rect.SetInsetAndSizeFromParentEdge((axis != 0) ? RectTransform.Edge.Top : RectTransform.Edge.Left, pos, size);
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00017984 File Offset: 0x00015B84
		private bool isRootLayoutGroup
		{
			get
			{
				Transform parent = base.transform.parent;
				return parent == null || base.transform.parent.GetComponent(typeof(ILayoutGroup)) == null;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000179CC File Offset: 0x00015BCC
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (this.isRootLayoutGroup)
			{
				this.SetDirty();
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000179E8 File Offset: 0x00015BE8
		protected virtual void OnTransformChildrenChanged()
		{
			this.SetDirty();
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000179F0 File Offset: 0x00015BF0
		protected void SetProperty<T>(ref T currentValue, T newValue)
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return;
			}
			currentValue = newValue;
			this.SetDirty();
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00017A50 File Offset: 0x00015C50
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x0400028A RID: 650
		[SerializeField]
		protected RectOffset m_Padding = new RectOffset();

		// Token: 0x0400028B RID: 651
		[FormerlySerializedAs("m_Alignment")]
		[SerializeField]
		protected TextAnchor m_ChildAlignment;

		// Token: 0x0400028C RID: 652
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x0400028D RID: 653
		protected DrivenRectTransformTracker m_Tracker;

		// Token: 0x0400028E RID: 654
		private Vector2 m_TotalMinSize = Vector2.zero;

		// Token: 0x0400028F RID: 655
		private Vector2 m_TotalPreferredSize = Vector2.zero;

		// Token: 0x04000290 RID: 656
		private Vector2 m_TotalFlexibleSize = Vector2.zero;

		// Token: 0x04000291 RID: 657
		[NonSerialized]
		private List<RectTransform> m_RectChildren = new List<RectTransform>();
	}
}
