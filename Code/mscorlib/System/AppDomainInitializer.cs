using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents the callback method to invoke when the application domain is initialized.</summary>
	/// <param name="args">An array of strings to pass as arguments to the callback method.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006E7 RID: 1767
	// (Invoke) Token: 0x06004390 RID: 17296
	[ComVisible(true)]
	[Serializable]
	public delegate void AppDomainInitializer(string[] args);
}
