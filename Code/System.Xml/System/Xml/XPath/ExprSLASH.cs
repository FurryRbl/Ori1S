using System;

namespace System.Xml.XPath
{
	// Token: 0x02000181 RID: 385
	internal class ExprSLASH : NodeSet
	{
		// Token: 0x06001060 RID: 4192 RVA: 0x0004CCF4 File Offset: 0x0004AEF4
		public ExprSLASH(Expression left, NodeSet right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0004CD0C File Offset: 0x0004AF0C
		public override Expression Optimize()
		{
			this.left = this.left.Optimize();
			this.right = (NodeSet)this.right.Optimize();
			return this;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0004CD44 File Offset: 0x0004AF44
		public override string ToString()
		{
			return this.left.ToString() + "/" + this.right.ToString();
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0004CD74 File Offset: 0x0004AF74
		public override object Evaluate(BaseIterator iter)
		{
			BaseIterator iter2 = this.left.EvaluateNodeSet(iter);
			if (this.left.Peer && this.right.Subtree)
			{
				return new SimpleSlashIterator(iter2, this.right);
			}
			BaseIterator iter3 = new SlashIterator(iter2, this.right);
			return new SortedIterator(iter3);
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0004CDD0 File Offset: 0x0004AFD0
		public override bool RequireSorting
		{
			get
			{
				return this.left.RequireSorting || this.right.RequireSorting;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0004CDF0 File Offset: 0x0004AFF0
		internal override XPathNodeType EvaluatedNodeType
		{
			get
			{
				return this.right.EvaluatedNodeType;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0004CE00 File Offset: 0x0004B000
		internal override bool IsPositional
		{
			get
			{
				return this.left.IsPositional || this.right.IsPositional;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0004CE20 File Offset: 0x0004B020
		internal override bool Peer
		{
			get
			{
				return this.left.Peer && this.right.Peer;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x0004CE40 File Offset: 0x0004B040
		internal override bool Subtree
		{
			get
			{
				NodeSet nodeSet = this.left as NodeSet;
				return nodeSet != null && nodeSet.Subtree && this.right.Subtree;
			}
		}

		// Token: 0x040006E7 RID: 1767
		public Expression left;

		// Token: 0x040006E8 RID: 1768
		public NodeSet right;
	}
}
