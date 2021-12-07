using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies flags for method attributes. These flags are defined in the corhdr.h file.</summary>
	// Token: 0x02000299 RID: 665
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum MethodAttributes
	{
		/// <summary>Retrieves accessibility information.</summary>
		// Token: 0x04000C95 RID: 3221
		MemberAccessMask = 7,
		/// <summary>Indicates that the member cannot be referenced.</summary>
		// Token: 0x04000C96 RID: 3222
		PrivateScope = 0,
		/// <summary>Indicates that the method is accessible only to the current class.</summary>
		// Token: 0x04000C97 RID: 3223
		Private = 1,
		/// <summary>Indicates that the method is accessible to members of this type and its derived types that are in this assembly only.</summary>
		// Token: 0x04000C98 RID: 3224
		FamANDAssem = 2,
		/// <summary>Indicates that the method is accessible to any class of this assembly.</summary>
		// Token: 0x04000C99 RID: 3225
		Assembly = 3,
		/// <summary>Indicates that the method is accessible only to members of this class and its derived classes.</summary>
		// Token: 0x04000C9A RID: 3226
		Family = 4,
		/// <summary>Indicates that the method is accessible to derived classes anywhere, as well as to any class in the assembly.</summary>
		// Token: 0x04000C9B RID: 3227
		FamORAssem = 5,
		/// <summary>Indicates that the method is accessible to any object for which this object is in scope.</summary>
		// Token: 0x04000C9C RID: 3228
		Public = 6,
		/// <summary>Indicates that the method is defined on the type; otherwise, it is defined per instance.</summary>
		// Token: 0x04000C9D RID: 3229
		Static = 16,
		/// <summary>Indicates that the method cannot be overridden.</summary>
		// Token: 0x04000C9E RID: 3230
		Final = 32,
		/// <summary>Indicates that the method is virtual.</summary>
		// Token: 0x04000C9F RID: 3231
		Virtual = 64,
		/// <summary>Indicates that the method hides by name and signature; otherwise, by name only.</summary>
		// Token: 0x04000CA0 RID: 3232
		HideBySig = 128,
		/// <summary>Retrieves vtable attributes.</summary>
		// Token: 0x04000CA1 RID: 3233
		VtableLayoutMask = 256,
		/// <summary>Indicates that the method can only be overridden when it is also accessible.</summary>
		// Token: 0x04000CA2 RID: 3234
		CheckAccessOnOverride = 512,
		/// <summary>Indicates that the method will reuse an existing slot in the vtable. This is the default behavior.</summary>
		// Token: 0x04000CA3 RID: 3235
		ReuseSlot = 0,
		/// <summary>Indicates that the method always gets a new slot in the vtable.</summary>
		// Token: 0x04000CA4 RID: 3236
		NewSlot = 256,
		/// <summary>Indicates that the class does not provide an implementation of this method.</summary>
		// Token: 0x04000CA5 RID: 3237
		Abstract = 1024,
		/// <summary>Indicates that the method is special. The name describes how this method is special.</summary>
		// Token: 0x04000CA6 RID: 3238
		SpecialName = 2048,
		/// <summary>Indicates that the method implementation is forwarded through PInvoke (Platform Invocation Services).</summary>
		// Token: 0x04000CA7 RID: 3239
		PinvokeImpl = 8192,
		/// <summary>Indicates that the managed method is exported by thunk to unmanaged code.</summary>
		// Token: 0x04000CA8 RID: 3240
		UnmanagedExport = 8,
		/// <summary>Indicates that the common language runtime checks the name encoding.</summary>
		// Token: 0x04000CA9 RID: 3241
		RTSpecialName = 4096,
		/// <summary>Indicates a reserved flag for runtime use only.</summary>
		// Token: 0x04000CAA RID: 3242
		ReservedMask = 53248,
		/// <summary>Indicates that the method has security associated with it. Reserved flag for runtime use only.</summary>
		// Token: 0x04000CAB RID: 3243
		HasSecurity = 16384,
		/// <summary>Indicates that the method calls another method containing security code. Reserved flag for runtime use only.</summary>
		// Token: 0x04000CAC RID: 3244
		RequireSecObject = 32768
	}
}
