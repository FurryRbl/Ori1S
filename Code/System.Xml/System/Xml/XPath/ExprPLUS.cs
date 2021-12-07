using System;

namespace System.Xml.XPath
{
	// Token: 0x02000179 RID: 377
	internal class ExprPLUS : ExprNumeric
	{
		// Token: 0x06001037 RID: 4151 RVA: 0x0004C86C File Offset: 0x0004AA6C
		public ExprPLUS(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0004C878 File Offset: 0x0004AA78
		protected override string Operator
		{
			get
			{
				return "+";
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x0004C880 File Offset: 0x0004AA80
		public override double StaticValueAsNumber
		{
			get
			{
				return (!this.HasStaticValue) ? 0.0 : (this._left.StaticValueAsNumber + this._right.StaticValueAsNumber);
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0004C8C0 File Offset: 0x0004AAC0
		public override double EvaluateNumber(BaseIterator iter)
		{
			return this._left.EvaluateNumber(iter) + this._right.EvaluateNumber(iter);
		}
	}
}
