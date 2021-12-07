using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.PARAMFLAG" /> instead.</summary>
	// Token: 0x020003AF RID: 943
	[Obsolete]
	[Flags]
	[Serializable]
	public enum PARAMFLAG
	{
		/// <summary>Whether the parameter passes or receives information is unspecified.</summary>
		// Token: 0x04001164 RID: 4452
		PARAMFLAG_NONE = 0,
		/// <summary>The parameter passes information from the caller to the callee.</summary>
		// Token: 0x04001165 RID: 4453
		PARAMFLAG_FIN = 1,
		/// <summary>The parameter returns information from the callee to the caller.</summary>
		// Token: 0x04001166 RID: 4454
		PARAMFLAG_FOUT = 2,
		/// <summary>The parameter is the local identifier of a client application.</summary>
		// Token: 0x04001167 RID: 4455
		PARAMFLAG_FLCID = 4,
		/// <summary>The parameter is the return value of the member.</summary>
		// Token: 0x04001168 RID: 4456
		PARAMFLAG_FRETVAL = 8,
		/// <summary>The parameter is optional.</summary>
		// Token: 0x04001169 RID: 4457
		PARAMFLAG_FOPT = 16,
		/// <summary>The parameter has default behaviors defined.</summary>
		// Token: 0x0400116A RID: 4458
		PARAMFLAG_FHASDEFAULT = 32,
		/// <summary>The parameter has custom data.</summary>
		// Token: 0x0400116B RID: 4459
		PARAMFLAG_FHASCUSTDATA = 64
	}
}
