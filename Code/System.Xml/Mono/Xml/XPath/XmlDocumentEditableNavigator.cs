using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

namespace Mono.Xml.XPath
{
	// Token: 0x0200004A RID: 74
	internal class XmlDocumentEditableNavigator : XPathNavigator, IHasXmlNode
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x000143F0 File Offset: 0x000125F0
		public XmlDocumentEditableNavigator(XPathEditableDocument doc)
		{
			this.document = doc;
			if (XmlDocumentEditableNavigator.isXmlDocumentNavigatorImpl)
			{
				this.navigator = new XmlDocumentNavigator(doc.Node);
			}
			else
			{
				this.navigator = doc.CreateNavigator();
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0001442C File Offset: 0x0001262C
		public XmlDocumentEditableNavigator(XmlDocumentEditableNavigator nav)
		{
			this.document = nav.document;
			this.navigator = nav.navigator.Clone();
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00014488 File Offset: 0x00012688
		public override string BaseURI
		{
			get
			{
				return this.navigator.BaseURI;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00014498 File Offset: 0x00012698
		public override bool CanEdit
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0001449C File Offset: 0x0001269C
		public override bool IsEmptyElement
		{
			get
			{
				return this.navigator.IsEmptyElement;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002EA RID: 746 RVA: 0x000144AC File Offset: 0x000126AC
		public override string LocalName
		{
			get
			{
				return this.navigator.LocalName;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002EB RID: 747 RVA: 0x000144BC File Offset: 0x000126BC
		public override XmlNameTable NameTable
		{
			get
			{
				return this.navigator.NameTable;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000144CC File Offset: 0x000126CC
		public override string Name
		{
			get
			{
				return this.navigator.Name;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000144DC File Offset: 0x000126DC
		public override string NamespaceURI
		{
			get
			{
				return this.navigator.NamespaceURI;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000144EC File Offset: 0x000126EC
		public override XPathNodeType NodeType
		{
			get
			{
				return this.navigator.NodeType;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002EF RID: 751 RVA: 0x000144FC File Offset: 0x000126FC
		public override string Prefix
		{
			get
			{
				return this.navigator.Prefix;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0001450C File Offset: 0x0001270C
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.navigator.SchemaInfo;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0001451C File Offset: 0x0001271C
		public override object UnderlyingObject
		{
			get
			{
				return this.navigator.UnderlyingObject;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0001452C File Offset: 0x0001272C
		public override string Value
		{
			get
			{
				return this.navigator.Value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0001453C File Offset: 0x0001273C
		public override string XmlLang
		{
			get
			{
				return this.navigator.XmlLang;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0001454C File Offset: 0x0001274C
		public override bool HasChildren
		{
			get
			{
				return this.navigator.HasChildren;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0001455C File Offset: 0x0001275C
		public override bool HasAttributes
		{
			get
			{
				return this.navigator.HasAttributes;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0001456C File Offset: 0x0001276C
		public override XPathNavigator Clone()
		{
			return new XmlDocumentEditableNavigator(this);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00014574 File Offset: 0x00012774
		public override XPathNavigator CreateNavigator()
		{
			return this.navigator.Clone();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00014584 File Offset: 0x00012784
		public XmlNode GetNode()
		{
			return ((IHasXmlNode)this.navigator).GetNode();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00014598 File Offset: 0x00012798
		public override bool IsSamePosition(XPathNavigator other)
		{
			XmlDocumentEditableNavigator xmlDocumentEditableNavigator = other as XmlDocumentEditableNavigator;
			if (xmlDocumentEditableNavigator != null)
			{
				return this.navigator.IsSamePosition(xmlDocumentEditableNavigator.navigator);
			}
			return this.navigator.IsSamePosition(xmlDocumentEditableNavigator);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000145D0 File Offset: 0x000127D0
		public override bool MoveTo(XPathNavigator other)
		{
			XmlDocumentEditableNavigator xmlDocumentEditableNavigator = other as XmlDocumentEditableNavigator;
			if (xmlDocumentEditableNavigator != null)
			{
				return this.navigator.MoveTo(xmlDocumentEditableNavigator.navigator);
			}
			return this.navigator.MoveTo(xmlDocumentEditableNavigator);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00014608 File Offset: 0x00012808
		public override bool MoveToFirstAttribute()
		{
			return this.navigator.MoveToFirstAttribute();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00014618 File Offset: 0x00012818
		public override bool MoveToFirstChild()
		{
			return this.navigator.MoveToFirstChild();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00014628 File Offset: 0x00012828
		public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
		{
			return this.navigator.MoveToFirstNamespace(scope);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00014638 File Offset: 0x00012838
		public override bool MoveToId(string id)
		{
			return this.navigator.MoveToId(id);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00014648 File Offset: 0x00012848
		public override bool MoveToNext()
		{
			return this.navigator.MoveToNext();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00014658 File Offset: 0x00012858
		public override bool MoveToNextAttribute()
		{
			return this.navigator.MoveToNextAttribute();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00014668 File Offset: 0x00012868
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			return this.navigator.MoveToNextNamespace(scope);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00014678 File Offset: 0x00012878
		public override bool MoveToParent()
		{
			return this.navigator.MoveToParent();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00014688 File Offset: 0x00012888
		public override bool MoveToPrevious()
		{
			return this.navigator.MoveToPrevious();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00014698 File Offset: 0x00012898
		public override XmlWriter AppendChild()
		{
			XmlNode node = ((IHasXmlNode)this.navigator).GetNode();
			if (node == null)
			{
				throw new InvalidOperationException("Should not happen.");
			}
			return new XmlDocumentInsertionWriter(node, null);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000146D0 File Offset: 0x000128D0
		public override void DeleteRange(XPathNavigator lastSiblingToDelete)
		{
			if (lastSiblingToDelete == null)
			{
				throw new ArgumentNullException();
			}
			XmlNode node = ((IHasXmlNode)this.navigator).GetNode();
			XmlNode xmlNode = null;
			if (lastSiblingToDelete is IHasXmlNode)
			{
				xmlNode = ((IHasXmlNode)lastSiblingToDelete).GetNode();
			}
			if (!this.navigator.MoveToParent())
			{
				throw new InvalidOperationException("There is no parent to remove current node.");
			}
			if (xmlNode == null || node.ParentNode != xmlNode.ParentNode)
			{
				throw new InvalidOperationException("Argument XPathNavigator has different parent node.");
			}
			XmlNode parentNode = node.ParentNode;
			bool flag = true;
			XmlNode xmlNode2 = node;
			while (flag)
			{
				flag = (xmlNode2 != xmlNode);
				XmlNode nextSibling = xmlNode2.NextSibling;
				parentNode.RemoveChild(xmlNode2);
				xmlNode2 = nextSibling;
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00014788 File Offset: 0x00012988
		public override XmlWriter ReplaceRange(XPathNavigator nav)
		{
			if (nav == null)
			{
				throw new ArgumentNullException();
			}
			XmlNode start = ((IHasXmlNode)this.navigator).GetNode();
			XmlNode end = null;
			if (nav is IHasXmlNode)
			{
				end = ((IHasXmlNode)nav).GetNode();
			}
			if (end == null || start.ParentNode != end.ParentNode)
			{
				throw new InvalidOperationException("Argument XPathNavigator has different parent node.");
			}
			XmlDocumentInsertionWriter xmlDocumentInsertionWriter = (XmlDocumentInsertionWriter)this.InsertBefore();
			XPathNavigator prev = this.Clone();
			if (!prev.MoveToPrevious())
			{
				prev = null;
			}
			XPathNavigator parentNav = this.Clone();
			parentNav.MoveToParent();
			xmlDocumentInsertionWriter.Closed += delegate(XmlWriter writer)
			{
				XmlNode parentNode = start.ParentNode;
				bool flag = true;
				XmlNode xmlNode = start;
				while (flag)
				{
					flag = (xmlNode != end);
					XmlNode nextSibling = xmlNode.NextSibling;
					parentNode.RemoveChild(xmlNode);
					xmlNode = nextSibling;
				}
				if (prev != null)
				{
					this.MoveTo(prev);
					this.MoveToNext();
				}
				else
				{
					this.MoveTo(parentNav);
					this.MoveToFirstChild();
				}
			};
			return xmlDocumentInsertionWriter;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00014874 File Offset: 0x00012A74
		public override XmlWriter InsertBefore()
		{
			XmlNode node = ((IHasXmlNode)this.navigator).GetNode();
			return new XmlDocumentInsertionWriter(node.ParentNode, node);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000148A0 File Offset: 0x00012AA0
		public override XmlWriter CreateAttributes()
		{
			XmlNode node = ((IHasXmlNode)this.navigator).GetNode();
			return new XmlDocumentAttributeWriter(node);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000148C4 File Offset: 0x00012AC4
		public override void DeleteSelf()
		{
			XmlNode node = ((IHasXmlNode)this.navigator).GetNode();
			XmlAttribute xmlAttribute = node as XmlAttribute;
			if (xmlAttribute != null)
			{
				if (xmlAttribute.OwnerElement == null)
				{
					throw new InvalidOperationException("This attribute node cannot be removed since it has no owner element.");
				}
				this.navigator.MoveToParent();
				xmlAttribute.OwnerElement.RemoveAttributeNode(xmlAttribute);
			}
			else
			{
				if (node.ParentNode == null)
				{
					throw new InvalidOperationException("This node cannot be removed since it has no parent.");
				}
				this.navigator.MoveToParent();
				node.ParentNode.RemoveChild(node);
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00014954 File Offset: 0x00012B54
		public override void ReplaceSelf(XmlReader reader)
		{
			XmlNode node = ((IHasXmlNode)this.navigator).GetNode();
			XmlNode parentNode = node.ParentNode;
			if (parentNode == null)
			{
				throw new InvalidOperationException("This node cannot be removed since it has no parent.");
			}
			bool flag = false;
			if (!this.MoveToPrevious())
			{
				this.MoveToParent();
			}
			else
			{
				flag = true;
			}
			XmlDocument xmlDocument = (parentNode.NodeType != XmlNodeType.Document) ? parentNode.OwnerDocument : (parentNode as XmlDocument);
			bool flag2 = false;
			if (reader.ReadState == ReadState.Initial)
			{
				reader.Read();
				if (reader.EOF)
				{
					flag2 = true;
				}
				else
				{
					while (!reader.EOF)
					{
						parentNode.AppendChild(xmlDocument.ReadNode(reader));
					}
				}
			}
			else if (reader.EOF)
			{
				flag2 = true;
			}
			else
			{
				parentNode.AppendChild(xmlDocument.ReadNode(reader));
			}
			if (flag2)
			{
				throw new InvalidOperationException("Content is required in argument XmlReader to replace current node.");
			}
			parentNode.RemoveChild(node);
			if (flag)
			{
				this.MoveToNext();
			}
			else
			{
				this.MoveToFirstChild();
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00014A64 File Offset: 0x00012C64
		public override void SetValue(string value)
		{
			XmlNode node = ((IHasXmlNode)this.navigator).GetNode();
			while (node.FirstChild != null)
			{
				node.RemoveChild(node.FirstChild);
			}
			node.InnerText = value;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00014AA8 File Offset: 0x00012CA8
		public override void MoveToRoot()
		{
			this.navigator.MoveToRoot();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00014AB8 File Offset: 0x00012CB8
		public override bool MoveToNamespace(string name)
		{
			return this.navigator.MoveToNamespace(name);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00014AC8 File Offset: 0x00012CC8
		public override bool MoveToFirst()
		{
			return this.navigator.MoveToFirst();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00014AD8 File Offset: 0x00012CD8
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this.navigator.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00014AE8 File Offset: 0x00012CE8
		public override bool IsDescendant(XPathNavigator nav)
		{
			XmlDocumentEditableNavigator xmlDocumentEditableNavigator = nav as XmlDocumentEditableNavigator;
			if (xmlDocumentEditableNavigator != null)
			{
				return this.navigator.IsDescendant(xmlDocumentEditableNavigator.navigator);
			}
			return this.navigator.IsDescendant(nav);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00014B20 File Offset: 0x00012D20
		public override string GetNamespace(string name)
		{
			return this.navigator.GetNamespace(name);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00014B30 File Offset: 0x00012D30
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.navigator.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00014B40 File Offset: 0x00012D40
		public override XmlNodeOrder ComparePosition(XPathNavigator nav)
		{
			XmlDocumentEditableNavigator xmlDocumentEditableNavigator = nav as XmlDocumentEditableNavigator;
			if (xmlDocumentEditableNavigator != null)
			{
				return this.navigator.ComparePosition(xmlDocumentEditableNavigator.navigator);
			}
			return this.navigator.ComparePosition(nav);
		}

		// Token: 0x04000224 RID: 548
		private static readonly bool isXmlDocumentNavigatorImpl = typeof(XmlDocumentEditableNavigator).Assembly == typeof(XmlDocument).Assembly;

		// Token: 0x04000225 RID: 549
		private XPathEditableDocument document;

		// Token: 0x04000226 RID: 550
		private XPathNavigator navigator;
	}
}
