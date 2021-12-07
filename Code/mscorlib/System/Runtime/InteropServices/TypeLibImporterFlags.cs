using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how an assembly should be produced.</summary>
	// Token: 0x020003C8 RID: 968
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibImporterFlags
	{
		/// <summary>Generates a primary interop assembly. See <see cref="T:System.Runtime.InteropServices.PrimaryInteropAssemblyAttribute" /> for details. A keyfile must be specified.</summary>
		// Token: 0x040011EA RID: 4586
		PrimaryInteropAssembly = 1,
		/// <summary>Imports all interfaces as interfaces that suppress the common language runtime's stack crawl for <see cref="F:System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode" /> permission. Be sure you understand the responsibilities associated with suppressing this security check.</summary>
		// Token: 0x040011EB RID: 4587
		UnsafeInterfaces = 2,
		/// <summary>Imports all SAFEARRAYs as <see cref="T:System.Array" /> rather than a typed, single dimensional, zero-based managed array. This option is useful when dealing with multi dimensional, non zero-based SAFEARRAYs which otherwise can not be accessed unless you edit the resulting assembly using the ILDASM and ILASM tools.</summary>
		// Token: 0x040011EC RID: 4588
		SafeArrayAsSystemArray = 4,
		/// <summary>Transforms [out, retval] parameters of methods on dispatch-only interfaces (dispinterfaces) into return values.</summary>
		// Token: 0x040011ED RID: 4589
		TransformDispRetVals = 8,
		/// <summary>Specifies no flags. This is the default.</summary>
		// Token: 0x040011EE RID: 4590
		None = 0,
		/// <summary>Not used.</summary>
		// Token: 0x040011EF RID: 4591
		PreventClassMembers = 16,
		/// <summary>Imports a type library for any platform.</summary>
		// Token: 0x040011F0 RID: 4592
		ImportAsAgnostic = 2048,
		/// <summary>Imports a type library for the Itanuim platform.</summary>
		// Token: 0x040011F1 RID: 4593
		ImportAsItanium = 1024,
		/// <summary>Imports a type library for the X86 64 bit platform.</summary>
		// Token: 0x040011F2 RID: 4594
		ImportAsX64 = 512,
		/// <summary>Imports a type library for the X86 platform.</summary>
		// Token: 0x040011F3 RID: 4595
		ImportAsX86 = 256,
		/// <summary>Specifies the use of reflection only loading.</summary>
		// Token: 0x040011F4 RID: 4596
		ReflectionOnlyLoading = 4096,
		/// <summary>Specifies the use of serailizable classes.</summary>
		// Token: 0x040011F5 RID: 4597
		SerializableValueClasses = 32
	}
}
