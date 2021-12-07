using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about a network interface address.</summary>
	// Token: 0x0200036F RID: 879
	public abstract class IPAddressInformation
	{
		/// <summary>Gets the Internet Protocol (IP) address.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance that contains the IP address of an interface.</returns>
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001F3C RID: 7996
		public abstract IPAddress Address { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the Internet Protocol (IP) address is valid to appear in a Domain Name System (DNS) server database.</summary>
		/// <returns>true if the address can appear in a DNS database; otherwise, false.</returns>
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001F3D RID: 7997
		public abstract bool IsDnsEligible { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the Internet Protocol (IP) address is transient (a cluster address).</summary>
		/// <returns>true if the address is transient; otherwise, false.</returns>
		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001F3E RID: 7998
		public abstract bool IsTransient { get; }
	}
}
