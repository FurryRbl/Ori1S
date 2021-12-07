using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines security impersonation levels. Security impersonation levels govern the degree to which a server process can act on behalf of a client process.</summary>
	// Token: 0x02000667 RID: 1639
	[ComVisible(true)]
	[Serializable]
	public enum TokenImpersonationLevel
	{
		/// <summary>The server process cannot obtain identification information about the client, and it cannot impersonate the client.</summary>
		// Token: 0x04001ACC RID: 6860
		Anonymous = 1,
		/// <summary>The server process can impersonate the client's security context on remote systems.</summary>
		// Token: 0x04001ACD RID: 6861
		Delegation = 4,
		/// <summary>The server process can obtain information about the client, such as security identifiers and privileges, but it cannot impersonate the client. This is useful for servers that export their own objects, for example, database products that export tables and views. Using the retrieved client-security information, the server can make access-validation decisions without being able to use other services that are using the client's security context.</summary>
		// Token: 0x04001ACE RID: 6862
		Identification = 2,
		/// <summary>The server process can impersonate the client's security context on its local system. The server cannot impersonate the client on remote systems.</summary>
		// Token: 0x04001ACF RID: 6863
		Impersonation,
		/// <summary>An impersonation level is not assigned.</summary>
		// Token: 0x04001AD0 RID: 6864
		None = 0
	}
}
