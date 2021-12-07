using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000075 RID: 117
	internal class XPathVariableBinding : Expression
	{
		// Token: 0x060003CC RID: 972 RVA: 0x00019B30 File Offset: 0x00017D30
		public XPathVariableBinding(XslGeneralVariable v)
		{
			this.v = v;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00019B40 File Offset: 0x00017D40
		public override string ToString()
		{
			return "$" + this.v.Name.ToString();
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00019B5C File Offset: 0x00017D5C
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00019B60 File Offset: 0x00017D60
		public override XPathResultType GetReturnType(BaseIterator iter)
		{
			return XPathResultType.Any;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00019B64 File Offset: 0x00017D64
		public override object Evaluate(BaseIterator iter)
		{
			return this.v.Evaluate(iter.NamespaceManager as XsltContext);
		}

		// Token: 0x040002A5 RID: 677
		private XslGeneralVariable v;
	}
}
