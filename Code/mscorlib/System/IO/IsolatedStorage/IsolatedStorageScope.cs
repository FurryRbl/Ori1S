using System;
using System.Runtime.InteropServices;

namespace System.IO.IsolatedStorage
{
	/// <summary>Enumerates the levels of isolated storage scope that are supported by <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" />.</summary>
	// Token: 0x0200026B RID: 619
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum IsolatedStorageScope
	{
		/// <summary>No isolated storage usage.</summary>
		// Token: 0x04000BEB RID: 3051
		None = 0,
		/// <summary>Isolated storage scoped by user identity.</summary>
		// Token: 0x04000BEC RID: 3052
		User = 1,
		/// <summary>Isolated storage scoped to the application domain identity.</summary>
		// Token: 0x04000BED RID: 3053
		Domain = 2,
		/// <summary>Isolated storage scoped to the identity of the assembly.</summary>
		// Token: 0x04000BEE RID: 3054
		Assembly = 4,
		/// <summary>The isolated store can be placed in a location on the file system that might roam (if roaming user data is enabled on the underlying operating system).</summary>
		// Token: 0x04000BEF RID: 3055
		Roaming = 8,
		/// <summary>Isolated storage scoped to the machine.</summary>
		// Token: 0x04000BF0 RID: 3056
		Machine = 16,
		/// <summary>Isolated storage scoped to the application.</summary>
		// Token: 0x04000BF1 RID: 3057
		Application = 32
	}
}
