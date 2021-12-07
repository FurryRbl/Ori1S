using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to registry objects.</summary>
	// Token: 0x0200058F RID: 1423
	[Flags]
	public enum RegistryRights
	{
		/// <summary>The right to query the name/value pairs in a registry key.</summary>
		// Token: 0x0400174D RID: 5965
		QueryValues = 1,
		/// <summary>The right to create, delete, or set name/value pairs in a registry key.</summary>
		// Token: 0x0400174E RID: 5966
		SetValue = 2,
		/// <summary>The right to create subkeys of a registry key.</summary>
		// Token: 0x0400174F RID: 5967
		CreateSubKey = 4,
		/// <summary>The right to list the subkeys of a registry key.</summary>
		// Token: 0x04001750 RID: 5968
		EnumerateSubKeys = 8,
		/// <summary>The right to request notification of changes on a registry key.</summary>
		// Token: 0x04001751 RID: 5969
		Notify = 16,
		/// <summary>Reserved for system use.</summary>
		// Token: 0x04001752 RID: 5970
		CreateLink = 32,
		/// <summary>The right to delete a registry key.</summary>
		// Token: 0x04001753 RID: 5971
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a registry key.</summary>
		// Token: 0x04001754 RID: 5972
		ReadPermissions = 131072,
		/// <summary>The right to create, delete, and set the name/value pairs in a registry key, to create or delete subkeys, to request notification of changes, to enumerate its subkeys, and to read its access rules and audit rules.</summary>
		// Token: 0x04001755 RID: 5973
		WriteKey = 131078,
		/// <summary>The right to query the name/value pairs in a registry key, to request notification of changes, to enumerate its subkeys, and to read its access rules and audit rules.</summary>
		// Token: 0x04001756 RID: 5974
		ReadKey = 131097,
		/// <summary>Same as <see cref="F:System.Security.AccessControl.RegistryRights.ReadKey" />.</summary>
		// Token: 0x04001757 RID: 5975
		ExecuteKey = 131097,
		/// <summary>The right to change the access rules and audit rules associated with a registry key.</summary>
		// Token: 0x04001758 RID: 5976
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a registry key.</summary>
		// Token: 0x04001759 RID: 5977
		TakeOwnership = 524288,
		/// <summary>The right to exert full control over a registry key, and to modify its access rules and audit rules.</summary>
		// Token: 0x0400175A RID: 5978
		FullControl = 983103
	}
}
