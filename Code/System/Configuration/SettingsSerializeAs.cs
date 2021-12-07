using System;

namespace System.Configuration
{
	/// <summary>Determines the serialization scheme used to store application settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001FE RID: 510
	public enum SettingsSerializeAs
	{
		/// <summary>The settings property is serialized as plain text.</summary>
		// Token: 0x040004F7 RID: 1271
		String,
		/// <summary>The settings property is serialized as XML using XML serialization.</summary>
		// Token: 0x040004F8 RID: 1272
		Xml,
		/// <summary>The settings property is serialized using binary object serialization.</summary>
		// Token: 0x040004F9 RID: 1273
		Binary,
		/// <summary>The settings provider has implicit knowledge of the property or its type and picks an appropriate serialization mechanism. Often used for custom serialization.</summary>
		// Token: 0x040004FA RID: 1274
		ProviderSpecific
	}
}
