using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.TYPELIBATTR" /> instead.</summary>
	// Token: 0x020003C2 RID: 962
	[Obsolete]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPELIBATTR
	{
		/// <summary>Represents a globally unique library ID of a type library.</summary>
		// Token: 0x040011CC RID: 4556
		public Guid guid;

		/// <summary>Represents a locale ID of a type library.</summary>
		// Token: 0x040011CD RID: 4557
		public int lcid;

		/// <summary>Represents the target hardware platform of a type library.</summary>
		// Token: 0x040011CE RID: 4558
		public SYSKIND syskind;

		/// <summary>Represents the major version number of a type library.</summary>
		// Token: 0x040011CF RID: 4559
		public short wMajorVerNum;

		/// <summary>Represents the minor version number of a type library.</summary>
		// Token: 0x040011D0 RID: 4560
		public short wMinorVerNum;

		/// <summary>Represents library flags.</summary>
		// Token: 0x040011D1 RID: 4561
		public LIBFLAGS wLibFlags;
	}
}
