﻿using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml.Schema;

namespace System.Xml.Serialization.Advanced
{
	/// <summary>Allows you to customize the code generated from a Web Services Description Language (WSDL) document when using automated query tools.</summary>
	// Token: 0x020002CC RID: 716
	public abstract class SchemaImporterExtension
	{
		/// <summary>Handles the importation of the &lt;xsd:any&gt; elements in the schema.</summary>
		/// <returns>The name of the CLR type that the element maps to. </returns>
		/// <param name="any">An <see cref="T:System.Xml.Schema.XmlSchemaAny" /> that represents the xsd:any element found in the XML Schema Document (XSD).</param>
		/// <param name="mixed">A <see cref="T:System.Boolean" /> that indicates whether the XSD complex attribute has been set to "mixed". true, if the attribute has been set to mixed, otherwise false. </param>
		/// <param name="schemas">An <see cref="T:System.Xml.Serialization.XmlSchemas" /> that contains the collection of schemas found in the XSD.</param>
		/// <param name="importer">The <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> that is the importer being used.</param>
		/// <param name="compileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> to which you can add CodeDOM structures to generate alternative code for the XSD. </param>
		/// <param name="mainNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> that represents the current namespace for the element.</param>
		/// <param name="options">A <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> for the setting options on the code compiler.</param>
		/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> that is the CodeDOM provider used to generate the new code. </param>
		// Token: 0x06001E1D RID: 7709 RVA: 0x0009D8D0 File Offset: 0x0009BAD0
		public virtual string ImportAnyElement(XmlSchemaAny any, bool mixed, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit codeCompileUnit, CodeNamespace codeNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			return null;
		}

		/// <summary>Allows you to specify the default value for the XSD type being imported.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> setting the new default value.</returns>
		/// <param name="value">The value found in the original XSD.</param>
		/// <param name="type">The XSD type name.</param>
		// Token: 0x06001E1E RID: 7710 RVA: 0x0009D8D4 File Offset: 0x0009BAD4
		public virtual CodeExpression ImportDefaultValue(string value, string type)
		{
			return null;
		}

		/// <summary>Allows you to manipulate the code generated by examining the imported schema and specifying the CLR type that it maps to.</summary>
		/// <returns>The name of the CLR type that this maps to.</returns>
		/// <param name="type">An <see cref="T:System.Xml.Schema.XmlSchemaType" /> that represents the XSD type.</param>
		/// <param name="context">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that represents schema information, such as the line number of the XML element.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Serialization.XmlSchemas" /> that contains the collection of schemas in the document.</param>
		/// <param name="importer">The <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> that is the importer being used.</param>
		/// <param name="compileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> to which you can add CodeDOM structures to generate alternative code for the XSD.</param>
		/// <param name="mainNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> that represents the current namespace for the element.</param>
		/// <param name="options">A <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> for the setting options on the code compiler.</param>
		/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> that is used to generate the new code.</param>
		// Token: 0x06001E1F RID: 7711 RVA: 0x0009D8D8 File Offset: 0x0009BAD8
		public virtual string ImportSchemaType(XmlSchemaType type, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit codeCompileUnit, CodeNamespace codeNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			return null;
		}

		/// <summary>Allows you to manipulate the code generated by examining the imported schema and specifying the CLR type that it maps to.</summary>
		/// <returns>The name of the CLR type that this maps to.</returns>
		/// <param name="name">The name of the element.</param>
		/// <param name="ns">The namespace of the element.</param>
		/// <param name="context">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that represents schema information, such as the line number of the XML element.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Serialization.XmlSchemas" /> that contains the collection of schemas in the document.</param>
		/// <param name="importer">The <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> that is the importer being used.</param>
		/// <param name="compileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> to which you can add CodeDOM structures to generate alternative code for the XSD.</param>
		/// <param name="mainNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> that represents the current namespace for the element.</param>
		/// <param name="options">A <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> for the setting options on the code compiler.</param>
		/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> that is used to generate the new code.</param>
		// Token: 0x06001E20 RID: 7712 RVA: 0x0009D8DC File Offset: 0x0009BADC
		public virtual string ImportSchemaType(string name, string ns, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit codeCompileUnit, CodeNamespace codeNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			return null;
		}
	}
}