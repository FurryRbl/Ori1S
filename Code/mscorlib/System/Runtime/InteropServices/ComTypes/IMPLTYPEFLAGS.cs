using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines the attributes of an implemented or inherited interface of a type.</summary>
	// Token: 0x020003FD RID: 1021
	[Flags]
	[Serializable]
	public enum IMPLTYPEFLAGS
	{
		/// <summary>The interface or dispinterface represents the default for the source or sink.</summary>
		// Token: 0x040012DB RID: 4827
		IMPLTYPEFLAG_FDEFAULT = 1,
		/// <summary>This member of a coclass is called rather than implemented.</summary>
		// Token: 0x040012DC RID: 4828
		IMPLTYPEFLAG_FSOURCE = 2,
		/// <summary>The member should not be displayed or programmable by users.</summary>
		// Token: 0x040012DD RID: 4829
		IMPLTYPEFLAG_FRESTRICTED = 4,
		/// <summary>Sinks receive events through the virtual function table (VTBL).</summary>
		// Token: 0x040012DE RID: 4830
		IMPLTYPEFLAG_FDEFAULTVTABLE = 8
	}
}
