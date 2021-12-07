using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x020002A4 RID: 676
	[StructLayout(LayoutKind.Sequential)]
	internal class Timeouts
	{
		// Token: 0x060017AE RID: 6062 RVA: 0x00041064 File Offset: 0x0003F264
		public Timeouts(int read_timeout, int write_timeout)
		{
			this.SetValues(read_timeout, write_timeout);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00041074 File Offset: 0x0003F274
		public void SetValues(int read_timeout, int write_timeout)
		{
			this.ReadIntervalTimeout = uint.MaxValue;
			this.ReadTotalTimeoutMultiplier = uint.MaxValue;
			this.ReadTotalTimeoutConstant = (uint)((read_timeout != -1) ? read_timeout : -2);
			this.WriteTotalTimeoutMultiplier = 0U;
			this.WriteTotalTimeoutConstant = (uint)((write_timeout != -1) ? write_timeout : -1);
		}

		// Token: 0x04000F04 RID: 3844
		public const uint MaxDWord = 4294967295U;

		// Token: 0x04000F05 RID: 3845
		public uint ReadIntervalTimeout;

		// Token: 0x04000F06 RID: 3846
		public uint ReadTotalTimeoutMultiplier;

		// Token: 0x04000F07 RID: 3847
		public uint ReadTotalTimeoutConstant;

		// Token: 0x04000F08 RID: 3848
		public uint WriteTotalTimeoutMultiplier;

		// Token: 0x04000F09 RID: 3849
		public uint WriteTotalTimeoutConstant;
	}
}
