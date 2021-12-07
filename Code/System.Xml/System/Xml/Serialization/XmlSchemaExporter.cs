using System;
using System.Collections;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Populates <see cref="T:System.Xml.Schema.XmlSchema" /> objects with XML schema element declarations that are found in type mapping objects. </summary>
	// Token: 0x0200029E RID: 670
	public class XmlSchemaExporter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemaExporter" /> class. </summary>
		/// <param name="schemas">A collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects to which element declarations obtained from type mappings are added.</param>
		// Token: 0x06001B24 RID: 6948 RVA: 0x0008D86C File Offset: 0x0008BA6C
		public XmlSchemaExporter(XmlSchemas schemas)
		{
			this.schemas = schemas;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0008D894 File Offset: 0x0008BA94
		internal XmlSchemaExporter(XmlSchemas schemas, bool encodedFormat)
		{
			this.encodedFormat = encodedFormat;
			this.schemas = schemas;
		}

		/// <summary>Exports an &lt;any&gt; element to the <see cref="T:System.Xml.Schema.XmlSchema" /> object that is identified by the specified namespace.</summary>
		/// <returns>An arbitrary name assigned to the &lt;any&gt; element declaration.</returns>
		/// <param name="ns">The namespace of the XML schema document to which to add an &lt;any&gt; element.</param>
		// Token: 0x06001B26 RID: 6950 RVA: 0x0008D8CC File Offset: 0x0008BACC
		[MonoTODO]
		public string ExportAnyType(string ns)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds an element declaration for an object or type to a SOAP message or to an <see cref="T:System.Xml.Schema.XmlSchema" /> object.</summary>
		/// <returns>The string "any" with an appended integer. </returns>
		/// <param name="members">An <see cref="T:System.Xml.Serialization.XmlMembersMapping" />  that contains mappings to export.</param>
		// Token: 0x06001B27 RID: 6951 RVA: 0x0008D8D4 File Offset: 0x0008BAD4
		[MonoNotSupported("")]
		public string ExportAnyType(XmlMembersMapping members)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds an element declaration to the applicable <see cref="T:System.Xml.Schema.XmlSchema" /> for each of the element parts of a literal SOAP message definition. </summary>
		/// <param name="xmlMembersMapping">The internal .NET Framework type mappings for the element parts of a Web Services Description Language (WSDL) message definition.</param>
		// Token: 0x06001B28 RID: 6952 RVA: 0x0008D8DC File Offset: 0x0008BADC
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping)
		{
			this.ExportMembersMapping(xmlMembersMapping, true);
		}

		/// <summary>Adds an element declaration to the applicable <see cref="T:System.Xml.Schema.XmlSchema" /> for each of the element parts of a literal SOAP message definition, and specifies whether enclosing elements are included.</summary>
		/// <param name="xmlMembersMapping">The internal mapping between a .NET Framework type and an XML schema element.</param>
		/// <param name="exportEnclosingType">true if the schema elements that enclose the schema are to be included; otherwise, false.</param>
		// Token: 0x06001B29 RID: 6953 RVA: 0x0008D8E8 File Offset: 0x0008BAE8
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping, bool exportEnclosingType)
		{
			ClassMap classMap = (ClassMap)xmlMembersMapping.ObjectMap;
			if (xmlMembersMapping.HasWrapperElement && exportEnclosingType)
			{
				XmlSchema schema = this.GetSchema(xmlMembersMapping.Namespace);
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence particle;
				XmlSchemaAnyAttribute anyAttribute;
				this.ExportMembersMapSchema(schema, classMap, null, xmlSchemaComplexType.Attributes, out particle, out anyAttribute);
				xmlSchemaComplexType.Particle = particle;
				xmlSchemaComplexType.AnyAttribute = anyAttribute;
				if (this.encodedFormat)
				{
					xmlSchemaComplexType.Name = xmlMembersMapping.ElementName;
					schema.Items.Add(xmlSchemaComplexType);
				}
				else
				{
					XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
					xmlSchemaElement.Name = xmlMembersMapping.ElementName;
					xmlSchemaElement.SchemaType = xmlSchemaComplexType;
					schema.Items.Add(xmlSchemaElement);
				}
			}
			else
			{
				ICollection elementMembers = classMap.ElementMembers;
				if (elementMembers != null)
				{
					foreach (object obj in elementMembers)
					{
						XmlTypeMapMemberElement xmlTypeMapMemberElement = (XmlTypeMapMemberElement)obj;
						if (xmlTypeMapMemberElement is XmlTypeMapMemberAnyElement && xmlTypeMapMemberElement.TypeData.IsListType)
						{
							XmlSchema schema2 = this.GetSchema(xmlMembersMapping.Namespace);
							XmlSchemaParticle schemaArrayElement = this.GetSchemaArrayElement(schema2, xmlTypeMapMemberElement.ElementInfo);
							if (schemaArrayElement is XmlSchemaAny)
							{
								XmlSchemaComplexType xmlSchemaComplexType2 = this.FindComplexType(schema2.Items, "any");
								if (xmlSchemaComplexType2 != null)
								{
									continue;
								}
								xmlSchemaComplexType2 = new XmlSchemaComplexType();
								xmlSchemaComplexType2.Name = "any";
								xmlSchemaComplexType2.IsMixed = true;
								XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
								xmlSchemaComplexType2.Particle = xmlSchemaSequence;
								xmlSchemaSequence.Items.Add(schemaArrayElement);
								schema2.Items.Add(xmlSchemaComplexType2);
								continue;
							}
						}
						XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)xmlTypeMapMemberElement.ElementInfo[0];
						XmlSchema schema3;
						if (this.encodedFormat)
						{
							schema3 = this.GetSchema(xmlMembersMapping.Namespace);
							this.ImportNamespace(schema3, "http://schemas.xmlsoap.org/soap/encoding/");
						}
						else
						{
							schema3 = this.GetSchema(xmlTypeMapElementInfo.Namespace);
						}
						XmlSchemaElement xmlSchemaElement2 = this.FindElement(schema3.Items, xmlTypeMapElementInfo.ElementName);
						XmlSchemaExporter.XmlSchemaObjectContainer container = null;
						if (!this.encodedFormat)
						{
							container = new XmlSchemaExporter.XmlSchemaObjectContainer(schema3);
						}
						Type type = xmlTypeMapMemberElement.GetType();
						if (xmlTypeMapMemberElement is XmlTypeMapMemberFlatList)
						{
							throw new InvalidOperationException("Unwrapped arrays not supported as parameters");
						}
						XmlSchemaElement xmlSchemaElement3;
						if (type == typeof(XmlTypeMapMemberElement))
						{
							xmlSchemaElement3 = (XmlSchemaElement)this.GetSchemaElement(schema3, xmlTypeMapElementInfo, xmlTypeMapMemberElement.DefaultValue, false, container);
						}
						else
						{
							xmlSchemaElement3 = (XmlSchemaElement)this.GetSchemaElement(schema3, xmlTypeMapElementInfo, false, container);
						}
						if (xmlSchemaElement2 != null)
						{
							if (!xmlSchemaElement2.SchemaTypeName.Equals(xmlSchemaElement3.SchemaTypeName))
							{
								string text = "The XML element named '" + xmlTypeMapElementInfo.ElementName + "' ";
								string text2 = text;
								text = string.Concat(new string[]
								{
									text2,
									"from namespace '",
									schema3.TargetNamespace,
									"' references distinct types ",
									xmlSchemaElement3.SchemaTypeName.Name,
									" and ",
									xmlSchemaElement2.SchemaTypeName.Name,
									". "
								});
								text += "Use XML attributes to specify another XML name or namespace for the element or types.";
								throw new InvalidOperationException(text);
							}
							schema3.Items.Remove(xmlSchemaElement3);
						}
					}
				}
			}
			this.CompileSchemas();
		}

		/// <summary>Adds an element declaration to the applicable <see cref="T:System.Xml.Schema.XmlSchema" /> object for a single element part of a literal SOAP message definition.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the qualified XML name of the exported element declaration.</returns>
		/// <param name="xmlMembersMapping">Internal .NET Framework type mappings for the element parts of a Web Services Description Language (WSDL) message definition.</param>
		// Token: 0x06001B2A RID: 6954 RVA: 0x0008DC68 File Offset: 0x0008BE68
		[MonoTODO]
		public XmlQualifiedName ExportTypeMapping(XmlMembersMapping xmlMembersMapping)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds an element declaration for a .NET Framework type to the applicable <see cref="T:System.Xml.Schema.XmlSchema" /> object. </summary>
		/// <param name="xmlTypeMapping">The internal mapping between a .NET Framework type and an XML schema element.</param>
		// Token: 0x06001B2B RID: 6955 RVA: 0x0008DC70 File Offset: 0x0008BE70
		public void ExportTypeMapping(XmlTypeMapping xmlTypeMapping)
		{
			if (!xmlTypeMapping.IncludeInSchema)
			{
				return;
			}
			if (this.IsElementExported(xmlTypeMapping))
			{
				return;
			}
			if (this.encodedFormat)
			{
				this.ExportClassSchema(xmlTypeMapping);
				XmlSchema schema = this.GetSchema(xmlTypeMapping.XmlTypeNamespace);
				this.ImportNamespace(schema, "http://schemas.xmlsoap.org/soap/encoding/");
			}
			else
			{
				XmlSchema schema2 = this.GetSchema(xmlTypeMapping.Namespace);
				XmlTypeMapElementInfo xmlTypeMapElementInfo = new XmlTypeMapElementInfo(null, xmlTypeMapping.TypeData);
				xmlTypeMapElementInfo.Namespace = xmlTypeMapping.Namespace;
				xmlTypeMapElementInfo.ElementName = xmlTypeMapping.ElementName;
				if (xmlTypeMapping.TypeData.IsComplexType)
				{
					xmlTypeMapElementInfo.MappedType = xmlTypeMapping;
				}
				xmlTypeMapElementInfo.IsNullable = xmlTypeMapping.IsNullable;
				this.GetSchemaElement(schema2, xmlTypeMapElementInfo, false, new XmlSchemaExporter.XmlSchemaObjectContainer(schema2));
				this.SetElementExported(xmlTypeMapping);
			}
			this.CompileSchemas();
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0008DD38 File Offset: 0x0008BF38
		private void ExportXmlSerializableSchema(XmlSchema currentSchema, XmlSerializableMapping map)
		{
			if (this.IsMapExported(map))
			{
				return;
			}
			this.SetMapExported(map);
			if (map.Schema == null)
			{
				return;
			}
			string targetNamespace = map.Schema.TargetNamespace;
			XmlSchema xmlSchema = this.schemas[targetNamespace];
			if (xmlSchema == null)
			{
				this.schemas.Add(map.Schema);
				this.ImportNamespace(currentSchema, targetNamespace);
			}
			else if (xmlSchema != map.Schema && !XmlSchemaExporter.CanBeDuplicated(xmlSchema, map.Schema))
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"The namespace '",
					targetNamespace,
					"' defined by the class '",
					map.TypeFullName,
					"' is a duplicate."
				}));
			}
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0008DDF8 File Offset: 0x0008BFF8
		private static bool CanBeDuplicated(XmlSchema existingSchema, XmlSchema schema)
		{
			return XmlSchemas.IsDataSet(existingSchema) && XmlSchemas.IsDataSet(schema) && existingSchema.Id == schema.Id;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0008DE34 File Offset: 0x0008C034
		private void ExportClassSchema(XmlTypeMapping map)
		{
			if (this.IsMapExported(map))
			{
				return;
			}
			this.SetMapExported(map);
			if (map.TypeData.Type == typeof(object))
			{
				foreach (object obj in map.DerivedTypes)
				{
					XmlTypeMapping xmlTypeMapping = (XmlTypeMapping)obj;
					if (xmlTypeMapping.TypeData.SchemaType == SchemaTypes.Class)
					{
						this.ExportClassSchema(xmlTypeMapping);
					}
				}
				return;
			}
			XmlSchema schema = this.GetSchema(map.XmlTypeNamespace);
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.Name = map.XmlType;
			schema.Items.Add(xmlSchemaComplexType);
			ClassMap classMap = (ClassMap)map.ObjectMap;
			if (classMap.HasSimpleContent)
			{
				XmlSchemaSimpleContent xmlSchemaSimpleContent = new XmlSchemaSimpleContent();
				xmlSchemaComplexType.ContentModel = xmlSchemaSimpleContent;
				XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = new XmlSchemaSimpleContentExtension();
				xmlSchemaSimpleContent.Content = xmlSchemaSimpleContentExtension;
				XmlSchemaSequence xmlSchemaSequence;
				XmlSchemaAnyAttribute anyAttribute;
				this.ExportMembersMapSchema(schema, classMap, map.BaseMap, xmlSchemaSimpleContentExtension.Attributes, out xmlSchemaSequence, out anyAttribute);
				xmlSchemaSimpleContentExtension.AnyAttribute = anyAttribute;
				if (map.BaseMap == null)
				{
					xmlSchemaSimpleContentExtension.BaseTypeName = classMap.SimpleContentBaseType;
				}
				else
				{
					xmlSchemaSimpleContentExtension.BaseTypeName = new XmlQualifiedName(map.BaseMap.XmlType, map.BaseMap.XmlTypeNamespace);
					this.ImportNamespace(schema, map.BaseMap.XmlTypeNamespace);
					this.ExportClassSchema(map.BaseMap);
				}
			}
			else if (map.BaseMap != null && map.BaseMap.IncludeInSchema)
			{
				XmlSchemaComplexContent xmlSchemaComplexContent = new XmlSchemaComplexContent();
				XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = new XmlSchemaComplexContentExtension();
				xmlSchemaComplexContentExtension.BaseTypeName = new XmlQualifiedName(map.BaseMap.XmlType, map.BaseMap.XmlTypeNamespace);
				xmlSchemaComplexContent.Content = xmlSchemaComplexContentExtension;
				xmlSchemaComplexType.ContentModel = xmlSchemaComplexContent;
				XmlSchemaSequence particle;
				XmlSchemaAnyAttribute anyAttribute2;
				this.ExportMembersMapSchema(schema, classMap, map.BaseMap, xmlSchemaComplexContentExtension.Attributes, out particle, out anyAttribute2);
				xmlSchemaComplexContentExtension.Particle = particle;
				xmlSchemaComplexContentExtension.AnyAttribute = anyAttribute2;
				xmlSchemaComplexType.IsMixed = this.HasMixedContent(map);
				xmlSchemaComplexContent.IsMixed = this.BaseHasMixedContent(map);
				this.ImportNamespace(schema, map.BaseMap.XmlTypeNamespace);
				this.ExportClassSchema(map.BaseMap);
			}
			else
			{
				XmlSchemaSequence particle2;
				XmlSchemaAnyAttribute anyAttribute3;
				this.ExportMembersMapSchema(schema, classMap, map.BaseMap, xmlSchemaComplexType.Attributes, out particle2, out anyAttribute3);
				xmlSchemaComplexType.Particle = particle2;
				xmlSchemaComplexType.AnyAttribute = anyAttribute3;
				xmlSchemaComplexType.IsMixed = (classMap.XmlTextCollector != null);
			}
			foreach (object obj2 in map.DerivedTypes)
			{
				XmlTypeMapping xmlTypeMapping2 = (XmlTypeMapping)obj2;
				if (xmlTypeMapping2.TypeData.SchemaType == SchemaTypes.Class)
				{
					this.ExportClassSchema(xmlTypeMapping2);
				}
			}
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0008E14C File Offset: 0x0008C34C
		private bool BaseHasMixedContent(XmlTypeMapping map)
		{
			ClassMap classMap = (ClassMap)map.ObjectMap;
			return classMap.XmlTextCollector != null && map.BaseMap != null && this.DefinedInBaseMap(map.BaseMap, classMap.XmlTextCollector);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0008E194 File Offset: 0x0008C394
		private bool HasMixedContent(XmlTypeMapping map)
		{
			ClassMap classMap = (ClassMap)map.ObjectMap;
			return classMap.XmlTextCollector != null && (map.BaseMap == null || !this.DefinedInBaseMap(map.BaseMap, classMap.XmlTextCollector));
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0008E1E0 File Offset: 0x0008C3E0
		private void ExportMembersMapSchema(XmlSchema schema, ClassMap map, XmlTypeMapping baseMap, XmlSchemaObjectCollection outAttributes, out XmlSchemaSequence particle, out XmlSchemaAnyAttribute anyAttribute)
		{
			particle = null;
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			ICollection elementMembers = map.ElementMembers;
			if (elementMembers != null && !map.HasSimpleContent)
			{
				foreach (object obj in elementMembers)
				{
					XmlTypeMapMemberElement xmlTypeMapMemberElement = (XmlTypeMapMemberElement)obj;
					if (baseMap == null || !this.DefinedInBaseMap(baseMap, xmlTypeMapMemberElement))
					{
						Type type = xmlTypeMapMemberElement.GetType();
						if (type == typeof(XmlTypeMapMemberFlatList))
						{
							XmlSchemaParticle schemaArrayElement = this.GetSchemaArrayElement(schema, xmlTypeMapMemberElement.ElementInfo);
							if (schemaArrayElement != null)
							{
								xmlSchemaSequence.Items.Add(schemaArrayElement);
							}
						}
						else if (type == typeof(XmlTypeMapMemberAnyElement))
						{
							xmlSchemaSequence.Items.Add(this.GetSchemaArrayElement(schema, xmlTypeMapMemberElement.ElementInfo));
						}
						else if (type == typeof(XmlTypeMapMemberElement))
						{
							this.GetSchemaElement(schema, (XmlTypeMapElementInfo)xmlTypeMapMemberElement.ElementInfo[0], xmlTypeMapMemberElement.DefaultValue, true, new XmlSchemaExporter.XmlSchemaObjectContainer(xmlSchemaSequence));
						}
						else
						{
							this.GetSchemaElement(schema, (XmlTypeMapElementInfo)xmlTypeMapMemberElement.ElementInfo[0], true, new XmlSchemaExporter.XmlSchemaObjectContainer(xmlSchemaSequence));
						}
					}
				}
			}
			if (xmlSchemaSequence.Items.Count > 0)
			{
				particle = xmlSchemaSequence;
			}
			ICollection attributeMembers = map.AttributeMembers;
			if (attributeMembers != null)
			{
				foreach (object obj2 in attributeMembers)
				{
					XmlTypeMapMemberAttribute xmlTypeMapMemberAttribute = (XmlTypeMapMemberAttribute)obj2;
					if (baseMap == null || !this.DefinedInBaseMap(baseMap, xmlTypeMapMemberAttribute))
					{
						outAttributes.Add(this.GetSchemaAttribute(schema, xmlTypeMapMemberAttribute, true));
					}
				}
			}
			XmlTypeMapMember defaultAnyAttributeMember = map.DefaultAnyAttributeMember;
			if (defaultAnyAttributeMember != null)
			{
				anyAttribute = new XmlSchemaAnyAttribute();
			}
			else
			{
				anyAttribute = null;
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0008E418 File Offset: 0x0008C618
		private XmlSchemaElement FindElement(XmlSchemaObjectCollection col, string name)
		{
			foreach (XmlSchemaObject xmlSchemaObject in col)
			{
				XmlSchemaElement xmlSchemaElement = xmlSchemaObject as XmlSchemaElement;
				if (xmlSchemaElement != null && xmlSchemaElement.Name == name)
				{
					return xmlSchemaElement;
				}
			}
			return null;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0008E4A4 File Offset: 0x0008C6A4
		private XmlSchemaComplexType FindComplexType(XmlSchemaObjectCollection col, string name)
		{
			foreach (XmlSchemaObject xmlSchemaObject in col)
			{
				XmlSchemaComplexType xmlSchemaComplexType = xmlSchemaObject as XmlSchemaComplexType;
				if (xmlSchemaComplexType != null && xmlSchemaComplexType.Name == name)
				{
					return xmlSchemaComplexType;
				}
			}
			return null;
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0008E530 File Offset: 0x0008C730
		private XmlSchemaAttribute GetSchemaAttribute(XmlSchema currentSchema, XmlTypeMapMemberAttribute attinfo, bool isTypeMember)
		{
			XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
			if (attinfo.DefaultValue != DBNull.Value)
			{
				xmlSchemaAttribute.DefaultValue = this.ExportDefaultValue(attinfo.TypeData, attinfo.MappedType, attinfo.DefaultValue);
			}
			else if (!attinfo.IsOptionalValueType && attinfo.TypeData.IsValueType)
			{
				xmlSchemaAttribute.Use = XmlSchemaUse.Required;
			}
			this.ImportNamespace(currentSchema, attinfo.Namespace);
			XmlSchema xmlSchema;
			if (attinfo.Namespace.Length == 0 && attinfo.Form != XmlSchemaForm.Qualified)
			{
				xmlSchema = currentSchema;
			}
			else
			{
				xmlSchema = this.GetSchema(attinfo.Namespace);
			}
			if (currentSchema != xmlSchema && !this.encodedFormat)
			{
				xmlSchemaAttribute.RefName = new XmlQualifiedName(attinfo.AttributeName, attinfo.Namespace);
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Items)
				{
					if (xmlSchemaObject is XmlSchemaAttribute && ((XmlSchemaAttribute)xmlSchemaObject).Name == attinfo.AttributeName)
					{
						return xmlSchemaAttribute;
					}
				}
				xmlSchema.Items.Add(this.GetSchemaAttribute(xmlSchema, attinfo, false));
				return xmlSchemaAttribute;
			}
			xmlSchemaAttribute.Name = attinfo.AttributeName;
			if (isTypeMember)
			{
				xmlSchemaAttribute.Form = attinfo.Form;
			}
			if (attinfo.TypeData.SchemaType == SchemaTypes.Enum)
			{
				this.ImportNamespace(currentSchema, attinfo.DataTypeNamespace);
				this.ExportEnumSchema(attinfo.MappedType);
				xmlSchemaAttribute.SchemaTypeName = new XmlQualifiedName(attinfo.TypeData.XmlType, attinfo.DataTypeNamespace);
			}
			else if (attinfo.TypeData.SchemaType == SchemaTypes.Array && TypeTranslator.IsPrimitive(attinfo.TypeData.ListItemType))
			{
				xmlSchemaAttribute.SchemaType = this.GetSchemaSimpleListType(attinfo.TypeData);
			}
			else
			{
				xmlSchemaAttribute.SchemaTypeName = new XmlQualifiedName(attinfo.TypeData.XmlType, attinfo.DataTypeNamespace);
			}
			return xmlSchemaAttribute;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0008E768 File Offset: 0x0008C968
		private XmlSchemaParticle GetSchemaElement(XmlSchema currentSchema, XmlTypeMapElementInfo einfo, bool isTypeMember)
		{
			return this.GetSchemaElement(currentSchema, einfo, DBNull.Value, isTypeMember, null);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0008E77C File Offset: 0x0008C97C
		private XmlSchemaParticle GetSchemaElement(XmlSchema currentSchema, XmlTypeMapElementInfo einfo, bool isTypeMember, XmlSchemaExporter.XmlSchemaObjectContainer container)
		{
			return this.GetSchemaElement(currentSchema, einfo, DBNull.Value, isTypeMember, container);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0008E790 File Offset: 0x0008C990
		private XmlSchemaParticle GetSchemaElement(XmlSchema currentSchema, XmlTypeMapElementInfo einfo, object defaultValue, bool isTypeMember, XmlSchemaExporter.XmlSchemaObjectContainer container)
		{
			if (einfo.IsTextElement)
			{
				return null;
			}
			if (einfo.IsUnnamedAnyElement)
			{
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = 1m;
				if (container != null)
				{
					container.Items.Add(xmlSchemaAny);
				}
				return xmlSchemaAny;
			}
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.IsNillable = einfo.IsNullable;
			if (container != null)
			{
				container.Items.Add(xmlSchemaElement);
			}
			if (isTypeMember)
			{
				xmlSchemaElement.MaxOccurs = 1m;
				xmlSchemaElement.MinOccurs = ((!einfo.IsNullable) ? 0 : 1);
				if ((defaultValue == DBNull.Value && einfo.TypeData.IsValueType && einfo.Member != null && !einfo.Member.IsOptionalValueType) || this.encodedFormat)
				{
					xmlSchemaElement.MinOccurs = 1m;
				}
			}
			XmlSchema xmlSchema = null;
			if (!this.encodedFormat)
			{
				xmlSchema = this.GetSchema(einfo.Namespace);
				this.ImportNamespace(currentSchema, einfo.Namespace);
			}
			if (currentSchema != xmlSchema && !this.encodedFormat && isTypeMember)
			{
				xmlSchemaElement.RefName = new XmlQualifiedName(einfo.ElementName, einfo.Namespace);
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Items)
				{
					if (xmlSchemaObject is XmlSchemaElement && ((XmlSchemaElement)xmlSchemaObject).Name == einfo.ElementName)
					{
						return xmlSchemaElement;
					}
				}
				this.GetSchemaElement(xmlSchema, einfo, defaultValue, false, new XmlSchemaExporter.XmlSchemaObjectContainer(xmlSchema));
				return xmlSchemaElement;
			}
			if (isTypeMember)
			{
				xmlSchemaElement.IsNillable = einfo.IsNullable;
			}
			xmlSchemaElement.Name = einfo.ElementName;
			if (defaultValue != DBNull.Value)
			{
				xmlSchemaElement.DefaultValue = this.ExportDefaultValue(einfo.TypeData, einfo.MappedType, defaultValue);
			}
			if (einfo.Form != XmlSchemaForm.Qualified)
			{
				xmlSchemaElement.Form = einfo.Form;
			}
			switch (einfo.TypeData.SchemaType)
			{
			case SchemaTypes.Primitive:
				xmlSchemaElement.SchemaTypeName = new XmlQualifiedName(einfo.TypeData.XmlType, einfo.DataTypeNamespace);
				if (!einfo.TypeData.IsXsdType)
				{
					this.ImportNamespace(currentSchema, einfo.MappedType.XmlTypeNamespace);
					this.ExportDerivedSchema(einfo.MappedType);
				}
				break;
			case SchemaTypes.Enum:
				xmlSchemaElement.SchemaTypeName = new XmlQualifiedName(einfo.MappedType.XmlType, einfo.MappedType.XmlTypeNamespace);
				this.ImportNamespace(currentSchema, einfo.MappedType.XmlTypeNamespace);
				this.ExportEnumSchema(einfo.MappedType);
				break;
			case SchemaTypes.Array:
			{
				XmlQualifiedName xmlQualifiedName = this.ExportArraySchema(einfo.MappedType, currentSchema.TargetNamespace);
				xmlSchemaElement.SchemaTypeName = xmlQualifiedName;
				this.ImportNamespace(currentSchema, xmlQualifiedName.Namespace);
				break;
			}
			case SchemaTypes.Class:
				if (einfo.MappedType.TypeData.Type != typeof(object))
				{
					xmlSchemaElement.SchemaTypeName = new XmlQualifiedName(einfo.MappedType.XmlType, einfo.MappedType.XmlTypeNamespace);
					this.ImportNamespace(currentSchema, einfo.MappedType.XmlTypeNamespace);
				}
				else if (this.encodedFormat)
				{
					xmlSchemaElement.SchemaTypeName = new XmlQualifiedName(einfo.MappedType.XmlType, einfo.MappedType.XmlTypeNamespace);
				}
				this.ExportClassSchema(einfo.MappedType);
				break;
			case SchemaTypes.XmlSerializable:
				this.SetSchemaXmlSerializableType(einfo.MappedType as XmlSerializableMapping, xmlSchemaElement);
				this.ExportXmlSerializableSchema(currentSchema, einfo.MappedType as XmlSerializableMapping);
				break;
			case SchemaTypes.XmlNode:
				xmlSchemaElement.SchemaType = this.GetSchemaXmlNodeType();
				break;
			}
			return xmlSchemaElement;
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0008EBA8 File Offset: 0x0008CDA8
		private void ImportNamespace(XmlSchema schema, string ns)
		{
			if (ns == null || ns.Length == 0 || ns == schema.TargetNamespace || ns == "http://www.w3.org/2001/XMLSchema")
			{
				return;
			}
			foreach (XmlSchemaObject xmlSchemaObject in schema.Includes)
			{
				if (xmlSchemaObject is XmlSchemaImport && ((XmlSchemaImport)xmlSchemaObject).Namespace == ns)
				{
					return;
				}
			}
			XmlSchemaImport xmlSchemaImport = new XmlSchemaImport();
			xmlSchemaImport.Namespace = ns;
			schema.Includes.Add(xmlSchemaImport);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0008EC80 File Offset: 0x0008CE80
		private bool DefinedInBaseMap(XmlTypeMapping map, XmlTypeMapMember member)
		{
			return ((ClassMap)map.ObjectMap).FindMember(member.Name) != null || (map.BaseMap != null && this.DefinedInBaseMap(map.BaseMap, member));
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0008ECC4 File Offset: 0x0008CEC4
		private XmlSchemaType GetSchemaXmlNodeType()
		{
			return new XmlSchemaComplexType
			{
				IsMixed = true,
				Particle = new XmlSchemaSequence
				{
					Items = 
					{
						new XmlSchemaAny()
					}
				}
			};
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0008ED00 File Offset: 0x0008CF00
		private void SetSchemaXmlSerializableType(XmlSerializableMapping map, XmlSchemaElement elem)
		{
			if (map.SchemaType != null && map.Schema != null)
			{
				elem.SchemaType = map.SchemaType;
				return;
			}
			if (map.SchemaType == null && map.SchemaTypeName != null)
			{
				elem.SchemaTypeName = map.SchemaTypeName;
				elem.Name = map.SchemaTypeName.Name;
				return;
			}
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			if (map.Schema == null)
			{
				XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
				xmlSchemaElement.RefName = new XmlQualifiedName("schema", "http://www.w3.org/2001/XMLSchema");
				xmlSchemaSequence.Items.Add(xmlSchemaElement);
				xmlSchemaSequence.Items.Add(new XmlSchemaAny());
			}
			else
			{
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = map.Schema.TargetNamespace;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
			}
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			elem.SchemaType = xmlSchemaComplexType;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0008EDF4 File Offset: 0x0008CFF4
		private XmlSchemaSimpleType GetSchemaSimpleListType(TypeData typeData)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
			XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = new XmlSchemaSimpleTypeList();
			TypeData typeData2 = TypeTranslator.GetTypeData(typeData.ListItemType);
			xmlSchemaSimpleTypeList.ItemTypeName = new XmlQualifiedName(typeData2.XmlType, "http://www.w3.org/2001/XMLSchema");
			xmlSchemaSimpleType.Content = xmlSchemaSimpleTypeList;
			return xmlSchemaSimpleType;
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0008EE38 File Offset: 0x0008D038
		private XmlSchemaParticle GetSchemaArrayElement(XmlSchema currentSchema, XmlTypeMapElementInfoList infos)
		{
			int num = infos.Count;
			if (num > 0 && ((XmlTypeMapElementInfo)infos[0]).IsTextElement)
			{
				num--;
			}
			if (num == 0)
			{
				return null;
			}
			if (num == 1)
			{
				XmlSchemaParticle schemaElement = this.GetSchemaElement(currentSchema, (XmlTypeMapElementInfo)infos[infos.Count - 1], true);
				schemaElement.MinOccursString = "0";
				schemaElement.MaxOccursString = "unbounded";
				return schemaElement;
			}
			XmlSchemaChoice xmlSchemaChoice = new XmlSchemaChoice();
			xmlSchemaChoice.MinOccursString = "0";
			xmlSchemaChoice.MaxOccursString = "unbounded";
			foreach (object obj in infos)
			{
				XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
				if (!xmlTypeMapElementInfo.IsTextElement)
				{
					xmlSchemaChoice.Items.Add(this.GetSchemaElement(currentSchema, xmlTypeMapElementInfo, true));
				}
			}
			return xmlSchemaChoice;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x0008EF50 File Offset: 0x0008D150
		private string ExportDefaultValue(TypeData typeData, XmlTypeMapping map, object defaultValue)
		{
			if (typeData.SchemaType == SchemaTypes.Enum)
			{
				EnumMap enumMap = (EnumMap)map.ObjectMap;
				return enumMap.GetXmlName(map.TypeFullName, defaultValue);
			}
			return XmlCustomFormatter.ToXmlString(typeData, defaultValue);
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0008EF8C File Offset: 0x0008D18C
		private void ExportDerivedSchema(XmlTypeMapping map)
		{
			if (this.IsMapExported(map))
			{
				return;
			}
			this.SetMapExported(map);
			XmlSchema schema = this.GetSchema(map.XmlTypeNamespace);
			for (int i = 0; i < schema.Items.Count; i++)
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = schema.Items[i] as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType != null && xmlSchemaSimpleType.Name == map.ElementName)
				{
					return;
				}
			}
			XmlSchemaSimpleType xmlSchemaSimpleType2 = new XmlSchemaSimpleType();
			xmlSchemaSimpleType2.Name = map.ElementName;
			schema.Items.Add(xmlSchemaSimpleType2);
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = new XmlSchemaSimpleTypeRestriction();
			xmlSchemaSimpleTypeRestriction.BaseTypeName = new XmlQualifiedName(map.TypeData.MappedType.XmlType, "http://www.w3.org/2001/XMLSchema");
			XmlSchemaPatternFacet xmlSchemaPatternFacet = map.TypeData.XmlSchemaPatternFacet;
			if (xmlSchemaPatternFacet != null)
			{
				xmlSchemaSimpleTypeRestriction.Facets.Add(xmlSchemaPatternFacet);
			}
			xmlSchemaSimpleType2.Content = xmlSchemaSimpleTypeRestriction;
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x0008F078 File Offset: 0x0008D278
		private void ExportEnumSchema(XmlTypeMapping map)
		{
			if (this.IsMapExported(map))
			{
				return;
			}
			this.SetMapExported(map);
			XmlSchema schema = this.GetSchema(map.XmlTypeNamespace);
			XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
			xmlSchemaSimpleType.Name = map.ElementName;
			schema.Items.Add(xmlSchemaSimpleType);
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = new XmlSchemaSimpleTypeRestriction();
			xmlSchemaSimpleTypeRestriction.BaseTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
			EnumMap enumMap = (EnumMap)map.ObjectMap;
			foreach (EnumMap.EnumMapMember enumMapMember in enumMap.Members)
			{
				XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet = new XmlSchemaEnumerationFacet();
				xmlSchemaEnumerationFacet.Value = enumMapMember.XmlName;
				xmlSchemaSimpleTypeRestriction.Facets.Add(xmlSchemaEnumerationFacet);
			}
			if (enumMap.IsFlags)
			{
				xmlSchemaSimpleType.Content = new XmlSchemaSimpleTypeList
				{
					ItemType = new XmlSchemaSimpleType
					{
						Content = xmlSchemaSimpleTypeRestriction
					}
				};
			}
			else
			{
				xmlSchemaSimpleType.Content = xmlSchemaSimpleTypeRestriction;
			}
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0008F178 File Offset: 0x0008D378
		private XmlQualifiedName ExportArraySchema(XmlTypeMapping map, string defaultNamespace)
		{
			ListMap listMap = (ListMap)map.ObjectMap;
			if (this.encodedFormat)
			{
				string str;
				string text;
				listMap.GetArrayType(-1, out str, out text);
				string text2;
				if (text == "http://www.w3.org/2001/XMLSchema")
				{
					text2 = defaultNamespace;
				}
				else
				{
					text2 = text;
				}
				if (this.IsMapExported(map))
				{
					return new XmlQualifiedName(listMap.GetSchemaArrayName(), text2);
				}
				this.SetMapExported(map);
				XmlSchema schema = this.GetSchema(text2);
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				xmlSchemaComplexType.Name = listMap.GetSchemaArrayName();
				schema.Items.Add(xmlSchemaComplexType);
				XmlSchemaComplexContent xmlSchemaComplexContent = new XmlSchemaComplexContent();
				xmlSchemaComplexContent.IsMixed = false;
				xmlSchemaComplexType.ContentModel = xmlSchemaComplexContent;
				XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = new XmlSchemaComplexContentRestriction();
				xmlSchemaComplexContent.Content = xmlSchemaComplexContentRestriction;
				xmlSchemaComplexContentRestriction.BaseTypeName = new XmlQualifiedName("Array", "http://schemas.xmlsoap.org/soap/encoding/");
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaComplexContentRestriction.Attributes.Add(xmlSchemaAttribute);
				xmlSchemaAttribute.RefName = new XmlQualifiedName("arrayType", "http://schemas.xmlsoap.org/soap/encoding/");
				XmlAttribute xmlAttribute = this.Document.CreateAttribute("arrayType", "http://schemas.xmlsoap.org/wsdl/");
				xmlAttribute.Value = text + ((!(text != string.Empty)) ? string.Empty : ":") + str;
				xmlSchemaAttribute.UnhandledAttributes = new XmlAttribute[]
				{
					xmlAttribute
				};
				this.ImportNamespace(schema, "http://schemas.xmlsoap.org/wsdl/");
				XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)listMap.ItemInfo[0];
				if (xmlTypeMapElementInfo.MappedType != null)
				{
					switch (xmlTypeMapElementInfo.TypeData.SchemaType)
					{
					case SchemaTypes.Enum:
						this.ExportEnumSchema(xmlTypeMapElementInfo.MappedType);
						break;
					case SchemaTypes.Array:
						this.ExportArraySchema(xmlTypeMapElementInfo.MappedType, text2);
						break;
					case SchemaTypes.Class:
						this.ExportClassSchema(xmlTypeMapElementInfo.MappedType);
						break;
					}
				}
				return new XmlQualifiedName(listMap.GetSchemaArrayName(), text2);
			}
			else
			{
				if (this.IsMapExported(map))
				{
					return new XmlQualifiedName(map.XmlType, map.XmlTypeNamespace);
				}
				this.SetMapExported(map);
				XmlSchema schema2 = this.GetSchema(map.XmlTypeNamespace);
				XmlSchemaComplexType xmlSchemaComplexType2 = new XmlSchemaComplexType();
				xmlSchemaComplexType2.Name = map.ElementName;
				schema2.Items.Add(xmlSchemaComplexType2);
				XmlSchemaParticle schemaArrayElement = this.GetSchemaArrayElement(schema2, listMap.ItemInfo);
				if (schemaArrayElement is XmlSchemaChoice)
				{
					xmlSchemaComplexType2.Particle = schemaArrayElement;
				}
				else
				{
					xmlSchemaComplexType2.Particle = new XmlSchemaSequence
					{
						Items = 
						{
							schemaArrayElement
						}
					};
				}
				return new XmlQualifiedName(map.XmlType, map.XmlTypeNamespace);
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x0008F418 File Offset: 0x0008D618
		private XmlDocument Document
		{
			get
			{
				if (this.xmlDoc == null)
				{
					this.xmlDoc = new XmlDocument();
				}
				return this.xmlDoc;
			}
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0008F438 File Offset: 0x0008D638
		private bool IsMapExported(XmlTypeMapping map)
		{
			return this.exportedMaps.ContainsKey(this.GetMapKey(map));
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0008F454 File Offset: 0x0008D654
		private void SetMapExported(XmlTypeMapping map)
		{
			this.exportedMaps[this.GetMapKey(map)] = map;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0008F46C File Offset: 0x0008D66C
		private bool IsElementExported(XmlTypeMapping map)
		{
			return this.exportedElements.ContainsKey(this.GetMapKey(map)) || map.TypeData.Type == typeof(object);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0008F4B0 File Offset: 0x0008D6B0
		private void SetElementExported(XmlTypeMapping map)
		{
			this.exportedElements[this.GetMapKey(map)] = map;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0008F4C8 File Offset: 0x0008D6C8
		private string GetMapKey(XmlTypeMapping map)
		{
			if (map.TypeData.IsListType)
			{
				return string.Concat(new string[]
				{
					this.GetArrayKeyName(map.TypeData),
					" ",
					map.XmlType,
					" ",
					map.XmlTypeNamespace
				});
			}
			return string.Concat(new string[]
			{
				map.TypeData.FullTypeName,
				" ",
				map.XmlType,
				" ",
				map.XmlTypeNamespace
			});
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0008F560 File Offset: 0x0008D760
		private string GetArrayKeyName(TypeData td)
		{
			TypeData listItemTypeData = td.ListItemTypeData;
			return "*arrayof*" + ((!listItemTypeData.IsListType) ? listItemTypeData.FullTypeName : this.GetArrayKeyName(listItemTypeData));
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0008F59C File Offset: 0x0008D79C
		private void CompileSchemas()
		{
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0008F5A0 File Offset: 0x0008D7A0
		private XmlSchema GetSchema(string ns)
		{
			XmlSchema xmlSchema = this.schemas[ns];
			if (xmlSchema == null)
			{
				xmlSchema = new XmlSchema();
				if (ns != null && ns.Length > 0)
				{
					xmlSchema.TargetNamespace = ns;
				}
				if (!this.encodedFormat)
				{
					xmlSchema.ElementFormDefault = XmlSchemaForm.Qualified;
				}
				this.schemas.Add(xmlSchema);
			}
			return xmlSchema;
		}

		// Token: 0x04000B1F RID: 2847
		private XmlSchemas schemas;

		// Token: 0x04000B20 RID: 2848
		private Hashtable exportedMaps = new Hashtable();

		// Token: 0x04000B21 RID: 2849
		private Hashtable exportedElements = new Hashtable();

		// Token: 0x04000B22 RID: 2850
		private bool encodedFormat;

		// Token: 0x04000B23 RID: 2851
		private XmlDocument xmlDoc;

		// Token: 0x0200029F RID: 671
		private class XmlSchemaObjectContainer
		{
			// Token: 0x06001B4B RID: 6987 RVA: 0x0008F600 File Offset: 0x0008D800
			public XmlSchemaObjectContainer(XmlSchema schema)
			{
				this._xmlSchemaObject = schema;
			}

			// Token: 0x06001B4C RID: 6988 RVA: 0x0008F610 File Offset: 0x0008D810
			public XmlSchemaObjectContainer(XmlSchemaGroupBase group)
			{
				this._xmlSchemaObject = group;
			}

			// Token: 0x170007E1 RID: 2017
			// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0008F620 File Offset: 0x0008D820
			public XmlSchemaObjectCollection Items
			{
				get
				{
					if (this._xmlSchemaObject is XmlSchema)
					{
						return ((XmlSchema)this._xmlSchemaObject).Items;
					}
					return ((XmlSchemaGroupBase)this._xmlSchemaObject).Items;
				}
			}

			// Token: 0x04000B24 RID: 2852
			private readonly XmlSchemaObject _xmlSchemaObject;
		}
	}
}
