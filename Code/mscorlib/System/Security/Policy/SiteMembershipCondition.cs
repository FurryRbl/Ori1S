using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing the site from which it originated. This class cannot be inherited.</summary>
	// Token: 0x02000652 RID: 1618
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IMembershipCondition
	{
		// Token: 0x06003D95 RID: 15765 RVA: 0x000D4484 File Offset: 0x000D2684
		internal SiteMembershipCondition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.SiteMembershipCondition" /> class with name of the site that determines membership.</summary>
		/// <param name="site">The site name or wildcard expression. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="site" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="site" /> parameter is not a valid <see cref="T:System.Security.Policy.Site" />. </exception>
		// Token: 0x06003D96 RID: 15766 RVA: 0x000D4494 File Offset: 0x000D2694
		public SiteMembershipCondition(string site)
		{
			this.Site = site;
		}

		/// <summary>Gets or sets the site for which the membership condition tests.</summary>
		/// <returns>The site for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> to null. </exception>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> to an invalid <see cref="T:System.Security.Policy.Site" />. </exception>
		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06003D97 RID: 15767 RVA: 0x000D44AC File Offset: 0x000D26AC
		// (set) Token: 0x06003D98 RID: 15768 RVA: 0x000D44B4 File Offset: 0x000D26B4
		public string Site
		{
			get
			{
				return this._site;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("site");
				}
				if (!System.Security.Policy.Site.IsValid(value))
				{
					throw new ArgumentException("invalid site");
				}
				this._site = value;
			}
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <returns>true if the specified evidence satisfies the membership condition; otherwise, false.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> against which to make the test. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> property is null. </exception>
		// Token: 0x06003D99 RID: 15769 RVA: 0x000D44F0 File Offset: 0x000D26F0
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				if (hostEnumerator.Current is Site)
				{
					string[] array = this._site.Split(new char[]
					{
						'.'
					});
					string[] array2 = (hostEnumerator.Current as Site).origin_site.Split(new char[]
					{
						'.'
					});
					int i = array.Length - 1;
					int num = array2.Length - 1;
					while (i >= 0)
					{
						if (i == 0)
						{
							return string.Compare(array[0], "*", true, CultureInfo.InvariantCulture) == 0;
						}
						if (string.Compare(array[i], array2[num], true, CultureInfo.InvariantCulture) != 0)
						{
							return false;
						}
						i--;
						num--;
					}
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> property is null. </exception>
		// Token: 0x06003D9A RID: 15770 RVA: 0x000D45C0 File Offset: 0x000D27C0
		public IMembershipCondition Copy()
		{
			return new SiteMembershipCondition(this._site);
		}

		/// <summary>Determines whether the site from the specified <see cref="T:System.Security.Policy.SiteMembershipCondition" /> object is equivalent to the site contained in the current <see cref="T:System.Security.Policy.SiteMembershipCondition" />.</summary>
		/// <returns>true if the site from the specified <see cref="T:System.Security.Policy.SiteMembershipCondition" /> object is equivalent to the site contained in the current <see cref="T:System.Security.Policy.SiteMembershipCondition" />; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Policy.SiteMembershipCondition" /> object to compare to the current <see cref="T:System.Security.Policy.SiteMembershipCondition" />. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> property for the current object or the specified object is null. </exception>
		// Token: 0x06003D9B RID: 15771 RVA: 0x000D45D0 File Offset: 0x000D27D0
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			if (o is SiteMembershipCondition)
			{
				Site site = new Site((o as SiteMembershipCondition)._site);
				return site.Equals(new Site(this._site));
			}
			return false;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06003D9C RID: 15772 RVA: 0x000D4614 File Offset: 0x000D2814
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06003D9D RID: 15773 RVA: 0x000D4620 File Offset: 0x000D2820
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			this._site = e.Attribute("Site");
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003D9E RID: 15774 RVA: 0x000D464C File Offset: 0x000D284C
		public override int GetHashCode()
		{
			return this._site.GetHashCode();
		}

		/// <summary>Creates and returns a string representation of the membership condition.</summary>
		/// <returns>A string representation of the membership condition.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> property is null. </exception>
		// Token: 0x06003D9F RID: 15775 RVA: 0x000D465C File Offset: 0x000D285C
		public override string ToString()
		{
			return "Site - " + this._site;
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> property is null. </exception>
		// Token: 0x06003DA0 RID: 15776 RVA: 0x000D4670 File Offset: 0x000D2870
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> property is null. </exception>
		// Token: 0x06003DA1 RID: 15777 RVA: 0x000D467C File Offset: 0x000D287C
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(SiteMembershipCondition), this.version);
			securityElement.AddAttribute("Site", this._site);
			return securityElement;
		}

		// Token: 0x04001A95 RID: 6805
		private readonly int version = 1;

		// Token: 0x04001A96 RID: 6806
		private string _site;
	}
}
