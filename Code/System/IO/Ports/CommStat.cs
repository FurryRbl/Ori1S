using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x020002A5 RID: 677
	[StructLayout(LayoutKind.Sequential)]
	internal class CommStat
	{
		// Token: 0x04000F0A RID: 3850
		public uint flags;

		// Token: 0x04000F0B RID: 3851
		public uint BytesIn;

		// Token: 0x04000F0C RID: 3852
		public uint BytesOut;
	}
}
