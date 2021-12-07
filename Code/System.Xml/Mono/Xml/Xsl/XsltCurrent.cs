using System;
using System.Xml.XPath;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000A5 RID: 165
	internal class XsltCurrent : XPathFunction
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x00023254 File Offset: 0x00021454
		public XsltCurrent(FunctionArguments args) : base(args)
		{
			if (args != null)
			{
				throw new XPathException("current takes 0 args");
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00023270 File Offset: 0x00021470
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00023274 File Offset: 0x00021474
		public override object Evaluate(BaseIterator iter)
		{
			XsltCompiledContext xsltCompiledContext = (XsltCompiledContext)iter.NamespaceManager;
			return new SelfIterator(xsltCompiledContext.Processor.CurrentNode, xsltCompiledContext);
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x000232A0 File Offset: 0x000214A0
		internal override bool Peer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000232A4 File Offset: 0x000214A4
		public override string ToString()
		{
			return "current()";
		}
	}
}
