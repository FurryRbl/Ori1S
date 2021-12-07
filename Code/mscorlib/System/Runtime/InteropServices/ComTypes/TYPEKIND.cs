using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Specifies various types of data and functions.</summary>
	// Token: 0x0200040F RID: 1039
	[Serializable]
	public enum TYPEKIND
	{
		/// <summary>A set of enumerators.</summary>
		// Token: 0x0400132A RID: 4906
		TKIND_ENUM,
		/// <summary>A structure with no methods.</summary>
		// Token: 0x0400132B RID: 4907
		TKIND_RECORD,
		/// <summary>A module that can have only static functions and data (for example, a DLL).</summary>
		// Token: 0x0400132C RID: 4908
		TKIND_MODULE,
		/// <summary>A type that has virtual functions, all of which are pure.</summary>
		// Token: 0x0400132D RID: 4909
		TKIND_INTERFACE,
		/// <summary>A set of methods and properties that are accessible through IDispatch::Invoke. By default, dual interfaces return TKIND_DISPATCH.</summary>
		// Token: 0x0400132E RID: 4910
		TKIND_DISPATCH,
		/// <summary>A set of implemented components interfaces.</summary>
		// Token: 0x0400132F RID: 4911
		TKIND_COCLASS,
		/// <summary>A type that is an alias for another type.</summary>
		// Token: 0x04001330 RID: 4912
		TKIND_ALIAS,
		/// <summary>A union of all members that have an offset of zero.</summary>
		// Token: 0x04001331 RID: 4913
		TKIND_UNION,
		/// <summary>End-of-enumeration marker.</summary>
		// Token: 0x04001332 RID: 4914
		TKIND_MAX
	}
}
