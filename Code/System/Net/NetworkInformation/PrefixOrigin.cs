using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies how an IP address network prefix was located.</summary>
	// Token: 0x020003B9 RID: 953
	public enum PrefixOrigin
	{
		/// <summary>The prefix was located using an unspecified source.</summary>
		// Token: 0x04001435 RID: 5173
		Other,
		/// <summary>The prefix was manually configured.</summary>
		// Token: 0x04001436 RID: 5174
		Manual,
		/// <summary>The prefix is a well-known prefix. Well-known prefixes are specified in standard-track Request for Comments (RFC) documents and assigned by the Internet Assigned Numbers Authority (Iana) or an address registry. Such prefixes are reserved for special purposes.</summary>
		// Token: 0x04001437 RID: 5175
		WellKnown,
		/// <summary>The prefix was supplied by a Dynamic Host Configuration Protocol (DHCP) server.</summary>
		// Token: 0x04001438 RID: 5176
		Dhcp,
		/// <summary>The prefix was supplied by a router advertisement.</summary>
		// Token: 0x04001439 RID: 5177
		RouterAdvertisement
	}
}
