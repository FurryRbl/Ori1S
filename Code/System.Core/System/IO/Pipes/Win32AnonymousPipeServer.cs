using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000081 RID: 129
	internal class Win32AnonymousPipeServer : Win32AnonymousPipe, IPipe, IAnonymousPipeServer
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x00019F9C File Offset: 0x0001819C
		public Win32AnonymousPipeServer(AnonymousPipeServerStream owner, PipeDirection direction, HandleInheritability inheritability, int bufferSize)
		{
			SecurityAttributesHack securityAttributesHack = new SecurityAttributesHack(inheritability == HandleInheritability.Inheritable);
			IntPtr preexistingHandle;
			IntPtr preexistingHandle2;
			if (!Win32Marshal.CreatePipe(out preexistingHandle, out preexistingHandle2, ref securityAttributesHack, bufferSize))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			SafePipeHandle safePipeHandle = new SafePipeHandle(preexistingHandle, true);
			SafePipeHandle safePipeHandle2 = new SafePipeHandle(preexistingHandle2, true);
			if (direction == PipeDirection.Out)
			{
				this.server_handle = safePipeHandle2;
				this.client_handle = safePipeHandle;
			}
			else
			{
				this.server_handle = safePipeHandle;
				this.client_handle = safePipeHandle2;
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001A014 File Offset: 0x00018214
		public Win32AnonymousPipeServer(AnonymousPipeServerStream owner, SafePipeHandle serverHandle, SafePipeHandle clientHandle)
		{
			this.server_handle = serverHandle;
			this.client_handle = clientHandle;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0001A02C File Offset: 0x0001822C
		public override SafePipeHandle Handle
		{
			get
			{
				return this.server_handle;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001A034 File Offset: 0x00018234
		public SafePipeHandle ClientHandle
		{
			get
			{
				return this.client_handle;
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001A03C File Offset: 0x0001823C
		public void DisposeLocalCopyOfClientHandle()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040001B2 RID: 434
		private SafePipeHandle server_handle;

		// Token: 0x040001B3 RID: 435
		private SafePipeHandle client_handle;
	}
}
