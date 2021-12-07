using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Defines the method that creates a new identity permission.</summary>
	// Token: 0x02000645 RID: 1605
	[ComVisible(true)]
	public interface IIdentityPermissionFactory
	{
		/// <summary>Creates a new identity permission for the specified evidence.</summary>
		/// <returns>The new identity permission.</returns>
		/// <param name="evidence">The evidence from which to create the new identity permission. </param>
		// Token: 0x06003D16 RID: 15638
		IPermission CreateIdentityPermission(Evidence evidence);
	}
}
