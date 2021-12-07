using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000070 RID: 112
	internal interface IPipe
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060005BC RID: 1468
		SafePipeHandle Handle { get; }

		// Token: 0x060005BD RID: 1469
		void WaitForPipeDrain();
	}
}
