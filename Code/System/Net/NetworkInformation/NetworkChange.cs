using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Allows applications to receive notification when the Internet Protocol (IP) address of a network interface, also called a network card or adapter, changes.</summary>
	// Token: 0x020003A3 RID: 931
	public sealed class NetworkChange
	{
		// Token: 0x06002086 RID: 8326 RVA: 0x0005FDAC File Offset: 0x0005DFAC
		private NetworkChange()
		{
		}

		/// <summary>Occurs when the IP address of a network interface changes.</summary>
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06002087 RID: 8327 RVA: 0x0005FDB4 File Offset: 0x0005DFB4
		// (remove) Token: 0x06002088 RID: 8328 RVA: 0x0005FDCC File Offset: 0x0005DFCC
		public static event NetworkAddressChangedEventHandler NetworkAddressChanged;

		/// <summary>Occurs when the availability of the network changes.</summary>
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06002089 RID: 8329 RVA: 0x0005FDE4 File Offset: 0x0005DFE4
		// (remove) Token: 0x0600208A RID: 8330 RVA: 0x0005FDFC File Offset: 0x0005DFFC
		public static event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged;
	}
}
