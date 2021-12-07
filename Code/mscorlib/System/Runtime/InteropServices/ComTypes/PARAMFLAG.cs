using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes how to transfer a structure element, parameter, or function return value between processes.</summary>
	// Token: 0x02000409 RID: 1033
	[Flags]
	[Serializable]
	public enum PARAMFLAG
	{
		/// <summary>Does not specify whether the parameter passes or receives information.</summary>
		// Token: 0x040012EC RID: 4844
		PARAMFLAG_NONE = 0,
		/// <summary>The parameter passes information from the caller to the callee.</summary>
		// Token: 0x040012ED RID: 4845
		PARAMFLAG_FIN = 1,
		/// <summary>The parameter returns information from the callee to the caller.</summary>
		// Token: 0x040012EE RID: 4846
		PARAMFLAG_FOUT = 2,
		/// <summary>The parameter is the local identifier of a client application.</summary>
		// Token: 0x040012EF RID: 4847
		PARAMFLAG_FLCID = 4,
		/// <summary>The parameter is the return value of the member.</summary>
		// Token: 0x040012F0 RID: 4848
		PARAMFLAG_FRETVAL = 8,
		/// <summary>The parameter is optional.</summary>
		// Token: 0x040012F1 RID: 4849
		PARAMFLAG_FOPT = 16,
		/// <summary>The parameter has default behaviors defined.</summary>
		// Token: 0x040012F2 RID: 4850
		PARAMFLAG_FHASDEFAULT = 32,
		/// <summary>The parameter has custom data.</summary>
		// Token: 0x040012F3 RID: 4851
		PARAMFLAG_FHASCUSTDATA = 64
	}
}
