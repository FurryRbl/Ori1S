using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x0200007A RID: 122
	internal class UnixAnonymousPipeClient : UnixAnonymousPipe, IPipe, IAnonymousPipeClient
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x00019BD0 File Offset: 0x00017DD0
		public UnixAnonymousPipeClient(AnonymousPipeClientStream owner, SafePipeHandle handle)
		{
			this.handle = handle;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00019BE0 File Offset: 0x00017DE0
		public override SafePipeHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x040001A8 RID: 424
		private SafePipeHandle handle;
	}
}
