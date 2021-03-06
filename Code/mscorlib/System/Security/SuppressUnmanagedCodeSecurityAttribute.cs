using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Allows managed code to call into unmanaged code without a stack walk. This class cannot be inherited.</summary>
	// Token: 0x02000550 RID: 1360
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	public sealed class SuppressUnmanagedCodeSecurityAttribute : Attribute
	{
	}
}
