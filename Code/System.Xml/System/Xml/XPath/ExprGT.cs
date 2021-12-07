using System;

namespace System.Xml.XPath
{
	// Token: 0x02000174 RID: 372
	internal class ExprGT : RelationalExpr
	{
		// Token: 0x06001027 RID: 4135 RVA: 0x0004C7A4 File Offset: 0x0004A9A4
		public ExprGT(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0004C7B0 File Offset: 0x0004A9B0
		protected override string Operator
		{
			get
			{
				return ">";
			}
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0004C7B8 File Offset: 0x0004A9B8
		public override bool Compare(double arg1, double arg2)
		{
			return arg1 > arg2;
		}
	}
}
