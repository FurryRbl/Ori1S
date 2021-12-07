using System;
using System.Collections.Generic;

namespace LostPolygon.SpriteSharp.ClipperLib
{
	// Token: 0x02000003 RID: 3
	public class PolyTree : PolyNode
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002098 File Offset: 0x00000298
		~PolyTree()
		{
			this.Clear();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020C4 File Offset: 0x000002C4
		public void Clear()
		{
			for (int i = 0; i < this.m_AllPolys.Count; i++)
			{
				this.m_AllPolys[i] = null;
			}
			this.m_AllPolys.Clear();
			this.m_Childs.Clear();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000210A File Offset: 0x0000030A
		public PolyNode GetFirst()
		{
			if (this.m_Childs.Count > 0)
			{
				return this.m_Childs[0];
			}
			return null;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002128 File Offset: 0x00000328
		public int Total
		{
			get
			{
				return this.m_AllPolys.Count;
			}
		}

		// Token: 0x04000003 RID: 3
		internal List<PolyNode> m_AllPolys = new List<PolyNode>();
	}
}
