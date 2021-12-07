using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes how to transfer a structure element, parameter, or function return value between processes.</summary>
	// Token: 0x020003F6 RID: 1014
	[Flags]
	[Serializable]
	public enum IDLFLAG
	{
		/// <summary>Does not specify whether the parameter passes or receives information.</summary>
		// Token: 0x040012D5 RID: 4821
		IDLFLAG_NONE = 0,
		/// <summary>The parameter passes information from the caller to the callee.</summary>
		// Token: 0x040012D6 RID: 4822
		IDLFLAG_FIN = 1,
		/// <summary>The parameter returns information from the callee to the caller.</summary>
		// Token: 0x040012D7 RID: 4823
		IDLFLAG_FOUT = 2,
		/// <summary>The parameter is the local identifier of a client application.</summary>
		// Token: 0x040012D8 RID: 4824
		IDLFLAG_FLCID = 4,
		/// <summary>The parameter is the return value of the member.</summary>
		// Token: 0x040012D9 RID: 4825
		IDLFLAG_FRETVAL = 8
	}
}
