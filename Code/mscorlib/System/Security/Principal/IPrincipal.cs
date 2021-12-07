using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines the basic functionality of a principal object.</summary>
	// Token: 0x02000662 RID: 1634
	[ComVisible(true)]
	public interface IPrincipal
	{
		/// <summary>Gets the identity of the current principal.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IIdentity" /> object associated with the current principal.</returns>
		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06003E2F RID: 15919
		IIdentity Identity { get; }

		/// <summary>Determines whether the current principal belongs to the specified role.</summary>
		/// <returns>true if the current principal is a member of the specified role; otherwise, false.</returns>
		/// <param name="role">The name of the role for which to check membership. </param>
		// Token: 0x06003E30 RID: 15920
		bool IsInRole(string role);
	}
}
