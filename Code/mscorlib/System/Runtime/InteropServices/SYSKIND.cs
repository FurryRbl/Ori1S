using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.SYSKIND" /> instead.</summary>
	// Token: 0x020003BD RID: 957
	[Obsolete]
	[Serializable]
	public enum SYSKIND
	{
		/// <summary>The target operating system for the type library is 16-bit Windows systems. By default, data fields are packed.</summary>
		// Token: 0x0400119A RID: 4506
		SYS_WIN16,
		/// <summary>The target operating system for the type library is 32-bit Windows systems. By default, data fields are naturally aligned (for example, 2-byte integers are aligned on even-byte boundaries; 4-byte integers are aligned on quad-word boundaries, and so on). </summary>
		// Token: 0x0400119B RID: 4507
		SYS_WIN32,
		/// <summary>The target operating system for the type library is Apple Macintosh. By default, all data fields are aligned on even-byte boundaries.</summary>
		// Token: 0x0400119C RID: 4508
		SYS_MAC
	}
}
