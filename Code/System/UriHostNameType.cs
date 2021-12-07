using System;

namespace System
{
	/// <summary>Defines host name types for the <see cref="M:System.Uri.CheckHostName(System.String)" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004B6 RID: 1206
	public enum UriHostNameType
	{
		/// <summary>The type of the host name is not supplied.</summary>
		// Token: 0x04001B6E RID: 7022
		Unknown,
		/// <summary>The host is set, but the type cannot be determined.</summary>
		// Token: 0x04001B6F RID: 7023
		Basic,
		/// <summary>The host name is a domain name system (DNS) style host name.</summary>
		// Token: 0x04001B70 RID: 7024
		Dns,
		/// <summary>The host name is an Internet Protocol (IP) version 4 host address.</summary>
		// Token: 0x04001B71 RID: 7025
		IPv4,
		/// <summary>The host name is an Internet Protocol (IP) version 6 host address.</summary>
		// Token: 0x04001B72 RID: 7026
		IPv6
	}
}
