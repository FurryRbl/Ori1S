using System;

namespace System.Xml.XPath
{
	// Token: 0x02000182 RID: 386
	internal class ExprSLASH2 : NodeSet
	{
		// Token: 0x06001069 RID: 4201 RVA: 0x0004CE78 File Offset: 0x0004B078
		public ExprSLASH2(Expression left, NodeSet right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0004CEA0 File Offset: 0x0004B0A0
		public override Expression Optimize()
		{
			this.left = this.left.Optimize();
			this.right = (NodeSet)this.right.Optimize();
			NodeTest nodeTest = this.right as NodeTest;
			if (nodeTest != null && nodeTest.Axis.Axis == Axes.Child)
			{
				NodeNameTest nodeNameTest = nodeTest as NodeNameTest;
				if (nodeNameTest != null)
				{
					return new ExprSLASH(this.left, new NodeNameTest(nodeNameTest, Axes.Descendant));
				}
				NodeTypeTest nodeTypeTest = nodeTest as NodeTypeTest;
				if (nodeTypeTest != null)
				{
					return new ExprSLASH(this.left, new NodeTypeTest(nodeTypeTest, Axes.Descendant));
				}
			}
			return this;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0004CF38 File Offset: 0x0004B138
		public override string ToString()
		{
			return this.left.ToString() + "//" + this.right.ToString();
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0004CF68 File Offset: 0x0004B168
		public override object Evaluate(BaseIterator iter)
		{
			BaseIterator iter2 = this.left.EvaluateNodeSet(iter);
			if (this.left.Peer && !this.left.RequireSorting)
			{
				iter2 = new SimpleSlashIterator(iter2, ExprSLASH2.DescendantOrSelfStar);
			}
			else
			{
				BaseIterator baseIterator = new SlashIterator(iter2, ExprSLASH2.DescendantOrSelfStar);
				iter2 = ((!this.left.RequireSorting) ? baseIterator : new SortedIterator(baseIterator));
			}
			SlashIterator iter3 = new SlashIterator(iter2, this.right);
			return new SortedIterator(iter3);
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0004CFF0 File Offset: 0x0004B1F0
		public override bool RequireSorting
		{
			get
			{
				return this.left.RequireSorting || this.right.RequireSorting;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x0004D010 File Offset: 0x0004B210
		internal override XPathNodeType EvaluatedNodeType
		{
			get
			{
				return this.right.EvaluatedNodeType;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0004D020 File Offset: 0x0004B220
		internal override bool IsPositional
		{
			get
			{
				return this.left.IsPositional || this.right.IsPositional;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0004D040 File Offset: 0x0004B240
		internal override bool Peer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0004D044 File Offset: 0x0004B244
		internal override bool Subtree
		{
			get
			{
				NodeSet nodeSet = this.left as NodeSet;
				return nodeSet != null && nodeSet.Subtree && this.right.Subtree;
			}
		}

		// Token: 0x040006E9 RID: 1769
		public Expression left;

		// Token: 0x040006EA RID: 1770
		public NodeSet right;

		// Token: 0x040006EB RID: 1771
		private static NodeTest DescendantOrSelfStar = new NodeTypeTest(Axes.DescendantOrSelf, XPathNodeType.All);
	}
}
