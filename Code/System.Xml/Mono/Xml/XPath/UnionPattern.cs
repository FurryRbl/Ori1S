using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.XPath
{
	// Token: 0x02000046 RID: 70
	internal class UnionPattern : Pattern
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x00013BA4 File Offset: 0x00011DA4
		public UnionPattern(Pattern p0, Pattern p1)
		{
			this.p0 = p0;
			this.p1 = p1;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00013BBC File Offset: 0x00011DBC
		public override XPathNodeType EvaluatedNodeType
		{
			get
			{
				return (this.p0.EvaluatedNodeType != this.p1.EvaluatedNodeType) ? XPathNodeType.All : this.p0.EvaluatedNodeType;
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00013BEC File Offset: 0x00011DEC
		public override bool Matches(XPathNavigator node, XsltContext ctx)
		{
			return this.p0.Matches(node, ctx) || this.p1.Matches(node, ctx);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00013C1C File Offset: 0x00011E1C
		public override string ToString()
		{
			return this.p0.ToString() + " | " + this.p1.ToString();
		}

		// Token: 0x04000218 RID: 536
		public readonly Pattern p0;

		// Token: 0x04000219 RID: 537
		public readonly Pattern p1;
	}
}
