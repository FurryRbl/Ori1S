using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000A7 RID: 167
	internal class XsltElementAvailable : XPathFunction
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x0002369C File Offset: 0x0002189C
		public XsltElementAvailable(FunctionArguments args, IStaticXsltContext ctx) : base(args)
		{
			if (args == null || args.Tail != null)
			{
				throw new XPathException("element-available takes 1 arg");
			}
			this.arg0 = args.Arg;
			this.ctx = ctx;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x000236E0 File Offset: 0x000218E0
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x000236E4 File Offset: 0x000218E4
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer;
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000236F4 File Offset: 0x000218F4
		public override object Evaluate(BaseIterator iter)
		{
			XmlQualifiedName xmlQualifiedName = XslNameUtil.FromString(this.arg0.EvaluateString(iter), this.ctx);
			return xmlQualifiedName.Namespace == "http://www.w3.org/1999/XSL/Transform" && (xmlQualifiedName.Name == "apply-imports" || xmlQualifiedName.Name == "apply-templates" || xmlQualifiedName.Name == "call-template" || xmlQualifiedName.Name == "choose" || xmlQualifiedName.Name == "comment" || xmlQualifiedName.Name == "copy" || xmlQualifiedName.Name == "copy-of" || xmlQualifiedName.Name == "element" || xmlQualifiedName.Name == "fallback" || xmlQualifiedName.Name == "for-each" || xmlQualifiedName.Name == "message" || xmlQualifiedName.Name == "number" || xmlQualifiedName.Name == "processing-instruction" || xmlQualifiedName.Name == "text" || xmlQualifiedName.Name == "value-of" || xmlQualifiedName.Name == "variable");
		}

		// Token: 0x040003A5 RID: 933
		private Expression arg0;

		// Token: 0x040003A6 RID: 934
		private IStaticXsltContext ctx;
	}
}
