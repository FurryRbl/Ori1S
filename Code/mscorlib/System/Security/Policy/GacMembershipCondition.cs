using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its global assembly cache membership. This class cannot be inherited.</summary>
	// Token: 0x0200063F RID: 1599
	[ComVisible(true)]
	[Serializable]
	public sealed class GacMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IMembershipCondition
	{
		/// <summary>Indicates whether the specified evidence satisfies the membership condition.</summary>
		/// <returns>true if the specified evidence satisfies the membership condition; otherwise, false.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> against which to make the test. </param>
		// Token: 0x06003CE9 RID: 15593 RVA: 0x000D170C File Offset: 0x000CF90C
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				if (hostEnumerator.Current is GacInstalled)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new <see cref="T:System.Security.Policy.GacMembershipCondition" /> object.</returns>
		// Token: 0x06003CEA RID: 15594 RVA: 0x000D174C File Offset: 0x000CF94C
		public IMembershipCondition Copy()
		{
			return new GacMembershipCondition();
		}

		/// <summary>Indicates whether the current object is equivalent to the specified object.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.Security.Policy.GacMembershipCondition" />; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current object. </param>
		// Token: 0x06003CEB RID: 15595 RVA: 0x000D1754 File Offset: 0x000CF954
		public override bool Equals(object o)
		{
			return o != null && o is GacMembershipCondition;
		}

		/// <summary>Uses the specified XML encoding to reconstruct a security object.</summary>
		/// <param name="e">The <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> is not a valid membership condition element. </exception>
		// Token: 0x06003CEC RID: 15596 RVA: 0x000D1768 File Offset: 0x000CF968
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Uses the specified XML encoding to reconstruct a security object, using the specified policy level context.</summary>
		/// <param name="e">The <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for resolving <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> is not a valid membership condition element. </exception>
		// Token: 0x06003CED RID: 15597 RVA: 0x000D1774 File Offset: 0x000CF974
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
		}

		/// <summary>Gets a hash code for the current membership condition.</summary>
		/// <returns>0 (zero).</returns>
		// Token: 0x06003CEE RID: 15598 RVA: 0x000D1790 File Offset: 0x000CF990
		public override int GetHashCode()
		{
			return 0;
		}

		/// <summary>Returns a string representation of the membership condition.</summary>
		/// <returns>A string representation of the membership condition.</returns>
		// Token: 0x06003CEF RID: 15599 RVA: 0x000D1794 File Offset: 0x000CF994
		public override string ToString()
		{
			return "GAC";
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding of the security object, including any state information.</returns>
		// Token: 0x06003CF0 RID: 15600 RVA: 0x000D179C File Offset: 0x000CF99C
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state, using the specified policy level context.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for resolving <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		// Token: 0x06003CF1 RID: 15601 RVA: 0x000D17A8 File Offset: 0x000CF9A8
		public SecurityElement ToXml(PolicyLevel level)
		{
			return MembershipConditionHelper.Element(typeof(GacMembershipCondition), this.version);
		}

		// Token: 0x04001A71 RID: 6769
		private readonly int version = 1;
	}
}
