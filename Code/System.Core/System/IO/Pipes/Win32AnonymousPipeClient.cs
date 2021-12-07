using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000080 RID: 128
	internal class Win32AnonymousPipeClient : Win32AnonymousPipe, IPipe, IAnonymousPipeClient
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x00019F84 File Offset: 0x00018184
		public Win32AnonymousPipeClient(AnonymousPipeClientStream owner, SafePipeHandle handle)
		{
			this.handle = handle;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00019F94 File Offset: 0x00018194
		public override SafePipeHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x040001B1 RID: 433
		private SafePipeHandle handle;
	}
}
