﻿using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Notifies one or more waiting threads that an event has occurred. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006A4 RID: 1700
	[ComVisible(true)]
	public sealed class ManualResetEvent : EventWaitHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ManualResetEvent" /> class with a Boolean value indicating whether to set the initial state to signaled.</summary>
		/// <param name="initialState">true to set the initial state to signaled; false to set the initial state to nonsignaled. </param>
		// Token: 0x060040AC RID: 16556 RVA: 0x000DF484 File Offset: 0x000DD684
		public ManualResetEvent(bool initialState) : base(initialState, EventResetMode.ManualReset)
		{
		}
	}
}
