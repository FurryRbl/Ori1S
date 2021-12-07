using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Identifies the operating system, or platform, supported by an assembly.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200016A RID: 362
	[ComVisible(true)]
	[Serializable]
	public enum PlatformID
	{
		/// <summary>The operating system is Win32s. Win32s is a layer that runs on 16-bit versions of Windows to provide access to 32-bit applications.</summary>
		// Token: 0x04000596 RID: 1430
		Win32S,
		/// <summary>The operating system is Windows 95 or Windows 98.</summary>
		// Token: 0x04000597 RID: 1431
		Win32Windows,
		/// <summary>The operating system is Windows NT or later.</summary>
		// Token: 0x04000598 RID: 1432
		Win32NT,
		/// <summary>The operating system is Windows CE.</summary>
		// Token: 0x04000599 RID: 1433
		WinCE,
		/// <summary>The operating system is Unix.</summary>
		// Token: 0x0400059A RID: 1434
		Unix,
		/// <summary>The development platform is Xbox 360.</summary>
		// Token: 0x0400059B RID: 1435
		Xbox,
		/// <summary>The operating system is Macintosh.</summary>
		// Token: 0x0400059C RID: 1436
		MacOSX
	}
}
