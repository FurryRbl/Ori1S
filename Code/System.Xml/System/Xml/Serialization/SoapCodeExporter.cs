using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Generates types and attribute declarations from internal type mapping information for SOAP-encoded message parts defined in a WSDL document. </summary>
	// Token: 0x0200026E RID: 622
	public class SoapCodeExporter : CodeExporter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapCodeExporter" /> class, assuming no code compile unit. </summary>
		/// <param name="codeNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies the namespace of the types to generate.</param>
		// Token: 0x0600192F RID: 6447 RVA: 0x00084BC8 File Offset: 0x00082DC8
		public SoapCodeExporter(CodeNamespace codeNamespace) : this(codeNamespace, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapCodeExporter" /> class, specifying a code compile unit parameter in addition to a namespace parameter.</summary>
		/// <param name="codeNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies the namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that identifies the program graph container to which used assembly references are automatically added.</param>
		// Token: 0x06001930 RID: 6448 RVA: 0x00084BD4 File Offset: 0x00082DD4
		public SoapCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit)
		{
			this.codeGenerator = new SoapMapCodeGenerator(codeNamespace, codeCompileUnit);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapCodeExporter" /> class, specifying a code namespace, a code compile unit, and code generation options.</summary>
		/// <param name="codeNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies the namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that identifies the program graph container to which used assembly references are automatically added.</param>
		/// <param name="options">A <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> enumeration that specifies the options with which exported code is generated.</param>
		// Token: 0x06001931 RID: 6449 RVA: 0x00084BEC File Offset: 0x00082DEC
		public SoapCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeGenerationOptions options) : this(codeNamespace, codeCompileUnit, null, options, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapCodeExporter" /> class, specifying a code namespace, a code compile unit, code generation options, and mappings.</summary>
		/// <param name="codeNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies the namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that identifies the program graph container to which used assembly references are automatically added.</param>
		/// <param name="options">A <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> enumeration that specifies the options with which exported code is generated.</param>
		/// <param name="mappings">A <see cref="T:System.Collections.Hashtable" /> that contains <see cref="T:System.Xml.Serialization.XmlMapping" /> objects.</param>
		// Token: 0x06001932 RID: 6450 RVA: 0x00084BFC File Offset: 0x00082DFC
		public SoapCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeGenerationOptions options, Hashtable mappings) : this(codeNamespace, codeCompileUnit, null, options, mappings)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapCodeExporter" /> class, specifying a code namespace, a code compile unit, a code generator, code generation options, and mappings.</summary>
		/// <param name="codeNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies the namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that identifies the program graph container to which used assembly references are automatically added.</param>
		/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" />  that is used to create the code.</param>
		/// <param name="options">A <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> enumeration that specifies the options with which exported code is generated.</param>
		/// <param name="mappings">A <see cref="T:System.Collections.Hashtable" /> that contains <see cref="T:System.Xml.Serialization.XmlMapping" /> objects.</param>
		// Token: 0x06001933 RID: 6451 RVA: 0x00084C0C File Offset: 0x00082E0C
		[MonoTODO]
		public SoapCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeDomProvider codeProvider, CodeGenerationOptions options, Hashtable mappings)
		{
			this.codeGenerator = new SoapMapCodeGenerator(codeNamespace, codeCompileUnit, codeProvider, options, mappings);
		}

		/// <summary>Add a <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> declaration to a method parameter or return value corresponding to a part element of a SOAP message definition in a WSDL document. </summary>
		/// <param name="metadata">The collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects for the generated type, to which the method adds an attribute declaration.</param>
		/// <param name="member">An internal .NET Framework type mapping for a single part of a WSDL message definition.</param>
		// Token: 0x06001934 RID: 6452 RVA: 0x00084C34 File Offset: 0x00082E34
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlMemberMapping member)
		{
			this.AddMappingMetadata(metadata, member, false);
		}

		/// <summary>Adds a <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> declaration to a method parameter or return value that corresponds to a part element of a SOAP message definition in a WSDL document. </summary>
		/// <param name="metadata">The collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects for the generated type to which the method adds an attribute declaration.</param>
		/// <param name="member">An internal .NET Framework type mapping for a single part of a WSDL message definition.</param>
		/// <param name="forceUseMemberName">true to add an initial argument that contains the XML element name for the attribute declaration that is being generated; otherwise, false.</param>
		// Token: 0x06001935 RID: 6453 RVA: 0x00084C40 File Offset: 0x00082E40
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlMemberMapping member, bool forceUseMemberName)
		{
			TypeData typeData = member.TypeMapMember.TypeData;
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Xml.Serialization.SoapElement");
			if (forceUseMemberName || member.ElementName != member.MemberName)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(member.ElementName)));
			}
			if (!TypeTranslator.IsDefaultPrimitiveTpeData(typeData))
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("DataType", new CodePrimitiveExpression(member.TypeName)));
			}
			if (codeAttributeDeclaration.Arguments.Count > 0)
			{
				metadata.Add(codeAttributeDeclaration);
			}
		}

		/// <summary>Generates a .NET Framework type, plus attribute declarations, for each of the parts that belong to a SOAP message definition in a WSDL document. </summary>
		/// <param name="xmlMembersMapping">Internal .NET Framework type mappings for the element parts of a WSDL message definition.</param>
		// Token: 0x06001936 RID: 6454 RVA: 0x00084CE4 File Offset: 0x00082EE4
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping)
		{
			this.codeGenerator.ExportMembersMapping(xmlMembersMapping);
		}

		/// <summary>Generates a .NET Framework type, plus attribute declarations, for a SOAP header. </summary>
		/// <param name="xmlTypeMapping">Internal .NET Framework type mapping information for a SOAP header element.</param>
		// Token: 0x06001937 RID: 6455 RVA: 0x00084CF4 File Offset: 0x00082EF4
		public void ExportTypeMapping(XmlTypeMapping xmlTypeMapping)
		{
			this.codeGenerator.ExportTypeMapping(xmlTypeMapping, true);
		}
	}
}
