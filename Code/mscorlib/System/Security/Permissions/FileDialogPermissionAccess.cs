using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of access to files allowed through the File dialog boxes.</summary>
	// Token: 0x020005F5 RID: 1525
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum FileDialogPermissionAccess
	{
		/// <summary>No access to files through the File dialog boxes.</summary>
		// Token: 0x0400193B RID: 6459
		None = 0,
		/// <summary>Ability to open files through the File dialog boxes.</summary>
		// Token: 0x0400193C RID: 6460
		Open = 1,
		/// <summary>Ability to save files through the File dialog boxes.</summary>
		// Token: 0x0400193D RID: 6461
		Save = 2,
		/// <summary>Ability to open and save files through the File dialog boxes.</summary>
		// Token: 0x0400193E RID: 6462
		OpenSave = 3
	}
}
