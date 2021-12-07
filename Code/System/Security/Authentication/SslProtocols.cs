using System;

namespace System.Security.Authentication
{
	/// <summary>Defines the possible versions of <see cref="T:System.Security.Authentication.SslProtocols" />.</summary>
	// Token: 0x0200042E RID: 1070
	[Flags]
	public enum SslProtocols
	{
		/// <summary>No SSL protocol is specified.</summary>
		// Token: 0x040017B3 RID: 6067
		None = 0,
		/// <summary>Specifies the SSL 2.0 protocol. SSL 2.0 has been superseded by the TLS protocol and is provided for backward compatibility only.</summary>
		// Token: 0x040017B4 RID: 6068
		Ssl2 = 12,
		/// <summary>Specifies the SSL 3.0 protocol. SSL 3.0 has been superseded by the TLS protocol and is provided for backward compatibility only.</summary>
		// Token: 0x040017B5 RID: 6069
		Ssl3 = 48,
		/// <summary>Specifies the TLS 1.0 security protocol. The TLS protocol is defined in IETF RFC 2246.</summary>
		// Token: 0x040017B6 RID: 6070
		Tls = 192,
		/// <summary>Specifies that either Secure Sockets Layer (SSL) 3.0 or Transport Layer Security (TLS) 1.0 are acceptable for secure communications</summary>
		// Token: 0x040017B7 RID: 6071
		Default = 240
	}
}
