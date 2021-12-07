using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the action that a custom application domain manager takes when initializing a new domain.</summary>
	// Token: 0x0200012A RID: 298
	[ComVisible(true)]
	[Flags]
	public enum AppDomainManagerInitializationOptions
	{
		/// <summary>No initialization action.</summary>
		// Token: 0x040004D0 RID: 1232
		None = 0,
		/// <summary>Register the COM callable wrapper for the current <see cref="T:System.AppDomainManager" /> with the unmanaged host. </summary>
		// Token: 0x040004D1 RID: 1233
		RegisterWithHost = 1
	}
}
