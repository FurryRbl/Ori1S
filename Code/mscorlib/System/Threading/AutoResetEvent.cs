using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Notifies a waiting thread that an event has occurred. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200069A RID: 1690
	[ComVisible(true)]
	public sealed class AutoResetEvent : EventWaitHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AutoResetEvent" /> class with a Boolean value indicating whether to set the initial state to signaled.</summary>
		/// <param name="initialState">true to set the initial state to signaled; false to set the initial state to non-signaled. </param>
		// Token: 0x0600405D RID: 16477 RVA: 0x000DEBE4 File Offset: 0x000DCDE4
		public AutoResetEvent(bool initialState) : base(initialState, EventResetMode.AutoReset)
		{
		}
	}
}
