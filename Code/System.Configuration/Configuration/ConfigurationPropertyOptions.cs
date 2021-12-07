using System;

namespace System.Configuration
{
	/// <summary>Specifies the options to apply to a property.</summary>
	// Token: 0x02000033 RID: 51
	[Flags]
	public enum ConfigurationPropertyOptions
	{
		/// <summary>Indicates that no option applies to the property.</summary>
		// Token: 0x040000A1 RID: 161
		None = 0,
		/// <summary>Indicates that the property is a default collection. </summary>
		// Token: 0x040000A2 RID: 162
		IsDefaultCollection = 1,
		/// <summary>Indicates that the property is required. </summary>
		// Token: 0x040000A3 RID: 163
		IsRequired = 2,
		/// <summary>Indicates that the property is a collection key.</summary>
		// Token: 0x040000A4 RID: 164
		IsKey = 4
	}
}
