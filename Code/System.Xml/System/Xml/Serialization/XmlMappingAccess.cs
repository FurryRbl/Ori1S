using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies whether a mapping is read, write, or both.</summary>
	// Token: 0x02000297 RID: 663
	[Flags]
	public enum XmlMappingAccess
	{
		/// <summary>Both read and write methods are generated.</summary>
		// Token: 0x04000AFE RID: 2814
		None = 0,
		/// <summary>Read methods are generated.</summary>
		// Token: 0x04000AFF RID: 2815
		Read = 1,
		/// <summary>Write methods are generated.</summary>
		// Token: 0x04000B00 RID: 2816
		Write = 2
	}
}
