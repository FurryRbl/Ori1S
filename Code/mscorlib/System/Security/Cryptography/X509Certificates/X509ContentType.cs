using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the format of an X.509 certificate. </summary>
	// Token: 0x020005EE RID: 1518
	[ComVisible(true)]
	public enum X509ContentType
	{
		/// <summary>An unknown X.509 certificate.  </summary>
		// Token: 0x0400191E RID: 6430
		Unknown,
		/// <summary>A single X.509 certificate.</summary>
		// Token: 0x0400191F RID: 6431
		Cert,
		/// <summary>A single serialized X.509 certificate. </summary>
		// Token: 0x04001920 RID: 6432
		SerializedCert,
		/// <summary>A PFX-formatted certificate. The Pfx value is identical to the Pkcs12 value.</summary>
		// Token: 0x04001921 RID: 6433
		Pfx,
		/// <summary>A serialized store.</summary>
		// Token: 0x04001922 RID: 6434
		SerializedStore,
		/// <summary>A PKCS #7–formatted certificate.</summary>
		// Token: 0x04001923 RID: 6435
		Pkcs7,
		/// <summary>An Authenticode X.509 certificate. </summary>
		// Token: 0x04001924 RID: 6436
		Authenticode,
		/// <summary>A PKCS #12–formatted certificate. The Pkcs12 value is identical to the Pfx value.</summary>
		// Token: 0x04001925 RID: 6437
		Pkcs12 = 3
	}
}
