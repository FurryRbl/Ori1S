using System;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x0200006B RID: 107
	[MonoTODO("working only on win32 right now")]
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class NamedPipeServerStream : PipeStream
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x00019400 File Offset: 0x00017600
		public NamedPipeServerStream(string pipeName) : this(pipeName, PipeDirection.InOut)
		{
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001940C File Offset: 0x0001760C
		public NamedPipeServerStream(string pipeName, PipeDirection direction) : this(pipeName, direction, 1)
		{
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00019418 File Offset: 0x00017618
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances) : this(pipeName, direction, maxNumberOfServerInstances, PipeTransmissionMode.Byte)
		{
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00019424 File Offset: 0x00017624
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, PipeOptions.None)
		{
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00019434 File Offset: 0x00017634
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, 1024, 1024)
		{
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00019458 File Offset: 0x00017658
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, inBufferSize, outBufferSize, null)
		{
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00019478 File Offset: 0x00017678
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, PipeSecurity pipeSecurity) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, inBufferSize, outBufferSize, pipeSecurity, HandleInheritability.None)
		{
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001949C File Offset: 0x0001769C
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, PipeSecurity pipeSecurity, HandleInheritability inheritability) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, inBufferSize, outBufferSize, pipeSecurity, inheritability, PipeAccessRights.ReadData | PipeAccessRights.WriteData)
		{
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000194C0 File Offset: 0x000176C0
		[MonoTODO]
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, PipeSecurity pipeSecurity, HandleInheritability inheritability, PipeAccessRights additionalAccessRights) : base(direction, transmissionMode, outBufferSize)
		{
			if (pipeSecurity != null)
			{
				throw base.ThrowACLException();
			}
			PipeAccessRights rights = PipeStream.ToAccessRights(direction) | additionalAccessRights;
			if (PipeStream.IsWindows)
			{
				this.impl = new Win32NamedPipeServer(this, pipeName, maxNumberOfServerInstances, transmissionMode, rights, options, inBufferSize, outBufferSize, inheritability);
			}
			else
			{
				this.impl = new UnixNamedPipeServer(this, pipeName, maxNumberOfServerInstances, transmissionMode, rights, options, inBufferSize, outBufferSize, inheritability);
			}
			base.InitializeHandle(this.impl.Handle, false, (options & PipeOptions.Asynchronous) != PipeOptions.None);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00019550 File Offset: 0x00017750
		public NamedPipeServerStream(PipeDirection direction, bool isAsync, bool isConnected, SafePipeHandle safePipeHandle) : base(direction, 1024)
		{
			if (PipeStream.IsWindows)
			{
				this.impl = new Win32NamedPipeServer(this, safePipeHandle);
			}
			else
			{
				this.impl = new UnixNamedPipeServer(this, safePipeHandle);
			}
			base.IsConnected = isConnected;
			base.InitializeHandle(safePipeHandle, true, isAsync);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000195A8 File Offset: 0x000177A8
		public void Disconnect()
		{
			this.impl.Disconnect();
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000195B8 File Offset: 0x000177B8
		[MonoTODO]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"ControlPrincipal\"/>\n</PermissionSet>\n")]
		public void RunAsClient(PipeStreamImpersonationWorker impersonationWorker)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000195C0 File Offset: 0x000177C0
		public void WaitForConnection()
		{
			this.impl.WaitForConnection();
			base.IsConnected = true;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000195D4 File Offset: 0x000177D4
		[MonoTODO]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"ControlPrincipal\"/>\n</PermissionSet>\n")]
		public string GetImpersonationUserName()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000195DC File Offset: 0x000177DC
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
		public IAsyncResult BeginWaitForConnection(AsyncCallback callback, object state)
		{
			if (this.wait_connect_delegate == null)
			{
				this.wait_connect_delegate = new Action(this.WaitForConnection);
			}
			return this.wait_connect_delegate.BeginInvoke(callback, state);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00019614 File Offset: 0x00017814
		public void EndWaitForConnection(IAsyncResult asyncResult)
		{
			this.wait_connect_delegate.EndInvoke(asyncResult);
		}

		// Token: 0x04000179 RID: 377
		[MonoTODO]
		public const int MaxAllowedServerInstances = 1;

		// Token: 0x0400017A RID: 378
		private INamedPipeServer impl;

		// Token: 0x0400017B RID: 379
		private Action wait_connect_delegate;
	}
}
