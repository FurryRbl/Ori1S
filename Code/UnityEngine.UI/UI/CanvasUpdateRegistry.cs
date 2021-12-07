using System;
using UnityEngine.UI.Collections;

namespace UnityEngine.UI
{
	// Token: 0x0200003C RID: 60
	public class CanvasUpdateRegistry
	{
		// Token: 0x06000171 RID: 369 RVA: 0x00005C88 File Offset: 0x00003E88
		protected CanvasUpdateRegistry()
		{
			Canvas.willRenderCanvases += this.PerformUpdate;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005CCC File Offset: 0x00003ECC
		public static CanvasUpdateRegistry instance
		{
			get
			{
				if (CanvasUpdateRegistry.s_Instance == null)
				{
					CanvasUpdateRegistry.s_Instance = new CanvasUpdateRegistry();
				}
				return CanvasUpdateRegistry.s_Instance;
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005CE8 File Offset: 0x00003EE8
		private bool ObjectValidForUpdate(ICanvasElement element)
		{
			bool result = element != null;
			bool flag = element is Object;
			if (flag)
			{
				result = (element as Object != null);
			}
			return result;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005D1C File Offset: 0x00003F1C
		private void CleanInvalidItems()
		{
			for (int i = this.m_LayoutRebuildQueue.Count - 1; i >= 0; i--)
			{
				ICanvasElement canvasElement = this.m_LayoutRebuildQueue[i];
				if (canvasElement == null)
				{
					this.m_LayoutRebuildQueue.RemoveAt(i);
				}
				else if (canvasElement.IsDestroyed())
				{
					this.m_LayoutRebuildQueue.RemoveAt(i);
					canvasElement.LayoutComplete();
				}
			}
			for (int j = this.m_GraphicRebuildQueue.Count - 1; j >= 0; j--)
			{
				ICanvasElement canvasElement2 = this.m_GraphicRebuildQueue[j];
				if (canvasElement2 == null)
				{
					this.m_GraphicRebuildQueue.RemoveAt(j);
				}
				else if (canvasElement2.IsDestroyed())
				{
					this.m_GraphicRebuildQueue.RemoveAt(j);
					canvasElement2.GraphicUpdateComplete();
				}
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005DE8 File Offset: 0x00003FE8
		private void PerformUpdate()
		{
			this.CleanInvalidItems();
			this.m_PerformingLayoutUpdate = true;
			this.m_LayoutRebuildQueue.Sort(CanvasUpdateRegistry.s_SortLayoutFunction);
			for (int i = 0; i <= 2; i++)
			{
				for (int j = 0; j < this.m_LayoutRebuildQueue.Count; j++)
				{
					ICanvasElement canvasElement = CanvasUpdateRegistry.instance.m_LayoutRebuildQueue[j];
					try
					{
						if (this.ObjectValidForUpdate(canvasElement))
						{
							canvasElement.Rebuild((CanvasUpdate)i);
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception, canvasElement.transform);
					}
				}
			}
			for (int k = 0; k < this.m_LayoutRebuildQueue.Count; k++)
			{
				this.m_LayoutRebuildQueue[k].LayoutComplete();
			}
			CanvasUpdateRegistry.instance.m_LayoutRebuildQueue.Clear();
			this.m_PerformingLayoutUpdate = false;
			ClipperRegistry.instance.Cull();
			this.m_PerformingGraphicUpdate = true;
			for (int l = 3; l < 5; l++)
			{
				for (int m = 0; m < CanvasUpdateRegistry.instance.m_GraphicRebuildQueue.Count; m++)
				{
					try
					{
						ICanvasElement canvasElement2 = CanvasUpdateRegistry.instance.m_GraphicRebuildQueue[m];
						if (this.ObjectValidForUpdate(canvasElement2))
						{
							canvasElement2.Rebuild((CanvasUpdate)l);
						}
					}
					catch (Exception exception2)
					{
						Debug.LogException(exception2, CanvasUpdateRegistry.instance.m_GraphicRebuildQueue[m].transform);
					}
				}
			}
			for (int n = 0; n < this.m_GraphicRebuildQueue.Count; n++)
			{
				this.m_GraphicRebuildQueue[n].LayoutComplete();
			}
			CanvasUpdateRegistry.instance.m_GraphicRebuildQueue.Clear();
			this.m_PerformingGraphicUpdate = false;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005FDC File Offset: 0x000041DC
		private static int ParentCount(Transform child)
		{
			if (child == null)
			{
				return 0;
			}
			Transform parent = child.parent;
			int num = 0;
			while (parent != null)
			{
				num++;
				parent = parent.parent;
			}
			return num;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006020 File Offset: 0x00004220
		private static int SortLayoutList(ICanvasElement x, ICanvasElement y)
		{
			Transform transform = x.transform;
			Transform transform2 = y.transform;
			return CanvasUpdateRegistry.ParentCount(transform) - CanvasUpdateRegistry.ParentCount(transform2);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006048 File Offset: 0x00004248
		public static void RegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			CanvasUpdateRegistry.instance.InternalRegisterCanvasElementForLayoutRebuild(element);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006058 File Offset: 0x00004258
		public static bool TryRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			return CanvasUpdateRegistry.instance.InternalRegisterCanvasElementForLayoutRebuild(element);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006068 File Offset: 0x00004268
		private bool InternalRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			if (this.m_LayoutRebuildQueue.Contains(element))
			{
				return false;
			}
			if (this.m_PerformingLayoutUpdate)
			{
				Debug.LogError(string.Format("Trying to add {0} for layout rebuild while we are already inside a layout rebuild loop. This is not supported.", element));
				return false;
			}
			return this.m_LayoutRebuildQueue.AddUnique(element);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000060B4 File Offset: 0x000042B4
		public static void RegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			CanvasUpdateRegistry.instance.InternalRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000060C4 File Offset: 0x000042C4
		public static bool TryRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			return CanvasUpdateRegistry.instance.InternalRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000060D4 File Offset: 0x000042D4
		private bool InternalRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			if (this.m_PerformingGraphicUpdate)
			{
				Debug.LogError(string.Format("Trying to add {0} for graphic rebuild while we are already inside a graphic rebuild loop. This is not supported.", element));
				return false;
			}
			return this.m_GraphicRebuildQueue.AddUnique(element);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006100 File Offset: 0x00004300
		public static void UnRegisterCanvasElementForRebuild(ICanvasElement element)
		{
			CanvasUpdateRegistry.instance.InternalUnRegisterCanvasElementForLayoutRebuild(element);
			CanvasUpdateRegistry.instance.InternalUnRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006118 File Offset: 0x00004318
		private void InternalUnRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			if (this.m_PerformingLayoutUpdate)
			{
				Debug.LogError(string.Format("Trying to remove {0} from rebuild list while we are already inside a rebuild loop. This is not supported.", element));
				return;
			}
			element.LayoutComplete();
			CanvasUpdateRegistry.instance.m_LayoutRebuildQueue.Remove(element);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006150 File Offset: 0x00004350
		private void InternalUnRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			if (this.m_PerformingGraphicUpdate)
			{
				Debug.LogError(string.Format("Trying to remove {0} from rebuild list while we are already inside a rebuild loop. This is not supported.", element));
				return;
			}
			element.GraphicUpdateComplete();
			CanvasUpdateRegistry.instance.m_GraphicRebuildQueue.Remove(element);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006188 File Offset: 0x00004388
		public static bool IsRebuildingLayout()
		{
			return CanvasUpdateRegistry.instance.m_PerformingLayoutUpdate;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006194 File Offset: 0x00004394
		public static bool IsRebuildingGraphics()
		{
			return CanvasUpdateRegistry.instance.m_PerformingGraphicUpdate;
		}

		// Token: 0x040000B1 RID: 177
		private static CanvasUpdateRegistry s_Instance;

		// Token: 0x040000B2 RID: 178
		private bool m_PerformingLayoutUpdate;

		// Token: 0x040000B3 RID: 179
		private bool m_PerformingGraphicUpdate;

		// Token: 0x040000B4 RID: 180
		private readonly IndexedSet<ICanvasElement> m_LayoutRebuildQueue = new IndexedSet<ICanvasElement>();

		// Token: 0x040000B5 RID: 181
		private readonly IndexedSet<ICanvasElement> m_GraphicRebuildQueue = new IndexedSet<ICanvasElement>();

		// Token: 0x040000B6 RID: 182
		private static readonly Comparison<ICanvasElement> s_SortLayoutFunction = new Comparison<ICanvasElement>(CanvasUpdateRegistry.SortLayoutList);
	}
}
