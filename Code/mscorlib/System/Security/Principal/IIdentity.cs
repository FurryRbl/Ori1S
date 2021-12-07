using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines the basic functionality of an identity object.</summary>
	// Token: 0x02000661 RID: 1633
	[ComVisible(true)]
	public interface IIdentity
	{
		/// <summary>Gets the type of authentication used.</summary>
		/// <returns>The type of authentication used to identify the user.</returns>
		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06003E2C RID: 15916
		string AuthenticationType { get; }

		/// <summary>Gets a value that indicates whether the user has been authenticated.</summary>
		/// <returns>true if the user was authenticated; otherwise, false.</returns>
		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06003E2D RID: 15917
		bool IsAuthenticated { get; }

		/// <summary>Gets the name of the current user.</summary>
		/// <returns>The name of the user on whose behalf the code is running.</returns>
		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06003E2E RID: 15918
		string Name { get; }
	}
}
