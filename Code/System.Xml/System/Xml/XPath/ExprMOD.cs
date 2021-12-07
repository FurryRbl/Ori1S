using System;

namespace System.Xml.XPath
{
	// Token: 0x0200017D RID: 381
	internal class ExprMOD : ExprNumeric
	{
		// Token: 0x06001047 RID: 4167 RVA: 0x0004CA2C File Offset: 0x0004AC2C
		public ExprMOD(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0004CA38 File Offset: 0x0004AC38
		protected override string Operator
		{
			get
			{
				return "%";
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x0004CA40 File Offset: 0x0004AC40
		public override double StaticValueAsNumber
		{
			get
			{
				return (!this.HasStaticValue) ? 0.0 : (this._left.StaticValueAsNumber % this._right.StaticValueAsNumber);
			}
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0004CA80 File Offset: 0x0004AC80
		public override double EvaluateNumber(BaseIterator iter)
		{
			return this._left.EvaluateNumber(iter) % this._right.EvaluateNumber(iter);
		}
	}
}
