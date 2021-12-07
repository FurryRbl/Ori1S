using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies flags that modify the behavior of the cryptographic service providers (CSP).</summary>
	// Token: 0x020005A4 RID: 1444
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum CspProviderFlags
	{
		/// <summary>Use key information from the computer's key store.</summary>
		// Token: 0x04001825 RID: 6181
		UseMachineKeyStore = 1,
		/// <summary>Use key information from the default key container.</summary>
		// Token: 0x04001826 RID: 6182
		UseDefaultKeyContainer = 2,
		/// <summary>Use key information from the current key.</summary>
		// Token: 0x04001827 RID: 6183
		UseExistingKey = 8,
		/// <summary>Don't specify any settings.</summary>
		// Token: 0x04001828 RID: 6184
		NoFlags = 0,
		/// <summary>Prevent the CSP from displaying any user interface (UI) for this context.</summary>
		// Token: 0x04001829 RID: 6185
		NoPrompt = 64,
		/// <summary>Allow a key to be exported for archival or recovery.</summary>
		// Token: 0x0400182A RID: 6186
		UseArchivableKey = 16,
		/// <summary>Use key information that can not be exported.</summary>
		// Token: 0x0400182B RID: 6187
		UseNonExportableKey = 4,
		/// <summary>Notify the user through a dialog box or another method when certain actions are attempting to use a key.  This flag is not compatible with the <see cref="F:System.Security.Cryptography.CspProviderFlags.NoPrompt" /> flag.</summary>
		// Token: 0x0400182C RID: 6188
		UseUserProtectedKey = 32
	}
}
