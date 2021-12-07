﻿using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.NetworkInformation
{
	/// <summary>Controls access to network information and traffic statistics for the local computer. This class cannot be inherited. </summary>
	// Token: 0x020003A7 RID: 935
	[Serializable]
	public sealed class NetworkInformationPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		// Token: 0x06002093 RID: 8339 RVA: 0x0005FF70 File Offset: 0x0005E170
		[MonoTODO]
		public NetworkInformationPermission(PermissionState state)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> class using the specified <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> value.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> values.</param>
		// Token: 0x06002094 RID: 8340 RVA: 0x0005FF78 File Offset: 0x0005E178
		[MonoTODO]
		public NetworkInformationPermission(NetworkInformationAccess access)
		{
		}

		/// <summary>Adds the specified value to this permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> values.</param>
		// Token: 0x06002095 RID: 8341 RVA: 0x0005FF80 File Offset: 0x0005E180
		[MonoTODO]
		public void AddPermission(NetworkInformationAccess access)
		{
		}

		/// <summary>Creates and returns an identical copy of this permission.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> that is identical to the current permission</returns>
		// Token: 0x06002096 RID: 8342 RVA: 0x0005FF84 File Offset: 0x0005E184
		[MonoTODO]
		public override IPermission Copy()
		{
			return null;
		}

		/// <summary>Sets the state of this permission using the specified XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding to use to set the state of the current permission</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a permission encoding.-or-<paramref name="securityElement" /> is not an encoding of a <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" />. -or-<paramref name="securityElement" /> has invalid <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> values.</exception>
		// Token: 0x06002097 RID: 8343 RVA: 0x0005FF88 File Offset: 0x0005E188
		[MonoTODO]
		public override void FromXml(SecurityElement securityElement)
		{
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty or <paramref name="target" /> is null.</returns>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" />.</exception>
		// Token: 0x06002098 RID: 8344 RVA: 0x0005FF8C File Offset: 0x0005E18C
		[MonoTODO]
		public override IPermission Intersect(IPermission target)
		{
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> that is to be tested for the subset relationship. This permission must be of the same type as the current permission</param>
		// Token: 0x06002099 RID: 8345 RVA: 0x0005FF90 File Offset: 0x0005E190
		[MonoTODO]
		public override bool IsSubsetOf(IPermission target)
		{
			return false;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x0600209A RID: 8346 RVA: 0x0005FF94 File Offset: 0x0005E194
		[MonoTODO]
		public bool IsUnrestricted()
		{
			return false;
		}

		/// <summary>Creates an XML encoding of the state of this permission.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding of the current permission.</returns>
		// Token: 0x0600209B RID: 8347 RVA: 0x0005FF98 File Offset: 0x0005E198
		[MonoTODO]
		public override SecurityElement ToXml()
		{
			return PermissionHelper.Element(typeof(NetworkInformationPermission), 1);
		}

		/// <summary>Creates a permission that is the union of this permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" />  permission to combine with the current permission. </param>
		// Token: 0x0600209C RID: 8348 RVA: 0x0005FFB8 File Offset: 0x0005E1B8
		[MonoTODO]
		public override IPermission Union(IPermission target)
		{
			return null;
		}

		/// <summary>Gets the level of access to network information controlled by this permission. </summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> values.</returns>
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x0005FFBC File Offset: 0x0005E1BC
		[MonoTODO]
		public NetworkInformationAccess Access
		{
			get
			{
				return NetworkInformationAccess.None;
			}
		}

		// Token: 0x040013DA RID: 5082
		private const int version = 1;
	}
}
