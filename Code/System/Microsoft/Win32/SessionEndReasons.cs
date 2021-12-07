using System;

namespace Microsoft.Win32
{
	/// <summary>Defines identifiers that represent how the current logon session is ending.</summary>
	// Token: 0x02000017 RID: 23
	public enum SessionEndReasons
	{
		/// <summary>The user is logging off and ending the current user session. The operating system continues to run.</summary>
		// Token: 0x0400003C RID: 60
		Logoff = 1,
		/// <summary>The operating system is shutting down.</summary>
		// Token: 0x0400003D RID: 61
		SystemShutdown
	}
}
