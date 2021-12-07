using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x0200001B RID: 27
	internal class XmlSchemaValidatingReader : XmlReader, IHasXmlParserContext, IXmlLineInfo, IXmlNamespaceResolver, IXmlSchemaInfo
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00006A28 File Offset: 0x00004C28
		public XmlSchemaValidatingReader(XmlReader reader, XmlReaderSettings settings)
		{
			XmlSchemaValidatingReader <>f__this = this;
			IXmlNamespaceResolver xmlNamespaceResolver = reader as IXmlNamespaceResolver;
			if (xmlNamespaceResolver == null)
			{
				xmlNamespaceResolver = new XmlNamespaceManager(reader.NameTable);
			}
			XmlSchemaSet xmlSchemaSet = settings.Schemas;
			if (xmlSchemaSet == null)
			{
				xmlSchemaSet = new XmlSchemaSet();
			}
			this.options = settings.ValidationFlags;
			this.reader = reader;
			this.v = new XmlSchemaValidator(reader.NameTable, xmlSchemaSet, xmlNamespaceResolver, this.options);
			if (reader.BaseURI != string.Empty)
			{
				this.v.SourceUri = new Uri(reader.BaseURI);
			}
			this.readerLineInfo = (reader as IXmlLineInfo);
			this.getter = delegate()
			{
				if (<>f__this.v.CurrentAttributeType != null)
				{
					return <>f__this.v.CurrentAttributeType.ParseValue(<>f__this.Value, <>f__this.NameTable, <>f__this);
				}
				return <>f__this.Value;
			};
			this.xsinfo = new XmlSchemaInfo();
			this.v.LineInfoProvider = this;
			this.v.ValidationEventSender = reader;
			this.nsResolver = xmlNamespaceResolver;
			this.ValidationEventHandler += delegate(object o, ValidationEventArgs e)
			{
				settings.OnValidationError(o, e);
			};
			if (settings != null && settings.Schemas != null)
			{
				this.v.XmlResolver = settings.Schemas.XmlResolver;
			}
			else
			{
				this.v.XmlResolver = new XmlUrlResolver();
			}
			this.v.Initialize();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600007F RID: 127 RVA: 0x00006BBC File Offset: 0x00004DBC
		// (remove) Token: 0x06000080 RID: 128 RVA: 0x00006BCC File Offset: 0x00004DCC
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.v.ValidationEventHandler += value;
			}
			remove
			{
				this.v.ValidationEventHandler -= value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00006BDC File Offset: 0x00004DDC
		int IXmlLineInfo.LineNumber
		{
			get
			{
				return (this.readerLineInfo == null) ? 0 : this.readerLineInfo.LineNumber;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00006BFC File Offset: 0x00004DFC
		int IXmlLineInfo.LinePosition
		{
			get
			{
				return (this.readerLineInfo == null) ? 0 : this.readerLineInfo.LinePosition;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00006C1C File Offset: 0x00004E1C
		bool IXmlLineInfo.HasLineInfo()
		{
			return this.readerLineInfo != null && this.readerLineInfo.HasLineInfo();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00006C38 File Offset: 0x00004E38
		public XmlSchemaType ElementSchemaType
		{
			get
			{
				return (this.element == null) ? null : this.element.ElementSchemaType;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00006C58 File Offset: 0x00004E58
		private void ResetStateOnRead()
		{
			this.currentDefaultAttribute = -1;
			this.defaultAttributeConsumed = false;
			this.currentAttrType = null;
			this.defaultAttributes = XmlSchemaValidatingReader.emptyAttributeArray;
			this.v.CurrentAttributeType = null;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00006C94 File Offset: 0x00004E94
		public int LineNumber
		{
			get
			{
				return (this.readerLineInfo == null) ? 0 : this.readerLineInfo.LineNumber;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00006CB4 File Offset: 0x00004EB4
		public int LinePosition
		{
			get
			{
				return (this.readerLineInfo == null) ? 0 : this.readerLineInfo.LinePosition;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00006CD4 File Offset: 0x00004ED4
		public XmlSchemaType SchemaType
		{
			get
			{
				if (this.ReadState != ReadState.Interactive)
				{
					return null;
				}
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						return null;
					}
					if (this.currentAttrType == null)
					{
						XmlSchemaComplexType xmlSchemaComplexType = this.ElementSchemaType as XmlSchemaComplexType;
						if (xmlSchemaComplexType != null)
						{
							XmlSchemaAttribute xmlSchemaAttribute = xmlSchemaComplexType.AttributeUses[new XmlQualifiedName(this.LocalName, this.NamespaceURI)] as XmlSchemaAttribute;
							if (xmlSchemaAttribute != null)
							{
								this.currentAttrType = xmlSchemaAttribute.AttributeSchemaType;
							}
							return this.currentAttrType;
						}
					}
					return this.currentAttrType;
				}
				else
				{
					if (this.ElementSchemaType != null)
					{
						return this.ElementSchemaType;
					}
					return null;
				}
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00006D80 File Offset: 0x00004F80
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00006D88 File Offset: 0x00004F88
		public ValidationType ValidationType
		{
			get
			{
				return this.validationType;
			}
			set
			{
				if (this.ReadState != ReadState.Initial)
				{
					throw new InvalidOperationException("ValidationType must be set before reading.");
				}
				this.validationType = value;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00006DA8 File Offset: 0x00004FA8
		public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			IXmlNamespaceResolver xmlNamespaceResolver = this.reader as IXmlNamespaceResolver;
			if (xmlNamespaceResolver == null)
			{
				throw new NotSupportedException("The input XmlReader does not implement IXmlNamespaceResolver and thus this validating reader cannot collect in-scope namespaces.");
			}
			return xmlNamespaceResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00006DDC File Offset: 0x00004FDC
		public string LookupPrefix(string ns)
		{
			return this.nsResolver.LookupPrefix(ns);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00006DEC File Offset: 0x00004FEC
		public override int AttributeCount
		{
			get
			{
				return this.reader.AttributeCount + this.defaultAttributes.Length;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00006E04 File Offset: 0x00005004
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00006E14 File Offset: 0x00005014
		public override bool CanResolveEntity
		{
			get
			{
				return this.reader.CanResolveEntity;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00006E24 File Offset: 0x00005024
		public override int Depth
		{
			get
			{
				if (this.currentDefaultAttribute < 0)
				{
					return this.reader.Depth;
				}
				if (this.defaultAttributeConsumed)
				{
					return this.reader.Depth + 2;
				}
				return this.reader.Depth + 1;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00006E70 File Offset: 0x00005070
		public override bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00006E80 File Offset: 0x00005080
		public override bool HasValue
		{
			get
			{
				return this.currentDefaultAttribute >= 0 || this.reader.HasValue;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00006E9C File Offset: 0x0000509C
		public override bool IsDefault
		{
			get
			{
				return this.currentDefaultAttribute >= 0 || this.reader.IsDefault;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00006EB8 File Offset: 0x000050B8
		public override bool IsEmptyElement
		{
			get
			{
				return this.currentDefaultAttribute < 0 && this.reader.IsEmptyElement;
			}
		}

		// Token: 0x17000012 RID: 18
		public override string this[int i]
		{
			get
			{
				return this.GetAttribute(i);
			}
		}

		// Token: 0x17000013 RID: 19
		public override string this[string name]
		{
			get
			{
				return this.GetAttribute(name);
			}
		}

		// Token: 0x17000014 RID: 20
		public override string this[string localName, string ns]
		{
			get
			{
				return this.GetAttribute(localName, ns);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00006EF8 File Offset: 0x000050F8
		public override string LocalName
		{
			get
			{
				if (this.currentDefaultAttribute < 0)
				{
					return this.reader.LocalName;
				}
				if (this.defaultAttributeConsumed)
				{
					return string.Empty;
				}
				return this.defaultAttributes[this.currentDefaultAttribute].QualifiedName.Name;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00006F48 File Offset: 0x00005148
		public override string Name
		{
			get
			{
				if (this.currentDefaultAttribute < 0)
				{
					return this.reader.Name;
				}
				if (this.defaultAttributeConsumed)
				{
					return string.Empty;
				}
				XmlQualifiedName qualifiedName = this.defaultAttributes[this.currentDefaultAttribute].QualifiedName;
				string prefix = this.Prefix;
				if (prefix == string.Empty)
				{
					return qualifiedName.Name;
				}
				return prefix + ":" + qualifiedName.Name;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00006FC0 File Offset: 0x000051C0
		public override string NamespaceURI
		{
			get
			{
				if (this.currentDefaultAttribute < 0)
				{
					return this.reader.NamespaceURI;
				}
				if (this.defaultAttributeConsumed)
				{
					return string.Empty;
				}
				return this.defaultAttributes[this.currentDefaultAttribute].QualifiedName.Namespace;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00007010 File Offset: 0x00005210
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00007020 File Offset: 0x00005220
		public override XmlNodeType NodeType
		{
			get
			{
				if (this.currentDefaultAttribute < 0)
				{
					return this.reader.NodeType;
				}
				if (this.defaultAttributeConsumed)
				{
					return XmlNodeType.Text;
				}
				return XmlNodeType.Attribute;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00007054 File Offset: 0x00005254
		public XmlParserContext ParserContext
		{
			get
			{
				return XmlSchemaUtil.GetParserContext(this.reader);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00007064 File Offset: 0x00005264
		public override string Prefix
		{
			get
			{
				if (this.currentDefaultAttribute < 0)
				{
					return this.reader.Prefix;
				}
				if (this.defaultAttributeConsumed)
				{
					return string.Empty;
				}
				XmlQualifiedName qualifiedName = this.defaultAttributes[this.currentDefaultAttribute].QualifiedName;
				string text = this.nsResolver.LookupPrefix(qualifiedName.Namespace);
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000070CC File Offset: 0x000052CC
		public override char QuoteChar
		{
			get
			{
				return this.reader.QuoteChar;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000070DC File Offset: 0x000052DC
		public override ReadState ReadState
		{
			get
			{
				return this.reader.ReadState;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000070EC File Offset: 0x000052EC
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000070F0 File Offset: 0x000052F0
		public override string Value
		{
			get
			{
				if (this.currentDefaultAttribute < 0)
				{
					return this.reader.Value;
				}
				string text = this.defaultAttributes[this.currentDefaultAttribute].ValidatedDefaultValue;
				if (text == null)
				{
					text = this.defaultAttributes[this.currentDefaultAttribute].ValidatedFixedValue;
				}
				return text;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00007144 File Offset: 0x00005344
		public override string XmlLang
		{
			get
			{
				string text = this.reader.XmlLang;
				if (text != null)
				{
					return text;
				}
				int num = this.FindDefaultAttribute("lang", "http://www.w3.org/XML/1998/namespace");
				if (num < 0)
				{
					return null;
				}
				text = this.defaultAttributes[num].ValidatedDefaultValue;
				if (text == null)
				{
					text = this.defaultAttributes[num].ValidatedFixedValue;
				}
				return text;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000071A4 File Offset: 0x000053A4
		public override XmlSpace XmlSpace
		{
			get
			{
				XmlSpace xmlSpace = this.reader.XmlSpace;
				if (xmlSpace != XmlSpace.None)
				{
					return xmlSpace;
				}
				int num = this.FindDefaultAttribute("space", "http://www.w3.org/XML/1998/namespace");
				if (num < 0)
				{
					return XmlSpace.None;
				}
				string text = this.defaultAttributes[num].ValidatedDefaultValue;
				if (text == null)
				{
					text = this.defaultAttributes[num].ValidatedFixedValue;
				}
				return (XmlSpace)((int)Enum.Parse(typeof(XmlSpace), text, false));
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00007218 File Offset: 0x00005418
		public override void Close()
		{
			this.reader.Close();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007228 File Offset: 0x00005428
		public override string GetAttribute(int i)
		{
			XmlNodeType nodeType = this.reader.NodeType;
			if (nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.XmlDeclaration)
			{
				return this.reader.GetAttribute(i);
			}
			if (this.reader.AttributeCount > i)
			{
				this.reader.GetAttribute(i);
			}
			int num = i - this.reader.AttributeCount;
			if (i < this.AttributeCount)
			{
				return this.defaultAttributes[num].DefaultValue;
			}
			throw new ArgumentOutOfRangeException("i", i, "Specified attribute index is out of range.");
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000072C0 File Offset: 0x000054C0
		public override string GetAttribute(string name)
		{
			XmlNodeType nodeType = this.reader.NodeType;
			if (nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.XmlDeclaration)
			{
				return this.reader.GetAttribute(name);
			}
			string attribute = this.reader.GetAttribute(name);
			if (attribute != null)
			{
				return attribute;
			}
			XmlQualifiedName xmlQualifiedName = this.SplitQName(name);
			return this.GetDefaultAttribute(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000732C File Offset: 0x0000552C
		private XmlQualifiedName SplitQName(string name)
		{
			XmlConvert.VerifyName(name);
			Exception ex = null;
			XmlQualifiedName result = XmlSchemaUtil.ToQName(this.reader, name, out ex);
			if (ex != null)
			{
				return XmlQualifiedName.Empty;
			}
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007360 File Offset: 0x00005560
		public override string GetAttribute(string localName, string ns)
		{
			XmlNodeType nodeType = this.reader.NodeType;
			if (nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.XmlDeclaration)
			{
				return this.reader.GetAttribute(localName, ns);
			}
			string attribute = this.reader.GetAttribute(localName, ns);
			if (attribute != null)
			{
				return attribute;
			}
			return this.GetDefaultAttribute(localName, ns);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000073BC File Offset: 0x000055BC
		private string GetDefaultAttribute(string localName, string ns)
		{
			int num = this.FindDefaultAttribute(localName, ns);
			if (num < 0)
			{
				return null;
			}
			string text = this.defaultAttributes[num].ValidatedDefaultValue;
			if (text == null)
			{
				text = this.defaultAttributes[num].ValidatedFixedValue;
			}
			return text;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00007400 File Offset: 0x00005600
		private int FindDefaultAttribute(string localName, string ns)
		{
			for (int i = 0; i < this.defaultAttributes.Length; i++)
			{
				XmlSchemaAttribute xmlSchemaAttribute = this.defaultAttributes[i];
				if (xmlSchemaAttribute.QualifiedName.Name == localName && (ns == null || xmlSchemaAttribute.QualifiedName.Namespace == ns))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00007464 File Offset: 0x00005664
		public override string LookupNamespace(string prefix)
		{
			return this.reader.LookupNamespace(prefix);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007474 File Offset: 0x00005674
		public override void MoveToAttribute(int i)
		{
			XmlNodeType nodeType = this.reader.NodeType;
			if (nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.XmlDeclaration)
			{
				this.reader.MoveToAttribute(i);
				return;
			}
			this.currentAttrType = null;
			if (i < this.reader.AttributeCount)
			{
				this.reader.MoveToAttribute(i);
				this.currentDefaultAttribute = -1;
				this.defaultAttributeConsumed = false;
			}
			if (i < this.AttributeCount)
			{
				this.currentDefaultAttribute = i - this.reader.AttributeCount;
				this.defaultAttributeConsumed = false;
				return;
			}
			throw new ArgumentOutOfRangeException("i", i, "Attribute index is out of range.");
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00007524 File Offset: 0x00005724
		public override bool MoveToAttribute(string name)
		{
			XmlNodeType nodeType = this.reader.NodeType;
			if (nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.XmlDeclaration)
			{
				return this.reader.MoveToAttribute(name);
			}
			this.currentAttrType = null;
			bool flag = this.reader.MoveToAttribute(name);
			if (flag)
			{
				this.currentDefaultAttribute = -1;
				this.defaultAttributeConsumed = false;
				return true;
			}
			return this.MoveToDefaultAttribute(name, null);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00007594 File Offset: 0x00005794
		public override bool MoveToAttribute(string localName, string ns)
		{
			XmlNodeType nodeType = this.reader.NodeType;
			if (nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.XmlDeclaration)
			{
				return this.reader.MoveToAttribute(localName, ns);
			}
			this.currentAttrType = null;
			bool flag = this.reader.MoveToAttribute(localName, ns);
			if (flag)
			{
				this.currentDefaultAttribute = -1;
				this.defaultAttributeConsumed = false;
				return true;
			}
			return this.MoveToDefaultAttribute(localName, ns);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00007604 File Offset: 0x00005804
		private bool MoveToDefaultAttribute(string localName, string ns)
		{
			int num = this.FindDefaultAttribute(localName, ns);
			if (num < 0)
			{
				return false;
			}
			this.currentDefaultAttribute = num;
			this.defaultAttributeConsumed = false;
			return true;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00007634 File Offset: 0x00005834
		public override bool MoveToElement()
		{
			this.currentDefaultAttribute = -1;
			this.defaultAttributeConsumed = false;
			this.currentAttrType = null;
			return this.reader.MoveToElement();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00007664 File Offset: 0x00005864
		public override bool MoveToFirstAttribute()
		{
			XmlNodeType nodeType = this.reader.NodeType;
			if (nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.XmlDeclaration)
			{
				return this.reader.MoveToFirstAttribute();
			}
			this.currentAttrType = null;
			if (this.reader.AttributeCount > 0)
			{
				bool flag = this.reader.MoveToFirstAttribute();
				if (flag)
				{
					this.currentDefaultAttribute = -1;
					this.defaultAttributeConsumed = false;
				}
				return flag;
			}
			if (this.defaultAttributes.Length > 0)
			{
				this.currentDefaultAttribute = 0;
				this.defaultAttributeConsumed = false;
				return true;
			}
			return false;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000076F8 File Offset: 0x000058F8
		public override bool MoveToNextAttribute()
		{
			XmlNodeType nodeType = this.reader.NodeType;
			if (nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.XmlDeclaration)
			{
				return this.reader.MoveToNextAttribute();
			}
			this.currentAttrType = null;
			if (this.currentDefaultAttribute >= 0)
			{
				if (this.defaultAttributes.Length == this.currentDefaultAttribute + 1)
				{
					return false;
				}
				this.currentDefaultAttribute++;
				this.defaultAttributeConsumed = false;
				return true;
			}
			else
			{
				bool flag = this.reader.MoveToNextAttribute();
				if (flag)
				{
					this.currentDefaultAttribute = -1;
					this.defaultAttributeConsumed = false;
					return true;
				}
				if (this.defaultAttributes.Length > 0)
				{
					this.currentDefaultAttribute = 0;
					this.defaultAttributeConsumed = false;
					return true;
				}
				return false;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000077B4 File Offset: 0x000059B4
		public override bool Read()
		{
			if (!this.reader.Read())
			{
				if (!this.validationDone)
				{
					this.v.EndValidation();
					this.validationDone = true;
				}
				return false;
			}
			this.ResetStateOnRead();
			XmlNodeType nodeType = this.reader.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
			{
				string attribute = this.reader.GetAttribute("schemaLocation", "http://www.w3.org/2001/XMLSchema-instance");
				string attribute2 = this.reader.GetAttribute("noNamespaceSchemaLocation", "http://www.w3.org/2001/XMLSchema-instance");
				string attribute3 = this.reader.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
				string attribute4 = this.reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
				this.v.ValidateElement(this.reader.LocalName, this.reader.NamespaceURI, this.xsinfo, attribute3, attribute4, attribute, attribute2);
				if (this.reader.MoveToFirstAttribute())
				{
					for (;;)
					{
						string namespaceURI = this.reader.NamespaceURI;
						if (namespaceURI == null)
						{
							goto IL_202;
						}
						if (XmlSchemaValidatingReader.<>f__switch$map1 == null)
						{
							XmlSchemaValidatingReader.<>f__switch$map1 = new Dictionary<string, int>(2)
							{
								{
									"http://www.w3.org/2001/XMLSchema-instance",
									0
								},
								{
									"http://www.w3.org/2000/xmlns/",
									1
								}
							};
						}
						int num;
						if (!XmlSchemaValidatingReader.<>f__switch$map1.TryGetValue(namespaceURI, out num))
						{
							goto IL_202;
						}
						if (num == 0)
						{
							string localName = this.reader.LocalName;
							if (localName != null)
							{
								if (XmlSchemaValidatingReader.<>f__switch$map0 == null)
								{
									XmlSchemaValidatingReader.<>f__switch$map0 = new Dictionary<string, int>(4)
									{
										{
											"schemaLocation",
											0
										},
										{
											"noNamespaceSchemaLocation",
											0
										},
										{
											"nil",
											0
										},
										{
											"type",
											0
										}
									};
								}
								int num2;
								if (XmlSchemaValidatingReader.<>f__switch$map0.TryGetValue(localName, out num2))
								{
									if (num2 == 0)
									{
										goto IL_230;
									}
								}
							}
							goto IL_202;
						}
						if (num != 1)
						{
							goto IL_202;
						}
						IL_230:
						if (!this.reader.MoveToNextAttribute())
						{
							break;
						}
						continue;
						IL_202:
						this.v.ValidateAttribute(this.reader.LocalName, this.reader.NamespaceURI, this.getter, this.xsinfo);
						goto IL_230;
					}
					this.reader.MoveToElement();
				}
				this.v.GetUnspecifiedDefaultAttributes(this.defaultAttributesCache);
				this.defaultAttributes = (XmlSchemaAttribute[])this.defaultAttributesCache.ToArray(typeof(XmlSchemaAttribute));
				this.v.ValidateEndOfAttributes(this.xsinfo);
				this.defaultAttributesCache.Clear();
				if (!this.reader.IsEmptyElement)
				{
					return true;
				}
				break;
			}
			default:
				switch (nodeType)
				{
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					this.v.ValidateWhitespace(this.getter);
					return true;
				case XmlNodeType.EndElement:
					break;
				default:
					return true;
				}
				break;
			case XmlNodeType.Text:
				this.v.ValidateText(this.getter);
				return true;
			}
			this.v.ValidateEndElement(this.xsinfo);
			return true;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00007AB8 File Offset: 0x00005CB8
		public override bool ReadAttributeValue()
		{
			if (this.currentDefaultAttribute < 0)
			{
				return this.reader.ReadAttributeValue();
			}
			if (this.defaultAttributeConsumed)
			{
				return false;
			}
			this.defaultAttributeConsumed = true;
			return true;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00007AE8 File Offset: 0x00005CE8
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00007AF8 File Offset: 0x00005CF8
		public bool IsNil
		{
			get
			{
				return this.xsinfo.IsNil;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00007B08 File Offset: 0x00005D08
		public XmlSchemaSimpleType MemberType
		{
			get
			{
				return this.xsinfo.MemberType;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00007B18 File Offset: 0x00005D18
		public XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.xsinfo.SchemaAttribute;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00007B28 File Offset: 0x00005D28
		public XmlSchemaElement SchemaElement
		{
			get
			{
				return this.xsinfo.SchemaElement;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00007B38 File Offset: 0x00005D38
		public XmlSchemaValidity Validity
		{
			get
			{
				return this.xsinfo.Validity;
			}
		}

		// Token: 0x040000D8 RID: 216
		private static readonly XmlSchemaAttribute[] emptyAttributeArray = new XmlSchemaAttribute[0];

		// Token: 0x040000D9 RID: 217
		private XmlReader reader;

		// Token: 0x040000DA RID: 218
		private XmlSchemaValidationFlags options;

		// Token: 0x040000DB RID: 219
		private XmlSchemaValidator v;

		// Token: 0x040000DC RID: 220
		private XmlValueGetter getter;

		// Token: 0x040000DD RID: 221
		private XmlSchemaInfo xsinfo;

		// Token: 0x040000DE RID: 222
		private IXmlLineInfo readerLineInfo;

		// Token: 0x040000DF RID: 223
		private ValidationType validationType;

		// Token: 0x040000E0 RID: 224
		private IXmlNamespaceResolver nsResolver;

		// Token: 0x040000E1 RID: 225
		private XmlSchemaAttribute[] defaultAttributes = XmlSchemaValidatingReader.emptyAttributeArray;

		// Token: 0x040000E2 RID: 226
		private int currentDefaultAttribute = -1;

		// Token: 0x040000E3 RID: 227
		private ArrayList defaultAttributesCache = new ArrayList();

		// Token: 0x040000E4 RID: 228
		private bool defaultAttributeConsumed;

		// Token: 0x040000E5 RID: 229
		private XmlSchemaType currentAttrType;

		// Token: 0x040000E6 RID: 230
		private bool validationDone;

		// Token: 0x040000E7 RID: 231
		private XmlSchemaElement element;
	}
}
