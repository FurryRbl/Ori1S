using System;

namespace System.Xml.XPath
{
	// Token: 0x02000189 RID: 393
	internal class ExprFilter : NodeSet
	{
		// Token: 0x06001096 RID: 4246 RVA: 0x0004D840 File Offset: 0x0004BA40
		public ExprFilter(Expression expr, Expression pred)
		{
			this.expr = expr;
			this.pred = pred;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0004D858 File Offset: 0x0004BA58
		public override Expression Optimize()
		{
			this.expr = this.expr.Optimize();
			this.pred = this.pred.Optimize();
			return this;
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x0004D880 File Offset: 0x0004BA80
		internal Expression LeftHandSide
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0004D888 File Offset: 0x0004BA88
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"(",
				this.expr.ToString(),
				")[",
				this.pred.ToString(),
				"]"
			});
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0004D8D4 File Offset: 0x0004BAD4
		public override object Evaluate(BaseIterator iter)
		{
			BaseIterator iter2 = this.expr.EvaluateNodeSet(iter);
			return new PredicateIterator(iter2, this.pred);
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0004D8FC File Offset: 0x0004BAFC
		internal override XPathNodeType EvaluatedNodeType
		{
			get
			{
				return this.expr.EvaluatedNodeType;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0004D90C File Offset: 0x0004BB0C
		internal override bool IsPositional
		{
			get
			{
				return this.pred.ReturnType == XPathResultType.Number || this.expr.IsPositional || this.pred.IsPositional;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x0004D94C File Offset: 0x0004BB4C
		internal override bool Peer
		{
			get
			{
				return this.expr.Peer && this.pred.Peer;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x0004D96C File Offset: 0x0004BB6C
		internal override bool Subtree
		{
			get
			{
				NodeSet nodeSet = this.expr as NodeSet;
				return nodeSet != null && nodeSet.Subtree;
			}
		}

		// Token: 0x04000700 RID: 1792
		internal Expression expr;

		// Token: 0x04000701 RID: 1793
		internal Expression pred;
	}
}
