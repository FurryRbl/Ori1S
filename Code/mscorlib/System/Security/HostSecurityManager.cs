using System;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
	/// <summary>Allows the control and customization of security behavior for application domains.</summary>
	// Token: 0x02000534 RID: 1332
	[ComVisible(true)]
	[Serializable]
	public class HostSecurityManager
	{
		/// <summary>When overridden in a derived class, gets the security policy for the current application domain.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.PolicyLevel" /> object. The default is null.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x000AC5D0 File Offset: 0x000AA7D0
		public virtual PolicyLevel DomainPolicy
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the flag representing the security policy components of concern to the host.</summary>
		/// <returns>One of the <see cref="T:System.Security.HostSecurityManagerOptions" /> values. The default is <see cref="F:System.Security.HostSecurityManagerOptions.AllFlags" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x000AC5D4 File Offset: 0x000AA7D4
		public virtual HostSecurityManagerOptions Flags
		{
			get
			{
				return HostSecurityManagerOptions.AllFlags;
			}
		}

		/// <summary>Determines whether an application should be executed.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.ApplicationTrust" /> object that contains trust information about the application.</returns>
		/// <param name="applicationEvidence">The <see cref="T:System.Security.Policy.Evidence" />  for the application to be activated.</param>
		/// <param name="activatorEvidence">Optionally, the <see cref="T:System.Security.Policy.Evidence" /> for the activating application domain. </param>
		/// <param name="context">A <see cref="T:System.Security.Policy.TrustManagerContext" /> that specifies the trust context. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationEvidence" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">An <see cref="T:System.Runtime.Hosting.ActivationArguments" /> object could not be found in the application evidence.-or-The <see cref="P:System.Runtime.Hosting.ActivationArguments.ActivationContext" /> property in the activation arguments is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Security.Policy.ApplicationTrust" /> grant set does not contain the minimum request set specified by the <see cref="T:System.ActivationContext" />.</exception>
		// Token: 0x06003487 RID: 13447 RVA: 0x000AC5D8 File Offset: 0x000AA7D8
		public virtual ApplicationTrust DetermineApplicationTrust(Evidence applicationEvidence, Evidence activatorEvidence, TrustManagerContext context)
		{
			if (applicationEvidence == null)
			{
				throw new ArgumentNullException("applicationEvidence");
			}
			ActivationArguments activationArguments = null;
			foreach (object obj in applicationEvidence)
			{
				activationArguments = (obj as ActivationArguments);
				if (activationArguments != null)
				{
					break;
				}
			}
			if (activationArguments == null)
			{
				string text = Locale.GetText("No {0} found in {1}.");
				throw new ArgumentException(string.Format(text, "ActivationArguments", "Evidence"), "applicationEvidence");
			}
			if (activationArguments.ActivationContext == null)
			{
				string text2 = Locale.GetText("No {0} found in {1}.");
				throw new ArgumentException(string.Format(text2, "ActivationContext", "ActivationArguments"), "applicationEvidence");
			}
			if (!ApplicationSecurityManager.DetermineApplicationTrust(activationArguments.ActivationContext, context))
			{
				return null;
			}
			if (activationArguments.ApplicationIdentity == null)
			{
				return new ApplicationTrust();
			}
			return new ApplicationTrust(activationArguments.ApplicationIdentity);
		}

		/// <summary>Provides the application domain evidence for an assembly being loaded.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object representing the evidence to be used for the <see cref="T:System.AppDomain" />.</returns>
		/// <param name="inputEvidence">Additional <see cref="T:System.Security.Policy.Evidence" /> to add to the <see cref="T:System.AppDomain" /> evidence.</param>
		// Token: 0x06003488 RID: 13448 RVA: 0x000AC6EC File Offset: 0x000AA8EC
		public virtual Evidence ProvideAppDomainEvidence(Evidence inputEvidence)
		{
			return inputEvidence;
		}

		/// <summary>Provides the assembly evidence for an assembly being loaded.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object representing the evidence to be used for the assembly.</returns>
		/// <param name="loadedAssembly">An <see cref="T:System.Reflection.Assembly" />  object representing the loaded assembly. </param>
		/// <param name="inputEvidence">Additional <see cref="T:System.Security.Policy.Evidence" /> to add to the assembly evidence.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003489 RID: 13449 RVA: 0x000AC6F0 File Offset: 0x000AA8F0
		public virtual Evidence ProvideAssemblyEvidence(Assembly loadedAssembly, Evidence inputEvidence)
		{
			return inputEvidence;
		}

		/// <summary>Determines what permissions to grant to code based on the specified evidence.</summary>
		/// <returns>The <see cref="T:System.Security.PermissionSet" /> that can be granted by the security system.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" />  set used to evaluate policy.</param>
		// Token: 0x0600348A RID: 13450 RVA: 0x000AC6F4 File Offset: 0x000AA8F4
		public virtual PermissionSet ResolvePolicy(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new NullReferenceException("evidence");
			}
			return SecurityManager.ResolvePolicy(evidence);
		}
	}
}
