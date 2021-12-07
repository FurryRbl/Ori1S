using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation.MacOsStructs
{
	// Token: 0x0200039B RID: 923
	internal struct sockaddr_dl
	{
		// Token: 0x040013B4 RID: 5044
		public byte sdl_len;

		// Token: 0x040013B5 RID: 5045
		public byte sdl_family;

		// Token: 0x040013B6 RID: 5046
		public ushort sdl_index;

		// Token: 0x040013B7 RID: 5047
		public byte sdl_type;

		// Token: 0x040013B8 RID: 5048
		public byte sdl_nlen;

		// Token: 0x040013B9 RID: 5049
		public byte sdl_alen;

		// Token: 0x040013BA RID: 5050
		public byte sdl_slen;

		// Token: 0x040013BB RID: 5051
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
		public byte[] sdl_data;
	}
}
