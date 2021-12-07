using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Controls rights to access Domain Name System (DNS) servers on the network.</summary>
	// Token: 0x020002FE RID: 766
	[Serializable]
	public sealed class DnsPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.DnsPermission" /> class that either allows unrestricted DNS access or disallows DNS access.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value. </exception>
		// Token: 0x06001A41 RID: 6721 RVA: 0x00049218 File Offset: 0x00047418
		public DnsPermission(PermissionState state)
		{
			this.m_noRestriction = (state == PermissionState.Unrestricted);
		}

		/// <summary>Creates an identical copy of the current permission instance.</summary>
		/// <returns>A new instance of the <see cref="T:System.Net.DnsPermission" /> class that is an identical copy of the current instance.</returns>
		// Token: 0x06001A42 RID: 6722 RVA: 0x0004922C File Offset: 0x0004742C
		public override IPermission Copy()
		{
			return new DnsPermission((!this.m_noRestriction) ? PermissionState.None : PermissionState.Unrestricted);
		}

		/// <summary>Creates a permission instance that is the intersection of the current permission instance and the specified permission instance.</summary>
		/// <returns>A <see cref="T:System.Net.DnsPermission" /> instance that represents the intersection of the current <see cref="T:System.Net.DnsPermission" /> instance with the specified <see cref="T:System.Net.DnsPermission" /> instance, or null if the intersection is empty. If both the current instance and <paramref name="target" /> are unrestricted, this method returns a new <see cref="T:System.Net.DnsPermission" /> instance that is unrestricted; otherwise, it returns null.</returns>
		/// <param name="target">The <see cref="T:System.Net.DnsPermission" /> instance to intersect with the current instance. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is neither a <see cref="T:System.Net.DnsPermission" /> nor null. </exception>
		// Token: 0x06001A43 RID: 6723 RVA: 0x00049248 File Offset: 0x00047448
		public override IPermission Intersect(IPermission target)
		{
			DnsPermission dnsPermission = this.Cast(target);
			if (dnsPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted() && dnsPermission.IsUnrestricted())
			{
				return new DnsPermission(PermissionState.Unrestricted);
			}
			return null;
		}

		/// <summary>Determines whether the current permission instance is a subset of the specified permission instance.</summary>
		/// <returns>false if the current instance is unrestricted and <paramref name="target" /> is either null or unrestricted; otherwise, true.</returns>
		/// <param name="target">The second <see cref="T:System.Net.DnsPermission" /> instance to be tested for the subset relationship. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is neither a <see cref="T:System.Net.DnsPermission" /> nor null. </exception>
		// Token: 0x06001A44 RID: 6724 RVA: 0x00049284 File Offset: 0x00047484
		public override bool IsSubsetOf(IPermission target)
		{
			DnsPermission dnsPermission = this.Cast(target);
			if (dnsPermission == null)
			{
				return this.IsEmpty();
			}
			return dnsPermission.IsUnrestricted() || this.m_noRestriction == dnsPermission.m_noRestriction;
		}

		/// <summary>Checks the overall permission state of the object.</summary>
		/// <returns>true if the <see cref="T:System.Net.DnsPermission" /> instance was created with <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" />; otherwise, false.</returns>
		// Token: 0x06001A45 RID: 6725 RVA: 0x000492C4 File Offset: 0x000474C4
		public bool IsUnrestricted()
		{
			return this.m_noRestriction;
		}

		/// <summary>Creates an XML encoding of a <see cref="T:System.Net.DnsPermission" /> instance and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> instance that contains an XML-encoded representation of the security object, including state information.</returns>
		// Token: 0x06001A46 RID: 6726 RVA: 0x000492CC File Offset: 0x000474CC
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(typeof(DnsPermission), 1);
			if (this.m_noRestriction)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		/// <summary>Reconstructs a <see cref="T:System.Net.DnsPermission" /> instance from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the <see cref="T:System.Net.DnsPermission" /> instance. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a <see cref="T:System.Net.DnsPermission" /> element. </exception>
		// Token: 0x06001A47 RID: 6727 RVA: 0x00049308 File Offset: 0x00047508
		public override void FromXml(SecurityElement securityElement)
		{
			PermissionHelper.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			if (securityElement.Tag != "IPermission")
			{
				throw new ArgumentException("securityElement");
			}
			this.m_noRestriction = PermissionHelper.IsUnrestricted(securityElement);
		}

		/// <summary>Creates a permission instance that is the union of the current permission instance and the specified permission instance.</summary>
		/// <returns>A <see cref="T:System.Net.DnsPermission" /> instance that represents the union of the current <see cref="T:System.Net.DnsPermission" /> instance with the specified <see cref="T:System.Net.DnsPermission" /> instance. If <paramref name="target" /> is null, this method returns a copy of the current instance. If the current instance or <paramref name="target" /> is unrestricted, this method returns a <see cref="T:System.Net.DnsPermission" /> instance that is unrestricted; otherwise, it returns a <see cref="T:System.Net.DnsPermission" /> instance that is restricted.</returns>
		/// <param name="target">The <see cref="T:System.Net.DnsPermission" /> instance to combine with the current instance. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is neither a <see cref="T:System.Net.DnsPermission" /> nor null. </exception>
		// Token: 0x06001A48 RID: 6728 RVA: 0x00049350 File Offset: 0x00047550
		public override IPermission Union(IPermission target)
		{
			DnsPermission dnsPermission = this.Cast(target);
			if (dnsPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || dnsPermission.IsUnrestricted())
			{
				return new DnsPermission(PermissionState.Unrestricted);
			}
			return new DnsPermission(PermissionState.None);
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00049398 File Offset: 0x00047598
		private bool IsEmpty()
		{
			return !this.m_noRestriction;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x000493A4 File Offset: 0x000475A4
		private DnsPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			DnsPermission dnsPermission = target as DnsPermission;
			if (dnsPermission == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(DnsPermission));
			}
			return dnsPermission;
		}

		// Token: 0x04001046 RID: 4166
		private const int version = 1;

		// Token: 0x04001047 RID: 4167
		private bool m_noRestriction;
	}
}
