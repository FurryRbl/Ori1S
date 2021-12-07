using System;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000069 RID: 105
	[MonoTODO("Anonymous pipes are not working even on win32, due to some access authorization issue")]
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class AnonymousPipeServerStream : PipeStream
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x00019098 File Offset: 0x00017298
		public AnonymousPipeServerStream() : this(PipeDirection.Out)
		{
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000190A4 File Offset: 0x000172A4
		public AnonymousPipeServerStream(PipeDirection direction) : this(direction, HandleInheritability.None)
		{
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000190B0 File Offset: 0x000172B0
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability) : this(direction, inheritability, 1024)
		{
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000190C0 File Offset: 0x000172C0
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability, int bufferSize) : this(direction, inheritability, bufferSize, null)
		{
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000190CC File Offset: 0x000172CC
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability, int bufferSize, PipeSecurity pipeSecurity) : base(direction, bufferSize)
		{
			if (pipeSecurity != null)
			{
				throw base.ThrowACLException();
			}
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException("Anonymous pipe direction can only be either in or out.");
			}
			if (PipeStream.IsWindows)
			{
				this.impl = new Win32AnonymousPipeServer(this, direction, inheritability, bufferSize);
			}
			else
			{
				this.impl = new UnixAnonymousPipeServer(this, direction, inheritability, bufferSize);
			}
			base.InitializeHandle(this.impl.Handle, false, false);
			base.IsConnected = true;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00019148 File Offset: 0x00017348
		[MonoTODO]
		public AnonymousPipeServerStream(PipeDirection direction, SafePipeHandle serverSafePipeHandle, SafePipeHandle clientSafePipeHandle) : base(direction, 1024)
		{
			if (serverSafePipeHandle == null)
			{
				throw new ArgumentNullException("serverSafePipeHandle");
			}
			if (clientSafePipeHandle == null)
			{
				throw new ArgumentNullException("clientSafePipeHandle");
			}
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException("Anonymous pipe direction can only be either in or out.");
			}
			if (PipeStream.IsWindows)
			{
				this.impl = new Win32AnonymousPipeServer(this, serverSafePipeHandle, clientSafePipeHandle);
			}
			else
			{
				this.impl = new UnixAnonymousPipeServer(this, serverSafePipeHandle, clientSafePipeHandle);
			}
			base.InitializeHandle(serverSafePipeHandle, true, false);
			base.IsConnected = true;
			this.ClientSafePipeHandle = clientSafePipeHandle;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x000191D8 File Offset: 0x000173D8
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x000191E0 File Offset: 0x000173E0
		[MonoTODO]
		public SafePipeHandle ClientSafePipeHandle { get; private set; }

		// Token: 0x17000083 RID: 131
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x000191EC File Offset: 0x000173EC
		public override PipeTransmissionMode ReadMode
		{
			set
			{
				if (value == PipeTransmissionMode.Message)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x000191FC File Offset: 0x000173FC
		public override PipeTransmissionMode TransmissionMode
		{
			get
			{
				return PipeTransmissionMode.Byte;
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00019200 File Offset: 0x00017400
		[MonoTODO]
		public void DisposeLocalCopyOfClientHandle()
		{
			this.impl.DisposeLocalCopyOfClientHandle();
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00019210 File Offset: 0x00017410
		public string GetClientHandleAsString()
		{
			return this.impl.Handle.DangerousGetHandle().ToInt64().ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x04000176 RID: 374
		private IAnonymousPipeServer impl;
	}
}
