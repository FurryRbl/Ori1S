using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows the use of declarative security actions to determine host protection requirements. This class cannot be inherited.</summary>
	// Token: 0x020005FC RID: 1532
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class HostProtectionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.HostProtectionAttribute" /> class with default values.</summary>
		// Token: 0x06003A7C RID: 14972 RVA: 0x000C8E6C File Offset: 0x000C706C
		public HostProtectionAttribute() : base(SecurityAction.LinkDemand)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.HostProtectionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" /> value.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not <see cref="F:System.Security.Permissions.SecurityAction.LinkDemand" />. </exception>
		// Token: 0x06003A7D RID: 14973 RVA: 0x000C8E78 File Offset: 0x000C7078
		public HostProtectionAttribute(SecurityAction action) : base(action)
		{
			if (action != SecurityAction.LinkDemand)
			{
				string message = string.Format(Locale.GetText("Only {0} is accepted."), SecurityAction.LinkDemand);
				throw new ArgumentException(message, "action");
			}
		}

		/// <summary>Gets or sets a value indicating whether external process management is exposed.</summary>
		/// <returns>true if external process management is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x000C8EB8 File Offset: 0x000C70B8
		// (set) Token: 0x06003A7F RID: 14975 RVA: 0x000C8EC8 File Offset: 0x000C70C8
		public bool ExternalProcessMgmt
		{
			get
			{
				return (this._resources & HostProtectionResource.ExternalProcessMgmt) != HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.ExternalProcessMgmt;
				}
				else
				{
					this._resources &= ~HostProtectionResource.ExternalProcessMgmt;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether external threading is exposed.</summary>
		/// <returns>true if external threading is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06003A80 RID: 14976 RVA: 0x000C8F00 File Offset: 0x000C7100
		// (set) Token: 0x06003A81 RID: 14977 RVA: 0x000C8F14 File Offset: 0x000C7114
		public bool ExternalThreading
		{
			get
			{
				return (this._resources & HostProtectionResource.ExternalThreading) != HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.ExternalThreading;
				}
				else
				{
					this._resources &= ~HostProtectionResource.ExternalThreading;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether resources might leak memory if the operation is terminated.</summary>
		/// <returns>true if resources might leak memory on termination; otherwise, false.</returns>
		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06003A82 RID: 14978 RVA: 0x000C8F40 File Offset: 0x000C7140
		// (set) Token: 0x06003A83 RID: 14979 RVA: 0x000C8F54 File Offset: 0x000C7154
		public bool MayLeakOnAbort
		{
			get
			{
				return (this._resources & HostProtectionResource.MayLeakOnAbort) != HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.MayLeakOnAbort;
				}
				else
				{
					this._resources &= ~HostProtectionResource.MayLeakOnAbort;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the security infrastructure is exposed.</summary>
		/// <returns>true if the security infrastructure is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06003A84 RID: 14980 RVA: 0x000C8F88 File Offset: 0x000C7188
		// (set) Token: 0x06003A85 RID: 14981 RVA: 0x000C8F9C File Offset: 0x000C719C
		[ComVisible(true)]
		public bool SecurityInfrastructure
		{
			get
			{
				return (this._resources & HostProtectionResource.SecurityInfrastructure) != HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SecurityInfrastructure;
				}
				else
				{
					this._resources &= ~HostProtectionResource.SecurityInfrastructure;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether self-affecting process management is exposed.</summary>
		/// <returns>true if self-affecting process management is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06003A86 RID: 14982 RVA: 0x000C8FC8 File Offset: 0x000C71C8
		// (set) Token: 0x06003A87 RID: 14983 RVA: 0x000C8FD8 File Offset: 0x000C71D8
		public bool SelfAffectingProcessMgmt
		{
			get
			{
				return (this._resources & HostProtectionResource.SelfAffectingProcessMgmt) != HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SelfAffectingProcessMgmt;
				}
				else
				{
					this._resources &= ~HostProtectionResource.SelfAffectingProcessMgmt;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether self-affecting threading is exposed.</summary>
		/// <returns>true if self-affecting threading is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06003A88 RID: 14984 RVA: 0x000C9010 File Offset: 0x000C7210
		// (set) Token: 0x06003A89 RID: 14985 RVA: 0x000C9024 File Offset: 0x000C7224
		public bool SelfAffectingThreading
		{
			get
			{
				return (this._resources & HostProtectionResource.SelfAffectingThreading) != HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SelfAffectingThreading;
				}
				else
				{
					this._resources &= ~HostProtectionResource.SelfAffectingThreading;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether shared state is exposed.</summary>
		/// <returns>true if shared state is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06003A8A RID: 14986 RVA: 0x000C9050 File Offset: 0x000C7250
		// (set) Token: 0x06003A8B RID: 14987 RVA: 0x000C9060 File Offset: 0x000C7260
		public bool SharedState
		{
			get
			{
				return (this._resources & HostProtectionResource.SharedState) != HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SharedState;
				}
				else
				{
					this._resources &= ~HostProtectionResource.SharedState;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether synchronization is exposed.</summary>
		/// <returns>true if synchronization is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000C9098 File Offset: 0x000C7298
		// (set) Token: 0x06003A8D RID: 14989 RVA: 0x000C90A8 File Offset: 0x000C72A8
		public bool Synchronization
		{
			get
			{
				return (this._resources & HostProtectionResource.Synchronization) != HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.Synchronization;
				}
				else
				{
					this._resources &= ~HostProtectionResource.Synchronization;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the user interface is exposed.</summary>
		/// <returns>true if the user interface is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x000C90E0 File Offset: 0x000C72E0
		// (set) Token: 0x06003A8F RID: 14991 RVA: 0x000C90F4 File Offset: 0x000C72F4
		public bool UI
		{
			get
			{
				return (this._resources & HostProtectionResource.UI) != HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.UI;
				}
				else
				{
					this._resources &= ~HostProtectionResource.UI;
				}
			}
		}

		/// <summary>Gets or sets flags specifying categories of functionality that are potentially harmful to the host.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.HostProtectionResource" /> values. The default is <see cref="F:System.Security.Permissions.HostProtectionResource.None" />.</returns>
		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06003A90 RID: 14992 RVA: 0x000C9128 File Offset: 0x000C7328
		// (set) Token: 0x06003A91 RID: 14993 RVA: 0x000C9130 File Offset: 0x000C7330
		public HostProtectionResource Resources
		{
			get
			{
				return this._resources;
			}
			set
			{
				this._resources = value;
			}
		}

		/// <summary>Creates and returns a new host protection permission.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that corresponds to the current attribute.</returns>
		// Token: 0x06003A92 RID: 14994 RVA: 0x000C913C File Offset: 0x000C733C
		public override IPermission CreatePermission()
		{
			return new HostProtectionPermission(this._resources);
		}

		// Token: 0x0400195B RID: 6491
		private HostProtectionResource _resources;
	}
}
