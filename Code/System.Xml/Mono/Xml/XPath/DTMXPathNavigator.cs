using System;
using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Mono.Xml.XPath
{
	// Token: 0x02000036 RID: 54
	internal class DTMXPathNavigator : XPathNavigator, IXmlLineInfo
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x0000F470 File Offset: 0x0000D670
		public DTMXPathNavigator(DTMXPathDocument document, XmlNameTable nameTable, DTMXPathLinkedNode[] nodes, DTMXPathAttributeNode[] attributes, DTMXPathNamespaceNode[] namespaces, Hashtable idTable)
		{
			this.nodes = nodes;
			this.attributes = attributes;
			this.namespaces = namespaces;
			this.idTable = idTable;
			this.nameTable = nameTable;
			this.MoveToRoot();
			this.document = document;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000F4AC File Offset: 0x0000D6AC
		public DTMXPathNavigator(DTMXPathNavigator org) : this(org.document, org.nameTable, org.nodes, org.attributes, org.namespaces, org.idTable)
		{
			this.currentIsNode = org.currentIsNode;
			this.currentIsAttr = org.currentIsAttr;
			this.currentNode = org.currentNode;
			this.currentAttr = org.currentAttr;
			this.currentNs = org.currentNs;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000F520 File Offset: 0x0000D720
		internal DTMXPathNavigator(XmlNameTable nt)
		{
			this.nameTable = nt;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000F530 File Offset: 0x0000D730
		int IXmlLineInfo.LineNumber
		{
			get
			{
				return (!this.currentIsAttr) ? this.nodes[this.currentNode].LineNumber : this.attributes[this.currentAttr].LineNumber;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000F57C File Offset: 0x0000D77C
		int IXmlLineInfo.LinePosition
		{
			get
			{
				return (!this.currentIsAttr) ? this.nodes[this.currentNode].LinePosition : this.attributes[this.currentAttr].LinePosition;
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000F5C8 File Offset: 0x0000D7C8
		bool IXmlLineInfo.HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000F5CC File Offset: 0x0000D7CC
		public override string BaseURI
		{
			get
			{
				return this.nodes[this.currentNode].BaseURI;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000F5E4 File Offset: 0x0000D7E4
		public override bool HasAttributes
		{
			get
			{
				return this.currentIsNode && this.nodes[this.currentNode].FirstAttribute != 0;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000F614 File Offset: 0x0000D814
		public override bool HasChildren
		{
			get
			{
				return this.currentIsNode && this.nodes[this.currentNode].FirstChild != 0;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000F644 File Offset: 0x0000D844
		public override bool IsEmptyElement
		{
			get
			{
				return this.currentIsNode && this.nodes[this.currentNode].IsEmptyElement;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000F670 File Offset: 0x0000D870
		public override string LocalName
		{
			get
			{
				if (this.currentIsNode)
				{
					return this.nodes[this.currentNode].LocalName;
				}
				if (this.currentIsAttr)
				{
					return this.attributes[this.currentAttr].LocalName;
				}
				return this.namespaces[this.currentNs].Name;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000F6D8 File Offset: 0x0000D8D8
		public override string Name
		{
			get
			{
				string prefix;
				string localName;
				if (this.currentIsNode)
				{
					prefix = this.nodes[this.currentNode].Prefix;
					localName = this.nodes[this.currentNode].LocalName;
				}
				else
				{
					if (!this.currentIsAttr)
					{
						return this.namespaces[this.currentNs].Name;
					}
					prefix = this.attributes[this.currentAttr].Prefix;
					localName = this.attributes[this.currentAttr].LocalName;
				}
				if (prefix != string.Empty)
				{
					return prefix + ':' + localName;
				}
				return localName;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000F798 File Offset: 0x0000D998
		public override string NamespaceURI
		{
			get
			{
				if (this.currentIsNode)
				{
					return this.nodes[this.currentNode].NamespaceURI;
				}
				if (this.currentIsAttr)
				{
					return this.attributes[this.currentAttr].NamespaceURI;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
		public override XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000F7F8 File Offset: 0x0000D9F8
		public override XPathNodeType NodeType
		{
			get
			{
				if (this.currentIsNode)
				{
					return this.nodes[this.currentNode].NodeType;
				}
				if (this.currentIsAttr)
				{
					return XPathNodeType.Attribute;
				}
				return XPathNodeType.Namespace;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000F838 File Offset: 0x0000DA38
		public override string Prefix
		{
			get
			{
				if (this.currentIsNode)
				{
					return this.nodes[this.currentNode].Prefix;
				}
				if (this.currentIsAttr)
				{
					return this.attributes[this.currentAttr].Prefix;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000F890 File Offset: 0x0000DA90
		public override string Value
		{
			get
			{
				if (this.currentIsAttr)
				{
					return this.attributes[this.currentAttr].Value;
				}
				if (!this.currentIsNode)
				{
					return this.namespaces[this.currentNs].Namespace;
				}
				switch (this.nodes[this.currentNode].NodeType)
				{
				case XPathNodeType.Text:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
				case XPathNodeType.ProcessingInstruction:
				case XPathNodeType.Comment:
					return this.nodes[this.currentNode].Value;
				default:
				{
					int i = this.nodes[this.currentNode].FirstChild;
					if (i == 0)
					{
						return string.Empty;
					}
					if (this.valueBuilder == null)
					{
						this.valueBuilder = new StringBuilder();
					}
					else
					{
						this.valueBuilder.Length = 0;
					}
					int num = this.nodes[this.currentNode].NextSibling;
					if (num == 0)
					{
						int parent = this.currentNode;
						do
						{
							parent = this.nodes[parent].Parent;
							num = this.nodes[parent].NextSibling;
						}
						while (num == 0 && parent != 0);
						if (num == 0)
						{
							num = this.nodes.Length;
						}
					}
					while (i < num)
					{
						switch (this.nodes[i].NodeType)
						{
						case XPathNodeType.Text:
						case XPathNodeType.SignificantWhitespace:
						case XPathNodeType.Whitespace:
							this.valueBuilder.Append(this.nodes[i].Value);
							break;
						}
						i++;
					}
					return this.valueBuilder.ToString();
				}
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000FA48 File Offset: 0x0000DC48
		public override string XmlLang
		{
			get
			{
				return this.nodes[this.currentNode].XmlLang;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000FA60 File Offset: 0x0000DC60
		public override XPathNavigator Clone()
		{
			return new DTMXPathNavigator(this);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000FA68 File Offset: 0x0000DC68
		public override XmlNodeOrder ComparePosition(XPathNavigator nav)
		{
			DTMXPathNavigator dtmxpathNavigator = nav as DTMXPathNavigator;
			if (dtmxpathNavigator == null || dtmxpathNavigator.document != this.document)
			{
				return XmlNodeOrder.Unknown;
			}
			if (this.currentNode > dtmxpathNavigator.currentNode)
			{
				return XmlNodeOrder.After;
			}
			if (this.currentNode < dtmxpathNavigator.currentNode)
			{
				return XmlNodeOrder.Before;
			}
			if (dtmxpathNavigator.currentIsAttr)
			{
				if (!this.currentIsAttr)
				{
					return XmlNodeOrder.Before;
				}
				if (this.currentAttr > dtmxpathNavigator.currentAttr)
				{
					return XmlNodeOrder.After;
				}
				if (this.currentAttr < dtmxpathNavigator.currentAttr)
				{
					return XmlNodeOrder.Before;
				}
				return XmlNodeOrder.Same;
			}
			else
			{
				if (dtmxpathNavigator.currentIsNode)
				{
					return dtmxpathNavigator.currentIsNode ? XmlNodeOrder.Same : XmlNodeOrder.Before;
				}
				if (this.currentIsNode)
				{
					return XmlNodeOrder.Before;
				}
				if (this.currentNs > dtmxpathNavigator.currentNs)
				{
					return XmlNodeOrder.After;
				}
				if (this.currentNs < dtmxpathNavigator.currentNs)
				{
					return XmlNodeOrder.Before;
				}
				return XmlNodeOrder.Same;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000FB50 File Offset: 0x0000DD50
		private int findAttribute(string localName, string namespaceURI)
		{
			if (this.currentIsNode && this.nodes[this.currentNode].NodeType == XPathNodeType.Element)
			{
				for (int num = this.nodes[this.currentNode].FirstAttribute; num != 0; num = this.attributes[num].NextAttribute)
				{
					if (this.attributes[num].LocalName == localName && this.attributes[num].NamespaceURI == namespaceURI)
					{
						return num;
					}
				}
			}
			return 0;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000FBF4 File Offset: 0x0000DDF4
		public override string GetAttribute(string localName, string namespaceURI)
		{
			int num = this.findAttribute(localName, namespaceURI);
			return (num == 0) ? string.Empty : this.attributes[num].Value;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		public override string GetNamespace(string name)
		{
			if (this.currentIsNode && this.nodes[this.currentNode].NodeType == XPathNodeType.Element)
			{
				for (int num = this.nodes[this.currentNode].FirstNamespace; num != 0; num = this.namespaces[num].NextNamespace)
				{
					if (this.namespaces[num].Name == name)
					{
						return this.namespaces[num].Namespace;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000FCC8 File Offset: 0x0000DEC8
		public override bool IsDescendant(XPathNavigator nav)
		{
			DTMXPathNavigator dtmxpathNavigator = nav as DTMXPathNavigator;
			if (dtmxpathNavigator == null || dtmxpathNavigator.document != this.document)
			{
				return false;
			}
			if (dtmxpathNavigator.currentNode == this.currentNode)
			{
				return !dtmxpathNavigator.currentIsNode;
			}
			for (int parent = this.nodes[dtmxpathNavigator.currentNode].Parent; parent != 0; parent = this.nodes[parent].Parent)
			{
				if (parent == this.currentNode)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000FD54 File Offset: 0x0000DF54
		public override bool IsSamePosition(XPathNavigator other)
		{
			DTMXPathNavigator dtmxpathNavigator = other as DTMXPathNavigator;
			if (dtmxpathNavigator == null || dtmxpathNavigator.document != this.document)
			{
				return false;
			}
			if (this.currentNode != dtmxpathNavigator.currentNode || this.currentIsAttr != dtmxpathNavigator.currentIsAttr || this.currentIsNode != dtmxpathNavigator.currentIsNode)
			{
				return false;
			}
			if (this.currentIsAttr)
			{
				return this.currentAttr == dtmxpathNavigator.currentAttr;
			}
			return this.currentIsNode || this.currentNs == dtmxpathNavigator.currentNs;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		public override bool MoveTo(XPathNavigator other)
		{
			DTMXPathNavigator dtmxpathNavigator = other as DTMXPathNavigator;
			if (dtmxpathNavigator == null || dtmxpathNavigator.document != this.document)
			{
				return false;
			}
			this.currentNode = dtmxpathNavigator.currentNode;
			this.currentAttr = dtmxpathNavigator.currentAttr;
			this.currentNs = dtmxpathNavigator.currentNs;
			this.currentIsNode = dtmxpathNavigator.currentIsNode;
			this.currentIsAttr = dtmxpathNavigator.currentIsAttr;
			return true;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000FE58 File Offset: 0x0000E058
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			int num = this.findAttribute(localName, namespaceURI);
			if (num == 0)
			{
				return false;
			}
			this.currentAttr = num;
			this.currentIsAttr = true;
			this.currentIsNode = false;
			return true;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000FE8C File Offset: 0x0000E08C
		public override bool MoveToFirst()
		{
			if (this.currentIsAttr)
			{
				return false;
			}
			int num = this.nodes[this.currentNode].PreviousSibling;
			if (num == 0)
			{
				return false;
			}
			for (int num2 = num; num2 != 0; num2 = this.nodes[num].PreviousSibling)
			{
				num = num2;
			}
			this.currentNode = num;
			this.currentIsNode = true;
			return true;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000FEF8 File Offset: 0x0000E0F8
		public override bool MoveToFirstAttribute()
		{
			if (!this.currentIsNode)
			{
				return false;
			}
			int firstAttribute = this.nodes[this.currentNode].FirstAttribute;
			if (firstAttribute == 0)
			{
				return false;
			}
			this.currentAttr = firstAttribute;
			this.currentIsAttr = true;
			this.currentIsNode = false;
			return true;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000FF48 File Offset: 0x0000E148
		public override bool MoveToFirstChild()
		{
			if (!this.currentIsNode)
			{
				return false;
			}
			int firstChild = this.nodes[this.currentNode].FirstChild;
			if (firstChild == 0)
			{
				return false;
			}
			this.currentNode = firstChild;
			return true;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000FF8C File Offset: 0x0000E18C
		private bool moveToSpecifiedNamespace(int cur, XPathNamespaceScope namespaceScope)
		{
			if (cur == 0)
			{
				return false;
			}
			if (namespaceScope == XPathNamespaceScope.Local && this.namespaces[cur].DeclaredElement != this.currentNode)
			{
				return false;
			}
			if (namespaceScope != XPathNamespaceScope.All && this.namespaces[cur].Namespace == "http://www.w3.org/XML/1998/namespace")
			{
				return false;
			}
			if (cur != 0)
			{
				this.moveToNamespace(cur);
				return true;
			}
			return false;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00010000 File Offset: 0x0000E200
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			if (!this.currentIsNode)
			{
				return false;
			}
			int firstNamespace = this.nodes[this.currentNode].FirstNamespace;
			return this.moveToSpecifiedNamespace(firstNamespace, namespaceScope);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0001003C File Offset: 0x0000E23C
		public override bool MoveToId(string id)
		{
			if (this.idTable.ContainsKey(id))
			{
				this.currentNode = (int)this.idTable[id];
				this.currentIsNode = true;
				this.currentIsAttr = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00010078 File Offset: 0x0000E278
		private void moveToNamespace(int nsNode)
		{
			this.currentIsNode = (this.currentIsAttr = false);
			this.currentNs = nsNode;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0001009C File Offset: 0x0000E29C
		public override bool MoveToNamespace(string name)
		{
			int num = this.nodes[this.currentNode].FirstNamespace;
			if (num == 0)
			{
				return false;
			}
			while (num != 0)
			{
				if (this.namespaces[num].Name == name)
				{
					this.moveToNamespace(num);
					return true;
				}
				num = this.namespaces[num].NextNamespace;
			}
			return false;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0001010C File Offset: 0x0000E30C
		public override bool MoveToNext()
		{
			if (this.currentIsAttr)
			{
				return false;
			}
			int nextSibling = this.nodes[this.currentNode].NextSibling;
			if (nextSibling == 0)
			{
				return false;
			}
			this.currentNode = nextSibling;
			this.currentIsNode = true;
			return true;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00010154 File Offset: 0x0000E354
		public override bool MoveToNextAttribute()
		{
			if (!this.currentIsAttr)
			{
				return false;
			}
			int nextAttribute = this.attributes[this.currentAttr].NextAttribute;
			if (nextAttribute == 0)
			{
				return false;
			}
			this.currentAttr = nextAttribute;
			return true;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00010198 File Offset: 0x0000E398
		public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			if (this.currentIsAttr || this.currentIsNode)
			{
				return false;
			}
			int nextNamespace = this.namespaces[this.currentNs].NextNamespace;
			return this.moveToSpecifiedNamespace(nextNamespace, namespaceScope);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000101DC File Offset: 0x0000E3DC
		public override bool MoveToParent()
		{
			if (!this.currentIsNode)
			{
				this.currentIsNode = true;
				this.currentIsAttr = false;
				return true;
			}
			int parent = this.nodes[this.currentNode].Parent;
			if (parent == 0)
			{
				return false;
			}
			this.currentNode = parent;
			return true;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0001022C File Offset: 0x0000E42C
		public override bool MoveToPrevious()
		{
			if (this.currentIsAttr)
			{
				return false;
			}
			int previousSibling = this.nodes[this.currentNode].PreviousSibling;
			if (previousSibling == 0)
			{
				return false;
			}
			this.currentNode = previousSibling;
			this.currentIsNode = true;
			return true;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00010274 File Offset: 0x0000E474
		public override void MoveToRoot()
		{
			this.currentNode = 1;
			this.currentIsNode = true;
			this.currentIsAttr = false;
		}

		// Token: 0x04000187 RID: 391
		private XmlNameTable nameTable;

		// Token: 0x04000188 RID: 392
		private DTMXPathDocument document;

		// Token: 0x04000189 RID: 393
		private DTMXPathLinkedNode[] nodes;

		// Token: 0x0400018A RID: 394
		private DTMXPathAttributeNode[] attributes;

		// Token: 0x0400018B RID: 395
		private DTMXPathNamespaceNode[] namespaces;

		// Token: 0x0400018C RID: 396
		private Hashtable idTable;

		// Token: 0x0400018D RID: 397
		private bool currentIsNode;

		// Token: 0x0400018E RID: 398
		private bool currentIsAttr;

		// Token: 0x0400018F RID: 399
		private int currentNode;

		// Token: 0x04000190 RID: 400
		private int currentAttr;

		// Token: 0x04000191 RID: 401
		private int currentNs;

		// Token: 0x04000192 RID: 402
		private StringBuilder valueBuilder;
	}
}
