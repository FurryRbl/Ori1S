using System;

namespace System.Xml.XPath
{
	// Token: 0x0200017A RID: 378
	internal class ExprMINUS : ExprNumeric
	{
		// Token: 0x0600103B RID: 4155 RVA: 0x0004C8DC File Offset: 0x0004AADC
		public ExprMINUS(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x0004C8E8 File Offset: 0x0004AAE8
		protected override string Operator
		{
			get
			{
				return "-";
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0004C8F0 File Offset: 0x0004AAF0
		public override double StaticValueAsNumber
		{
			get
			{
				return (!this.HasStaticValue) ? 0.0 : (this._left.StaticValueAsNumber - this._right.StaticValueAsNumber);
			}
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0004C930 File Offset: 0x0004AB30
		public override double EvaluateNumber(BaseIterator iter)
		{
			return this._left.EvaluateNumber(iter) - this._right.EvaluateNumber(iter);
		}
	}
}
