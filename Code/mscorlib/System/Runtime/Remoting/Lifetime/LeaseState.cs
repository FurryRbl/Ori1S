using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Indicates the possible lease states of a lifetime lease.</summary>
	// Token: 0x0200048A RID: 1162
	[ComVisible(true)]
	[Serializable]
	public enum LeaseState
	{
		/// <summary>The lease is not initialized.</summary>
		// Token: 0x04001428 RID: 5160
		Null,
		/// <summary>The lease has been created, but is not yet active.</summary>
		// Token: 0x04001429 RID: 5161
		Initial,
		/// <summary>The lease is active and has not expired.</summary>
		// Token: 0x0400142A RID: 5162
		Active,
		/// <summary>The lease has expired and is seeking sponsorship.</summary>
		// Token: 0x0400142B RID: 5163
		Renewing,
		/// <summary>The lease has expired and cannot be renewed.</summary>
		// Token: 0x0400142C RID: 5164
		Expired
	}
}
