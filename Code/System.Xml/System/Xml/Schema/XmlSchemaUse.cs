using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Indicator of how the attribute is used.</summary>
	// Token: 0x02000245 RID: 581
	public enum XmlSchemaUse
	{
		/// <summary>Attribute use not specified.</summary>
		// Token: 0x0400097F RID: 2431
		[XmlIgnore]
		None,
		/// <summary>Attribute is optional.</summary>
		// Token: 0x04000980 RID: 2432
		[XmlEnum("optional")]
		Optional,
		/// <summary>Attribute cannot be used.</summary>
		// Token: 0x04000981 RID: 2433
		[XmlEnum("prohibited")]
		Prohibited,
		/// <summary>Attribute must appear once.</summary>
		// Token: 0x04000982 RID: 2434
		[XmlEnum("required")]
		Required
	}
}
