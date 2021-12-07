using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IDLFLAG" /> instead. </summary>
	// Token: 0x0200039C RID: 924
	[Obsolete]
	[Flags]
	[Serializable]
	public enum IDLFLAG
	{
		/// <summary>Whether the parameter passes or receives information is unspecified.</summary>
		// Token: 0x0400113D RID: 4413
		IDLFLAG_NONE = 0,
		/// <summary>The parameter passes information from the caller to the callee.</summary>
		// Token: 0x0400113E RID: 4414
		IDLFLAG_FIN = 1,
		/// <summary>The parameter returns information from the callee to the caller.</summary>
		// Token: 0x0400113F RID: 4415
		IDLFLAG_FOUT = 2,
		/// <summary>The parameter is the local identifier of a client application.</summary>
		// Token: 0x04001140 RID: 4416
		IDLFLAG_FLCID = 4,
		/// <summary>The parameter is the return value of the member.</summary>
		// Token: 0x04001141 RID: 4417
		IDLFLAG_FRETVAL = 8
	}
}
