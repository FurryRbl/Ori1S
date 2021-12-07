using System;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x0200007D RID: 125
	internal class UnixNamedPipeClient : UnixNamedPipe, IPipe, INamedPipeClient
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x00019D48 File Offset: 0x00017F48
		public UnixNamedPipeClient(NamedPipeClientStream owner, SafePipeHandle safePipeHandle)
		{
			this.owner = owner;
			this.handle = safePipeHandle;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00019D60 File Offset: 0x00017F60
		public UnixNamedPipeClient(NamedPipeClientStream owner, string serverName, string pipeName, PipeAccessRights desiredAccessRights, PipeOptions options, HandleInheritability inheritability)
		{
			UnixNamedPipeClient <>f__this = this;
			this.owner = owner;
			if (serverName != "." && !Dns.GetHostEntry(serverName).AddressList.Contains(IPAddress.Loopback))
			{
				throw new NotImplementedException("Unix fifo does not support remote server connection");
			}
			string name = Path.Combine("/var/tmp/", pipeName);
			base.EnsureTargetFile(name);
			string text = base.RightsToAccess(desiredAccessRights);
			base.ValidateOptions(options, owner.TransmissionMode);
			this.opener = delegate()
			{
				FileStream fileStream = new FileStream(name, FileMode.Open, <>f__this.RightsToFileAccess(desiredAccessRights), FileShare.ReadWrite);
				owner.Stream = fileStream;
				<>f__this.handle = new SafePipeHandle(fileStream.Handle, false);
			};
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00019E20 File Offset: 0x00018020
		public override SafePipeHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00019E28 File Offset: 0x00018028
		public void Connect()
		{
			if (this.owner.IsConnected)
			{
				throw new InvalidOperationException("The named pipe is already connected");
			}
			this.opener();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00019E5C File Offset: 0x0001805C
		public void Connect(int timeout)
		{
			AutoResetEvent waitHandle = new AutoResetEvent(false);
			this.opener.BeginInvoke(delegate(IAsyncResult result)
			{
				this.opener.EndInvoke(result);
				waitHandle.Set();
			}, null);
			if (!waitHandle.WaitOne(TimeSpan.FromMilliseconds((double)timeout)))
			{
				throw new TimeoutException();
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00019EB8 File Offset: 0x000180B8
		public bool IsAsync
		{
			get
			{
				return this.is_async;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00019EC0 File Offset: 0x000180C0
		public int NumberOfServerInstances
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x040001AB RID: 427
		private NamedPipeClientStream owner;

		// Token: 0x040001AC RID: 428
		private bool is_async;

		// Token: 0x040001AD RID: 429
		private SafePipeHandle handle;

		// Token: 0x040001AE RID: 430
		private Action opener;
	}
}
