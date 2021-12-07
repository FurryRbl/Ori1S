using System;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000068 RID: 104
	[MonoTODO("Anonymous pipes are not working even on win32, due to some access authorization issue")]
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class AnonymousPipeClientStream : PipeStream
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x0001901C File Offset: 0x0001721C
		public AnonymousPipeClientStream(string pipeHandleAsString) : this(PipeDirection.In, pipeHandleAsString)
		{
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00019028 File Offset: 0x00017228
		public AnonymousPipeClientStream(PipeDirection direction, string pipeHandleAsString) : this(direction, AnonymousPipeClientStream.ToSafePipeHandle(pipeHandleAsString))
		{
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00019038 File Offset: 0x00017238
		public AnonymousPipeClientStream(PipeDirection direction, SafePipeHandle safePipeHandle) : base(direction, 1024)
		{
			base.InitializeHandle(safePipeHandle, false, false);
			base.IsConnected = true;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00019058 File Offset: 0x00017258
		private static SafePipeHandle ToSafePipeHandle(string pipeHandleAsString)
		{
			if (pipeHandleAsString == null)
			{
				throw new ArgumentNullException("pipeHandleAsString");
			}
			return new SafePipeHandle(new IntPtr(long.Parse(pipeHandleAsString, NumberFormatInfo.InvariantInfo)), false);
		}

		// Token: 0x17000080 RID: 128
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x00019084 File Offset: 0x00017284
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

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00019094 File Offset: 0x00017294
		public override PipeTransmissionMode TransmissionMode
		{
			get
			{
				return PipeTransmissionMode.Byte;
			}
		}
	}
}
