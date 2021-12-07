using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000220 RID: 544
	internal class XsdInference
	{
		// Token: 0x060015A0 RID: 5536 RVA: 0x000623F0 File Offset: 0x000605F0
		private XsdInference(XmlReader xmlReader, XmlSchemaSet schemas, bool laxOccurrence, bool laxTypeInference)
		{
			this.source = xmlReader;
			this.schemas = schemas;
			this.laxOccurrence = laxOccurrence;
			this.laxTypeInference = laxTypeInference;
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00062590 File Offset: 0x00060790
		public static XmlSchemaSet Process(XmlReader xmlReader, XmlSchemaSet schemas, bool laxOccurrence, bool laxTypeInference)
		{
			XsdInference xsdInference = new XsdInference(xmlReader, schemas, laxOccurrence, laxTypeInference);
			xsdInference.Run();
			return xsdInference.schemas;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x000625B4 File Offset: 0x000607B4
		private void Run()
		{
			this.schemas.Compile();
			this.source.MoveToContent();
			if (this.source.NodeType != XmlNodeType.Element)
			{
				throw new ArgumentException("Argument XmlReader content is expected to be an element.");
			}
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.source.LocalName, this.source.NamespaceURI);
			XmlSchemaElement xmlSchemaElement = this.GetGlobalElement(xmlQualifiedName);
			if (xmlSchemaElement == null)
			{
				xmlSchemaElement = this.CreateGlobalElement(xmlQualifiedName);
				this.InferElement(xmlSchemaElement, xmlQualifiedName.Namespace, true);
			}
			else
			{
				this.InferElement(xmlSchemaElement, xmlQualifiedName.Namespace, false);
			}
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00062648 File Offset: 0x00060848
		private void AddImport(string current, string import)
		{
			foreach (object obj in this.schemas.Schemas(current))
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				bool flag = false;
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Includes)
				{
					XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
					XmlSchemaImport xmlSchemaImport = xmlSchemaExternal as XmlSchemaImport;
					if (xmlSchemaImport != null && xmlSchemaImport.Namespace == import)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					XmlSchemaImport xmlSchemaImport2 = new XmlSchemaImport();
					xmlSchemaImport2.Namespace = import;
					xmlSchema.Includes.Add(xmlSchemaImport2);
				}
			}
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00062764 File Offset: 0x00060964
		private void IncludeXmlAttributes()
		{
			if (this.schemas.Schemas("http://www.w3.org/XML/1998/namespace").Count == 0)
			{
				this.schemas.Add("http://www.w3.org/XML/1998/namespace", "http://www.w3.org/2001/xml.xsd");
			}
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x000627A4 File Offset: 0x000609A4
		private void InferElement(XmlSchemaElement el, string ns, bool isNew)
		{
			if (el.RefName != XmlQualifiedName.Empty)
			{
				XmlSchemaElement xmlSchemaElement = this.GetGlobalElement(el.RefName);
				if (xmlSchemaElement == null)
				{
					xmlSchemaElement = this.CreateElement(el.RefName);
					this.InferElement(xmlSchemaElement, ns, true);
				}
				else
				{
					this.InferElement(xmlSchemaElement, ns, isNew);
				}
				return;
			}
			if (this.source.MoveToFirstAttribute())
			{
				this.InferAttributes(el, ns, isNew);
				this.source.MoveToElement();
			}
			if (this.source.IsEmptyElement)
			{
				this.InferAsEmptyElement(el, ns, isNew);
				this.source.Read();
				this.source.MoveToContent();
			}
			else
			{
				this.InferContent(el, ns, isNew);
				this.source.ReadEndElement();
			}
			if (el.SchemaType == null && el.SchemaTypeName == XmlQualifiedName.Empty)
			{
				el.SchemaTypeName = XsdInference.QNameString;
			}
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00062898 File Offset: 0x00060A98
		private Hashtable CollectAttrTable(XmlSchemaObjectCollection attList)
		{
			Hashtable hashtable = new Hashtable();
			foreach (XmlSchemaObject xmlSchemaObject in attList)
			{
				XmlSchemaAttribute xmlSchemaAttribute = xmlSchemaObject as XmlSchemaAttribute;
				if (xmlSchemaAttribute == null)
				{
					throw this.Error(xmlSchemaObject, string.Format("Attribute inference only supports direct attribute definition. {0} is not supported.", xmlSchemaObject.GetType()));
				}
				if (xmlSchemaAttribute.RefName != XmlQualifiedName.Empty)
				{
					hashtable.Add(xmlSchemaAttribute.RefName, xmlSchemaAttribute);
				}
				else
				{
					hashtable.Add(new XmlQualifiedName(xmlSchemaAttribute.Name, string.Empty), xmlSchemaAttribute);
				}
			}
			return hashtable;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x00062964 File Offset: 0x00060B64
		private void InferAttributes(XmlSchemaElement el, string ns, bool isNew)
		{
			XmlSchemaComplexType xmlSchemaComplexType = null;
			XmlSchemaObjectCollection xmlSchemaObjectCollection = null;
			Hashtable hashtable = null;
			for (;;)
			{
				string namespaceURI = this.source.NamespaceURI;
				if (namespaceURI == null)
				{
					goto IL_D5;
				}
				if (XsdInference.<>f__switch$map44 == null)
				{
					XsdInference.<>f__switch$map44 = new Dictionary<string, int>(3)
					{
						{
							"http://www.w3.org/XML/1998/namespace",
							0
						},
						{
							"http://www.w3.org/2001/XMLSchema-instance",
							1
						},
						{
							"http://www.w3.org/2000/xmlns/",
							2
						}
					};
				}
				int num;
				if (!XsdInference.<>f__switch$map44.TryGetValue(namespaceURI, out num))
				{
					goto IL_D5;
				}
				switch (num)
				{
				case 0:
					if (this.schemas.Schemas("http://www.w3.org/XML/1998/namespace").Count == 0)
					{
						this.IncludeXmlAttributes();
					}
					goto IL_D5;
				case 1:
					if (this.source.LocalName == "nil")
					{
						el.IsNillable = true;
					}
					break;
				case 2:
					break;
				default:
					goto IL_D5;
				}
				IL_175:
				if (!this.source.MoveToNextAttribute())
				{
					break;
				}
				continue;
				IL_D5:
				if (xmlSchemaComplexType == null)
				{
					xmlSchemaComplexType = this.ToComplexType(el);
					xmlSchemaObjectCollection = this.GetAttributes(xmlSchemaComplexType);
					hashtable = this.CollectAttrTable(xmlSchemaObjectCollection);
				}
				XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.source.LocalName, this.source.NamespaceURI);
				XmlSchemaAttribute xmlSchemaAttribute = hashtable[xmlQualifiedName] as XmlSchemaAttribute;
				if (xmlSchemaAttribute == null)
				{
					xmlSchemaObjectCollection.Add(this.InferNewAttribute(xmlQualifiedName, isNew, ns));
					goto IL_175;
				}
				hashtable.Remove(xmlQualifiedName);
				if (xmlSchemaAttribute.RefName != null && xmlSchemaAttribute.RefName != XmlQualifiedName.Empty)
				{
					goto IL_175;
				}
				this.InferMergedAttribute(xmlSchemaAttribute);
				goto IL_175;
			}
			if (hashtable != null)
			{
				foreach (object obj in hashtable.Values)
				{
					XmlSchemaAttribute xmlSchemaAttribute2 = (XmlSchemaAttribute)obj;
					xmlSchemaAttribute2.Use = XmlSchemaUse.Optional;
				}
			}
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00062B68 File Offset: 0x00060D68
		private XmlSchemaAttribute InferNewAttribute(XmlQualifiedName attrName, bool isNewTypeDefinition, string ns)
		{
			bool flag = false;
			XmlSchemaAttribute xmlSchemaAttribute;
			if (attrName.Namespace.Length > 0)
			{
				xmlSchemaAttribute = this.GetGlobalAttribute(attrName);
				if (xmlSchemaAttribute == null)
				{
					xmlSchemaAttribute = this.CreateGlobalAttribute(attrName);
					xmlSchemaAttribute.SchemaTypeName = this.InferSimpleType(this.source.Value);
				}
				else
				{
					this.InferMergedAttribute(xmlSchemaAttribute);
					flag = (xmlSchemaAttribute.Use == XmlSchemaUse.Required);
				}
				xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.RefName = attrName;
				this.AddImport(ns, attrName.Namespace);
			}
			else
			{
				xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = attrName.Name;
				xmlSchemaAttribute.SchemaTypeName = this.InferSimpleType(this.source.Value);
			}
			if (!this.laxOccurrence && (isNewTypeDefinition || flag))
			{
				xmlSchemaAttribute.Use = XmlSchemaUse.Required;
			}
			else
			{
				xmlSchemaAttribute.Use = XmlSchemaUse.Optional;
			}
			return xmlSchemaAttribute;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00062C40 File Offset: 0x00060E40
		private void InferMergedAttribute(XmlSchemaAttribute attr)
		{
			attr.SchemaTypeName = this.InferMergedType(this.source.Value, attr.SchemaTypeName);
			attr.SchemaType = null;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00062C74 File Offset: 0x00060E74
		private XmlQualifiedName InferMergedType(string value, XmlQualifiedName typeName)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = XmlSchemaType.GetBuiltInSimpleType(typeName);
			if (xmlSchemaSimpleType == null)
			{
				return XsdInference.QNameString;
			}
			do
			{
				try
				{
					xmlSchemaSimpleType.Datatype.ParseValue(value, this.source.NameTable, this.source as IXmlNamespaceResolver);
					return typeName;
				}
				catch
				{
					xmlSchemaSimpleType = (xmlSchemaSimpleType.BaseXmlSchemaType as XmlSchemaSimpleType);
					typeName = ((xmlSchemaSimpleType == null) ? XmlQualifiedName.Empty : xmlSchemaSimpleType.QualifiedName);
				}
			}
			while (typeName != XmlQualifiedName.Empty);
			return XsdInference.QNameString;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00062D20 File Offset: 0x00060F20
		private XmlSchemaObjectCollection GetAttributes(XmlSchemaComplexType ct)
		{
			if (ct.ContentModel == null)
			{
				return ct.Attributes;
			}
			XmlSchemaSimpleContent xmlSchemaSimpleContent = ct.ContentModel as XmlSchemaSimpleContent;
			if (xmlSchemaSimpleContent != null)
			{
				XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = xmlSchemaSimpleContent.Content as XmlSchemaSimpleContentExtension;
				if (xmlSchemaSimpleContentExtension != null)
				{
					return xmlSchemaSimpleContentExtension.Attributes;
				}
				XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = xmlSchemaSimpleContent.Content as XmlSchemaSimpleContentRestriction;
				if (xmlSchemaSimpleContentRestriction != null)
				{
					return xmlSchemaSimpleContentRestriction.Attributes;
				}
				throw this.Error(xmlSchemaSimpleContent, "Invalid simple content model.");
			}
			else
			{
				XmlSchemaComplexContent xmlSchemaComplexContent = ct.ContentModel as XmlSchemaComplexContent;
				if (xmlSchemaComplexContent == null)
				{
					throw this.Error(xmlSchemaComplexContent, "Invalid complexType. Should not happen.");
				}
				XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = xmlSchemaComplexContent.Content as XmlSchemaComplexContentExtension;
				if (xmlSchemaComplexContentExtension != null)
				{
					return xmlSchemaComplexContentExtension.Attributes;
				}
				XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = xmlSchemaComplexContent.Content as XmlSchemaComplexContentRestriction;
				if (xmlSchemaComplexContentRestriction != null)
				{
					return xmlSchemaComplexContentRestriction.Attributes;
				}
				throw this.Error(xmlSchemaComplexContent, "Invalid simple content model.");
			}
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x00062DF4 File Offset: 0x00060FF4
		private XmlSchemaComplexType ToComplexType(XmlSchemaElement el)
		{
			XmlQualifiedName schemaTypeName = el.SchemaTypeName;
			XmlSchemaType schemaType = el.SchemaType;
			XmlSchemaComplexType xmlSchemaComplexType = schemaType as XmlSchemaComplexType;
			if (xmlSchemaComplexType != null)
			{
				return xmlSchemaComplexType;
			}
			XmlSchemaType xmlSchemaType = this.schemas.GlobalTypes[schemaTypeName] as XmlSchemaType;
			xmlSchemaComplexType = (xmlSchemaType as XmlSchemaComplexType);
			if (xmlSchemaComplexType != null)
			{
				return xmlSchemaComplexType;
			}
			xmlSchemaComplexType = new XmlSchemaComplexType();
			el.SchemaType = xmlSchemaComplexType;
			el.SchemaTypeName = XmlQualifiedName.Empty;
			if (schemaTypeName == XsdInference.QNameAnyType)
			{
				return xmlSchemaComplexType;
			}
			if (schemaType == null && schemaTypeName == XmlQualifiedName.Empty)
			{
				return xmlSchemaComplexType;
			}
			XmlSchemaSimpleContent xmlSchemaSimpleContent = new XmlSchemaSimpleContent();
			xmlSchemaComplexType.ContentModel = xmlSchemaSimpleContent;
			XmlSchemaSimpleType xmlSchemaSimpleType = schemaType as XmlSchemaSimpleType;
			if (xmlSchemaSimpleType != null)
			{
				xmlSchemaSimpleContent.Content = new XmlSchemaSimpleContentRestriction
				{
					BaseType = xmlSchemaSimpleType
				};
				return xmlSchemaComplexType;
			}
			XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = new XmlSchemaSimpleContentExtension();
			xmlSchemaSimpleContent.Content = xmlSchemaSimpleContentExtension;
			xmlSchemaSimpleType = XmlSchemaType.GetBuiltInSimpleType(schemaTypeName);
			if (xmlSchemaSimpleType != null)
			{
				xmlSchemaSimpleContentExtension.BaseTypeName = schemaTypeName;
				return xmlSchemaComplexType;
			}
			xmlSchemaSimpleType = (xmlSchemaType as XmlSchemaSimpleType);
			if (xmlSchemaSimpleType != null)
			{
				xmlSchemaSimpleContentExtension.BaseTypeName = schemaTypeName;
				return xmlSchemaComplexType;
			}
			throw this.Error(el, "Unexpected schema component that contains simpleTypeName that could not be resolved.");
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00062F10 File Offset: 0x00061110
		private void InferAsEmptyElement(XmlSchemaElement el, string ns, bool isNew)
		{
			XmlSchemaComplexType xmlSchemaComplexType = el.SchemaType as XmlSchemaComplexType;
			if (xmlSchemaComplexType == null)
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = el.SchemaType as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType != null)
				{
					xmlSchemaSimpleType = this.MakeBaseTypeAsEmptiable(xmlSchemaSimpleType);
					string @namespace = xmlSchemaSimpleType.QualifiedName.Namespace;
					if (@namespace != null)
					{
						if (XsdInference.<>f__switch$map45 == null)
						{
							XsdInference.<>f__switch$map45 = new Dictionary<string, int>(2)
							{
								{
									"http://www.w3.org/2001/XMLSchema",
									0
								},
								{
									"http://www.w3.org/2003/11/xpath-datatypes",
									0
								}
							};
						}
						int num;
						if (XsdInference.<>f__switch$map45.TryGetValue(@namespace, out num))
						{
							if (num == 0)
							{
								el.SchemaTypeName = xmlSchemaSimpleType.QualifiedName;
								return;
							}
						}
					}
					el.SchemaType = xmlSchemaSimpleType;
				}
				return;
			}
			XmlSchemaSimpleContent xmlSchemaSimpleContent = xmlSchemaComplexType.ContentModel as XmlSchemaSimpleContent;
			if (xmlSchemaSimpleContent != null)
			{
				this.ToEmptiableSimpleContent(xmlSchemaSimpleContent, isNew);
				return;
			}
			XmlSchemaComplexContent xmlSchemaComplexContent = xmlSchemaComplexType.ContentModel as XmlSchemaComplexContent;
			if (xmlSchemaComplexContent != null)
			{
				this.ToEmptiableComplexContent(xmlSchemaComplexContent, isNew);
				return;
			}
			if (xmlSchemaComplexType.Particle != null)
			{
				xmlSchemaComplexType.Particle.MinOccurs = 0m;
			}
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00063020 File Offset: 0x00061220
		private XmlSchemaSimpleType MakeBaseTypeAsEmptiable(XmlSchemaSimpleType st)
		{
			string @namespace = st.QualifiedName.Namespace;
			if (@namespace != null)
			{
				if (XsdInference.<>f__switch$map46 == null)
				{
					XsdInference.<>f__switch$map46 = new Dictionary<string, int>(2)
					{
						{
							"http://www.w3.org/2001/XMLSchema",
							0
						},
						{
							"http://www.w3.org/2003/11/xpath-datatypes",
							0
						}
					};
				}
				int num;
				if (XsdInference.<>f__switch$map46.TryGetValue(@namespace, out num))
				{
					if (num == 0)
					{
						return XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String);
					}
				}
			}
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = st.Content as XmlSchemaSimpleTypeRestriction;
			if (xmlSchemaSimpleTypeRestriction != null)
			{
				ArrayList arrayList = null;
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaSimpleTypeRestriction.Facets)
				{
					XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)xmlSchemaObject;
					if (xmlSchemaFacet is XmlSchemaLengthFacet || xmlSchemaFacet is XmlSchemaMinLengthFacet)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(xmlSchemaFacet);
					}
				}
				foreach (object obj in arrayList)
				{
					XmlSchemaFacet item = (XmlSchemaFacet)obj;
					xmlSchemaSimpleTypeRestriction.Facets.Remove(item);
				}
				if (xmlSchemaSimpleTypeRestriction.BaseType != null)
				{
					xmlSchemaSimpleTypeRestriction.BaseType = this.MakeBaseTypeAsEmptiable(st);
				}
				else
				{
					xmlSchemaSimpleTypeRestriction.BaseTypeName = XsdInference.QNameString;
				}
			}
			return st;
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x000631C8 File Offset: 0x000613C8
		private void ToEmptiableSimpleContent(XmlSchemaSimpleContent sm, bool isNew)
		{
			XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = sm.Content as XmlSchemaSimpleContentExtension;
			if (xmlSchemaSimpleContentExtension != null)
			{
				xmlSchemaSimpleContentExtension.BaseTypeName = XsdInference.QNameString;
			}
			else
			{
				XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = sm.Content as XmlSchemaSimpleContentRestriction;
				if (xmlSchemaSimpleContentRestriction == null)
				{
					throw this.Error(sm, "Invalid simple content model was passed.");
				}
				xmlSchemaSimpleContentRestriction.BaseTypeName = XsdInference.QNameString;
				xmlSchemaSimpleContentRestriction.BaseType = null;
			}
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00063228 File Offset: 0x00061428
		private void ToEmptiableComplexContent(XmlSchemaComplexContent cm, bool isNew)
		{
			XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = cm.Content as XmlSchemaComplexContentExtension;
			if (xmlSchemaComplexContentExtension != null)
			{
				if (xmlSchemaComplexContentExtension.Particle != null)
				{
					xmlSchemaComplexContentExtension.Particle.MinOccurs = 0m;
				}
				else if (xmlSchemaComplexContentExtension.BaseTypeName != null && xmlSchemaComplexContentExtension.BaseTypeName != XmlQualifiedName.Empty && xmlSchemaComplexContentExtension.BaseTypeName != XsdInference.QNameAnyType)
				{
					throw this.Error(xmlSchemaComplexContentExtension, "Complex type content extension has a reference to an external component that is not supported.");
				}
			}
			else
			{
				XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = cm.Content as XmlSchemaComplexContentRestriction;
				if (xmlSchemaComplexContentRestriction == null)
				{
					throw this.Error(cm, "Invalid complex content model was passed.");
				}
				if (xmlSchemaComplexContentRestriction.Particle != null)
				{
					xmlSchemaComplexContentRestriction.Particle.MinOccurs = 0m;
				}
				else if (xmlSchemaComplexContentRestriction.BaseTypeName != null && xmlSchemaComplexContentRestriction.BaseTypeName != XmlQualifiedName.Empty && xmlSchemaComplexContentRestriction.BaseTypeName != XsdInference.QNameAnyType)
				{
					throw this.Error(xmlSchemaComplexContentRestriction, "Complex type content extension has a reference to an external component that is not supported.");
				}
			}
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00063340 File Offset: 0x00061540
		private void InferContent(XmlSchemaElement el, string ns, bool isNew)
		{
			this.source.Read();
			this.source.MoveToContent();
			XmlNodeType nodeType = this.source.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				break;
			default:
				switch (nodeType)
				{
				case XmlNodeType.Whitespace:
					this.InferContent(el, ns, isNew);
					return;
				case XmlNodeType.SignificantWhitespace:
					goto IL_72;
				case XmlNodeType.EndElement:
					this.InferAsEmptyElement(el, ns, isNew);
					return;
				default:
					return;
				}
				break;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				goto IL_72;
			}
			IL_64:
			this.InferComplexContent(el, ns, isNew);
			return;
			IL_72:
			this.InferTextContent(el, isNew);
			this.source.MoveToContent();
			if (this.source.NodeType == XmlNodeType.Element)
			{
				goto IL_64;
			}
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000633FC File Offset: 0x000615FC
		private void InferComplexContent(XmlSchemaElement el, string ns, bool isNew)
		{
			XmlSchemaComplexType xmlSchemaComplexType = this.ToComplexType(el);
			this.ToComplexContentType(xmlSchemaComplexType);
			int num = 0;
			bool flag = false;
			for (;;)
			{
				XmlNodeType nodeType = this.source.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.None:
					goto IL_DD;
				case XmlNodeType.Element:
				{
					XmlSchemaSequence xmlSchemaSequence = this.PopulateSequence(xmlSchemaComplexType);
					XmlSchemaChoice xmlSchemaChoice = (xmlSchemaSequence.Items.Count <= 0) ? null : (xmlSchemaSequence.Items[0] as XmlSchemaChoice);
					if (xmlSchemaChoice != null)
					{
						this.ProcessLax(xmlSchemaChoice, ns);
					}
					else
					{
						this.ProcessSequence(xmlSchemaComplexType, xmlSchemaSequence, ns, ref num, ref flag, isNew);
					}
					this.source.MoveToContent();
					break;
				}
				default:
					if (nodeType == XmlNodeType.SignificantWhitespace)
					{
						goto IL_B8;
					}
					if (nodeType == XmlNodeType.EndElement)
					{
						return;
					}
					break;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					goto IL_B8;
				}
				continue;
				IL_B8:
				this.MarkAsMixed(xmlSchemaComplexType);
				this.source.ReadString();
				this.source.MoveToContent();
			}
			return;
			IL_DD:
			throw new NotImplementedException("Internal Error: Should not happen.");
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x000634F8 File Offset: 0x000616F8
		private void InferTextContent(XmlSchemaElement el, bool isNew)
		{
			string value = this.source.ReadString();
			if (el.SchemaType == null)
			{
				if (el.SchemaTypeName == XmlQualifiedName.Empty)
				{
					if (isNew)
					{
						el.SchemaTypeName = this.InferSimpleType(value);
					}
					else
					{
						el.SchemaTypeName = XsdInference.QNameString;
					}
					return;
				}
				string @namespace = el.SchemaTypeName.Namespace;
				if (@namespace != null)
				{
					if (XsdInference.<>f__switch$map47 == null)
					{
						XsdInference.<>f__switch$map47 = new Dictionary<string, int>(2)
						{
							{
								"http://www.w3.org/2001/XMLSchema",
								0
							},
							{
								"http://www.w3.org/2003/11/xpath-datatypes",
								0
							}
						};
					}
					int num;
					if (XsdInference.<>f__switch$map47.TryGetValue(@namespace, out num))
					{
						if (num == 0)
						{
							el.SchemaTypeName = this.InferMergedType(value, el.SchemaTypeName);
							return;
						}
					}
				}
				XmlSchemaComplexType xmlSchemaComplexType = this.schemas.GlobalTypes[el.SchemaTypeName] as XmlSchemaComplexType;
				if (xmlSchemaComplexType != null)
				{
					this.MarkAsMixed(xmlSchemaComplexType);
				}
				else
				{
					el.SchemaTypeName = XsdInference.QNameString;
				}
				return;
			}
			else
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = el.SchemaType as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType != null)
				{
					el.SchemaType = null;
					el.SchemaTypeName = XsdInference.QNameString;
					return;
				}
				XmlSchemaComplexType xmlSchemaComplexType2 = el.SchemaType as XmlSchemaComplexType;
				XmlSchemaSimpleContent xmlSchemaSimpleContent = xmlSchemaComplexType2.ContentModel as XmlSchemaSimpleContent;
				if (xmlSchemaSimpleContent == null)
				{
					this.MarkAsMixed(xmlSchemaComplexType2);
					return;
				}
				XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = xmlSchemaSimpleContent.Content as XmlSchemaSimpleContentExtension;
				if (xmlSchemaSimpleContentExtension != null)
				{
					xmlSchemaSimpleContentExtension.BaseTypeName = this.InferMergedType(value, xmlSchemaSimpleContentExtension.BaseTypeName);
				}
				XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = xmlSchemaSimpleContent.Content as XmlSchemaSimpleContentRestriction;
				if (xmlSchemaSimpleContentRestriction != null)
				{
					xmlSchemaSimpleContentRestriction.BaseTypeName = this.InferMergedType(value, xmlSchemaSimpleContentRestriction.BaseTypeName);
					xmlSchemaSimpleContentRestriction.BaseType = null;
				}
				return;
			}
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x000636BC File Offset: 0x000618BC
		private void MarkAsMixed(XmlSchemaComplexType ct)
		{
			XmlSchemaComplexContent xmlSchemaComplexContent = ct.ContentModel as XmlSchemaComplexContent;
			if (xmlSchemaComplexContent != null)
			{
				xmlSchemaComplexContent.IsMixed = true;
			}
			else
			{
				ct.IsMixed = true;
			}
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x000636F0 File Offset: 0x000618F0
		private void ProcessLax(XmlSchemaChoice c, string ns)
		{
			foreach (XmlSchemaObject xmlSchemaObject in c.Items)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaObject;
				XmlSchemaElement xmlSchemaElement = xmlSchemaParticle as XmlSchemaElement;
				if (xmlSchemaElement == null)
				{
					throw this.Error(c, string.Format("Target schema item contains unacceptable particle {0}. Only element is allowed here.", new object[0]));
				}
				if (this.ElementMatches(xmlSchemaElement, ns))
				{
					this.InferElement(xmlSchemaElement, ns, false);
					return;
				}
			}
			XmlSchemaElement xmlSchemaElement2 = new XmlSchemaElement();
			if (this.source.NamespaceURI == ns)
			{
				xmlSchemaElement2.Name = this.source.LocalName;
			}
			else
			{
				xmlSchemaElement2.RefName = new XmlQualifiedName(this.source.LocalName, this.source.NamespaceURI);
				this.AddImport(ns, this.source.NamespaceURI);
			}
			this.InferElement(xmlSchemaElement2, this.source.NamespaceURI, true);
			c.Items.Add(xmlSchemaElement2);
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00063820 File Offset: 0x00061A20
		private bool ElementMatches(XmlSchemaElement el, string ns)
		{
			bool result = false;
			if (el.RefName != XmlQualifiedName.Empty)
			{
				if (el.RefName.Name == this.source.LocalName && el.RefName.Namespace == this.source.NamespaceURI)
				{
					result = true;
				}
			}
			else if (el.Name == this.source.LocalName && ns == this.source.NamespaceURI)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x000638C0 File Offset: 0x00061AC0
		private void ProcessSequence(XmlSchemaComplexType ct, XmlSchemaSequence s, string ns, ref int position, ref bool consumed, bool isNew)
		{
			for (int i = 0; i < position; i++)
			{
				XmlSchemaElement el = s.Items[i] as XmlSchemaElement;
				if (this.ElementMatches(el, ns))
				{
					this.ProcessLax(this.ToSequenceOfChoice(s), ns);
					return;
				}
			}
			if (s.Items.Count <= position)
			{
				XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.source.LocalName, this.source.NamespaceURI);
				XmlSchemaElement xmlSchemaElement = this.CreateElement(xmlQualifiedName);
				if (this.laxOccurrence)
				{
					xmlSchemaElement.MinOccurs = 0m;
				}
				this.InferElement(xmlSchemaElement, ns, true);
				if (ns == xmlQualifiedName.Namespace)
				{
					s.Items.Add(xmlSchemaElement);
				}
				else
				{
					XmlSchemaElement xmlSchemaElement2 = new XmlSchemaElement();
					if (this.laxOccurrence)
					{
						xmlSchemaElement2.MinOccurs = 0m;
					}
					xmlSchemaElement2.RefName = xmlQualifiedName;
					this.AddImport(ns, xmlQualifiedName.Namespace);
					s.Items.Add(xmlSchemaElement2);
				}
				consumed = true;
				return;
			}
			XmlSchemaElement xmlSchemaElement3 = s.Items[position] as XmlSchemaElement;
			if (xmlSchemaElement3 == null)
			{
				throw this.Error(s, string.Format("Target complex type content sequence has an unacceptable type of particle {0}", s.Items[position]));
			}
			bool flag = this.ElementMatches(xmlSchemaElement3, ns);
			if (flag)
			{
				if (consumed)
				{
					xmlSchemaElement3.MaxOccursString = "unbounded";
				}
				this.InferElement(xmlSchemaElement3, this.source.NamespaceURI, false);
				this.source.MoveToContent();
				XmlNodeType nodeType = this.source.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.None:
					break;
				case XmlNodeType.Element:
					goto IL_1FA;
				default:
					switch (nodeType)
					{
					case XmlNodeType.Whitespace:
						this.source.ReadString();
						break;
					case XmlNodeType.SignificantWhitespace:
						goto IL_20E;
					case XmlNodeType.EndElement:
						return;
					default:
						this.source.Read();
						goto IL_249;
					}
					break;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					goto IL_20E;
				}
				IL_1C8:
				if (this.source.NodeType != XmlNodeType.Element)
				{
					if (this.source.NodeType == XmlNodeType.EndElement)
					{
						return;
					}
					goto IL_249;
				}
				IL_1FA:
				this.ProcessSequence(ct, s, ns, ref position, ref consumed, isNew);
				goto IL_249;
				IL_20E:
				this.MarkAsMixed(ct);
				this.source.ReadString();
				goto IL_1C8;
				IL_249:;
			}
			else if (consumed)
			{
				position++;
				consumed = false;
				this.ProcessSequence(ct, s, ns, ref position, ref consumed, isNew);
			}
			else
			{
				this.ProcessLax(this.ToSequenceOfChoice(s), ns);
			}
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00063B54 File Offset: 0x00061D54
		private XmlSchemaChoice ToSequenceOfChoice(XmlSchemaSequence s)
		{
			XmlSchemaChoice xmlSchemaChoice = new XmlSchemaChoice();
			if (this.laxOccurrence)
			{
				xmlSchemaChoice.MinOccurs = 0m;
			}
			xmlSchemaChoice.MaxOccursString = "unbounded";
			foreach (XmlSchemaObject xmlSchemaObject in s.Items)
			{
				XmlSchemaParticle item = (XmlSchemaParticle)xmlSchemaObject;
				xmlSchemaChoice.Items.Add(item);
			}
			s.Items.Clear();
			s.Items.Add(xmlSchemaChoice);
			return xmlSchemaChoice;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00063C0C File Offset: 0x00061E0C
		private void ToComplexContentType(XmlSchemaComplexType type)
		{
			if (!(type.ContentModel is XmlSchemaSimpleContent))
			{
				return;
			}
			XmlSchemaObjectCollection attributes = this.GetAttributes(type);
			foreach (XmlSchemaObject item in attributes)
			{
				type.Attributes.Add(item);
			}
			type.ContentModel = null;
			type.IsMixed = true;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00063CA4 File Offset: 0x00061EA4
		private XmlSchemaSequence PopulateSequence(XmlSchemaComplexType ct)
		{
			XmlSchemaParticle xmlSchemaParticle = this.PopulateParticle(ct);
			XmlSchemaSequence xmlSchemaSequence = xmlSchemaParticle as XmlSchemaSequence;
			if (xmlSchemaSequence != null)
			{
				return xmlSchemaSequence;
			}
			throw this.Error(ct, string.Format("Target complexType contains unacceptable type of particle {0}", xmlSchemaParticle));
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00063CDC File Offset: 0x00061EDC
		private XmlSchemaSequence CreateSequence()
		{
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			if (this.laxOccurrence)
			{
				xmlSchemaSequence.MinOccurs = 0m;
			}
			return xmlSchemaSequence;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00063D08 File Offset: 0x00061F08
		private XmlSchemaParticle PopulateParticle(XmlSchemaComplexType ct)
		{
			if (ct.ContentModel == null)
			{
				if (ct.Particle == null)
				{
					ct.Particle = this.CreateSequence();
				}
				return ct.Particle;
			}
			XmlSchemaComplexContent xmlSchemaComplexContent = ct.ContentModel as XmlSchemaComplexContent;
			if (xmlSchemaComplexContent != null)
			{
				XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = xmlSchemaComplexContent.Content as XmlSchemaComplexContentExtension;
				if (xmlSchemaComplexContentExtension != null)
				{
					if (xmlSchemaComplexContentExtension.Particle == null)
					{
						xmlSchemaComplexContentExtension.Particle = this.CreateSequence();
					}
					return xmlSchemaComplexContentExtension.Particle;
				}
				XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = xmlSchemaComplexContent.Content as XmlSchemaComplexContentRestriction;
				if (xmlSchemaComplexContentRestriction != null)
				{
					if (xmlSchemaComplexContentRestriction.Particle == null)
					{
						xmlSchemaComplexContentRestriction.Particle = this.CreateSequence();
					}
					return xmlSchemaComplexContentRestriction.Particle;
				}
			}
			throw this.Error(ct, "Schema inference internal error. The complexType should have been converted to have a complex content.");
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00063DBC File Offset: 0x00061FBC
		private XmlQualifiedName InferSimpleType(string value)
		{
			if (this.laxTypeInference)
			{
				return XsdInference.QNameString;
			}
			if (value != null)
			{
				if (XsdInference.<>f__switch$map48 == null)
				{
					XsdInference.<>f__switch$map48 = new Dictionary<string, int>(2)
					{
						{
							"true",
							0
						},
						{
							"false",
							0
						}
					};
				}
				int num;
				if (XsdInference.<>f__switch$map48.TryGetValue(value, out num))
				{
					if (num == 0)
					{
						return XsdInference.QNameBoolean;
					}
				}
			}
			try
			{
				long num2 = XmlConvert.ToInt64(value);
				if (0L <= num2 && num2 <= 255L)
				{
					return XsdInference.QNameUByte;
				}
				if (-128L <= num2 && num2 <= 127L)
				{
					return XsdInference.QNameByte;
				}
				if (0L <= num2 && num2 <= 65535L)
				{
					return XsdInference.QNameUShort;
				}
				if (-32768L <= num2 && num2 <= 32767L)
				{
					return XsdInference.QNameShort;
				}
				if (0L <= num2 && num2 <= (long)((ulong)-1))
				{
					return XsdInference.QNameUInt;
				}
				if (-2147483648L <= num2 && num2 <= 2147483647L)
				{
					return XsdInference.QNameInt;
				}
				return XsdInference.QNameLong;
			}
			catch (Exception)
			{
			}
			try
			{
				XmlConvert.ToUInt64(value);
				return XsdInference.QNameULong;
			}
			catch (Exception)
			{
			}
			try
			{
				XmlConvert.ToDecimal(value);
				return XsdInference.QNameDecimal;
			}
			catch (Exception)
			{
			}
			try
			{
				double num3 = XmlConvert.ToDouble(value);
				if (-3.4028234663852886E+38 <= num3 && num3 <= 3.4028234663852886E+38)
				{
					return XsdInference.QNameFloat;
				}
				return XsdInference.QNameDouble;
			}
			catch (Exception)
			{
			}
			try
			{
				XmlConvert.ToDateTime(value);
				return XsdInference.QNameDateTime;
			}
			catch (Exception)
			{
			}
			try
			{
				XmlConvert.ToTimeSpan(value);
				return XsdInference.QNameDuration;
			}
			catch (Exception)
			{
			}
			return XsdInference.QNameString;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00064074 File Offset: 0x00062274
		private XmlSchemaElement GetGlobalElement(XmlQualifiedName name)
		{
			XmlSchemaElement xmlSchemaElement = this.newElements[name] as XmlSchemaElement;
			if (xmlSchemaElement == null)
			{
				xmlSchemaElement = (this.schemas.GlobalElements[name] as XmlSchemaElement);
			}
			return xmlSchemaElement;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x000640B4 File Offset: 0x000622B4
		private XmlSchemaAttribute GetGlobalAttribute(XmlQualifiedName name)
		{
			XmlSchemaAttribute xmlSchemaAttribute = this.newElements[name] as XmlSchemaAttribute;
			if (xmlSchemaAttribute == null)
			{
				xmlSchemaAttribute = (this.schemas.GlobalAttributes[name] as XmlSchemaAttribute);
			}
			return xmlSchemaAttribute;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x000640F4 File Offset: 0x000622F4
		private XmlSchemaElement CreateElement(XmlQualifiedName name)
		{
			return new XmlSchemaElement
			{
				Name = name.Name
			};
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x00064114 File Offset: 0x00062314
		private XmlSchemaElement CreateGlobalElement(XmlQualifiedName name)
		{
			XmlSchemaElement xmlSchemaElement = this.CreateElement(name);
			XmlSchema xmlSchema = this.PopulateSchema(name.Namespace);
			xmlSchema.Items.Add(xmlSchemaElement);
			this.newElements.Add(name, xmlSchemaElement);
			return xmlSchemaElement;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00064154 File Offset: 0x00062354
		private XmlSchemaAttribute CreateGlobalAttribute(XmlQualifiedName name)
		{
			XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
			XmlSchema xmlSchema = this.PopulateSchema(name.Namespace);
			xmlSchemaAttribute.Name = name.Name;
			xmlSchema.Items.Add(xmlSchemaAttribute);
			this.newAttributes.Add(name, xmlSchemaAttribute);
			return xmlSchemaAttribute;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0006419C File Offset: 0x0006239C
		private XmlSchema PopulateSchema(string ns)
		{
			ICollection collection = this.schemas.Schemas(ns);
			if (collection.Count > 0)
			{
				IEnumerator enumerator = collection.GetEnumerator();
				enumerator.MoveNext();
				return (XmlSchema)enumerator.Current;
			}
			XmlSchema xmlSchema = new XmlSchema();
			if (ns != null && ns.Length > 0)
			{
				xmlSchema.TargetNamespace = ns;
			}
			xmlSchema.ElementFormDefault = XmlSchemaForm.Qualified;
			xmlSchema.AttributeFormDefault = XmlSchemaForm.Unqualified;
			this.schemas.Add(xmlSchema);
			return xmlSchema;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00064218 File Offset: 0x00062418
		private XmlSchemaInferenceException Error(XmlSchemaObject sourceObj, string message)
		{
			return this.Error(sourceObj, false, message);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00064224 File Offset: 0x00062424
		private XmlSchemaInferenceException Error(XmlSchemaObject sourceObj, bool useReader, string message)
		{
			string message2 = message + ((sourceObj == null) ? string.Empty : string.Format(". Related schema component is {0}", sourceObj.SourceUri, sourceObj.LineNumber, sourceObj.LinePosition)) + ((!useReader) ? string.Empty : string.Format(". {0}", this.source.BaseURI));
			IXmlLineInfo xmlLineInfo = this.source as IXmlLineInfo;
			if (useReader && xmlLineInfo != null)
			{
				return new XmlSchemaInferenceException(message2, null, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
			}
			return new XmlSchemaInferenceException(message2);
		}

		// Token: 0x040008A6 RID: 2214
		public const string NamespaceXml = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x040008A7 RID: 2215
		public const string NamespaceXmlns = "http://www.w3.org/2000/xmlns/";

		// Token: 0x040008A8 RID: 2216
		public const string XdtNamespace = "http://www.w3.org/2003/11/xpath-datatypes";

		// Token: 0x040008A9 RID: 2217
		private static readonly XmlQualifiedName QNameString = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008AA RID: 2218
		private static readonly XmlQualifiedName QNameBoolean = new XmlQualifiedName("boolean", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008AB RID: 2219
		private static readonly XmlQualifiedName QNameAnyType = new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008AC RID: 2220
		private static readonly XmlQualifiedName QNameByte = new XmlQualifiedName("byte", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008AD RID: 2221
		private static readonly XmlQualifiedName QNameUByte = new XmlQualifiedName("unsignedByte", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008AE RID: 2222
		private static readonly XmlQualifiedName QNameShort = new XmlQualifiedName("short", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008AF RID: 2223
		private static readonly XmlQualifiedName QNameUShort = new XmlQualifiedName("unsignedShort", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008B0 RID: 2224
		private static readonly XmlQualifiedName QNameInt = new XmlQualifiedName("int", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008B1 RID: 2225
		private static readonly XmlQualifiedName QNameUInt = new XmlQualifiedName("unsignedInt", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008B2 RID: 2226
		private static readonly XmlQualifiedName QNameLong = new XmlQualifiedName("long", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008B3 RID: 2227
		private static readonly XmlQualifiedName QNameULong = new XmlQualifiedName("unsignedLong", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008B4 RID: 2228
		private static readonly XmlQualifiedName QNameDecimal = new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008B5 RID: 2229
		private static readonly XmlQualifiedName QNameUDecimal = new XmlQualifiedName("unsignedDecimal", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008B6 RID: 2230
		private static readonly XmlQualifiedName QNameDouble = new XmlQualifiedName("double", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008B7 RID: 2231
		private static readonly XmlQualifiedName QNameFloat = new XmlQualifiedName("float", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008B8 RID: 2232
		private static readonly XmlQualifiedName QNameDateTime = new XmlQualifiedName("dateTime", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008B9 RID: 2233
		private static readonly XmlQualifiedName QNameDuration = new XmlQualifiedName("duration", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x040008BA RID: 2234
		private XmlReader source;

		// Token: 0x040008BB RID: 2235
		private XmlSchemaSet schemas;

		// Token: 0x040008BC RID: 2236
		private bool laxOccurrence;

		// Token: 0x040008BD RID: 2237
		private bool laxTypeInference;

		// Token: 0x040008BE RID: 2238
		private Hashtable newElements = new Hashtable();

		// Token: 0x040008BF RID: 2239
		private Hashtable newAttributes = new Hashtable();
	}
}
