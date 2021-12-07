using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access key containers. This class cannot be inherited.</summary>
	// Token: 0x02000607 RID: 1543
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermission" /> class with either restricted or unrestricted permission.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value. </exception>
		// Token: 0x06003ABA RID: 15034 RVA: 0x000C9980 File Offset: 0x000C7B80
		public KeyContainerPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._flags = KeyContainerPermissionFlags.AllFlags;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermission" /> class with the specified access.</summary>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flags" /> is not a valid combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. </exception>
		// Token: 0x06003ABB RID: 15035 RVA: 0x000C99A0 File Offset: 0x000C7BA0
		public KeyContainerPermission(KeyContainerPermissionFlags flags)
		{
			this.SetFlags(flags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermission" /> class with the specified global access and specific key container access rights.</summary>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. </param>
		/// <param name="accessList">An array of <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects identifying specific key container access rights. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flags" /> is not a valid combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="accessList" /> is null. </exception>
		// Token: 0x06003ABC RID: 15036 RVA: 0x000C99B0 File Offset: 0x000C7BB0
		public KeyContainerPermission(KeyContainerPermissionFlags flags, KeyContainerPermissionAccessEntry[] accessList)
		{
			this.SetFlags(flags);
			if (accessList != null)
			{
				foreach (KeyContainerPermissionAccessEntry accessEntry in accessList)
				{
					this._accessEntries.Add(accessEntry);
				}
			}
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x000C99F8 File Offset: 0x000C7BF8
		int IBuiltInPermission.GetTokenIndex()
		{
			return 16;
		}

		/// <summary>Gets the collection of <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects associated with the current permission.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryCollection" /> containing the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects for this <see cref="T:System.Security.Permissions.KeyContainerPermission" />.</returns>
		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06003ABE RID: 15038 RVA: 0x000C99FC File Offset: 0x000C7BFC
		public KeyContainerPermissionAccessEntryCollection AccessEntries
		{
			get
			{
				return this._accessEntries;
			}
		}

		/// <summary>Gets the key container permission flags that apply to all key containers associated with the permission.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</returns>
		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06003ABF RID: 15039 RVA: 0x000C9A04 File Offset: 0x000C7C04
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003AC0 RID: 15040 RVA: 0x000C9A0C File Offset: 0x000C7C0C
		public override IPermission Copy()
		{
			if (this._accessEntries.Count == 0)
			{
				return new KeyContainerPermission(this._flags);
			}
			KeyContainerPermissionAccessEntry[] array = new KeyContainerPermissionAccessEntry[this._accessEntries.Count];
			this._accessEntries.CopyTo(array, 0);
			return new KeyContainerPermission(this._flags, array);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding used to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a valid permission element.-or- The version number of <paramref name="securityElement" /> is not supported. </exception>
		// Token: 0x06003AC1 RID: 15041 RVA: 0x000C9A60 File Offset: 0x000C7C60
		[MonoTODO("(2.0) missing support for AccessEntries")]
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(securityElement))
			{
				this._flags = KeyContainerPermissionFlags.AllFlags;
			}
			else
			{
				this._flags = (KeyContainerPermissionFlags)((int)Enum.Parse(typeof(KeyContainerPermissionFlags), securityElement.Attribute("Flags")));
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and does not specify a permission of the same type as the current permission. </exception>
		// Token: 0x06003AC2 RID: 15042 RVA: 0x000C9ABC File Offset: 0x000C7CBC
		[MonoTODO("(2.0)")]
		public override IPermission Intersect(IPermission target)
		{
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission to test for the subset relationship. This permission must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and does not specify a permission of the same type as the current permission. </exception>
		// Token: 0x06003AC3 RID: 15043 RVA: 0x000C9AC0 File Offset: 0x000C7CC0
		[MonoTODO("(2.0)")]
		public override bool IsSubsetOf(IPermission target)
		{
			return false;
		}

		/// <summary>Determines whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06003AC4 RID: 15044 RVA: 0x000C9AC4 File Offset: 0x000C7CC4
		public bool IsUnrestricted()
		{
			return this._flags == KeyContainerPermissionFlags.AllFlags;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains an XML encoding of the permission, including state information.</returns>
		// Token: 0x06003AC5 RID: 15045 RVA: 0x000C9AD4 File Offset: 0x000C7CD4
		[MonoTODO("(2.0) missing support for AccessEntries")]
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and does not specify a permission of the same type as the current permission. </exception>
		// Token: 0x06003AC6 RID: 15046 RVA: 0x000C9B0C File Offset: 0x000C7D0C
		public override IPermission Union(IPermission target)
		{
			KeyContainerPermission keyContainerPermission = this.Cast(target);
			if (keyContainerPermission == null)
			{
				return this.Copy();
			}
			KeyContainerPermissionAccessEntryCollection keyContainerPermissionAccessEntryCollection = new KeyContainerPermissionAccessEntryCollection();
			foreach (KeyContainerPermissionAccessEntry accessEntry in this._accessEntries)
			{
				keyContainerPermissionAccessEntryCollection.Add(accessEntry);
			}
			foreach (KeyContainerPermissionAccessEntry accessEntry2 in keyContainerPermission._accessEntries)
			{
				if (this._accessEntries.IndexOf(accessEntry2) == -1)
				{
					keyContainerPermissionAccessEntryCollection.Add(accessEntry2);
				}
			}
			if (keyContainerPermissionAccessEntryCollection.Count == 0)
			{
				return new KeyContainerPermission(this._flags | keyContainerPermission._flags);
			}
			KeyContainerPermissionAccessEntry[] array = new KeyContainerPermissionAccessEntry[keyContainerPermissionAccessEntryCollection.Count];
			keyContainerPermissionAccessEntryCollection.CopyTo(array, 0);
			return new KeyContainerPermission(this._flags | keyContainerPermission._flags, array);
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x000C9BEC File Offset: 0x000C7DEC
		private void SetFlags(KeyContainerPermissionFlags flags)
		{
			if ((flags & KeyContainerPermissionFlags.AllFlags) != KeyContainerPermissionFlags.NoFlags)
			{
				string message = string.Format(Locale.GetText("Invalid enum {0}"), flags);
				throw new ArgumentException(message, "KeyContainerPermissionFlags");
			}
			this._flags = flags;
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x000C9C30 File Offset: 0x000C7E30
		private KeyContainerPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			KeyContainerPermission keyContainerPermission = target as KeyContainerPermission;
			if (keyContainerPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(KeyContainerPermission));
			}
			return keyContainerPermission;
		}

		// Token: 0x04001992 RID: 6546
		private const int version = 1;

		// Token: 0x04001993 RID: 6547
		private KeyContainerPermissionAccessEntryCollection _accessEntries;

		// Token: 0x04001994 RID: 6548
		private KeyContainerPermissionFlags _flags;
	}
}
