using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates that the COM threading model for an application is single-threaded apartment (STA). </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000172 RID: 370
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(true)]
	public sealed class STAThreadAttribute : Attribute
	{
	}
}
