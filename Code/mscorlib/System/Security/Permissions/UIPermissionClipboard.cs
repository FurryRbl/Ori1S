using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of clipboard access that is allowed to the calling code.</summary>
	// Token: 0x02000626 RID: 1574
	[ComVisible(true)]
	[Serializable]
	public enum UIPermissionClipboard
	{
		/// <summary>Clipboard cannot be used.</summary>
		// Token: 0x04001A14 RID: 6676
		NoClipboard,
		/// <summary>The ability to put data on the clipboard (Copy, Cut) is unrestricted. Intrinsic controls that accept Paste, such as text box, can accept the clipboard data, but user controls that must programmatically read the clipboard cannot.</summary>
		// Token: 0x04001A15 RID: 6677
		OwnClipboard,
		/// <summary>Clipboard can be used without restriction.</summary>
		// Token: 0x04001A16 RID: 6678
		AllClipboard
	}
}
