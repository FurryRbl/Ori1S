using System;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	// Token: 0x020001B0 RID: 432
	internal class SimpleXsltDebugger
	{
		// Token: 0x060011AA RID: 4522 RVA: 0x00050E00 File Offset: 0x0004F000
		public void OnCompile(XPathNavigator style)
		{
			Console.Write("Compiling: ");
			this.PrintXPathNavigator(style);
			Console.WriteLine();
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00050E18 File Offset: 0x0004F018
		public void OnExecute(XPathNodeIterator currentNodeSet, XPathNavigator style, XsltContext xsltContext)
		{
			Console.Write("Executing: ");
			this.PrintXPathNavigator(style);
			Console.WriteLine(" / NodeSet: (type {1}) {0} / XsltContext: {2}", currentNodeSet, currentNodeSet.GetType(), xsltContext);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00050E48 File Offset: 0x0004F048
		private void PrintXPathNavigator(XPathNavigator nav)
		{
			IXmlLineInfo xmlLineInfo = nav as IXmlLineInfo;
			IXmlLineInfo xmlLineInfo3;
			if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
			{
				IXmlLineInfo xmlLineInfo2 = xmlLineInfo;
				xmlLineInfo3 = xmlLineInfo2;
			}
			else
			{
				xmlLineInfo3 = null;
			}
			xmlLineInfo = xmlLineInfo3;
			Console.Write("({0}, {1}) element {2}", (xmlLineInfo == null) ? 0 : xmlLineInfo.LineNumber, (xmlLineInfo == null) ? 0 : xmlLineInfo.LinePosition, nav.Name);
		}
	}
}
