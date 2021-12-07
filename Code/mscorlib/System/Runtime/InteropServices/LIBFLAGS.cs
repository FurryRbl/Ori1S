using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.LIBFLAGS" /> instead.</summary>
	// Token: 0x020003AA RID: 938
	[Obsolete]
	[Flags]
	[Serializable]
	public enum LIBFLAGS
	{
		/// <summary>The type library is restricted, and should not be displayed to users.</summary>
		// Token: 0x04001156 RID: 4438
		LIBFLAG_FRESTRICTED = 1,
		/// <summary>The type library describes controls, and should not be displayed in type browsers intended for nonvisual objects.</summary>
		// Token: 0x04001157 RID: 4439
		LIBFLAG_FCONTROL = 2,
		/// <summary>The type library should not be displayed to users, although its use is not restricted. Should be used by controls. Hosts should create a new type library that wraps the control with extended properties.</summary>
		// Token: 0x04001158 RID: 4440
		LIBFLAG_FHIDDEN = 4,
		/// <summary>The type library exists in a persisted form on disk.</summary>
		// Token: 0x04001159 RID: 4441
		LIBFLAG_FHASDISKIMAGE = 8
	}
}
