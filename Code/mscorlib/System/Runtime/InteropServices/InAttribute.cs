using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that data should be marshaled from the caller to the callee, but not back to the caller.</summary>
	// Token: 0x02000043 RID: 67
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class InAttribute : Attribute
	{
	}
}
