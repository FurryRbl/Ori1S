using System;

namespace System.Xml.XPath
{
	// Token: 0x0200017E RID: 382
	internal class ExprNEG : Expression
	{
		// Token: 0x0600104B RID: 4171 RVA: 0x0004CA9C File Offset: 0x0004AC9C
		public ExprNEG(Expression expr)
		{
			this._expr = expr;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0004CAAC File Offset: 0x0004ACAC
		public override string ToString()
		{
			return "- " + this._expr.ToString();
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0004CAC4 File Offset: 0x0004ACC4
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0004CAC8 File Offset: 0x0004ACC8
		public override Expression Optimize()
		{
			this._expr = this._expr.Optimize();
			return this.HasStaticValue ? new ExprNumber(this.StaticValueAsNumber) : this;
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0004CB04 File Offset: 0x0004AD04
		internal override bool Peer
		{
			get
			{
				return this._expr.Peer;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0004CB14 File Offset: 0x0004AD14
		public override bool HasStaticValue
		{
			get
			{
				return this._expr.HasStaticValue;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x0004CB24 File Offset: 0x0004AD24
		public override double StaticValueAsNumber
		{
			get
			{
				return (!this._expr.HasStaticValue) ? 0.0 : (-1.0 * this._expr.StaticValueAsNumber);
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0004CB5C File Offset: 0x0004AD5C
		public override object Evaluate(BaseIterator iter)
		{
			return -this._expr.EvaluateNumber(iter);
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0004CB70 File Offset: 0x0004AD70
		public override double EvaluateNumber(BaseIterator iter)
		{
			return -this._expr.EvaluateNumber(iter);
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0004CB80 File Offset: 0x0004AD80
		internal override bool IsPositional
		{
			get
			{
				return this._expr.IsPositional;
			}
		}

		// Token: 0x040006E4 RID: 1764
		private Expression _expr;
	}
}
