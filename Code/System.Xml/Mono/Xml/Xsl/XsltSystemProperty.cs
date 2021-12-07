using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000AC RID: 172
	internal class XsltSystemProperty : XPathFunction
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x00023F40 File Offset: 0x00022140
		public XsltSystemProperty(FunctionArguments args, IStaticXsltContext ctx) : base(args)
		{
			if (args == null || args.Tail != null)
			{
				throw new XPathException("system-property takes 1 arg");
			}
			this.arg0 = args.Arg;
			this.ctx = ctx;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00023F84 File Offset: 0x00022184
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00023F88 File Offset: 0x00022188
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer;
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00023F98 File Offset: 0x00022198
		public override object Evaluate(BaseIterator iter)
		{
			XmlQualifiedName xmlQualifiedName = XslNameUtil.FromString(this.arg0.EvaluateString(iter), this.ctx);
			if (xmlQualifiedName.Namespace == "http://www.w3.org/1999/XSL/Transform")
			{
				string name = xmlQualifiedName.Name;
				switch (name)
				{
				case "version":
					return "1.0";
				case "vendor":
					return "Mono";
				case "vendor-url":
					return "http://www.go-mono.com/";
				}
			}
			return string.Empty;
		}

		// Token: 0x040003B1 RID: 945
		private Expression arg0;

		// Token: 0x040003B2 RID: 946
		private IStaticXsltContext ctx;
	}
}
