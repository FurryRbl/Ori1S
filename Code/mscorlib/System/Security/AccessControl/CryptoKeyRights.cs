using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the cryptographic key operation for which an authorization rule controls access or auditing.</summary>
	// Token: 0x0200056A RID: 1386
	[Flags]
	public enum CryptoKeyRights
	{
		/// <summary>Read the key data.</summary>
		// Token: 0x040016E3 RID: 5859
		ReadData = 1,
		/// <summary>Write key data.</summary>
		// Token: 0x040016E4 RID: 5860
		WriteData = 2,
		/// <summary>Read extended attributes of the key.</summary>
		// Token: 0x040016E5 RID: 5861
		ReadExtendedAttributes = 8,
		/// <summary>Write extended attributes of the key.</summary>
		// Token: 0x040016E6 RID: 5862
		WriteExtendedAttributes = 16,
		/// <summary>Read attributes of the key.</summary>
		// Token: 0x040016E7 RID: 5863
		ReadAttributes = 128,
		/// <summary>Write attributes of the key.</summary>
		// Token: 0x040016E8 RID: 5864
		WriteAttributes = 256,
		/// <summary>Delete the key.</summary>
		// Token: 0x040016E9 RID: 5865
		Delete = 65536,
		/// <summary>Read permissions for the key.</summary>
		// Token: 0x040016EA RID: 5866
		ReadPermissions = 131072,
		/// <summary>Change permissions for the key.</summary>
		// Token: 0x040016EB RID: 5867
		ChangePermissions = 262144,
		/// <summary>Take ownership of the key.</summary>
		// Token: 0x040016EC RID: 5868
		TakeOwnership = 524288,
		/// <summary>Use the key for synchronization.</summary>
		// Token: 0x040016ED RID: 5869
		Synchronize = 1048576,
		/// <summary>Full control of the key.</summary>
		// Token: 0x040016EE RID: 5870
		FullControl = 2032027,
		/// <summary>A combination of <see cref="F:System.Security.AccessControl.CryptoKeyRights.GenericRead" /> and <see cref="F:System.Security.AccessControl.CryptoKeyRights.GenericWrite" />.</summary>
		// Token: 0x040016EF RID: 5871
		GenericAll = 268435456,
		/// <summary>Not used.</summary>
		// Token: 0x040016F0 RID: 5872
		GenericExecute = 536870912,
		/// <summary>Write the key data, extended attributes of the key, attributes of the key, and permissions for the key.</summary>
		// Token: 0x040016F1 RID: 5873
		GenericWrite = 1073741824,
		/// <summary>Read the key data, extended attributes of the key, attributes of the key, and permissions for the key.</summary>
		// Token: 0x040016F2 RID: 5874
		GenericRead = -2147483648
	}
}
