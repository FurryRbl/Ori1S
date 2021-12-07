using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies how to expose an interface to COM.</summary>
	// Token: 0x0200037C RID: 892
	[ComVisible(true)]
	[Serializable]
	public enum ComInterfaceType
	{
		/// <summary>Indicates that the interface is exposed to COM as a dual interface, which enables both early and late binding. <see cref="F:System.Runtime.InteropServices.ComInterfaceType.InterfaceIsDual" /> is the default value.</summary>
		// Token: 0x040010DF RID: 4319
		InterfaceIsDual,
		/// <summary>Indicates that an interface is exposed to COM as an IUnknown -derived interface, which enables only early binding.</summary>
		// Token: 0x040010E0 RID: 4320
		InterfaceIsIUnknown,
		/// <summary>Indicates that an interface is exposed to COM as a dispinterface, which enables late binding only.</summary>
		// Token: 0x040010E1 RID: 4321
		InterfaceIsIDispatch
	}
}
