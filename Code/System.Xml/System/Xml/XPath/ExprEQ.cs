using System;

namespace System.Xml.XPath
{
	// Token: 0x02000171 RID: 369
	internal class ExprEQ : EqualityExpr
	{
		// Token: 0x0600101E RID: 4126 RVA: 0x0004C51C File Offset: 0x0004A71C
		public ExprEQ(Expression left, Expression right) : base(left, right, true)
		{
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x0004C528 File Offset: 0x0004A728
		protected override string Operator
		{
			get
			{
				return "=";
			}
		}
	}
}
