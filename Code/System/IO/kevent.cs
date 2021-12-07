using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x0200028C RID: 652
	internal struct kevent : IDisposable
	{
		// Token: 0x060016D3 RID: 5843 RVA: 0x0003EA8C File Offset: 0x0003CC8C
		public void Dispose()
		{
			if (this.udata != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.udata);
			}
		}

		// Token: 0x0400076F RID: 1903
		public int ident;

		// Token: 0x04000770 RID: 1904
		public short filter;

		// Token: 0x04000771 RID: 1905
		public ushort flags;

		// Token: 0x04000772 RID: 1906
		public uint fflags;

		// Token: 0x04000773 RID: 1907
		public int data;

		// Token: 0x04000774 RID: 1908
		public IntPtr udata;
	}
}
