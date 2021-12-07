using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.FUNCKIND" /> instead.</summary>
	// Token: 0x02000392 RID: 914
	[Obsolete]
	[Serializable]
	public enum FUNCKIND
	{
		/// <summary>The function is accessed the same as <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_PUREVIRTUAL" />, except the function has an implementation.</summary>
		// Token: 0x04001128 RID: 4392
		FUNC_VIRTUAL,
		/// <summary>The function is accessed through the virtual function table (VTBL), and takes an implicit this pointer.</summary>
		// Token: 0x04001129 RID: 4393
		FUNC_PUREVIRTUAL,
		/// <summary>The function is accessed by static address and takes an implicit this pointer.</summary>
		// Token: 0x0400112A RID: 4394
		FUNC_NONVIRTUAL,
		/// <summary>The function is accessed by static address and does not take an implicit this pointer.</summary>
		// Token: 0x0400112B RID: 4395
		FUNC_STATIC,
		/// <summary>The function can be accessed only through IDispatch.</summary>
		// Token: 0x0400112C RID: 4396
		FUNC_DISPATCH
	}
}
