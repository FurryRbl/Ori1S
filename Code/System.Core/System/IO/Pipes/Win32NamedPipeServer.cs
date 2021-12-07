using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000084 RID: 132
	internal class Win32NamedPipeServer : Win32NamedPipe, IPipe, INamedPipeServer
	{
		// Token: 0x06000639 RID: 1593 RVA: 0x0001A23C File Offset: 0x0001843C
		public Win32NamedPipeServer(NamedPipeServerStream owner, SafePipeHandle safePipeHandle)
		{
			this.handle = safePipeHandle;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001A24C File Offset: 0x0001844C
		public Win32NamedPipeServer(NamedPipeServerStream owner, string pipeName, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeAccessRights rights, PipeOptions options, int inBufferSize, int outBufferSize, HandleInheritability inheritability)
		{
			string name = string.Format("\\\\.\\pipe\\{0}", pipeName);
			uint num = 0U;
			if ((rights & PipeAccessRights.ReadData) != (PipeAccessRights)0)
			{
				num |= 1U;
			}
			if ((rights & PipeAccessRights.WriteData) != (PipeAccessRights)0)
			{
				num |= 2U;
			}
			if ((options & PipeOptions.WriteThrough) != PipeOptions.None)
			{
				num |= 2147483648U;
			}
			int num2 = 0;
			if ((owner.TransmissionMode & PipeTransmissionMode.Message) != PipeTransmissionMode.Byte)
			{
				num2 |= 4;
			}
			if ((options & PipeOptions.Asynchronous) != PipeOptions.None)
			{
				num2 |= 1;
			}
			SecurityAttributesHack securityAttributesHack = new SecurityAttributesHack(inheritability == HandleInheritability.Inheritable);
			IntPtr intPtr = Win32Marshal.CreateNamedPipe(name, num, num2, maxNumberOfServerInstances, outBufferSize, inBufferSize, 0, ref securityAttributesHack, IntPtr.Zero);
			if (intPtr == new IntPtr(-1L))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			this.handle = new SafePipeHandle(intPtr, true);
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001A308 File Offset: 0x00018508
		public override SafePipeHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001A310 File Offset: 0x00018510
		public void Disconnect()
		{
			Win32Marshal.DisconnectNamedPipe(this.Handle);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001A320 File Offset: 0x00018520
		public void WaitForConnection()
		{
			if (!Win32Marshal.ConnectNamedPipe(this.Handle, IntPtr.Zero))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x040001BA RID: 442
		private SafePipeHandle handle;
	}
}
