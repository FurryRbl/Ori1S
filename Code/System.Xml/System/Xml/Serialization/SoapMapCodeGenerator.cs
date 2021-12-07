using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x0200026F RID: 623
	internal class SoapMapCodeGenerator : MapCodeGenerator
	{
		// Token: 0x06001938 RID: 6456 RVA: 0x00084D04 File Offset: 0x00082F04
		public SoapMapCodeGenerator(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit) : base(codeNamespace, codeCompileUnit, CodeGenerationOptions.None)
		{
			this.includeArrayTypes = true;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00084D18 File Offset: 0x00082F18
		public SoapMapCodeGenerator(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeDomProvider codeProvider, CodeGenerationOptions options, Hashtable mappings) : base(codeNamespace, codeCompileUnit, codeProvider, options, mappings)
		{
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00084D28 File Offset: 0x00082F28
		protected override void GenerateClass(XmlTypeMapping map, CodeTypeDeclaration codeClass, bool isTopLevel)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Xml.Serialization.SoapType");
			if (map.XmlType != map.TypeData.TypeName)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg(map.XmlType));
			}
			if (map.XmlTypeNamespace != string.Empty)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("Namespace", map.XmlTypeNamespace));
			}
			MapCodeGenerator.AddCustomAttribute(codeClass, codeAttributeDeclaration, false);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00084DAC File Offset: 0x00082FAC
		protected override void GenerateClassInclude(CodeAttributeDeclarationCollection attributes, XmlTypeMapping map)
		{
			attributes.Add(new CodeAttributeDeclaration("System.Xml.Serialization.SoapInclude")
			{
				Arguments = 
				{
					new CodeAttributeArgument(new CodeTypeOfExpression(map.TypeData.FullTypeName))
				}
			});
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00084DF0 File Offset: 0x00082FF0
		protected override void GenerateAttributeMember(CodeAttributeDeclarationCollection attributes, XmlTypeMapMemberAttribute attinfo, string defaultNamespace, bool forceUseMemberName)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Xml.Serialization.SoapAttribute");
			if (attinfo.Name != attinfo.AttributeName)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg(attinfo.AttributeName));
			}
			if (attinfo.Namespace != defaultNamespace)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("Namespace", attinfo.Namespace));
			}
			if (!TypeTranslator.IsDefaultPrimitiveTpeData(attinfo.TypeData))
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("DataType", attinfo.TypeData.XmlType));
			}
			attributes.Add(codeAttributeDeclaration);
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00084E9C File Offset: 0x0008309C
		protected override void GenerateElementInfoMember(CodeAttributeDeclarationCollection attributes, XmlTypeMapMemberElement member, XmlTypeMapElementInfo einfo, TypeData defaultType, string defaultNamespace, bool addAlwaysAttr, bool forceUseMemberName)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Xml.Serialization.SoapElement");
			if (forceUseMemberName || einfo.ElementName != member.Name)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg(einfo.ElementName));
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

		// Token: 0x0600193E RID: 6462 RVA: 0x00084F3C File Offset: 0x0008313C
		protected override void GenerateEnum(XmlTypeMapping map, CodeTypeDeclaration codeEnum, bool isTopLevel)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Xml.Serialization.SoapType");
			if (map.XmlType != map.TypeData.TypeName)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg(map.XmlType));
			}
			if (map.XmlTypeNamespace != string.Empty)
			{
				codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("Namespace", map.XmlTypeNamespace));
			}
			MapCodeGenerator.AddCustomAttribute(codeEnum, codeAttributeDeclaration, false);
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00084FC0 File Offset: 0x000831C0
		protected override void GenerateEnumItem(CodeMemberField codeField, EnumMap.EnumMapMember emem)
		{
			if (emem.EnumName != emem.XmlName)
			{
				MapCodeGenerator.AddCustomAttribute(codeField, new CodeAttributeDeclaration("System.Xml.Serialization.SoapEnum")
				{
					Arguments = 
					{
						MapCodeGenerator.GetArg("Name", emem.XmlName)
					}
				}, true);
			}
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00085014 File Offset: 0x00083214
		protected override void GenerateSpecifierMember(CodeTypeMember codeField)
		{
			MapCodeGenerator.AddCustomAttribute(codeField, "System.Xml.Serialization.SoapIgnore", new CodeAttributeArgument[0]);
		}
	}
}
