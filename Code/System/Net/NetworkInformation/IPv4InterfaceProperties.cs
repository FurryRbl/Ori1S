using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 4 (IPv4).</summary>
	// Token: 0x02000383 RID: 899
	public abstract class IPv4InterfaceProperties
	{
		/// <summary>Gets the index of the network interface associated with the Internet Protocol version 4 (IPv4) address.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the index of the IPv4 interface.</returns>
		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x0600200B RID: 8203
		public abstract int Index { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface has an automatic private IP addressing (APIPA) address.</summary>
		/// <returns>true if the interface uses an APIPA address; otherwise, false.</returns>
		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x0600200C RID: 8204
		public abstract bool IsAutomaticPrivateAddressingActive { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface has automatic private IP addressing (APIPA) enabled.</summary>
		/// <returns>true if the interface uses APIPA; otherwise, false.</returns>
		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600200D RID: 8205
		public abstract bool IsAutomaticPrivateAddressingEnabled { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the interface is configured to use a Dynamic Host Configuration Protocol (DHCP) server to obtain an IP address.</summary>
		/// <returns>true if the interface is configured to obtain an IP address from a DHCP server; otherwise, false.</returns>
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600200E RID: 8206
		public abstract bool IsDhcpEnabled { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface can forward (route) packets.</summary>
		/// <returns>true if this interface routes packets; otherwise false.</returns>
		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x0600200F RID: 8207
		public abstract bool IsForwardingEnabled { get; }

		/// <summary>Gets the maximum transmission unit (MTU) for this network interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the MTU.</returns>
		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002010 RID: 8208
		public abstract int Mtu { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether an interface uses Windows Internet Name Service (WINS).</summary>
		/// <returns>true if the interface uses WINS; otherwise, false.</returns>
		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002011 RID: 8209
		public abstract bool UsesWins { get; }
	}
}
