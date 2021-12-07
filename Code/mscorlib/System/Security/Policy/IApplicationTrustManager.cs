using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Determines whether an application should be executed and which set of permissions should be granted to the application.</summary>
	// Token: 0x02000642 RID: 1602
	[ComVisible(true)]
	public interface IApplicationTrustManager : ISecurityEncodable
	{
		/// <summary>Determines whether an application should be executed and which set of permissions should be granted to the application.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.ApplicationTrust" />.</returns>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> identifying the activation context for the application.</param>
		/// <param name="context">A <see cref="T:System.Security.Policy.TrustManagerContext" />  identifying the trust manager context for the application.</param>
		// Token: 0x06003D12 RID: 15634
		ApplicationTrust DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context);
	}
}
