using System;

namespace System.Xml.XPath
{
	// Token: 0x0200018E RID: 398
	internal class ExprParens : Expression
	{
		// Token: 0x060010BF RID: 4287 RVA: 0x0004DBEC File Offset: 0x0004BDEC
		public ExprParens(Expression expr)
		{
			this._expr = expr;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0004DBFC File Offset: 0x0004BDFC
		public override Expression Optimize()
		{
			this._expr.Optimize();
			return this;
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0004DC0C File Offset: 0x0004BE0C
		public override bool HasStaticValue
		{
			get
			{
				return this._expr.HasStaticValue;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x0004DC1C File Offset: 0x0004BE1C
		public override object StaticValue
		{
			get
			{
				return this._expr.StaticValue;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x0004DC2C File Offset: 0x0004BE2C
		public override string StaticValueAsString
		{
			get
			{
				return this._expr.StaticValueAsString;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0004DC3C File Offset: 0x0004BE3C
		public override double StaticValueAsNumber
		{
			get
			{
				return this._expr.StaticValueAsNumber;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x0004DC4C File Offset: 0x0004BE4C
		public override bool StaticValueAsBoolean
		{
			get
			{
				return this._expr.StaticValueAsBoolean;
			}
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0004DC5C File Offset: 0x0004BE5C
		public override string ToString()
		{
			return "(" + this._expr.ToString() + ")";
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0004DC78 File Offset: 0x0004BE78
		public override XPathResultType ReturnType
		{
			get
			{
				return this._expr.ReturnType;
			}
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0004DC88 File Offset: 0x0004BE88
		public override object Evaluate(BaseIterator iter)
		{
			object obj = this._expr.Evaluate(iter);
			XPathNodeIterator xpathNodeIterator = obj as XPathNodeIterator;
			BaseIterator baseIterator = xpathNodeIterator as BaseIterator;
			if (baseIterator == null && xpathNodeIterator != null)
			{
				baseIterator = new WrapperIterator(xpathNodeIterator, iter.NamespaceManager);
			}
			if (baseIterator != null)
			{
				return new ParensIterator(baseIterator);
			}
			return obj;
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x0004DCD8 File Offset: 0x0004BED8
		internal override XPathNodeType EvaluatedNodeType
		{
			get
			{
				return this._expr.EvaluatedNodeType;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x0004DCE8 File Offset: 0x0004BEE8
		internal override bool IsPositional
		{
			get
			{
				return this._expr.IsPositional;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0004DCF8 File Offset: 0x0004BEF8
		internal override bool Peer
		{
			get
			{
				return this._expr.Peer;
			}
		}

		// Token: 0x04000707 RID: 1799
		protected Expression _expr;
	}
}
