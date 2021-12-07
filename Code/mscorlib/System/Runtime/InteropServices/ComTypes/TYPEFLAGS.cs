using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines the properties and attributes of a type description.</summary>
	// Token: 0x0200040E RID: 1038
	[Flags]
	[Serializable]
	public enum TYPEFLAGS
	{
		/// <summary>A type description that describes an Application object.</summary>
		// Token: 0x0400131A RID: 4890
		TYPEFLAG_FAPPOBJECT = 1,
		/// <summary>Instances of the type can be created by ITypeInfo::CreateInstance.</summary>
		// Token: 0x0400131B RID: 4891
		TYPEFLAG_FCANCREATE = 2,
		/// <summary>The type is licensed.</summary>
		// Token: 0x0400131C RID: 4892
		TYPEFLAG_FLICENSED = 4,
		/// <summary>The type is predefined. The client application should automatically create a single instance of the object that has this attribute. The name of the variable that points to the object is the same as the class name of the object.</summary>
		// Token: 0x0400131D RID: 4893
		TYPEFLAG_FPREDECLID = 8,
		/// <summary>The type should not be displayed to browsers.</summary>
		// Token: 0x0400131E RID: 4894
		TYPEFLAG_FHIDDEN = 16,
		/// <summary>The type is a control from which other types will be derived and should not be displayed to users.</summary>
		// Token: 0x0400131F RID: 4895
		TYPEFLAG_FCONTROL = 32,
		/// <summary>The interface supplies both IDispatch and VTBL binding.</summary>
		// Token: 0x04001320 RID: 4896
		TYPEFLAG_FDUAL = 64,
		/// <summary>The interface cannot add members at run time.</summary>
		// Token: 0x04001321 RID: 4897
		TYPEFLAG_FNONEXTENSIBLE = 128,
		/// <summary>The types used in the interface are fully compatible with Automation, including VTBL binding support. Setting dual on an interface sets both this flag and the  <see cref="F:System.Runtime.InteropServices.TYPEFLAGS.TYPEFLAG_FDUAL" />. This flag is not allowed on dispinterfaces.</summary>
		// Token: 0x04001322 RID: 4898
		TYPEFLAG_FOLEAUTOMATION = 256,
		/// <summary>Should not be accessible from macro languages. This flag is intended for system-level types or types that type browsers should not display.</summary>
		// Token: 0x04001323 RID: 4899
		TYPEFLAG_FRESTRICTED = 512,
		/// <summary>The class supports aggregation.</summary>
		// Token: 0x04001324 RID: 4900
		TYPEFLAG_FAGGREGATABLE = 1024,
		/// <summary>The object supports IConnectionPointWithDefault, and has default behaviors.</summary>
		// Token: 0x04001325 RID: 4901
		TYPEFLAG_FREPLACEABLE = 2048,
		/// <summary>Indicates that the interface derives from IDispatch, either directly or indirectly. This flag is computed; there is no Object Description Language for the flag.</summary>
		// Token: 0x04001326 RID: 4902
		TYPEFLAG_FDISPATCHABLE = 4096,
		/// <summary>Indicates base interfaces should be checked for name resolution before checking children, which is the reverse of the default behavior.</summary>
		// Token: 0x04001327 RID: 4903
		TYPEFLAG_FREVERSEBIND = 8192,
		/// <summary>Indicates that the interface will be using a proxy/stub dynamic link library. This flag specifies that the type library proxy should not be unregistered when the type library is unregistered.</summary>
		// Token: 0x04001328 RID: 4904
		TYPEFLAG_FPROXY = 16384
	}
}
