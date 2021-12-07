using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies whether white space should be ignored in the base 64 transformation.</summary>
	// Token: 0x020005AE RID: 1454
	[ComVisible(true)]
	[Serializable]
	public enum FromBase64TransformMode
	{
		/// <summary>White space should be ignored.</summary>
		// Token: 0x04001850 RID: 6224
		IgnoreWhiteSpaces,
		/// <summary>White space should not be ignored.</summary>
		// Token: 0x04001851 RID: 6225
		DoNotIgnoreWhiteSpaces
	}
}
