using System;

namespace System.Xml.XPath
{
	// Token: 0x0200017C RID: 380
	internal class ExprDIV : ExprNumeric
	{
		// Token: 0x06001043 RID: 4163 RVA: 0x0004C9BC File Offset: 0x0004ABBC
		public ExprDIV(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0004C9C8 File Offset: 0x0004ABC8
		protected override string Operator
		{
			get
			{
				return " div ";
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x0004C9D0 File Offset: 0x0004ABD0
		public override double StaticValueAsNumber
		{
			get
			{
				return (!this.HasStaticValue) ? 0.0 : (this._left.StaticValueAsNumber / this._right.StaticValueAsNumber);
			}
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0004CA10 File Offset: 0x0004AC10
		public override double EvaluateNumber(BaseIterator iter)
		{
			return this._left.EvaluateNumber(iter) / this._right.EvaluateNumber(iter);
		}
	}
}
