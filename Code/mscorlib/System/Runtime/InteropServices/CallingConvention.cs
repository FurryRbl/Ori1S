using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the calling convention required to call methods implemented in unmanaged code.</summary>
	// Token: 0x02000372 RID: 882
	[ComVisible(true)]
	[Serializable]
	public enum CallingConvention
	{
		/// <summary>This member is not actually a calling convention, but instead uses the default platform calling convention. For example, on Windows the default is <see cref="F:System.Runtime.InteropServices.CallingConvention.StdCall" /> and on Windows CE.NET it is <see cref="F:System.Runtime.InteropServices.CallingConvention.Cdecl" />.</summary>
		// Token: 0x040010C7 RID: 4295
		Winapi = 1,
		/// <summary>The caller cleans the stack. This enables calling functions with varargs, which makes it appropriate to use for methods that accept a variable number of parameters, such as Printf.</summary>
		// Token: 0x040010C8 RID: 4296
		Cdecl,
		/// <summary>The callee cleans the stack. This is the default convention for calling unmanaged functions with platform invoke.</summary>
		// Token: 0x040010C9 RID: 4297
		StdCall,
		/// <summary>The first parameter is the this pointer and is stored in register ECX. Other parameters are pushed on the stack. This calling convention is used to call methods on classes exported from an unmanaged DLL.</summary>
		// Token: 0x040010CA RID: 4298
		ThisCall,
		/// <summary>This calling convention is not supported.</summary>
		// Token: 0x040010CB RID: 4299
		FastCall
	}
}
