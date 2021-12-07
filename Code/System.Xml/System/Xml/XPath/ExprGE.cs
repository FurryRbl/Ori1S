using System;

namespace System.Xml.XPath
{
	// Token: 0x02000175 RID: 373
	internal class ExprGE : RelationalExpr
	{
		// Token: 0x0600102A RID: 4138 RVA: 0x0004C7C0 File Offset: 0x0004A9C0
		public ExprGE(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0004C7CC File Offset: 0x0004A9CC
		protected override string Operator
		{
			get
			{
				return ">=";
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0004C7D4 File Offset: 0x0004A9D4
		public override bool Compare(double arg1, double arg2)
		{
			return arg1 >= arg2;
		}
	}
}
