using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.DESCKIND" /> instead.</summary>
	// Token: 0x02000384 RID: 900
	[Obsolete]
	[Serializable]
	public enum DESCKIND
	{
		/// <summary>Indicates that no match was found.</summary>
		// Token: 0x040010ED RID: 4333
		DESCKIND_NONE,
		/// <summary>Indicates that a <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> was returned.</summary>
		// Token: 0x040010EE RID: 4334
		DESCKIND_FUNCDESC,
		/// <summary>Indicates that a VARDESC was returned.</summary>
		// Token: 0x040010EF RID: 4335
		DESCKIND_VARDESC,
		/// <summary>Indicates that a TYPECOMP was returned.</summary>
		// Token: 0x040010F0 RID: 4336
		DESCKIND_TYPECOMP,
		/// <summary>Indicates that an IMPLICITAPPOBJ was returned.</summary>
		// Token: 0x040010F1 RID: 4337
		DESCKIND_IMPLICITAPPOBJ,
		/// <summary>Indicates an end of enumeration marker.</summary>
		// Token: 0x040010F2 RID: 4338
		DESCKIND_MAX
	}
}
