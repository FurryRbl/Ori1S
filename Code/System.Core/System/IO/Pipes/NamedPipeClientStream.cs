using System;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x0200006A RID: 106
	[MonoTODO("working only on win32 right now")]
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class NamedPipeClientStream : PipeStream
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x00019244 File Offset: 0x00017444
		public NamedPipeClientStream(string pipeName) : this(".", pipeName)
		{
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00019254 File Offset: 0x00017454
		public NamedPipeClientStream(string serverName, string pipeName) : this(serverName, pipeName, PipeDirection.InOut)
		{
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00019260 File Offset: 0x00017460
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction) : this(serverName, pipeName, direction, PipeOptions.None)
		{
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001926C File Offset: 0x0001746C
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options) : this(serverName, pipeName, direction, options, TokenImpersonationLevel.None)
		{
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001927C File Offset: 0x0001747C
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options, TokenImpersonationLevel impersonationLevel) : this(serverName, pipeName, direction, options, impersonationLevel, HandleInheritability.None)
		{
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001928C File Offset: 0x0001748C
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options, TokenImpersonationLevel impersonationLevel, HandleInheritability inheritability) : this(serverName, pipeName, PipeStream.ToAccessRights(direction), options, impersonationLevel, inheritability)
		{
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000192B0 File Offset: 0x000174B0
		public NamedPipeClientStream(PipeDirection direction, bool isAsync, bool isConnected, SafePipeHandle safePipeHandle) : base(direction, 1024)
		{
			if (PipeStream.IsWindows)
			{
				this.impl = new Win32NamedPipeClient(this, safePipeHandle);
			}
			else
			{
				this.impl = new UnixNamedPipeClient(this, safePipeHandle);
			}
			base.IsConnected = isConnected;
			base.InitializeHandle(safePipeHandle, true, isAsync);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00019308 File Offset: 0x00017508
		public NamedPipeClientStream(string serverName, string pipeName, PipeAccessRights desiredAccessRights, PipeOptions options, TokenImpersonationLevel impersonationLevel, HandleInheritability inheritability) : base(PipeStream.ToDirection(desiredAccessRights), 1024)
		{
			if (impersonationLevel != TokenImpersonationLevel.None || inheritability != HandleInheritability.None)
			{
				throw base.ThrowACLException();
			}
			if (PipeStream.IsWindows)
			{
				this.impl = new Win32NamedPipeClient(this, serverName, pipeName, desiredAccessRights, options, inheritability);
			}
			else
			{
				this.impl = new UnixNamedPipeClient(this, serverName, pipeName, desiredAccessRights, options, inheritability);
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00019370 File Offset: 0x00017570
		public void Connect()
		{
			this.impl.Connect();
			base.InitializeHandle(this.impl.Handle, false, this.impl.IsAsync);
			base.IsConnected = true;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000193AC File Offset: 0x000175AC
		public void Connect(int timeout)
		{
			this.impl.Connect(timeout);
			base.InitializeHandle(this.impl.Handle, false, this.impl.IsAsync);
			base.IsConnected = true;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x000193EC File Offset: 0x000175EC
		public int NumberOfServerInstances
		{
			get
			{
				this.CheckPipePropertyOperations();
				return this.impl.NumberOfServerInstances;
			}
		}

		// Token: 0x04000178 RID: 376
		private INamedPipeClient impl;
	}
}
