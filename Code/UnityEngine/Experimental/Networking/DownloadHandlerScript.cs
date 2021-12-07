using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Experimental.Networking
{
	// Token: 0x02000226 RID: 550
	[StructLayout(LayoutKind.Sequential)]
	public class DownloadHandlerScript : DownloadHandler
	{
		// Token: 0x0600220B RID: 8715 RVA: 0x0002AB2C File Offset: 0x00028D2C
		public DownloadHandlerScript()
		{
			base.InternalCreateScript();
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x0002AB3C File Offset: 0x00028D3C
		public DownloadHandlerScript(byte[] preallocatedBuffer)
		{
			if (preallocatedBuffer == null || preallocatedBuffer.Length < 1)
			{
				throw new ArgumentException("Cannot create a preallocated-buffer DownloadHandlerScript backed by a null or zero-length array");
			}
			base.InternalCreateScript();
			this.InternalSetPreallocatedBuffer(preallocatedBuffer);
		}

		// Token: 0x0600220D RID: 8717
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetPreallocatedBuffer(byte[] buffer);
	}
}
