using System;

namespace System.Xml.XPath
{
	// Token: 0x02000177 RID: 375
	internal class ExprLE : RelationalExpr
	{
		// Token: 0x06001030 RID: 4144 RVA: 0x0004C7FC File Offset: 0x0004A9FC
		public ExprLE(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0004C808 File Offset: 0x0004AA08
		protected override string Operator
		{
			get
			{
				return "<=";
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0004C810 File Offset: 0x0004AA10
		public override bool Compare(double arg1, double arg2)
		{
			return arg1 <= arg2;
		}
	}
}
