using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000086 RID: 134
	internal static class Win32Marshal
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001A36C File Offset: 0x0001856C
		internal static bool IsWindows
		{
			get
			{
				switch (Environment.OSVersion.Platform)
				{
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.Win32NT:
				case PlatformID.WinCE:
					return true;
				default:
					return false;
				}
			}
		}

		// Token: 0x06000640 RID: 1600
		[DllImport("kernel32")]
		internal static extern bool CreatePipe(out IntPtr readHandle, out IntPtr writeHandle, ref SecurityAttributesHack pipeAtts, int size);

		// Token: 0x06000641 RID: 1601
		[DllImport("kernel32")]
		internal static extern IntPtr CreateNamedPipe(string name, uint openMode, int pipeMode, int maxInstances, int outBufferSize, int inBufferSize, int defaultTimeout, ref SecurityAttributesHack securityAttributes, IntPtr atts);

		// Token: 0x06000642 RID: 1602
		[DllImport("kernel32")]
		internal static extern bool ConnectNamedPipe(SafePipeHandle handle, IntPtr overlapped);

		// Token: 0x06000643 RID: 1603
		[DllImport("kernel32")]
		internal static extern bool DisconnectNamedPipe(SafePipeHandle handle);

		// Token: 0x06000644 RID: 1604
		[DllImport("kernel32")]
		internal static extern bool GetNamedPipeHandleState(SafePipeHandle handle, out int state, out int curInstances, out int maxCollectionCount, out int collectDateTimeout, byte[] userName, int maxUserNameSize);

		// Token: 0x06000645 RID: 1605
		[DllImport("kernel32")]
		internal static extern bool WaitNamedPipe(string name, int timeout);

		// Token: 0x06000646 RID: 1606
		[DllImport("kernel32")]
		internal static extern IntPtr CreateFile(string name, PipeAccessRights desiredAccess, FileShare fileShare, ref SecurityAttributesHack atts, int creationDisposition, int flags, IntPtr templateHandle);
	}
}
