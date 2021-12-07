using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines the valid calling conventions for a method.</summary>
	// Token: 0x02000285 RID: 645
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum CallingConventions
	{
		/// <summary>Specifies the default calling convention as determined by the common language runtime. Use this calling convention for static methods. For instance or virtual methods use HasThis.</summary>
		// Token: 0x04000C39 RID: 3129
		Standard = 1,
		/// <summary>Specifies the calling convention for methods with variable arguments.</summary>
		// Token: 0x04000C3A RID: 3130
		VarArgs = 2,
		/// <summary>Specifies that either the Standard or the VarArgs calling convention may be used.</summary>
		// Token: 0x04000C3B RID: 3131
		Any = 3,
		/// <summary>Specifies an instance or virtual method (not a static method). At run-time, the called method is passed a pointer to the target object as its first argument (the this pointer). The signature stored in metadata does not include the type of this first argument, because the method is known and its owner class can be discovered from metadata.</summary>
		// Token: 0x04000C3C RID: 3132
		HasThis = 32,
		/// <summary>Specifies that the signature is a function-pointer signature, representing a call to an instance or virtual method (not a static method). If ExplicitThis is set, HasThis must also be set. The first argument passed to the called method is still a this pointer, but the type of the first argument is now unknown. Therefore, a token that describes the type (or class) of the this pointer is explicitly stored into its metadata signature.</summary>
		// Token: 0x04000C3D RID: 3133
		ExplicitThis = 64
	}
}
