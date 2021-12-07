using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Provides different methods for preventing derivation.</summary>
	// Token: 0x0200020E RID: 526
	[Flags]
	public enum XmlSchemaDerivationMethod
	{
		/// <summary>Override default derivation method to allow any derivation.</summary>
		// Token: 0x04000845 RID: 2117
		[XmlEnum("")]
		Empty = 0,
		/// <summary>Refers to derivations by Substitution.</summary>
		// Token: 0x04000846 RID: 2118
		[XmlEnum("substitution")]
		Substitution = 1,
		/// <summary>Refers to derivations by Extension.</summary>
		// Token: 0x04000847 RID: 2119
		[XmlEnum("extension")]
		Extension = 2,
		/// <summary>Refers to derivations by Restriction.</summary>
		// Token: 0x04000848 RID: 2120
		[XmlEnum("restriction")]
		Restriction = 4,
		/// <summary>Refers to derivations by List.</summary>
		// Token: 0x04000849 RID: 2121
		[XmlEnum("list")]
		List = 8,
		/// <summary>Refers to derivations by Union.</summary>
		// Token: 0x0400084A RID: 2122
		[XmlEnum("union")]
		Union = 16,
		/// <summary>#all. Refers to all derivation methods.</summary>
		// Token: 0x0400084B RID: 2123
		[XmlEnum("#all")]
		All = 255,
		/// <summary>Accepts the default derivation method.</summary>
		// Token: 0x0400084C RID: 2124
		[XmlIgnore]
		None = 256
	}
}
