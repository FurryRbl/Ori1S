using System;
using System.Collections.Generic;
using UnityEngine.UI.Collections;

namespace UnityEngine.UI
{
	// Token: 0x0200004A RID: 74
	public class GraphicRegistry
	{
		// Token: 0x0600024B RID: 587 RVA: 0x00009B6C File Offset: 0x00007D6C
		protected GraphicRegistry()
		{
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00009B98 File Offset: 0x00007D98
		public static GraphicRegistry instance
		{
			get
			{
				if (GraphicRegistry.s_Instance == null)
				{
					GraphicRegistry.s_Instance = new GraphicRegistry();
				}
				return GraphicRegistry.s_Instance;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009BB4 File Offset: 0x00007DB4
		public static void RegisterGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null)
			{
				return;
			}
			IndexedSet<Graphic> indexedSet;
			GraphicRegistry.instance.m_Graphics.TryGetValue(c, out indexedSet);
			if (indexedSet != null)
			{
				indexedSet.AddUnique(graphic);
				return;
			}
			indexedSet = new IndexedSet<Graphic>();
			indexedSet.Add(graphic);
			GraphicRegistry.instance.m_Graphics.Add(c, indexedSet);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009C10 File Offset: 0x00007E10
		public static void UnregisterGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null)
			{
				return;
			}
			IndexedSet<Graphic> indexedSet;
			if (GraphicRegistry.instance.m_Graphics.TryGetValue(c, out indexedSet))
			{
				indexedSet.Remove(graphic);
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009C4C File Offset: 0x00007E4C
		public static IList<Graphic> GetGraphicsForCanvas(Canvas canvas)
		{
			IndexedSet<Graphic> result;
			if (GraphicRegistry.instance.m_Graphics.TryGetValue(canvas, out result))
			{
				return result;
			}
			return GraphicRegistry.s_EmptyList;
		}

		// Token: 0x0400010D RID: 269
		private static GraphicRegistry s_Instance;

		// Token: 0x0400010E RID: 270
		private readonly Dictionary<Canvas, IndexedSet<Graphic>> m_Graphics = new Dictionary<Canvas, IndexedSet<Graphic>>();

		// Token: 0x0400010F RID: 271
		private static readonly List<Graphic> s_EmptyList = new List<Graphic>();
	}
}
