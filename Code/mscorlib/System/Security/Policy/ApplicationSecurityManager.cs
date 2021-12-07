using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Manages trust decisions for manifest activated applications. </summary>
	// Token: 0x02000630 RID: 1584
	[ComVisible(true)]
	public static class ApplicationSecurityManager
	{
		/// <summary>Gets or sets the current application trust manager.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.IApplicationTrustManager" /> that represents the current trust manager.</returns>
		/// <exception cref="T:System.Security.Policy.PolicyException">The policy on this application does not have a trust manager.</exception>
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x000CF0F0 File Offset: 0x000CD2F0
		public static IApplicationTrustManager ApplicationTrustManager
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				if (ApplicationSecurityManager._appTrustManager == null)
				{
					ApplicationSecurityManager._appTrustManager = new MonoTrustManager();
				}
				return ApplicationSecurityManager._appTrustManager;
			}
		}

		/// <summary>Gets an application trust collection that contains the cached trust decisions for the user.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> that contains the cached trust decisions for the user.</returns>
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06003C4C RID: 15436 RVA: 0x000CF10C File Offset: 0x000CD30C
		public static ApplicationTrustCollection UserApplicationTrusts
		{
			get
			{
				if (ApplicationSecurityManager._userAppTrusts == null)
				{
					ApplicationSecurityManager._userAppTrusts = new ApplicationTrustCollection();
				}
				return ApplicationSecurityManager._userAppTrusts;
			}
		}

		/// <summary>Determines whether the user approves the specified application to execute with the requested permission set.</summary>
		/// <returns>true to execute the specified application; otherwise, false.</returns>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> identifying the activation context for the application.</param>
		/// <param name="context">A <see cref="T:System.Security.Policy.TrustManagerContext" />  identifying the trust manager context for the application.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="activationContext" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003C4D RID: 15437 RVA: 0x000CF128 File Offset: 0x000CD328
		[MonoTODO("Missing application manifest support")]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
		public static bool DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context)
		{
			if (activationContext == null)
			{
				throw new NullReferenceException("activationContext");
			}
			ApplicationTrust applicationTrust = ApplicationSecurityManager.ApplicationTrustManager.DetermineApplicationTrust(activationContext, context);
			return applicationTrust.IsApplicationTrustedToRun;
		}

		// Token: 0x04001A2A RID: 6698
		private const string config = "ApplicationTrust.config";

		// Token: 0x04001A2B RID: 6699
		private static IApplicationTrustManager _appTrustManager;

		// Token: 0x04001A2C RID: 6700
		private static ApplicationTrustCollection _userAppTrusts;
	}
}
