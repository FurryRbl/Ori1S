using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Describes a set of security permissions applied to code. This class cannot be inherited.</summary>
	// Token: 0x0200061B RID: 1563
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SecurityPermission" /> class with either restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003B7C RID: 15228 RVA: 0x000CC654 File Offset: 0x000CA854
		public SecurityPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.flags = SecurityPermissionFlag.AllFlags;
			}
			else
			{
				this.flags = SecurityPermissionFlag.NoFlags;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SecurityPermission" /> class with the specified initial set state of the flags.</summary>
		/// <param name="flag">The initial state of the permission, represented by a bitwise OR combination of any permission bits defined by <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />. </exception>
		// Token: 0x06003B7D RID: 15229 RVA: 0x000CC68C File Offset: 0x000CA88C
		public SecurityPermission(SecurityPermissionFlag flag)
		{
			this.Flags = flag;
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x000CC69C File Offset: 0x000CA89C
		int IBuiltInPermission.GetTokenIndex()
		{
			return 6;
		}

		/// <summary>Gets or sets the security permission flags.</summary>
		/// <returns>The state of the current permission, represented by a bitwise OR combination of any permission bits defined by <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.SecurityPermissionFlag" /> for the valid values. </exception>
		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06003B7F RID: 15231 RVA: 0x000CC6A0 File Offset: 0x000CA8A0
		// (set) Token: 0x06003B80 RID: 15232 RVA: 0x000CC6A8 File Offset: 0x000CA8A8
		public SecurityPermissionFlag Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				if ((value & SecurityPermissionFlag.AllFlags) != value)
				{
					string message = string.Format(Locale.GetText("Invalid flags {0}"), value);
					throw new ArgumentException(message, "SecurityPermissionFlag");
				}
				this.flags = value;
			}
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06003B81 RID: 15233 RVA: 0x000CC6EC File Offset: 0x000CA8EC
		public bool IsUnrestricted()
		{
			return this.flags == SecurityPermissionFlag.AllFlags;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003B82 RID: 15234 RVA: 0x000CC6FC File Offset: 0x000CA8FC
		public override IPermission Copy()
		{
			return new SecurityPermission(this.flags);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission object that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003B83 RID: 15235 RVA: 0x000CC70C File Offset: 0x000CA90C
		public override IPermission Intersect(IPermission target)
		{
			SecurityPermission securityPermission = this.Cast(target);
			if (securityPermission == null)
			{
				return null;
			}
			if (this.IsEmpty() || securityPermission.IsEmpty())
			{
				return null;
			}
			if (this.IsUnrestricted() && securityPermission.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			if (this.IsUnrestricted())
			{
				return securityPermission.Copy();
			}
			if (securityPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			SecurityPermissionFlag securityPermissionFlag = this.flags & securityPermission.flags;
			if (securityPermissionFlag == SecurityPermissionFlag.NoFlags)
			{
				return null;
			}
			return new SecurityPermission(securityPermissionFlag);
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003B84 RID: 15236 RVA: 0x000CC7A0 File Offset: 0x000CA9A0
		public override IPermission Union(IPermission target)
		{
			SecurityPermission securityPermission = this.Cast(target);
			if (securityPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || securityPermission.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			return new SecurityPermission(this.flags | securityPermission.flags);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003B85 RID: 15237 RVA: 0x000CC7F4 File Offset: 0x000CA9F4
		public override bool IsSubsetOf(IPermission target)
		{
			SecurityPermission securityPermission = this.Cast(target);
			if (securityPermission == null)
			{
				return this.IsEmpty();
			}
			return securityPermission.IsUnrestricted() || (!this.IsUnrestricted() && (this.flags & ~securityPermission.flags) == SecurityPermissionFlag.NoFlags);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not supported. </exception>
		// Token: 0x06003B86 RID: 15238 RVA: 0x000CC844 File Offset: 0x000CAA44
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.flags = SecurityPermissionFlag.AllFlags;
			}
			else
			{
				string text = esd.Attribute("Flags");
				if (text == null)
				{
					this.flags = SecurityPermissionFlag.NoFlags;
				}
				else
				{
					this.flags = (SecurityPermissionFlag)((int)Enum.Parse(typeof(SecurityPermissionFlag), text));
				}
			}
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003B87 RID: 15239 RVA: 0x000CC8B4 File Offset: 0x000CAAB4
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("Flags", this.flags.ToString());
			}
			return securityElement;
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x000CC908 File Offset: 0x000CAB08
		private bool IsEmpty()
		{
			return this.flags == SecurityPermissionFlag.NoFlags;
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x000CC914 File Offset: 0x000CAB14
		private SecurityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SecurityPermission securityPermission = target as SecurityPermission;
			if (securityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(SecurityPermission));
			}
			return securityPermission;
		}

		// Token: 0x040019EB RID: 6635
		private const int version = 1;

		// Token: 0x040019EC RID: 6636
		private SecurityPermissionFlag flags;
	}
}
