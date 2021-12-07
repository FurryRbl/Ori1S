using System;

namespace System.Security
{
	/// <summary>Specifies that code or an assembly performs security-critical operations.</summary>
	// Token: 0x02000541 RID: 1345
	[MonoTODO("Only supported by the runtime when CoreCLR is enabled")]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class SecurityCriticalAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityCriticalAttribute" /> class with default scope. </summary>
		// Token: 0x06003503 RID: 13571 RVA: 0x000AEB5C File Offset: 0x000ACD5C
		public SecurityCriticalAttribute()
		{
			this._scope = SecurityCriticalScope.Explicit;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityCriticalAttribute" /> class with the specified scope. </summary>
		/// <param name="scope">One of the <see cref="T:System.Security.SecurityCriticalScope" /> values that specifies the scope of the attribute. </param>
		// Token: 0x06003504 RID: 13572 RVA: 0x000AEB6C File Offset: 0x000ACD6C
		public SecurityCriticalAttribute(SecurityCriticalScope scope)
		{
			if (scope != SecurityCriticalScope.Everything)
			{
				this._scope = SecurityCriticalScope.Explicit;
			}
			else
			{
				this._scope = SecurityCriticalScope.Everything;
			}
		}

		/// <summary>Gets the scope for the attribute.</summary>
		/// <returns>One of the <see cref="T:System.Security.SecurityCriticalScope" /> values that specifies the scope of the attribute. The default is <see cref="F:System.Security.SecurityCriticalScope.Explicit" />.</returns>
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06003505 RID: 13573 RVA: 0x000AEBA8 File Offset: 0x000ACDA8
		public SecurityCriticalScope Scope
		{
			get
			{
				return this._scope;
			}
		}

		// Token: 0x0400163E RID: 5694
		private SecurityCriticalScope _scope;
	}
}
