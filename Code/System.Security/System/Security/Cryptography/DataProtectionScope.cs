using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the scope of the data protection to be applied by the <see cref="M:System.Security.Cryptography.ProtectedData.Protect(System.Byte[],System.Byte[],System.Security.Cryptography.DataProtectionScope)" /> method.</summary>
	// Token: 0x0200000F RID: 15
	public enum DataProtectionScope
	{
		/// <summary>The protected data is associated with the current user. Only threads running under the current user context can unprotect the data.</summary>
		// Token: 0x0400003B RID: 59
		CurrentUser,
		/// <summary>The protected data is associated with the machine context. Any process running on the computer can unprotect data. This enumeration value is usually used in server-specific applications that run on a server where untrusted users are not allowed access.</summary>
		// Token: 0x0400003C RID: 60
		LocalMachine
	}
}
