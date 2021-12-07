using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents the method that executes on a <see cref="T:System.Threading.Thread" />.</summary>
	/// <param name="obj">An object that contains data for the thread procedure.</param>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006FA RID: 1786
	// (Invoke) Token: 0x060043DC RID: 17372
	[ComVisible(false)]
	public delegate void ParameterizedThreadStart(object obj);
}
