using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace Mono.Xml.XPath
{
	// Token: 0x02000033 RID: 51
	internal class DTMXPathDocument : IXPathNavigable
	{
		// Token: 0x0600019B RID: 411 RVA: 0x0000D6A0 File Offset: 0x0000B8A0
		public DTMXPathDocument(XmlNameTable nameTable, DTMXPathLinkedNode[] nodes, DTMXPathAttributeNode[] attributes, DTMXPathNamespaceNode[] namespaces, Hashtable idTable)
		{
			this.root = new DTMXPathNavigator(this, nameTable, nodes, attributes, namespaces, idTable);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
		public XPathNavigator CreateNavigator()
		{
			return this.root.Clone();
		}

		// Token: 0x04000159 RID: 345
		private XPathNavigator root;
	}
}
