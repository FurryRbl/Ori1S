using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Specifies common roles to be used with <see cref="M:System.Security.Principal.WindowsPrincipal.IsInRole(System.String)" />.</summary>
	// Token: 0x0200066A RID: 1642
	[ComVisible(true)]
	[Serializable]
	public enum WindowsBuiltInRole
	{
		/// <summary>Administrators have complete and unrestricted access to the computer or domain.</summary>
		// Token: 0x04001B16 RID: 6934
		Administrator = 544,
		/// <summary>Users are prevented from making accidental or intentional system-wide changes. Thus, users can run certified applications, but not most legacy applications.</summary>
		// Token: 0x04001B17 RID: 6935
		User,
		/// <summary>Guests are more restricted than users.</summary>
		// Token: 0x04001B18 RID: 6936
		Guest,
		/// <summary>Power users possess most administrative permissions with some restrictions. Thus, power users can run legacy applications, in addition to certified applications.</summary>
		// Token: 0x04001B19 RID: 6937
		PowerUser,
		/// <summary>Account operators manage the user accounts on a computer or domain.</summary>
		// Token: 0x04001B1A RID: 6938
		AccountOperator,
		/// <summary>System operators manage a particular computer.</summary>
		// Token: 0x04001B1B RID: 6939
		SystemOperator,
		/// <summary>Print operators can take control of a printer.</summary>
		// Token: 0x04001B1C RID: 6940
		PrintOperator,
		/// <summary>Backup operators can override security restrictions for the sole purpose of backing up or restoring files.</summary>
		// Token: 0x04001B1D RID: 6941
		BackupOperator,
		/// <summary>Replicators support file replication in a domain.</summary>
		// Token: 0x04001B1E RID: 6942
		Replicator
	}
}
