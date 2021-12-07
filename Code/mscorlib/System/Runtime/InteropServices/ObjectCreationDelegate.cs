using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Creates a COM object.</summary>
	/// <returns>An <see cref="T:System.IntPtr" /> object that represents the IUnknown interface of the COM object.</returns>
	/// <param name="aggregator">A pointer to the managed object's IUnknown interface. </param>
	// Token: 0x020006F4 RID: 1780
	// (Invoke) Token: 0x060043C4 RID: 17348
	[ComVisible(true)]
	public delegate IntPtr ObjectCreationDelegate(IntPtr aggregator);
}
