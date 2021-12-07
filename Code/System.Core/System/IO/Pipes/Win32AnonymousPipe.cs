using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x0200007F RID: 127
	internal abstract class Win32AnonymousPipe : IPipe
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000625 RID: 1573
		public abstract SafePipeHandle Handle { get; }

		// Token: 0x06000626 RID: 1574 RVA: 0x00019F7C File Offset: 0x0001817C
		public void WaitForPipeDrain()
		{
			throw new NotImplementedException();
		}
	}
}
