using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Generates types and attribute declarations from internal type mapping information for XML schema element declarations.</summary>
	// Token: 0x02000287 RID: 647
	public class XmlCodeExporter : CodeExporter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlCodeExporter" /> class using the specified namespace. </summary>
		/// <param name="codeNamespace">The namespace of the types to generate.</param>
		// Token: 0x06001A38 RID: 6712 RVA: 0x00088C74 File Offset: 0x00086E74
		public XmlCodeExporter(CodeNamespace codeNamespace) : this(codeNamespace, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlCodeExporter" /> class using the specified namespace and code compile unit.</summary>
		/// <param name="codeNamespace">The namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A CodeDOM graph container to which used assembly references are automatically added.</param>
		// Token: 0x06001A39 RID: 6713 RVA: 0x00088C80 File Offset: 0x00086E80
		public XmlCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit)
		{
			this.codeGenerator = new XmlMapCodeGenerator(codeNamespace, codeCompileUnit, CodeGenerationOptions.GenerateProperties);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlCodeExporter" /> class using the specified namespace, code compile unit, and code generation options.</summary>
		/// <param name="codeNamespace">The namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> program graph container to which used assembly references are automatically added.</param>
		/// <param name="options">An enumeration value that provides options for generating .NET Framework types from XML schema custom data types.</param>
		// Token: 0x06001A3A RID: 6714 RVA: 0x00088C98 File Offset: 0x00086E98
		public XmlCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeGenerationOptions options) : this(codeNamespace, codeCompileUnit, null, options, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlCodeExporter" /> class using the specified .NET Framework namespace, code compile unit containing the graph of the objects, an object representing code generation options, and a collection of mapping objects.</summary>
		/// <param name="codeNamespace">The namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> program graph container to which used assembly references are automatically added.</param>
		/// <param name="options">An enumeration value that provides options for generating .NET Framework types from XML schema custom data types.</param>
		/// <param name="mappings">A <see cref="T:System.Collections.Hashtable" /> that contains <see cref="T:System.Xml.Serialization.XmlMapping" /> objects.</param>
		// Token: 0x06001A3B RID: 6715 RVA: 0x00088CA8 File Offset: 0x00086EA8
		public XmlCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeGenerationOptions options, Hashtable mappings) : this(codeNamespace, codeCompileUnit, null, options, mappings)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlCodeExporter" /> class using the specified .NET Framework namespace, code compile unit containing the graph of the objects, an enumeration specifying code options, and a collection of mapping objects.</summary>
		/// <param name="codeNamespace">The namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" />  program graph container to which used assembly references are automatically added.</param>
		/// <param name="codeProvider">An enumeration value that provides options for generating .NET Framework types from XML schema custom data types.</param>
		/// <param name="options">A <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> that contains special instructions for code creation.</param>
		/// <param name="mappings">A <see cref="T:System.Collections.Hashtable" /> that contains <see cref="T:System.Xml.Serialization.XmlMapping" /> objects.</param>
		// Token: 0x06001A3C RID: 6716 RVA: 0x00088CB8 File Offset: 0x00086EB8
		[MonoTODO]
		public XmlCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeDomProvider codeProvider, CodeGenerationOptions options, Hashtable mappings)
		{
			this.codeGenerator = new XmlMapCodeGenerator(codeNamespace, codeCompileUnit, codeProvider, options, mappings);
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> declaration to a method parameter or return value that corresponds to a &lt;part&gt; element of a SOAP message definition in a Web Services Description Language (WSDL) document. </summary>
		/// <param name="metadata">The collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects for the generated type to which the method adds an attribute declaration.</param>
		/// <param name="member">An internal .NET Framework type mapping for a single element part of a WSDL message definition.</param>
		/// <param name="ns">The XML namespace of the SOAP message part for which the type mapping information in the member parameter has been generated.</param>
		// Token: 0x06001A3D RID: 6717 RVA: 0x00088CE0 File Offset: 0x00086EE0
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlMemberMapping member, string ns)
		{
			this.AddMappingMetadata(metadata, member, ns, false);
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> declaration to a method return value that corresponds to a &lt;part&gt; element of a non-SOAP message definition in a Web Services Description Language (WSDL) document. </summary>
		/// <param name="metadata">The collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects for the generated type to which the method adds an attribute declaration.</param>
		/// <param name="mapping">The internal .NET Framework type mapping information for an XML schema element.</param>
		/// <param name="ns">The XML namespace of the SOAP message part for which the type mapping information in the member parameter has been generated.</param>
		// Token: 0x06001A3E RID: 6718 RVA: 0x00088CEC File Offset: 0x00086EEC
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlTypeMapping member, string ns)
		{
			if ((member.TypeData.SchemaType == SchemaTypes.Primitive || member.TypeData.SchemaType == SchemaTypes.Array) && member.Namespace != "http://www.w3.org/2001/XMLSchema")
			{
				metadata.Add(new CodeAttributeDeclaration("System.Xml.Serialization.XmlRoot")
				{
					Arguments = 
					{
						MapCodeGenerator.GetArg(member.ElementName),
						MapCodeGenerator.GetArg("Namespace", member.Namespace),
						MapCodeGenerator.GetArg("IsNullable", member.IsNullable)
					}
				});
			}
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> declaration to a method parameter or return value that corresponds to a &lt;part&gt; element of a SOAP message definition in a Web Services Description Language (WSDL) document. </summary>
		/// <param name="metadata">The collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects for the generated type to which the method adds an attribute declaration.</param>
		/// <param name="member">An internal .NET Framework type mapping for a single element part of a WSDL message definition.</param>
		/// <param name="ns">The XML namespace of the SOAP message part for which the type mapping information in the member parameter has been generated.</param>
		/// <param name="forceUseMemberName">Flag that helps determine whether to add an initial argument containing the XML element name for the attribute declaration being generated.</param>
		// Token: 0x06001A3F RID: 6719 RVA: 0x00088D98 File Offset: 0x00086F98
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlMemberMapping member, string ns, bool forceUseMemberName)
		{
			TypeData typeData = member.TypeMapMember.TypeData;
			if (member.Any)
			{
				XmlTypeMapElementInfoList elementInfo = ((XmlTypeMapMemberElement)member.TypeMapMember).ElementInfo;
				foreach (object obj in elementInfo)
				{
					XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
					if (xmlTypeMapElementInfo.IsTextElement)
					{
						metadata.Add(new CodeAttributeDeclaration("System.Xml.Serialization.XmlText"));
					}
					else
					{
						CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Xml.Serialization.XmlAnyElement");
						if (!xmlTypeMapElementInfo.IsUnnamedAnyElement)
						{
							codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("Name", xmlTypeMapElementInfo.ElementName));
							if (xmlTypeMapElementInfo.Namespace != ns)
							{
								codeAttributeDeclaration.Arguments.Add(MapCodeGenerator.GetArg("Namespace", member.Namespace));
							}
						}
						metadata.Add(codeAttributeDeclaration);
					}
				}
			}
			else if (member.TypeMapMember is XmlTypeMapMemberList)
			{
				XmlTypeMapMemberList xmlTypeMapMemberList = member.TypeMapMember as XmlTypeMapMemberList;
				ListMap listMap = (ListMap)xmlTypeMapMemberList.ListTypeMapping.ObjectMap;
				this.codeGenerator.AddArrayAttributes(metadata, xmlTypeMapMemberList, ns, forceUseMemberName);
				this.codeGenerator.AddArrayItemAttributes(metadata, listMap, typeData.ListItemTypeData, xmlTypeMapMemberList.Namespace, 0);
			}
			else if (member.TypeMapMember is XmlTypeMapMemberElement)
			{
				this.codeGenerator.AddElementMemberAttributes((XmlTypeMapMemberElement)member.TypeMapMember, ns, metadata, forceUseMemberName);
			}
			else
			{
				if (!(member.TypeMapMember is XmlTypeMapMemberAttribute))
				{
					throw new NotSupportedException("Schema type not supported");
				}
				this.codeGenerator.AddAttributeMemberAttributes((XmlTypeMapMemberAttribute)member.TypeMapMember, ns, metadata, forceUseMemberName);
			}
		}

		/// <summary>Generates a .NET Framework type, plus attribute declarations, for each of the parts that belong to a SOAP message definition in a Web Services Description Language (WSDL) document. </summary>
		/// <param name="xmlMembersMapping">The internal .NET Framework type mappings for the element parts of a WSDL message definition.</param>
		// Token: 0x06001A40 RID: 6720 RVA: 0x00088F84 File Offset: 0x00087184
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping)
		{
			this.codeGenerator.ExportMembersMapping(xmlMembersMapping);
		}

		/// <summary>Generates a .NET Framework type, plus attribute declarations, for an XML schema element. </summary>
		/// <param name="xmlTypeMapping">The internal .NET Framework type mapping information for an XML schema element.</param>
		// Token: 0x06001A41 RID: 6721 RVA: 0x00088F94 File Offset: 0x00087194
		public void ExportTypeMapping(XmlTypeMapping xmlTypeMapping)
		{
			this.codeGenerator.ExportTypeMapping(xmlTypeMapping, true);
		}
	}
}
