using System;

namespace System.Xml.Schema
{
	/// <summary>A delegate used by the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> class to pass attribute, text, and white space values as a Common Language Runtime (CLR) type compatible with the XML Schema Definition Language (XSD) type of the attribute, text, or white space.</summary>
	/// <returns>An object containing the attribute, text, or white space value. The object is a CLR type that that corresponds to the XSD type of the attribute, text, or white space value.</returns>
	// Token: 0x020002D4 RID: 724
	// (Invoke) Token: 0x06001EA5 RID: 7845
	public delegate object XmlValueGetter();
}
