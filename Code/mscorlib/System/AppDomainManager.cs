using System;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System
{
	/// <summary>Provides a managed equivalent of an unmanaged host.</summary>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. See the Requirements section.</exception>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FA RID: 250
	[ComVisible(true)]
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"Infrastructure\"/>\n</PermissionSet>\n")]
	[PermissionSet(SecurityAction.InheritanceDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"Infrastructure\"/>\n</PermissionSet>\n")]
	public class AppDomainManager : MarshalByRefObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainManager" /> class. </summary>
		// Token: 0x06000D1C RID: 3356 RVA: 0x0003A2A4 File Offset: 0x000384A4
		public AppDomainManager()
		{
			this._flags = AppDomainManagerInitializationOptions.None;
		}

		/// <summary>Gets the application activator that handles the activation of add-ins and manifest-based applications for the domain.</summary>
		/// <returns>An <see cref="T:System.Runtime.Hosting.ApplicationActivator" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0003A2B4 File Offset: 0x000384B4
		public virtual ApplicationActivator ApplicationActivator
		{
			get
			{
				if (this._activator == null)
				{
					this._activator = new ApplicationActivator();
				}
				return this._activator;
			}
		}

		/// <summary>Gets the entry assembly for an application.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object representing the entry assembly for the application.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0003A2D4 File Offset: 0x000384D4
		public virtual Assembly EntryAssembly
		{
			get
			{
				return Assembly.GetEntryAssembly();
			}
		}

		/// <summary>Gets the host execution context manager that manages the flow of the execution context.</summary>
		/// <returns>A <see cref="T:System.Threading.HostExecutionContextManager" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0003A2DC File Offset: 0x000384DC
		[MonoTODO]
		public virtual HostExecutionContextManager HostExecutionContextManager
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the host security manager that participates in security decisions for the application domain.</summary>
		/// <returns>A <see cref="T:System.Security.HostSecurityManager" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0003A2E4 File Offset: 0x000384E4
		public virtual HostSecurityManager HostSecurityManager
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the initialization flags for custom application domain managers.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.AppDomainManagerInitializationOptions" /> describing the initialization action to perform. The default is <see cref="F:System.AppDomainManagerInitializationOptions.None" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0003A2E8 File Offset: 0x000384E8
		// (set) Token: 0x06000D22 RID: 3362 RVA: 0x0003A2F0 File Offset: 0x000384F0
		public AppDomainManagerInitializationOptions InitializationFlags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		/// <summary>Returns an application domain that can be either a new or existing domain.</summary>
		/// <returns>An <see cref="T:System.AppDomain" /> object.</returns>
		/// <param name="friendlyName">The friendly name of the domain. </param>
		/// <param name="securityInfo">An <see cref="T:System.Security.Policy.Evidence" /> object that contains evidence mapped through the security policy to establish a top-of-stack permission set.</param>
		/// <param name="appDomainInfo">An <see cref="T:System.AppDomainSetup" /> object that contains application domain initialization information.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06000D23 RID: 3363 RVA: 0x0003A2FC File Offset: 0x000384FC
		public virtual AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup appDomainInfo)
		{
			this.InitializeNewDomain(appDomainInfo);
			AppDomain appDomain = AppDomainManager.CreateDomainHelper(friendlyName, securityInfo, appDomainInfo);
			if ((this.HostSecurityManager.Flags & HostSecurityManagerOptions.HostPolicyLevel) == HostSecurityManagerOptions.HostPolicyLevel)
			{
				PolicyLevel domainPolicy = this.HostSecurityManager.DomainPolicy;
				if (domainPolicy != null)
				{
					appDomain.SetAppDomainPolicy(domainPolicy);
				}
			}
			return appDomain;
		}

		/// <summary>Initializes the new application domain.</summary>
		/// <param name="appDomainInfo">An <see cref="T:System.AppDomainSetup" /> object that contains application domain initialization information.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06000D24 RID: 3364 RVA: 0x0003A348 File Offset: 0x00038548
		public virtual void InitializeNewDomain(AppDomainSetup appDomainInfo)
		{
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> indicating whether the specified operation is allowed in the application domain.</summary>
		/// <returns>true if the host allows the operation specified by <paramref name="state" /> to be performed in the application domain; otherwise false.</returns>
		/// <param name="state">A subclass of <see cref="T:System.Security.SecurityState" /> that identifies the operation whose security status is requested.</param>
		// Token: 0x06000D25 RID: 3365 RVA: 0x0003A34C File Offset: 0x0003854C
		public virtual bool CheckSecuritySettings(SecurityState state)
		{
			return false;
		}

		/// <summary>Provides a helper method to create an application domain.</summary>
		/// <returns>A newly created <see cref="T:System.AppDomain" /> object.</returns>
		/// <param name="friendlyName">The friendly name of the domain. </param>
		/// <param name="securityInfo">An <see cref="T:System.Security.Policy.Evidence" /> object that contains evidence mapped through the security policy to establish a top-of-stack permission set.</param>
		/// <param name="appDomainInfo">An <see cref="T:System.AppDomainSetup" /> object that contains application domain initialization information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is null. </exception>
		// Token: 0x06000D26 RID: 3366 RVA: 0x0003A350 File Offset: 0x00038550
		protected static AppDomain CreateDomainHelper(string friendlyName, Evidence securityInfo, AppDomainSetup appDomainInfo)
		{
			return AppDomain.CreateDomain(friendlyName, securityInfo, appDomainInfo);
		}

		// Token: 0x0400037D RID: 893
		private ApplicationActivator _activator;

		// Token: 0x0400037E RID: 894
		private AppDomainManagerInitializationOptions _flags;
	}
}
