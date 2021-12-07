using System;

namespace System.Xml.XPath
{
	// Token: 0x0200016D RID: 365
	internal abstract class ExprBoolean : ExprBinary
	{
		// Token: 0x0600100D RID: 4109 RVA: 0x0004BF98 File Offset: 0x0004A198
		public ExprBoolean(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0004BFA4 File Offset: 0x0004A1A4
		public override Expression Optimize()
		{
			base.Optimize();
			if (!this.HasStaticValue)
			{
				return this;
			}
			if (this.StaticValueAsBoolean)
			{
				return new XPathFunctionTrue(null);
			}
			return new XPathFunctionFalse(null);
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x0004BFE0 File Offset: 0x0004A1E0
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0004BFE4 File Offset: 0x0004A1E4
		public override object Evaluate(BaseIterator iter)
		{
			return this.EvaluateBoolean(iter);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0004BFF4 File Offset: 0x0004A1F4
		public override double EvaluateNumber(BaseIterator iter)
		{
			return (double)((!this.EvaluateBoolean(iter)) ? 0 : 1);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0004C00C File Offset: 0x0004A20C
		public override string EvaluateString(BaseIterator iter)
		{
			return (!this.EvaluateBoolean(iter)) ? "false" : "true";
		}
	}
}
