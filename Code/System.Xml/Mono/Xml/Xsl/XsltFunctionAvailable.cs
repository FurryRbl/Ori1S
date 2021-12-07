using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000A9 RID: 169
	internal class XsltFunctionAvailable : XPathFunction
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x00023A2C File Offset: 0x00021C2C
		public XsltFunctionAvailable(FunctionArguments args, IStaticXsltContext ctx) : base(args)
		{
			if (args == null || args.Tail != null)
			{
				throw new XPathException("element-available takes 1 arg");
			}
			this.arg0 = args.Arg;
			this.ctx = ctx;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00023A70 File Offset: 0x00021C70
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00023A74 File Offset: 0x00021C74
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer;
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00023A84 File Offset: 0x00021C84
		public override object Evaluate(BaseIterator iter)
		{
			string text = this.arg0.EvaluateString(iter);
			int num = text.IndexOf(':');
			if (num > 0)
			{
				return (iter.NamespaceManager as XsltCompiledContext).ResolveFunction(XslNameUtil.FromString(text, this.ctx), null) != null;
			}
			return text == "boolean" || text == "ceiling" || text == "concat" || text == "contains" || text == "count" || text == "false" || text == "floor" || text == "id" || text == "lang" || text == "last" || text == "local-name" || text == "name" || text == "namespace-uri" || text == "normalize-space" || text == "not" || text == "number" || text == "position" || text == "round" || text == "starts-with" || text == "string" || text == "string-length" || text == "substring" || text == "substring-after" || text == "substring-before" || text == "sum" || text == "translate" || text == "true" || text == "document" || text == "format-number" || text == "function-available" || text == "generate-id" || text == "key" || text == "current" || text == "unparsed-entity-uri" || text == "element-available" || text == "system-property";
		}

		// Token: 0x040003AB RID: 939
		private Expression arg0;

		// Token: 0x040003AC RID: 940
		private IStaticXsltContext ctx;
	}
}
