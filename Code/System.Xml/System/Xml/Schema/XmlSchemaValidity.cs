using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the validity of an XML item validated by the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> class.</summary>
	// Token: 0x02000248 RID: 584
	public enum XmlSchemaValidity
	{
		/// <summary>The validity of the XML item is not known.</summary>
		// Token: 0x040009A4 RID: 2468
		NotKnown,
		/// <summary>The XML item is valid.</summary>
		// Token: 0x040009A5 RID: 2469
		Valid,
		/// <summary>The XML item is invalid.</summary>
		// Token: 0x040009A6 RID: 2470
		Invalid
	}
}
