using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the mode of a cryptographic stream.</summary>
	// Token: 0x020005A1 RID: 1441
	[ComVisible(true)]
	[Serializable]
	public enum CryptoStreamMode
	{
		/// <summary>Read access to a cryptographic stream.</summary>
		// Token: 0x04001819 RID: 6169
		Read,
		/// <summary>Write access to a cryptographic stream.</summary>
		// Token: 0x0400181A RID: 6170
		Write
	}
}
