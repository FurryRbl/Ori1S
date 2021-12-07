using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the base attribute class for declarative security from which <see cref="T:System.Security.Permissions.CodeAccessSecurityAttribute" /> is derived.</summary>
	// Token: 0x02000046 RID: 70
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public abstract class SecurityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Permissions.SecurityAttribute" /> with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06000662 RID: 1634 RVA: 0x00014AE8 File Offset: 0x00012CE8
		protected SecurityAttribute(SecurityAction action)
		{
			this.Action = action;
		}

		/// <summary>When overridden in a derived class, creates a permission object that can then be serialized into binary form and persistently stored along with the <see cref="T:System.Security.Permissions.SecurityAction" /> in an assembly's metadata.</summary>
		/// <returns>A serializable permission object.</returns>
		// Token: 0x06000663 RID: 1635
		public abstract IPermission CreatePermission();

		/// <summary>Gets or sets a value indicating whether full (unrestricted) permission to the resource protected by the attribute is declared.</summary>
		/// <returns>true if full permission to the protected resource is declared; otherwise, false.</returns>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00014AF8 File Offset: 0x00012CF8
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x00014B00 File Offset: 0x00012D00
		public bool Unrestricted
		{
			get
			{
				return this.m_Unrestricted;
			}
			set
			{
				this.m_Unrestricted = value;
			}
		}

		/// <summary>Gets or sets a security action.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</returns>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00014B0C File Offset: 0x00012D0C
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x00014B14 File Offset: 0x00012D14
		public SecurityAction Action
		{
			get
			{
				return this.m_Action;
			}
			set
			{
				this.m_Action = value;
			}
		}

		// Token: 0x0400009B RID: 155
		private SecurityAction m_Action;

		// Token: 0x0400009C RID: 156
		private bool m_Unrestricted;
	}
}
