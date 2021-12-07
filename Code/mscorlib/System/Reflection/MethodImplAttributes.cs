using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies flags for the attributes of a method implementation.</summary>
	// Token: 0x0200029C RID: 668
	[ComVisible(true)]
	[Serializable]
	public enum MethodImplAttributes
	{
		/// <summary>Specifies flags about code type.</summary>
		// Token: 0x04000CB4 RID: 3252
		CodeTypeMask = 3,
		/// <summary>Specifies that the method implementation is in Microsoft intermediate language (MSIL).</summary>
		// Token: 0x04000CB5 RID: 3253
		IL = 0,
		/// <summary>Specifies that the method implementation is native.</summary>
		// Token: 0x04000CB6 RID: 3254
		Native,
		/// <summary>Specifies that the method implementation is in Optimized Intermediate Language (OPTIL).</summary>
		// Token: 0x04000CB7 RID: 3255
		OPTIL,
		/// <summary>Specifies that the method implementation is provided by the runtime.</summary>
		// Token: 0x04000CB8 RID: 3256
		Runtime,
		/// <summary>Specifies whether the method is implemented in managed or unmanaged code.</summary>
		// Token: 0x04000CB9 RID: 3257
		ManagedMask,
		/// <summary>Specifies that the method is implemented in unmanaged code.</summary>
		// Token: 0x04000CBA RID: 3258
		Unmanaged = 4,
		/// <summary>Specifies that the method is implemented in managed code. </summary>
		// Token: 0x04000CBB RID: 3259
		Managed = 0,
		/// <summary>Specifies that the method is not defined.</summary>
		// Token: 0x04000CBC RID: 3260
		ForwardRef = 16,
		/// <summary>Specifies that the method signature is exported exactly as declared.</summary>
		// Token: 0x04000CBD RID: 3261
		PreserveSig = 128,
		/// <summary>Specifies an internal call.</summary>
		// Token: 0x04000CBE RID: 3262
		InternalCall = 4096,
		/// <summary>Specifies that the method is single-threaded through the body. Static methods (Shared in Visual Basic) lock on the type, whereas instance methods lock on the instance. You can also use the C# lock statement or the Visual Basic Lock function for this purpose. </summary>
		// Token: 0x04000CBF RID: 3263
		Synchronized = 32,
		/// <summary>Specifies that the method cannot be inlined.</summary>
		// Token: 0x04000CC0 RID: 3264
		NoInlining = 8,
		/// <summary>Specifies a range check value.</summary>
		// Token: 0x04000CC1 RID: 3265
		MaxMethodImplVal = 65535
	}
}
