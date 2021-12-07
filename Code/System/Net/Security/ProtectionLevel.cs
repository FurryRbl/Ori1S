using System;

namespace System.Net.Security
{
	/// <summary>Indicates the security services requested for an authenticated stream.</summary>
	// Token: 0x020003E0 RID: 992
	public enum ProtectionLevel
	{
		/// <summary>Authentication only.</summary>
		// Token: 0x040014FC RID: 5372
		None,
		/// <summary>Sign data to help ensure the integrity of transmitted data.</summary>
		// Token: 0x040014FD RID: 5373
		Sign,
		/// <summary>Encrypt and sign data to help ensure the confidentiality and integrity of transmitted data.</summary>
		// Token: 0x040014FE RID: 5374
		EncryptAndSign
	}
}
