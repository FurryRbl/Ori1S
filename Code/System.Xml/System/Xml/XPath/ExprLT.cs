using System;

namespace System.Xml.XPath
{
	// Token: 0x02000176 RID: 374
	internal class ExprLT : RelationalExpr
	{
		// Token: 0x0600102D RID: 4141 RVA: 0x0004C7E0 File Offset: 0x0004A9E0
		public ExprLT(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0004C7EC File Offset: 0x0004A9EC
		protected override string Operator
		{
			get
			{
				return "<";
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0004C7F4 File Offset: 0x0004A9F4
		public override bool Compare(double arg1, double arg2)
		{
			return arg1 < arg2;
		}
	}
}
