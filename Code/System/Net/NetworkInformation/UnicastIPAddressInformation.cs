using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about a network interface's unicast address.</summary>
	// Token: 0x020003C8 RID: 968
	public abstract class UnicastIPAddressInformation : IPAddressInformation
	{
		/// <summary>Gets the number of seconds remaining during which this address is the preferred address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of seconds left for this address to remain preferred.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x0600217E RID: 8574
		public abstract long AddressPreferredLifetime { get; }

		/// <summary>Gets the number of seconds remaining during which this address is valid.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of seconds left for this address to remain assigned.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x0600217F RID: 8575
		public abstract long AddressValidLifetime { get; }

		/// <summary>Specifies the amount of time remaining on the Dynamic Host Configuration Protocol (DHCP) lease for this IP address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that contains the number of seconds remaining before the computer must release the <see cref="T:System.Net.IPAddress" /> instance.</returns>
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002180 RID: 8576
		public abstract long DhcpLeaseLifetime { get; }

		/// <summary>Gets a value that indicates the state of the duplicate address detection algorithm.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.DuplicateAddressDetectionState" /> values that indicates the progress of the algorithm in determining the uniqueness of this IP address.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002181 RID: 8577
		public abstract DuplicateAddressDetectionState DuplicateAddressDetectionState { get; }

		/// <summary>Gets the IPv4 mask.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> object that contains the IPv4 mask.</returns>
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06002182 RID: 8578
		public abstract IPAddress IPv4Mask { get; }

		/// <summary>Gets a value that identifies the source of a unicast Internet Protocol (IP) address prefix.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.PrefixOrigin" /> values that identifies how the prefix information was obtained.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06002183 RID: 8579
		public abstract PrefixOrigin PrefixOrigin { get; }

		/// <summary>Gets a value that identifies the source of a unicast Internet Protocol (IP) address suffix.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.SuffixOrigin" /> values that identifies how the suffix information was obtained.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06002184 RID: 8580
		public abstract SuffixOrigin SuffixOrigin { get; }
	}
}
