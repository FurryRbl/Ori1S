using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI
{
	// Token: 0x02000099 RID: 153
	public class LayoutRebuilder : ICanvasElement
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x00017A74 File Offset: 0x00015C74
		static LayoutRebuilder()
		{
			RectTransform.reapplyDrivenProperties += LayoutRebuilder.ReapplyDrivenProperties;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00017AB0 File Offset: 0x00015CB0
		private void Initialize(RectTransform controller)
		{
			this.m_ToRebuild = controller;
			this.m_CachedHashFromTransform = controller.GetHashCode();
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00017AC8 File Offset: 0x00015CC8
		private void Clear()
		{
			this.m_ToRebuild = null;
			this.m_CachedHashFromTransform = 0;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00017AD8 File Offset: 0x00015CD8
		private static void ReapplyDrivenProperties(RectTransform driven)
		{
			LayoutRebuilder.MarkLayoutForRebuild(driven);
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00017AE0 File Offset: 0x00015CE0
		public Transform transform
		{
			get
			{
				return this.m_ToRebuild;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00017AE8 File Offset: 0x00015CE8
		public bool IsDestroyed()
		{
			return this.m_ToRebuild == null;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00017AF8 File Offset: 0x00015CF8
		private static void StripDisabledBehavioursFromList(List<Component> components)
		{
			components.RemoveAll((Component e) => e is Behaviour && !((Behaviour)e).isActiveAndEnabled);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00017B2C File Offset: 0x00015D2C
		public static void ForceRebuildLayoutImmediate(RectTransform layoutRoot)
		{
			LayoutRebuilder layoutRebuilder = LayoutRebuilder.s_Rebuilders.Get();
			layoutRebuilder.Initialize(layoutRoot);
			layoutRebuilder.Rebuild(CanvasUpdate.Layout);
			LayoutRebuilder.s_Rebuilders.Release(layoutRebuilder);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00017B60 File Offset: 0x00015D60
		public void Rebuild(CanvasUpdate executing)
		{
			if (executing == CanvasUpdate.Layout)
			{
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputHorizontal();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutHorizontal();
				});
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputVertical();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutVertical();
				});
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00017C24 File Offset: 0x00015E24
		private void PerformLayoutControl(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = ListPool<Component>.Get();
			rect.GetComponents(typeof(ILayoutController), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] is ILayoutSelfController)
					{
						action(list[i]);
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					if (!(list[j] is ILayoutSelfController))
					{
						action(list[j]);
					}
				}
				for (int k = 0; k < rect.childCount; k++)
				{
					this.PerformLayoutControl(rect.GetChild(k) as RectTransform, action);
				}
			}
			ListPool<Component>.Release(list);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00017D04 File Offset: 0x00015F04
		private void PerformLayoutCalculation(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = ListPool<Component>.Get();
			rect.GetComponents(typeof(ILayoutElement), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			if (list.Count > 0)
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					this.PerformLayoutCalculation(rect.GetChild(i) as RectTransform, action);
				}
				for (int j = 0; j < list.Count; j++)
				{
					action(list[j]);
				}
			}
			ListPool<Component>.Release(list);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00017D9C File Offset: 0x00015F9C
		public static void MarkLayoutForRebuild(RectTransform rect)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = ListPool<Component>.Get();
			RectTransform rectTransform = rect;
			for (;;)
			{
				RectTransform rectTransform2 = rectTransform.parent as RectTransform;
				if (!LayoutRebuilder.ValidLayoutGroup(rectTransform2, list))
				{
					break;
				}
				rectTransform = rectTransform2;
			}
			if (rectTransform == rect && !LayoutRebuilder.ValidController(rectTransform, list))
			{
				ListPool<Component>.Release(list);
				return;
			}
			LayoutRebuilder.MarkLayoutRootForRebuild(rectTransform);
			ListPool<Component>.Release(list);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00017E10 File Offset: 0x00016010
		private static bool ValidLayoutGroup(RectTransform parent, List<Component> comps)
		{
			if (parent == null)
			{
				return false;
			}
			parent.GetComponents(typeof(ILayoutGroup), comps);
			LayoutRebuilder.StripDisabledBehavioursFromList(comps);
			return comps.Count > 0;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00017E50 File Offset: 0x00016050
		private static bool ValidController(RectTransform layoutRoot, List<Component> comps)
		{
			if (layoutRoot == null)
			{
				return false;
			}
			layoutRoot.GetComponents(typeof(ILayoutController), comps);
			LayoutRebuilder.StripDisabledBehavioursFromList(comps);
			return comps.Count > 0;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00017E90 File Offset: 0x00016090
		private static void MarkLayoutRootForRebuild(RectTransform controller)
		{
			if (controller == null)
			{
				return;
			}
			LayoutRebuilder layoutRebuilder = LayoutRebuilder.s_Rebuilders.Get();
			layoutRebuilder.Initialize(controller);
			if (!CanvasUpdateRegistry.TryRegisterCanvasElementForLayoutRebuild(layoutRebuilder))
			{
				LayoutRebuilder.s_Rebuilders.Release(layoutRebuilder);
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00017ED4 File Offset: 0x000160D4
		public void LayoutComplete()
		{
			LayoutRebuilder.s_Rebuilders.Release(this);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00017EE4 File Offset: 0x000160E4
		public void GraphicUpdateComplete()
		{
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00017EE8 File Offset: 0x000160E8
		public override int GetHashCode()
		{
			return this.m_CachedHashFromTransform;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00017EF0 File Offset: 0x000160F0
		public override bool Equals(object obj)
		{
			return obj.GetHashCode() == this.GetHashCode();
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00017F00 File Offset: 0x00016100
		public override string ToString()
		{
			return "(Layout Rebuilder for) " + this.m_ToRebuild;
		}

		// Token: 0x04000292 RID: 658
		private RectTransform m_ToRebuild;

		// Token: 0x04000293 RID: 659
		private int m_CachedHashFromTransform;

		// Token: 0x04000294 RID: 660
		private static ObjectPool<LayoutRebuilder> s_Rebuilders = new ObjectPool<LayoutRebuilder>(null, delegate(LayoutRebuilder x)
		{
			x.Clear();
		});
	}
}
