using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the permitted access to X.509 certificate stores.</summary>
	// Token: 0x02000460 RID: 1120
	[Flags]
	[Serializable]
	public enum StorePermissionFlags
	{
		/// <summary>Permission is not given to perform any certificate or store operations.</summary>
		// Token: 0x040018C7 RID: 6343
		NoFlags = 0,
		/// <summary>The ability to create a new store.</summary>
		// Token: 0x040018C8 RID: 6344
		CreateStore = 1,
		/// <summary>The ability to delete a store.</summary>
		// Token: 0x040018C9 RID: 6345
		DeleteStore = 2,
		/// <summary>The ability to enumerate the stores on a computer.</summary>
		// Token: 0x040018CA RID: 6346
		EnumerateStores = 4,
		/// <summary>The ability to open a store.</summary>
		// Token: 0x040018CB RID: 6347
		OpenStore = 16,
		/// <summary>The ability to add a certificate to a store.</summary>
		// Token: 0x040018CC RID: 6348
		AddToStore = 32,
		/// <summary>The ability to remove a certificate from a store.</summary>
		// Token: 0x040018CD RID: 6349
		RemoveFromStore = 64,
		/// <summary>The ability to enumerate the certificates in a store.</summary>
		// Token: 0x040018CE RID: 6350
		EnumerateCertificates = 128,
		/// <summary>The ability to perform all certificate and store operations.</summary>
		// Token: 0x040018CF RID: 6351
		AllFlags = 247
	}
}
