using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Mono.Xml2;

namespace Mono.Xml
{
	// Token: 0x020000D4 RID: 212
	internal class DTDValidatingReader : XmlReader, IHasXmlParserContext, IHasXmlSchemaInfo, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x0002A1FC File Offset: 0x000283FC
		public DTDValidatingReader(XmlReader reader) : this(reader, null)
		{
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0002A208 File Offset: 0x00028408
		internal DTDValidatingReader(XmlReader reader, XmlValidatingReader validatingReader)
		{
			this.reader = new EntityResolvingXmlReader(reader);
			this.sourceTextReader = (reader as System.Xml.XmlTextReader);
			this.elementStack = new Stack();
			this.automataStack = new Stack();
			this.attributes = new DTDValidatingReader.AttributeSlot[10];
			this.nsmgr = new XmlNamespaceManager(reader.NameTable);
			this.validatingReader = validatingReader;
			this.valueBuilder = new StringBuilder();
			this.idList = new ArrayList();
			this.missingIDReferences = new ArrayList();
			System.Xml.XmlTextReader xmlTextReader = reader as System.Xml.XmlTextReader;
			if (xmlTextReader != null)
			{
				this.resolver = xmlTextReader.Resolver;
			}
			else
			{
				this.resolver = new XmlUrlResolver();
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0002A2DC File Offset: 0x000284DC
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			IXmlNamespaceResolver xmlNamespaceResolver = this.reader;
			IDictionary<string, string> result;
			if (xmlNamespaceResolver != null)
			{
				IDictionary<string, string> namespacesInScope = xmlNamespaceResolver.GetNamespacesInScope(scope);
				result = namespacesInScope;
			}
			else
			{
				result = new Dictionary<string, string>();
			}
			return result;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0002A30C File Offset: 0x0002850C
		bool IXmlLineInfo.HasLineInfo()
		{
			IXmlLineInfo xmlLineInfo = this.reader;
			return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0002A330 File Offset: 0x00028530
		string IXmlNamespaceResolver.LookupPrefix(string ns)
		{
			IXmlNamespaceResolver xmlNamespaceResolver = this.reader;
			return (xmlNamespaceResolver == null) ? null : xmlNamespaceResolver.LookupPrefix(ns);
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0002A358 File Offset: 0x00028558
		internal EntityResolvingXmlReader Source
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0002A360 File Offset: 0x00028560
		public DTDObjectModel DTD
		{
			get
			{
				return this.dtd;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0002A368 File Offset: 0x00028568
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0002A378 File Offset: 0x00028578
		public EntityHandling EntityHandling
		{
			get
			{
				return this.reader.EntityHandling;
			}
			set
			{
				this.reader.EntityHandling = value;
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0002A388 File Offset: 0x00028588
		public override void Close()
		{
			this.reader.Close();
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0002A398 File Offset: 0x00028598
		private int GetAttributeIndex(string name)
		{
			for (int i = 0; i < this.attributeCount; i++)
			{
				if (this.attributes[i].Name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0002A3D8 File Offset: 0x000285D8
		private int GetAttributeIndex(string localName, string ns)
		{
			for (int i = 0; i < this.attributeCount; i++)
			{
				if (this.attributes[i].LocalName == localName && this.attributes[i].NS == ns)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0002A430 File Offset: 0x00028630
		public override string GetAttribute(int i)
		{
			if (this.currentTextValue != null)
			{
				throw new IndexOutOfRangeException("Specified index is out of range: " + i);
			}
			if (this.attributeCount <= i)
			{
				throw new IndexOutOfRangeException("Specified index is out of range: " + i);
			}
			return this.attributes[i].Value;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0002A490 File Offset: 0x00028690
		public override string GetAttribute(string name)
		{
			if (this.currentTextValue != null)
			{
				return null;
			}
			int attributeIndex = this.GetAttributeIndex(name);
			return (attributeIndex >= 0) ? this.attributes[attributeIndex].Value : null;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0002A4CC File Offset: 0x000286CC
		public override string GetAttribute(string name, string ns)
		{
			if (this.currentTextValue != null)
			{
				return null;
			}
			int attributeIndex = this.GetAttributeIndex(name, ns);
			return (attributeIndex >= 0) ? this.attributes[attributeIndex].Value : null;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0002A50C File Offset: 0x0002870C
		public override string LookupNamespace(string prefix)
		{
			string text = this.nsmgr.LookupNamespace(this.NameTable.Get(prefix));
			return (!(text == string.Empty)) ? text : null;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0002A548 File Offset: 0x00028748
		public override void MoveToAttribute(int i)
		{
			if (this.currentTextValue != null)
			{
				throw new IndexOutOfRangeException("The index is out of range.");
			}
			if (this.attributeCount <= i)
			{
				throw new IndexOutOfRangeException("The index is out of range.");
			}
			if (i < this.reader.AttributeCount)
			{
				this.reader.MoveToAttribute(i);
			}
			this.currentAttribute = i;
			this.consumedAttribute = false;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0002A5B0 File Offset: 0x000287B0
		public override bool MoveToAttribute(string name)
		{
			if (this.currentTextValue != null)
			{
				return false;
			}
			int attributeIndex = this.GetAttributeIndex(name);
			if (attributeIndex < 0)
			{
				return false;
			}
			if (attributeIndex < this.reader.AttributeCount)
			{
				this.reader.MoveToAttribute(attributeIndex);
			}
			this.currentAttribute = attributeIndex;
			this.consumedAttribute = false;
			return true;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0002A608 File Offset: 0x00028808
		public override bool MoveToAttribute(string name, string ns)
		{
			if (this.currentTextValue != null)
			{
				return false;
			}
			int attributeIndex = this.GetAttributeIndex(name, ns);
			if (attributeIndex < 0)
			{
				return false;
			}
			if (attributeIndex < this.reader.AttributeCount)
			{
				this.reader.MoveToAttribute(attributeIndex);
			}
			this.currentAttribute = attributeIndex;
			this.consumedAttribute = false;
			return true;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0002A660 File Offset: 0x00028860
		public override bool MoveToElement()
		{
			if (this.currentTextValue != null)
			{
				return false;
			}
			if (!this.reader.MoveToElement() && !this.IsDefault)
			{
				return false;
			}
			this.currentAttribute = -1;
			this.consumedAttribute = false;
			return true;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0002A6A8 File Offset: 0x000288A8
		public override bool MoveToFirstAttribute()
		{
			if (this.currentTextValue != null)
			{
				return false;
			}
			if (this.attributeCount == 0)
			{
				return false;
			}
			this.currentAttribute = 0;
			this.reader.MoveToFirstAttribute();
			this.consumedAttribute = false;
			return true;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0002A6E0 File Offset: 0x000288E0
		public override bool MoveToNextAttribute()
		{
			if (this.currentTextValue != null)
			{
				return false;
			}
			if (this.currentAttribute == -1)
			{
				return this.MoveToFirstAttribute();
			}
			if (++this.currentAttribute == this.attributeCount)
			{
				this.currentAttribute--;
				return false;
			}
			if (this.currentAttribute < this.reader.AttributeCount)
			{
				this.reader.MoveToAttribute(this.currentAttribute);
			}
			this.consumedAttribute = false;
			return true;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0002A768 File Offset: 0x00028968
		public override bool Read()
		{
			if (this.currentTextValue != null)
			{
				this.shouldResetCurrentTextValue = true;
			}
			if (this.currentAttribute >= 0)
			{
				this.MoveToElement();
			}
			this.currentElement = null;
			this.currentAttribute = -1;
			this.consumedAttribute = false;
			this.attributeCount = 0;
			this.isWhitespace = false;
			this.isSignificantWhitespace = false;
			this.isText = false;
			bool flag = this.ReadContent() || this.currentTextValue != null;
			if (!flag && (this.Settings == null || (this.Settings.ValidationFlags & XmlSchemaValidationFlags.ProcessIdentityConstraints) == XmlSchemaValidationFlags.None) && this.missingIDReferences.Count > 0)
			{
				this.HandleError("Missing ID reference was found: " + string.Join(",", this.missingIDReferences.ToArray(typeof(string)) as string[]), XmlSeverityType.Error);
				this.missingIDReferences.Clear();
			}
			if (this.validatingReader != null)
			{
				this.EntityHandling = this.validatingReader.EntityHandling;
			}
			return flag;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0002A878 File Offset: 0x00028A78
		private bool ReadContent()
		{
			switch (this.reader.ReadState)
			{
			case ReadState.Error:
			case ReadState.EndOfFile:
			case ReadState.Closed:
				return false;
			default:
			{
				if (this.popScope)
				{
					this.nsmgr.PopScope();
					this.popScope = false;
					if (this.elementStack.Count == 0)
					{
						this.currentAutomata = null;
					}
				}
				bool flag = !this.reader.EOF;
				if (this.shouldResetCurrentTextValue)
				{
					this.currentTextValue = null;
					this.shouldResetCurrentTextValue = false;
				}
				else
				{
					flag = this.reader.Read();
				}
				if (flag)
				{
					return this.ProcessContent();
				}
				if (this.elementStack.Count != 0)
				{
					throw new InvalidOperationException("Unexpected end of XmlReader.");
				}
				return false;
			}
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0002A944 File Offset: 0x00028B44
		private bool ProcessContent()
		{
			switch (this.reader.NodeType)
			{
			case XmlNodeType.Element:
				if (this.constructingTextValue != null)
				{
					this.currentTextValue = this.constructingTextValue;
					this.constructingTextValue = null;
					if (this.isWhitespace)
					{
						this.ValidateWhitespaceNode();
					}
					return true;
				}
				this.ProcessStartElement();
				goto IL_1BB;
			case XmlNodeType.Attribute:
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.Document:
			case XmlNodeType.Notation:
			case XmlNodeType.EndEntity:
				goto IL_1BB;
			case XmlNodeType.Text:
				this.isWhitespace = (this.isSignificantWhitespace = false);
				this.isText = true;
				break;
			case XmlNodeType.CDATA:
				this.isSignificantWhitespace = (this.isWhitespace = false);
				this.isText = true;
				this.ValidateText();
				if (this.currentTextValue != null)
				{
					this.currentTextValue = this.constructingTextValue;
					this.constructingTextValue = null;
					return true;
				}
				goto IL_1BB;
			case XmlNodeType.DocumentType:
				this.ReadDoctype();
				goto IL_1BB;
			case XmlNodeType.DocumentFragment:
				break;
			case XmlNodeType.Whitespace:
				if (!this.isText && !this.isSignificantWhitespace)
				{
					this.isWhitespace = true;
				}
				break;
			case XmlNodeType.SignificantWhitespace:
				if (!this.isText)
				{
					this.isSignificantWhitespace = true;
				}
				this.isWhitespace = false;
				break;
			case XmlNodeType.EndElement:
				if (this.constructingTextValue != null)
				{
					this.currentTextValue = this.constructingTextValue;
					this.constructingTextValue = null;
					return true;
				}
				this.ProcessEndElement();
				goto IL_1BB;
			case XmlNodeType.XmlDeclaration:
				this.FillAttributes();
				if (this.GetAttribute("standalone") == "yes")
				{
					this.isStandalone = true;
				}
				goto IL_1BB;
			default:
				goto IL_1BB;
			}
			if (this.reader.NodeType != XmlNodeType.DocumentFragment)
			{
				this.ValidateText();
			}
			IL_1BB:
			if (this.isWhitespace)
			{
				this.ValidateWhitespaceNode();
			}
			this.currentTextValue = this.constructingTextValue;
			this.constructingTextValue = null;
			return true;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0002AB34 File Offset: 0x00028D34
		private void FillAttributes()
		{
			if (this.reader.MoveToFirstAttribute())
			{
				do
				{
					DTDValidatingReader.AttributeSlot attributeSlot = this.GetAttributeSlot();
					attributeSlot.Name = this.reader.Name;
					attributeSlot.LocalName = this.reader.LocalName;
					attributeSlot.Prefix = this.reader.Prefix;
					attributeSlot.NS = this.reader.NamespaceURI;
					attributeSlot.Value = this.reader.Value;
				}
				while (this.reader.MoveToNextAttribute());
				this.reader.MoveToElement();
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0002ABCC File Offset: 0x00028DCC
		private void ValidateText()
		{
			if (this.currentAutomata == null)
			{
				return;
			}
			DTDElementDeclaration dtdelementDeclaration = null;
			if (this.elementStack.Count > 0)
			{
				dtdelementDeclaration = this.dtd.ElementDecls[this.elementStack.Peek() as string];
			}
			if (dtdelementDeclaration != null && !dtdelementDeclaration.IsMixedContent && !dtdelementDeclaration.IsAny && !this.isWhitespace)
			{
				this.HandleError(string.Format("Current element {0} does not allow character data content.", this.elementStack.Peek() as string), XmlSeverityType.Error);
				this.currentAutomata = this.previousAutomata;
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0002AC70 File Offset: 0x00028E70
		private void ValidateWhitespaceNode()
		{
			if (this.isStandalone && this.DTD != null && this.elementStack.Count > 0)
			{
				DTDElementDeclaration dtdelementDeclaration = this.DTD.ElementDecls[this.elementStack.Peek() as string];
				if (dtdelementDeclaration != null && !dtdelementDeclaration.IsInternalSubset && !dtdelementDeclaration.IsMixedContent && !dtdelementDeclaration.IsAny && !dtdelementDeclaration.IsEmpty)
				{
					this.HandleError("In a standalone document, whitespace cannot appear in an element which is declared to contain only element children.", XmlSeverityType.Error);
				}
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0002AD04 File Offset: 0x00028F04
		private void HandleError(string message, XmlSeverityType severity)
		{
			if (this.validatingReader != null && this.validatingReader.ValidationType == ValidationType.None)
			{
				return;
			}
			bool flag = ((IXmlLineInfo)this).HasLineInfo();
			XmlSchemaException ex = new XmlSchemaException(message, (!flag) ? 0 : ((IXmlLineInfo)this).LineNumber, (!flag) ? 0 : ((IXmlLineInfo)this).LinePosition, null, this.BaseURI, null);
			this.HandleError(ex, severity);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0002AD74 File Offset: 0x00028F74
		private void HandleError(XmlSchemaException ex, XmlSeverityType severity)
		{
			if (this.validatingReader != null && this.validatingReader.ValidationType == ValidationType.None)
			{
				return;
			}
			if (this.validatingReader != null)
			{
				this.validatingReader.OnValidationEvent(this, new ValidationEventArgs(ex, ex.Message, severity));
			}
			else if (severity == XmlSeverityType.Error)
			{
				throw ex;
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0002ADD0 File Offset: 0x00028FD0
		private void ValidateAttributes(DTDAttListDeclaration decl, bool validate)
		{
			this.DtdValidateAttributes(decl, validate);
			for (int i = 0; i < this.attributeCount; i++)
			{
				DTDValidatingReader.AttributeSlot attributeSlot = this.attributes[i];
				if (attributeSlot.Name == "xmlns" || attributeSlot.Prefix == "xmlns")
				{
					this.nsmgr.AddNamespace((!(attributeSlot.Prefix == "xmlns")) ? string.Empty : attributeSlot.LocalName, attributeSlot.Value);
				}
			}
			for (int j = 0; j < this.attributeCount; j++)
			{
				DTDValidatingReader.AttributeSlot attributeSlot2 = this.attributes[j];
				if (attributeSlot2.Name == "xmlns")
				{
					attributeSlot2.NS = "http://www.w3.org/2000/xmlns/";
				}
				else if (attributeSlot2.Prefix.Length > 0)
				{
					attributeSlot2.NS = this.LookupNamespace(attributeSlot2.Prefix);
				}
				else
				{
					attributeSlot2.NS = string.Empty;
				}
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0002AEE0 File Offset: 0x000290E0
		private DTDValidatingReader.AttributeSlot GetAttributeSlot()
		{
			if (this.attributeCount == this.attributes.Length)
			{
				DTDValidatingReader.AttributeSlot[] destinationArray = new DTDValidatingReader.AttributeSlot[this.attributeCount << 1];
				Array.Copy(this.attributes, destinationArray, this.attributeCount);
				this.attributes = destinationArray;
			}
			if (this.attributes[this.attributeCount] == null)
			{
				this.attributes[this.attributeCount] = new DTDValidatingReader.AttributeSlot();
			}
			DTDValidatingReader.AttributeSlot attributeSlot = this.attributes[this.attributeCount];
			attributeSlot.Clear();
			this.attributeCount++;
			return attributeSlot;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0002AF70 File Offset: 0x00029170
		private void DtdValidateAttributes(DTDAttListDeclaration decl, bool validate)
		{
			while (this.reader.MoveToNextAttribute())
			{
				string name = this.reader.Name;
				DTDValidatingReader.AttributeSlot attributeSlot = this.GetAttributeSlot();
				attributeSlot.Name = this.reader.Name;
				attributeSlot.LocalName = this.reader.LocalName;
				attributeSlot.Prefix = this.reader.Prefix;
				XmlReader xmlReader = this.reader;
				string text = string.Empty;
				while (this.attributeValueEntityStack.Count >= 0)
				{
					if (!xmlReader.ReadAttributeValue())
					{
						if (this.attributeValueEntityStack.Count <= 0)
						{
							break;
						}
						xmlReader = (this.attributeValueEntityStack.Pop() as XmlReader);
					}
					else
					{
						XmlNodeType nodeType = xmlReader.NodeType;
						if (nodeType != XmlNodeType.EntityReference)
						{
							if (nodeType != XmlNodeType.EndEntity)
							{
								text += xmlReader.Value;
							}
						}
						else
						{
							DTDEntityDeclaration dtdentityDeclaration = this.DTD.EntityDecls[xmlReader.Name];
							if (dtdentityDeclaration == null)
							{
								this.HandleError(string.Format("Referenced entity {0} is not declared.", xmlReader.Name), XmlSeverityType.Error);
							}
							else
							{
								System.Xml.XmlTextReader xmlTextReader = new System.Xml.XmlTextReader(dtdentityDeclaration.EntityValue, XmlNodeType.Attribute, this.ParserContext);
								this.attributeValueEntityStack.Push(xmlReader);
								xmlReader = xmlTextReader;
							}
						}
					}
				}
				this.reader.MoveToElement();
				this.reader.MoveToAttribute(name);
				attributeSlot.Value = this.FilterNormalization(name, text);
				if (validate)
				{
					DTDAttributeDefinition dtdattributeDefinition = decl[this.reader.Name];
					if (dtdattributeDefinition != null)
					{
						if (dtdattributeDefinition.EnumeratedAttributeDeclaration.Count > 0 && !dtdattributeDefinition.EnumeratedAttributeDeclaration.Contains(attributeSlot.Value))
						{
							this.HandleError(string.Format("Attribute enumeration constraint error in attribute {0}, value {1}.", this.reader.Name, text), XmlSeverityType.Error);
						}
						if (dtdattributeDefinition.EnumeratedNotations.Count > 0 && !dtdattributeDefinition.EnumeratedNotations.Contains(attributeSlot.Value))
						{
							this.HandleError(string.Format("Attribute notation enumeration constraint error in attribute {0}, value {1}.", this.reader.Name, text), XmlSeverityType.Error);
						}
						string text2 = null;
						if (dtdattributeDefinition.Datatype != null)
						{
							text2 = this.FilterNormalization(dtdattributeDefinition.Name, text);
						}
						else
						{
							text2 = text;
						}
						string[] array = null;
						switch (dtdattributeDefinition.Datatype.TokenizedType)
						{
						case XmlTokenizedType.IDREFS:
						case XmlTokenizedType.ENTITIES:
						case XmlTokenizedType.NMTOKENS:
							try
							{
								array = (dtdattributeDefinition.Datatype.ParseValue(text2, this.NameTable, null) as string[]);
							}
							catch (Exception)
							{
								this.HandleError("Attribute value is invalid against its data type.", XmlSeverityType.Error);
								array = new string[0];
							}
							break;
						case XmlTokenizedType.ENTITY:
						case XmlTokenizedType.NMTOKEN:
							goto IL_2D6;
						default:
							goto IL_2D6;
						}
						IL_31C:
						switch (dtdattributeDefinition.Datatype.TokenizedType)
						{
						case XmlTokenizedType.ID:
							if (this.idList.Contains(text2))
							{
								this.HandleError(string.Format("Node with ID {0} was already appeared.", text), XmlSeverityType.Error);
							}
							else
							{
								if (this.missingIDReferences.Contains(text2))
								{
									this.missingIDReferences.Remove(text2);
								}
								this.idList.Add(text2);
							}
							break;
						case XmlTokenizedType.IDREF:
							if (!this.idList.Contains(text2))
							{
								this.missingIDReferences.Add(text2);
							}
							break;
						case XmlTokenizedType.IDREFS:
							foreach (string text3 in array)
							{
								if (!this.idList.Contains(text3))
								{
									this.missingIDReferences.Add(text3);
								}
							}
							break;
						case XmlTokenizedType.ENTITY:
						{
							DTDEntityDeclaration dtdentityDeclaration2 = this.dtd.EntityDecls[text2];
							if (dtdentityDeclaration2 == null)
							{
								this.HandleError("Reference to undeclared entity was found in attribute: " + this.reader.Name + ".", XmlSeverityType.Error);
							}
							else if (dtdentityDeclaration2.NotationName == null)
							{
								this.HandleError("The entity specified by entity type value must be an unparsed entity. The entity definition has no NDATA in attribute: " + this.reader.Name + ".", XmlSeverityType.Error);
							}
							break;
						}
						case XmlTokenizedType.ENTITIES:
							foreach (string rawValue in array)
							{
								DTDEntityDeclaration dtdentityDeclaration2 = this.dtd.EntityDecls[this.FilterNormalization(this.reader.Name, rawValue)];
								if (dtdentityDeclaration2 == null)
								{
									this.HandleError("Reference to undeclared entity was found in attribute: " + this.reader.Name + ".", XmlSeverityType.Error);
								}
								else if (dtdentityDeclaration2.NotationName == null)
								{
									this.HandleError("The entity specified by ENTITIES type value must be an unparsed entity. The entity definition has no NDATA in attribute: " + this.reader.Name + ".", XmlSeverityType.Error);
								}
							}
							break;
						}
						if (this.isStandalone && !dtdattributeDefinition.IsInternalSubset && text != text2)
						{
							this.HandleError("In standalone document, attribute value characters must not be checked against external definition.", XmlSeverityType.Error);
						}
						if (dtdattributeDefinition.OccurenceType == DTDAttributeOccurenceType.Fixed && text != dtdattributeDefinition.DefaultValue)
						{
							this.HandleError(string.Format("Fixed attribute {0} in element {1} has invalid value {2}.", dtdattributeDefinition.Name, decl.Name, text), XmlSeverityType.Error);
							continue;
						}
						continue;
						try
						{
							IL_2D6:
							dtdattributeDefinition.Datatype.ParseValue(text2, this.NameTable, null);
						}
						catch (Exception ex)
						{
							this.HandleError(string.Format("Attribute value is invalid against its data type '{0}'. {1}", dtdattributeDefinition.Datatype, ex.Message), XmlSeverityType.Error);
						}
						goto IL_31C;
					}
					this.HandleError(string.Format("Attribute {0} is not declared.", this.reader.Name), XmlSeverityType.Error);
				}
			}
			if (validate)
			{
				this.VerifyDeclaredAttributes(decl);
			}
			this.MoveToElement();
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0002B56C File Offset: 0x0002976C
		private void ReadDoctype()
		{
			this.FillAttributes();
			IHasXmlParserContext hasXmlParserContext = this.reader;
			if (hasXmlParserContext != null)
			{
				this.dtd = hasXmlParserContext.ParserContext.Dtd;
			}
			if (this.dtd == null)
			{
				Mono.Xml2.XmlTextReader xmlTextReader = new Mono.Xml2.XmlTextReader(string.Empty, XmlNodeType.Document, null);
				xmlTextReader.XmlResolver = this.resolver;
				xmlTextReader.GenerateDTDObjectModel(this.reader.Name, this.reader["PUBLIC"], this.reader["SYSTEM"], this.reader.Value);
				this.dtd = xmlTextReader.DTD;
			}
			this.currentAutomata = this.dtd.RootAutomata;
			for (int i = 0; i < this.DTD.Errors.Length; i++)
			{
				this.HandleError(this.DTD.Errors[i].Message, XmlSeverityType.Error);
			}
			foreach (DTDNode dtdnode in this.dtd.EntityDecls.Values)
			{
				DTDEntityDeclaration dtdentityDeclaration = (DTDEntityDeclaration)dtdnode;
				if (dtdentityDeclaration.NotationName != null && this.dtd.NotationDecls[dtdentityDeclaration.NotationName] == null)
				{
					this.HandleError("Target notation was not found for NData in entity declaration " + dtdentityDeclaration.Name + ".", XmlSeverityType.Error);
				}
			}
			foreach (DTDNode dtdnode2 in this.dtd.AttListDecls.Values)
			{
				DTDAttListDeclaration dtdattListDeclaration = (DTDAttListDeclaration)dtdnode2;
				foreach (object obj in dtdattListDeclaration.Definitions)
				{
					DTDAttributeDefinition dtdattributeDefinition = (DTDAttributeDefinition)obj;
					if (dtdattributeDefinition.Datatype.TokenizedType == XmlTokenizedType.NOTATION)
					{
						foreach (object obj2 in dtdattributeDefinition.EnumeratedNotations)
						{
							string name = (string)obj2;
							if (this.dtd.NotationDecls[name] == null)
							{
								this.HandleError("Target notation was not found for NOTATION typed attribute default " + dtdattributeDefinition.Name + ".", XmlSeverityType.Error);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0002B864 File Offset: 0x00029A64
		private void ProcessStartElement()
		{
			this.nsmgr.PushScope();
			this.popScope = this.reader.IsEmptyElement;
			this.elementStack.Push(this.reader.Name);
			this.currentElement = this.Name;
			if (this.currentAutomata == null)
			{
				this.ValidateAttributes(null, false);
				if (this.reader.IsEmptyElement)
				{
					this.ProcessEndElement();
				}
				return;
			}
			this.previousAutomata = this.currentAutomata;
			this.currentAutomata = this.currentAutomata.TryStartElement(this.reader.Name);
			if (this.currentAutomata == this.DTD.Invalid)
			{
				this.HandleError(string.Format("Invalid start element found: {0}", this.reader.Name), XmlSeverityType.Error);
				this.currentAutomata = this.previousAutomata;
			}
			DTDElementDeclaration dtdelementDeclaration = this.DTD.ElementDecls[this.reader.Name];
			if (dtdelementDeclaration == null)
			{
				this.HandleError(string.Format("Element {0} is not declared.", this.reader.Name), XmlSeverityType.Error);
				this.currentAutomata = this.previousAutomata;
			}
			this.automataStack.Push(this.currentAutomata);
			if (dtdelementDeclaration != null)
			{
				this.currentAutomata = dtdelementDeclaration.ContentModel.GetAutomata();
			}
			DTDAttListDeclaration dtdattListDeclaration = this.dtd.AttListDecls[this.currentElement];
			if (dtdattListDeclaration != null)
			{
				this.ValidateAttributes(dtdattListDeclaration, true);
				this.currentAttribute = -1;
			}
			else
			{
				if (this.reader.HasAttributes)
				{
					this.HandleError(string.Format("Attributes are found on element {0} while it has no attribute definitions.", this.currentElement), XmlSeverityType.Error);
				}
				this.ValidateAttributes(null, false);
			}
			if (this.reader.IsEmptyElement)
			{
				this.ProcessEndElement();
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0002BA28 File Offset: 0x00029C28
		private void ProcessEndElement()
		{
			this.popScope = true;
			this.elementStack.Pop();
			if (this.currentAutomata == null)
			{
				return;
			}
			if (this.DTD.ElementDecls[this.reader.Name] == null)
			{
				this.HandleError(string.Format("Element {0} is not declared.", this.reader.Name), XmlSeverityType.Error);
			}
			this.previousAutomata = this.currentAutomata;
			DTDAutomata dtdautomata = this.currentAutomata.TryEndElement();
			if (dtdautomata == this.DTD.Invalid)
			{
				this.HandleError(string.Format("Invalid end element found: {0}", this.reader.Name), XmlSeverityType.Error);
				this.currentAutomata = this.previousAutomata;
			}
			this.currentAutomata = (this.automataStack.Pop() as DTDAutomata);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0002BAFC File Offset: 0x00029CFC
		private void VerifyDeclaredAttributes(DTDAttListDeclaration decl)
		{
			for (int i = 0; i < decl.Definitions.Count; i++)
			{
				DTDAttributeDefinition dtdattributeDefinition = (DTDAttributeDefinition)decl.Definitions[i];
				bool flag = false;
				for (int j = 0; j < this.attributeCount; j++)
				{
					if (this.attributes[j].Name == dtdattributeDefinition.Name)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					if (dtdattributeDefinition.OccurenceType == DTDAttributeOccurenceType.Required)
					{
						this.HandleError(string.Format("Required attribute {0} in element {1} not found .", dtdattributeDefinition.Name, decl.Name), XmlSeverityType.Error);
					}
					else if (dtdattributeDefinition.DefaultValue != null)
					{
						if (this.isStandalone && !dtdattributeDefinition.IsInternalSubset)
						{
							this.HandleError("In standalone document, external default value definition must not be applied.", XmlSeverityType.Error);
						}
						switch (this.validatingReader.ValidationType)
						{
						case ValidationType.None:
						case ValidationType.DTD:
							break;
						case ValidationType.Auto:
							if (this.validatingReader.Schemas.Count != 0)
							{
								goto IL_197;
							}
							break;
						default:
							goto IL_197;
						}
						DTDValidatingReader.AttributeSlot attributeSlot = this.GetAttributeSlot();
						attributeSlot.Name = dtdattributeDefinition.Name;
						int num = dtdattributeDefinition.Name.IndexOf(':');
						attributeSlot.LocalName = ((num >= 0) ? dtdattributeDefinition.Name.Substring(num + 1) : dtdattributeDefinition.Name);
						string prefix = (num >= 0) ? dtdattributeDefinition.Name.Substring(0, num) : string.Empty;
						attributeSlot.Prefix = prefix;
						attributeSlot.Value = dtdattributeDefinition.DefaultValue;
						attributeSlot.IsDefault = true;
					}
				}
				IL_197:;
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0002BCB8 File Offset: 0x00029EB8
		public override bool ReadAttributeValue()
		{
			if (this.consumedAttribute)
			{
				return false;
			}
			if (this.NodeType == XmlNodeType.Attribute && this.EntityHandling == EntityHandling.ExpandEntities)
			{
				this.consumedAttribute = true;
				return true;
			}
			if (this.IsDefault)
			{
				this.consumedAttribute = true;
				return true;
			}
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0002BD14 File Offset: 0x00029F14
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0002BD24 File Offset: 0x00029F24
		public override int AttributeCount
		{
			get
			{
				if (this.currentTextValue != null)
				{
					return 0;
				}
				return this.attributeCount;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0002BD3C File Offset: 0x00029F3C
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0002BD4C File Offset: 0x00029F4C
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x0002BD50 File Offset: 0x00029F50
		public override int Depth
		{
			get
			{
				int num = this.reader.Depth;
				if (this.currentTextValue != null && this.reader.NodeType == XmlNodeType.EndElement)
				{
					num++;
				}
				return (!this.IsDefault) ? num : (num + 1);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0002BDA0 File Offset: 0x00029FA0
		public override bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0002BDB0 File Offset: 0x00029FB0
		public override bool HasValue
		{
			get
			{
				return this.currentAttribute >= 0 || this.currentTextValue != null || this.reader.HasValue;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0002BDEC File Offset: 0x00029FEC
		public override bool IsDefault
		{
			get
			{
				return this.currentTextValue == null && this.currentAttribute != -1 && this.attributes[this.currentAttribute].IsDefault;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0002BE1C File Offset: 0x0002A01C
		public override bool IsEmptyElement
		{
			get
			{
				return this.currentTextValue == null && this.reader.IsEmptyElement;
			}
		}

		// Token: 0x170001FF RID: 511
		public override string this[int i]
		{
			get
			{
				return this.GetAttribute(i);
			}
		}

		// Token: 0x17000200 RID: 512
		public override string this[string name]
		{
			get
			{
				return this.GetAttribute(name);
			}
		}

		// Token: 0x17000201 RID: 513
		public override string this[string name, string ns]
		{
			get
			{
				return this.GetAttribute(name, ns);
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0002BE5C File Offset: 0x0002A05C
		public int LineNumber
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.reader;
				return (xmlLineInfo == null) ? 0 : xmlLineInfo.LineNumber;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0002BE84 File Offset: 0x0002A084
		public int LinePosition
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.reader;
				return (xmlLineInfo == null) ? 0 : xmlLineInfo.LinePosition;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x0002BEAC File Offset: 0x0002A0AC
		public override string LocalName
		{
			get
			{
				if (this.currentTextValue != null || this.consumedAttribute)
				{
					return string.Empty;
				}
				if (this.NodeType == XmlNodeType.Attribute)
				{
					return this.attributes[this.currentAttribute].LocalName;
				}
				return this.reader.LocalName;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0002BF00 File Offset: 0x0002A100
		public override string Name
		{
			get
			{
				if (this.currentTextValue != null || this.consumedAttribute)
				{
					return string.Empty;
				}
				if (this.NodeType == XmlNodeType.Attribute)
				{
					return this.attributes[this.currentAttribute].Name;
				}
				return this.reader.Name;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0002BF54 File Offset: 0x0002A154
		public override string NamespaceURI
		{
			get
			{
				if (this.currentTextValue != null || this.consumedAttribute)
				{
					return string.Empty;
				}
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.Attribute)
					{
						return this.attributes[this.currentAttribute].NS;
					}
					if (nodeType != XmlNodeType.EndElement)
					{
						return string.Empty;
					}
				}
				return this.nsmgr.LookupNamespace(this.Prefix);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0002BFCC File Offset: 0x0002A1CC
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0002BFDC File Offset: 0x0002A1DC
		public override XmlNodeType NodeType
		{
			get
			{
				if (this.currentTextValue != null)
				{
					return (!this.isSignificantWhitespace) ? ((!this.isWhitespace) ? XmlNodeType.Text : XmlNodeType.Whitespace) : XmlNodeType.SignificantWhitespace;
				}
				return (!this.consumedAttribute) ? ((!this.IsDefault) ? this.reader.NodeType : XmlNodeType.Attribute) : XmlNodeType.Text;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0002C048 File Offset: 0x0002A248
		public XmlParserContext ParserContext
		{
			get
			{
				return XmlSchemaUtil.GetParserContext(this.reader);
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0002C058 File Offset: 0x0002A258
		public override string Prefix
		{
			get
			{
				if (this.currentTextValue != null || this.consumedAttribute)
				{
					return string.Empty;
				}
				if (this.NodeType == XmlNodeType.Attribute)
				{
					return this.attributes[this.currentAttribute].Prefix;
				}
				return this.reader.Prefix;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0002C0AC File Offset: 0x0002A2AC
		public override char QuoteChar
		{
			get
			{
				return this.reader.QuoteChar;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0002C0BC File Offset: 0x0002A2BC
		public override ReadState ReadState
		{
			get
			{
				if (this.reader.ReadState == ReadState.EndOfFile && this.currentTextValue != null)
				{
					return ReadState.Interactive;
				}
				return this.reader.ReadState;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0002C0E8 File Offset: 0x0002A2E8
		public object SchemaType
		{
			get
			{
				if (this.DTD == null || this.currentAttribute == -1 || this.currentElement == null)
				{
					return null;
				}
				DTDAttListDeclaration dtdattListDeclaration = this.DTD.AttListDecls[this.currentElement];
				DTDAttributeDefinition dtdattributeDefinition = (dtdattListDeclaration == null) ? null : dtdattListDeclaration[this.attributes[this.currentAttribute].Name];
				return (dtdattributeDefinition == null) ? null : dtdattributeDefinition.Datatype;
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0002C168 File Offset: 0x0002A368
		private string FilterNormalization(string attrName, string rawValue)
		{
			if (this.DTD == null || this.sourceTextReader == null || !this.sourceTextReader.Normalization)
			{
				return rawValue;
			}
			DTDAttributeDefinition dtdattributeDefinition = this.dtd.AttListDecls[this.currentElement].Get(attrName);
			this.valueBuilder.Append(rawValue);
			this.valueBuilder.Replace('\r', ' ');
			this.valueBuilder.Replace('\n', ' ');
			this.valueBuilder.Replace('\t', ' ');
			string result;
			try
			{
				if (dtdattributeDefinition == null || dtdattributeDefinition.Datatype.TokenizedType == XmlTokenizedType.CDATA)
				{
					result = this.valueBuilder.ToString();
				}
				else
				{
					for (int i = 0; i < this.valueBuilder.Length; i++)
					{
						if (this.valueBuilder[i] == ' ')
						{
							while (++i < this.valueBuilder.Length && this.valueBuilder[i] == ' ')
							{
								this.valueBuilder.Remove(i, 1);
							}
						}
					}
					result = this.valueBuilder.ToString().Trim(this.whitespaceChars);
				}
			}
			finally
			{
				this.valueBuilder.Length = 0;
			}
			return result;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0002C2D8 File Offset: 0x0002A4D8
		public override string Value
		{
			get
			{
				if (this.currentTextValue != null)
				{
					return this.currentTextValue;
				}
				if (this.NodeType == XmlNodeType.Attribute || this.consumedAttribute)
				{
					return this.attributes[this.currentAttribute].Value;
				}
				return this.reader.Value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0002C32C File Offset: 0x0002A52C
		public override string XmlLang
		{
			get
			{
				string text = this["xml:lang"];
				return (text == null) ? this.reader.XmlLang : text;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0002C35C File Offset: 0x0002A55C
		internal XmlResolver Resolver
		{
			get
			{
				return this.resolver;
			}
		}

		// Token: 0x17000211 RID: 529
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x0002C364 File Offset: 0x0002A564
		public XmlResolver XmlResolver
		{
			set
			{
				if (this.dtd != null)
				{
					this.dtd.XmlResolver = value;
				}
				this.resolver = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0002C384 File Offset: 0x0002A584
		public override XmlSpace XmlSpace
		{
			get
			{
				string text = this["xml:space"];
				string text2 = text;
				if (text2 != null)
				{
					if (DTDValidatingReader.<>f__switch$map2A == null)
					{
						DTDValidatingReader.<>f__switch$map2A = new Dictionary<string, int>(2)
						{
							{
								"preserve",
								0
							},
							{
								"default",
								1
							}
						};
					}
					int num;
					if (DTDValidatingReader.<>f__switch$map2A.TryGetValue(text2, out num))
					{
						if (num == 0)
						{
							return XmlSpace.Preserve;
						}
						if (num == 1)
						{
							return XmlSpace.Default;
						}
					}
				}
				return this.reader.XmlSpace;
			}
		}

		// Token: 0x04000445 RID: 1093
		private EntityResolvingXmlReader reader;

		// Token: 0x04000446 RID: 1094
		private System.Xml.XmlTextReader sourceTextReader;

		// Token: 0x04000447 RID: 1095
		private XmlValidatingReader validatingReader;

		// Token: 0x04000448 RID: 1096
		private DTDObjectModel dtd;

		// Token: 0x04000449 RID: 1097
		private XmlResolver resolver;

		// Token: 0x0400044A RID: 1098
		private string currentElement;

		// Token: 0x0400044B RID: 1099
		private DTDValidatingReader.AttributeSlot[] attributes;

		// Token: 0x0400044C RID: 1100
		private int attributeCount;

		// Token: 0x0400044D RID: 1101
		private int currentAttribute = -1;

		// Token: 0x0400044E RID: 1102
		private bool consumedAttribute;

		// Token: 0x0400044F RID: 1103
		private Stack elementStack;

		// Token: 0x04000450 RID: 1104
		private Stack automataStack;

		// Token: 0x04000451 RID: 1105
		private bool popScope;

		// Token: 0x04000452 RID: 1106
		private bool isStandalone;

		// Token: 0x04000453 RID: 1107
		private DTDAutomata currentAutomata;

		// Token: 0x04000454 RID: 1108
		private DTDAutomata previousAutomata;

		// Token: 0x04000455 RID: 1109
		private ArrayList idList;

		// Token: 0x04000456 RID: 1110
		private ArrayList missingIDReferences;

		// Token: 0x04000457 RID: 1111
		private XmlNamespaceManager nsmgr;

		// Token: 0x04000458 RID: 1112
		private string currentTextValue;

		// Token: 0x04000459 RID: 1113
		private string constructingTextValue;

		// Token: 0x0400045A RID: 1114
		private bool shouldResetCurrentTextValue;

		// Token: 0x0400045B RID: 1115
		private bool isSignificantWhitespace;

		// Token: 0x0400045C RID: 1116
		private bool isWhitespace;

		// Token: 0x0400045D RID: 1117
		private bool isText;

		// Token: 0x0400045E RID: 1118
		private Stack attributeValueEntityStack = new Stack();

		// Token: 0x0400045F RID: 1119
		private StringBuilder valueBuilder;

		// Token: 0x04000460 RID: 1120
		private char[] whitespaceChars = new char[]
		{
			' '
		};

		// Token: 0x020000D5 RID: 213
		private class AttributeSlot
		{
			// Token: 0x060007B0 RID: 1968 RVA: 0x0002C410 File Offset: 0x0002A610
			public void Clear()
			{
				this.Prefix = string.Empty;
				this.LocalName = string.Empty;
				this.NS = string.Empty;
				this.Value = string.Empty;
				this.IsDefault = false;
			}

			// Token: 0x04000462 RID: 1122
			public string Name;

			// Token: 0x04000463 RID: 1123
			public string LocalName;

			// Token: 0x04000464 RID: 1124
			public string NS;

			// Token: 0x04000465 RID: 1125
			public string Prefix;

			// Token: 0x04000466 RID: 1126
			public string Value;

			// Token: 0x04000467 RID: 1127
			public bool IsDefault;
		}
	}
}
