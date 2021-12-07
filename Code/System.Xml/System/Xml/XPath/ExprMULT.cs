using System;

namespace System.Xml.XPath
{
	// Token: 0x0200017B RID: 379
	internal class ExprMULT : ExprNumeric
	{
		// Token: 0x0600103F RID: 4159 RVA: 0x0004C94C File Offset: 0x0004AB4C
		public ExprMULT(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0004C958 File Offset: 0x0004AB58
		protected override string Operator
		{
			get
			{
				return "*";
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x0004C960 File Offset: 0x0004AB60
		public override double StaticValueAsNumber
		{
			get
			{
				return (!this.HasStaticValue) ? 0.0 : (this._left.StaticValueAsNumber * this._right.StaticValueAsNumber);
			}
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0004C9A0 File Offset: 0x0004ABA0
		public override double EvaluateNumber(BaseIterator iter)
		{
			return this._left.EvaluateNumber(iter) * this._right.EvaluateNumber(iter);
		}
	}
}
