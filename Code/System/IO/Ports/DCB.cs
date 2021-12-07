using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x020002A3 RID: 675
	[StructLayout(LayoutKind.Sequential)]
	internal class DCB
	{
		// Token: 0x060017AD RID: 6061 RVA: 0x00040F7C File Offset: 0x0003F17C
		public void SetValues(int baud_rate, Parity parity, int byte_size, StopBits sb, Handshake hs)
		{
			switch (sb)
			{
			case StopBits.One:
				this.stop_bits = 0;
				break;
			case StopBits.Two:
				this.stop_bits = 2;
				break;
			case StopBits.OnePointFive:
				this.stop_bits = 1;
				break;
			}
			this.baud_rate = baud_rate;
			this.parity = (byte)parity;
			this.byte_size = (byte)byte_size;
			this.flags &= -8965;
			switch (hs)
			{
			case Handshake.XOnXOff:
				this.flags |= 768;
				break;
			case Handshake.RequestToSend:
				this.flags |= 8196;
				break;
			case Handshake.RequestToSendXOnXOff:
				this.flags |= 8964;
				break;
			}
		}

		// Token: 0x04000EF1 RID: 3825
		private const int fOutxCtsFlow = 4;

		// Token: 0x04000EF2 RID: 3826
		private const int fOutX = 256;

		// Token: 0x04000EF3 RID: 3827
		private const int fInX = 512;

		// Token: 0x04000EF4 RID: 3828
		private const int fRtsControl2 = 8192;

		// Token: 0x04000EF5 RID: 3829
		public int dcb_length;

		// Token: 0x04000EF6 RID: 3830
		public int baud_rate;

		// Token: 0x04000EF7 RID: 3831
		public int flags;

		// Token: 0x04000EF8 RID: 3832
		public short w_reserved;

		// Token: 0x04000EF9 RID: 3833
		public short xon_lim;

		// Token: 0x04000EFA RID: 3834
		public short xoff_lim;

		// Token: 0x04000EFB RID: 3835
		public byte byte_size;

		// Token: 0x04000EFC RID: 3836
		public byte parity;

		// Token: 0x04000EFD RID: 3837
		public byte stop_bits;

		// Token: 0x04000EFE RID: 3838
		public byte xon_char;

		// Token: 0x04000EFF RID: 3839
		public byte xoff_char;

		// Token: 0x04000F00 RID: 3840
		public byte error_char;

		// Token: 0x04000F01 RID: 3841
		public byte eof_char;

		// Token: 0x04000F02 RID: 3842
		public byte evt_char;

		// Token: 0x04000F03 RID: 3843
		public short w_reserved1;
	}
}
