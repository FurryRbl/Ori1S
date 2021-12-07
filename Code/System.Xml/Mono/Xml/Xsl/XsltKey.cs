using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000AB RID: 171
	internal class XsltKey : XPathFunction
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x00023E4C File Offset: 0x0002204C
		public XsltKey(FunctionArguments args, IStaticXsltContext ctx) : base(args)
		{
			this.staticContext = ctx;
			if (args == null || args.Tail == null)
			{
				throw new XPathException("key takes 2 args");
			}
			this.arg0 = args.Arg;
			this.arg1 = args.Tail.Arg;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00023EA0 File Offset: 0x000220A0
		public Expression KeyName
		{
			get
			{
				return this.arg0;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00023EA8 File Offset: 0x000220A8
		public Expression Field
		{
			get
			{
				return this.arg1;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00023EB0 File Offset: 0x000220B0
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00023EB4 File Offset: 0x000220B4
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer && this.arg1.Peer;
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00023ED4 File Offset: 0x000220D4
		public bool PatternMatches(XPathNavigator nav, XsltContext nsmgr)
		{
			XsltCompiledContext xsltCompiledContext = nsmgr as XsltCompiledContext;
			return xsltCompiledContext.MatchesKey(nav, this.staticContext, this.arg0.StaticValueAsString, this.arg1.StaticValueAsString);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00023F0C File Offset: 0x0002210C
		public override object Evaluate(BaseIterator iter)
		{
			XsltCompiledContext xsltCompiledContext = iter.NamespaceManager as XsltCompiledContext;
			return xsltCompiledContext.EvaluateKey(this.staticContext, iter, this.arg0, this.arg1);
		}

		// Token: 0x040003AE RID: 942
		private Expression arg0;

		// Token: 0x040003AF RID: 943
		private Expression arg1;

		// Token: 0x040003B0 RID: 944
		private IStaticXsltContext staticContext;
	}
}
