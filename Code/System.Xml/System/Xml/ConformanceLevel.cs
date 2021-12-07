using System;

namespace System.Xml
{
	/// <summary>Specifies the amount of input or output checking that the created <see cref="T:System.Xml.XmlReader" /> and <see cref="T:System.Xml.XmlWriter" /> objects perform.</summary>
	// Token: 0x020000B4 RID: 180
	public enum ConformanceLevel
	{
		/// <summary>The <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.Xml.XmlWriter" /> object automatically detects whether document or fragment checking should be performed, and does the appropriate checking. In the case where you are wrapping another <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.Xml.XmlWriter" /> object, the outer object does not do any additional conformance checking. Conformance checking is left up to the underlying object.</summary>
		// Token: 0x040003C9 RID: 969
		Auto,
		/// <summary>The XML data is a well-formed XML fragment.</summary>
		// Token: 0x040003CA RID: 970
		Fragment,
		/// <summary>The XML data is in conformance to the rules for a well-formed XML 1.0 document.</summary>
		// Token: 0x040003CB RID: 971
		Document
	}
}
