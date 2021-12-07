using System;
using System.Collections;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000AE RID: 174
	internal class MSXslNodeSet : XPathFunction
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x00024140 File Offset: 0x00022340
		public MSXslNodeSet(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail != null)
			{
				throw new XPathException("element-available takes 1 arg");
			}
			this.arg0 = args.Arg;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00024174 File Offset: 0x00022374
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00024178 File Offset: 0x00022378
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer;
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00024188 File Offset: 0x00022388
		public override object Evaluate(BaseIterator iter)
		{
			XsltCompiledContext nsm = iter.NamespaceManager as XsltCompiledContext;
			XPathNavigator xpathNavigator = (iter.Current == null) ? null : iter.Current.Clone();
			XPathNavigator xpathNavigator2 = this.arg0.EvaluateAs(iter, XPathResultType.Navigator) as XPathNavigator;
			if (xpathNavigator2 != null)
			{
				return new ListIterator(new ArrayList
				{
					xpathNavigator2
				}, nsm);
			}
			if (xpathNavigator != null)
			{
				return new XsltException("Cannot convert the XPath argument to a result tree fragment.", null, xpathNavigator);
			}
			return new XsltException("Cannot convert the XPath argument to a result tree fragment.", null);
		}

		// Token: 0x040003B5 RID: 949
		private Expression arg0;
	}
}
