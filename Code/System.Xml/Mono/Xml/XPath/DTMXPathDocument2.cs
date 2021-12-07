using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace Mono.Xml.XPath
{
	// Token: 0x0200003B RID: 59
	internal class DTMXPathDocument2 : IXPathNavigable
	{
		// Token: 0x06000211 RID: 529 RVA: 0x00010294 File Offset: 0x0000E494
		public DTMXPathDocument2(XmlNameTable nameTable, DTMXPathLinkedNode2[] nodes, DTMXPathAttributeNode2[] attributes, DTMXPathNamespaceNode2[] namespaces, string[] atomicStringPool, string[] nonAtomicStringPool, Hashtable idTable)
		{
			this.Nodes = nodes;
			this.Attributes = attributes;
			this.Namespaces = namespaces;
			this.AtomicStringPool = atomicStringPool;
			this.NonAtomicStringPool = nonAtomicStringPool;
			this.IdTable = idTable;
			this.NameTable = nameTable;
			this.root = new DTMXPathNavigator2(this);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000102E8 File Offset: 0x0000E4E8
		public XPathNavigator CreateNavigator()
		{
			return this.root.Clone();
		}

		// Token: 0x040001B3 RID: 435
		private readonly XPathNavigator root;

		// Token: 0x040001B4 RID: 436
		internal readonly XmlNameTable NameTable;

		// Token: 0x040001B5 RID: 437
		internal readonly DTMXPathLinkedNode2[] Nodes;

		// Token: 0x040001B6 RID: 438
		internal readonly DTMXPathAttributeNode2[] Attributes;

		// Token: 0x040001B7 RID: 439
		internal readonly DTMXPathNamespaceNode2[] Namespaces;

		// Token: 0x040001B8 RID: 440
		internal readonly string[] AtomicStringPool;

		// Token: 0x040001B9 RID: 441
		internal readonly string[] NonAtomicStringPool;

		// Token: 0x040001BA RID: 442
		internal readonly Hashtable IdTable;
	}
}
