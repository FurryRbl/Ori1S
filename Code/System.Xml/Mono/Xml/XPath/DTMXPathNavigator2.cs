using System;
using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Mono.Xml.XPath
{
	// Token: 0x0200003E RID: 62
	internal class DTMXPathNavigator2 : XPathNavigator, IXmlLineInfo
	{
		// Token: 0x06000262 RID: 610 RVA: 0x00012710 File Offset: 0x00010910
		public DTMXPathNavigator2(DTMXPathDocument2 document)
		{
			this.MoveToRoot();
			this.document = document;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00012728 File Offset: 0x00010928
		public DTMXPathNavigator2(DTMXPathNavigator2 org)
		{
			this.document = org.document;
			this.currentIsNode = org.currentIsNode;
			this.currentIsAttr = org.currentIsAttr;
			this.currentNode = org.currentNode;
			this.currentAttr = org.currentAttr;
			this.currentNs = org.currentNs;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00012784 File Offset: 0x00010984
		int IXmlLineInfo.LineNumber
		{
			get
			{
				return (!this.currentIsAttr) ? this.nodes[this.currentNode].LineNumber : this.attributes[this.currentAttr].LineNumber;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000127D0 File Offset: 0x000109D0
		int IXmlLineInfo.LinePosition
		{
			get
			{
				return (!this.currentIsAttr) ? this.nodes[this.currentNode].LinePosition : this.attributes[this.currentAttr].LinePosition;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0001281C File Offset: 0x00010A1C
		bool IXmlLineInfo.HasLineInfo()
		{
			return true;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00012820 File Offset: 0x00010A20
		private XmlNameTable nameTable
		{
			get
			{
				return this.document.NameTable;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00012830 File Offset: 0x00010A30
		private DTMXPathLinkedNode2[] nodes
		{
			get
			{
				return this.document.Nodes;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00012840 File Offset: 0x00010A40
		private DTMXPathAttributeNode2[] attributes
		{
			get
			{
				return this.document.Attributes;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00012850 File Offset: 0x00010A50
		private DTMXPathNamespaceNode2[] namespaces
		{
			get
			{
				return this.document.Namespaces;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00012860 File Offset: 0x00010A60
		private string[] atomicStringPool
		{
			get
			{
				return this.document.AtomicStringPool;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00012870 File Offset: 0x00010A70
		private string[] nonAtomicStringPool
		{
			get
			{
				return this.document.NonAtomicStringPool;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00012880 File Offset: 0x00010A80
		private Hashtable idTable
		{
			get
			{
				return this.document.IdTable;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00012890 File Offset: 0x00010A90
		public override string BaseURI
		{
			get
			{
				return this.atomicStringPool[this.nodes[this.currentNode].BaseURI];
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000128B0 File Offset: 0x00010AB0
		public override bool HasAttributes
		{
			get
			{
				return this.currentIsNode && this.nodes[this.currentNode].FirstAttribute != 0;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000270 RID: 624 RVA: 0x000128E0 File Offset: 0x00010AE0
		public override bool HasChildren
		{
			get
			{
				return this.currentIsNode && this.nodes[this.currentNode].FirstChild != 0;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00012910 File Offset: 0x00010B10
		public override bool IsEmptyElement
		{
			get
			{
				return this.currentIsNode && this.nodes[this.currentNode].IsEmptyElement;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0001293C File Offset: 0x00010B3C
		public override string LocalName
		{
			get
			{
				if (this.currentIsNode)
				{
					return this.atomicStringPool[this.nodes[this.currentNode].LocalName];
				}
				if (this.currentIsAttr)
				{
					return this.atomicStringPool[this.attributes[this.currentAttr].LocalName];
				}
				return this.atomicStringPool[this.namespaces[this.currentNs].Name];
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000273 RID: 627 RVA: 0x000129B8 File Offset: 0x00010BB8
		public override string Name
		{
			get
			{
				string text;
				string text2;
				if (this.currentIsNode)
				{
					text = this.atomicStringPool[this.nodes[this.currentNode].Prefix];
					text2 = this.atomicStringPool[this.nodes[this.currentNode].LocalName];
				}
				else
				{
					if (!this.currentIsAttr)
					{
						return this.atomicStringPool[this.namespaces[this.currentNs].Name];
					}
					text = this.atomicStringPool[this.attributes[this.currentAttr].Prefix];
					text2 = this.atomicStringPool[this.attributes[this.currentAttr].LocalName];
				}
				if (text != string.Empty)
				{
					return text + ':' + text2;
				}
				return text2;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00012A9C File Offset: 0x00010C9C
		public override string NamespaceURI
		{
			get
			{
				if (this.currentIsNode)
				{
					return this.atomicStringPool[this.nodes[this.currentNode].NamespaceURI];
				}
				if (this.currentIsAttr)
				{
					return this.atomicStringPool[this.attributes[this.currentAttr].NamespaceURI];
				}
				return string.Empty;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00012B00 File Offset: 0x00010D00
		public override XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00012B08 File Offset: 0x00010D08
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00012B48 File Offset: 0x00010D48
		public override string Prefix
		{
			get
			{
				if (this.currentIsNode)
				{
					return this.atomicStringPool[this.nodes[this.currentNode].Prefix];
				}
				if (this.currentIsAttr)
				{
					return this.atomicStringPool[this.attributes[this.currentAttr].Prefix];
				}
				return string.Empty;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00012BAC File Offset: 0x00010DAC
		public override string Value
		{
			get
			{
				if (this.currentIsAttr)
				{
					return this.nonAtomicStringPool[this.attributes[this.currentAttr].Value];
				}
				if (!this.currentIsNode)
				{
					return this.atomicStringPool[this.namespaces[this.currentNs].Namespace];
				}
				switch (this.nodes[this.currentNode].NodeType)
				{
				case XPathNodeType.Text:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
				case XPathNodeType.ProcessingInstruction:
				case XPathNodeType.Comment:
					return this.nonAtomicStringPool[this.nodes[this.currentNode].Value];
				default:
				{
					int firstChild = this.nodes[this.currentNode].FirstChild;
					if (firstChild == 0)
					{
						return string.Empty;
					}
					StringBuilder stringBuilder = null;
					this.BuildValue(firstChild, ref stringBuilder);
					return (stringBuilder != null) ? stringBuilder.ToString() : string.Empty;
				}
				}
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00012CA8 File Offset: 0x00010EA8
		private void BuildValue(int iter, ref StringBuilder valueBuilder)
		{
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
			while (iter < num)
			{
				switch (this.nodes[iter].NodeType)
				{
				case XPathNodeType.Text:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
					if (valueBuilder == null)
					{
						valueBuilder = new StringBuilder();
					}
					valueBuilder.Append(this.nonAtomicStringPool[this.nodes[iter].Value]);
					break;
				}
				iter++;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00012D88 File Offset: 0x00010F88
		public override string XmlLang
		{
			get
			{
				return this.atomicStringPool[this.nodes[this.currentNode].XmlLang];
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00012DA8 File Offset: 0x00010FA8
		public override XPathNavigator Clone()
		{
			return new DTMXPathNavigator2(this);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00012DB0 File Offset: 0x00010FB0
		public override XmlNodeOrder ComparePosition(XPathNavigator nav)
		{
			DTMXPathNavigator2 dtmxpathNavigator = nav as DTMXPathNavigator2;
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

		// Token: 0x0600027D RID: 637 RVA: 0x00012E98 File Offset: 0x00011098
		private int findAttribute(string localName, string namespaceURI)
		{
			if (this.currentIsNode && this.nodes[this.currentNode].NodeType == XPathNodeType.Element)
			{
				for (int num = this.nodes[this.currentNode].FirstAttribute; num != 0; num = this.attributes[num].NextAttribute)
				{
					if (this.atomicStringPool[this.attributes[num].LocalName] == localName && this.atomicStringPool[this.attributes[num].NamespaceURI] == namespaceURI)
					{
						return num;
					}
				}
			}
			return 0;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00012F4C File Offset: 0x0001114C
		public override string GetAttribute(string localName, string namespaceURI)
		{
			int num = this.findAttribute(localName, namespaceURI);
			return (num == 0) ? string.Empty : this.nonAtomicStringPool[this.attributes[num].Value];
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00012F8C File Offset: 0x0001118C
		public override string GetNamespace(string name)
		{
			if (this.currentIsNode && this.nodes[this.currentNode].NodeType == XPathNodeType.Element)
			{
				for (int num = this.nodes[this.currentNode].FirstNamespace; num != 0; num = this.namespaces[num].NextNamespace)
				{
					if (this.atomicStringPool[this.namespaces[num].Name] == name)
					{
						return this.atomicStringPool[this.namespaces[num].Namespace];
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00013038 File Offset: 0x00011238
		public override bool IsDescendant(XPathNavigator nav)
		{
			DTMXPathNavigator2 dtmxpathNavigator = nav as DTMXPathNavigator2;
			if (dtmxpathNavigator == null || dtmxpathNavigator.document != this.document)
			{
				return false;
			}
			if (dtmxpathNavigator.currentNode == this.currentNode)
			{
				return !dtmxpathNavigator.currentIsNode;
			}
			int parent = this.nodes[dtmxpathNavigator.currentNode].Parent;
			if (parent < this.currentNode)
			{
				return false;
			}
			while (parent != 0)
			{
				if (parent == this.currentNode)
				{
					return true;
				}
				parent = this.nodes[parent].Parent;
			}
			return false;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000130D4 File Offset: 0x000112D4
		public override bool IsSamePosition(XPathNavigator other)
		{
			DTMXPathNavigator2 dtmxpathNavigator = other as DTMXPathNavigator2;
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

		// Token: 0x06000282 RID: 642 RVA: 0x0001316C File Offset: 0x0001136C
		public override bool MoveTo(XPathNavigator other)
		{
			DTMXPathNavigator2 dtmxpathNavigator = other as DTMXPathNavigator2;
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

		// Token: 0x06000283 RID: 643 RVA: 0x000131D8 File Offset: 0x000113D8
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

		// Token: 0x06000284 RID: 644 RVA: 0x0001320C File Offset: 0x0001140C
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
			num = this.nodes[num].Parent;
			num = this.nodes[num].FirstChild;
			this.currentNode = num;
			this.currentIsNode = true;
			return true;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00013278 File Offset: 0x00011478
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

		// Token: 0x06000286 RID: 646 RVA: 0x000132C8 File Offset: 0x000114C8
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

		// Token: 0x06000287 RID: 647 RVA: 0x0001330C File Offset: 0x0001150C
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
			if (namespaceScope != XPathNamespaceScope.All && this.namespaces[cur].Namespace == 2)
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

		// Token: 0x06000288 RID: 648 RVA: 0x00013378 File Offset: 0x00011578
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			if (!this.currentIsNode)
			{
				return false;
			}
			int firstNamespace = this.nodes[this.currentNode].FirstNamespace;
			return this.moveToSpecifiedNamespace(firstNamespace, namespaceScope);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000133B4 File Offset: 0x000115B4
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

		// Token: 0x0600028A RID: 650 RVA: 0x000133FC File Offset: 0x000115FC
		private void moveToNamespace(int nsNode)
		{
			this.currentIsNode = (this.currentIsAttr = false);
			this.currentNs = nsNode;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00013420 File Offset: 0x00011620
		public override bool MoveToNamespace(string name)
		{
			int num = this.nodes[this.currentNode].FirstNamespace;
			if (num == 0)
			{
				return false;
			}
			while (num != 0)
			{
				if (this.atomicStringPool[this.namespaces[num].Name] == name)
				{
					this.moveToNamespace(num);
					return true;
				}
				num = this.namespaces[num].NextNamespace;
			}
			return false;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00013498 File Offset: 0x00011698
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

		// Token: 0x0600028D RID: 653 RVA: 0x000134E0 File Offset: 0x000116E0
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

		// Token: 0x0600028E RID: 654 RVA: 0x00013524 File Offset: 0x00011724
		public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			if (this.currentIsAttr || this.currentIsNode)
			{
				return false;
			}
			int nextNamespace = this.namespaces[this.currentNs].NextNamespace;
			return this.moveToSpecifiedNamespace(nextNamespace, namespaceScope);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00013568 File Offset: 0x00011768
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

		// Token: 0x06000290 RID: 656 RVA: 0x000135B8 File Offset: 0x000117B8
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

		// Token: 0x06000291 RID: 657 RVA: 0x00013600 File Offset: 0x00011800
		public override void MoveToRoot()
		{
			this.currentNode = 1;
			this.currentIsNode = true;
			this.currentIsAttr = false;
		}

		// Token: 0x040001F0 RID: 496
		private DTMXPathDocument2 document;

		// Token: 0x040001F1 RID: 497
		private bool currentIsNode;

		// Token: 0x040001F2 RID: 498
		private bool currentIsAttr;

		// Token: 0x040001F3 RID: 499
		private int currentNode;

		// Token: 0x040001F4 RID: 500
		private int currentAttr;

		// Token: 0x040001F5 RID: 501
		private int currentNs;
	}
}
