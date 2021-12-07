using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Confirms that a code assembly originates in the global assembly cache (GAC) as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x0200063E RID: 1598
	[ComVisible(true)]
	[Serializable]
	public sealed class GacInstalled : IBuiltInEvidence, IIdentityPermissionFactory
	{
		// Token: 0x06003CE0 RID: 15584 RVA: 0x000D168C File Offset: 0x000CF88C
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return 1;
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x000D1690 File Offset: 0x000CF890
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return position;
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x000D1694 File Offset: 0x000CF894
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			buffer[position] = '\t';
			return position + 1;
		}

		/// <summary>Creates an equivalent copy of the current object.</summary>
		/// <returns>A new <see cref="T:System.Security.Policy.GacInstalled" /> object.</returns>
		// Token: 0x06003CE3 RID: 15587 RVA: 0x000D16A0 File Offset: 0x000CF8A0
		public object Copy()
		{
			return new GacInstalled();
		}

		/// <summary>Creates a new identity permission that corresponds to the current object.</summary>
		/// <returns>A new <see cref="T:System.Security.Permissions.GacIdentityPermission" />.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> from which to construct the identity permission. </param>
		// Token: 0x06003CE4 RID: 15588 RVA: 0x000D16A8 File Offset: 0x000CF8A8
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new GacIdentityPermission();
		}

		/// <summary>Indicates whether the current object is equivalent to the specified object.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.Security.Policy.GacInstalled" /> object; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current object. </param>
		// Token: 0x06003CE5 RID: 15589 RVA: 0x000D16B0 File Offset: 0x000CF8B0
		public override bool Equals(object o)
		{
			return o != null && o is GacInstalled;
		}

		/// <summary>Returns a hash code for the current object.</summary>
		/// <returns>0 (zero).</returns>
		// Token: 0x06003CE6 RID: 15590 RVA: 0x000D16C4 File Offset: 0x000CF8C4
		public override int GetHashCode()
		{
			return 0;
		}

		/// <summary>Returns a string representation of the current  object.</summary>
		/// <returns>A string representation of the current object.</returns>
		// Token: 0x06003CE7 RID: 15591 RVA: 0x000D16C8 File Offset: 0x000CF8C8
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			return securityElement.ToString();
		}
	}
}
