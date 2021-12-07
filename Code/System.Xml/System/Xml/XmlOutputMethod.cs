using System;

namespace System.Xml
{
	/// <summary>Specifies the method used to serialize the <see cref="T:System.Xml.XmlWriter" /> output. </summary>
	// Token: 0x02000111 RID: 273
	public enum XmlOutputMethod
	{
		/// <summary>Serialize according to the XML 1.0 rules.</summary>
		// Token: 0x0400056C RID: 1388
		Xml,
		/// <summary>Serialize according to the HTML rules specified by XSLT.</summary>
		// Token: 0x0400056D RID: 1389
		Html,
		/// <summary>Serialize text blocks only.</summary>
		// Token: 0x0400056E RID: 1390
		Text,
		/// <summary>Use the XSLT rules to choose between the <see cref="F:System.Xml.XmlOutputMethod.Xml" /> and <see cref="F:System.Xml.XmlOutputMethod.Html" /> output methods at runtime.</summary>
		// Token: 0x0400056F RID: 1391
		AutoDetect
	}
}
