using System;

namespace System.Net
{
	/// <summary>Specifies the security protocols that are supported by the Schannel security package.</summary>
	// Token: 0x020003E1 RID: 993
	[Flags]
	public enum SecurityProtocolType
	{
		/// <summary>Specifies the Secure Socket Layer (SSL) 3.0 security protocol.</summary>
		// Token: 0x04001500 RID: 5376
		Ssl3 = 48,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.0 security protocol.</summary>
		// Token: 0x04001501 RID: 5377
		Tls = 192
	}
}
