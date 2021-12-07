using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the allowed usage of a private virtual file system. This class cannot be inherited.</summary>
	// Token: 0x02000602 RID: 1538
	[ComVisible(true)]
	[Serializable]
	public sealed class IsolatedStorageFilePermission : IsolatedStoragePermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.IsolatedStorageFilePermission" /> class with either fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003AA1 RID: 15009 RVA: 0x000C93C8 File Offset: 0x000C75C8
		public IsolatedStorageFilePermission(PermissionState state) : base(state)
		{
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000C93D4 File Offset: 0x000C75D4
		int IBuiltInPermission.GetTokenIndex()
		{
			return 3;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003AA3 RID: 15011 RVA: 0x000C93D8 File Offset: 0x000C75D8
		public override IPermission Copy()
		{
			return new IsolatedStorageFilePermission(PermissionState.None)
			{
				m_userQuota = this.m_userQuota,
				m_machineQuota = this.m_machineQuota,
				m_expirationDays = this.m_expirationDays,
				m_permanentData = this.m_permanentData,
				m_allowed = this.m_allowed
			};
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission object. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003AA4 RID: 15012 RVA: 0x000C942C File Offset: 0x000C762C
		public override IPermission Intersect(IPermission target)
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission = this.Cast(target);
			if (isolatedStorageFilePermission == null)
			{
				return null;
			}
			if (base.IsEmpty() && isolatedStorageFilePermission.IsEmpty())
			{
				return null;
			}
			return new IsolatedStorageFilePermission(PermissionState.None)
			{
				m_userQuota = ((this.m_userQuota >= isolatedStorageFilePermission.m_userQuota) ? isolatedStorageFilePermission.m_userQuota : this.m_userQuota),
				m_machineQuota = ((this.m_machineQuota >= isolatedStorageFilePermission.m_machineQuota) ? isolatedStorageFilePermission.m_machineQuota : this.m_machineQuota),
				m_expirationDays = ((this.m_expirationDays >= isolatedStorageFilePermission.m_expirationDays) ? isolatedStorageFilePermission.m_expirationDays : this.m_expirationDays),
				m_permanentData = (this.m_permanentData && isolatedStorageFilePermission.m_permanentData),
				UsageAllowed = ((this.m_allowed >= isolatedStorageFilePermission.m_allowed) ? isolatedStorageFilePermission.m_allowed : this.m_allowed)
			};
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003AA5 RID: 15013 RVA: 0x000C9524 File Offset: 0x000C7724
		public override bool IsSubsetOf(IPermission target)
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission = this.Cast(target);
			if (isolatedStorageFilePermission == null)
			{
				return base.IsEmpty();
			}
			return isolatedStorageFilePermission.IsUnrestricted() || (this.m_userQuota <= isolatedStorageFilePermission.m_userQuota && this.m_machineQuota <= isolatedStorageFilePermission.m_machineQuota && this.m_expirationDays <= isolatedStorageFilePermission.m_expirationDays && this.m_permanentData == isolatedStorageFilePermission.m_permanentData && this.m_allowed <= isolatedStorageFilePermission.m_allowed);
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003AA6 RID: 15014 RVA: 0x000C95B4 File Offset: 0x000C77B4
		public override IPermission Union(IPermission target)
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission = this.Cast(target);
			if (isolatedStorageFilePermission == null)
			{
				return this.Copy();
			}
			return new IsolatedStorageFilePermission(PermissionState.None)
			{
				m_userQuota = ((this.m_userQuota <= isolatedStorageFilePermission.m_userQuota) ? isolatedStorageFilePermission.m_userQuota : this.m_userQuota),
				m_machineQuota = ((this.m_machineQuota <= isolatedStorageFilePermission.m_machineQuota) ? isolatedStorageFilePermission.m_machineQuota : this.m_machineQuota),
				m_expirationDays = ((this.m_expirationDays <= isolatedStorageFilePermission.m_expirationDays) ? isolatedStorageFilePermission.m_expirationDays : this.m_expirationDays),
				m_permanentData = (this.m_permanentData || isolatedStorageFilePermission.m_permanentData),
				UsageAllowed = ((this.m_allowed <= isolatedStorageFilePermission.m_allowed) ? isolatedStorageFilePermission.m_allowed : this.m_allowed)
			};
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003AA7 RID: 15015 RVA: 0x000C9698 File Offset: 0x000C7898
		[MonoTODO("(2.0) new override - something must have been added ???")]
		[ComVisible(false)]
		public override SecurityElement ToXml()
		{
			return base.ToXml();
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000C96A0 File Offset: 0x000C78A0
		private IsolatedStorageFilePermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			IsolatedStorageFilePermission isolatedStorageFilePermission = target as IsolatedStorageFilePermission;
			if (isolatedStorageFilePermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(IsolatedStorageFilePermission));
			}
			return isolatedStorageFilePermission;
		}

		// Token: 0x04001989 RID: 6537
		private const int version = 1;
	}
}
