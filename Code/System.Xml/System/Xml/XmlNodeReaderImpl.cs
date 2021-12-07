using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using Mono.Xml;

namespace System.Xml
{
	// Token: 0x0200010F RID: 271
	internal class XmlNodeReaderImpl : XmlReader, IHasXmlParserContext, IXmlNamespaceResolver
	{
		// Token: 0x06000AD9 RID: 2777 RVA: 0x00038784 File Offset: 0x00036984
		internal XmlNodeReaderImpl(XmlNodeReaderImpl entityContainer) : this(entityContainer.current)
		{
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00038794 File Offset: 0x00036994
		public XmlNodeReaderImpl(XmlNode node)
		{
			this.startNode = node;
			this.depth = 0;
			this.document = ((this.startNode.NodeType != XmlNodeType.Document) ? this.startNode.OwnerDocument : (this.startNode as XmlDocument));
			XmlNodeType nodeType = node.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
				break;
			default:
				if (nodeType != XmlNodeType.EntityReference)
				{
					return;
				}
				break;
			}
			this.ignoreStartNode = true;
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0003881C File Offset: 0x00036A1C
		XmlParserContext IHasXmlParserContext.ParserContext
		{
			get
			{
				return new XmlParserContext(this.document.NameTable, (this.current != null) ? this.current.ConstructNamespaceManager() : new XmlNamespaceManager(this.document.NameTable), (this.document.DocumentType == null) ? null : this.document.DocumentType.DTD, (this.current != null) ? this.current.BaseURI : this.document.BaseURI, this.XmlLang, this.XmlSpace, Encoding.Unicode);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x000388C4 File Offset: 0x00036AC4
		public override int AttributeCount
		{
			get
			{
				if (this.state != ReadState.Interactive)
				{
					return 0;
				}
				if (this.isEndElement || this.current == null)
				{
					return 0;
				}
				XmlNode xmlNode = this.ownerLinkedNode;
				return (xmlNode.Attributes == null) ? 0 : xmlNode.Attributes.Count;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0003891C File Offset: 0x00036B1C
		public override string BaseURI
		{
			get
			{
				if (this.current == null)
				{
					return this.startNode.BaseURI;
				}
				return this.current.BaseURI;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0003894C File Offset: 0x00036B4C
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00038950 File Offset: 0x00036B50
		public override bool CanReadValueChunk
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00038954 File Offset: 0x00036B54
		public override bool CanResolveEntity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00038958 File Offset: 0x00036B58
		public override int Depth
		{
			get
			{
				return (this.current != null) ? ((this.current != this.ownerLinkedNode) ? ((this.current.NodeType != XmlNodeType.Attribute) ? (this.depth + 2) : (this.depth + 1)) : this.depth) : 0;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x000389B8 File Offset: 0x00036BB8
		public override bool EOF
		{
			get
			{
				return this.state == ReadState.EndOfFile || this.state == ReadState.Error;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x000389D4 File Offset: 0x00036BD4
		public override bool HasAttributes
		{
			get
			{
				if (this.isEndElement || this.current == null)
				{
					return false;
				}
				XmlNode xmlNode = this.ownerLinkedNode;
				return xmlNode.Attributes != null && xmlNode.Attributes.Count != 0;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00038A20 File Offset: 0x00036C20
		public override bool HasValue
		{
			get
			{
				if (this.current == null)
				{
					return false;
				}
				XmlNodeType nodeType = this.current.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.EntityReference:
				case XmlNodeType.Document:
				case XmlNodeType.DocumentFragment:
				case XmlNodeType.Notation:
				case XmlNodeType.EndElement:
				case XmlNodeType.EndEntity:
					break;
				default:
					if (nodeType != XmlNodeType.Element)
					{
						return true;
					}
					break;
				}
				return false;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00038A90 File Offset: 0x00036C90
		public override bool IsDefault
		{
			get
			{
				return this.current != null && this.current.NodeType == XmlNodeType.Attribute && !((XmlAttribute)this.current).Specified;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00038AD0 File Offset: 0x00036CD0
		public override bool IsEmptyElement
		{
			get
			{
				return this.current != null && this.current.NodeType == XmlNodeType.Element && ((XmlElement)this.current).IsEmpty;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00038B10 File Offset: 0x00036D10
		public override string LocalName
		{
			get
			{
				if (this.current == null)
				{
					return string.Empty;
				}
				XmlNodeType nodeType = this.current.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
				case XmlNodeType.Attribute:
				case XmlNodeType.EntityReference:
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.DocumentType:
					break;
				default:
					if (nodeType != XmlNodeType.XmlDeclaration)
					{
						return string.Empty;
					}
					break;
				}
				return this.current.LocalName;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00038B88 File Offset: 0x00036D88
		public override string Name
		{
			get
			{
				if (this.current == null)
				{
					return string.Empty;
				}
				XmlNodeType nodeType = this.current.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
				case XmlNodeType.Attribute:
				case XmlNodeType.EntityReference:
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.DocumentType:
					break;
				default:
					if (nodeType != XmlNodeType.XmlDeclaration)
					{
						return string.Empty;
					}
					break;
				}
				return this.current.Name;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00038C00 File Offset: 0x00036E00
		public override string NamespaceURI
		{
			get
			{
				if (this.current == null)
				{
					return string.Empty;
				}
				return this.current.NamespaceURI;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00038C20 File Offset: 0x00036E20
		public override XmlNameTable NameTable
		{
			get
			{
				return this.document.NameTable;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00038C30 File Offset: 0x00036E30
		public override XmlNodeType NodeType
		{
			get
			{
				if (this.current == null)
				{
					return XmlNodeType.None;
				}
				return (!this.isEndElement) ? this.current.NodeType : XmlNodeType.EndElement;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00038C68 File Offset: 0x00036E68
		public override string Prefix
		{
			get
			{
				if (this.current == null)
				{
					return string.Empty;
				}
				return this.current.Prefix;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00038C88 File Offset: 0x00036E88
		public override ReadState ReadState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00038C90 File Offset: 0x00036E90
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				IXmlSchemaInfo result;
				if (this.current != null)
				{
					IXmlSchemaInfo schemaInfo = this.current.SchemaInfo;
					result = schemaInfo;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00038CBC File Offset: 0x00036EBC
		public override string Value
		{
			get
			{
				if (this.NodeType == XmlNodeType.DocumentType)
				{
					return ((XmlDocumentType)this.current).InternalSubset;
				}
				return (!this.HasValue) ? string.Empty : this.current.Value;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00038D08 File Offset: 0x00036F08
		public override string XmlLang
		{
			get
			{
				if (this.current == null)
				{
					return this.startNode.XmlLang;
				}
				return this.current.XmlLang;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00038D38 File Offset: 0x00036F38
		public override XmlSpace XmlSpace
		{
			get
			{
				if (this.current == null)
				{
					return this.startNode.XmlSpace;
				}
				return this.current.XmlSpace;
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00038D68 File Offset: 0x00036F68
		public override void Close()
		{
			this.current = null;
			this.state = ReadState.Closed;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00038D78 File Offset: 0x00036F78
		public override string GetAttribute(int attributeIndex)
		{
			if (this.NodeType == XmlNodeType.XmlDeclaration)
			{
				XmlDeclaration xmlDeclaration = this.current as XmlDeclaration;
				if (attributeIndex == 0)
				{
					return xmlDeclaration.Version;
				}
				if (attributeIndex == 1)
				{
					if (xmlDeclaration.Encoding != string.Empty)
					{
						return xmlDeclaration.Encoding;
					}
					if (xmlDeclaration.Standalone != string.Empty)
					{
						return xmlDeclaration.Standalone;
					}
				}
				else if (attributeIndex == 2 && xmlDeclaration.Encoding != string.Empty && xmlDeclaration.Standalone != null)
				{
					return xmlDeclaration.Standalone;
				}
				throw new ArgumentOutOfRangeException("Index out of range.");
			}
			else
			{
				if (this.NodeType == XmlNodeType.DocumentType)
				{
					XmlDocumentType xmlDocumentType = this.current as XmlDocumentType;
					if (attributeIndex == 0)
					{
						if (xmlDocumentType.PublicId != string.Empty)
						{
							return xmlDocumentType.PublicId;
						}
						if (xmlDocumentType.SystemId != string.Empty)
						{
							return xmlDocumentType.SystemId;
						}
					}
					else if (attributeIndex == 1 && xmlDocumentType.PublicId == string.Empty && xmlDocumentType.SystemId != string.Empty)
					{
						return xmlDocumentType.SystemId;
					}
					throw new ArgumentOutOfRangeException("Index out of range.");
				}
				if (this.isEndElement || this.current == null)
				{
					return null;
				}
				if (attributeIndex < 0 || attributeIndex > this.AttributeCount)
				{
					throw new ArgumentOutOfRangeException("Index out of range.");
				}
				return this.ownerLinkedNode.Attributes[attributeIndex].Value;
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00038F14 File Offset: 0x00037114
		public override string GetAttribute(string name)
		{
			if (this.isEndElement || this.current == null)
			{
				return null;
			}
			if (this.NodeType == XmlNodeType.XmlDeclaration)
			{
				return this.GetXmlDeclarationAttribute(name);
			}
			if (this.NodeType == XmlNodeType.DocumentType)
			{
				return this.GetDocumentTypeAttribute(name);
			}
			if (this.ownerLinkedNode.Attributes == null)
			{
				return null;
			}
			XmlAttribute xmlAttribute = this.ownerLinkedNode.Attributes[name];
			if (xmlAttribute == null)
			{
				return null;
			}
			return xmlAttribute.Value;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00038F98 File Offset: 0x00037198
		public override string GetAttribute(string name, string namespaceURI)
		{
			if (this.isEndElement || this.current == null)
			{
				return null;
			}
			if (this.NodeType == XmlNodeType.XmlDeclaration)
			{
				return this.GetXmlDeclarationAttribute(name);
			}
			if (this.NodeType == XmlNodeType.DocumentType)
			{
				return this.GetDocumentTypeAttribute(name);
			}
			if (this.ownerLinkedNode.Attributes == null)
			{
				return null;
			}
			XmlAttribute xmlAttribute = this.ownerLinkedNode.Attributes[name, namespaceURI];
			if (xmlAttribute == null)
			{
				return null;
			}
			return xmlAttribute.Value;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0003901C File Offset: 0x0003721C
		private string GetXmlDeclarationAttribute(string name)
		{
			XmlDeclaration xmlDeclaration = this.current as XmlDeclaration;
			switch (name)
			{
			case "version":
				return xmlDeclaration.Version;
			case "encoding":
				return (!(xmlDeclaration.Encoding != string.Empty)) ? null : xmlDeclaration.Encoding;
			case "standalone":
				return xmlDeclaration.Standalone;
			}
			return null;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000390D4 File Offset: 0x000372D4
		private string GetDocumentTypeAttribute(string name)
		{
			XmlDocumentType xmlDocumentType = this.current as XmlDocumentType;
			if (name != null)
			{
				if (XmlNodeReaderImpl.<>f__switch$map35 == null)
				{
					XmlNodeReaderImpl.<>f__switch$map35 = new Dictionary<string, int>(2)
					{
						{
							"PUBLIC",
							0
						},
						{
							"SYSTEM",
							1
						}
					};
				}
				int num;
				if (XmlNodeReaderImpl.<>f__switch$map35.TryGetValue(name, out num))
				{
					if (num == 0)
					{
						return xmlDocumentType.PublicId;
					}
					if (num == 1)
					{
						return xmlDocumentType.SystemId;
					}
				}
			}
			return null;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00039158 File Offset: 0x00037358
		public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			XmlNode parentNode = this.current;
			while (parentNode.NodeType != XmlNodeType.Document)
			{
				for (int i = 0; i < this.current.Attributes.Count; i++)
				{
					XmlAttribute xmlAttribute = this.current.Attributes[i];
					if (xmlAttribute.NamespaceURI == "http://www.w3.org/2000/xmlns/")
					{
						dictionary.Add((!(xmlAttribute.Prefix == "xmlns")) ? string.Empty : xmlAttribute.LocalName, xmlAttribute.Value);
					}
				}
				if (scope == XmlNamespaceScope.Local)
				{
					return dictionary;
				}
				parentNode = parentNode.ParentNode;
				if (parentNode == null)
				{
					IL_AE:
					if (scope == XmlNamespaceScope.All)
					{
						dictionary.Add("xml", "http://www.w3.org/XML/1998/namespace");
					}
					return dictionary;
				}
			}
			goto IL_AE;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0003922C File Offset: 0x0003742C
		private XmlElement GetCurrentElement()
		{
			XmlElement result = null;
			switch (this.current.NodeType)
			{
			case XmlNodeType.Element:
				result = (XmlElement)this.current;
				break;
			case XmlNodeType.Attribute:
				result = ((XmlAttribute)this.current).OwnerElement;
				break;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.EntityReference:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				result = (this.current.ParentNode as XmlElement);
				break;
			}
			return result;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000392CC File Offset: 0x000374CC
		public override string LookupNamespace(string prefix)
		{
			if (this.current == null)
			{
				return null;
			}
			for (XmlElement xmlElement = this.GetCurrentElement(); xmlElement != null; xmlElement = (xmlElement.ParentNode as XmlElement))
			{
				for (int i = 0; i < xmlElement.Attributes.Count; i++)
				{
					XmlAttribute xmlAttribute = xmlElement.Attributes[i];
					if (!(xmlAttribute.NamespaceURI != "http://www.w3.org/2000/xmlns/"))
					{
						if (prefix == string.Empty)
						{
							if (xmlAttribute.Prefix == string.Empty)
							{
								return xmlAttribute.Value;
							}
						}
						else if (xmlAttribute.LocalName == prefix)
						{
							return xmlAttribute.Value;
						}
					}
				}
			}
			if (prefix != null)
			{
				if (XmlNodeReaderImpl.<>f__switch$map36 == null)
				{
					XmlNodeReaderImpl.<>f__switch$map36 = new Dictionary<string, int>(2)
					{
						{
							"xml",
							0
						},
						{
							"xmlns",
							1
						}
					};
				}
				int num;
				if (XmlNodeReaderImpl.<>f__switch$map36.TryGetValue(prefix, out num))
				{
					if (num == 0)
					{
						return "http://www.w3.org/XML/1998/namespace";
					}
					if (num == 1)
					{
						return "http://www.w3.org/2000/xmlns/";
					}
				}
			}
			return null;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00039404 File Offset: 0x00037604
		public string LookupPrefix(string ns)
		{
			return this.LookupPrefix(ns, false);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00039410 File Offset: 0x00037610
		public string LookupPrefix(string ns, bool atomizedNames)
		{
			if (this.current == null)
			{
				return null;
			}
			for (XmlElement xmlElement = this.GetCurrentElement(); xmlElement != null; xmlElement = (xmlElement.ParentNode as XmlElement))
			{
				for (int i = 0; i < xmlElement.Attributes.Count; i++)
				{
					XmlAttribute xmlAttribute = xmlElement.Attributes[i];
					if (atomizedNames)
					{
						if (object.ReferenceEquals(xmlAttribute.NamespaceURI, "http://www.w3.org/2000/xmlns/"))
						{
							if (object.ReferenceEquals(xmlAttribute.Value, ns))
							{
								return (!(xmlAttribute.Prefix != string.Empty)) ? string.Empty : xmlAttribute.LocalName;
							}
						}
					}
					else if (!(xmlAttribute.NamespaceURI != "http://www.w3.org/2000/xmlns/"))
					{
						if (xmlAttribute.Value == ns)
						{
							return (!(xmlAttribute.Prefix != string.Empty)) ? string.Empty : xmlAttribute.LocalName;
						}
					}
				}
			}
			if (ns != null)
			{
				if (XmlNodeReaderImpl.<>f__switch$map37 == null)
				{
					XmlNodeReaderImpl.<>f__switch$map37 = new Dictionary<string, int>(2)
					{
						{
							"http://www.w3.org/XML/1998/namespace",
							0
						},
						{
							"http://www.w3.org/2000/xmlns/",
							1
						}
					};
				}
				int num;
				if (XmlNodeReaderImpl.<>f__switch$map37.TryGetValue(ns, out num))
				{
					if (num == 0)
					{
						return "xml";
					}
					if (num == 1)
					{
						return "xmlns";
					}
				}
			}
			return null;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0003958C File Offset: 0x0003778C
		public override void MoveToAttribute(int attributeIndex)
		{
			if (this.isEndElement || attributeIndex < 0 || attributeIndex > this.AttributeCount)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.state = ReadState.Interactive;
			this.current = this.ownerLinkedNode.Attributes[attributeIndex];
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000395DC File Offset: 0x000377DC
		public override bool MoveToAttribute(string name)
		{
			if (this.isEndElement || this.current == null)
			{
				return false;
			}
			XmlNode xmlNode = this.current;
			if (this.current.ParentNode.NodeType == XmlNodeType.Attribute)
			{
				this.current = this.current.ParentNode;
			}
			if (this.ownerLinkedNode.Attributes == null)
			{
				return false;
			}
			XmlAttribute xmlAttribute = this.ownerLinkedNode.Attributes[name];
			if (xmlAttribute == null)
			{
				this.current = xmlNode;
				return false;
			}
			this.current = xmlAttribute;
			return true;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0003966C File Offset: 0x0003786C
		public override bool MoveToAttribute(string name, string namespaceURI)
		{
			if (this.isEndElement || this.current == null)
			{
				return false;
			}
			if (this.ownerLinkedNode.Attributes == null)
			{
				return false;
			}
			XmlAttribute xmlAttribute = this.ownerLinkedNode.Attributes[name, namespaceURI];
			if (xmlAttribute == null)
			{
				return false;
			}
			this.current = xmlAttribute;
			return true;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000396C8 File Offset: 0x000378C8
		public override bool MoveToElement()
		{
			if (this.current == null)
			{
				return false;
			}
			XmlNode xmlNode = this.ownerLinkedNode;
			if (this.current != xmlNode)
			{
				this.current = xmlNode;
				return true;
			}
			return false;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00039700 File Offset: 0x00037900
		public override bool MoveToFirstAttribute()
		{
			if (this.current == null)
			{
				return false;
			}
			if (this.ownerLinkedNode.Attributes == null)
			{
				return false;
			}
			if (this.ownerLinkedNode.Attributes.Count > 0)
			{
				this.current = this.ownerLinkedNode.Attributes[0];
				return true;
			}
			return false;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0003975C File Offset: 0x0003795C
		public override bool MoveToNextAttribute()
		{
			if (this.current == null)
			{
				return false;
			}
			XmlNode parentNode = this.current;
			if (this.current.NodeType != XmlNodeType.Attribute)
			{
				if (this.current.ParentNode == null || this.current.ParentNode.NodeType != XmlNodeType.Attribute)
				{
					return this.MoveToFirstAttribute();
				}
				parentNode = this.current.ParentNode;
			}
			XmlAttributeCollection attributes = ((XmlAttribute)parentNode).OwnerElement.Attributes;
			int i = 0;
			while (i < attributes.Count - 1)
			{
				XmlAttribute xmlAttribute = attributes[i];
				if (xmlAttribute == parentNode)
				{
					i++;
					if (i == attributes.Count)
					{
						return false;
					}
					this.current = attributes[i];
					return true;
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00039824 File Offset: 0x00037A24
		public override bool Read()
		{
			switch (this.state)
			{
			case ReadState.Error:
			case ReadState.EndOfFile:
			case ReadState.Closed:
				return false;
			default:
			{
				if (base.Binary != null)
				{
					base.Binary.Reset();
				}
				bool result = this.ReadContent();
				this.ownerLinkedNode = this.current;
				return result;
			}
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00039880 File Offset: 0x00037A80
		private bool ReadContent()
		{
			if (this.ReadState == ReadState.Initial)
			{
				this.current = this.startNode;
				this.state = ReadState.Interactive;
				if (this.ignoreStartNode)
				{
					this.current = this.startNode.FirstChild;
				}
				if (this.current == null)
				{
					this.state = ReadState.Error;
					return false;
				}
				return true;
			}
			else
			{
				this.MoveToElement();
				XmlNode xmlNode = (this.isEndElement || this.current.NodeType == XmlNodeType.EntityReference) ? null : this.current.FirstChild;
				if (xmlNode != null)
				{
					this.isEndElement = false;
					this.current = xmlNode;
					this.depth++;
					return true;
				}
				if (this.current == this.startNode)
				{
					if (this.IsEmptyElement || this.isEndElement)
					{
						this.isEndElement = false;
						this.current = null;
						this.state = ReadState.EndOfFile;
						return false;
					}
					this.isEndElement = true;
					return true;
				}
				else
				{
					if (!this.isEndElement && !this.IsEmptyElement && this.current.NodeType == XmlNodeType.Element)
					{
						this.isEndElement = true;
						return true;
					}
					XmlNode nextSibling = this.current.NextSibling;
					if (nextSibling != null)
					{
						this.isEndElement = false;
						this.current = nextSibling;
						return true;
					}
					XmlNode parentNode = this.current.ParentNode;
					if (parentNode == null || (parentNode == this.startNode && this.ignoreStartNode))
					{
						this.isEndElement = false;
						this.current = null;
						this.state = ReadState.EndOfFile;
						return false;
					}
					this.current = parentNode;
					this.depth--;
					this.isEndElement = true;
					return true;
				}
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00039A2C File Offset: 0x00037C2C
		public override bool ReadAttributeValue()
		{
			if (this.current.NodeType == XmlNodeType.Attribute)
			{
				if (this.current.FirstChild == null)
				{
					return false;
				}
				this.current = this.current.FirstChild;
				return true;
			}
			else
			{
				if (this.current.ParentNode.NodeType != XmlNodeType.Attribute)
				{
					return false;
				}
				if (this.current.NextSibling == null)
				{
					return false;
				}
				this.current = this.current.NextSibling;
				return true;
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00039AAC File Offset: 0x00037CAC
		public override string ReadString()
		{
			return base.ReadString();
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00039AB4 File Offset: 0x00037CB4
		public override void ResolveEntity()
		{
			throw new NotSupportedException("Should not happen.");
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00039AC0 File Offset: 0x00037CC0
		public override void Skip()
		{
			base.Skip();
		}

		// Token: 0x0400054C RID: 1356
		private XmlDocument document;

		// Token: 0x0400054D RID: 1357
		private XmlNode startNode;

		// Token: 0x0400054E RID: 1358
		private XmlNode current;

		// Token: 0x0400054F RID: 1359
		private XmlNode ownerLinkedNode;

		// Token: 0x04000550 RID: 1360
		private ReadState state;

		// Token: 0x04000551 RID: 1361
		private int depth;

		// Token: 0x04000552 RID: 1362
		private bool isEndElement;

		// Token: 0x04000553 RID: 1363
		private bool ignoreStartNode;
	}
}
