using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using Microsoft.CSharp;

namespace System.Xml.Serialization
{
	// Token: 0x0200025A RID: 602
	internal class MapCodeGenerator
	{
		// Token: 0x0600184A RID: 6218 RVA: 0x0007A448 File Offset: 0x00078648
		public MapCodeGenerator(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeGenerationOptions options)
		{
			this.codeCompileUnit = codeCompileUnit;
			this.codeNamespace = codeNamespace;
			this.options = options;
			this.identifiers = new CodeIdentifiers();
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0007A494 File Offset: 0x00078694
		public MapCodeGenerator(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeDomProvider codeProvider, CodeGenerationOptions options, Hashtable mappings)
		{
			this.codeCompileUnit = codeCompileUnit;
			this.codeNamespace = codeNamespace;
			this.options = options;
			this.codeProvider = codeProvider;
			this.identifiers = new CodeIdentifiers((codeProvider.LanguageOptions & LanguageOptions.CaseInsensitive) == LanguageOptions.None);
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x0007A4F0 File Offset: 0x000786F0
		public CodeAttributeDeclarationCollection IncludeMetadata
		{
			get
			{
				if (this.includeMetadata != null)
				{
					return this.includeMetadata;
				}
				this.includeMetadata = new CodeAttributeDeclarationCollection();
				foreach (object obj in this.includeMaps.Values)
				{
					XmlTypeMapping map = (XmlTypeMapping)obj;
					this.GenerateClassInclude(this.includeMetadata, map);
				}
				return this.includeMetadata;
			}
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0007A590 File Offset: 0x00078790
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping)
		{
			CodeTypeDeclaration codeClass = new CodeTypeDeclaration();
			this.ExportMembersMapCode(codeClass, (ClassMap)xmlMembersMapping.ObjectMap, xmlMembersMapping.Namespace, null);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0007A5BC File Offset: 0x000787BC
		public void ExportTypeMapping(XmlTypeMapping xmlTypeMapping, bool isTopLevel)
		{
			this.ExportMapCode(xmlTypeMapping, isTopLevel);
			this.RemoveInclude(xmlTypeMapping);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0007A5D0 File Offset: 0x000787D0
		private void ExportMapCode(XmlTypeMapping map, bool isTopLevel)
		{
			switch (map.TypeData.SchemaType)
			{
			case SchemaTypes.Enum:
				this.ExportEnumCode(map, isTopLevel);
				break;
			case SchemaTypes.Array:
				this.ExportArrayCode(map);
				break;
			case SchemaTypes.Class:
				this.ExportClassCode(map, isTopLevel);
				break;
			}
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0007A63C File Offset: 0x0007883C
		private void ExportClassCode(XmlTypeMapping map, bool isTopLevel)
		{
			CodeTypeDeclaration codeTypeDeclaration;
			if (this.IsMapExported(map))
			{
				codeTypeDeclaration = this.GetMapDeclaration(map);
				if (codeTypeDeclaration != null)
				{
					codeTypeDeclaration.CustomAttributes.Clear();
					this.AddClassAttributes(codeTypeDeclaration);
					this.GenerateClass(map, codeTypeDeclaration, isTopLevel);
					this.ExportDerivedTypeAttributes(map, codeTypeDeclaration);
				}
				return;
			}
			if (map.TypeData.Type == typeof(object))
			{
				this.exportedAnyType = map;
				this.SetMapExported(map, null);
				foreach (object obj in this.exportedAnyType.DerivedTypes)
				{
					XmlTypeMapping xmlTypeMapping = (XmlTypeMapping)obj;
					if (!this.IsMapExported(xmlTypeMapping) && xmlTypeMapping.IncludeInSchema)
					{
						this.ExportTypeMapping(xmlTypeMapping, false);
						this.AddInclude(xmlTypeMapping);
					}
				}
				return;
			}
			codeTypeDeclaration = new CodeTypeDeclaration(map.TypeData.TypeName);
			this.SetMapExported(map, codeTypeDeclaration);
			this.AddCodeType(codeTypeDeclaration, map.Documentation);
			codeTypeDeclaration.Attributes = MemberAttributes.Public;
			codeTypeDeclaration.IsPartial = this.CodeProvider.Supports(GeneratorSupport.PartialTypes);
			this.AddClassAttributes(codeTypeDeclaration);
			this.GenerateClass(map, codeTypeDeclaration, isTopLevel);
			this.ExportDerivedTypeAttributes(map, codeTypeDeclaration);
			this.ExportMembersMapCode(codeTypeDeclaration, (ClassMap)map.ObjectMap, map.XmlTypeNamespace, map.BaseMap);
			if (map.BaseMap != null && map.BaseMap.TypeData.SchemaType != SchemaTypes.XmlNode)
			{
				CodeTypeReference domType = this.GetDomType(map.BaseMap.TypeData, false);
				codeTypeDeclaration.BaseTypes.Add(domType);
				if (map.BaseMap.IncludeInSchema)
				{
					this.ExportMapCode(map.BaseMap, false);
					this.AddInclude(map.BaseMap);
				}
			}
			this.ExportDerivedTypes(map, codeTypeDeclaration);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0007A830 File Offset: 0x00078A30
		private void ExportDerivedTypeAttributes(XmlTypeMapping map, CodeTypeDeclaration codeClass)
		{
			foreach (object obj in map.DerivedTypes)
			{
				XmlTypeMapping map2 = (XmlTypeMapping)obj;
				this.GenerateClassInclude(codeClass.CustomAttributes, map2);
				this.ExportDerivedTypeAttributes(map2, codeClass);
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0007A8B0 File Offset: 0x00078AB0
		private void ExportDerivedTypes(XmlTypeMapping map, CodeTypeDeclaration codeClass)
		{
			foreach (object obj in map.DerivedTypes)
			{
				XmlTypeMapping map2 = (XmlTypeMapping)obj;
				if (codeClass.CustomAttributes == null)
				{
					codeClass.CustomAttributes = new CodeAttributeDeclarationCollection();
				}
				this.ExportMapCode(map2, false);
				this.ExportDerivedTypes(map2, codeClass);
			}
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0007A940 File Offset: 0x00078B40
		private void ExportMembersMapCode(CodeTypeDeclaration codeClass, ClassMap map, string defaultNamespace, XmlTypeMapping baseMap)
		{
			ICollection attributeMembers = map.AttributeMembers;
			ICollection elementMembers = map.ElementMembers;
			if (attributeMembers != null)
			{
				foreach (object obj in attributeMembers)
				{
					XmlTypeMapMemberAttribute xmlTypeMapMemberAttribute = (XmlTypeMapMemberAttribute)obj;
					this.identifiers.AddUnique(xmlTypeMapMemberAttribute.Name, xmlTypeMapMemberAttribute);
				}
			}
			if (elementMembers != null)
			{
				foreach (object obj2 in elementMembers)
				{
					XmlTypeMapMemberElement xmlTypeMapMemberElement = (XmlTypeMapMemberElement)obj2;
					this.identifiers.AddUnique(xmlTypeMapMemberElement.Name, xmlTypeMapMemberElement);
				}
			}
			if (attributeMembers != null)
			{
				foreach (object obj3 in attributeMembers)
				{
					XmlTypeMapMemberAttribute xmlTypeMapMemberAttribute2 = (XmlTypeMapMemberAttribute)obj3;
					if (baseMap == null || !this.DefinedInBaseMap(baseMap, xmlTypeMapMemberAttribute2))
					{
						this.AddAttributeFieldMember(codeClass, xmlTypeMapMemberAttribute2, defaultNamespace);
					}
				}
			}
			elementMembers = map.ElementMembers;
			if (elementMembers != null)
			{
				foreach (object obj4 in elementMembers)
				{
					XmlTypeMapMemberElement xmlTypeMapMemberElement2 = (XmlTypeMapMemberElement)obj4;
					if (baseMap == null || !this.DefinedInBaseMap(baseMap, xmlTypeMapMemberElement2))
					{
						Type type = xmlTypeMapMemberElement2.GetType();
						if (type == typeof(XmlTypeMapMemberList))
						{
							this.AddArrayElementFieldMember(codeClass, (XmlTypeMapMemberList)xmlTypeMapMemberElement2, defaultNamespace);
						}
						else if (type == typeof(XmlTypeMapMemberFlatList))
						{
							this.AddElementFieldMember(codeClass, xmlTypeMapMemberElement2, defaultNamespace);
						}
						else if (type == typeof(XmlTypeMapMemberAnyElement))
						{
							this.AddAnyElementFieldMember(codeClass, xmlTypeMapMemberElement2, defaultNamespace);
						}
						else
						{
							if (type != typeof(XmlTypeMapMemberElement))
							{
								throw new InvalidOperationException("Member type " + type + " not supported");
							}
							this.AddElementFieldMember(codeClass, xmlTypeMapMemberElement2, defaultNamespace);
						}
					}
				}
			}
			XmlTypeMapMember defaultAnyAttributeMember = map.DefaultAnyAttributeMember;
			if (defaultAnyAttributeMember != null)
			{
				CodeTypeMember codeTypeMember = this.CreateFieldMember(codeClass, defaultAnyAttributeMember.TypeData, defaultAnyAttributeMember.Name);
				MapCodeGenerator.AddComments(codeTypeMember, defaultAnyAttributeMember.Documentation);
				codeTypeMember.Attributes = MemberAttributes.Public;
				this.GenerateAnyAttribute(codeTypeMember);
			}
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0007AC38 File Offset: 0x00078E38
		private CodeTypeMember CreateFieldMember(CodeTypeDeclaration codeClass, Type type, string name)
		{
			return this.CreateFieldMember(codeClass, new CodeTypeReference(type), name, DBNull.Value, null, null);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0007AC50 File Offset: 0x00078E50
		private CodeTypeMember CreateFieldMember(CodeTypeDeclaration codeClass, TypeData type, string name)
		{
			return this.CreateFieldMember(codeClass, this.GetDomType(type, false), name, DBNull.Value, null, null);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0007AC74 File Offset: 0x00078E74
		private CodeTypeMember CreateFieldMember(CodeTypeDeclaration codeClass, XmlTypeMapMember member)
		{
			return this.CreateFieldMember(codeClass, this.GetDomType(member.TypeData, member.RequiresNullable), member.Name, member.DefaultValue, member.TypeData, member.Documentation);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0007ACB4 File Offset: 0x00078EB4
		private CodeTypeMember CreateFieldMember(CodeTypeDeclaration codeClass, CodeTypeReference type, string name, object defaultValue, TypeData defaultType, string documentation)
		{
			CodeMemberField codeMemberField;
			CodeTypeMember codeTypeMember;
			if ((this.options & CodeGenerationOptions.GenerateProperties) > CodeGenerationOptions.None)
			{
				string text = this.identifiers.AddUnique(CodeIdentifier.MakeCamel(name + "Field"), name);
				codeMemberField = new CodeMemberField(type, text);
				codeMemberField.Attributes = MemberAttributes.Private;
				codeClass.Members.Add(codeMemberField);
				CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Name = name;
				codeMemberProperty.Type = type;
				codeMemberProperty.Attributes = (MemberAttributes)24578;
				codeTypeMember = codeMemberProperty;
				CodeMemberProperty codeMemberProperty2 = codeMemberProperty;
				bool flag = true;
				codeMemberProperty.HasSet = flag;
				codeMemberProperty2.HasGet = flag;
				CodeExpression codeExpression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), text);
				codeMemberProperty.SetStatements.Add(new CodeAssignStatement(codeExpression, new CodePropertySetValueReferenceExpression()));
				codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(codeExpression));
			}
			else
			{
				codeMemberField = new CodeMemberField(type, name);
				codeMemberField.Attributes = MemberAttributes.Public;
				codeTypeMember = codeMemberField;
			}
			if (defaultValue != DBNull.Value)
			{
				this.GenerateDefaultAttribute(codeMemberField, codeTypeMember, defaultType, defaultValue);
			}
			MapCodeGenerator.AddComments(codeTypeMember, documentation);
			codeClass.Members.Add(codeTypeMember);
			return codeTypeMember;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0007ADC4 File Offset: 0x00078FC4
		private void AddAttributeFieldMember(CodeTypeDeclaration codeClass, XmlTypeMapMemberAttribute attinfo, string defaultNamespace)
		{
			CodeTypeMember codeTypeMember = this.CreateFieldMember(codeClass, attinfo);
			CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = codeTypeMember.CustomAttributes;
			if (codeAttributeDeclarationCollection == null)
			{
				codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection();
			}
			this.GenerateAttributeMember(codeAttributeDeclarationCollection, attinfo, defaultNamespace, false);
			if (codeAttributeDeclarationCollection.Count > 0)
			{
				codeTypeMember.CustomAttributes = codeAttributeDeclarationCollection;
			}
			if (attinfo.MappedType != null)
			{
				this.ExportMapCode(attinfo.MappedType, false);
				this.RemoveInclude(attinfo.MappedType);
			}
			if (attinfo.TypeData.IsValueType && attinfo.IsOptionalValueType)
			{
				codeTypeMember = this.CreateFieldMember(codeClass, typeof(bool), this.identifiers.MakeUnique(attinfo.Name + "Specified"));
				codeTypeMember.Attributes = MemberAttributes.Public;
				this.GenerateSpecifierMember(codeTypeMember);
			}
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0007AE88 File Offset: 0x00079088
		public void AddAttributeMemberAttributes(XmlTypeMapMemberAttribute attinfo, string defaultNamespace, CodeAttributeDeclarationCollection attributes, bool forceUseMemberName)
		{
			this.GenerateAttributeMember(attributes, attinfo, defaultNamespace, forceUseMemberName);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x0007AE98 File Offset: 0x00079098
		private void AddElementFieldMember(CodeTypeDeclaration codeClass, XmlTypeMapMemberElement member, string defaultNamespace)
		{
			CodeTypeMember codeTypeMember = this.CreateFieldMember(codeClass, member);
			CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = codeTypeMember.CustomAttributes;
			if (codeAttributeDeclarationCollection == null)
			{
				codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection();
			}
			this.AddElementMemberAttributes(member, defaultNamespace, codeAttributeDeclarationCollection, false);
			if (codeAttributeDeclarationCollection.Count > 0)
			{
				codeTypeMember.CustomAttributes = codeAttributeDeclarationCollection;
			}
			if (member.TypeData.IsValueType && member.IsOptionalValueType)
			{
				codeTypeMember = this.CreateFieldMember(codeClass, typeof(bool), this.identifiers.MakeUnique(member.Name + "Specified"));
				codeTypeMember.Attributes = MemberAttributes.Public;
				this.GenerateSpecifierMember(codeTypeMember);
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0007AF38 File Offset: 0x00079138
		public void AddElementMemberAttributes(XmlTypeMapMemberElement member, string defaultNamespace, CodeAttributeDeclarationCollection attributes, bool forceUseMemberName)
		{
			TypeData typeData = member.TypeData;
			bool flag = false;
			if (member is XmlTypeMapMemberFlatList)
			{
				typeData = typeData.ListItemTypeData;
				flag = true;
			}
			foreach (object obj in member.ElementInfo)
			{
				XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
				if (xmlTypeMapElementInfo.MappedType != null)
				{
					this.ExportMapCode(xmlTypeMapElementInfo.MappedType, false);
					this.RemoveInclude(xmlTypeMapElementInfo.MappedType);
				}
				if (!this.ExportExtraElementAttributes(attributes, xmlTypeMapElementInfo, defaultNamespace, typeData))
				{
					this.GenerateElementInfoMember(attributes, member, xmlTypeMapElementInfo, typeData, defaultNamespace, flag, forceUseMemberName || flag);
				}
			}
			this.GenerateElementMember(attributes, member);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0007B010 File Offset: 0x00079210
		private void AddAnyElementFieldMember(CodeTypeDeclaration codeClass, XmlTypeMapMemberElement member, string defaultNamespace)
		{
			CodeTypeMember codeTypeMember = this.CreateFieldMember(codeClass, member);
			CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection();
			foreach (object obj in member.ElementInfo)
			{
				XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
				this.ExportExtraElementAttributes(codeAttributeDeclarationCollection, xmlTypeMapElementInfo, defaultNamespace, xmlTypeMapElementInfo.TypeData);
			}
			if (codeAttributeDeclarationCollection.Count > 0)
			{
				codeTypeMember.CustomAttributes = codeAttributeDeclarationCollection;
			}
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0007B0B0 File Offset: 0x000792B0
		private bool DefinedInBaseMap(XmlTypeMapping map, XmlTypeMapMember member)
		{
			return ((ClassMap)map.ObjectMap).FindMember(member.Name) != null || (map.BaseMap != null && this.DefinedInBaseMap(map.BaseMap, member));
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0007B0F4 File Offset: 0x000792F4
		private void AddArrayElementFieldMember(CodeTypeDeclaration codeClass, XmlTypeMapMemberList member, string defaultNamespace)
		{
			CodeTypeMember codeTypeMember = this.CreateFieldMember(codeClass, member.TypeData, member.Name);
			CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection();
			this.AddArrayAttributes(codeAttributeDeclarationCollection, member, defaultNamespace, false);
			ListMap listMap = (ListMap)member.ListTypeMapping.ObjectMap;
			this.AddArrayItemAttributes(codeAttributeDeclarationCollection, listMap, member.TypeData.ListItemTypeData, defaultNamespace, 0);
			if (codeAttributeDeclarationCollection.Count > 0)
			{
				codeTypeMember.CustomAttributes = codeAttributeDeclarationCollection;
			}
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0007B160 File Offset: 0x00079360
		public void AddArrayAttributes(CodeAttributeDeclarationCollection attributes, XmlTypeMapMemberElement member, string defaultNamespace, bool forceUseMemberName)
		{
			this.GenerateArrayElement(attributes, member, defaultNamespace, forceUseMemberName);
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x0007B170 File Offset: 0x00079370
		public void AddArrayItemAttributes(CodeAttributeDeclarationCollection attributes, ListMap listMap, TypeData type, string defaultNamespace, int nestingLevel)
		{
			foreach (object obj in listMap.ItemInfo)
			{
				XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
				string defaultName;
				if (xmlTypeMapElementInfo.MappedType != null)
				{
					defaultName = xmlTypeMapElementInfo.MappedType.ElementName;
				}
				else
				{
					defaultName = xmlTypeMapElementInfo.TypeData.XmlType;
				}
				this.GenerateArrayItemAttributes(attributes, listMap, type, xmlTypeMapElementInfo, defaultName, defaultNamespace, nestingLevel);
				if (xmlTypeMapElementInfo.MappedType != null)
				{
					if (!this.IsMapExported(xmlTypeMapElementInfo.MappedType) && this.includeArrayTypes)
					{
						this.AddInclude(xmlTypeMapElementInfo.MappedType);
					}
					this.ExportMapCode(xmlTypeMapElementInfo.MappedType, false);
				}
			}
			if (listMap.IsMultiArray)
			{
				XmlTypeMapping nestedArrayMapping = listMap.NestedArrayMapping;
				this.AddArrayItemAttributes(attributes, (ListMap)nestedArrayMapping.ObjectMap, nestedArrayMapping.TypeData.ListItemTypeData, defaultNamespace, nestingLevel + 1);
			}
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0007B288 File Offset: 0x00079488
		private void ExportArrayCode(XmlTypeMapping map)
		{
			ListMap listMap = (ListMap)map.ObjectMap;
			foreach (object obj in listMap.ItemInfo)
			{
				XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
				if (xmlTypeMapElementInfo.MappedType != null)
				{
					if (!this.IsMapExported(xmlTypeMapElementInfo.MappedType) && this.includeArrayTypes)
					{
						this.AddInclude(xmlTypeMapElementInfo.MappedType);
					}
					this.ExportMapCode(xmlTypeMapElementInfo.MappedType, false);
				}
			}
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0007B33C File Offset: 0x0007953C
		private bool ExportExtraElementAttributes(CodeAttributeDeclarationCollection attributes, XmlTypeMapElementInfo einfo, string defaultNamespace, TypeData defaultType)
		{
			if (einfo.IsTextElement)
			{
				this.GenerateTextElementAttribute(attributes, einfo, defaultType);
				return true;
			}
			if (einfo.IsUnnamedAnyElement)
			{
				this.GenerateUnnamedAnyElementAttribute(attributes, einfo, defaultNamespace);
				return true;
			}
			return false;
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0007B378 File Offset: 0x00079578
		private void ExportEnumCode(XmlTypeMapping map, bool isTopLevel)
		{
			if (this.IsMapExported(map))
			{
				return;
			}
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(map.TypeData.TypeName);
			this.SetMapExported(map, codeTypeDeclaration);
			codeTypeDeclaration.Attributes = MemberAttributes.Public;
			codeTypeDeclaration.IsEnum = true;
			this.AddCodeType(codeTypeDeclaration, map.Documentation);
			EnumMap enumMap = (EnumMap)map.ObjectMap;
			if (enumMap.IsFlags)
			{
				codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration("System.FlagsAttribute"));
			}
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(GeneratedCodeAttribute)));
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression("System.Xml")));
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression("2.0.50727.1433")));
			codeTypeDeclaration.CustomAttributes.Add(codeAttributeDeclaration);
			codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(SerializableAttribute))));
			this.GenerateEnum(map, codeTypeDeclaration, isTopLevel);
			int num = 1;
			foreach (EnumMap.EnumMapMember enumMapMember in enumMap.Members)
			{
				CodeMemberField codeMemberField = new CodeMemberField(string.Empty, enumMapMember.EnumName);
				if (enumMap.IsFlags)
				{
					codeMemberField.InitExpression = new CodePrimitiveExpression(num);
					num *= 2;
				}
				MapCodeGenerator.AddComments(codeMemberField, enumMapMember.Documentation);
				this.GenerateEnumItem(codeMemberField, enumMapMember);
				codeTypeDeclaration.Members.Add(codeMemberField);
			}
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0007B4FC File Offset: 0x000796FC
		private void AddInclude(XmlTypeMapping map)
		{
			if (!this.includeMaps.ContainsKey(map.TypeData.FullTypeName))
			{
				this.includeMaps[map.TypeData.FullTypeName] = map;
			}
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0007B53C File Offset: 0x0007973C
		private void RemoveInclude(XmlTypeMapping map)
		{
			this.includeMaps.Remove(map.TypeData.FullTypeName);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0007B554 File Offset: 0x00079754
		private bool IsMapExported(XmlTypeMapping map)
		{
			return this.exportedMaps.Contains(map.TypeData.FullTypeName);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0007B574 File Offset: 0x00079774
		private void SetMapExported(XmlTypeMapping map, CodeTypeDeclaration declaration)
		{
			this.exportedMaps.Add(map.TypeData.FullTypeName, declaration);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0007B590 File Offset: 0x00079790
		private CodeTypeDeclaration GetMapDeclaration(XmlTypeMapping map)
		{
			return this.exportedMaps[map.TypeData.FullTypeName] as CodeTypeDeclaration;
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0007B5B0 File Offset: 0x000797B0
		public static void AddCustomAttribute(CodeTypeMember ctm, CodeAttributeDeclaration att, bool addIfNoParams)
		{
			if (att.Arguments.Count == 0 && !addIfNoParams)
			{
				return;
			}
			if (ctm.CustomAttributes == null)
			{
				ctm.CustomAttributes = new CodeAttributeDeclarationCollection();
			}
			ctm.CustomAttributes.Add(att);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0007B5F8 File Offset: 0x000797F8
		public static void AddCustomAttribute(CodeTypeMember ctm, string name, params CodeAttributeArgument[] args)
		{
			if (ctm.CustomAttributes == null)
			{
				ctm.CustomAttributes = new CodeAttributeDeclarationCollection();
			}
			ctm.CustomAttributes.Add(new CodeAttributeDeclaration(name, args));
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0007B630 File Offset: 0x00079830
		public static CodeAttributeArgument GetArg(string name, object value)
		{
			return new CodeAttributeArgument(name, new CodePrimitiveExpression(value));
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0007B640 File Offset: 0x00079840
		public static CodeAttributeArgument GetArg(object value)
		{
			return new CodeAttributeArgument(new CodePrimitiveExpression(value));
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0007B650 File Offset: 0x00079850
		public static CodeAttributeArgument GetTypeArg(string name, string typeName)
		{
			return new CodeAttributeArgument(name, new CodeTypeOfExpression(typeName));
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0007B660 File Offset: 0x00079860
		public static CodeAttributeArgument GetEnumArg(string name, string enumType, string enumValue)
		{
			return new CodeAttributeArgument(name, new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(enumType), enumValue));
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0007B674 File Offset: 0x00079874
		public static void AddComments(CodeTypeMember member, string comments)
		{
			if (comments == null || comments == string.Empty)
			{
				member.Comments.Add(new CodeCommentStatement("<remarks/>", true));
			}
			else
			{
				member.Comments.Add(new CodeCommentStatement("<remarks>\n" + comments + "\n</remarks>", true));
			}
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0007B6D8 File Offset: 0x000798D8
		private void AddCodeType(CodeTypeDeclaration type, string comments)
		{
			MapCodeGenerator.AddComments(type, comments);
			this.codeNamespace.Types.Add(type);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0007B6F4 File Offset: 0x000798F4
		private void AddClassAttributes(CodeTypeDeclaration codeClass)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(GeneratedCodeAttribute)));
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression("System.Xml")));
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression("2.0.50727.1433")));
			codeClass.CustomAttributes.Add(codeAttributeDeclaration);
			codeClass.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(SerializableAttribute))));
			codeClass.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(DebuggerStepThroughAttribute))));
			CodeAttributeDeclaration codeAttributeDeclaration2 = new CodeAttributeDeclaration(new CodeTypeReference(typeof(DesignerCategoryAttribute)));
			codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression("code")));
			codeClass.CustomAttributes.Add(codeAttributeDeclaration2);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0007B7D8 File Offset: 0x000799D8
		private CodeTypeReference GetDomType(TypeData data, bool requiresNullable)
		{
			if (data.IsValueType && (data.IsNullable || requiresNullable))
			{
				return new CodeTypeReference("System.Nullable", new CodeTypeReference[]
				{
					new CodeTypeReference(data.FullTypeName)
				});
			}
			if (data.SchemaType == SchemaTypes.Array)
			{
				return new CodeTypeReference(this.GetDomType(data.ListItemTypeData, false), 1);
			}
			return new CodeTypeReference(data.FullTypeName);
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x0007B84C File Offset: 0x00079A4C
		private CodeDomProvider CodeProvider
		{
			get
			{
				if (this.codeProvider == null)
				{
					this.codeProvider = new CSharpCodeProvider();
				}
				return this.codeProvider;
			}
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0007B86C File Offset: 0x00079A6C
		protected virtual void GenerateClass(XmlTypeMapping map, CodeTypeDeclaration codeClass, bool isTopLevel)
		{
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0007B870 File Offset: 0x00079A70
		protected virtual void GenerateClassInclude(CodeAttributeDeclarationCollection attributes, XmlTypeMapping map)
		{
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0007B874 File Offset: 0x00079A74
		protected virtual void GenerateAnyAttribute(CodeTypeMember codeField)
		{
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0007B878 File Offset: 0x00079A78
		protected virtual void GenerateDefaultAttribute(CodeMemberField internalField, CodeTypeMember externalField, TypeData typeData, object defaultValue)
		{
			if (typeData.Type == null)
			{
				if (typeData.SchemaType != SchemaTypes.Enum)
				{
					throw new InvalidOperationException("Type " + typeData.TypeName + " not supported");
				}
				IFormattable formattable = defaultValue as IFormattable;
				CodeFieldReferenceExpression value = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(this.GetDomType(typeData, false)), (formattable == null) ? defaultValue.ToString() : formattable.ToString(null, CultureInfo.InvariantCulture));
				CodeAttributeArgument codeAttributeArgument = new CodeAttributeArgument(value);
				MapCodeGenerator.AddCustomAttribute(externalField, "System.ComponentModel.DefaultValue", new CodeAttributeArgument[]
				{
					codeAttributeArgument
				});
			}
			else
			{
				MapCodeGenerator.AddCustomAttribute(externalField, "System.ComponentModel.DefaultValue", new CodeAttributeArgument[]
				{
					MapCodeGenerator.GetArg(defaultValue)
				});
			}
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0007B92C File Offset: 0x00079B2C
		protected virtual void GenerateAttributeMember(CodeAttributeDeclarationCollection attributes, XmlTypeMapMemberAttribute attinfo, string defaultNamespace, bool forceUseMemberName)
		{
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0007B930 File Offset: 0x00079B30
		protected virtual void GenerateElementInfoMember(CodeAttributeDeclarationCollection attributes, XmlTypeMapMemberElement member, XmlTypeMapElementInfo einfo, TypeData defaultType, string defaultNamespace, bool addAlwaysAttr, bool forceUseMemberName)
		{
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0007B934 File Offset: 0x00079B34
		protected virtual void GenerateElementMember(CodeAttributeDeclarationCollection attributes, XmlTypeMapMemberElement member)
		{
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x0007B938 File Offset: 0x00079B38
		protected virtual void GenerateArrayElement(CodeAttributeDeclarationCollection attributes, XmlTypeMapMemberElement member, string defaultNamespace, bool forceUseMemberName)
		{
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x0007B93C File Offset: 0x00079B3C
		protected virtual void GenerateArrayItemAttributes(CodeAttributeDeclarationCollection attributes, ListMap listMap, TypeData type, XmlTypeMapElementInfo ainfo, string defaultName, string defaultNamespace, int nestingLevel)
		{
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0007B940 File Offset: 0x00079B40
		protected virtual void GenerateTextElementAttribute(CodeAttributeDeclarationCollection attributes, XmlTypeMapElementInfo einfo, TypeData defaultType)
		{
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0007B944 File Offset: 0x00079B44
		protected virtual void GenerateUnnamedAnyElementAttribute(CodeAttributeDeclarationCollection attributes, XmlTypeMapElementInfo einfo, string defaultNamespace)
		{
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0007B948 File Offset: 0x00079B48
		protected virtual void GenerateEnum(XmlTypeMapping map, CodeTypeDeclaration codeEnum, bool isTopLevel)
		{
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0007B94C File Offset: 0x00079B4C
		protected virtual void GenerateEnumItem(CodeMemberField codeField, EnumMap.EnumMapMember emem)
		{
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x0007B950 File Offset: 0x00079B50
		protected virtual void GenerateSpecifierMember(CodeTypeMember codeField)
		{
		}

		// Token: 0x04000A0F RID: 2575
		private CodeNamespace codeNamespace;

		// Token: 0x04000A10 RID: 2576
		private CodeCompileUnit codeCompileUnit;

		// Token: 0x04000A11 RID: 2577
		private CodeAttributeDeclarationCollection includeMetadata;

		// Token: 0x04000A12 RID: 2578
		private XmlTypeMapping exportedAnyType;

		// Token: 0x04000A13 RID: 2579
		protected bool includeArrayTypes;

		// Token: 0x04000A14 RID: 2580
		private CodeDomProvider codeProvider;

		// Token: 0x04000A15 RID: 2581
		private CodeGenerationOptions options;

		// Token: 0x04000A16 RID: 2582
		private CodeIdentifiers identifiers;

		// Token: 0x04000A17 RID: 2583
		private Hashtable exportedMaps = new Hashtable();

		// Token: 0x04000A18 RID: 2584
		private Hashtable includeMaps = new Hashtable();
	}
}
