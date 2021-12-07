using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.SecurityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x0200061C RID: 1564
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SecurityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003B8A RID: 15242 RVA: 0x000CC948 File Offset: 0x000CAB48
		public SecurityPermissionAttribute(SecurityAction action) : base(action)
		{
			this.m_Flags = SecurityPermissionFlag.NoFlags;
		}

		/// <summary>Gets or sets a value indicating whether permission to assert that all this code's callers have the requisite permission for the operation is declared.</summary>
		/// <returns>true if permission to assert is declared; otherwise, false.</returns>
		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06003B8B RID: 15243 RVA: 0x000CC958 File Offset: 0x000CAB58
		// (set) Token: 0x06003B8C RID: 15244 RVA: 0x000CC968 File Offset: 0x000CAB68
		public bool Assertion
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.Assertion) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.Assertion;
				}
				else
				{
					this.m_Flags &= ~SecurityPermissionFlag.Assertion;
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether code has permission to perform binding redirection in the application configuration file.</summary>
		/// <returns>true if code can perform binding redirects; otherwise, false.</returns>
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06003B8D RID: 15245 RVA: 0x000CC9A0 File Offset: 0x000CABA0
		// (set) Token: 0x06003B8E RID: 15246 RVA: 0x000CC9B4 File Offset: 0x000CABB4
		public bool BindingRedirects
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.BindingRedirects) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.BindingRedirects;
				}
				else
				{
					this.m_Flags &= ~SecurityPermissionFlag.BindingRedirects;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to manipulate <see cref="T:System.AppDomain" /> is declared.</summary>
		/// <returns>true if permission to manipulate <see cref="T:System.AppDomain" /> is declared; otherwise, false.</returns>
		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06003B8F RID: 15247 RVA: 0x000CC9E8 File Offset: 0x000CABE8
		// (set) Token: 0x06003B90 RID: 15248 RVA: 0x000CC9FC File Offset: 0x000CABFC
		public bool ControlAppDomain
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlAppDomain) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlAppDomain;
				}
				else
				{
					this.m_Flags &= ~SecurityPermissionFlag.ControlAppDomain;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to alter or manipulate domain security policy is declared.</summary>
		/// <returns>true if permission to alter or manipulate security policy in an application domain is declared; otherwise, false.</returns>
		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06003B91 RID: 15249 RVA: 0x000CCA30 File Offset: 0x000CAC30
		// (set) Token: 0x06003B92 RID: 15250 RVA: 0x000CCA44 File Offset: 0x000CAC44
		public bool ControlDomainPolicy
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlDomainPolicy) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlDomainPolicy;
				}
				else
				{
					this.m_Flags &= ~SecurityPermissionFlag.ControlDomainPolicy;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to alter or manipulate evidence is declared.</summary>
		/// <returns>true if the ability to alter or manipulate evidence is declared; otherwise, false.</returns>
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06003B93 RID: 15251 RVA: 0x000CCA78 File Offset: 0x000CAC78
		// (set) Token: 0x06003B94 RID: 15252 RVA: 0x000CCA8C File Offset: 0x000CAC8C
		public bool ControlEvidence
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlEvidence) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlEvidence;
				}
				else
				{
					this.m_Flags &= ~SecurityPermissionFlag.ControlEvidence;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to view and manipulate security policy is declared.</summary>
		/// <returns>true if permission to manipulate security policy is declared; otherwise, false.</returns>
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06003B95 RID: 15253 RVA: 0x000CCAB8 File Offset: 0x000CACB8
		// (set) Token: 0x06003B96 RID: 15254 RVA: 0x000CCACC File Offset: 0x000CACCC
		public bool ControlPolicy
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlPolicy) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlPolicy;
				}
				else
				{
					this.m_Flags &= ~SecurityPermissionFlag.ControlPolicy;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to manipulate the current principal is declared.</summary>
		/// <returns>true if permission to manipulate the current principal is declared; otherwise, false.</returns>
		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06003B97 RID: 15255 RVA: 0x000CCAF8 File Offset: 0x000CACF8
		// (set) Token: 0x06003B98 RID: 15256 RVA: 0x000CCB0C File Offset: 0x000CAD0C
		public bool ControlPrincipal
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlPrincipal) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlPrincipal;
				}
				else
				{
					this.m_Flags &= ~SecurityPermissionFlag.ControlPrincipal;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to manipulate threads is declared.</summary>
		/// <returns>true if permission to manipulate threads is declared; otherwise, false.</returns>
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06003B99 RID: 15257 RVA: 0x000CCB40 File Offset: 0x000CAD40
		// (set) Token: 0x06003B9A RID: 15258 RVA: 0x000CCB54 File Offset: 0x000CAD54
		public bool ControlThread
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlThread) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlThread;
				}
				else
				{
					this.m_Flags &= ~SecurityPermissionFlag.ControlThread;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to execute code is declared.</summary>
		/// <returns>true if permission to execute code is declared; otherwise, false.</returns>
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06003B9B RID: 15259 RVA: 0x000CCB80 File Offset: 0x000CAD80
		// (set) Token: 0x06003B9C RID: 15260 RVA: 0x000CCB90 File Offset: 0x000CAD90
		public bool Execution
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.Execution) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.Execution;
				}
				else
				{
					this.m_Flags &= ~SecurityPermissionFlag.Execution;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether code can plug into the common language runtime infrastructure, such as adding Remoting Context Sinks, Envoy Sinks and Dynamic Sinks.</summary>
		/// <returns>true if code can plug into the common language runtime infrastructure; otherwise, false.</returns>
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06003B9D RID: 15261 RVA: 0x000CCBC8 File Offset: 0x000CADC8
		// (set) Token: 0x06003B9E RID: 15262 RVA: 0x000CCBDC File Offset: 0x000CADDC
		[ComVisible(true)]
		public bool Infrastructure
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.Infrastructure) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.Infrastructure;
				}
				else
				{
					this.m_Flags &= ~SecurityPermissionFlag.Infrastructure;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether code can configure remoting types and channels.</summary>
		/// <returns>true if code can configure remoting types and channels; otherwise, false.</returns>
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06003B9F RID: 15263 RVA: 0x000CCC10 File Offset: 0x000CAE10
		// (set) Token: 0x06003BA0 RID: 15264 RVA: 0x000CCC24 File Offset: 0x000CAE24
		public bool RemotingConfiguration
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.RemotingConfiguration) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.RemotingConfiguration;
				}
				else
				{
					this.m_Flags &= ~SecurityPermissionFlag.RemotingConfiguration;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether code can use a serialization formatter to serialize or deserialize an object.</summary>
		/// <returns>true if code can use a serialization formatter to serialize or deserialize an object; otherwise, false.</returns>
		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06003BA1 RID: 15265 RVA: 0x000CCC58 File Offset: 0x000CAE58
		// (set) Token: 0x06003BA2 RID: 15266 RVA: 0x000CCC6C File Offset: 0x000CAE6C
		public bool SerializationFormatter
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.SerializationFormatter) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.SerializationFormatter;
				}
				else
				{
					this.m_Flags &= ~SecurityPermissionFlag.SerializationFormatter;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to bypass code verification is declared.</summary>
		/// <returns>true if permission to bypass code verification is declared; otherwise, false.</returns>
		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06003BA3 RID: 15267 RVA: 0x000CCCA0 File Offset: 0x000CAEA0
		// (set) Token: 0x06003BA4 RID: 15268 RVA: 0x000CCCB0 File Offset: 0x000CAEB0
		public bool SkipVerification
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.SkipVerification) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.SkipVerification;
				}
				else
				{
					this.m_Flags &= ~SecurityPermissionFlag.SkipVerification;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to call unmanaged code is declared.</summary>
		/// <returns>true if permission to call unmanaged code is declared; otherwise, false.</returns>
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06003BA5 RID: 15269 RVA: 0x000CCCE8 File Offset: 0x000CAEE8
		// (set) Token: 0x06003BA6 RID: 15270 RVA: 0x000CCCF8 File Offset: 0x000CAEF8
		public bool UnmanagedCode
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.UnmanagedCode) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.UnmanagedCode;
				}
				else
				{
					this.m_Flags &= ~SecurityPermissionFlag.UnmanagedCode;
				}
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.SecurityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.SecurityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06003BA7 RID: 15271 RVA: 0x000CCD30 File Offset: 0x000CAF30
		public override IPermission CreatePermission()
		{
			SecurityPermission result;
			if (base.Unrestricted)
			{
				result = new SecurityPermission(PermissionState.Unrestricted);
			}
			else
			{
				result = new SecurityPermission(this.m_Flags);
			}
			return result;
		}

		/// <summary>Gets or sets all permission flags comprising the <see cref="T:System.Security.Permissions.SecurityPermission" /> permissions.</summary>
		/// <returns>One or more of the <see cref="T:System.Security.Permissions.SecurityPermissionFlag" /> values combined using a bitwise OR.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.SecurityPermissionFlag" /> for the valid values. </exception>
		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06003BA8 RID: 15272 RVA: 0x000CCD64 File Offset: 0x000CAF64
		// (set) Token: 0x06003BA9 RID: 15273 RVA: 0x000CCD6C File Offset: 0x000CAF6C
		public SecurityPermissionFlag Flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x040019ED RID: 6637
		private SecurityPermissionFlag m_Flags;
	}
}
