using System;

namespace System.Xml.XPath
{
	// Token: 0x02000180 RID: 384
	internal class ExprUNION : NodeSet
	{
		// Token: 0x06001058 RID: 4184 RVA: 0x0004CB9C File Offset: 0x0004AD9C
		public ExprUNION(Expression left, Expression right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0004CBB4 File Offset: 0x0004ADB4
		public override Expression Optimize()
		{
			this.left = this.left.Optimize();
			this.right = this.right.Optimize();
			return this;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0004CBDC File Offset: 0x0004ADDC
		public override string ToString()
		{
			return this.left.ToString() + " | " + this.right.ToString();
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0004CC0C File Offset: 0x0004AE0C
		public override object Evaluate(BaseIterator iter)
		{
			BaseIterator baseIterator = this.left.EvaluateNodeSet(iter);
			BaseIterator baseIterator2 = this.right.EvaluateNodeSet(iter);
			return new UnionIterator(iter, baseIterator, baseIterator2);
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0004CC3C File Offset: 0x0004AE3C
		internal override XPathNodeType EvaluatedNodeType
		{
			get
			{
				return (this.left.EvaluatedNodeType != this.right.EvaluatedNodeType) ? XPathNodeType.All : this.left.EvaluatedNodeType;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x0004CC6C File Offset: 0x0004AE6C
		internal override bool IsPositional
		{
			get
			{
				return this.left.IsPositional || this.right.IsPositional;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0004CC8C File Offset: 0x0004AE8C
		internal override bool Peer
		{
			get
			{
				return this.left.Peer && this.right.Peer;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x0004CCAC File Offset: 0x0004AEAC
		internal override bool Subtree
		{
			get
			{
				NodeSet nodeSet = this.left as NodeSet;
				NodeSet nodeSet2 = this.right as NodeSet;
				return nodeSet != null && nodeSet2 != null && nodeSet.Subtree && nodeSet2.Subtree;
			}
		}

		// Token: 0x040006E5 RID: 1765
		internal Expression left;

		// Token: 0x040006E6 RID: 1766
		internal Expression right;
	}
}
