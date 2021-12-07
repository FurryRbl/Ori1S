using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines where and how to export the private key of an X.509 certificate.</summary>
	// Token: 0x020005EF RID: 1519
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum X509KeyStorageFlags
	{
		/// <summary>The default key set is used.  The user key set is usually the default. </summary>
		// Token: 0x04001927 RID: 6439
		DefaultKeySet = 0,
		/// <summary>Private keys are stored in the current user store rather than the local computer store. This occurs even if the certificate specifies that the keys should go in the local computer store. </summary>
		// Token: 0x04001928 RID: 6440
		UserKeySet = 1,
		/// <summary>Private keys are stored in the local computer store rather than the current user store. </summary>
		// Token: 0x04001929 RID: 6441
		MachineKeySet = 2,
		/// <summary>Imported keys are marked as exportable.  .</summary>
		// Token: 0x0400192A RID: 6442
		Exportable = 4,
		/// <summary>Notify the user through a dialog box or other method that the key is accessed.  The Cryptographic Service Provider (CSP) in use defines the precise behavior.</summary>
		// Token: 0x0400192B RID: 6443
		UserProtected = 8,
		/// <summary>The key associated with a PFX file is persisted when importing a certificate.</summary>
		// Token: 0x0400192C RID: 6444
		PersistKeySet = 16
	}
}
