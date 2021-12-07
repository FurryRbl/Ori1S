using System;

namespace System.Xml
{
	/// <summary>Represents the XML type for the string. This allows the string to be read as a particular XML type, for example a CDATA section type.</summary>
	// Token: 0x02000126 RID: 294
	public enum XmlTokenizedType
	{
		/// <summary>CDATA type.</summary>
		// Token: 0x04000609 RID: 1545
		CDATA,
		/// <summary>ID type.</summary>
		// Token: 0x0400060A RID: 1546
		ID,
		/// <summary>IDREF type.</summary>
		// Token: 0x0400060B RID: 1547
		IDREF,
		/// <summary>IDREFS type.</summary>
		// Token: 0x0400060C RID: 1548
		IDREFS,
		/// <summary>ENTITY type.</summary>
		// Token: 0x0400060D RID: 1549
		ENTITY,
		/// <summary>ENTITIES type.</summary>
		// Token: 0x0400060E RID: 1550
		ENTITIES,
		/// <summary>NMTOKEN type.</summary>
		// Token: 0x0400060F RID: 1551
		NMTOKEN,
		/// <summary>NMTOKENS type.</summary>
		// Token: 0x04000610 RID: 1552
		NMTOKENS,
		/// <summary>NOTATION type.</summary>
		// Token: 0x04000611 RID: 1553
		NOTATION,
		/// <summary>ENUMERATION type.</summary>
		// Token: 0x04000612 RID: 1554
		ENUMERATION,
		/// <summary>QName type.</summary>
		// Token: 0x04000613 RID: 1555
		QName,
		/// <summary>NCName type.</summary>
		// Token: 0x04000614 RID: 1556
		NCName,
		/// <summary>No type.</summary>
		// Token: 0x04000615 RID: 1557
		None
	}
}
