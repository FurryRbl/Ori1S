using System;
using System.Xml.XPath;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000090 RID: 144
	internal class ExprKeyContainer : Expression
	{
		// Token: 0x060004DE RID: 1246 RVA: 0x0001EE68 File Offset: 0x0001D068
		public ExprKeyContainer(Expression expr)
		{
			this.expr = expr;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0001EE78 File Offset: 0x0001D078
		public Expression BodyExpression
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001EE80 File Offset: 0x0001D080
		public override object Evaluate(BaseIterator iter)
		{
			return this.expr.Evaluate(iter);
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0001EE90 File Offset: 0x0001D090
		internal override XPathNodeType EvaluatedNodeType
		{
			get
			{
				return this.expr.EvaluatedNodeType;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0001EEA0 File Offset: 0x0001D0A0
		public override XPathResultType ReturnType
		{
			get
			{
				return this.expr.ReturnType;
			}
		}

		// Token: 0x0400031F RID: 799
		private Expression expr;
	}
}
