using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies various options to use when generating .NET Framework types for use with an XML Web Service.</summary>
	// Token: 0x02000254 RID: 596
	[Flags]
	public enum CodeGenerationOptions
	{
		/// <summary>Represents primitive types by fields and primitive types by <see cref="N:System" /> namespace types.</summary>
		// Token: 0x04000A00 RID: 2560
		[XmlIgnore]
		None = 0,
		/// <summary>Represents primitive types by properties.</summary>
		// Token: 0x04000A01 RID: 2561
		[XmlEnum("properties")]
		GenerateProperties = 1,
		/// <summary>Creates events for the asynchronous invocation of Web methods.</summary>
		// Token: 0x04000A02 RID: 2562
		[XmlEnum("newAsync")]
		GenerateNewAsync = 2,
		/// <summary>Creates Begin and End methods for the asynchronous invocation of Web methods.</summary>
		// Token: 0x04000A03 RID: 2563
		[XmlEnum("oldAsync")]
		GenerateOldAsync = 4,
		/// <summary>Generates explicitly ordered serialization code as specified through the Order property of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" />, <see cref="T:System.Xml.Serialization.XmlArrayAttribute" />, and <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> attributes. </summary>
		// Token: 0x04000A04 RID: 2564
		[XmlEnum("order")]
		GenerateOrder = 8,
		/// <summary>Enables data binding.</summary>
		// Token: 0x04000A05 RID: 2565
		[XmlEnum("enableDataBinding")]
		EnableDataBinding = 16
	}
}
