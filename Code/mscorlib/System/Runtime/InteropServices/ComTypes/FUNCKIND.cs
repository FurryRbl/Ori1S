using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines how to access a function.</summary>
	// Token: 0x020003F1 RID: 1009
	[Serializable]
	public enum FUNCKIND
	{
		/// <summary>The function is accessed in the same way as <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_PUREVIRTUAL" />, except the function has an implementation.</summary>
		// Token: 0x040012CD RID: 4813
		FUNC_VIRTUAL,
		/// <summary>The function is accessed through the virtual function table (VTBL), and takes an implicit this pointer.</summary>
		// Token: 0x040012CE RID: 4814
		FUNC_PUREVIRTUAL,
		/// <summary>The function is accessed by static address and takes an implicit this pointer.</summary>
		// Token: 0x040012CF RID: 4815
		FUNC_NONVIRTUAL,
		/// <summary>The function is accessed by static address and does not take an implicit this pointer.</summary>
		// Token: 0x040012D0 RID: 4816
		FUNC_STATIC,
		/// <summary>The function can be accessed only through IDispatch.</summary>
		// Token: 0x040012D1 RID: 4817
		FUNC_DISPATCH
	}
}
