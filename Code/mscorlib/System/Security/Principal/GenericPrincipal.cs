using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Represents a generic principal.</summary>
	// Token: 0x0200065D RID: 1629
	[ComVisible(true)]
	[Serializable]
	public class GenericPrincipal : IPrincipal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericPrincipal" /> class from a user identity and an array of role names to which the user represented by that identity belongs.</summary>
		/// <param name="identity">A basic implementation of <see cref="T:System.Security.Principal.IIdentity" /> that represents any user. </param>
		/// <param name="roles">An array of role names to which the user represented by the <paramref name="identity" /> parameter belongs. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identity" /> parameter is null. </exception>
		// Token: 0x06003E0C RID: 15884 RVA: 0x000D58E8 File Offset: 0x000D3AE8
		public GenericPrincipal(IIdentity identity, string[] roles)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.m_identity = identity;
			if (roles != null)
			{
				this.m_roles = new string[roles.Length];
				for (int i = 0; i < roles.Length; i++)
				{
					this.m_roles[i] = roles[i];
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Principal.GenericIdentity" /> of the user represented by the current <see cref="T:System.Security.Principal.GenericPrincipal" />.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.GenericIdentity" /> of the user represented by the <see cref="T:System.Security.Principal.GenericPrincipal" />.</returns>
		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06003E0D RID: 15885 RVA: 0x000D5948 File Offset: 0x000D3B48
		public virtual IIdentity Identity
		{
			get
			{
				return this.m_identity;
			}
		}

		/// <summary>Determines whether the current <see cref="T:System.Security.Principal.GenericPrincipal" /> belongs to the specified role.</summary>
		/// <returns>true if the current <see cref="T:System.Security.Principal.GenericPrincipal" /> is a member of the specified role; otherwise, false.</returns>
		/// <param name="role">The name of the role for which to check membership. </param>
		// Token: 0x06003E0E RID: 15886 RVA: 0x000D5950 File Offset: 0x000D3B50
		public virtual bool IsInRole(string role)
		{
			if (this.m_roles == null)
			{
				return false;
			}
			int length = role.Length;
			foreach (string text in this.m_roles)
			{
				if (text != null && length == text.Length && string.Compare(role, 0, text, 0, length, true) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001AB1 RID: 6833
		private IIdentity m_identity;

		// Token: 0x04001AB2 RID: 6834
		private string[] m_roles;
	}
}
