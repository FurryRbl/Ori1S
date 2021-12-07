using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Receives the error code, number of bytes, and overlapped value type when an I/O operation completes on the thread pool.</summary>
	/// <param name="errorCode">The error code. </param>
	/// <param name="numBytes">The number of bytes that are transferred. </param>
	/// <param name="pOVERLAP">A <see cref="T:System.Threading.NativeOverlapped" /> representing an unmanaged pointer to the native overlapped value type. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006F9 RID: 1785
	// (Invoke) Token: 0x060043D8 RID: 17368
	[ComVisible(true)]
	[CLSCompliant(false)]
	public unsafe delegate void IOCompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* pOVERLAP);
}
