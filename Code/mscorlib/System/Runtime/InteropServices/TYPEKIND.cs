using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.TYPEKIND" /> instead.</summary>
	// Token: 0x020003C1 RID: 961
	[Obsolete]
	[Serializable]
	public enum TYPEKIND
	{
		/// <summary>A set of enumerators.</summary>
		// Token: 0x040011C3 RID: 4547
		TKIND_ENUM,
		/// <summary>A structure with no methods.</summary>
		// Token: 0x040011C4 RID: 4548
		TKIND_RECORD,
		/// <summary>A module that can only have static functions and data (for example, a DLL).</summary>
		// Token: 0x040011C5 RID: 4549
		TKIND_MODULE,
		/// <summary>A type that has virtual functions, all of which are pure.</summary>
		// Token: 0x040011C6 RID: 4550
		TKIND_INTERFACE,
		/// <summary>A set of methods and properties that are accessible through IDispatch::Invoke. By default, dual interfaces return TKIND_DISPATCH.</summary>
		// Token: 0x040011C7 RID: 4551
		TKIND_DISPATCH,
		/// <summary>A set of implemented components interfaces.</summary>
		// Token: 0x040011C8 RID: 4552
		TKIND_COCLASS,
		/// <summary>A type that is an alias for another type.</summary>
		// Token: 0x040011C9 RID: 4553
		TKIND_ALIAS,
		/// <summary>A union of all members that have an offset of zero.</summary>
		// Token: 0x040011CA RID: 4554
		TKIND_UNION,
		/// <summary>End of enumeration marker.</summary>
		// Token: 0x040011CB RID: 4555
		TKIND_MAX
	}
}
