using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Hides the code from the debugger. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000054 RID: 84
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	[Serializable]
	public sealed class DebuggerHiddenAttribute : Attribute
	{
	}
}
