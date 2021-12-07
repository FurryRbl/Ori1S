using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeKeyChoice" /> enumeration defines the type of key used in a key agreement protocol.</summary>
	// Token: 0x02000018 RID: 24
	public enum KeyAgreeKeyChoice
	{
		/// <summary>The key agreement key type is unknown.</summary>
		// Token: 0x0400005C RID: 92
		Unknown,
		/// <summary>The key agreement key is ephemeral, existing only for the duration of the key agreement protocol.</summary>
		// Token: 0x0400005D RID: 93
		EphemeralKey,
		/// <summary>The key agreement key is static, existing for an extended period of time.</summary>
		// Token: 0x0400005E RID: 94
		StaticKey
	}
}
