using System;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000AA RID: 170
	internal class XsltGenerateId : XPathFunction
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x00023D1C File Offset: 0x00021F1C
		public XsltGenerateId(FunctionArguments args) : base(args)
		{
			if (args != null)
			{
				if (args.Tail != null)
				{
					throw new XPathException("generate-id takes 1 or no args");
				}
				this.arg0 = args.Arg;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00023D50 File Offset: 0x00021F50
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00023D54 File Offset: 0x00021F54
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer;
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00023D64 File Offset: 0x00021F64
		public override object Evaluate(BaseIterator iter)
		{
			XPathNavigator xpathNavigator;
			if (this.arg0 != null)
			{
				XPathNodeIterator xpathNodeIterator = this.arg0.EvaluateNodeSet(iter);
				if (!xpathNodeIterator.MoveNext())
				{
					return string.Empty;
				}
				xpathNavigator = xpathNodeIterator.Current.Clone();
			}
			else
			{
				xpathNavigator = iter.Current.Clone();
			}
			StringBuilder stringBuilder = new StringBuilder("Mono");
			stringBuilder.Append(XmlConvert.EncodeLocalName(xpathNavigator.BaseURI));
			stringBuilder.Replace('_', 'm');
			stringBuilder.Append(xpathNavigator.NodeType);
			stringBuilder.Append('m');
			do
			{
				stringBuilder.Append(this.IndexInParent(xpathNavigator));
				stringBuilder.Append('m');
			}
			while (xpathNavigator.MoveToParent());
			return stringBuilder.ToString();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00023E28 File Offset: 0x00022028
		private int IndexInParent(XPathNavigator nav)
		{
			int num = 0;
			while (nav.MoveToPrevious())
			{
				num++;
			}
			return num;
		}

		// Token: 0x040003AD RID: 941
		private Expression arg0;
	}
}
