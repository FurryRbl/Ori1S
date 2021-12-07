using System;

namespace System.Net
{
	/// <summary>Specifies protocols for authentication.</summary>
	// Token: 0x020002BA RID: 698
	[Flags]
	public enum AuthenticationSchemes
	{
		/// <summary>No authentication is allowed. A client requesting an <see cref="T:System.Net.HttpListener" /> object with this flag set will always receive a 403 Forbidden status. Use this flag when a resource should never be served to a client.</summary>
		// Token: 0x04000F6D RID: 3949
		None = 0,
		/// <summary>Specifies digest authentication.</summary>
		// Token: 0x04000F6E RID: 3950
		Digest = 1,
		/// <summary>Negotiates with the client to determine the authentication scheme. If both client and server support Kerberos, it is used; otherwise, NTLM is used.</summary>
		// Token: 0x04000F6F RID: 3951
		Negotiate = 2,
		/// <summary>Specifies NTLM authentication.</summary>
		// Token: 0x04000F70 RID: 3952
		Ntlm = 4,
		/// <summary>Specifies Windows authentication.</summary>
		// Token: 0x04000F71 RID: 3953
		IntegratedWindowsAuthentication = 6,
		/// <summary>Specifies basic authentication. </summary>
		// Token: 0x04000F72 RID: 3954
		Basic = 8,
		/// <summary>Specifies anonymous authentication.</summary>
		// Token: 0x04000F73 RID: 3955
		Anonymous = 32768
	}
}
