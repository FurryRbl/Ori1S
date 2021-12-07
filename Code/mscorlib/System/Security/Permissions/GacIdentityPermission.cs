using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for files originating in the global assembly cache. This class cannot be inherited.</summary>
	// Token: 0x020005FA RID: 1530
	[ComVisible(true)]
	[Serializable]
	public sealed class GacIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.GacIdentityPermission" /> class.</summary>
		// Token: 0x06003A70 RID: 14960 RVA: 0x000C8D88 File Offset: 0x000C6F88
		public GacIdentityPermission()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.GacIdentityPermission" /> class with fully restricted <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value. </exception>
		// Token: 0x06003A71 RID: 14961 RVA: 0x000C8D90 File Offset: 0x000C6F90
		public GacIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000C8DA0 File Offset: 0x000C6FA0
		int IBuiltInPermission.GetTokenIndex()
		{
			return 15;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003A73 RID: 14963 RVA: 0x000C8DA4 File Offset: 0x000C6FA4
		public override IPermission Copy()
		{
			return new GacIdentityPermission();
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. The new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003A74 RID: 14964 RVA: 0x000C8DAC File Offset: 0x000C6FAC
		public override IPermission Intersect(IPermission target)
		{
			if (this.Cast(target) == null)
			{
				return null;
			}
			return this.Copy();
		}

		/// <summary>Indicates whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission object to test for the subset relationship. The permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003A75 RID: 14965 RVA: 0x000C8DD0 File Offset: 0x000C6FD0
		public override bool IsSubsetOf(IPermission target)
		{
			GacIdentityPermission gacIdentityPermission = this.Cast(target);
			return gacIdentityPermission != null;
		}

		/// <summary>Creates and returns a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003A76 RID: 14966 RVA: 0x000C8DEC File Offset: 0x000C6FEC
		public override IPermission Union(IPermission target)
		{
			this.Cast(target);
			return this.Copy();
		}

		/// <summary>Creates a permission from an XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" />  that contains the XML encoding to use to create the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" />is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a valid permission element. -or- The version number of <paramref name="securityElement" /> is not valid. </exception>
		// Token: 0x06003A77 RID: 14967 RVA: 0x000C8DFC File Offset: 0x000C6FFC
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.CheckSecurityElement(securityElement, "securityElement", 1, 1);
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that represents the XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003A78 RID: 14968 RVA: 0x000C8E0C File Offset: 0x000C700C
		public override SecurityElement ToXml()
		{
			return base.Element(1);
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000C8E24 File Offset: 0x000C7024
		private GacIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			GacIdentityPermission gacIdentityPermission = target as GacIdentityPermission;
			if (gacIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(GacIdentityPermission));
			}
			return gacIdentityPermission;
		}

		// Token: 0x0400195A RID: 6490
		private const int version = 1;
	}
}
