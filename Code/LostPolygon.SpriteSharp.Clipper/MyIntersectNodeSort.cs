using System;
using System.Collections.Generic;

namespace LostPolygon.SpriteSharp.ClipperLib
{
	// Token: 0x02000011 RID: 17
	public class MyIntersectNodeSort : IComparer<IntersectNode>
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000028DC File Offset: 0x00000ADC
		public int Compare(IntersectNode node1, IntersectNode node2)
		{
			return node2.Pt.Y - node1.Pt.Y;
		}
	}
}
