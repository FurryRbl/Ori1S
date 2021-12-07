using System;
using System.Xml;
using System.Xml.XPath;

namespace Mono.Xml.XPath
{
	// Token: 0x02000047 RID: 71
	internal class XPathEditableDocument : IXPathNavigable
	{
		// Token: 0x060002AB RID: 683 RVA: 0x00013C4C File Offset: 0x00011E4C
		public XPathEditableDocument(XmlNode node)
		{
			this.node = node;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00013C5C File Offset: 0x00011E5C
		public XmlNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00013C64 File Offset: 0x00011E64
		public XPathNavigator CreateNavigator()
		{
			return new XmlDocumentEditableNavigator(this);
		}

		// Token: 0x0400021A RID: 538
		private XmlNode node;
	}
}
