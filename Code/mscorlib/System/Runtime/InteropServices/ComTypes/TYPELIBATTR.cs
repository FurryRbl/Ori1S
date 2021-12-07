using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies a particular type library and provides localization support for member names.</summary>
	// Token: 0x02000410 RID: 1040
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPELIBATTR
	{
		/// <summary>Represents a globally unique library ID of a type library.</summary>
		// Token: 0x04001333 RID: 4915
		public Guid guid;

		/// <summary>Represents a locale ID of a type library.</summary>
		// Token: 0x04001334 RID: 4916
		public int lcid;

		/// <summary>Represents the target hardware platform of a type library.</summary>
		// Token: 0x04001335 RID: 4917
		public SYSKIND syskind;

		/// <summary>Represents the major version number of a type library.</summary>
		// Token: 0x04001336 RID: 4918
		public short wMajorVerNum;

		/// <summary>Represents the minor version number of a type library.</summary>
		// Token: 0x04001337 RID: 4919
		public short wMinorVerNum;

		/// <summary>Represents library flags.</summary>
		// Token: 0x04001338 RID: 4920
		public LIBFLAGS wLibFlags;
	}
}
