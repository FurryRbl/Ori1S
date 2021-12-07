using System;
using System.Collections;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000F2 RID: 242
	internal class XmlDocumentNavigator : XPathNavigator, IHasXmlNode
	{
		// Token: 0x06000930 RID: 2352 RVA: 0x00031AD8 File Offset: 0x0002FCD8
		internal XmlDocumentNavigator(XmlNode node)
		{
			this.node = node;
			if (node.NodeType == XmlNodeType.Attribute && node.NamespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				this.nsNode = (XmlAttribute)node;
				node = this.nsNode.OwnerElement;
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00031B2C File Offset: 0x0002FD2C
		XmlNode IHasXmlNode.GetNode()
		{
			return this.Node;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00031B34 File Offset: 0x0002FD34
		internal XmlDocument Document
		{
			get
			{
				return (this.node.NodeType != XmlNodeType.Document) ? this.node.OwnerDocument : (this.node as XmlDocument);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00031B64 File Offset: 0x0002FD64
		public override string BaseURI
		{
			get
			{
				return this.node.BaseURI;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x00031B74 File Offset: 0x0002FD74
		public override bool HasAttributes
		{
			get
			{
				if (this.NsNode != null)
				{
					return false;
				}
				XmlElement xmlElement = this.node as XmlElement;
				if (xmlElement == null || !xmlElement.HasAttributes)
				{
					return false;
				}
				for (int i = 0; i < this.node.Attributes.Count; i++)
				{
					if (this.node.Attributes[i].NamespaceURI != "http://www.w3.org/2000/xmlns/")
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00031BF8 File Offset: 0x0002FDF8
		public override bool HasChildren
		{
			get
			{
				if (this.NsNode != null)
				{
					return false;
				}
				XPathNodeType nodeType = this.NodeType;
				bool flag = nodeType == XPathNodeType.Root || nodeType == XPathNodeType.Element;
				return flag && this.GetFirstChild(this.node) != null;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00031C44 File Offset: 0x0002FE44
		public override bool IsEmptyElement
		{
			get
			{
				return this.NsNode == null && this.node.NodeType == XmlNodeType.Element && ((XmlElement)this.node).IsEmpty;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00031C84 File Offset: 0x0002FE84
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x00031C8C File Offset: 0x0002FE8C
		public XmlAttribute NsNode
		{
			get
			{
				return this.nsNode;
			}
			set
			{
				if (value == null)
				{
					this.iteratedNsNames = null;
				}
				else
				{
					if (this.iteratedNsNames == null)
					{
						this.iteratedNsNames = new ArrayList();
					}
					else if (this.iteratedNsNames.IsReadOnly)
					{
						this.iteratedNsNames = new ArrayList(this.iteratedNsNames);
					}
					this.iteratedNsNames.Add(value.Name);
				}
				this.nsNode = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00031D00 File Offset: 0x0002FF00
		public override string LocalName
		{
			get
			{
				XmlAttribute xmlAttribute = this.NsNode;
				if (xmlAttribute == null)
				{
					XPathNodeType nodeType = this.NodeType;
					bool flag = nodeType == XPathNodeType.Element || nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.ProcessingInstruction || nodeType == XPathNodeType.Namespace;
					return (!flag) ? string.Empty : this.node.LocalName;
				}
				if (xmlAttribute == this.Document.NsNodeXml)
				{
					return "xml";
				}
				return (!(xmlAttribute.Name == "xmlns")) ? xmlAttribute.LocalName : string.Empty;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00031D98 File Offset: 0x0002FF98
		public override string Name
		{
			get
			{
				if (this.NsNode != null)
				{
					return this.LocalName;
				}
				XPathNodeType nodeType = this.NodeType;
				bool flag = nodeType == XPathNodeType.Element || nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.ProcessingInstruction || nodeType == XPathNodeType.Namespace;
				return (!flag) ? string.Empty : this.node.Name;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00031DF8 File Offset: 0x0002FFF8
		public override string NamespaceURI
		{
			get
			{
				return (this.NsNode == null) ? this.node.NamespaceURI : string.Empty;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00031E28 File Offset: 0x00030028
		public override XmlNameTable NameTable
		{
			get
			{
				return this.Document.NameTable;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00031E38 File Offset: 0x00030038
		public override XPathNodeType NodeType
		{
			get
			{
				if (this.NsNode != null)
				{
					return XPathNodeType.Namespace;
				}
				XmlNode xmlNode = this.node;
				bool flag = false;
				for (;;)
				{
					XmlNodeType nodeType = xmlNode.NodeType;
					if (nodeType == XmlNodeType.Text || nodeType == XmlNodeType.CDATA)
					{
						break;
					}
					if (nodeType != XmlNodeType.Whitespace)
					{
						if (nodeType != XmlNodeType.SignificantWhitespace)
						{
							xmlNode = null;
						}
						else
						{
							flag = true;
							xmlNode = this.GetNextSibling(xmlNode);
						}
					}
					else
					{
						xmlNode = this.GetNextSibling(xmlNode);
					}
					if (xmlNode == null)
					{
						goto Block_6;
					}
				}
				return XPathNodeType.Text;
				Block_6:
				return (!flag) ? this.node.XPathNodeType : XPathNodeType.SignificantWhitespace;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00031EC8 File Offset: 0x000300C8
		public override string Prefix
		{
			get
			{
				return (this.NsNode == null) ? this.node.Prefix : string.Empty;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00031EF8 File Offset: 0x000300F8
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				IXmlSchemaInfo result;
				if (this.NsNode != null)
				{
					IXmlSchemaInfo xmlSchemaInfo = null;
					result = xmlSchemaInfo;
				}
				else
				{
					result = this.node.SchemaInfo;
				}
				return result;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00031F24 File Offset: 0x00030124
		public override object UnderlyingObject
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00031F2C File Offset: 0x0003012C
		public override string Value
		{
			get
			{
				switch (this.NodeType)
				{
				case XPathNodeType.Root:
				case XPathNodeType.Element:
					return this.node.InnerText;
				case XPathNodeType.Attribute:
				case XPathNodeType.ProcessingInstruction:
				case XPathNodeType.Comment:
					return this.node.Value;
				case XPathNodeType.Namespace:
					return (this.NsNode != this.Document.NsNodeXml) ? this.NsNode.Value : "http://www.w3.org/XML/1998/namespace";
				case XPathNodeType.Text:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
				{
					string text = this.node.Value;
					XmlNode nextSibling = this.GetNextSibling(this.node);
					while (nextSibling != null)
					{
						switch (nextSibling.XPathNodeType)
						{
						case XPathNodeType.Text:
						case XPathNodeType.SignificantWhitespace:
						case XPathNodeType.Whitespace:
							text += nextSibling.Value;
							nextSibling = this.GetNextSibling(nextSibling);
							break;
						default:
							return text;
						}
					}
					return text;
				}
				default:
					return string.Empty;
				}
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00032020 File Offset: 0x00030220
		public override string XmlLang
		{
			get
			{
				return this.node.XmlLang;
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00032030 File Offset: 0x00030230
		private bool CheckNsNameAppearance(string name, string ns)
		{
			if (this.iteratedNsNames != null && this.iteratedNsNames.Contains(name))
			{
				return true;
			}
			if (ns == string.Empty)
			{
				if (this.iteratedNsNames == null)
				{
					this.iteratedNsNames = new ArrayList();
				}
				else if (this.iteratedNsNames.IsReadOnly)
				{
					this.iteratedNsNames = new ArrayList(this.iteratedNsNames);
				}
				this.iteratedNsNames.Add("xmlns");
				return true;
			}
			return false;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x000320BC File Offset: 0x000302BC
		public override XPathNavigator Clone()
		{
			return new XmlDocumentNavigator(this.node)
			{
				nsNode = this.nsNode,
				iteratedNsNames = ((this.iteratedNsNames != null && !this.iteratedNsNames.IsReadOnly) ? ArrayList.ReadOnly(this.iteratedNsNames) : this.iteratedNsNames)
			};
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0003211C File Offset: 0x0003031C
		public override string GetAttribute(string localName, string namespaceURI)
		{
			if (this.HasAttributes)
			{
				XmlElement xmlElement = this.Node as XmlElement;
				return (xmlElement == null) ? string.Empty : xmlElement.GetAttribute(localName, namespaceURI);
			}
			return string.Empty;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00032160 File Offset: 0x00030360
		public override string GetNamespace(string name)
		{
			return this.Node.GetNamespaceOfPrefix(name);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00032170 File Offset: 0x00030370
		public override bool IsDescendant(XPathNavigator other)
		{
			if (this.NsNode != null)
			{
				return false;
			}
			XmlDocumentNavigator xmlDocumentNavigator = other as XmlDocumentNavigator;
			if (xmlDocumentNavigator == null)
			{
				return false;
			}
			for (XmlNode xmlNode = (xmlDocumentNavigator.node.NodeType != XmlNodeType.Attribute) ? xmlDocumentNavigator.node.ParentNode : ((XmlAttribute)xmlDocumentNavigator.node).OwnerElement; xmlNode != null; xmlNode = xmlNode.ParentNode)
			{
				if (xmlNode == this.node)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x000321EC File Offset: 0x000303EC
		public override bool IsSamePosition(XPathNavigator other)
		{
			XmlDocumentNavigator xmlDocumentNavigator = other as XmlDocumentNavigator;
			return xmlDocumentNavigator != null && this.node == xmlDocumentNavigator.node && this.NsNode == xmlDocumentNavigator.NsNode;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0003222C File Offset: 0x0003042C
		public override bool MoveTo(XPathNavigator other)
		{
			XmlDocumentNavigator xmlDocumentNavigator = other as XmlDocumentNavigator;
			if (xmlDocumentNavigator != null && this.Document == xmlDocumentNavigator.Document)
			{
				this.node = xmlDocumentNavigator.node;
				this.NsNode = xmlDocumentNavigator.NsNode;
				return true;
			}
			return false;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00032274 File Offset: 0x00030474
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			if (this.HasAttributes)
			{
				XmlAttribute xmlAttribute = this.node.Attributes[localName, namespaceURI];
				if (xmlAttribute != null)
				{
					this.node = xmlAttribute;
					this.NsNode = null;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000322B8 File Offset: 0x000304B8
		public override bool MoveToFirstAttribute()
		{
			if (this.NodeType == XPathNodeType.Element)
			{
				XmlElement xmlElement = this.node as XmlElement;
				if (!xmlElement.HasAttributes)
				{
					return false;
				}
				for (int i = 0; i < this.node.Attributes.Count; i++)
				{
					XmlAttribute xmlAttribute = this.node.Attributes[i];
					if (xmlAttribute.NamespaceURI != "http://www.w3.org/2000/xmlns/")
					{
						this.node = xmlAttribute;
						this.NsNode = null;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00032344 File Offset: 0x00030544
		public override bool MoveToFirstChild()
		{
			if (!this.HasChildren)
			{
				return false;
			}
			XmlNode firstChild = this.GetFirstChild(this.node);
			if (firstChild == null)
			{
				return false;
			}
			this.node = firstChild;
			return true;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0003237C File Offset: 0x0003057C
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			if (this.NodeType != XPathNodeType.Element)
			{
				return false;
			}
			XmlElement xmlElement = this.node as XmlElement;
			XmlAttribute xmlAttribute;
			for (;;)
			{
				if (xmlElement.HasAttributes)
				{
					for (int i = 0; i < xmlElement.Attributes.Count; i++)
					{
						xmlAttribute = xmlElement.Attributes[i];
						if (xmlAttribute.NamespaceURI == "http://www.w3.org/2000/xmlns/")
						{
							if (!this.CheckNsNameAppearance(xmlAttribute.Name, xmlAttribute.Value))
							{
								goto IL_6A;
							}
						}
					}
				}
				if (namespaceScope == XPathNamespaceScope.Local)
				{
					return false;
				}
				xmlElement = (this.GetParentNode(xmlElement) as XmlElement);
				if (xmlElement == null)
				{
					goto Block_6;
				}
			}
			IL_6A:
			this.NsNode = xmlAttribute;
			return true;
			Block_6:
			if (namespaceScope != XPathNamespaceScope.All)
			{
				return false;
			}
			if (this.CheckNsNameAppearance(this.Document.NsNodeXml.Name, this.Document.NsNodeXml.Value))
			{
				return false;
			}
			this.NsNode = this.Document.NsNodeXml;
			return true;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00032474 File Offset: 0x00030674
		public override bool MoveToId(string id)
		{
			XmlElement elementById = this.Document.GetElementById(id);
			if (elementById == null)
			{
				return false;
			}
			this.node = elementById;
			return true;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x000324A0 File Offset: 0x000306A0
		public override bool MoveToNamespace(string name)
		{
			if (name == "xml")
			{
				this.NsNode = this.Document.NsNodeXml;
				return true;
			}
			if (this.NodeType != XPathNodeType.Element)
			{
				return false;
			}
			XmlElement xmlElement = this.node as XmlElement;
			XmlAttribute xmlAttribute;
			for (;;)
			{
				if (xmlElement.HasAttributes)
				{
					for (int i = 0; i < xmlElement.Attributes.Count; i++)
					{
						xmlAttribute = xmlElement.Attributes[i];
						if (xmlAttribute.NamespaceURI == "http://www.w3.org/2000/xmlns/" && xmlAttribute.Name == name)
						{
							goto Block_5;
						}
					}
				}
				xmlElement = (this.GetParentNode(this.node) as XmlElement);
				if (xmlElement == null)
				{
					return false;
				}
			}
			Block_5:
			this.NsNode = xmlAttribute;
			return true;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00032568 File Offset: 0x00030768
		public override bool MoveToNext()
		{
			if (this.NsNode != null)
			{
				return false;
			}
			XmlNode nextSibling = this.node;
			if (this.NodeType == XPathNodeType.Text)
			{
				for (;;)
				{
					nextSibling = this.GetNextSibling(nextSibling);
					if (nextSibling == null)
					{
						break;
					}
					XmlNodeType nodeType = nextSibling.NodeType;
					if (nodeType != XmlNodeType.Text && nodeType != XmlNodeType.CDATA && nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.SignificantWhitespace)
					{
						goto Block_6;
					}
				}
				return false;
				Block_6:;
			}
			else
			{
				nextSibling = this.GetNextSibling(nextSibling);
			}
			if (nextSibling == null)
			{
				return false;
			}
			this.node = nextSibling;
			return true;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00032600 File Offset: 0x00030800
		public override bool MoveToNextAttribute()
		{
			if (this.node == null)
			{
				return false;
			}
			if (this.NodeType != XPathNodeType.Attribute)
			{
				return false;
			}
			int i = 0;
			XmlElement ownerElement = ((XmlAttribute)this.node).OwnerElement;
			if (ownerElement == null)
			{
				return false;
			}
			int count = ownerElement.Attributes.Count;
			while (i < count)
			{
				if (ownerElement.Attributes[i] == this.node)
				{
					break;
				}
				i++;
			}
			if (i == count)
			{
				return false;
			}
			for (i++; i < count; i++)
			{
				if (ownerElement.Attributes[i].NamespaceURI != "http://www.w3.org/2000/xmlns/")
				{
					this.node = ownerElement.Attributes[i];
					this.NsNode = null;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x000326D4 File Offset: 0x000308D4
		public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			if (this.NsNode == this.Document.NsNodeXml)
			{
				return false;
			}
			if (this.NsNode == null)
			{
				return false;
			}
			int i = 0;
			XmlElement xmlElement = this.NsNode.OwnerElement;
			if (xmlElement == null)
			{
				return false;
			}
			int count = xmlElement.Attributes.Count;
			while (i < count)
			{
				if (xmlElement.Attributes[i] == this.NsNode)
				{
					break;
				}
				i++;
			}
			if (i == count)
			{
				return false;
			}
			for (i++; i < count; i++)
			{
				if (xmlElement.Attributes[i].NamespaceURI == "http://www.w3.org/2000/xmlns/")
				{
					XmlAttribute xmlAttribute = xmlElement.Attributes[i];
					if (!this.CheckNsNameAppearance(xmlAttribute.Name, xmlAttribute.Value))
					{
						this.NsNode = xmlAttribute;
						return true;
					}
				}
			}
			if (namespaceScope == XPathNamespaceScope.Local)
			{
				return false;
			}
			for (xmlElement = (this.GetParentNode(xmlElement) as XmlElement); xmlElement != null; xmlElement = (this.GetParentNode(xmlElement) as XmlElement))
			{
				if (xmlElement.HasAttributes)
				{
					for (int j = 0; j < xmlElement.Attributes.Count; j++)
					{
						XmlAttribute xmlAttribute2 = xmlElement.Attributes[j];
						if (xmlAttribute2.NamespaceURI == "http://www.w3.org/2000/xmlns/")
						{
							if (!this.CheckNsNameAppearance(xmlAttribute2.Name, xmlAttribute2.Value))
							{
								this.NsNode = xmlAttribute2;
								return true;
							}
						}
					}
				}
			}
			if (namespaceScope != XPathNamespaceScope.All)
			{
				return false;
			}
			if (this.CheckNsNameAppearance(this.Document.NsNodeXml.Name, this.Document.NsNodeXml.Value))
			{
				return false;
			}
			this.NsNode = this.Document.NsNodeXml;
			return true;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x000328B0 File Offset: 0x00030AB0
		public override bool MoveToParent()
		{
			if (this.NsNode != null)
			{
				this.NsNode = null;
				return true;
			}
			if (this.node.NodeType == XmlNodeType.Attribute)
			{
				XmlElement ownerElement = ((XmlAttribute)this.node).OwnerElement;
				if (ownerElement != null)
				{
					this.node = ownerElement;
					this.NsNode = null;
					return true;
				}
				return false;
			}
			else
			{
				XmlNode parentNode = this.GetParentNode(this.node);
				if (parentNode == null)
				{
					return false;
				}
				this.node = parentNode;
				this.NsNode = null;
				return true;
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00032930 File Offset: 0x00030B30
		public override bool MoveToPrevious()
		{
			if (this.NsNode != null)
			{
				return false;
			}
			XmlNode previousSibling = this.GetPreviousSibling(this.node);
			if (previousSibling == null)
			{
				return false;
			}
			this.node = previousSibling;
			return true;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00032968 File Offset: 0x00030B68
		public override void MoveToRoot()
		{
			XmlAttribute xmlAttribute = this.node as XmlAttribute;
			XmlNode xmlNode = (xmlAttribute == null) ? this.node : xmlAttribute.OwnerElement;
			if (xmlNode == null)
			{
				return;
			}
			for (XmlNode parentNode = this.GetParentNode(xmlNode); parentNode != null; parentNode = this.GetParentNode(parentNode))
			{
				xmlNode = parentNode;
			}
			this.node = xmlNode;
			this.NsNode = null;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x000329CC File Offset: 0x00030BCC
		private XmlNode Node
		{
			get
			{
				return (this.NsNode == null) ? this.node : this.NsNode;
			}
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000329EC File Offset: 0x00030BEC
		private XmlNode GetFirstChild(XmlNode n)
		{
			if (n.FirstChild == null)
			{
				return null;
			}
			XmlNodeType nodeType = n.FirstChild.NodeType;
			if (nodeType == XmlNodeType.EntityReference)
			{
				foreach (object obj in n.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.NodeType != XmlNodeType.EntityReference)
					{
						return xmlNode;
					}
					XmlNode firstChild = this.GetFirstChild(xmlNode);
					if (firstChild != null)
					{
						return firstChild;
					}
				}
				return null;
			}
			if (nodeType != XmlNodeType.DocumentType && nodeType != XmlNodeType.XmlDeclaration)
			{
				return n.FirstChild;
			}
			return this.GetNextSibling(n.FirstChild);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00032AD4 File Offset: 0x00030CD4
		private XmlNode GetLastChild(XmlNode n)
		{
			if (n.LastChild == null)
			{
				return null;
			}
			XmlNodeType nodeType = n.LastChild.NodeType;
			if (nodeType == XmlNodeType.EntityReference)
			{
				for (XmlNode xmlNode = n.LastChild; xmlNode != null; xmlNode = xmlNode.PreviousSibling)
				{
					if (xmlNode.NodeType != XmlNodeType.EntityReference)
					{
						return xmlNode;
					}
					XmlNode lastChild = this.GetLastChild(xmlNode);
					if (lastChild != null)
					{
						return lastChild;
					}
				}
				return null;
			}
			if (nodeType != XmlNodeType.DocumentType && nodeType != XmlNodeType.XmlDeclaration)
			{
				return n.LastChild;
			}
			return this.GetPreviousSibling(n.LastChild);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00032B68 File Offset: 0x00030D68
		private XmlNode GetPreviousSibling(XmlNode n)
		{
			XmlNode previousSibling = n.PreviousSibling;
			if (previousSibling != null)
			{
				XmlNodeType nodeType = previousSibling.NodeType;
				if (nodeType != XmlNodeType.EntityReference)
				{
					if (nodeType != XmlNodeType.DocumentType && nodeType != XmlNodeType.XmlDeclaration)
					{
						return previousSibling;
					}
					return this.GetPreviousSibling(previousSibling);
				}
				else
				{
					XmlNode lastChild = this.GetLastChild(previousSibling);
					if (lastChild != null)
					{
						return lastChild;
					}
					return this.GetPreviousSibling(previousSibling);
				}
			}
			else
			{
				if (n.ParentNode == null || n.ParentNode.NodeType != XmlNodeType.EntityReference)
				{
					return null;
				}
				return this.GetPreviousSibling(n.ParentNode);
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00032BF4 File Offset: 0x00030DF4
		private XmlNode GetNextSibling(XmlNode n)
		{
			XmlNode nextSibling = n.NextSibling;
			if (nextSibling != null)
			{
				XmlNodeType nodeType = nextSibling.NodeType;
				if (nodeType != XmlNodeType.EntityReference)
				{
					if (nodeType != XmlNodeType.DocumentType && nodeType != XmlNodeType.XmlDeclaration)
					{
						return n.NextSibling;
					}
					return this.GetNextSibling(nextSibling);
				}
				else
				{
					XmlNode firstChild = this.GetFirstChild(nextSibling);
					if (firstChild != null)
					{
						return firstChild;
					}
					return this.GetNextSibling(nextSibling);
				}
			}
			else
			{
				if (n.ParentNode == null || n.ParentNode.NodeType != XmlNodeType.EntityReference)
				{
					return null;
				}
				return this.GetNextSibling(n.ParentNode);
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00032C84 File Offset: 0x00030E84
		private XmlNode GetParentNode(XmlNode n)
		{
			if (n.ParentNode == null)
			{
				return null;
			}
			for (XmlNode parentNode = n.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
			{
				if (parentNode.NodeType != XmlNodeType.EntityReference)
				{
					return parentNode;
				}
			}
			return null;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00032CC8 File Offset: 0x00030EC8
		public override string LookupNamespace(string prefix)
		{
			return base.LookupNamespace(prefix);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00032CD4 File Offset: 0x00030ED4
		public override string LookupPrefix(string namespaceUri)
		{
			return base.LookupPrefix(namespaceUri);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00032CE0 File Offset: 0x00030EE0
		public override bool MoveToChild(XPathNodeType type)
		{
			return base.MoveToChild(type);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00032CEC File Offset: 0x00030EEC
		public override bool MoveToChild(string localName, string namespaceURI)
		{
			return base.MoveToChild(localName, namespaceURI);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00032CF8 File Offset: 0x00030EF8
		public override bool MoveToNext(string localName, string namespaceURI)
		{
			return base.MoveToNext(localName, namespaceURI);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00032D04 File Offset: 0x00030F04
		public override bool MoveToNext(XPathNodeType type)
		{
			return base.MoveToNext(type);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00032D10 File Offset: 0x00030F10
		public override bool MoveToFollowing(string localName, string namespaceURI, XPathNavigator end)
		{
			return base.MoveToFollowing(localName, namespaceURI, end);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00032D1C File Offset: 0x00030F1C
		public override bool MoveToFollowing(XPathNodeType type, XPathNavigator end)
		{
			return base.MoveToFollowing(type, end);
		}

		// Token: 0x040004DB RID: 1243
		private const string Xmlns = "http://www.w3.org/2000/xmlns/";

		// Token: 0x040004DC RID: 1244
		private const string XmlnsXML = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x040004DD RID: 1245
		private XmlNode node;

		// Token: 0x040004DE RID: 1246
		private XmlAttribute nsNode;

		// Token: 0x040004DF RID: 1247
		private ArrayList iteratedNsNames;
	}
}
