using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000079 RID: 121
	internal abstract class UnixAnonymousPipe : IPipe
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000608 RID: 1544
		public abstract SafePipeHandle Handle { get; }

		// Token: 0x06000609 RID: 1545 RVA: 0x00019BC8 File Offset: 0x00017DC8
		public void WaitForPipeDrain()
		{
			throw new NotImplementedException();
		}
	}
}
