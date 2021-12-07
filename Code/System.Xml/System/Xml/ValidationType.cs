using System;

namespace System.Xml
{
	/// <summary>Specifies the type of validation to perform.</summary>
	// Token: 0x020000E3 RID: 227
	public enum ValidationType
	{
		/// <summary>No validation is performed. This setting creates an XML 1.0 compliant non-validating parser.</summary>
		// Token: 0x0400048D RID: 1165
		None,
		/// <summary>Validates if DTD or schema information is found.</summary>
		// Token: 0x0400048E RID: 1166
		[Obsolete]
		Auto,
		/// <summary>Validates according to the DTD.</summary>
		// Token: 0x0400048F RID: 1167
		DTD,
		/// <summary>Validate according to XML-Data Reduced (XDR) schemas, including inline XDR schemas. XDR schemas are recognized using the x-schema namespace prefix or the <see cref="P:System.Xml.XmlValidatingReader.Schemas" /> property.</summary>
		// Token: 0x04000490 RID: 1168
		[Obsolete]
		XDR,
		/// <summary>Validate according to XML Schema definition language (XSD) schemas, including inline XML Schemas. XML Schemas are associated with namespace URIs either by using the schemaLocation attribute or the provided Schemas property.</summary>
		// Token: 0x04000491 RID: 1169
		Schema
	}
}
