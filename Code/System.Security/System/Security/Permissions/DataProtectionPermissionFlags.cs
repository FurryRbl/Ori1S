using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the access permissions for encrypting data and memory.</summary>
	// Token: 0x02000073 RID: 115
	[Flags]
	[Serializable]
	public enum DataProtectionPermissionFlags
	{
		/// <summary>No protection abilities.</summary>
		// Token: 0x040001A6 RID: 422
		NoFlags = 0,
		/// <summary>The ability to encrypt data.</summary>
		// Token: 0x040001A7 RID: 423
		ProtectData = 1,
		/// <summary>The ability to unencrypt data.</summary>
		// Token: 0x040001A8 RID: 424
		UnprotectData = 2,
		/// <summary>The ability to encrypt memory.</summary>
		// Token: 0x040001A9 RID: 425
		ProtectMemory = 4,
		/// <summary>The ability to unencrypt memory.</summary>
		// Token: 0x040001AA RID: 426
		UnprotectMemory = 8,
		/// <summary>The ability to encrypt data, encrypt memory, unencrypt data, and unencrypt memory.</summary>
		// Token: 0x040001AB RID: 427
		AllFlags = 15
	}
}
