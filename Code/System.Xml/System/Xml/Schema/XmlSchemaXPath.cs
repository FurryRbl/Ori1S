using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Mono.Xml.Schema;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) selector element.</summary>
	// Token: 0x0200024B RID: 587
	public class XmlSchemaXPath : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the attribute for the XPath expression.</summary>
		/// <returns>The string attribute value for the XPath expression.</returns>
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x000778EC File Offset: 0x00075AEC
		// (set) Token: 0x060017BC RID: 6076 RVA: 0x000778F4 File Offset: 0x00075AF4
		[XmlAttribute("xpath")]
		[DefaultValue("")]
		public string XPath
		{
			get
			{
				return this.xpath;
			}
			set
			{
				this.xpath = value;
			}
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00077900 File Offset: 0x00075B00
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			if (this.nsmgr == null)
			{
				this.nsmgr = new XmlNamespaceManager(new NameTable());
				if (base.Namespaces != null)
				{
					foreach (XmlQualifiedName xmlQualifiedName in base.Namespaces.ToArray())
					{
						this.nsmgr.AddNamespace(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
					}
				}
			}
			this.currentPath = new XsdIdentityPath();
			this.ParseExpression(this.xpath, h, schema);
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x000779C4 File Offset: 0x00075BC4
		internal XsdIdentityPath[] CompiledExpression
		{
			get
			{
				return this.compiledExpression;
			}
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x000779CC File Offset: 0x00075BCC
		private void ParseExpression(string xpath, ValidationEventHandler h, XmlSchema schema)
		{
			ArrayList arrayList = new ArrayList();
			this.ParsePath(xpath, 0, arrayList, h, schema);
			this.compiledExpression = (XsdIdentityPath[])arrayList.ToArray(typeof(XsdIdentityPath));
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00077A08 File Offset: 0x00075C08
		private void ParsePath(string xpath, int pos, ArrayList paths, ValidationEventHandler h, XmlSchema schema)
		{
			pos = this.SkipWhitespace(xpath, pos);
			if (xpath.Length >= pos + 3 && xpath[pos] == '.')
			{
				int num = pos;
				pos++;
				pos = this.SkipWhitespace(xpath, pos);
				if (xpath.Length > pos + 2 && xpath.IndexOf("//", pos, 2) == pos)
				{
					this.currentPath.Descendants = true;
					pos += 2;
				}
				else
				{
					pos = num;
				}
			}
			ArrayList steps = new ArrayList();
			this.ParseStep(xpath, pos, steps, paths, h, schema);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00077A9C File Offset: 0x00075C9C
		private void ParseStep(string xpath, int pos, ArrayList steps, ArrayList paths, ValidationEventHandler h, XmlSchema schema)
		{
			pos = this.SkipWhitespace(xpath, pos);
			if (xpath.Length == pos)
			{
				base.error(h, "Empty xpath expression is specified");
				return;
			}
			XsdIdentityStep xsdIdentityStep = new XsdIdentityStep();
			char c = xpath[pos];
			switch (c)
			{
			case 'a':
				if (xpath.Length > pos + 9 && xpath.IndexOf("attribute", pos, 9) == pos)
				{
					int num = pos;
					pos += 9;
					pos = this.SkipWhitespace(xpath, pos);
					if (xpath.Length > pos && xpath[pos] == ':' && xpath[pos + 1] == ':')
					{
						if (this.isSelector)
						{
							base.error(h, "Selector cannot include attribute axes.");
							this.currentPath = null;
							return;
						}
						pos += 2;
						xsdIdentityStep.IsAttribute = true;
						if (xpath.Length > pos && xpath[pos] == '*')
						{
							pos++;
							xsdIdentityStep.IsAnyName = true;
							goto IL_3D5;
						}
						pos = this.SkipWhitespace(xpath, pos);
					}
					else
					{
						pos = num;
					}
				}
				break;
			default:
				if (c == '*')
				{
					pos++;
					xsdIdentityStep.IsAnyName = true;
					goto IL_3D5;
				}
				if (c == '.')
				{
					pos++;
					xsdIdentityStep.IsCurrent = true;
					goto IL_3D5;
				}
				if (c == '@')
				{
					if (this.isSelector)
					{
						base.error(h, "Selector cannot include attribute axes.");
						this.currentPath = null;
						return;
					}
					pos++;
					xsdIdentityStep.IsAttribute = true;
					pos = this.SkipWhitespace(xpath, pos);
					if (xpath.Length > pos && xpath[pos] == '*')
					{
						pos++;
						xsdIdentityStep.IsAnyName = true;
						goto IL_3D5;
					}
				}
				break;
			case 'c':
				if (xpath.Length > pos + 5 && xpath.IndexOf("child", pos, 5) == pos)
				{
					int num2 = pos;
					pos += 5;
					pos = this.SkipWhitespace(xpath, pos);
					if (xpath.Length > pos && xpath[pos] == ':' && xpath[pos + 1] == ':')
					{
						pos += 2;
						if (xpath.Length > pos && xpath[pos] == '*')
						{
							pos++;
							xsdIdentityStep.IsAnyName = true;
							goto IL_3D5;
						}
						pos = this.SkipWhitespace(xpath, pos);
					}
					else
					{
						pos = num2;
					}
				}
				break;
			}
			int num3 = pos;
			while (xpath.Length > pos)
			{
				if (!XmlChar.IsNCNameChar((int)xpath[pos]))
				{
					break;
				}
				pos++;
			}
			if (pos == num3)
			{
				base.error(h, "Invalid path format for a field.");
				this.currentPath = null;
				return;
			}
			if (xpath.Length == pos || xpath[pos] != ':')
			{
				xsdIdentityStep.Name = xpath.Substring(num3, pos - num3);
			}
			else
			{
				string text = xpath.Substring(num3, pos - num3);
				pos++;
				if (xpath.Length > pos && xpath[pos] == '*')
				{
					string text2 = this.nsmgr.LookupNamespace(text, false);
					if (text2 == null)
					{
						base.error(h, "Specified prefix '" + text + "' is not declared.");
						this.currentPath = null;
						return;
					}
					xsdIdentityStep.NsName = text2;
					pos++;
				}
				else
				{
					int num4 = pos;
					while (xpath.Length > pos)
					{
						if (!XmlChar.IsNCNameChar((int)xpath[pos]))
						{
							break;
						}
						pos++;
					}
					xsdIdentityStep.Name = xpath.Substring(num4, pos - num4);
					string text3 = this.nsmgr.LookupNamespace(text, false);
					if (text3 == null)
					{
						base.error(h, "Specified prefix '" + text + "' is not declared.");
						this.currentPath = null;
						return;
					}
					xsdIdentityStep.Namespace = text3;
				}
			}
			IL_3D5:
			if (!xsdIdentityStep.IsCurrent)
			{
				steps.Add(xsdIdentityStep);
			}
			pos = this.SkipWhitespace(xpath, pos);
			if (xpath.Length == pos)
			{
				this.currentPath.OrderedSteps = (XsdIdentityStep[])steps.ToArray(typeof(XsdIdentityStep));
				paths.Add(this.currentPath);
				return;
			}
			if (xpath[pos] == '/')
			{
				pos++;
				if (xsdIdentityStep.IsAttribute)
				{
					base.error(h, "Unexpected xpath token after Attribute NameTest.");
					this.currentPath = null;
					return;
				}
				this.ParseStep(xpath, pos, steps, paths, h, schema);
				if (this.currentPath == null)
				{
					return;
				}
				this.currentPath.OrderedSteps = (XsdIdentityStep[])steps.ToArray(typeof(XsdIdentityStep));
			}
			else
			{
				if (xpath[pos] != '|')
				{
					base.error(h, "Unexpected xpath token after NameTest.");
					this.currentPath = null;
					return;
				}
				pos++;
				this.currentPath.OrderedSteps = (XsdIdentityStep[])steps.ToArray(typeof(XsdIdentityStep));
				paths.Add(this.currentPath);
				this.currentPath = new XsdIdentityPath();
				this.ParsePath(xpath, pos, paths, h, schema);
			}
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00077FC0 File Offset: 0x000761C0
		private int SkipWhitespace(string xpath, int pos)
		{
			bool flag = true;
			while (flag && xpath.Length > pos)
			{
				char c = xpath[pos];
				switch (c)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				default:
					if (c != ' ')
					{
						flag = false;
						continue;
					}
					break;
				}
				pos++;
			}
			return pos;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0007802C File Offset: 0x0007622C
		internal static XmlSchemaXPath Read(XmlSchemaReader reader, ValidationEventHandler h, string name)
		{
			XmlSchemaXPath xmlSchemaXPath = new XmlSchemaXPath();
			reader.MoveToElement();
			if (reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema" || reader.LocalName != name)
			{
				XmlSchemaObject.error(h, "Should not happen :1: XmlSchemaComplexContentRestriction.Read, name=" + reader.Name, null);
				reader.Skip();
				return null;
			}
			xmlSchemaXPath.LineNumber = reader.LineNumber;
			xmlSchemaXPath.LinePosition = reader.LinePosition;
			xmlSchemaXPath.SourceUri = reader.BaseURI;
			XmlNamespaceManager namespaceManager = XmlSchemaUtil.GetParserContext(reader.Reader).NamespaceManager;
			if (namespaceManager != null)
			{
				xmlSchemaXPath.nsmgr = new XmlNamespaceManager(reader.NameTable);
				foreach (object obj in namespaceManager)
				{
					string text = obj as string;
					string text2 = text;
					if (text2 != null)
					{
						if (XmlSchemaXPath.<>f__switch$map4A == null)
						{
							XmlSchemaXPath.<>f__switch$map4A = new Dictionary<string, int>(2)
							{
								{
									"xml",
									0
								},
								{
									"xmlns",
									0
								}
							};
						}
						int num;
						if (XmlSchemaXPath.<>f__switch$map4A.TryGetValue(text2, out num))
						{
							if (num == 0)
							{
								continue;
							}
						}
					}
					xmlSchemaXPath.nsmgr.AddNamespace(text, namespaceManager.LookupNamespace(text, false));
				}
			}
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "id")
				{
					xmlSchemaXPath.Id = reader.Value;
				}
				else if (reader.Name == "xpath")
				{
					xmlSchemaXPath.xpath = reader.Value;
				}
				else if ((reader.NamespaceURI == string.Empty && reader.Name != "xmlns") || reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					XmlSchemaObject.error(h, reader.Name + " is not a valid attribute for " + name, null);
				}
				else
				{
					XmlSchemaUtil.ReadUnhandledAttribute(reader, xmlSchemaXPath);
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				return xmlSchemaXPath;
			}
			int num2 = 1;
			while (reader.ReadNextElement())
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName != name)
					{
						XmlSchemaObject.error(h, "Should not happen :2: XmlSchemaXPath.Read, name=" + reader.Name, null);
					}
					break;
				}
				if (num2 <= 1 && reader.LocalName == "annotation")
				{
					num2 = 2;
					XmlSchemaAnnotation xmlSchemaAnnotation = XmlSchemaAnnotation.Read(reader, h);
					if (xmlSchemaAnnotation != null)
					{
						xmlSchemaXPath.Annotation = xmlSchemaAnnotation;
					}
				}
				else
				{
					reader.RaiseInvalidElementError();
				}
			}
			return xmlSchemaXPath;
		}

		// Token: 0x040009A9 RID: 2473
		private string xpath;

		// Token: 0x040009AA RID: 2474
		private XmlNamespaceManager nsmgr;

		// Token: 0x040009AB RID: 2475
		internal bool isSelector;

		// Token: 0x040009AC RID: 2476
		private XsdIdentityPath[] compiledExpression;

		// Token: 0x040009AD RID: 2477
		private XsdIdentityPath currentPath;
	}
}
