﻿using System;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access encrypted data and memory. This class cannot be inherited. </summary>
	// Token: 0x02000071 RID: 113
	[Serializable]
	public sealed class DataProtectionPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.DataProtectionPermission" /> class with the specified permission state. </summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value. </exception>
		// Token: 0x06000346 RID: 838 RVA: 0x0000EBFC File Offset: 0x0000CDFC
		public DataProtectionPermission(PermissionState state)
		{
			if (PermissionHelper.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._flags = DataProtectionPermissionFlags.AllFlags;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.DataProtectionPermission" /> class with the specified permission flags. </summary>
		/// <param name="flag">A bitwise combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flags" /> is not a valid combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values. </exception>
		// Token: 0x06000347 RID: 839 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		public DataProtectionPermission(DataProtectionPermissionFlags flags)
		{
			this.Flags = flags;
		}

		/// <summary>Gets or sets the data and memory protection flags.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value is not a valid combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values. </exception>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000EC2C File Offset: 0x0000CE2C
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public DataProtectionPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if ((value & ~(DataProtectionPermissionFlags.ProtectData | DataProtectionPermissionFlags.UnprotectData | DataProtectionPermissionFlags.ProtectMemory | DataProtectionPermissionFlags.UnprotectMemory)) != DataProtectionPermissionFlags.NoFlags)
				{
					string message = string.Format(Locale.GetText("Invalid enum {0}"), value);
					throw new ArgumentException(message, "DataProtectionPermissionFlags");
				}
				this._flags = value;
			}
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x0600034A RID: 842 RVA: 0x0000EC74 File Offset: 0x0000CE74
		public bool IsUnrestricted()
		{
			return this._flags == DataProtectionPermissionFlags.AllFlags;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x0600034B RID: 843 RVA: 0x0000EC80 File Offset: 0x0000CE80
		public override IPermission Copy()
		{
			return new DataProtectionPermission(this._flags);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and does not specify a permission of the same type as the current permission. </exception>
		// Token: 0x0600034C RID: 844 RVA: 0x0000EC90 File Offset: 0x0000CE90
		public override IPermission Intersect(IPermission target)
		{
			DataProtectionPermission dataProtectionPermission = this.Cast(target);
			if (dataProtectionPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted() && dataProtectionPermission.IsUnrestricted())
			{
				return new DataProtectionPermission(PermissionState.Unrestricted);
			}
			if (this.IsUnrestricted())
			{
				return dataProtectionPermission.Copy();
			}
			if (dataProtectionPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			return new DataProtectionPermission(this._flags & dataProtectionPermission._flags);
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and does not specify a permission of the same type as the current permission. </exception>
		// Token: 0x0600034D RID: 845 RVA: 0x0000ED00 File Offset: 0x0000CF00
		public override IPermission Union(IPermission target)
		{
			DataProtectionPermission dataProtectionPermission = this.Cast(target);
			if (dataProtectionPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || dataProtectionPermission.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			return new DataProtectionPermission(this._flags | dataProtectionPermission._flags);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission to test for the subset relationship. This permission must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and does not specify a permission of the same type as the current permission. </exception>
		// Token: 0x0600034E RID: 846 RVA: 0x0000ED54 File Offset: 0x0000CF54
		public override bool IsSubsetOf(IPermission target)
		{
			DataProtectionPermission dataProtectionPermission = this.Cast(target);
			if (dataProtectionPermission == null)
			{
				return this._flags == DataProtectionPermissionFlags.NoFlags;
			}
			return dataProtectionPermission.IsUnrestricted() || (!this.IsUnrestricted() && (this._flags & ~dataProtectionPermission._flags) == DataProtectionPermissionFlags.NoFlags);
		}

		/// <summary>Reconstructs a permission with a specific state from an XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding used to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a valid permission element.-or- The version number of <paramref name="securityElement" /> is not supported. </exception>
		// Token: 0x0600034F RID: 847 RVA: 0x0000EDA4 File Offset: 0x0000CFA4
		public override void FromXml(SecurityElement e)
		{
			PermissionHelper.CheckSecurityElement(e, "e", 1, 1);
			this._flags = (DataProtectionPermissionFlags)((int)Enum.Parse(typeof(DataProtectionPermissionFlags), e.Attribute("Flags")));
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including state information.</returns>
		// Token: 0x06000350 RID: 848 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(typeof(DataProtectionPermission), 1);
			securityElement.AddAttribute("Flags", this._flags.ToString());
			return securityElement;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000EE18 File Offset: 0x0000D018
		private DataProtectionPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			DataProtectionPermission dataProtectionPermission = target as DataProtectionPermission;
			if (dataProtectionPermission == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(DataProtectionPermission));
			}
			return dataProtectionPermission;
		}

		// Token: 0x040001A2 RID: 418
		private const int version = 1;

		// Token: 0x040001A3 RID: 419
		private DataProtectionPermissionFlags _flags;
	}
}
