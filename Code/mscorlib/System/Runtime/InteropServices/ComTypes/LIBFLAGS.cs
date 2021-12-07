using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines flags that apply to type libraries.</summary>
	// Token: 0x02000407 RID: 1031
	[Flags]
	[Serializable]
	public enum LIBFLAGS
	{
		/// <summary>The type library is restricted, and should not be displayed to users.</summary>
		// Token: 0x040012E5 RID: 4837
		LIBFLAG_FRESTRICTED = 1,
		/// <summary>The type library describes controls and should not be displayed in type browsers intended for nonvisual objects.</summary>
		// Token: 0x040012E6 RID: 4838
		LIBFLAG_FCONTROL = 2,
		/// <summary>The type library should not be displayed to users, although its use is not restricted. The type library should be used by controls. Hosts should create a new type library that wraps the control with extended properties.</summary>
		// Token: 0x040012E7 RID: 4839
		LIBFLAG_FHIDDEN = 4,
		/// <summary>The type library exists in a persisted form on disk.</summary>
		// Token: 0x040012E8 RID: 4840
		LIBFLAG_FHASDISKIMAGE = 8
	}
}
