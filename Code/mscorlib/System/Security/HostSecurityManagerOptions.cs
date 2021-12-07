using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Specifies the security policy components to be used by the host security manager.</summary>
	// Token: 0x02000535 RID: 1333
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum HostSecurityManagerOptions
	{
		/// <summary>Use none of the security policy components.</summary>
		// Token: 0x04001618 RID: 5656
		None = 0,
		/// <summary>Use the application domain evidence.</summary>
		// Token: 0x04001619 RID: 5657
		HostAppDomainEvidence = 1,
		/// <summary>Use the policy level specified in the <see cref="P:System.Security.HostSecurityManager.DomainPolicy" /> property.</summary>
		// Token: 0x0400161A RID: 5658
		HostPolicyLevel = 2,
		/// <summary>Use the assembly evidence.</summary>
		// Token: 0x0400161B RID: 5659
		HostAssemblyEvidence = 4,
		/// <summary>Route calls to the <see cref="M:System.Security.Policy.ApplicationSecurityManager.DetermineApplicationTrust(System.ActivationContext,System.Security.Policy.TrustManagerContext)" /> method to the <see cref="M:System.Security.HostSecurityManager.DetermineApplicationTrust(System.Security.Policy.Evidence,System.Security.Policy.Evidence,System.Security.Policy.TrustManagerContext)" /> method first.</summary>
		// Token: 0x0400161C RID: 5660
		HostDetermineApplicationTrust = 8,
		/// <summary>Use the <see cref="M:System.Security.HostSecurityManager.ResolvePolicy(System.Security.Policy.Evidence)" /> method to resolve the application evidence.</summary>
		// Token: 0x0400161D RID: 5661
		HostResolvePolicy = 16,
		/// <summary>Use all security policy components.</summary>
		// Token: 0x0400161E RID: 5662
		AllFlags = 31
	}
}
