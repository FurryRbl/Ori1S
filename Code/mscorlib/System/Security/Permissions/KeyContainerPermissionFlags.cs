using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of key container access allowed.</summary>
	// Token: 0x0200060C RID: 1548
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum KeyContainerPermissionFlags
	{
		/// <summary>No access to a key container.</summary>
		// Token: 0x040019A4 RID: 6564
		NoFlags = 0,
		/// <summary>Create a key container.</summary>
		// Token: 0x040019A5 RID: 6565
		Create = 1,
		/// <summary>Open a key container and use the public key.</summary>
		// Token: 0x040019A6 RID: 6566
		Open = 2,
		/// <summary>Delete a key container.</summary>
		// Token: 0x040019A7 RID: 6567
		Delete = 4,
		/// <summary>Import a key into a key container.</summary>
		// Token: 0x040019A8 RID: 6568
		Import = 16,
		/// <summary>Export a key from a key container.</summary>
		// Token: 0x040019A9 RID: 6569
		Export = 32,
		/// <summary>Sign a file using a key.</summary>
		// Token: 0x040019AA RID: 6570
		Sign = 256,
		/// <summary>Decrypt a key container.</summary>
		// Token: 0x040019AB RID: 6571
		Decrypt = 512,
		/// <summary>View the access control list (ACL) for a key container.</summary>
		// Token: 0x040019AC RID: 6572
		ViewAcl = 4096,
		/// <summary>Change the access control list (ACL) for a key container. </summary>
		// Token: 0x040019AD RID: 6573
		ChangeAcl = 8192,
		/// <summary>Create, decrypt, delete, and open a key container; export and import a key; sign files using a key; and view and change the access control list for a key container.</summary>
		// Token: 0x040019AE RID: 6574
		AllFlags = 13111
	}
}
