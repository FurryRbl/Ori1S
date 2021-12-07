using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines the details of how a method is implemented.</summary>
	// Token: 0x02000340 RID: 832
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum MethodImplOptions
	{
		/// <summary>Specifies that the method is implemented in unmanaged code.</summary>
		// Token: 0x04001095 RID: 4245
		Unmanaged = 4,
		/// <summary>Specifies that the method is declared, but its implementation is provided elsewhere.</summary>
		// Token: 0x04001096 RID: 4246
		ForwardRef = 16,
		/// <summary>Specifies an internal call. An internal call is a call to a method that is implemented within the common language runtime itself.</summary>
		// Token: 0x04001097 RID: 4247
		InternalCall = 4096,
		/// <summary>Specifies that the method can be executed by only one thread at a time.  Static methods lock on the type, whereas instance methods lock on the instance. Only one thread can execute in any of the instance functions, and only one thread can execute in any of a class's static functions.</summary>
		// Token: 0x04001098 RID: 4248
		Synchronized = 32,
		/// <summary>Specifies that the method cannot be inlined.</summary>
		// Token: 0x04001099 RID: 4249
		NoInlining = 8,
		/// <summary>Specifies that the method signature is exported exactly as declared.</summary>
		// Token: 0x0400109A RID: 4250
		PreserveSig = 128,
		/// <summary>Specifies that the method is not optimized by the just-in-time (JIT) compiler or by native code generation (see Ngen.exe) when debugging possible code generation problems.</summary>
		// Token: 0x0400109B RID: 4251
		NoOptimization = 64
	}
}
