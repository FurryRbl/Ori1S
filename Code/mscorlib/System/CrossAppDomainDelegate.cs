using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Used by <see cref="M:System.AppDomain.DoCallBack(System.CrossAppDomainDelegate)" /> for cross-application domain calls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006EA RID: 1770
	// (Invoke) Token: 0x0600439C RID: 17308
	[ComVisible(true)]
	public delegate void CrossAppDomainDelegate();
}
