using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies how an IP address host suffix was located.</summary>
	// Token: 0x020003BA RID: 954
	public enum SuffixOrigin
	{
		/// <summary>The suffix was located using an unspecified source.</summary>
		// Token: 0x0400143B RID: 5179
		Other,
		/// <summary>The suffix was manually configured.</summary>
		// Token: 0x0400143C RID: 5180
		Manual,
		/// <summary>The suffix is a well-known suffix. Well-known suffixes are specified in standard-track Request for Comments (RFC) documents and assigned by the Internet Assigned Numbers Authority (Iana) or an address registry. Such suffixes are reserved for special purposes.</summary>
		// Token: 0x0400143D RID: 5181
		WellKnown,
		/// <summary>The suffix was supplied by a Dynamic Host Configuration Protocol (DHCP) server.</summary>
		// Token: 0x0400143E RID: 5182
		OriginDhcp,
		/// <summary>The suffix is a link-local suffix.</summary>
		// Token: 0x0400143F RID: 5183
		LinkLayerAddress,
		/// <summary>The suffix was randomly assigned.</summary>
		// Token: 0x04001440 RID: 5184
		Random
	}
}
