using System;
using System.Collections.Generic;

namespace LostPolygon.SpriteSharp.ClipperLib
{
	// Token: 0x02000004 RID: 4
	public class PolyNode
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002148 File Offset: 0x00000348
		private bool IsHoleNode()
		{
			bool flag = true;
			for (PolyNode parent = this.m_Parent; parent != null; parent = parent.m_Parent)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002170 File Offset: 0x00000370
		public int ChildCount
		{
			get
			{
				return this.m_Childs.Count;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000217D File Offset: 0x0000037D
		public List<IntPoint> Contour
		{
			get
			{
				return this.m_polygon;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002188 File Offset: 0x00000388
		internal void AddChild(PolyNode Child)
		{
			int count = this.m_Childs.Count;
			this.m_Childs.Add(Child);
			Child.m_Parent = this;
			Child.m_Index = count;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021BB File Offset: 0x000003BB
		public PolyNode GetNext()
		{
			if (this.m_Childs.Count > 0)
			{
				return this.m_Childs[0];
			}
			return this.GetNextSiblingUp();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021E0 File Offset: 0x000003E0
		internal PolyNode GetNextSiblingUp()
		{
			if (this.m_Parent == null)
			{
				return null;
			}
			if (this.m_Index == this.m_Parent.m_Childs.Count - 1)
			{
				return this.m_Parent.GetNextSiblingUp();
			}
			return this.m_Parent.m_Childs[this.m_Index + 1];
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002235 File Offset: 0x00000435
		public List<PolyNode> Childs
		{
			get
			{
				return this.m_Childs;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000223D File Offset: 0x0000043D
		public PolyNode Parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002245 File Offset: 0x00000445
		public bool IsHole
		{
			get
			{
				return this.IsHoleNode();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000224D File Offset: 0x0000044D
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002255 File Offset: 0x00000455
		public bool IsOpen { get; set; }

		// Token: 0x04000004 RID: 4
		internal PolyNode m_Parent;

		// Token: 0x04000005 RID: 5
		internal List<IntPoint> m_polygon = new List<IntPoint>();

		// Token: 0x04000006 RID: 6
		internal int m_Index;

		// Token: 0x04000007 RID: 7
		internal JoinType m_jointype;

		// Token: 0x04000008 RID: 8
		internal EndType m_endtype;

		// Token: 0x04000009 RID: 9
		internal List<PolyNode> m_Childs = new List<PolyNode>();
	}
}
