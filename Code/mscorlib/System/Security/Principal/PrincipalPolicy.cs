using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Specifies how principal and identity objects should be created for an application domain. The default is UnauthenticatedPrincipal.</summary>
	// Token: 0x02000664 RID: 1636
	[ComVisible(true)]
	[Serializable]
	public enum PrincipalPolicy
	{
		/// <summary>Principal and identity objects for the unauthenticated entity should be created. An unauthenticated entity has <see cref="P:System.Security.Principal.GenericIdentity.Name" /> set to the empty string ("") and <see cref="P:System.Security.Principal.GenericIdentity.IsAuthenticated" /> set to false.</summary>
		// Token: 0x04001AB7 RID: 6839
		UnauthenticatedPrincipal,
		/// <summary>No principal or identity objects should be created.</summary>
		// Token: 0x04001AB8 RID: 6840
		NoPrincipal,
		/// <summary>Principal and identity objects that reflect the operating system token associated with the current execution thread should be created, and the associated operating system groups should be mapped into roles.</summary>
		// Token: 0x04001AB9 RID: 6841
		WindowsPrincipal
	}
}
