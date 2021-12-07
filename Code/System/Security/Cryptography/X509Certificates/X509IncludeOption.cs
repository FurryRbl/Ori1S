using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies how much of the X.509 certificate chain should be included in the X.509 data.</summary>
	// Token: 0x02000451 RID: 1105
	public enum X509IncludeOption
	{
		/// <summary>No X.509 chain information is included.</summary>
		// Token: 0x04001878 RID: 6264
		None,
		/// <summary>The entire X.509 chain is included except for the root certificate.</summary>
		// Token: 0x04001879 RID: 6265
		ExcludeRoot,
		/// <summary>Only the end certificate is included in the X.509 chain information.</summary>
		// Token: 0x0400187A RID: 6266
		EndCertOnly,
		/// <summary>The entire X.509 chain is included.</summary>
		// Token: 0x0400187B RID: 6267
		WholeChain
	}
}
