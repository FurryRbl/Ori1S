using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the original settings of the <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> in the COM type library from which the type was imported.</summary>
	// Token: 0x020003CA RID: 970
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum TypeLibTypeFlags
	{
		/// <summary>A type description that describes an Application object.</summary>
		// Token: 0x040011F8 RID: 4600
		FAppObject = 1,
		/// <summary>Instances of the type can be created by ITypeInfo::CreateInstance.</summary>
		// Token: 0x040011F9 RID: 4601
		FCanCreate = 2,
		/// <summary>The type is licensed.</summary>
		// Token: 0x040011FA RID: 4602
		FLicensed = 4,
		/// <summary>The type is predefined. The client application should automatically create a single instance of the object that has this attribute. The name of the variable that points to the object is the same as the class name of the object.</summary>
		// Token: 0x040011FB RID: 4603
		FPreDeclId = 8,
		/// <summary>The type should not be displayed to browsers.</summary>
		// Token: 0x040011FC RID: 4604
		FHidden = 16,
		/// <summary>The type is a control from which other types will be derived, and should not be displayed to users.</summary>
		// Token: 0x040011FD RID: 4605
		FControl = 32,
		/// <summary>The interface supplies both IDispatch and V-table binding.</summary>
		// Token: 0x040011FE RID: 4606
		FDual = 64,
		/// <summary>The interface cannot add members at run time.</summary>
		// Token: 0x040011FF RID: 4607
		FNonExtensible = 128,
		/// <summary>The types used in the interface are fully compatible with Automation, including vtable binding support.</summary>
		// Token: 0x04001200 RID: 4608
		FOleAutomation = 256,
		/// <summary>This flag is intended for system-level types or types that type browsers should not display.</summary>
		// Token: 0x04001201 RID: 4609
		FRestricted = 512,
		/// <summary>The class supports aggregation.</summary>
		// Token: 0x04001202 RID: 4610
		FAggregatable = 1024,
		/// <summary>The object supports IConnectionPointWithDefault, and has default behaviors.</summary>
		// Token: 0x04001203 RID: 4611
		FReplaceable = 2048,
		/// <summary>Indicates that the interface derives from IDispatch, either directly or indirectly.</summary>
		// Token: 0x04001204 RID: 4612
		FDispatchable = 4096,
		/// <summary>Indicates base interfaces should be checked for name resolution before checking child interfaces. This is the reverse of the default behavior.</summary>
		// Token: 0x04001205 RID: 4613
		FReverseBind = 8192
	}
}
