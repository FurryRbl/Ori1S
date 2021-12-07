using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 6 (IPv6).</summary>
	// Token: 0x0200038D RID: 909
	public abstract class IPv6InterfaceProperties
	{
		/// <summary>Gets the index of the network interface associated with the Internet Protocol version 6 (IPv6) address.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the index of the IPv6 interface.</returns>
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x0600205E RID: 8286
		public abstract int Index { get; }

		/// <summary>Gets the maximum transmission unit (MTU) for this network interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the MTU.</returns>
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x0600205F RID: 8287
		public abstract int Mtu { get; }
	}
}
