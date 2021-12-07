using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Holds the security evidence for an application. This class cannot be inherited.</summary>
	// Token: 0x0200062F RID: 1583
	[ComVisible(true)]
	public sealed class ApplicationSecurityInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationSecurityInfo" /> class using the provided activation context. </summary>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> object that uniquely identifies the target application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is null. </exception>
		// Token: 0x06003C42 RID: 15426 RVA: 0x000CF02C File Offset: 0x000CD22C
		public ApplicationSecurityInfo(ActivationContext activationContext)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			this._context = activationContext;
		}

		/// <summary>Gets or sets the evidence for the application.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object for the application.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.ApplicationEvidence" /> is set to null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x000CF04C File Offset: 0x000CD24C
		// (set) Token: 0x06003C44 RID: 15428 RVA: 0x000CF054 File Offset: 0x000CD254
		public Evidence ApplicationEvidence
		{
			get
			{
				return this._evidence;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ApplicationEvidence");
				}
				this._evidence = value;
			}
		}

		/// <summary>Gets or sets the application identity information.</summary>
		/// <returns>An <see cref="T:System.ApplicationId" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.ApplicationId" /> is set to null.</exception>
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x000CF070 File Offset: 0x000CD270
		// (set) Token: 0x06003C46 RID: 15430 RVA: 0x000CF078 File Offset: 0x000CD278
		public ApplicationId ApplicationId
		{
			get
			{
				return this._appid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ApplicationId");
				}
				this._appid = value;
			}
		}

		/// <summary>Gets or sets the default permission set.</summary>
		/// <returns>A <see cref="T:System.Security.PermissionSet" /> object representing the default permissions for the application. The default is a <see cref="T:System.Security.PermissionSet" /> with a permission state of <see cref="F:System.Security.Permissions.PermissionState.None" /></returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.DefaultRequestSet" /> is set to null. </exception>
		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x000CF094 File Offset: 0x000CD294
		// (set) Token: 0x06003C48 RID: 15432 RVA: 0x000CF0B0 File Offset: 0x000CD2B0
		public PermissionSet DefaultRequestSet
		{
			get
			{
				if (this._defaultSet == null)
				{
					return new PermissionSet(PermissionState.None);
				}
				return this._defaultSet;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("DefaultRequestSet");
				}
				this._defaultSet = value;
			}
		}

		/// <summary>Gets or sets the top element in the application, which is described in the deployment identity.</summary>
		/// <returns>An <see cref="T:System.ApplicationId" /> object describing the top element of the application.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.DeploymentId" /> is set to null. </exception>
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06003C49 RID: 15433 RVA: 0x000CF0CC File Offset: 0x000CD2CC
		// (set) Token: 0x06003C4A RID: 15434 RVA: 0x000CF0D4 File Offset: 0x000CD2D4
		public ApplicationId DeploymentId
		{
			get
			{
				return this._deployid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("DeploymentId");
				}
				this._deployid = value;
			}
		}

		// Token: 0x04001A25 RID: 6693
		private ActivationContext _context;

		// Token: 0x04001A26 RID: 6694
		private Evidence _evidence;

		// Token: 0x04001A27 RID: 6695
		private ApplicationId _appid;

		// Token: 0x04001A28 RID: 6696
		private PermissionSet _defaultSet;

		// Token: 0x04001A29 RID: 6697
		private ApplicationId _deployid;
	}
}
