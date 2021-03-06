using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Provides information about the validation mode of any and anyAttribute element replacements.</summary>
	// Token: 0x0200020A RID: 522
	public enum XmlSchemaContentProcessing
	{
		/// <summary>Document items are not validated.</summary>
		// Token: 0x04000800 RID: 2048
		[XmlIgnore]
		None,
		/// <summary>Document items must consist of well-formed XML and are not validated by the schema.</summary>
		// Token: 0x04000801 RID: 2049
		[XmlEnum("skip")]
		Skip,
		/// <summary>If the associated schema is found, the document items will be validated. No errors will be thrown otherwise.</summary>
		// Token: 0x04000802 RID: 2050
		[XmlEnum("lax")]
		Lax,
		/// <summary>The schema processor must find a schema associated with the indicated namespace to validate the document items.</summary>
		// Token: 0x04000803 RID: 2051
		[XmlEnum("strict")]
		Strict
	}
}
