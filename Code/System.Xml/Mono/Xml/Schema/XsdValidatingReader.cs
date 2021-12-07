using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x0200002F RID: 47
	internal class XsdValidatingReader : XmlReader, IHasXmlParserContext, IHasXmlSchemaInfo, IXmlLineInfo
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00009BF0 File Offset: 0x00007DF0
		public XsdValidatingReader(XmlReader reader)
		{
			this.reader = reader;
			this.readerLineInfo = (reader as IXmlLineInfo);
			this.sourceReaderSchemaInfo = (reader as IHasXmlSchemaInfo);
			this.schemas.ValidationEventHandler += this.ValidationEventHandler;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00009CC8 File Offset: 0x00007EC8
		private XsdValidationContext Context
		{
			get
			{
				return this.state.Context;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00009CD8 File Offset: 0x00007ED8
		internal ArrayList CurrentKeyFieldConsumers
		{
			get
			{
				if (this.currentKeyFieldConsumers == null)
				{
					this.currentKeyFieldConsumers = new ArrayList();
				}
				return this.currentKeyFieldConsumers;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00009CF8 File Offset: 0x00007EF8
		public int XsiNilDepth
		{
			get
			{
				return this.xsiNilDepth;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00009D00 File Offset: 0x00007F00
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00009D08 File Offset: 0x00007F08
		public bool Namespaces
		{
			get
			{
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00009D14 File Offset: 0x00007F14
		public XmlResolver XmlResolver
		{
			set
			{
				this.resolver = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00009D20 File Offset: 0x00007F20
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00009D28 File Offset: 0x00007F28
		public XmlSchemaSet Schemas
		{
			get
			{
				return this.schemas;
			}
			set
			{
				if (this.validationStarted)
				{
					throw new InvalidOperationException("Schemas must be set before the first call to Read().");
				}
				this.schemas = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00009D48 File Offset: 0x00007F48
		public object SchemaType
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
						return this.SourceReaderSchemaType;
					}
					if (this.currentAttrType == null)
					{
						XmlSchemaComplexType xmlSchemaComplexType = this.Context.ActualType as XmlSchemaComplexType;
						if (xmlSchemaComplexType != null)
						{
							XmlSchemaAttribute xmlSchemaAttribute = xmlSchemaComplexType.AttributeUses[new XmlQualifiedName(this.LocalName, this.NamespaceURI)] as XmlSchemaAttribute;
							if (xmlSchemaAttribute != null)
							{
								this.currentAttrType = xmlSchemaAttribute.AttributeType;
							}
							return this.currentAttrType;
						}
						this.currentAttrType = this.SourceReaderSchemaType;
					}
					return this.currentAttrType;
				}
				else
				{
					if (this.Context.ActualType != null)
					{
						return this.Context.ActualType;
					}
					return this.SourceReaderSchemaType;
				}
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00009E18 File Offset: 0x00008018
		private object SourceReaderSchemaType
		{
			get
			{
				return (this.sourceReaderSchemaInfo == null) ? null : this.sourceReaderSchemaInfo.SchemaType;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00009E38 File Offset: 0x00008038
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00009E40 File Offset: 0x00008040
		public ValidationType ValidationType
		{
			get
			{
				return this.validationType;
			}
			set
			{
				if (this.validationStarted)
				{
					throw new InvalidOperationException("ValidationType must be set before reading.");
				}
				this.validationType = value;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009E60 File Offset: 0x00008060
		public object ReadTypedValue()
		{
			object result = XmlSchemaUtil.ReadTypedValue(this, this.SchemaType, this.NamespaceManager, this.storedCharacters);
			this.storedCharacters.Length = 0;
			return result;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00009E94 File Offset: 0x00008094
		public override int AttributeCount
		{
			get
			{
				return this.reader.AttributeCount + this.defaultAttributes.Length;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00009EAC File Offset: 0x000080AC
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00009EBC File Offset: 0x000080BC
		public override bool CanResolveEntity
		{
			get
			{
				return this.reader.CanResolveEntity;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00009ECC File Offset: 0x000080CC
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00009F18 File Offset: 0x00008118
		public override bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00009F28 File Offset: 0x00008128
		public override bool HasValue
		{
			get
			{
				return this.currentDefaultAttribute >= 0 || this.reader.HasValue;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00009F44 File Offset: 0x00008144
		public override bool IsDefault
		{
			get
			{
				return this.currentDefaultAttribute >= 0 || this.reader.IsDefault;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00009F60 File Offset: 0x00008160
		public override bool IsEmptyElement
		{
			get
			{
				return this.currentDefaultAttribute < 0 && this.reader.IsEmptyElement;
			}
		}

		// Token: 0x1700004C RID: 76
		public override string this[int i]
		{
			get
			{
				return this.GetAttribute(i);
			}
		}

		// Token: 0x1700004D RID: 77
		public override string this[string name]
		{
			get
			{
				return this.GetAttribute(name);
			}
		}

		// Token: 0x1700004E RID: 78
		public override string this[string localName, string ns]
		{
			get
			{
				return this.GetAttribute(localName, ns);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00009FA0 File Offset: 0x000081A0
		public int LineNumber
		{
			get
			{
				return (this.readerLineInfo == null) ? 0 : this.readerLineInfo.LineNumber;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00009FC0 File Offset: 0x000081C0
		public int LinePosition
		{
			get
			{
				return (this.readerLineInfo == null) ? 0 : this.readerLineInfo.LinePosition;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00009FE0 File Offset: 0x000081E0
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000A030 File Offset: 0x00008230
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000A0A8 File Offset: 0x000082A8
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000A0F8 File Offset: 0x000082F8
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000A108 File Offset: 0x00008308
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

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000A13C File Offset: 0x0000833C
		public XmlParserContext ParserContext
		{
			get
			{
				return XmlSchemaUtil.GetParserContext(this.reader);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000A14C File Offset: 0x0000834C
		internal XmlNamespaceManager NamespaceManager
		{
			get
			{
				return (this.ParserContext == null) ? null : this.ParserContext.NamespaceManager;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000A16C File Offset: 0x0000836C
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
				string text = (this.NamespaceManager == null) ? null : this.NamespaceManager.LookupPrefix(qualifiedName.Namespace, false);
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000A1E8 File Offset: 0x000083E8
		public override char QuoteChar
		{
			get
			{
				return this.reader.QuoteChar;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000A1F8 File Offset: 0x000083F8
		public override ReadState ReadState
		{
			get
			{
				return this.reader.ReadState;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000A208 File Offset: 0x00008408
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000A25C File Offset: 0x0000845C
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000A2BC File Offset: 0x000084BC
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

		// Token: 0x0600014D RID: 333 RVA: 0x0000A330 File Offset: 0x00008530
		private void HandleError(string error)
		{
			this.HandleError(error, null);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000A33C File Offset: 0x0000853C
		private void HandleError(string error, Exception innerException)
		{
			this.HandleError(error, innerException, false);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000A348 File Offset: 0x00008548
		private void HandleError(string error, Exception innerException, bool isWarning)
		{
			if (this.ValidationType == ValidationType.None)
			{
				return;
			}
			XmlSchemaValidationException schemaException = new XmlSchemaValidationException(error, this, this.BaseURI, null, innerException);
			this.HandleError(schemaException, isWarning);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000A37C File Offset: 0x0000857C
		private void HandleError(XmlSchemaValidationException schemaException)
		{
			this.HandleError(schemaException, false);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000A388 File Offset: 0x00008588
		private void HandleError(XmlSchemaValidationException schemaException, bool isWarning)
		{
			if (this.ValidationType == ValidationType.None)
			{
				return;
			}
			ValidationEventArgs validationEventArgs = new ValidationEventArgs(schemaException, schemaException.Message, (!isWarning) ? XmlSeverityType.Error : XmlSeverityType.Warning);
			if (this.ValidationEventHandler != null)
			{
				this.ValidationEventHandler(this, validationEventArgs);
			}
			else if (validationEventArgs.Severity == XmlSeverityType.Error)
			{
				throw validationEventArgs.Exception;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000A3EC File Offset: 0x000085EC
		private XmlSchemaElement FindElement(string name, string ns)
		{
			return (XmlSchemaElement)this.schemas.GlobalElements[new XmlQualifiedName(name, ns)];
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000A40C File Offset: 0x0000860C
		private XmlSchemaType FindType(XmlQualifiedName qname)
		{
			return (XmlSchemaType)this.schemas.GlobalTypes[qname];
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000A424 File Offset: 0x00008624
		private void ValidateStartElementParticle()
		{
			if (this.Context.State == null)
			{
				return;
			}
			this.Context.XsiType = null;
			this.state.CurrentElement = null;
			this.Context.EvaluateStartElement(this.reader.LocalName, this.reader.NamespaceURI);
			if (this.Context.IsInvalid)
			{
				this.HandleError("Invalid start element: " + this.reader.NamespaceURI + ":" + this.reader.LocalName);
			}
			this.Context.PushCurrentElement(this.state.CurrentElement);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A4CC File Offset: 0x000086CC
		private void ValidateEndElementParticle()
		{
			if (this.Context.State != null && !this.Context.EvaluateEndElement())
			{
				this.HandleError("Invalid end element: " + this.reader.Name);
			}
			this.Context.PopCurrentElement();
			this.state.PopContext();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A52C File Offset: 0x0000872C
		private void ValidateCharacters()
		{
			if (this.xsiNilDepth >= 0 && this.xsiNilDepth < this.reader.Depth)
			{
				this.HandleError("Element item appeared, while current element context is nil.");
			}
			if (this.shouldValidateCharacters)
			{
				this.storedCharacters.Append(this.reader.Value);
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A588 File Offset: 0x00008788
		private void ValidateEndSimpleContent()
		{
			if (this.shouldValidateCharacters)
			{
				this.ValidateEndSimpleContentCore();
			}
			this.shouldValidateCharacters = false;
			this.storedCharacters.Length = 0;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A5BC File Offset: 0x000087BC
		private void ValidateEndSimpleContentCore()
		{
			if (this.Context.ActualType == null)
			{
				return;
			}
			string text = this.storedCharacters.ToString();
			if (text.Length == 0 && this.Context.Element != null && this.Context.Element.ValidatedDefaultValue != null)
			{
				text = this.Context.Element.ValidatedDefaultValue;
			}
			XmlSchemaDatatype xmlSchemaDatatype = this.Context.ActualType as XmlSchemaDatatype;
			XmlSchemaSimpleType xmlSchemaSimpleType = this.Context.ActualType as XmlSchemaSimpleType;
			if (xmlSchemaDatatype == null)
			{
				if (xmlSchemaSimpleType != null)
				{
					xmlSchemaDatatype = xmlSchemaSimpleType.Datatype;
				}
				else
				{
					XmlSchemaComplexType xmlSchemaComplexType = this.Context.ActualType as XmlSchemaComplexType;
					xmlSchemaDatatype = xmlSchemaComplexType.Datatype;
					XmlSchemaContentType contentType = xmlSchemaComplexType.ContentType;
					if (contentType != XmlSchemaContentType.Empty)
					{
						if (contentType == XmlSchemaContentType.ElementOnly)
						{
							if (text.Length > 0 && !XmlChar.IsWhitespace(text))
							{
								this.HandleError("Character content not allowed.");
							}
						}
					}
					else if (text.Length > 0)
					{
						this.HandleError("Character content not allowed.");
					}
				}
			}
			if (xmlSchemaDatatype != null)
			{
				if (this.Context.Element != null && this.Context.Element.ValidatedFixedValue != null && text != this.Context.Element.ValidatedFixedValue)
				{
					this.HandleError("Fixed value constraint was not satisfied.");
				}
				this.AssessStringValid(xmlSchemaSimpleType, xmlSchemaDatatype, text);
			}
			if (this.checkKeyConstraints)
			{
				this.ValidateSimpleContentIdentity(xmlSchemaDatatype, text);
			}
			this.shouldValidateCharacters = false;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A750 File Offset: 0x00008950
		private void AssessStringValid(XmlSchemaSimpleType st, XmlSchemaDatatype dt, string value)
		{
			XmlSchemaDatatype xmlSchemaDatatype = dt;
			if (st != null)
			{
				string normalized = xmlSchemaDatatype.Normalize(value);
				this.ValidateRestrictedSimpleTypeValue(st, ref xmlSchemaDatatype, normalized);
			}
			if (xmlSchemaDatatype != null)
			{
				try
				{
					xmlSchemaDatatype.ParseValue(value, this.NameTable, this.NamespaceManager);
				}
				catch (Exception innerException)
				{
					this.HandleError("Invalidly typed data was specified.", innerException);
				}
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A7C4 File Offset: 0x000089C4
		private void ValidateRestrictedSimpleTypeValue(XmlSchemaSimpleType st, ref XmlSchemaDatatype dt, string normalized)
		{
			XmlSchemaDerivationMethod derivedBy = st.DerivedBy;
			if (derivedBy != XmlSchemaDerivationMethod.Restriction)
			{
				if (derivedBy != XmlSchemaDerivationMethod.List)
				{
					if (derivedBy == XmlSchemaDerivationMethod.Union)
					{
						XmlSchemaSimpleTypeUnion xmlSchemaSimpleTypeUnion = st.Content as XmlSchemaSimpleTypeUnion;
						bool flag = false;
						object[] validatedTypes = xmlSchemaSimpleTypeUnion.ValidatedTypes;
						int i = 0;
						while (i < validatedTypes.Length)
						{
							object obj = validatedTypes[i];
							XmlSchemaDatatype xmlSchemaDatatype = obj as XmlSchemaDatatype;
							XmlSchemaSimpleType xmlSchemaSimpleType = obj as XmlSchemaSimpleType;
							if (xmlSchemaDatatype != null)
							{
								try
								{
									xmlSchemaDatatype.ParseValue(normalized, this.NameTable, this.NamespaceManager);
								}
								catch (Exception)
								{
									goto IL_16E;
								}
								goto IL_166;
							}
							try
							{
								this.AssessStringValid(xmlSchemaSimpleType, xmlSchemaSimpleType.Datatype, normalized);
							}
							catch (XmlSchemaValidationException)
							{
								goto IL_16E;
							}
							goto IL_166;
							IL_16E:
							i++;
							continue;
							IL_166:
							flag = true;
							break;
						}
						if (!flag)
						{
							this.HandleError("Union type value contains one or more invalid values.");
						}
					}
				}
				else
				{
					XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = st.Content as XmlSchemaSimpleTypeList;
					string[] array = normalized.Split(XmlChar.WhitespaceChars);
					XmlSchemaDatatype xmlSchemaDatatype = xmlSchemaSimpleTypeList.ValidatedListItemType as XmlSchemaDatatype;
					XmlSchemaSimpleType xmlSchemaSimpleType = xmlSchemaSimpleTypeList.ValidatedListItemType as XmlSchemaSimpleType;
					foreach (string text in array)
					{
						if (!(text == string.Empty))
						{
							if (xmlSchemaDatatype != null)
							{
								try
								{
									xmlSchemaDatatype.ParseValue(text, this.NameTable, this.NamespaceManager);
								}
								catch (Exception innerException)
								{
									this.HandleError("List type value contains one or more invalid values.", innerException);
									break;
								}
							}
							else
							{
								this.AssessStringValid(xmlSchemaSimpleType, xmlSchemaSimpleType.Datatype, text);
							}
						}
					}
				}
			}
			else
			{
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = st.Content as XmlSchemaSimpleTypeRestriction;
				if (xmlSchemaSimpleTypeRestriction != null)
				{
					XmlSchemaSimpleType xmlSchemaSimpleType2 = st.BaseXmlSchemaType as XmlSchemaSimpleType;
					if (xmlSchemaSimpleType2 != null)
					{
						this.AssessStringValid(xmlSchemaSimpleType2, dt, normalized);
					}
					if (!xmlSchemaSimpleTypeRestriction.ValidateValueWithFacets(normalized, this.NameTable, this.NamespaceManager))
					{
						this.HandleError("Specified value was invalid against the facets.");
						return;
					}
				}
				dt = st.Datatype;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000AA24 File Offset: 0x00008C24
		private object GetXsiType(string name)
		{
			XmlQualifiedName xmlQualifiedName = XmlQualifiedName.Parse(name, this);
			object result;
			if (xmlQualifiedName == XmlSchemaComplexType.AnyTypeName)
			{
				result = XmlSchemaComplexType.AnyType;
			}
			else if (XmlSchemaUtil.IsBuiltInDatatypeName(xmlQualifiedName))
			{
				result = XmlSchemaDatatype.FromName(xmlQualifiedName);
			}
			else
			{
				result = this.FindType(xmlQualifiedName);
			}
			return result;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000AA78 File Offset: 0x00008C78
		private void AssessLocalTypeDerivationOK(object xsiType, object baseType, XmlSchemaDerivationMethod flag)
		{
			XmlSchemaType xmlSchemaType = xsiType as XmlSchemaType;
			XmlSchemaComplexType xmlSchemaComplexType = baseType as XmlSchemaComplexType;
			XmlSchemaComplexType xmlSchemaComplexType2 = xmlSchemaType as XmlSchemaComplexType;
			if (xsiType != baseType)
			{
				if (xmlSchemaComplexType != null)
				{
					flag |= xmlSchemaComplexType.BlockResolved;
				}
				if (flag == XmlSchemaDerivationMethod.All)
				{
					this.HandleError("Prohibited element type substitution.");
					return;
				}
				if (xmlSchemaType != null && (flag & xmlSchemaType.DerivedBy) != XmlSchemaDerivationMethod.Empty)
				{
					this.HandleError("Prohibited element type substitution.");
					return;
				}
			}
			if (xmlSchemaComplexType2 != null)
			{
				try
				{
					xmlSchemaComplexType2.ValidateTypeDerivationOK(baseType, null, null);
				}
				catch (XmlSchemaValidationException schemaException)
				{
					this.HandleError(schemaException);
				}
			}
			else
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = xsiType as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType != null)
				{
					try
					{
						xmlSchemaSimpleType.ValidateTypeDerivationOK(baseType, null, null, true);
					}
					catch (XmlSchemaValidationException schemaException2)
					{
						this.HandleError(schemaException2);
					}
				}
				else if (!(xsiType is XmlSchemaDatatype))
				{
					this.HandleError("Primitive data type cannot be derived type using xsi:type specification.");
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000AB90 File Offset: 0x00008D90
		private void AssessStartElementSchemaValidity()
		{
			if (this.xsiNilDepth >= 0 && this.xsiNilDepth < this.reader.Depth)
			{
				this.HandleError("Element item appeared, while current element context is nil.");
			}
			this.ValidateStartElementParticle();
			string text = this.reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (text != null)
			{
				text = text.Trim(XmlChar.WhitespaceChars);
			}
			bool flag = text == "true";
			if (flag && this.xsiNilDepth < 0)
			{
				this.xsiNilDepth = this.reader.Depth;
			}
			string text2 = this.reader.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
			if (text2 != null)
			{
				text2 = text2.Trim(XmlChar.WhitespaceChars);
				object xsiType = this.GetXsiType(text2);
				if (xsiType == null)
				{
					this.HandleError("The instance type was not found: " + text2 + " .");
				}
				else
				{
					XmlSchemaType xmlSchemaType = xsiType as XmlSchemaType;
					if (xmlSchemaType != null && this.Context.Element != null)
					{
						XmlSchemaType xmlSchemaType2 = this.Context.Element.ElementType as XmlSchemaType;
						if (xmlSchemaType2 != null && (xmlSchemaType.DerivedBy & xmlSchemaType2.FinalResolved) != XmlSchemaDerivationMethod.Empty)
						{
							this.HandleError("The instance type is prohibited by the type of the context element.");
						}
						if (xmlSchemaType2 != xsiType && (xmlSchemaType.DerivedBy & this.Context.Element.BlockResolved) != XmlSchemaDerivationMethod.Empty)
						{
							this.HandleError("The instance type is prohibited by the context element.");
						}
					}
					XmlSchemaComplexType xmlSchemaComplexType = xsiType as XmlSchemaComplexType;
					if (xmlSchemaComplexType != null && xmlSchemaComplexType.IsAbstract)
					{
						this.HandleError("The instance type is abstract: " + text2 + " .");
					}
					else
					{
						if (this.Context.Element != null)
						{
							this.AssessLocalTypeDerivationOK(xsiType, this.Context.Element.ElementType, this.Context.Element.BlockResolved);
						}
						this.AssessStartElementLocallyValidType(xsiType);
						this.Context.XsiType = xsiType;
					}
				}
			}
			if (this.Context.Element == null)
			{
				this.state.CurrentElement = this.FindElement(this.reader.LocalName, this.reader.NamespaceURI);
				this.Context.PushCurrentElement(this.state.CurrentElement);
			}
			if (this.Context.Element != null)
			{
				if (this.Context.XsiType == null)
				{
					this.AssessElementLocallyValidElement(text);
				}
			}
			else
			{
				XmlSchemaContentProcessing processContents = this.state.ProcessContents;
				if (processContents != XmlSchemaContentProcessing.Skip)
				{
					if (processContents != XmlSchemaContentProcessing.Lax)
					{
						if (text2 == null && (this.schemas.Contains(this.reader.NamespaceURI) || !this.schemas.MissedSubComponents(this.reader.NamespaceURI)))
						{
							this.HandleError("Element declaration for " + new XmlQualifiedName(this.reader.LocalName, this.reader.NamespaceURI) + " is missing.");
						}
					}
				}
			}
			this.state.PushContext();
			XsdValidationState xsdValidationState = null;
			if (this.state.ProcessContents == XmlSchemaContentProcessing.Skip)
			{
				this.skipValidationDepth = this.reader.Depth;
			}
			else
			{
				XmlSchemaComplexType xmlSchemaComplexType2 = this.SchemaType as XmlSchemaComplexType;
				if (xmlSchemaComplexType2 != null)
				{
					xsdValidationState = this.state.Create(xmlSchemaComplexType2.ValidatableParticle);
				}
				else if (this.state.ProcessContents == XmlSchemaContentProcessing.Lax)
				{
					xsdValidationState = this.state.Create(XmlSchemaAny.AnyTypeContent);
				}
				else
				{
					xsdValidationState = this.state.Create(XmlSchemaParticle.Empty);
				}
			}
			this.Context.State = xsdValidationState;
			if (this.checkKeyConstraints)
			{
				this.ValidateKeySelectors();
				this.ValidateKeyFields();
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000AF5C File Offset: 0x0000915C
		private void AssessElementLocallyValidElement(string xsiNilValue)
		{
			XmlSchemaElement element = this.Context.Element;
			XmlQualifiedName arg = new XmlQualifiedName(this.reader.LocalName, this.reader.NamespaceURI);
			if (element == null)
			{
				this.HandleError("Element declaration is required for " + arg);
			}
			if (element.ActualIsAbstract)
			{
				this.HandleError("Abstract element declaration was specified for " + arg);
			}
			if (!element.ActualIsNillable && xsiNilValue != null)
			{
				this.HandleError("This element declaration is not nillable: " + arg);
			}
			else if (xsiNilValue == "true" && element.ValidatedFixedValue != null)
			{
				this.HandleError("Schema instance nil was specified, where the element declaration for " + arg + "has fixed value constraints.");
			}
			string attribute = this.reader.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null)
			{
				this.Context.XsiType = this.GetXsiType(attribute);
				this.AssessLocalTypeDerivationOK(this.Context.XsiType, element.ElementType, element.BlockResolved);
			}
			else
			{
				this.Context.XsiType = null;
			}
			if (element.ElementType != null)
			{
				this.AssessStartElementLocallyValidType(this.SchemaType);
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000B090 File Offset: 0x00009290
		private void AssessStartElementLocallyValidType(object schemaType)
		{
			if (schemaType == null)
			{
				this.HandleError("Schema type does not exist.");
				return;
			}
			XmlSchemaComplexType xmlSchemaComplexType = schemaType as XmlSchemaComplexType;
			XmlSchemaSimpleType xmlSchemaSimpleType = schemaType as XmlSchemaSimpleType;
			if (xmlSchemaSimpleType != null)
			{
				while (this.reader.MoveToNextAttribute())
				{
					if (!(this.reader.NamespaceURI == "http://www.w3.org/2000/xmlns/"))
					{
						if (this.reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema-instance")
						{
							this.HandleError("Current simple type cannot accept attributes other than schema instance namespace.");
						}
						string localName = this.reader.LocalName;
						if (localName != null)
						{
							if (XsdValidatingReader.<>f__switch$map3 == null)
							{
								XsdValidatingReader.<>f__switch$map3 = new Dictionary<string, int>(4)
								{
									{
										"type",
										0
									},
									{
										"nil",
										0
									},
									{
										"schemaLocation",
										0
									},
									{
										"noNamespaceSchemaLocation",
										0
									}
								};
							}
							int num;
							if (XsdValidatingReader.<>f__switch$map3.TryGetValue(localName, out num))
							{
								if (num == 0)
								{
									continue;
								}
							}
						}
						this.HandleError("Unknown schema instance namespace attribute: " + this.reader.LocalName);
					}
				}
				this.reader.MoveToElement();
			}
			else if (xmlSchemaComplexType != null)
			{
				if (xmlSchemaComplexType.IsAbstract)
				{
					this.HandleError("Target complex type is abstract.");
					return;
				}
				this.AssessElementLocallyValidComplexType(xmlSchemaComplexType);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000B1F0 File Offset: 0x000093F0
		private void AssessElementLocallyValidComplexType(XmlSchemaComplexType cType)
		{
			if (cType.IsAbstract)
			{
				this.HandleError("Target complex type is abstract.");
			}
			if (this.reader.MoveToFirstAttribute())
			{
				for (;;)
				{
					string namespaceURI = this.reader.NamespaceURI;
					if (namespaceURI == null)
					{
						goto IL_91;
					}
					if (XsdValidatingReader.<>f__switch$map4 == null)
					{
						XsdValidatingReader.<>f__switch$map4 = new Dictionary<string, int>(2)
						{
							{
								"http://www.w3.org/2000/xmlns/",
								0
							},
							{
								"http://www.w3.org/2001/XMLSchema-instance",
								0
							}
						};
					}
					int num;
					if (!XsdValidatingReader.<>f__switch$map4.TryGetValue(namespaceURI, out num))
					{
						goto IL_91;
					}
					if (num != 0)
					{
						goto IL_91;
					}
					IL_F8:
					if (!this.reader.MoveToNextAttribute())
					{
						break;
					}
					continue;
					IL_91:
					XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.reader.LocalName, this.reader.NamespaceURI);
					XmlSchemaObject xmlSchemaObject = XmlSchemaUtil.FindAttributeDeclaration(this.reader.NamespaceURI, this.schemas, cType, xmlQualifiedName);
					if (xmlSchemaObject == null)
					{
						this.HandleError("Attribute declaration was not found for " + xmlQualifiedName);
					}
					XmlSchemaAttribute xmlSchemaAttribute = xmlSchemaObject as XmlSchemaAttribute;
					if (xmlSchemaAttribute != null)
					{
						this.AssessAttributeLocallyValidUse(xmlSchemaAttribute);
						this.AssessAttributeLocallyValid(xmlSchemaAttribute);
						goto IL_F8;
					}
					goto IL_F8;
				}
				this.reader.MoveToElement();
			}
			foreach (object obj in cType.AttributeUses)
			{
				XmlSchemaAttribute xmlSchemaAttribute2 = (XmlSchemaAttribute)((DictionaryEntry)obj).Value;
				if (this.reader[xmlSchemaAttribute2.QualifiedName.Name, xmlSchemaAttribute2.QualifiedName.Namespace] == null)
				{
					if (xmlSchemaAttribute2.ValidatedUse == XmlSchemaUse.Required && xmlSchemaAttribute2.ValidatedFixedValue == null)
					{
						this.HandleError("Required attribute " + xmlSchemaAttribute2.QualifiedName + " was not found.");
					}
					else if (xmlSchemaAttribute2.ValidatedDefaultValue != null || xmlSchemaAttribute2.ValidatedFixedValue != null)
					{
						this.defaultAttributesCache.Add(xmlSchemaAttribute2);
					}
				}
			}
			if (this.defaultAttributesCache.Count == 0)
			{
				this.defaultAttributes = XsdValidatingReader.emptyAttributeArray;
			}
			else
			{
				this.defaultAttributes = (XmlSchemaAttribute[])this.defaultAttributesCache.ToArray(typeof(XmlSchemaAttribute));
			}
			this.defaultAttributesCache.Clear();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000B454 File Offset: 0x00009654
		private void AssessAttributeLocallyValid(XmlSchemaAttribute attr)
		{
			if (attr.AttributeType == null)
			{
				this.HandleError("Attribute type is missing for " + attr.QualifiedName);
			}
			XmlSchemaDatatype xmlSchemaDatatype = attr.AttributeType as XmlSchemaDatatype;
			if (xmlSchemaDatatype == null)
			{
				xmlSchemaDatatype = ((XmlSchemaSimpleType)attr.AttributeType).Datatype;
			}
			if (xmlSchemaDatatype != XmlSchemaSimpleType.AnySimpleType || attr.ValidatedFixedValue != null)
			{
				string text = xmlSchemaDatatype.Normalize(this.reader.Value);
				object parsedValue = null;
				XmlSchemaSimpleType xmlSchemaSimpleType = attr.AttributeType as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType != null)
				{
					this.ValidateRestrictedSimpleTypeValue(xmlSchemaSimpleType, ref xmlSchemaDatatype, text);
				}
				try
				{
					parsedValue = xmlSchemaDatatype.ParseValue(text, this.reader.NameTable, this.NamespaceManager);
				}
				catch (Exception innerException)
				{
					this.HandleError("Attribute value is invalid against its data type " + xmlSchemaDatatype.TokenizedType, innerException);
				}
				if (attr.ValidatedFixedValue != null && attr.ValidatedFixedValue != text)
				{
					this.HandleError("The value of the attribute " + attr.QualifiedName + " does not match with its fixed value.");
					parsedValue = xmlSchemaDatatype.ParseValue(attr.ValidatedFixedValue, this.reader.NameTable, this.NamespaceManager);
				}
				if (this.checkIdentity)
				{
					string text2 = this.idManager.AssessEachAttributeIdentityConstraint(xmlSchemaDatatype, parsedValue, ((XmlQualifiedName)this.elementQNameStack[this.elementQNameStack.Count - 1]).Name);
					if (text2 != null)
					{
						this.HandleError(text2);
					}
				}
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000B5EC File Offset: 0x000097EC
		private void AssessAttributeLocallyValidUse(XmlSchemaAttribute attr)
		{
			if (attr.ValidatedUse == XmlSchemaUse.Prohibited)
			{
				this.HandleError("Attribute " + attr.QualifiedName + " is prohibited in this context.");
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000B620 File Offset: 0x00009820
		private void AssessEndElementSchemaValidity()
		{
			this.ValidateEndSimpleContent();
			this.ValidateEndElementParticle();
			if (this.checkKeyConstraints)
			{
				this.ValidateEndElementKeyConstraints();
			}
			if (this.xsiNilDepth == this.reader.Depth)
			{
				this.xsiNilDepth = -1;
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B668 File Offset: 0x00009868
		private void ValidateEndElementKeyConstraints()
		{
			for (int i = 0; i < this.keyTables.Count; i++)
			{
				XsdKeyTable xsdKeyTable = this.keyTables[i] as XsdKeyTable;
				if (xsdKeyTable.StartDepth == this.reader.Depth)
				{
					this.EndIdentityValidation(xsdKeyTable);
				}
				else
				{
					for (int j = 0; j < xsdKeyTable.Entries.Count; j++)
					{
						XsdKeyEntry xsdKeyEntry = xsdKeyTable.Entries[j];
						if (xsdKeyEntry.StartDepth == this.reader.Depth)
						{
							if (xsdKeyEntry.KeyFound)
							{
								xsdKeyTable.FinishedEntries.Add(xsdKeyEntry);
							}
							else if (xsdKeyTable.SourceSchemaIdentity is XmlSchemaKey)
							{
								this.HandleError("Key sequence is missing.");
							}
							xsdKeyTable.Entries.RemoveAt(j);
							j--;
						}
						else
						{
							for (int k = 0; k < xsdKeyEntry.KeyFields.Count; k++)
							{
								XsdKeyEntryField xsdKeyEntryField = xsdKeyEntry.KeyFields[k];
								if (!xsdKeyEntryField.FieldFound && xsdKeyEntryField.FieldFoundDepth == this.reader.Depth)
								{
									xsdKeyEntryField.FieldFoundDepth = 0;
									xsdKeyEntryField.FieldFoundPath = null;
								}
							}
						}
					}
				}
			}
			for (int l = 0; l < this.keyTables.Count; l++)
			{
				XsdKeyTable xsdKeyTable2 = this.keyTables[l] as XsdKeyTable;
				if (xsdKeyTable2.StartDepth == this.reader.Depth)
				{
					this.keyTables.RemoveAt(l);
					l--;
				}
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000B810 File Offset: 0x00009A10
		private void ValidateKeySelectors()
		{
			if (this.tmpKeyrefPool != null)
			{
				this.tmpKeyrefPool.Clear();
			}
			if (this.Context.Element != null && this.Context.Element.Constraints.Count > 0)
			{
				for (int i = 0; i < this.Context.Element.Constraints.Count; i++)
				{
					XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)this.Context.Element.Constraints[i];
					XsdKeyTable value = this.CreateNewKeyTable(xmlSchemaIdentityConstraint);
					if (xmlSchemaIdentityConstraint is XmlSchemaKeyref)
					{
						if (this.tmpKeyrefPool == null)
						{
							this.tmpKeyrefPool = new ArrayList();
						}
						this.tmpKeyrefPool.Add(value);
					}
				}
			}
			for (int j = 0; j < this.keyTables.Count; j++)
			{
				XsdKeyTable xsdKeyTable = (XsdKeyTable)this.keyTables[j];
				if (xsdKeyTable.SelectorMatches(this.elementQNameStack, this.reader.Depth) != null)
				{
					XsdKeyEntry entry = new XsdKeyEntry(xsdKeyTable, this.reader.Depth, this.readerLineInfo);
					xsdKeyTable.Entries.Add(entry);
				}
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000B94C File Offset: 0x00009B4C
		private void ValidateKeyFields()
		{
			for (int i = 0; i < this.keyTables.Count; i++)
			{
				XsdKeyTable xsdKeyTable = (XsdKeyTable)this.keyTables[i];
				for (int j = 0; j < xsdKeyTable.Entries.Count; j++)
				{
					try
					{
						this.ProcessKeyEntry(xsdKeyTable.Entries[j]);
					}
					catch (XmlSchemaValidationException schemaException)
					{
						this.HandleError(schemaException);
					}
				}
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000B9E4 File Offset: 0x00009BE4
		private void ProcessKeyEntry(XsdKeyEntry entry)
		{
			bool isXsiNil = this.XsiNilDepth == this.Depth;
			entry.ProcessMatch(false, this.elementQNameStack, this, this.NameTable, this.BaseURI, this.SchemaType, this.NamespaceManager, this.readerLineInfo, this.Depth, null, null, null, isXsiNil, this.CurrentKeyFieldConsumers);
			if (this.MoveToFirstAttribute())
			{
				try
				{
					for (;;)
					{
						string namespaceURI = this.NamespaceURI;
						if (namespaceURI == null)
						{
							goto IL_BC;
						}
						if (XsdValidatingReader.<>f__switch$map5 == null)
						{
							XsdValidatingReader.<>f__switch$map5 = new Dictionary<string, int>(2)
							{
								{
									"http://www.w3.org/2000/xmlns/",
									0
								},
								{
									"http://www.w3.org/2001/XMLSchema-instance",
									0
								}
							};
						}
						int num;
						if (!XsdValidatingReader.<>f__switch$map5.TryGetValue(namespaceURI, out num))
						{
							goto IL_BC;
						}
						if (num != 0)
						{
							goto IL_BC;
						}
						IL_15B:
						if (!this.MoveToNextAttribute())
						{
							break;
						}
						continue;
						IL_BC:
						XmlSchemaDatatype xmlSchemaDatatype = this.SchemaType as XmlSchemaDatatype;
						XmlSchemaSimpleType xmlSchemaSimpleType = this.SchemaType as XmlSchemaSimpleType;
						if (xmlSchemaDatatype == null && xmlSchemaSimpleType != null)
						{
							xmlSchemaDatatype = xmlSchemaSimpleType.Datatype;
						}
						object obj = null;
						if (xmlSchemaDatatype != null)
						{
							obj = xmlSchemaDatatype.ParseValue(this.Value, this.NameTable, this.NamespaceManager);
						}
						if (obj == null)
						{
							obj = this.Value;
						}
						entry.ProcessMatch(true, this.elementQNameStack, this, this.NameTable, this.BaseURI, this.SchemaType, this.NamespaceManager, this.readerLineInfo, this.Depth, this.LocalName, this.NamespaceURI, obj, false, this.CurrentKeyFieldConsumers);
						goto IL_15B;
					}
				}
				finally
				{
					this.MoveToElement();
				}
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000BB80 File Offset: 0x00009D80
		private XsdKeyTable CreateNewKeyTable(XmlSchemaIdentityConstraint ident)
		{
			XsdKeyTable xsdKeyTable = new XsdKeyTable(ident);
			xsdKeyTable.StartDepth = this.reader.Depth;
			this.keyTables.Add(xsdKeyTable);
			return xsdKeyTable;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000BBB4 File Offset: 0x00009DB4
		private void ValidateSimpleContentIdentity(XmlSchemaDatatype dt, string value)
		{
			if (this.currentKeyFieldConsumers != null)
			{
				while (this.currentKeyFieldConsumers.Count > 0)
				{
					XsdKeyEntryField xsdKeyEntryField = this.currentKeyFieldConsumers[0] as XsdKeyEntryField;
					if (xsdKeyEntryField.Identity != null)
					{
						this.HandleError("Two or more identical field was found. Former value is '" + xsdKeyEntryField.Identity + "' .");
					}
					object obj = null;
					if (dt != null)
					{
						try
						{
							obj = dt.ParseValue(value, this.NameTable, this.NamespaceManager);
						}
						catch (Exception innerException)
						{
							this.HandleError("Identity value is invalid against its data type " + dt.TokenizedType, innerException);
						}
					}
					if (obj == null)
					{
						obj = value;
					}
					if (!xsdKeyEntryField.SetIdentityField(obj, this.reader.Depth == this.xsiNilDepth, dt as XsdAnySimpleType, this.Depth, this.readerLineInfo))
					{
						this.HandleError("Two or more identical key value was found: '" + value + "' .");
					}
					this.currentKeyFieldConsumers.RemoveAt(0);
				}
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		private void EndIdentityValidation(XsdKeyTable seq)
		{
			ArrayList arrayList = null;
			for (int i = 0; i < seq.Entries.Count; i++)
			{
				XsdKeyEntry xsdKeyEntry = seq.Entries[i];
				if (!xsdKeyEntry.KeyFound)
				{
					if (seq.SourceSchemaIdentity is XmlSchemaKey)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(string.Concat(new object[]
						{
							"line ",
							xsdKeyEntry.SelectorLineNumber,
							"position ",
							xsdKeyEntry.SelectorLinePosition
						}));
					}
				}
			}
			if (arrayList != null)
			{
				this.HandleError("Invalid identity constraints were found. Key was not found. " + string.Join(", ", arrayList.ToArray(typeof(string)) as string[]));
			}
			XmlSchemaKeyref xmlSchemaKeyref = seq.SourceSchemaIdentity as XmlSchemaKeyref;
			if (xmlSchemaKeyref != null)
			{
				this.EndKeyrefValidation(seq, xmlSchemaKeyref.Target);
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000BDCC File Offset: 0x00009FCC
		private void EndKeyrefValidation(XsdKeyTable seq, XmlSchemaIdentityConstraint targetIdent)
		{
			for (int i = this.keyTables.Count - 1; i >= 0; i--)
			{
				XsdKeyTable xsdKeyTable = this.keyTables[i] as XsdKeyTable;
				if (xsdKeyTable.SourceSchemaIdentity == targetIdent)
				{
					seq.ReferencedKey = xsdKeyTable;
					for (int j = 0; j < seq.FinishedEntries.Count; j++)
					{
						XsdKeyEntry xsdKeyEntry = seq.FinishedEntries[j];
						for (int k = 0; k < xsdKeyTable.FinishedEntries.Count; k++)
						{
							XsdKeyEntry other = xsdKeyTable.FinishedEntries[k];
							if (xsdKeyEntry.CompareIdentity(other))
							{
								xsdKeyEntry.KeyRefFound = true;
								break;
							}
						}
					}
				}
			}
			if (seq.ReferencedKey == null)
			{
				this.HandleError("Target key was not found.");
			}
			ArrayList arrayList = null;
			for (int l = 0; l < seq.FinishedEntries.Count; l++)
			{
				XsdKeyEntry xsdKeyEntry2 = seq.FinishedEntries[l];
				if (!xsdKeyEntry2.KeyRefFound)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(string.Concat(new object[]
					{
						" line ",
						xsdKeyEntry2.SelectorLineNumber,
						", position ",
						xsdKeyEntry2.SelectorLinePosition
					}));
				}
			}
			if (arrayList != null)
			{
				this.HandleError("Invalid identity constraints were found. Referenced key was not found: " + string.Join(" / ", arrayList.ToArray(typeof(string)) as string[]));
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000BF6C File Offset: 0x0000A16C
		public override void Close()
		{
			this.reader.Close();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000BF7C File Offset: 0x0000A17C
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

		// Token: 0x0600016E RID: 366 RVA: 0x0000C014 File Offset: 0x0000A214
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

		// Token: 0x0600016F RID: 367 RVA: 0x0000C080 File Offset: 0x0000A280
		private XmlQualifiedName SplitQName(string name)
		{
			if (!XmlChar.IsName(name))
			{
				throw new ArgumentException("Invalid name was specified.", "name");
			}
			Exception ex = null;
			XmlQualifiedName result = XmlSchemaUtil.ToQName(this.reader, name, out ex);
			if (ex != null)
			{
				return XmlQualifiedName.Empty;
			}
			return result;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
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

		// Token: 0x06000171 RID: 369 RVA: 0x0000C124 File Offset: 0x0000A324
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

		// Token: 0x06000172 RID: 370 RVA: 0x0000C168 File Offset: 0x0000A368
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

		// Token: 0x06000173 RID: 371 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		public bool HasLineInfo()
		{
			return this.readerLineInfo != null && this.readerLineInfo.HasLineInfo();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000C1E8 File Offset: 0x0000A3E8
		public override string LookupNamespace(string prefix)
		{
			return this.reader.LookupNamespace(prefix);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
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

		// Token: 0x06000176 RID: 374 RVA: 0x0000C2A8 File Offset: 0x0000A4A8
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

		// Token: 0x06000177 RID: 375 RVA: 0x0000C318 File Offset: 0x0000A518
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

		// Token: 0x06000178 RID: 376 RVA: 0x0000C388 File Offset: 0x0000A588
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

		// Token: 0x06000179 RID: 377 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		public override bool MoveToElement()
		{
			this.currentDefaultAttribute = -1;
			this.defaultAttributeConsumed = false;
			this.currentAttrType = null;
			return this.reader.MoveToElement();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
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

		// Token: 0x0600017B RID: 379 RVA: 0x0000C47C File Offset: 0x0000A67C
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

		// Token: 0x0600017C RID: 380 RVA: 0x0000C538 File Offset: 0x0000A738
		private XmlSchema ReadExternalSchema(string uri)
		{
			Uri uri2 = this.resolver.ResolveUri((!(this.BaseURI != string.Empty)) ? null : new Uri(this.BaseURI), uri);
			string url = (!(uri2 != null)) ? string.Empty : uri2.ToString();
			XmlTextReader xmlTextReader = null;
			XmlSchema result;
			try
			{
				xmlTextReader = new XmlTextReader(url, (Stream)this.resolver.GetEntity(uri2, null, typeof(Stream)), this.NameTable);
				result = XmlSchema.Read(xmlTextReader, this.ValidationEventHandler);
			}
			finally
			{
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
			}
			return result;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000C604 File Offset: 0x0000A804
		private void ExamineAdditionalSchema()
		{
			if (this.resolver == null || this.ValidationType == ValidationType.None)
			{
				return;
			}
			XmlSchema xmlSchema = null;
			string text = this.reader.GetAttribute("schemaLocation", "http://www.w3.org/2001/XMLSchema-instance");
			bool flag = false;
			if (text != null)
			{
				string[] array = null;
				try
				{
					text = XmlSchemaDatatype.FromName("token", "http://www.w3.org/2001/XMLSchema").Normalize(text);
					array = text.Split(XmlChar.WhitespaceChars);
				}
				catch (Exception innerException)
				{
					if (this.schemas.Count == 0)
					{
						this.HandleError("Invalid schemaLocation attribute format.", innerException, true);
					}
					array = new string[0];
				}
				if (array.Length % 2 != 0 && this.schemas.Count == 0)
				{
					this.HandleError("Invalid schemaLocation attribute format.");
				}
				int i = 0;
				do
				{
					try
					{
						while (i < array.Length)
						{
							xmlSchema = this.ReadExternalSchema(array[i + 1]);
							if (xmlSchema.TargetNamespace == null)
							{
								xmlSchema.TargetNamespace = array[i];
							}
							else if (xmlSchema.TargetNamespace != array[i])
							{
								this.HandleError("Specified schema has different target namespace.");
							}
							if (xmlSchema != null)
							{
								if (!this.schemas.Contains(xmlSchema.TargetNamespace))
								{
									flag = true;
									this.schemas.Add(xmlSchema);
								}
								xmlSchema = null;
							}
							i += 2;
						}
					}
					catch (Exception)
					{
						if (!this.schemas.Contains(array[i]))
						{
							this.HandleError(string.Format("Could not resolve schema location URI: {0}", (i + 1 >= array.Length) ? string.Empty : array[i + 1]), null, true);
						}
						i += 2;
					}
				}
				while (i < array.Length);
			}
			string attribute = this.reader.GetAttribute("noNamespaceSchemaLocation", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null)
			{
				try
				{
					xmlSchema = this.ReadExternalSchema(attribute);
				}
				catch (Exception)
				{
					if (this.schemas.Count != 0)
					{
						this.HandleError("Could not resolve schema location URI: " + attribute, null, true);
					}
				}
				if (xmlSchema != null && xmlSchema.TargetNamespace != null)
				{
					this.HandleError("Specified schema has different target namespace.");
				}
			}
			if (xmlSchema != null && !this.schemas.Contains(xmlSchema.TargetNamespace))
			{
				flag = true;
				this.schemas.Add(xmlSchema);
			}
			if (flag)
			{
				this.schemas.Compile();
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000C8AC File Offset: 0x0000AAAC
		public override bool Read()
		{
			this.validationStarted = true;
			this.currentDefaultAttribute = -1;
			this.defaultAttributeConsumed = false;
			this.currentAttrType = null;
			this.defaultAttributes = XsdValidatingReader.emptyAttributeArray;
			bool flag = this.reader.Read();
			if (this.reader.Depth == 0 && this.reader.NodeType == XmlNodeType.Element)
			{
				DTDValidatingReader dtdvalidatingReader = this.reader as DTDValidatingReader;
				if (dtdvalidatingReader != null && dtdvalidatingReader.DTD == null)
				{
					this.reader = dtdvalidatingReader.Source;
				}
				this.ExamineAdditionalSchema();
			}
			if (this.schemas.Count == 0)
			{
				return flag;
			}
			if (!this.schemas.IsCompiled)
			{
				this.schemas.Compile();
			}
			if (this.checkIdentity)
			{
				this.idManager.OnStartElement();
			}
			if (!flag && this.checkIdentity && this.idManager.HasMissingIDReferences())
			{
				this.HandleError("There are missing ID references: " + this.idManager.GetMissingIDString());
			}
			XmlNodeType nodeType = this.reader.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				if (this.checkKeyConstraints)
				{
					this.elementQNameStack.Add(new XmlQualifiedName(this.reader.LocalName, this.reader.NamespaceURI));
				}
				if (this.skipValidationDepth < 0 || this.reader.Depth <= this.skipValidationDepth)
				{
					this.ValidateEndSimpleContent();
					this.AssessStartElementSchemaValidity();
				}
				if (!this.reader.IsEmptyElement)
				{
					if (this.xsiNilDepth < this.reader.Depth)
					{
						this.shouldValidateCharacters = true;
					}
					return flag;
				}
				break;
			default:
				switch (nodeType)
				{
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					goto IL_249;
				case XmlNodeType.EndElement:
					break;
				default:
					return flag;
				}
				break;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				goto IL_249;
			}
			if (this.reader.Depth == this.skipValidationDepth)
			{
				this.skipValidationDepth = -1;
			}
			else if (this.skipValidationDepth < 0 || this.reader.Depth <= this.skipValidationDepth)
			{
				this.AssessEndElementSchemaValidity();
			}
			if (this.checkKeyConstraints)
			{
				this.elementQNameStack.RemoveAt(this.elementQNameStack.Count - 1);
			}
			return flag;
			IL_249:
			if (this.skipValidationDepth < 0 || this.reader.Depth <= this.skipValidationDepth)
			{
				XmlSchemaComplexType xmlSchemaComplexType = this.Context.ActualType as XmlSchemaComplexType;
				if (xmlSchemaComplexType != null)
				{
					XmlSchemaContentType contentType = xmlSchemaComplexType.ContentType;
					if (contentType != XmlSchemaContentType.Empty)
					{
						if (contentType == XmlSchemaContentType.ElementOnly)
						{
							if (this.reader.NodeType != XmlNodeType.Whitespace)
							{
								this.HandleError(string.Format("Not allowed character content is found (current content model '{0}' is element-only).", xmlSchemaComplexType.QualifiedName));
							}
						}
					}
					else
					{
						this.HandleError(string.Format("Not allowed character content is found (current element content model '{0}' is empty).", xmlSchemaComplexType.QualifiedName));
					}
				}
				this.ValidateCharacters();
			}
			return flag;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
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

		// Token: 0x06000180 RID: 384 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		public override string ReadString()
		{
			return base.ReadString();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000CBEC File Offset: 0x0000ADEC
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x0400012B RID: 299
		private static readonly XmlSchemaAttribute[] emptyAttributeArray = new XmlSchemaAttribute[0];

		// Token: 0x0400012C RID: 300
		private XmlReader reader;

		// Token: 0x0400012D RID: 301
		private XmlResolver resolver;

		// Token: 0x0400012E RID: 302
		private IHasXmlSchemaInfo sourceReaderSchemaInfo;

		// Token: 0x0400012F RID: 303
		private IXmlLineInfo readerLineInfo;

		// Token: 0x04000130 RID: 304
		private ValidationType validationType;

		// Token: 0x04000131 RID: 305
		private XmlSchemaSet schemas = new XmlSchemaSet();

		// Token: 0x04000132 RID: 306
		private bool namespaces = true;

		// Token: 0x04000133 RID: 307
		private bool validationStarted;

		// Token: 0x04000134 RID: 308
		private bool checkIdentity = true;

		// Token: 0x04000135 RID: 309
		private XsdIDManager idManager = new XsdIDManager();

		// Token: 0x04000136 RID: 310
		private bool checkKeyConstraints = true;

		// Token: 0x04000137 RID: 311
		private ArrayList keyTables = new ArrayList();

		// Token: 0x04000138 RID: 312
		private ArrayList currentKeyFieldConsumers;

		// Token: 0x04000139 RID: 313
		private ArrayList tmpKeyrefPool;

		// Token: 0x0400013A RID: 314
		private ArrayList elementQNameStack = new ArrayList();

		// Token: 0x0400013B RID: 315
		private XsdParticleStateManager state = new XsdParticleStateManager();

		// Token: 0x0400013C RID: 316
		private int skipValidationDepth = -1;

		// Token: 0x0400013D RID: 317
		private int xsiNilDepth = -1;

		// Token: 0x0400013E RID: 318
		private StringBuilder storedCharacters = new StringBuilder();

		// Token: 0x0400013F RID: 319
		private bool shouldValidateCharacters;

		// Token: 0x04000140 RID: 320
		private XmlSchemaAttribute[] defaultAttributes = XsdValidatingReader.emptyAttributeArray;

		// Token: 0x04000141 RID: 321
		private int currentDefaultAttribute = -1;

		// Token: 0x04000142 RID: 322
		private ArrayList defaultAttributesCache = new ArrayList();

		// Token: 0x04000143 RID: 323
		private bool defaultAttributeConsumed;

		// Token: 0x04000144 RID: 324
		private object currentAttrType;

		// Token: 0x04000145 RID: 325
		public ValidationEventHandler ValidationEventHandler;
	}
}
