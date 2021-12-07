using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000288 RID: 648
	internal class XmlMapCodeGenerator : MapCodeGenerator
	{
		// Token: 0x06001A42 RID: 6722 RVA: 0x00088FA4 File Offset: 0x000871A4
		public XmlMapCodeGenerator(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeGenerationOptions options) : base(codeNamespace, codeCompileUnit, options)
		{
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00088FB0 File Offset: 0x000871B0
		public XmlMapCodeGenerator(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeDomProvider codeProvider, CodeGenerationOptions options, Hashtable mappings) : base(codeNamespace, codeCompileUnit, codeProvider, options, mappings)
		{
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00088FC0 File Offset: 0x000871C0
		protected override void GenerateClass(XmlTypeMapping map, CodeTypeDeclaration codeClass, bool isTopLevel)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Xml.Serialization.XmlTypeAttribute");
			if (map.XmlType != map.TypeData.TypeName)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg(map.XmlType));
			}
			if (map.XmlTypeNamespace != string.Empty)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("Namespace", map.XmlTypeNamespace));
			}
			if (!map.IncludeInSchema)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("IncludeInSchema", false));
			}
			MapCodeGenerator.AddCustomAttribute(codeClass, codeAttributeDeclaration, false);
			CodeAttributeDeclaration codeAttributeDeclaration2 = new CodeAttributeDeclaration("System.Xml.Serialization.XmlRootAttribute");
			if (map.ElementName != map.XmlType)
			{
				codeAttributeDeclaration2.Arguments.Add(MapCodeGenerator.GetArg(map.ElementName));
			}
			if (isTopLevel)
			{
				codeAttributeDeclaration2.Arguments.Add(MapCodeGenerator.GetArg("Namespace", map.Namespace));
				codeAttributeDeclaration2.Arguments.Add(MapCodeGenerator.GetArg("IsNullable", map.IsNullable));
			}
			else if (map.Namespace != string.Empty)
			{
				codeAttributeDeclaration2.Arguments.Add(MapCodeGenerator.GetArg("Namespace", map.Namespace));
			}
			MapCodeGenerator.AddCustomAttribute(codeClass, codeAttributeDeclaration2, isTopLevel);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00089124 File Offset: 0x00087324
		protected override void GenerateClassInclude(CodeAttributeDeclarationCollection attributes, XmlTypeMapping map)
		{
			attributes.Add(new CodeAttributeDeclaration("System.Xml.Serialization.XmlIncludeAttribute")
			{
				Arguments = 
				{
					new CodeAttributeArgument(new CodeTypeOfExpression(map.TypeData.FullTypeName))
				}
			});
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00089168 File Offset: 0x00087368
		protected override void GenerateAnyAttribute(CodeTypeMember codeField)
		{
			MapCodeGenerator.AddCustomAttribute(codeField, "System.Xml.Serialization.XmlAnyAttribute", new CodeAttributeArgument[0]);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0008917C File Offset: 0x0008737C
		protected override void GenerateAttributeMember(CodeAttributeDeclarationCollection attributes, XmlTypeMapMemberAttribute attinfo, string defaultNamespace, bool forceUseMemberName)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Xml.Serialization.XmlAttributeAttribute");
			if (forceUseMemberName || attinfo.Name != attinfo.AttributeName)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg(attinfo.AttributeName));
			}
			if (attinfo.Namespace != defaultNamespace)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("Namespace", attinfo.Namespace));
			}
			if (attinfo.Form == XmlSchemaForm.Qualified)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetEnumArg("Form", "System.Xml.Schema.XmlSchemaForm", attinfo.Form.ToString()));
			}
			if (!TypeTranslator.IsDefaultPrimitiveTpeData(attinfo.TypeData))
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("DataType", attinfo.TypeData.XmlType));
			}
			attributes.Add(codeAttributeDeclaration);
			if (attinfo.Ignore)
			{
				attributes.Add(new CodeAttributeDeclaration("System.Xml.Serialization.XmlIgnoreAttribute"));
			}
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00089284 File Offset: 0x00087484
		protected override void GenerateElementInfoMember(CodeAttributeDeclarationCollection attributes, XmlTypeMapMemberElement member, XmlTypeMapElementInfo einfo, TypeData defaultType, string defaultNamespace, bool addAlwaysAttr, bool forceUseMemberName)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Xml.Serialization.XmlElementAttribute");
			if (forceUseMemberName || einfo.ElementName != member.Name)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg(einfo.ElementName));
			}
			if (einfo.TypeData.FullTypeName != defaultType.FullTypeName)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetTypeArg("Type", einfo.TypeData.FullTypeName));
			}
			if (einfo.Namespace != defaultNamespace)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("Namespace", einfo.Namespace));
			}
			if (einfo.Form == XmlSchemaForm.Unqualified)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetEnumArg("Form", "System.Xml.Schema.XmlSchemaForm", einfo.Form.ToString()));
			}
			if (einfo.IsNullable)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("IsNullable", true));
			}
			if (!TypeTranslator.IsDefaultPrimitiveTpeData(einfo.TypeData))
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("DataType", einfo.TypeData.XmlType));
			}
			if (addAlwaysAttr || codeAttributeDeclaration.Arguments.Count > 0)
			{
				attributes.Add(codeAttributeDeclaration);
			}
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000893EC File Offset: 0x000875EC
		protected override void GenerateElementMember(CodeAttributeDeclarationCollection attributes, XmlTypeMapMemberElement member)
		{
			if (member.ChoiceMember != null)
			{
				attributes.Add(new CodeAttributeDeclaration("System.Xml.Serialization.XmlChoiceIdentifier")
				{
					Arguments = 
					{
						MapCodeGenerator.GetArg(member.ChoiceMember)
					}
				});
			}
			if (member.Ignore)
			{
				attributes.Add(new CodeAttributeDeclaration("System.Xml.Serialization.XmlIgnoreAttribute"));
			}
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0008944C File Offset: 0x0008764C
		protected override void GenerateArrayElement(CodeAttributeDeclarationCollection attributes, XmlTypeMapMemberElement member, string defaultNamespace, bool forceUseMemberName)
		{
			XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)member.ElementInfo[0];
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Xml.Serialization.XmlArray");
			if (forceUseMemberName || xmlTypeMapElementInfo.ElementName != member.Name)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("ElementName", xmlTypeMapElementInfo.ElementName));
			}
			if (xmlTypeMapElementInfo.Namespace != defaultNamespace)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("Namespace", xmlTypeMapElementInfo.Namespace));
			}
			if (xmlTypeMapElementInfo.Form == XmlSchemaForm.Unqualified)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetEnumArg("Form", "System.Xml.Schema.XmlSchemaForm", xmlTypeMapElementInfo.Form.ToString()));
			}
			if (xmlTypeMapElementInfo.IsNullable)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("IsNullable", true));
			}
			if (codeAttributeDeclaration.Arguments.Count > 0)
			{
				attributes.Add(codeAttributeDeclaration);
			}
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00089554 File Offset: 0x00087754
		protected override void GenerateArrayItemAttributes(CodeAttributeDeclarationCollection attributes, ListMap listMap, TypeData type, XmlTypeMapElementInfo ainfo, string defaultName, string defaultNamespace, int nestingLevel)
		{
			bool flag = listMap.ItemInfo.Count > 1 || (ainfo.TypeData.FullTypeName != type.FullTypeName && !listMap.IsMultiArray);
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Xml.Serialization.XmlArrayItem");
			if (ainfo.ElementName != defaultName)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("ElementName", ainfo.ElementName));
			}
			if (ainfo.Namespace != defaultNamespace && ainfo.Namespace != "http://www.w3.org/2001/XMLSchema")
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("Namespace", ainfo.Namespace));
			}
			if (flag)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetTypeArg("Type", ainfo.TypeData.FullTypeName));
			}
			if (!ainfo.IsNullable)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("IsNullable", false));
			}
			if (ainfo.Form == XmlSchemaForm.Unqualified)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetEnumArg("Form", "System.Xml.Schema.XmlSchemaForm", ainfo.Form.ToString()));
			}
			if (codeAttributeDeclaration.Arguments.Count > 0 && nestingLevel > 0)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("NestingLevel", nestingLevel));
			}
			if (codeAttributeDeclaration.Arguments.Count > 0)
			{
				attributes.Add(codeAttributeDeclaration);
			}
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000896F8 File Offset: 0x000878F8
		protected override void GenerateTextElementAttribute(CodeAttributeDeclarationCollection attributes, XmlTypeMapElementInfo einfo, TypeData defaultType)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Xml.Serialization.XmlTextAttribute");
			if (einfo.TypeData.FullTypeName != defaultType.FullTypeName)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetTypeArg("Type", einfo.TypeData.FullTypeName));
			}
			attributes.Add(codeAttributeDeclaration);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00089754 File Offset: 0x00087954
		protected override void GenerateUnnamedAnyElementAttribute(CodeAttributeDeclarationCollection attributes, XmlTypeMapElementInfo einfo, string defaultNamespace)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Xml.Serialization.XmlAnyElement");
			if (!einfo.IsUnnamedAnyElement)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("Name", einfo.ElementName));
			}
			if (einfo.Namespace != defaultNamespace)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("Namespace", einfo.Namespace));
			}
			attributes.Add(codeAttributeDeclaration);
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x000897C8 File Offset: 0x000879C8
		protected override void GenerateEnum(XmlTypeMapping map, CodeTypeDeclaration codeEnum, bool isTopLevel)
		{
			this.GenerateClass(map, codeEnum, isTopLevel);
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x000897D4 File Offset: 0x000879D4
		protected override void GenerateEnumItem(CodeMemberField codeField, EnumMap.EnumMapMember emem)
		{
			if (emem.EnumName != emem.XmlName)
			{
				MapCodeGenerator.AddCustomAttribute(codeField, new CodeAttributeDeclaration("System.Xml.Serialization.XmlEnumAttribute")
				{
					Arguments = 
					{
						MapCodeGenerator.GetArg(emem.XmlName)
					}
				}, true);
			}
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00089824 File Offset: 0x00087A24
		protected override void GenerateSpecifierMember(CodeTypeMember codeField)
		{
			MapCodeGenerator.AddCustomAttribute(codeField, "System.Xml.Serialization.XmlIgnore", new CodeAttributeArgument[0]);
		}
	}
}
