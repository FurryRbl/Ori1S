using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.TYPEDESC" /> instead.</summary>
	// Token: 0x020003BF RID: 959
	[Obsolete]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEDESC
	{
		/// <summary>If the variable is VT_SAFEARRAY or VT_PTR, the <see cref="F:System.Runtime.InteropServices.TYPEDESC.lpValue" /> field contains a pointer to a TYPEDESC that specifies the element type.</summary>
		// Token: 0x040011B0 RID: 4528
		public IntPtr lpValue;

		/// <summary>Indicates the variant type for the item described by this TYPEDESC.</summary>
		// Token: 0x040011B1 RID: 4529
		public short vt;
	}
}
