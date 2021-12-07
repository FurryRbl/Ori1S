using System;

namespace System.Xml.XPath
{
	// Token: 0x02000178 RID: 376
	internal abstract class ExprNumeric : ExprBinary
	{
		// Token: 0x06001033 RID: 4147 RVA: 0x0004C81C File Offset: 0x0004AA1C
		public ExprNumeric(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x0004C828 File Offset: 0x0004AA28
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0004C82C File Offset: 0x0004AA2C
		public override Expression Optimize()
		{
			base.Optimize();
			return this.HasStaticValue ? new ExprNumber(this.StaticValueAsNumber) : this;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0004C85C File Offset: 0x0004AA5C
		public override object Evaluate(BaseIterator iter)
		{
			return this.EvaluateNumber(iter);
		}
	}
}
