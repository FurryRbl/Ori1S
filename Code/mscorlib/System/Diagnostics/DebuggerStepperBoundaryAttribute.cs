using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Indicates that the code following the attribute is to be executed in run mode, not step mode. This class cannot be inherited.</summary>
	// Token: 0x020001E2 RID: 482
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[Serializable]
	public sealed class DebuggerStepperBoundaryAttribute : Attribute
	{
	}
}
