using System;
using Microsoft.Win32.SafeHandles;
using Mono.Unix.Native;

namespace System.IO.Pipes
{
	// Token: 0x0200007C RID: 124
	internal abstract class UnixNamedPipe : IPipe
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000612 RID: 1554
		public abstract SafePipeHandle Handle { get; }

		// Token: 0x06000613 RID: 1555 RVA: 0x00019C34 File Offset: 0x00017E34
		public void WaitForPipeDrain()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00019C3C File Offset: 0x00017E3C
		public void EnsureTargetFile(string name)
		{
			if (!File.Exists(name))
			{
				int num = Syscall.mknod(name, FilePermissions.S_ISUID | FilePermissions.S_ISGID | FilePermissions.S_ISVTX | FilePermissions.S_IRUSR | FilePermissions.S_IWUSR | FilePermissions.S_IXUSR | FilePermissions.S_IRGRP | FilePermissions.S_IWGRP | FilePermissions.S_IXGRP | FilePermissions.S_IROTH | FilePermissions.S_IWOTH | FilePermissions.S_IXOTH | FilePermissions.S_IFIFO, 0UL);
				if (num != 0)
				{
					throw new IOException(string.Format("Error on creating named pipe: error code {0}", num));
				}
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00019C80 File Offset: 0x00017E80
		protected void ValidateOptions(PipeOptions options, PipeTransmissionMode mode)
		{
			if ((options & PipeOptions.WriteThrough) != PipeOptions.None)
			{
				throw new NotImplementedException("WriteThrough is not supported");
			}
			if ((mode & PipeTransmissionMode.Message) != PipeTransmissionMode.Byte)
			{
				throw new NotImplementedException("Message transmission mode is not supported");
			}
			if ((options & PipeOptions.Asynchronous) != PipeOptions.None)
			{
				throw new NotImplementedException("Asynchronous pipe mode is not supported");
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00019CBC File Offset: 0x00017EBC
		protected string RightsToAccess(PipeAccessRights rights)
		{
			string result;
			if ((rights & PipeAccessRights.ReadData) != (PipeAccessRights)0)
			{
				if ((rights & PipeAccessRights.WriteData) != (PipeAccessRights)0)
				{
					result = "r+";
				}
				else
				{
					result = "r";
				}
			}
			else
			{
				if ((rights & PipeAccessRights.WriteData) == (PipeAccessRights)0)
				{
					throw new InvalidOperationException("The pipe must be opened to either read or write");
				}
				result = "w";
			}
			return result;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00019D10 File Offset: 0x00017F10
		protected FileAccess RightsToFileAccess(PipeAccessRights rights)
		{
			if ((rights & PipeAccessRights.ReadData) != (PipeAccessRights)0)
			{
				if ((rights & PipeAccessRights.WriteData) != (PipeAccessRights)0)
				{
					return FileAccess.ReadWrite;
				}
				return FileAccess.Read;
			}
			else
			{
				if ((rights & PipeAccessRights.WriteData) != (PipeAccessRights)0)
				{
					return FileAccess.Write;
				}
				throw new InvalidOperationException("The pipe must be opened to either read or write");
			}
		}
	}
}
