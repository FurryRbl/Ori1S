using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003D7 RID: 983
	internal struct Win32_SOCKET_ADDRESS
	{
		// Token: 0x060021A6 RID: 8614 RVA: 0x000628C8 File Offset: 0x00060AC8
		public IPAddress GetIPAddress()
		{
			Win32_SOCKADDR win32_SOCKADDR = (Win32_SOCKADDR)Marshal.PtrToStructure(this.Sockaddr, typeof(Win32_SOCKADDR));
			byte[] array;
			if (win32_SOCKADDR.AddressFamily == 23)
			{
				array = new byte[16];
				Array.Copy(win32_SOCKADDR.AddressData, 6, array, 0, 16);
			}
			else
			{
				array = new byte[4];
				Array.Copy(win32_SOCKADDR.AddressData, 2, array, 0, 4);
			}
			return new IPAddress(array);
		}

		// Token: 0x040014E3 RID: 5347
		private const int AF_INET6 = 23;

		// Token: 0x040014E4 RID: 5348
		public IntPtr Sockaddr;

		// Token: 0x040014E5 RID: 5349
		public int SockaddrLength;
	}
}
