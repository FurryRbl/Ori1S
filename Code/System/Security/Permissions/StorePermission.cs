using System;

namespace System.Security.Permissions
{
	/// <summary>Controls access to stores containing X.509 certificates. This class cannot be inherited.</summary>
	// Token: 0x0200045F RID: 1119
	[Serializable]
	public sealed class StorePermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StorePermission" /> class with either fully restricted or unrestricted permission state.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value. </exception>
		// Token: 0x06002837 RID: 10295 RVA: 0x0007F98C File Offset: 0x0007DB8C
		public StorePermission(PermissionState state)
		{
			if (PermissionHelper.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._flags = StorePermissionFlags.AllFlags;
			}
			else
			{
				this._flags = StorePermissionFlags.NoFlags;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StorePermission" /> class with the specified access.</summary>
		/// <param name="flag">A bitwise combination of the <see cref="T:System.Security.Permissions.StorePermissionFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flag" /> is not a valid combination of <see cref="T:System.Security.Permissions.StorePermissionFlags" /> values. </exception>
		// Token: 0x06002838 RID: 10296 RVA: 0x0007F9C4 File Offset: 0x0007DBC4
		public StorePermission(StorePermissionFlags flags)
		{
			this.Flags = flags;
		}

		/// <summary>Gets or sets the type of <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> access allowed by the current permission.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.StorePermissionFlags" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.StorePermissionFlags" /> for the valid values. </exception>
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002839 RID: 10297 RVA: 0x0007F9D4 File Offset: 0x0007DBD4
		// (set) Token: 0x0600283A RID: 10298 RVA: 0x0007F9DC File Offset: 0x0007DBDC
		public StorePermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if (value != StorePermissionFlags.NoFlags && (value & StorePermissionFlags.AllFlags) == StorePermissionFlags.NoFlags)
				{
					string message = string.Format(Locale.GetText("Invalid enum {0}"), value);
					throw new ArgumentException(message, "StorePermissionFlags");
				}
				this._flags = value;
			}
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x0600283B RID: 10299 RVA: 0x0007FA24 File Offset: 0x0007DC24
		public bool IsUnrestricted()
		{
			return this._flags == StorePermissionFlags.AllFlags;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600283C RID: 10300 RVA: 0x0007FA34 File Offset: 0x0007DC34
		public override IPermission Copy()
		{
			if (this._flags == StorePermissionFlags.NoFlags)
			{
				return null;
			}
			return new StorePermission(this._flags);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> s not null and is not of the same type as the current permission. </exception>
		// Token: 0x0600283D RID: 10301 RVA: 0x0007FA50 File Offset: 0x0007DC50
		public override IPermission Intersect(IPermission target)
		{
			StorePermission storePermission = this.Cast(target);
			if (storePermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted() && storePermission.IsUnrestricted())
			{
				return new StorePermission(PermissionState.Unrestricted);
			}
			if (this.IsUnrestricted())
			{
				return storePermission.Copy();
			}
			if (storePermission.IsUnrestricted())
			{
				return this.Copy();
			}
			StorePermissionFlags storePermissionFlags = this._flags & storePermission._flags;
			if (storePermissionFlags == StorePermissionFlags.NoFlags)
			{
				return null;
			}
			return new StorePermission(storePermissionFlags);
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and is not of the same type as the current permission. </exception>
		// Token: 0x0600283E RID: 10302 RVA: 0x0007FACC File Offset: 0x0007DCCC
		public override IPermission Union(IPermission target)
		{
			StorePermission storePermission = this.Cast(target);
			if (storePermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || storePermission.IsUnrestricted())
			{
				return new StorePermission(PermissionState.Unrestricted);
			}
			StorePermissionFlags storePermissionFlags = this._flags | storePermission._flags;
			if (storePermissionFlags == StorePermissionFlags.NoFlags)
			{
				return null;
			}
			return new StorePermission(storePermissionFlags);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission to test for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and is not of the same type as the current permission. </exception>
		// Token: 0x0600283F RID: 10303 RVA: 0x0007FB28 File Offset: 0x0007DD28
		public override bool IsSubsetOf(IPermission target)
		{
			StorePermission storePermission = this.Cast(target);
			if (storePermission == null)
			{
				return this._flags == StorePermissionFlags.NoFlags;
			}
			return storePermission.IsUnrestricted() || (!this.IsUnrestricted() && (this._flags & ~storePermission._flags) == StorePermissionFlags.NoFlags);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a valid permission element.-or- The version number in <paramref name="securityElement" /> is not valid. </exception>
		// Token: 0x06002840 RID: 10304 RVA: 0x0007FB78 File Offset: 0x0007DD78
		public override void FromXml(SecurityElement e)
		{
			PermissionHelper.CheckSecurityElement(e, "e", 1, 1);
			string text = e.Attribute("Flags");
			if (text == null)
			{
				this._flags = StorePermissionFlags.NoFlags;
			}
			else
			{
				this._flags = (StorePermissionFlags)((int)Enum.Parse(typeof(StorePermissionFlags), text));
			}
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains an XML encoding of the permission, including any state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002841 RID: 10305 RVA: 0x0007FBCC File Offset: 0x0007DDCC
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(typeof(StorePermission), 1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", bool.TrueString);
			}
			else
			{
				securityElement.AddAttribute("Flags", this._flags.ToString());
			}
			return securityElement;
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x0007FC28 File Offset: 0x0007DE28
		private StorePermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			StorePermission storePermission = target as StorePermission;
			if (storePermission == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(StorePermission));
			}
			return storePermission;
		}

		// Token: 0x040018C4 RID: 6340
		private const int version = 1;

		// Token: 0x040018C5 RID: 6341
		private StorePermissionFlags _flags;
	}
}
