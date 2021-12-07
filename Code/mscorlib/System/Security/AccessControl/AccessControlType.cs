using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies whether an <see cref="T:System.Security.AccessControl.AccessRule" /> object is used to allow or deny access. These values are not flags, and they cannot be combined.</summary>
	// Token: 0x02000557 RID: 1367
	public enum AccessControlType
	{
		/// <summary>The <see cref="T:System.Security.AccessControl.AccessRule" /> object is used to allow access to a secured object.</summary>
		// Token: 0x0400168B RID: 5771
		Allow,
		/// <summary>The <see cref="T:System.Security.AccessControl.AccessRule" /> object is used to deny access to a secured object.</summary>
		// Token: 0x0400168C RID: 5772
		Deny
	}
}
