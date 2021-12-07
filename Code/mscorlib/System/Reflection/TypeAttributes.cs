using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies type attributes.</summary>
	// Token: 0x020002BE RID: 702
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum TypeAttributes
	{
		/// <summary>Specifies type visibility information.</summary>
		// Token: 0x04000D44 RID: 3396
		VisibilityMask = 7,
		/// <summary>Specifies that the class is not public.</summary>
		// Token: 0x04000D45 RID: 3397
		NotPublic = 0,
		/// <summary>Specifies that the class is public.</summary>
		// Token: 0x04000D46 RID: 3398
		Public = 1,
		/// <summary>Specifies that the class is nested with public visibility.</summary>
		// Token: 0x04000D47 RID: 3399
		NestedPublic = 2,
		/// <summary>Specifies that the class is nested with private visibility.</summary>
		// Token: 0x04000D48 RID: 3400
		NestedPrivate = 3,
		/// <summary>Specifies that the class is nested with family visibility, and is thus accessible only by methods within its own type and any subtypes.</summary>
		// Token: 0x04000D49 RID: 3401
		NestedFamily = 4,
		/// <summary>Specifies that the class is nested with assembly visibility, and is thus accessible only by methods within its assembly.</summary>
		// Token: 0x04000D4A RID: 3402
		NestedAssembly = 5,
		/// <summary>Specifies that the class is nested with assembly and family visibility, and is thus accessible only by methods lying in the intersection of its family and assembly.</summary>
		// Token: 0x04000D4B RID: 3403
		NestedFamANDAssem = 6,
		/// <summary>Specifies that the class is nested with family or assembly visibility, and is thus accessible only by methods lying in the union of its family and assembly.</summary>
		// Token: 0x04000D4C RID: 3404
		NestedFamORAssem = 7,
		/// <summary>Specifies class layout information.</summary>
		// Token: 0x04000D4D RID: 3405
		LayoutMask = 24,
		/// <summary>Specifies that class fields are automatically laid out by the common language runtime.</summary>
		// Token: 0x04000D4E RID: 3406
		AutoLayout = 0,
		/// <summary>Specifies that class fields are laid out sequentially, in the order that the fields were emitted to the metadata.</summary>
		// Token: 0x04000D4F RID: 3407
		SequentialLayout = 8,
		/// <summary>Specifies that class fields are laid out at the specified offsets.</summary>
		// Token: 0x04000D50 RID: 3408
		ExplicitLayout = 16,
		/// <summary>Specifies class semantics information; the current class is contextful (else agile).</summary>
		// Token: 0x04000D51 RID: 3409
		ClassSemanticsMask = 32,
		/// <summary>Specifies that the type is a class.</summary>
		// Token: 0x04000D52 RID: 3410
		Class = 0,
		/// <summary>Specifies that the type is an interface.</summary>
		// Token: 0x04000D53 RID: 3411
		Interface = 32,
		/// <summary>Specifies that the type is abstract.</summary>
		// Token: 0x04000D54 RID: 3412
		Abstract = 128,
		/// <summary>Specifies that the class is concrete and cannot be extended.</summary>
		// Token: 0x04000D55 RID: 3413
		Sealed = 256,
		/// <summary>Specifies that the class is special in a way denoted by the name.</summary>
		// Token: 0x04000D56 RID: 3414
		SpecialName = 1024,
		/// <summary>Specifies that the class or interface is imported from another module.</summary>
		// Token: 0x04000D57 RID: 3415
		Import = 4096,
		/// <summary>Specifies that the class can be serialized.</summary>
		// Token: 0x04000D58 RID: 3416
		Serializable = 8192,
		/// <summary>Used to retrieve string information for native interoperability.</summary>
		// Token: 0x04000D59 RID: 3417
		StringFormatMask = 196608,
		/// <summary>LPTSTR is interpreted as ANSI.</summary>
		// Token: 0x04000D5A RID: 3418
		AnsiClass = 0,
		/// <summary>LPTSTR is interpreted as UNICODE.</summary>
		// Token: 0x04000D5B RID: 3419
		UnicodeClass = 65536,
		/// <summary>LPTSTR is interpreted automatically.</summary>
		// Token: 0x04000D5C RID: 3420
		AutoClass = 131072,
		/// <summary>Specifies that calling static methods of the type does not force the system to initialize the type.</summary>
		// Token: 0x04000D5D RID: 3421
		BeforeFieldInit = 1048576,
		/// <summary>Attributes reserved for runtime use.</summary>
		// Token: 0x04000D5E RID: 3422
		ReservedMask = 264192,
		/// <summary>Runtime should check name encoding.</summary>
		// Token: 0x04000D5F RID: 3423
		RTSpecialName = 2048,
		/// <summary>Type has security associate with it.</summary>
		// Token: 0x04000D60 RID: 3424
		HasSecurity = 262144,
		/// <summary>LPSTR is interpreted by some implementation-specific means, which includes the possibility of throwing a <see cref="T:System.NotSupportedException" />. Not used in the Microsoft implementation of the .NET Framework.</summary>
		// Token: 0x04000D61 RID: 3425
		CustomFormatClass = 196608,
		/// <summary>Used to retrieve non-standard encoding information for native interop. The meaning of the values of these 2 bits is unspecified. Not used in the Microsoft implementation of the .NET Framework.</summary>
		// Token: 0x04000D62 RID: 3426
		CustomFormatMask = 12582912
	}
}
