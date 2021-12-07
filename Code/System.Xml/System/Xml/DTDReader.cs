using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Schema;
using Mono.Xml;

namespace System.Xml
{
	// Token: 0x020000D3 RID: 211
	internal class DTDReader : IXmlLineInfo
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x00027584 File Offset: 0x00025784
		public DTDReader(DTDObjectModel dtd, int startLineNumber, int startLinePosition)
		{
			this.DTD = dtd;
			this.currentLinkedNodeLineNumber = startLineNumber;
			this.currentLinkedNodeLinePosition = startLinePosition;
			this.Init();
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x000275A8 File Offset: 0x000257A8
		public string BaseURI
		{
			get
			{
				return this.currentInput.BaseURI;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x000275B8 File Offset: 0x000257B8
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x000275C0 File Offset: 0x000257C0
		public bool Normalization
		{
			get
			{
				return this.normalization;
			}
			set
			{
				this.normalization = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x000275CC File Offset: 0x000257CC
		public int LineNumber
		{
			get
			{
				return this.currentInput.LineNumber;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x000275DC File Offset: 0x000257DC
		public int LinePosition
		{
			get
			{
				return this.currentInput.LinePosition;
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000275EC File Offset: 0x000257EC
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x000275F0 File Offset: 0x000257F0
		private XmlException NotWFError(string message)
		{
			return new XmlException(this, this.BaseURI, message);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00027600 File Offset: 0x00025800
		private void Init()
		{
			this.parserInputStack = new Stack();
			this.nameBuffer = new char[256];
			this.nameLength = 0;
			this.nameCapacity = 256;
			this.valueBuffer = new StringBuilder(512);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00027640 File Offset: 0x00025840
		internal DTDObjectModel GenerateDTDObjectModel()
		{
			int count = this.parserInputStack.Count;
			if (this.DTD.InternalSubset != null && this.DTD.InternalSubset.Length > 0)
			{
				this.processingInternalSubset = true;
				XmlParserInput xmlParserInput = this.currentInput;
				this.currentInput = new XmlParserInput(new StringReader(this.DTD.InternalSubset), this.DTD.BaseURI, this.currentLinkedNodeLineNumber, this.currentLinkedNodeLinePosition);
				this.currentInput.AllowTextDecl = false;
				bool flag;
				do
				{
					flag = this.ProcessDTDSubset();
					if (this.PeekChar() == -1 && this.parserInputStack.Count > 0)
					{
						this.PopParserInput();
					}
				}
				while (flag || this.parserInputStack.Count > count);
				if (this.dtdIncludeSect != 0)
				{
					throw this.NotWFError("INCLUDE section is not ended correctly.");
				}
				this.currentInput = xmlParserInput;
				this.processingInternalSubset = false;
			}
			if (this.DTD.SystemId != null && this.DTD.SystemId != string.Empty && this.DTD.Resolver != null)
			{
				this.PushParserInput(this.DTD.SystemId);
				bool flag;
				do
				{
					flag = this.ProcessDTDSubset();
					if (this.PeekChar() == -1 && this.parserInputStack.Count > 1)
					{
						this.PopParserInput();
					}
				}
				while (flag || this.parserInputStack.Count > count + 1);
				if (this.dtdIncludeSect != 0)
				{
					throw this.NotWFError("INCLUDE section is not ended correctly.");
				}
				this.PopParserInput();
			}
			ArrayList arrayList = new ArrayList();
			foreach (DTDNode dtdnode in this.DTD.EntityDecls.Values)
			{
				DTDEntityDeclaration dtdentityDeclaration = (DTDEntityDeclaration)dtdnode;
				if (dtdentityDeclaration.NotationName != null)
				{
					dtdentityDeclaration.ScanEntityValue(arrayList);
					arrayList.Clear();
				}
			}
			this.DTD.ExternalResources.Clear();
			return this.DTD;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00027878 File Offset: 0x00025A78
		private bool ProcessDTDSubset()
		{
			this.SkipWhitespace();
			int num = this.ReadChar();
			int num2 = num;
			if (num2 != -1)
			{
				if (num2 != 37)
				{
					if (num2 != 60)
					{
						if (num2 != 93)
						{
							throw this.NotWFError(string.Format("Syntax Error inside doctypedecl markup : {0}({1})", num, (char)num));
						}
						if (this.dtdIncludeSect == 0)
						{
							throw this.NotWFError("Unbalanced end of INCLUDE/IGNORE section.");
						}
						this.Expect("]>");
						this.dtdIncludeSect--;
						this.SkipWhitespace();
					}
					else
					{
						int num3 = this.ReadChar();
						int num4 = num3;
						if (num4 == -1)
						{
							throw this.NotWFError("Unexpected end of stream.");
						}
						if (num4 != 33)
						{
							if (num4 != 63)
							{
								throw this.NotWFError("Syntax Error after '<' character: " + (char)num3);
							}
							this.ReadProcessingInstruction();
						}
						else
						{
							this.CompileDeclaration();
						}
					}
				}
				else
				{
					if (this.processingInternalSubset)
					{
						this.DTD.InternalSubsetHasPEReference = true;
					}
					string peName = this.ReadName();
					this.Expect(59);
					DTDParameterEntityDeclaration pedecl = this.GetPEDecl(peName);
					if (pedecl != null)
					{
						this.currentInput.PushPEBuffer(pedecl);
						while (this.currentInput.HasPEBuffer)
						{
							this.ProcessDTDSubset();
						}
						this.SkipWhitespace();
					}
				}
				this.currentInput.AllowTextDecl = false;
				return true;
			}
			return false;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x000279FC File Offset: 0x00025BFC
		private void CompileDeclaration()
		{
			int num = this.ReadChar();
			if (num != 45)
			{
				if (num != 65)
				{
					if (num != 69)
					{
						if (num != 78)
						{
							if (num != 91)
							{
								throw this.NotWFError("Syntax Error after '<!' characters.");
							}
							this.SkipWhitespace();
							this.TryExpandPERef();
							this.Expect(73);
							int num2 = this.ReadChar();
							if (num2 != 71)
							{
								if (num2 == 78)
								{
									this.Expect("CLUDE");
									this.ExpectAfterWhitespace('[');
									this.dtdIncludeSect++;
								}
							}
							else
							{
								this.Expect("NORE");
								this.ReadIgnoreSect();
							}
						}
						else
						{
							this.Expect("OTATION");
							DTDNotationDeclaration dtdnotationDeclaration = this.ReadNotationDecl();
							this.DTD.NotationDecls.Add(dtdnotationDeclaration.Name, dtdnotationDeclaration);
						}
					}
					else
					{
						switch (this.ReadChar())
						{
						case 76:
						{
							this.Expect("EMENT");
							DTDElementDeclaration dtdelementDeclaration = this.ReadElementDecl();
							this.DTD.ElementDecls.Add(dtdelementDeclaration.Name, dtdelementDeclaration);
							goto IL_167;
						}
						case 78:
						{
							this.Expect("TITY");
							if (!this.SkipWhitespace())
							{
								throw this.NotWFError("Whitespace is required after '<!ENTITY' in DTD entity declaration.");
							}
							while (this.PeekChar() == 37)
							{
								this.ReadChar();
								if (!this.SkipWhitespace())
								{
									this.ExpandPERef();
								}
								else
								{
									this.TryExpandPERef();
									if (XmlChar.IsNameChar(this.PeekChar()))
									{
										this.ReadParameterEntityDecl();
										goto IL_167;
									}
									throw this.NotWFError("expected name character");
								}
							}
							DTDEntityDeclaration dtdentityDeclaration = this.ReadEntityDecl();
							if (this.DTD.EntityDecls[dtdentityDeclaration.Name] == null)
							{
								this.DTD.EntityDecls.Add(dtdentityDeclaration.Name, dtdentityDeclaration);
							}
							goto IL_167;
						}
						}
						throw this.NotWFError("Syntax Error after '<!E' (ELEMENT or ENTITY must be found)");
						IL_167:;
					}
				}
				else
				{
					this.Expect("TTLIST");
					DTDAttListDeclaration dtdattListDeclaration = this.ReadAttListDecl();
					this.DTD.AttListDecls.Add(dtdattListDeclaration.Name, dtdattListDeclaration);
				}
			}
			else
			{
				this.Expect(45);
				this.ReadComment();
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00027C54 File Offset: 0x00025E54
		private void ReadIgnoreSect()
		{
			this.ExpectAfterWhitespace('[');
			int i = 1;
			while (i > 0)
			{
				int num = this.ReadChar();
				if (num == -1)
				{
					throw this.NotWFError("Unexpected IGNORE section end.");
				}
				if (num != 60)
				{
					if (num == 93)
					{
						if (this.PeekChar() == 93)
						{
							this.ReadChar();
							if (this.PeekChar() == 62)
							{
								this.ReadChar();
								i--;
							}
						}
					}
				}
				else if (this.PeekChar() == 33)
				{
					this.ReadChar();
					if (this.PeekChar() == 91)
					{
						this.ReadChar();
						i++;
					}
				}
			}
			if (i != 0)
			{
				throw this.NotWFError("IGNORE section is not ended correctly.");
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00027D30 File Offset: 0x00025F30
		private DTDElementDeclaration ReadElementDecl()
		{
			DTDElementDeclaration dtdelementDeclaration = new DTDElementDeclaration(this.DTD);
			dtdelementDeclaration.IsInternalSubset = this.processingInternalSubset;
			if (!this.SkipWhitespace())
			{
				throw this.NotWFError("Whitespace is required between '<!ELEMENT' and name in DTD element declaration.");
			}
			this.TryExpandPERef();
			dtdelementDeclaration.Name = this.ReadName();
			if (!this.SkipWhitespace())
			{
				throw this.NotWFError("Whitespace is required between name and content in DTD element declaration.");
			}
			this.TryExpandPERef();
			this.ReadContentSpec(dtdelementDeclaration);
			this.SkipWhitespace();
			this.TryExpandPERef();
			this.Expect(62);
			return dtdelementDeclaration;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00027DBC File Offset: 0x00025FBC
		private void ReadContentSpec(DTDElementDeclaration decl)
		{
			this.TryExpandPERef();
			int num = this.ReadChar();
			if (num != 40)
			{
				if (num != 65)
				{
					if (num != 69)
					{
						throw this.NotWFError("ContentSpec is missing.");
					}
					decl.IsEmpty = true;
					this.Expect("MPTY");
				}
				else
				{
					decl.IsAny = true;
					this.Expect("NY");
				}
			}
			else
			{
				DTDContentModel contentModel = decl.ContentModel;
				this.SkipWhitespace();
				this.TryExpandPERef();
				if (this.PeekChar() == 35)
				{
					decl.IsMixedContent = true;
					contentModel.Occurence = DTDOccurence.ZeroOrMore;
					contentModel.OrderType = DTDContentOrderType.Or;
					this.Expect("#PCDATA");
					this.SkipWhitespace();
					this.TryExpandPERef();
					while (this.PeekChar() != 41)
					{
						this.SkipWhitespace();
						if (this.PeekChar() == 37)
						{
							this.TryExpandPERef();
						}
						else
						{
							this.Expect(124);
							this.SkipWhitespace();
							this.TryExpandPERef();
							DTDContentModel dtdcontentModel = new DTDContentModel(this.DTD, decl.Name);
							dtdcontentModel.ElementName = this.ReadName();
							this.AddContentModel(contentModel.ChildModels, dtdcontentModel);
							this.SkipWhitespace();
							this.TryExpandPERef();
						}
					}
					this.Expect(41);
					if (contentModel.ChildModels.Count > 0)
					{
						this.Expect(42);
					}
					else if (this.PeekChar() == 42)
					{
						this.Expect(42);
					}
				}
				else
				{
					contentModel.ChildModels.Add(this.ReadCP(decl));
					this.SkipWhitespace();
					for (;;)
					{
						if (this.PeekChar() == 37)
						{
							this.TryExpandPERef();
						}
						else if (this.PeekChar() == 124)
						{
							if (contentModel.OrderType == DTDContentOrderType.Seq)
							{
								break;
							}
							contentModel.OrderType = DTDContentOrderType.Or;
							this.ReadChar();
							this.SkipWhitespace();
							this.AddContentModel(contentModel.ChildModels, this.ReadCP(decl));
							this.SkipWhitespace();
						}
						else
						{
							if (this.PeekChar() != 44)
							{
								goto IL_24D;
							}
							if (contentModel.OrderType == DTDContentOrderType.Or)
							{
								goto Block_13;
							}
							contentModel.OrderType = DTDContentOrderType.Seq;
							this.ReadChar();
							this.SkipWhitespace();
							contentModel.ChildModels.Add(this.ReadCP(decl));
							this.SkipWhitespace();
						}
					}
					throw this.NotWFError("Inconsistent choice markup in sequence cp.");
					Block_13:
					throw this.NotWFError("Inconsistent sequence markup in choice cp.");
					IL_24D:
					this.Expect(41);
					int num2 = this.PeekChar();
					if (num2 != 42)
					{
						if (num2 != 43)
						{
							if (num2 == 63)
							{
								contentModel.Occurence = DTDOccurence.Optional;
								this.ReadChar();
							}
						}
						else
						{
							contentModel.Occurence = DTDOccurence.OneOrMore;
							this.ReadChar();
						}
					}
					else
					{
						contentModel.Occurence = DTDOccurence.ZeroOrMore;
						this.ReadChar();
					}
					this.SkipWhitespace();
				}
				this.SkipWhitespace();
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000280A4 File Offset: 0x000262A4
		private DTDContentModel ReadCP(DTDElementDeclaration elem)
		{
			this.TryExpandPERef();
			DTDContentModel dtdcontentModel;
			if (this.PeekChar() == 40)
			{
				dtdcontentModel = new DTDContentModel(this.DTD, elem.Name);
				this.ReadChar();
				this.SkipWhitespace();
				dtdcontentModel.ChildModels.Add(this.ReadCP(elem));
				this.SkipWhitespace();
				for (;;)
				{
					if (this.PeekChar() == 37)
					{
						this.TryExpandPERef();
					}
					else if (this.PeekChar() == 124)
					{
						if (dtdcontentModel.OrderType == DTDContentOrderType.Seq)
						{
							break;
						}
						dtdcontentModel.OrderType = DTDContentOrderType.Or;
						this.ReadChar();
						this.SkipWhitespace();
						this.AddContentModel(dtdcontentModel.ChildModels, this.ReadCP(elem));
						this.SkipWhitespace();
					}
					else
					{
						if (this.PeekChar() != 44)
						{
							goto IL_119;
						}
						if (dtdcontentModel.OrderType == DTDContentOrderType.Or)
						{
							goto Block_6;
						}
						dtdcontentModel.OrderType = DTDContentOrderType.Seq;
						this.ReadChar();
						this.SkipWhitespace();
						dtdcontentModel.ChildModels.Add(this.ReadCP(elem));
						this.SkipWhitespace();
					}
				}
				throw this.NotWFError("Inconsistent choice markup in sequence cp.");
				Block_6:
				throw this.NotWFError("Inconsistent sequence markup in choice cp.");
				IL_119:
				this.ExpectAfterWhitespace(')');
			}
			else
			{
				this.TryExpandPERef();
				dtdcontentModel = new DTDContentModel(this.DTD, elem.Name);
				dtdcontentModel.ElementName = this.ReadName();
			}
			int num = this.PeekChar();
			if (num != 42)
			{
				if (num != 43)
				{
					if (num == 63)
					{
						dtdcontentModel.Occurence = DTDOccurence.Optional;
						this.ReadChar();
					}
				}
				else
				{
					dtdcontentModel.Occurence = DTDOccurence.OneOrMore;
					this.ReadChar();
				}
			}
			else
			{
				dtdcontentModel.Occurence = DTDOccurence.ZeroOrMore;
				this.ReadChar();
			}
			return dtdcontentModel;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00028264 File Offset: 0x00026464
		private void AddContentModel(DTDContentModelCollection cmc, DTDContentModel cm)
		{
			if (cm.ElementName != null)
			{
				for (int i = 0; i < cmc.Count; i++)
				{
					if (cmc[i].ElementName == cm.ElementName)
					{
						this.HandleError(new XmlSchemaException("Element content must be unique inside mixed content model.", this.LineNumber, this.LinePosition, null, this.BaseURI, null));
						return;
					}
				}
			}
			cmc.Add(cm);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000282DC File Offset: 0x000264DC
		private void ReadParameterEntityDecl()
		{
			DTDParameterEntityDeclaration dtdparameterEntityDeclaration = new DTDParameterEntityDeclaration(this.DTD);
			dtdparameterEntityDeclaration.BaseURI = this.BaseURI;
			dtdparameterEntityDeclaration.XmlResolver = this.DTD.Resolver;
			dtdparameterEntityDeclaration.Name = this.ReadName();
			if (!this.SkipWhitespace())
			{
				throw this.NotWFError("Whitespace is required after name in DTD parameter entity declaration.");
			}
			if (this.PeekChar() == 83 || this.PeekChar() == 80)
			{
				this.ReadExternalID();
				dtdparameterEntityDeclaration.PublicId = this.cachedPublicId;
				dtdparameterEntityDeclaration.SystemId = this.cachedSystemId;
				this.SkipWhitespace();
				dtdparameterEntityDeclaration.Resolve();
				this.ResolveExternalEntityReplacementText(dtdparameterEntityDeclaration);
			}
			else
			{
				this.TryExpandPERef();
				int num = this.ReadChar();
				if (num != 39 && num != 34)
				{
					throw this.NotWFError("quotation char was expected.");
				}
				this.ClearValueBuffer();
				bool flag = true;
				while (flag)
				{
					int num2 = this.ReadChar();
					int num3 = num2;
					if (num3 == -1)
					{
						throw this.NotWFError("unexpected end of stream in entity value definition.");
					}
					if (num3 != 34)
					{
						if (num3 != 39)
						{
							if (XmlChar.IsInvalid(num2))
							{
								throw this.NotWFError("Invalid character was used to define parameter entity.");
							}
							this.AppendValueChar(num2);
						}
						else if (num == 39)
						{
							flag = false;
						}
						else
						{
							this.AppendValueChar(39);
						}
					}
					else if (num == 34)
					{
						flag = false;
					}
					else
					{
						this.AppendValueChar(34);
					}
				}
				dtdparameterEntityDeclaration.LiteralEntityValue = this.CreateValueString();
				this.ClearValueBuffer();
				this.ResolveInternalEntityReplacementText(dtdparameterEntityDeclaration);
			}
			this.ExpectAfterWhitespace('>');
			if (this.DTD.PEDecls[dtdparameterEntityDeclaration.Name] == null)
			{
				this.DTD.PEDecls.Add(dtdparameterEntityDeclaration.Name, dtdparameterEntityDeclaration);
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000284A8 File Offset: 0x000266A8
		private void ResolveExternalEntityReplacementText(DTDEntityBase decl)
		{
			if (decl.SystemId != null && decl.SystemId.Length > 0)
			{
				XmlTextReader xmlTextReader = new XmlTextReader(decl.LiteralEntityValue, XmlNodeType.Element, null);
				xmlTextReader.SkipTextDeclaration();
				if (decl is DTDEntityDeclaration && this.DTD.EntityDecls[decl.Name] == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					xmlTextReader.Normalization = this.Normalization;
					xmlTextReader.Read();
					while (!xmlTextReader.EOF)
					{
						stringBuilder.Append(xmlTextReader.ReadOuterXml());
					}
					decl.ReplacementText = stringBuilder.ToString();
				}
				else
				{
					decl.ReplacementText = xmlTextReader.GetRemainder().ReadToEnd();
				}
			}
			else
			{
				decl.ReplacementText = decl.LiteralEntityValue;
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00028574 File Offset: 0x00026774
		private void ResolveInternalEntityReplacementText(DTDEntityBase decl)
		{
			string literalEntityValue = decl.LiteralEntityValue;
			int length = literalEntityValue.Length;
			this.ClearValueBuffer();
			for (int i = 0; i < length; i++)
			{
				int num = (int)literalEntityValue[i];
				int num2 = num;
				if (num2 != 37)
				{
					if (num2 != 38)
					{
						this.AppendValueChar(num);
					}
					else
					{
						i++;
						int num3 = literalEntityValue.IndexOf(';', i);
						if (num3 < i + 1)
						{
							throw new XmlException(decl, decl.BaseURI, "Invalid reference markup.");
						}
						if (literalEntityValue[i] == '#')
						{
							i++;
							num = this.GetCharacterReference(decl, literalEntityValue, ref i, num3);
							if (XmlChar.IsInvalid(num))
							{
								throw this.NotWFError("Invalid character was used to define parameter entity.");
							}
							if (XmlChar.IsInvalid(num))
							{
								throw new XmlException(decl, decl.BaseURI, "Invalid character was found in the entity declaration.");
							}
							this.AppendValueChar(num);
						}
						else
						{
							string text = literalEntityValue.Substring(i, num3 - i);
							if (!XmlChar.IsName(text))
							{
								throw this.NotWFError(string.Format("'{0}' is not a valid entity reference name.", text));
							}
							this.AppendValueChar(38);
							this.valueBuffer.Append(text);
							this.AppendValueChar(59);
							i = num3;
						}
					}
				}
				else
				{
					i++;
					int num3 = literalEntityValue.IndexOf(';', i);
					if (num3 < i + 1)
					{
						throw new XmlException(decl, decl.BaseURI, "Invalid reference markup.");
					}
					string text = literalEntityValue.Substring(i, num3 - i);
					this.valueBuffer.Append(this.GetPEValue(text));
					i = num3;
				}
			}
			decl.ReplacementText = this.CreateValueString();
			this.ClearValueBuffer();
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00028720 File Offset: 0x00026920
		private int GetCharacterReference(DTDEntityBase li, string value, ref int index, int end)
		{
			int result = 0;
			if (value[index] == 'x')
			{
				try
				{
					result = int.Parse(value.Substring(index + 1, end - index - 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				}
				catch (FormatException)
				{
					throw new XmlException(li, li.BaseURI, "Invalid number for a character reference.");
				}
			}
			else
			{
				try
				{
					result = int.Parse(value.Substring(index, end - index), CultureInfo.InvariantCulture);
				}
				catch (FormatException)
				{
					throw new XmlException(li, li.BaseURI, "Invalid number for a character reference.");
				}
			}
			index = end;
			return result;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000287F0 File Offset: 0x000269F0
		private string GetPEValue(string peName)
		{
			DTDParameterEntityDeclaration pedecl = this.GetPEDecl(peName);
			return (pedecl == null) ? string.Empty : pedecl.ReplacementText;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0002881C File Offset: 0x00026A1C
		private DTDParameterEntityDeclaration GetPEDecl(string peName)
		{
			DTDParameterEntityDeclaration dtdparameterEntityDeclaration = this.DTD.PEDecls[peName];
			if (dtdparameterEntityDeclaration != null)
			{
				if (dtdparameterEntityDeclaration.IsInternalSubset)
				{
					throw this.NotWFError("Parameter entity is not allowed in internal subset entity '" + peName + "'");
				}
				return dtdparameterEntityDeclaration;
			}
			else
			{
				if ((this.DTD.SystemId == null && !this.DTD.InternalSubsetHasPEReference) || this.DTD.IsStandalone)
				{
					throw this.NotWFError(string.Format("Parameter entity '{0}' not found.", peName));
				}
				this.HandleError(new XmlSchemaException("Parameter entity " + peName + " not found.", null));
				return null;
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000288C4 File Offset: 0x00026AC4
		private bool TryExpandPERef()
		{
			if (this.PeekChar() != 37)
			{
				return false;
			}
			while (this.PeekChar() == 37)
			{
				this.TryExpandPERefSpaceKeep();
				this.SkipWhitespace();
			}
			return true;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00028904 File Offset: 0x00026B04
		private bool TryExpandPERefSpaceKeep()
		{
			if (this.PeekChar() != 37)
			{
				return false;
			}
			if (this.processingInternalSubset)
			{
				throw this.NotWFError("Parameter entity reference is not allowed inside internal subset.");
			}
			this.ReadChar();
			this.ExpandPERef();
			return true;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00028948 File Offset: 0x00026B48
		private void ExpandPERef()
		{
			string text = this.ReadName();
			this.Expect(59);
			DTDParameterEntityDeclaration dtdparameterEntityDeclaration = this.DTD.PEDecls[text];
			if (dtdparameterEntityDeclaration == null)
			{
				this.HandleError(new XmlSchemaException("Parameter entity " + text + " not found.", null));
				return;
			}
			this.currentInput.PushPEBuffer(dtdparameterEntityDeclaration);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000289A8 File Offset: 0x00026BA8
		private DTDEntityDeclaration ReadEntityDecl()
		{
			DTDEntityDeclaration dtdentityDeclaration = new DTDEntityDeclaration(this.DTD);
			dtdentityDeclaration.BaseURI = this.BaseURI;
			dtdentityDeclaration.XmlResolver = this.DTD.Resolver;
			dtdentityDeclaration.IsInternalSubset = this.processingInternalSubset;
			this.TryExpandPERef();
			dtdentityDeclaration.Name = this.ReadName();
			if (!this.SkipWhitespace())
			{
				throw this.NotWFError("Whitespace is required between name and content in DTD entity declaration.");
			}
			this.TryExpandPERef();
			if (this.PeekChar() == 83 || this.PeekChar() == 80)
			{
				this.ReadExternalID();
				dtdentityDeclaration.PublicId = this.cachedPublicId;
				dtdentityDeclaration.SystemId = this.cachedSystemId;
				if (this.SkipWhitespace() && this.PeekChar() == 78)
				{
					this.Expect("NDATA");
					if (!this.SkipWhitespace())
					{
						throw this.NotWFError("Whitespace is required after NDATA.");
					}
					dtdentityDeclaration.NotationName = this.ReadName();
				}
				if (dtdentityDeclaration.NotationName == null)
				{
					dtdentityDeclaration.Resolve();
					this.ResolveExternalEntityReplacementText(dtdentityDeclaration);
				}
				else
				{
					dtdentityDeclaration.LiteralEntityValue = string.Empty;
					dtdentityDeclaration.ReplacementText = string.Empty;
				}
			}
			else
			{
				this.ReadEntityValueDecl(dtdentityDeclaration);
				this.ResolveInternalEntityReplacementText(dtdentityDeclaration);
			}
			this.SkipWhitespace();
			this.TryExpandPERef();
			this.Expect(62);
			return dtdentityDeclaration;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00028AF8 File Offset: 0x00026CF8
		private void ReadEntityValueDecl(DTDEntityDeclaration decl)
		{
			this.SkipWhitespace();
			int num = this.ReadChar();
			if (num != 39 && num != 34)
			{
				throw this.NotWFError("quotation char was expected.");
			}
			this.ClearValueBuffer();
			while (this.PeekChar() != num)
			{
				int num2 = this.ReadChar();
				int num3 = num2;
				if (num3 == -1)
				{
					throw this.NotWFError("unexpected end of stream.");
				}
				if (num3 != 37)
				{
					if (this.normalization && XmlChar.IsInvalid(num2))
					{
						throw this.NotWFError("Invalid character was found in the entity declaration.");
					}
					this.AppendValueChar(num2);
				}
				else
				{
					string text = this.ReadName();
					this.Expect(59);
					if (decl.IsInternalSubset)
					{
						throw this.NotWFError(string.Format("Parameter entity is not allowed in internal subset entity '{0}'", text));
					}
					this.valueBuffer.Append(this.GetPEValue(text));
				}
			}
			string literalEntityValue = this.CreateValueString();
			this.ClearValueBuffer();
			this.Expect(num);
			decl.LiteralEntityValue = literalEntityValue;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00028C00 File Offset: 0x00026E00
		private DTDAttListDeclaration ReadAttListDecl()
		{
			this.TryExpandPERefSpaceKeep();
			if (!this.SkipWhitespace())
			{
				throw this.NotWFError("Whitespace is required between ATTLIST and name in DTD attlist declaration.");
			}
			this.TryExpandPERef();
			string name = this.ReadName();
			DTDAttListDeclaration dtdattListDeclaration = this.DTD.AttListDecls[name];
			if (dtdattListDeclaration == null)
			{
				dtdattListDeclaration = new DTDAttListDeclaration(this.DTD);
			}
			dtdattListDeclaration.IsInternalSubset = this.processingInternalSubset;
			dtdattListDeclaration.Name = name;
			if (!this.SkipWhitespace() && this.PeekChar() != 62)
			{
				throw this.NotWFError("Whitespace is required between name and content in non-empty DTD attlist declaration.");
			}
			this.TryExpandPERef();
			while (XmlChar.IsNameChar(this.PeekChar()))
			{
				DTDAttributeDefinition dtdattributeDefinition = this.ReadAttributeDefinition();
				if (dtdattributeDefinition.Datatype.TokenizedType == XmlTokenizedType.ID)
				{
					for (int i = 0; i < dtdattListDeclaration.Definitions.Count; i++)
					{
						DTDAttributeDefinition dtdattributeDefinition2 = dtdattListDeclaration[i];
						if (dtdattributeDefinition2.Datatype.TokenizedType == XmlTokenizedType.ID)
						{
							this.HandleError(new XmlSchemaException("AttList declaration must not contain two or more ID attributes.", dtdattributeDefinition.LineNumber, dtdattributeDefinition.LinePosition, null, dtdattributeDefinition.BaseURI, null));
							break;
						}
					}
				}
				if (dtdattListDeclaration[dtdattributeDefinition.Name] == null)
				{
					dtdattListDeclaration.Add(dtdattributeDefinition);
				}
				this.SkipWhitespace();
				this.TryExpandPERef();
			}
			this.SkipWhitespace();
			this.TryExpandPERef();
			this.Expect(62);
			return dtdattListDeclaration;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00028D68 File Offset: 0x00026F68
		private DTDAttributeDefinition ReadAttributeDefinition()
		{
			DTDAttributeDefinition dtdattributeDefinition = new DTDAttributeDefinition(this.DTD);
			dtdattributeDefinition.IsInternalSubset = this.processingInternalSubset;
			this.TryExpandPERef();
			dtdattributeDefinition.Name = this.ReadName();
			if (!this.SkipWhitespace())
			{
				throw this.NotWFError("Whitespace is required between name and content in DTD attribute definition.");
			}
			this.TryExpandPERef();
			int num = this.PeekChar();
			switch (num)
			{
			case 67:
				this.Expect("CDATA");
				dtdattributeDefinition.Datatype = XmlSchemaDatatype.FromName("normalizedString", "http://www.w3.org/2001/XMLSchema");
				break;
			default:
				if (num != 73)
				{
					if (num != 78)
					{
						dtdattributeDefinition.Datatype = XmlSchemaDatatype.FromName("NMTOKEN", "http://www.w3.org/2001/XMLSchema");
						this.TryExpandPERef();
						this.Expect(40);
						this.SkipWhitespace();
						this.TryExpandPERef();
						dtdattributeDefinition.EnumeratedAttributeDeclaration.Add(dtdattributeDefinition.Datatype.Normalize(this.ReadNmToken()));
						this.SkipWhitespace();
						while (this.PeekChar() == 124)
						{
							this.ReadChar();
							this.SkipWhitespace();
							this.TryExpandPERef();
							dtdattributeDefinition.EnumeratedAttributeDeclaration.Add(dtdattributeDefinition.Datatype.Normalize(this.ReadNmToken()));
							this.SkipWhitespace();
							this.TryExpandPERef();
						}
						this.Expect(41);
					}
					else
					{
						this.ReadChar();
						switch (this.PeekChar())
						{
						case 77:
							this.Expect("MTOKEN");
							if (this.PeekChar() == 83)
							{
								this.ReadChar();
								dtdattributeDefinition.Datatype = XmlSchemaDatatype.FromName("NMTOKENS", "http://www.w3.org/2001/XMLSchema");
							}
							else
							{
								dtdattributeDefinition.Datatype = XmlSchemaDatatype.FromName("NMTOKEN", "http://www.w3.org/2001/XMLSchema");
							}
							goto IL_2DE;
						case 79:
							this.Expect("OTATION");
							dtdattributeDefinition.Datatype = XmlSchemaDatatype.FromName("NOTATION", "http://www.w3.org/2001/XMLSchema");
							this.TryExpandPERefSpaceKeep();
							if (!this.SkipWhitespace())
							{
								throw this.NotWFError("Whitespace is required after notation name in DTD attribute definition.");
							}
							this.Expect(40);
							this.SkipWhitespace();
							this.TryExpandPERef();
							dtdattributeDefinition.EnumeratedNotations.Add(this.ReadName());
							this.SkipWhitespace();
							this.TryExpandPERef();
							while (this.PeekChar() == 124)
							{
								this.ReadChar();
								this.SkipWhitespace();
								this.TryExpandPERef();
								dtdattributeDefinition.EnumeratedNotations.Add(this.ReadName());
								this.SkipWhitespace();
								this.TryExpandPERef();
							}
							this.Expect(41);
							goto IL_2DE;
						}
						throw this.NotWFError("attribute declaration syntax error.");
						IL_2DE:;
					}
				}
				else
				{
					this.Expect("ID");
					if (this.PeekChar() == 82)
					{
						this.Expect("REF");
						if (this.PeekChar() == 83)
						{
							this.ReadChar();
							dtdattributeDefinition.Datatype = XmlSchemaDatatype.FromName("IDREFS", "http://www.w3.org/2001/XMLSchema");
						}
						else
						{
							dtdattributeDefinition.Datatype = XmlSchemaDatatype.FromName("IDREF", "http://www.w3.org/2001/XMLSchema");
						}
					}
					else
					{
						dtdattributeDefinition.Datatype = XmlSchemaDatatype.FromName("ID", "http://www.w3.org/2001/XMLSchema");
					}
				}
				break;
			case 69:
			{
				this.Expect("ENTIT");
				int num2 = this.ReadChar();
				if (num2 != 73)
				{
					if (num2 == 89)
					{
						dtdattributeDefinition.Datatype = XmlSchemaDatatype.FromName("ENTITY", "http://www.w3.org/2001/XMLSchema");
					}
				}
				else
				{
					this.Expect("ES");
					dtdattributeDefinition.Datatype = XmlSchemaDatatype.FromName("ENTITIES", "http://www.w3.org/2001/XMLSchema");
				}
				break;
			}
			}
			this.TryExpandPERefSpaceKeep();
			if (!this.SkipWhitespace())
			{
				throw this.NotWFError("Whitespace is required between type and occurence in DTD attribute definition.");
			}
			this.ReadAttributeDefaultValue(dtdattributeDefinition);
			return dtdattributeDefinition;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00029134 File Offset: 0x00027334
		private void ReadAttributeDefaultValue(DTDAttributeDefinition def)
		{
			if (this.PeekChar() == 35)
			{
				this.ReadChar();
				int num = this.PeekChar();
				switch (num)
				{
				case 70:
					this.Expect("FIXED");
					def.OccurenceType = DTDAttributeOccurenceType.Fixed;
					if (!this.SkipWhitespace())
					{
						throw this.NotWFError("Whitespace is required between FIXED and actual value in DTD attribute definition.");
					}
					def.UnresolvedDefaultValue = this.ReadDefaultAttribute();
					break;
				default:
					if (num == 82)
					{
						this.Expect("REQUIRED");
						def.OccurenceType = DTDAttributeOccurenceType.Required;
					}
					break;
				case 73:
					this.Expect("IMPLIED");
					def.OccurenceType = DTDAttributeOccurenceType.Optional;
					break;
				}
			}
			else
			{
				this.SkipWhitespace();
				this.TryExpandPERef();
				def.UnresolvedDefaultValue = this.ReadDefaultAttribute();
			}
			if (def.DefaultValue != null)
			{
				string text = def.Datatype.Normalize(def.DefaultValue);
				bool flag = false;
				object obj = null;
				if (def.EnumeratedAttributeDeclaration.Count > 0 && !def.EnumeratedAttributeDeclaration.Contains(text))
				{
					this.HandleError(new XmlSchemaException("Default value is not one of the enumerated values.", def.LineNumber, def.LinePosition, null, def.BaseURI, null));
					flag = true;
				}
				if (def.EnumeratedNotations.Count > 0 && !def.EnumeratedNotations.Contains(text))
				{
					this.HandleError(new XmlSchemaException("Default value is not one of the enumerated notation values.", def.LineNumber, def.LinePosition, null, def.BaseURI, null));
					flag = true;
				}
				if (!flag)
				{
					try
					{
						obj = def.Datatype.ParseValue(text, this.DTD.NameTable, null);
					}
					catch (Exception innerException)
					{
						this.HandleError(new XmlSchemaException("Invalid default value for ENTITY type.", def.LineNumber, def.LinePosition, null, def.BaseURI, innerException));
						flag = true;
					}
				}
				if (!flag)
				{
					XmlTokenizedType tokenizedType = def.Datatype.TokenizedType;
					if (tokenizedType != XmlTokenizedType.ENTITY)
					{
						if (tokenizedType == XmlTokenizedType.ENTITIES)
						{
							foreach (string name in obj as string[])
							{
								if (this.DTD.EntityDecls[name] == null)
								{
									this.HandleError(new XmlSchemaException("Specified entity declaration used by default attribute value was not found.", def.LineNumber, def.LinePosition, null, def.BaseURI, null));
								}
							}
						}
					}
					else if (this.DTD.EntityDecls[text] == null)
					{
						this.HandleError(new XmlSchemaException("Specified entity declaration used by default attribute value was not found.", def.LineNumber, def.LinePosition, null, def.BaseURI, null));
					}
				}
			}
			if (def.Datatype != null && def.Datatype.TokenizedType == XmlTokenizedType.ID && def.UnresolvedDefaultValue != null)
			{
				this.HandleError(new XmlSchemaException("ID attribute must not have fixed value constraint.", def.LineNumber, def.LinePosition, null, def.BaseURI, null));
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00029444 File Offset: 0x00027644
		private DTDNotationDeclaration ReadNotationDecl()
		{
			DTDNotationDeclaration dtdnotationDeclaration = new DTDNotationDeclaration(this.DTD);
			if (!this.SkipWhitespace())
			{
				throw this.NotWFError("Whitespace is required between NOTATION and name in DTD notation declaration.");
			}
			this.TryExpandPERef();
			dtdnotationDeclaration.Name = this.ReadName();
			dtdnotationDeclaration.Prefix = string.Empty;
			dtdnotationDeclaration.LocalName = dtdnotationDeclaration.Name;
			this.SkipWhitespace();
			if (this.PeekChar() == 80)
			{
				dtdnotationDeclaration.PublicId = this.ReadPubidLiteral();
				bool flag = this.SkipWhitespace();
				if (this.PeekChar() == 39 || this.PeekChar() == 34)
				{
					if (!flag)
					{
						throw this.NotWFError("Whitespace is required between public id and system id.");
					}
					dtdnotationDeclaration.SystemId = this.ReadSystemLiteral(false);
					this.SkipWhitespace();
				}
			}
			else if (this.PeekChar() == 83)
			{
				dtdnotationDeclaration.SystemId = this.ReadSystemLiteral(true);
				this.SkipWhitespace();
			}
			if (dtdnotationDeclaration.PublicId == null && dtdnotationDeclaration.SystemId == null)
			{
				throw this.NotWFError("public or system declaration required for \"NOTATION\" declaration.");
			}
			this.TryExpandPERef();
			this.Expect(62);
			return dtdnotationDeclaration;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00029560 File Offset: 0x00027760
		private void ReadExternalID()
		{
			switch (this.PeekChar())
			{
			case 80:
				this.cachedPublicId = this.ReadPubidLiteral();
				if (!this.SkipWhitespace())
				{
					throw this.NotWFError("Whitespace is required between PUBLIC id and SYSTEM id.");
				}
				this.cachedSystemId = this.ReadSystemLiteral(false);
				break;
			case 83:
				this.cachedSystemId = this.ReadSystemLiteral(true);
				break;
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000295DC File Offset: 0x000277DC
		private string ReadSystemLiteral(bool expectSYSTEM)
		{
			if (expectSYSTEM)
			{
				this.Expect("SYSTEM");
				if (!this.SkipWhitespace())
				{
					throw this.NotWFError("Whitespace is required after 'SYSTEM'.");
				}
			}
			else
			{
				this.SkipWhitespace();
			}
			int num = this.ReadChar();
			int num2 = 0;
			this.ClearValueBuffer();
			while (num2 != num)
			{
				num2 = this.ReadChar();
				if (num2 < 0)
				{
					throw this.NotWFError("Unexpected end of stream in ExternalID.");
				}
				if (num2 != num)
				{
					this.AppendValueChar(num2);
				}
			}
			return this.CreateValueString();
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00029668 File Offset: 0x00027868
		private string ReadPubidLiteral()
		{
			this.Expect("PUBLIC");
			if (!this.SkipWhitespace())
			{
				throw this.NotWFError("Whitespace is required after 'PUBLIC'.");
			}
			int num = this.ReadChar();
			int num2 = 0;
			this.ClearValueBuffer();
			while (num2 != num)
			{
				num2 = this.ReadChar();
				if (num2 < 0)
				{
					throw this.NotWFError("Unexpected end of stream in ExternalID.");
				}
				if (num2 != num && !XmlChar.IsPubidChar(num2))
				{
					throw this.NotWFError(string.Format("character '{0}' not allowed for PUBLIC ID", (char)num2));
				}
				if (num2 != num)
				{
					this.AppendValueChar(num2);
				}
			}
			return this.CreateValueString();
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0002970C File Offset: 0x0002790C
		internal string ReadName()
		{
			return this.ReadNameOrNmToken(false);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00029718 File Offset: 0x00027918
		private string ReadNmToken()
		{
			return this.ReadNameOrNmToken(true);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00029724 File Offset: 0x00027924
		private string ReadNameOrNmToken(bool isNameToken)
		{
			int num = this.PeekChar();
			if (isNameToken)
			{
				if (!XmlChar.IsNameChar(num))
				{
					throw this.NotWFError(string.Format("a nmtoken did not start with a legal character {0} ({1})", num, (char)num));
				}
			}
			else if (!XmlChar.IsFirstNameChar(num))
			{
				throw this.NotWFError(string.Format("a name did not start with a legal character {0} ({1})", num, (char)num));
			}
			this.nameLength = 0;
			this.AppendNameChar(this.ReadChar());
			while (XmlChar.IsNameChar(this.PeekChar()))
			{
				this.AppendNameChar(this.ReadChar());
			}
			return this.CreateNameString();
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000297D0 File Offset: 0x000279D0
		private void Expect(int expected)
		{
			int num = this.ReadChar();
			if (num != expected)
			{
				throw this.NotWFError(string.Format(CultureInfo.InvariantCulture, "expected '{0}' ({1:X}) but found '{2}' ({3:X})", new object[]
				{
					(char)expected,
					expected,
					(char)num,
					num
				}));
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00029830 File Offset: 0x00027A30
		private void Expect(string expected)
		{
			int length = expected.Length;
			for (int i = 0; i < length; i++)
			{
				this.Expect((int)expected[i]);
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00029864 File Offset: 0x00027A64
		private void ExpectAfterWhitespace(char c)
		{
			int num;
			do
			{
				num = this.ReadChar();
			}
			while (XmlChar.IsWhitespace(num));
			if ((int)c != num)
			{
				throw this.NotWFError(string.Format(CultureInfo.InvariantCulture, "Expected {0} but found {1} [{2}].", new object[]
				{
					c,
					(char)num,
					num
				}));
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000298D4 File Offset: 0x00027AD4
		private bool SkipWhitespace()
		{
			bool result = XmlChar.IsWhitespace(this.PeekChar());
			while (XmlChar.IsWhitespace(this.PeekChar()))
			{
				this.ReadChar();
			}
			return result;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0002990C File Offset: 0x00027B0C
		private int PeekChar()
		{
			return this.currentInput.PeekChar();
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0002991C File Offset: 0x00027B1C
		private int ReadChar()
		{
			return this.currentInput.ReadChar();
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0002992C File Offset: 0x00027B2C
		private void ReadComment()
		{
			this.currentInput.AllowTextDecl = false;
			while (this.PeekChar() != -1)
			{
				int num = this.ReadChar();
				if (num == 45 && this.PeekChar() == 45)
				{
					this.ReadChar();
					if (this.PeekChar() != 62)
					{
						throw this.NotWFError("comments cannot contain '--'");
					}
					this.ReadChar();
					break;
				}
				else if (XmlChar.IsInvalid(num))
				{
					throw this.NotWFError("Not allowed character was found.");
				}
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000299B8 File Offset: 0x00027BB8
		private void ReadProcessingInstruction()
		{
			string text = this.ReadName();
			if (text == "xml")
			{
				this.ReadTextDeclaration();
				return;
			}
			if (CultureInfo.InvariantCulture.CompareInfo.Compare(text, "xml", CompareOptions.IgnoreCase) == 0)
			{
				throw this.NotWFError("Not allowed processing instruction name which starts with 'X', 'M', 'L' was found.");
			}
			this.currentInput.AllowTextDecl = false;
			if (!this.SkipWhitespace() && this.PeekChar() != 63)
			{
				throw this.NotWFError("Invalid processing instruction name was found.");
			}
			while (this.PeekChar() != -1)
			{
				int num = this.ReadChar();
				if (num == 63 && this.PeekChar() == 62)
				{
					this.ReadChar();
					break;
				}
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00029A74 File Offset: 0x00027C74
		private void ReadTextDeclaration()
		{
			if (!this.currentInput.AllowTextDecl)
			{
				throw this.NotWFError("Text declaration cannot appear in this state.");
			}
			this.currentInput.AllowTextDecl = false;
			this.SkipWhitespace();
			int num3;
			if (this.PeekChar() == 118)
			{
				this.Expect("version");
				this.ExpectAfterWhitespace('=');
				this.SkipWhitespace();
				int num = this.ReadChar();
				char[] array = new char[3];
				int num2 = 0;
				num3 = num;
				if (num3 != 34 && num3 != 39)
				{
					throw this.NotWFError("Invalid version declaration inside text declaration.");
				}
				while (this.PeekChar() != num)
				{
					if (this.PeekChar() == -1)
					{
						throw this.NotWFError("Invalid version declaration inside text declaration.");
					}
					if (num2 == 3)
					{
						throw this.NotWFError("Invalid version number inside text declaration.");
					}
					array[num2] = (char)this.ReadChar();
					num2++;
					if (num2 == 3 && new string(array) != "1.0")
					{
						throw this.NotWFError("Invalid version number inside text declaration.");
					}
				}
				this.ReadChar();
				this.SkipWhitespace();
			}
			if (this.PeekChar() != 101)
			{
				throw this.NotWFError("Encoding declaration is mandatory in text declaration.");
			}
			this.Expect("encoding");
			this.ExpectAfterWhitespace('=');
			this.SkipWhitespace();
			int num4 = this.ReadChar();
			num3 = num4;
			if (num3 != 34 && num3 != 39)
			{
				throw this.NotWFError("Invalid encoding declaration inside text declaration.");
			}
			while (this.PeekChar() != num4)
			{
				if (this.ReadChar() == -1)
				{
					throw this.NotWFError("Invalid encoding declaration inside text declaration.");
				}
			}
			this.ReadChar();
			this.SkipWhitespace();
			this.Expect("?>");
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00029C40 File Offset: 0x00027E40
		private void AppendNameChar(int ch)
		{
			this.CheckNameCapacity();
			if (ch < 65535)
			{
				this.nameBuffer[this.nameLength++] = (char)ch;
			}
			else
			{
				this.nameBuffer[this.nameLength++] = (char)(ch / 65536 + 55296 - 1);
				this.CheckNameCapacity();
				this.nameBuffer[this.nameLength++] = (char)(ch % 65536 + 56320);
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00029CD4 File Offset: 0x00027ED4
		private void CheckNameCapacity()
		{
			if (this.nameLength == this.nameCapacity)
			{
				this.nameCapacity *= 2;
				char[] sourceArray = this.nameBuffer;
				this.nameBuffer = new char[this.nameCapacity];
				Array.Copy(sourceArray, this.nameBuffer, this.nameLength);
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00029D2C File Offset: 0x00027F2C
		private string CreateNameString()
		{
			return this.DTD.NameTable.Add(this.nameBuffer, 0, this.nameLength);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00029D4C File Offset: 0x00027F4C
		private void AppendValueChar(int ch)
		{
			if (ch < 65536)
			{
				this.valueBuffer.Append((char)ch);
				return;
			}
			if (ch > 1114111)
			{
				throw new XmlException("The numeric entity value is too large", null, this.LineNumber, this.LinePosition);
			}
			int num = ch - 65536;
			this.valueBuffer.Append((char)((num >> 10) + 55296));
			this.valueBuffer.Append((char)((num & 1023) + 56320));
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00029DD0 File Offset: 0x00027FD0
		private string CreateValueString()
		{
			return this.valueBuffer.ToString();
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00029DE0 File Offset: 0x00027FE0
		private void ClearValueBuffer()
		{
			this.valueBuffer.Length = 0;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00029DF0 File Offset: 0x00027FF0
		private string ReadDefaultAttribute()
		{
			this.ClearValueBuffer();
			this.TryExpandPERef();
			int num = this.ReadChar();
			if (num != 39 && num != 34)
			{
				throw this.NotWFError("an attribute value was not quoted");
			}
			this.AppendValueChar(num);
			while (this.PeekChar() != num)
			{
				int num2 = this.ReadChar();
				int num3 = num2;
				if (num3 == -1)
				{
					throw this.NotWFError("unexpected end of file in an attribute value");
				}
				if (num3 != 38)
				{
					if (num3 == 60)
					{
						throw this.NotWFError("attribute values cannot contain '<'");
					}
					this.AppendValueChar(num2);
				}
				else
				{
					this.AppendValueChar(num2);
					if (this.PeekChar() != 35)
					{
						string text = this.ReadName();
						this.Expect(59);
						if (XmlChar.GetPredefinedEntity(text) < 0)
						{
							DTDEntityDeclaration dtdentityDeclaration = (this.DTD != null) ? this.DTD.EntityDecls[text] : null;
							if ((dtdentityDeclaration == null || dtdentityDeclaration.SystemId != null) && (this.DTD.IsStandalone || (this.DTD.SystemId == null && !this.DTD.InternalSubsetHasPEReference)))
							{
								throw this.NotWFError("Reference to external entities is not allowed in attribute value.");
							}
						}
						this.valueBuffer.Append(text);
						this.AppendValueChar(59);
					}
				}
			}
			this.ReadChar();
			this.AppendValueChar(num);
			return this.CreateValueString();
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00029F64 File Offset: 0x00028164
		private void PushParserInput(string url)
		{
			Uri uri = null;
			try
			{
				if (this.DTD.BaseURI != null && this.DTD.BaseURI.Length > 0)
				{
					uri = new Uri(this.DTD.BaseURI);
				}
			}
			catch (UriFormatException)
			{
			}
			Uri uri2 = (url == null || url.Length <= 0) ? uri : this.DTD.Resolver.ResolveUri(uri, url);
			string text = (!(uri2 != null)) ? string.Empty : uri2.ToString();
			foreach (XmlParserInput xmlParserInput in this.parserInputStack.ToArray())
			{
				if (xmlParserInput.BaseURI == text)
				{
					throw this.NotWFError("Nested inclusion is not allowed: " + url);
				}
			}
			this.parserInputStack.Push(this.currentInput);
			Stream stream = null;
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				stream = (this.DTD.Resolver.GetEntity(uri2, null, typeof(Stream)) as Stream);
				byte[] array2 = new byte[4096];
				int num;
				do
				{
					num = stream.Read(array2, 0, array2.Length);
					memoryStream.Write(array2, 0, num);
				}
				while (num > 0);
				stream.Close();
				memoryStream.Position = 0L;
				this.currentInput = new XmlParserInput(new XmlStreamReader(memoryStream), text);
			}
			catch (Exception innerException)
			{
				if (stream != null)
				{
					stream.Close();
				}
				int lineNumber = (this.currentInput != null) ? this.currentInput.LineNumber : 0;
				int linePosition = (this.currentInput != null) ? this.currentInput.LinePosition : 0;
				string sourceUri = (this.currentInput != null) ? this.currentInput.BaseURI : string.Empty;
				this.HandleError(new XmlSchemaException("Specified external entity not found. Target URL is " + url + " .", lineNumber, linePosition, null, sourceUri, innerException));
				this.currentInput = new XmlParserInput(new StringReader(string.Empty), text);
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0002A1C8 File Offset: 0x000283C8
		private void PopParserInput()
		{
			this.currentInput.Close();
			this.currentInput = (this.parserInputStack.Pop() as XmlParserInput);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0002A1EC File Offset: 0x000283EC
		private void HandleError(XmlSchemaException ex)
		{
			this.DTD.AddError(ex);
		}

		// Token: 0x04000436 RID: 1078
		private const int initialNameCapacity = 256;

		// Token: 0x04000437 RID: 1079
		private XmlParserInput currentInput;

		// Token: 0x04000438 RID: 1080
		private Stack parserInputStack;

		// Token: 0x04000439 RID: 1081
		private char[] nameBuffer;

		// Token: 0x0400043A RID: 1082
		private int nameLength;

		// Token: 0x0400043B RID: 1083
		private int nameCapacity;

		// Token: 0x0400043C RID: 1084
		private StringBuilder valueBuffer;

		// Token: 0x0400043D RID: 1085
		private int currentLinkedNodeLineNumber;

		// Token: 0x0400043E RID: 1086
		private int currentLinkedNodeLinePosition;

		// Token: 0x0400043F RID: 1087
		private int dtdIncludeSect;

		// Token: 0x04000440 RID: 1088
		private bool normalization;

		// Token: 0x04000441 RID: 1089
		private bool processingInternalSubset;

		// Token: 0x04000442 RID: 1090
		private string cachedPublicId;

		// Token: 0x04000443 RID: 1091
		private string cachedSystemId;

		// Token: 0x04000444 RID: 1092
		private DTDObjectModel DTD;
	}
}
