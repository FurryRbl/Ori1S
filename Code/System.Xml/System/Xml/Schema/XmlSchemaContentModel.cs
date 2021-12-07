using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Specifies the order and structure of the child elements of a type.</summary>
	// Token: 0x02000209 RID: 521
	public abstract class XmlSchemaContentModel : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the content of the type.</summary>
		/// <returns>Provides the content of the type.</returns>
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060014EF RID: 5359
		// (set) Token: 0x060014F0 RID: 5360
		[XmlIgnore]
		public abstract XmlSchemaContent Content { get; set; }
	}
}
