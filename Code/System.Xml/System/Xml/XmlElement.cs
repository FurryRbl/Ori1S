using System;
using System.Collections;
using System.Xml.Schema;
using System.Xml.XPath;
using Mono.Xml;

namespace System.Xml
{
	/// <summary>Represents an element.</summary>
	// Token: 0x020000F4 RID: 244
	public class XmlElement : XmlLinkedNode, IHasXmlChildNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlElement" /> class.</summary>
		/// <param name="prefix">The namespace prefix; see the <see cref="P:System.Xml.XmlElement.Prefix" /> property.</param>
		/// <param name="localName">The local name; see the <see cref="P:System.Xml.XmlElement.LocalName" /> property.</param>
		/// <param name="namespaceURI">The namespace URI; see the <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06000974 RID: 2420 RVA: 0x00032F9C File Offset: 0x0003119C
		protected internal XmlElement(string prefix, string localName, string namespaceURI, XmlDocument doc) : this(prefix, localName, namespaceURI, doc, false)
		{
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00032FAC File Offset: 0x000311AC
		internal XmlElement(string prefix, string localName, string namespaceURI, XmlDocument doc, bool atomizedNames) : base(doc)
		{
			if (!atomizedNames)
			{
				if (prefix == null)
				{
					prefix = string.Empty;
				}
				if (namespaceURI == null)
				{
					namespaceURI = string.Empty;
				}
				XmlConvert.VerifyName(localName);
				prefix = doc.NameTable.Add(prefix);
				localName = doc.NameTable.Add(localName);
				namespaceURI = doc.NameTable.Add(namespaceURI);
			}
			this.name = doc.NameCache.Add(prefix, localName, namespaceURI, true);
			if (doc.DocumentType != null)
			{
				DTDAttListDeclaration dtdattListDeclaration = doc.DocumentType.DTD.AttListDecls[localName];
				if (dtdattListDeclaration != null)
				{
					for (int i = 0; i < dtdattListDeclaration.Definitions.Count; i++)
					{
						DTDAttributeDefinition dtdattributeDefinition = dtdattListDeclaration[i];
						if (dtdattributeDefinition.DefaultValue != null)
						{
							this.SetAttribute(dtdattributeDefinition.Name, dtdattributeDefinition.DefaultValue);
							this.Attributes[dtdattributeDefinition.Name].SetDefault();
						}
					}
				}
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x000330B0 File Offset: 0x000312B0
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x000330B8 File Offset: 0x000312B8
		XmlLinkedNode IHasXmlChildNode.LastLinkedChild
		{
			get
			{
				return this.lastLinkedChild;
			}
			set
			{
				this.lastLinkedChild = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlAttributeCollection" /> containing the list of attributes for this node.</summary>
		/// <returns>
		///   <see cref="T:System.Xml.XmlAttributeCollection" /> containing the list of attributes for this node.</returns>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x000330C4 File Offset: 0x000312C4
		public override XmlAttributeCollection Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new XmlAttributeCollection(this);
				}
				return this.attributes;
			}
		}

		/// <summary>Gets a boolean value indicating whether the current node has any attributes.</summary>
		/// <returns>true if the current node has attributes; otherwise, false.</returns>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x000330E4 File Offset: 0x000312E4
		public virtual bool HasAttributes
		{
			get
			{
				return this.attributes != null && this.attributes.Count > 0;
			}
		}

		/// <summary>Gets or sets the concatenated values of the node and all its children.</summary>
		/// <returns>The concatenated values of the node and all its children.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00033104 File Offset: 0x00031304
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x0003310C File Offset: 0x0003130C
		public override string InnerText
		{
			get
			{
				return base.InnerText;
			}
			set
			{
				if (this.FirstChild != null && this.FirstChild.NextSibling == null && this.FirstChild.NodeType == XmlNodeType.Text)
				{
					this.FirstChild.Value = value;
				}
				else
				{
					while (this.FirstChild != null)
					{
						this.RemoveChild(this.FirstChild);
					}
					base.AppendChild(this.OwnerDocument.CreateTextNode(value), false);
				}
			}
		}

		/// <summary>Gets or sets the markup representing just the children of this node.</summary>
		/// <returns>The markup of the children of this node.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed. </exception>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00033188 File Offset: 0x00031388
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x00033190 File Offset: 0x00031390
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				while (this.FirstChild != null)
				{
					this.RemoveChild(this.FirstChild);
				}
				XmlNamespaceManager nsMgr = base.ConstructNamespaceManager();
				XmlParserContext context = new XmlParserContext(this.OwnerDocument.NameTable, nsMgr, (this.OwnerDocument.DocumentType == null) ? null : this.OwnerDocument.DocumentType.DTD, this.BaseURI, this.XmlLang, this.XmlSpace, null);
				XmlTextReader xmlTextReader = new XmlTextReader(value, XmlNodeType.Element, context);
				xmlTextReader.XmlResolver = this.OwnerDocument.Resolver;
				for (;;)
				{
					XmlNode xmlNode = this.OwnerDocument.ReadNode(xmlTextReader);
					if (xmlNode == null)
					{
						break;
					}
					this.AppendChild(xmlNode);
				}
			}
		}

		/// <summary>Gets or sets the tag format of the element.</summary>
		/// <returns>Returns true if the element is to be serialized in the short tag format "&lt;item/&gt;"; false for the long format "&lt;item&gt;&lt;/item&gt;".When setting this property, if set to true, the children of the element are removed and the element is serialized in the short tag format. If set to false, the value of the property is changed (regardless of whether or not the element has content); if the element is empty, it is serialized in the long format.This property is a Microsoft extension to the Document Object Model (DOM).</returns>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0003324C File Offset: 0x0003144C
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x00033268 File Offset: 0x00031468
		public bool IsEmpty
		{
			get
			{
				return !this.isNotEmpty && this.FirstChild == null;
			}
			set
			{
				this.isNotEmpty = !value;
				if (value)
				{
					while (this.FirstChild != null)
					{
						this.RemoveChild(this.FirstChild);
					}
				}
			}
		}

		/// <summary>Gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.</returns>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00033298 File Offset: 0x00031498
		public override string LocalName
		{
			get
			{
				return this.name.LocalName;
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>The qualified name of the node. For XmlElement nodes, this is the tag name of the element.</returns>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x000332A8 File Offset: 0x000314A8
		public override string Name
		{
			get
			{
				return this.name.GetPrefixedName(this.OwnerDocument.NameCache);
			}
		}

		/// <summary>Gets the namespace URI of this node.</summary>
		/// <returns>The namespace URI of this node. If there is no namespace URI, this property returns String.Empty.</returns>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x000332C0 File Offset: 0x000314C0
		public override string NamespaceURI
		{
			get
			{
				return this.name.NS;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNode" /> immediately following this element.</summary>
		/// <returns>The XmlNode immediately following this element.</returns>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x000332D0 File Offset: 0x000314D0
		public override XmlNode NextSibling
		{
			get
			{
				return (this.ParentNode != null && ((IHasXmlChildNode)this.ParentNode).LastLinkedChild != this) ? base.NextLinkedSibling : null;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type. For XmlElement nodes, this value is XmlNodeType.Element.</returns>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0003330C File Offset: 0x0003150C
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Element;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00033310 File Offset: 0x00031510
		internal override XPathNodeType XPathNodeType
		{
			get
			{
				return XPathNodeType.Element;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.</summary>
		/// <returns>The XmlDocument to which this element belongs.</returns>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00033314 File Offset: 0x00031514
		public override XmlDocument OwnerDocument
		{
			get
			{
				return base.OwnerDocument;
			}
		}

		/// <summary>Gets or sets the namespace prefix of this node.</summary>
		/// <returns>The namespace prefix of this node. If there is no prefix, this property returns String.Empty.</returns>
		/// <exception cref="T:System.ArgumentException">This node is read-only </exception>
		/// <exception cref="T:System.Xml.XmlException">The specified prefix contains an invalid character.The specified prefix is malformed.The namespaceURI of this node is null.The specified prefix is "xml" and the namespaceURI of this node is different from http://www.w3.org/XML/1998/namespace. </exception>
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0003331C File Offset: 0x0003151C
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x0003332C File Offset: 0x0003152C
		public override string Prefix
		{
			get
			{
				return this.name.Prefix;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new ArgumentException("This node is readonly.");
				}
				if (value == null)
				{
					value = string.Empty;
				}
				if (!string.Empty.Equals(value) && !XmlChar.IsNCName(value))
				{
					throw new ArgumentException("Specified name is not a valid NCName: " + value);
				}
				value = this.OwnerDocument.NameTable.Add(value);
				this.name = this.OwnerDocument.NameCache.Add(value, this.name.LocalName, this.name.NS, true);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x000333CC File Offset: 0x000315CC
		public override XmlNode ParentNode
		{
			get
			{
				return base.ParentNode;
			}
		}

		/// <summary>Gets the post schema validation infoset that has been assigned to this node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object containing the post schema validation infoset of this node.</returns>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x000333D4 File Offset: 0x000315D4
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x000333DC File Offset: 0x000315DC
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.schemaInfo;
			}
			internal set
			{
				this.schemaInfo = value;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself (and its attributes if the node is an XmlElement). </param>
		// Token: 0x0600098C RID: 2444 RVA: 0x000333E8 File Offset: 0x000315E8
		public override XmlNode CloneNode(bool deep)
		{
			XmlElement xmlElement = this.OwnerDocument.CreateElement(this.name.Prefix, this.name.LocalName, this.name.NS, true);
			for (int i = 0; i < this.Attributes.Count; i++)
			{
				xmlElement.SetAttributeNode((XmlAttribute)this.Attributes[i].CloneNode(true));
			}
			if (deep)
			{
				for (int j = 0; j < this.ChildNodes.Count; j++)
				{
					xmlElement.AppendChild(this.ChildNodes[j].CloneNode(true), false);
				}
			}
			return xmlElement;
		}

		/// <summary>Returns the value for the attribute with the specified name.</summary>
		/// <returns>The value of the specified attribute. An empty string is returned if a matching attribute is not found or if the attribute does not have a specified or default value.</returns>
		/// <param name="name">The name of the attribute to retrieve. This is a qualified name. It is matched against the Name property of the matching node. </param>
		// Token: 0x0600098D RID: 2445 RVA: 0x0003349C File Offset: 0x0003169C
		public virtual string GetAttribute(string name)
		{
			XmlNode namedItem = this.Attributes.GetNamedItem(name);
			return (namedItem == null) ? string.Empty : namedItem.Value;
		}

		/// <summary>Returns the value for the attribute with the specified local name and namespace URI.</summary>
		/// <returns>The value of the specified attribute. An empty string is returned if a matching attribute is not found or if the attribute does not have a specified or default value.</returns>
		/// <param name="localName">The local name of the attribute to retrieve. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute to retrieve. </param>
		// Token: 0x0600098E RID: 2446 RVA: 0x000334CC File Offset: 0x000316CC
		public virtual string GetAttribute(string localName, string namespaceURI)
		{
			XmlNode namedItem = this.Attributes.GetNamedItem(localName, namespaceURI);
			return (namedItem == null) ? string.Empty : namedItem.Value;
		}

		/// <summary>Returns the XmlAttribute with the specified name.</summary>
		/// <returns>The specified XmlAttribute or null if a matching attribute was not found.</returns>
		/// <param name="name">The name of the attribute to retrieve. This is a qualified name. It is matched against the Name property of the matching node. </param>
		// Token: 0x0600098F RID: 2447 RVA: 0x00033500 File Offset: 0x00031700
		public virtual XmlAttribute GetAttributeNode(string name)
		{
			XmlNode namedItem = this.Attributes.GetNamedItem(name);
			return (namedItem == null) ? null : (namedItem as XmlAttribute);
		}

		/// <summary>Returns the <see cref="T:System.Xml.XmlAttribute" /> with the specified local name and namespace URI.</summary>
		/// <returns>The specified XmlAttribute or null if a matching attribute was not found.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000990 RID: 2448 RVA: 0x0003352C File Offset: 0x0003172C
		public virtual XmlAttribute GetAttributeNode(string localName, string namespaceURI)
		{
			XmlNode namedItem = this.Attributes.GetNamedItem(localName, namespaceURI);
			return (namedItem == null) ? null : (namedItem as XmlAttribute);
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlNodeList" /> containing a list of all descendant elements that match the specified <see cref="P:System.Xml.XmlElement.Name" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a list of all matching nodes.</returns>
		/// <param name="name">The name tag to match. This is a qualified name. It is matched against the Name property of the matching node. The asterisk (*) is a special value that matches all tags. </param>
		// Token: 0x06000991 RID: 2449 RVA: 0x0003355C File Offset: 0x0003175C
		public virtual XmlNodeList GetElementsByTagName(string name)
		{
			ArrayList arrayList = new ArrayList();
			base.SearchDescendantElements(name, name == "*", arrayList);
			return new XmlNodeArrayList(arrayList);
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlNodeList" /> containing a list of all descendant elements that match the specified <see cref="P:System.Xml.XmlElement.LocalName" /> and <see cref="P:System.Xml.XmlElement.NamespaceURI" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a list of all matching nodes.</returns>
		/// <param name="localName">The local name to match. The asterisk (*) is a special value that matches all tags. </param>
		/// <param name="namespaceURI">The namespace URI to match. </param>
		// Token: 0x06000992 RID: 2450 RVA: 0x00033588 File Offset: 0x00031788
		public virtual XmlNodeList GetElementsByTagName(string localName, string namespaceURI)
		{
			ArrayList arrayList = new ArrayList();
			base.SearchDescendantElements(localName, localName == "*", namespaceURI, namespaceURI == "*", arrayList);
			return new XmlNodeArrayList(arrayList);
		}

		/// <summary>Determines whether the current node has an attribute with the specified name.</summary>
		/// <returns>true if the current node has the specified attribute; otherwise, false.</returns>
		/// <param name="name">The name of the attribute to find. This is a qualified name. It is matched against the Name property of the matching node. </param>
		// Token: 0x06000993 RID: 2451 RVA: 0x000335C0 File Offset: 0x000317C0
		public virtual bool HasAttribute(string name)
		{
			XmlNode namedItem = this.Attributes.GetNamedItem(name);
			return namedItem != null;
		}

		/// <summary>Determines whether the current node has an attribute with the specified local name and namespace URI.</summary>
		/// <returns>true if the current node has the specified attribute; otherwise, false.</returns>
		/// <param name="localName">The local name of the attribute to find. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute to find. </param>
		// Token: 0x06000994 RID: 2452 RVA: 0x000335E4 File Offset: 0x000317E4
		public virtual bool HasAttribute(string localName, string namespaceURI)
		{
			XmlNode namedItem = this.Attributes.GetNamedItem(localName, namespaceURI);
			return namedItem != null;
		}

		/// <summary>Removes all specified attributes and children of the current node. Default attributes are not removed.</summary>
		// Token: 0x06000995 RID: 2453 RVA: 0x00033608 File Offset: 0x00031808
		public override void RemoveAll()
		{
			base.RemoveAll();
		}

		/// <summary>Removes all specified attributes from the element. Default attributes are not removed.</summary>
		// Token: 0x06000996 RID: 2454 RVA: 0x00033610 File Offset: 0x00031810
		public virtual void RemoveAllAttributes()
		{
			if (this.attributes != null)
			{
				this.attributes.RemoveAll();
			}
		}

		/// <summary>Removes an attribute by name.</summary>
		/// <param name="name">The name of the attribute to remove.This is a qualified name. It is matched against the Name property of the matching node. </param>
		/// <exception cref="T:System.ArgumentException">The node is read-only. </exception>
		// Token: 0x06000997 RID: 2455 RVA: 0x00033628 File Offset: 0x00031828
		public virtual void RemoveAttribute(string name)
		{
			if (this.attributes == null)
			{
				return;
			}
			XmlAttribute xmlAttribute = this.Attributes.GetNamedItem(name) as XmlAttribute;
			if (xmlAttribute != null)
			{
				this.Attributes.Remove(xmlAttribute);
			}
		}

		/// <summary>Removes an attribute with the specified local name and namespace URI. (If the removed attribute has a default value, it is immediately replaced).</summary>
		/// <param name="localName">The local name of the attribute to remove. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute to remove. </param>
		/// <exception cref="T:System.ArgumentException">The node is read-only. </exception>
		// Token: 0x06000998 RID: 2456 RVA: 0x00033668 File Offset: 0x00031868
		public virtual void RemoveAttribute(string localName, string namespaceURI)
		{
			if (this.attributes == null)
			{
				return;
			}
			XmlAttribute xmlAttribute = this.attributes.GetNamedItem(localName, namespaceURI) as XmlAttribute;
			if (xmlAttribute != null)
			{
				this.Attributes.Remove(xmlAttribute);
			}
		}

		/// <summary>Removes the attribute node with the specified index from the element. (If the removed attribute has a default value, it is immediately replaced).</summary>
		/// <returns>The attribute node removed or null if there is no node at the given index.</returns>
		/// <param name="i">The index of the node to remove. The first node has index 0. </param>
		// Token: 0x06000999 RID: 2457 RVA: 0x000336A8 File Offset: 0x000318A8
		public virtual XmlNode RemoveAttributeAt(int i)
		{
			if (this.attributes == null || this.attributes.Count <= i)
			{
				return null;
			}
			return this.Attributes.RemoveAt(i);
		}

		/// <summary>Removes the specified <see cref="T:System.Xml.XmlAttribute" />.</summary>
		/// <returns>The removed XmlAttribute or null if <paramref name="oldAttr" /> is not an attribute node of the XmlElement.</returns>
		/// <param name="oldAttr">The XmlAttribute node to remove. If the removed attribute has a default value, it is immediately replaced. </param>
		/// <exception cref="T:System.ArgumentException">This node is read-only. </exception>
		// Token: 0x0600099A RID: 2458 RVA: 0x000336E0 File Offset: 0x000318E0
		public virtual XmlAttribute RemoveAttributeNode(XmlAttribute oldAttr)
		{
			if (this.attributes == null)
			{
				return null;
			}
			return this.Attributes.Remove(oldAttr);
		}

		/// <summary>Removes the <see cref="T:System.Xml.XmlAttribute" /> specified by the local name and namespace URI. (If the removed attribute has a default value, it is immediately replaced).</summary>
		/// <returns>The removed XmlAttribute or null if the XmlElement does not have a matching attribute node.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		/// <exception cref="T:System.ArgumentException">This node is read-only. </exception>
		// Token: 0x0600099B RID: 2459 RVA: 0x000336FC File Offset: 0x000318FC
		public virtual XmlAttribute RemoveAttributeNode(string localName, string namespaceURI)
		{
			if (this.attributes == null)
			{
				return null;
			}
			return this.Attributes.Remove(this.attributes[localName, namespaceURI]);
		}

		/// <summary>Sets the value of the attribute with the specified name.</summary>
		/// <param name="name">The name of the attribute to create or alter. This is a qualified name. If the name contains a colon it is parsed into prefix and local name components. </param>
		/// <param name="value">The value to set for the attribute. </param>
		/// <exception cref="T:System.Xml.XmlException">The specified name contains an invalid character. </exception>
		/// <exception cref="T:System.ArgumentException">The node is read-only. </exception>
		// Token: 0x0600099C RID: 2460 RVA: 0x00033724 File Offset: 0x00031924
		public virtual void SetAttribute(string name, string value)
		{
			XmlAttribute xmlAttribute = this.Attributes[name];
			if (xmlAttribute == null)
			{
				xmlAttribute = this.OwnerDocument.CreateAttribute(name);
				xmlAttribute.Value = value;
				this.Attributes.SetNamedItem(xmlAttribute);
			}
			else
			{
				xmlAttribute.Value = value;
			}
		}

		/// <summary>Sets the value of the attribute with the specified local name and namespace URI.</summary>
		/// <returns>The attribute value.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		/// <param name="value">The value to set for the attribute. </param>
		// Token: 0x0600099D RID: 2461 RVA: 0x00033774 File Offset: 0x00031974
		public virtual string SetAttribute(string localName, string namespaceURI, string value)
		{
			XmlAttribute xmlAttribute = this.Attributes[localName, namespaceURI];
			if (xmlAttribute == null)
			{
				xmlAttribute = this.OwnerDocument.CreateAttribute(localName, namespaceURI);
				xmlAttribute.Value = value;
				this.Attributes.SetNamedItem(xmlAttribute);
			}
			else
			{
				xmlAttribute.Value = value;
			}
			return xmlAttribute.Value;
		}

		/// <summary>Adds the specified <see cref="T:System.Xml.XmlAttribute" />.</summary>
		/// <returns>If the attribute replaces an existing attribute with the same name, the old XmlAttribute is returned; otherwise, null is returned.</returns>
		/// <param name="newAttr">The XmlAttribute node to add to the attribute collection for this element. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newAttr" /> was created from a different document than the one that created this node. Or this node is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="newAttr" /> is already an attribute of another XmlElement object. You must explicitly clone XmlAttribute nodes to re-use them in other XmlElement objects. </exception>
		// Token: 0x0600099E RID: 2462 RVA: 0x000337CC File Offset: 0x000319CC
		public virtual XmlAttribute SetAttributeNode(XmlAttribute newAttr)
		{
			if (newAttr.OwnerElement != null)
			{
				throw new InvalidOperationException("Specified attribute is already an attribute of another element.");
			}
			XmlAttribute xmlAttribute = this.Attributes.SetNamedItem(newAttr) as XmlAttribute;
			return (xmlAttribute != newAttr) ? xmlAttribute : null;
		}

		/// <summary>Adds the specified <see cref="T:System.Xml.XmlAttribute" />.</summary>
		/// <returns>The XmlAttribute to add.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x0600099F RID: 2463 RVA: 0x00033810 File Offset: 0x00031A10
		public virtual XmlAttribute SetAttributeNode(string localName, string namespaceURI)
		{
			XmlConvert.VerifyNCName(localName);
			return this.Attributes.Append(this.OwnerDocument.CreateAttribute(string.Empty, localName, namespaceURI, false, true));
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060009A0 RID: 2464 RVA: 0x00033844 File Offset: 0x00031A44
		public override void WriteContentTo(XmlWriter w)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				xmlNode.WriteTo(w);
			}
		}

		/// <summary>Saves the current node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060009A1 RID: 2465 RVA: 0x00033874 File Offset: 0x00031A74
		public override void WriteTo(XmlWriter w)
		{
			w.WriteStartElement((this.name.NS != null && this.name.NS.Length != 0) ? this.name.Prefix : string.Empty, this.name.LocalName, this.name.NS);
			if (this.HasAttributes)
			{
				for (int i = 0; i < this.Attributes.Count; i++)
				{
					this.Attributes[i].WriteTo(w);
				}
			}
			this.WriteContentTo(w);
			if (this.IsEmpty)
			{
				w.WriteEndElement();
			}
			else
			{
				w.WriteFullEndElement();
			}
		}

		// Token: 0x040004E3 RID: 1251
		private XmlAttributeCollection attributes;

		// Token: 0x040004E4 RID: 1252
		private XmlNameEntry name;

		// Token: 0x040004E5 RID: 1253
		private XmlLinkedNode lastLinkedChild;

		// Token: 0x040004E6 RID: 1254
		private bool isNotEmpty;

		// Token: 0x040004E7 RID: 1255
		private IXmlSchemaInfo schemaInfo;
	}
}
