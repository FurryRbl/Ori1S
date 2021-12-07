using System;

namespace System.Xml.XPath
{
	// Token: 0x02000172 RID: 370
	internal class ExprNE : EqualityExpr
	{
		// Token: 0x06001020 RID: 4128 RVA: 0x0004C530 File Offset: 0x0004A730
		public ExprNE(Expression left, Expression right) : base(left, right, false)
		{
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x0004C53C File Offset: 0x0004A73C
		protected override string Operator
		{
			get
			{
				return "!=";
			}
		}
	}
}
