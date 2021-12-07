using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the location where an environment variable is stored or retrieved in a set or get operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000134 RID: 308
	[ComVisible(true)]
	public enum EnvironmentVariableTarget
	{
		/// <summary>The environment variable is stored or retrieved from the environment block associated with the current process. </summary>
		// Token: 0x040004FA RID: 1274
		Process,
		/// <summary>The environment variable is stored or retrieved from the HKEY_CURRENT_USER\Environment key in the Windows operating system registry. </summary>
		// Token: 0x040004FB RID: 1275
		User,
		/// <summary>The environment variable is stored or retrieved from the HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Session Manager\Environment key in the Windows operating system registry. </summary>
		// Token: 0x040004FC RID: 1276
		Machine
	}
}
