using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates that the method will allow a variable number of arguments in its invocation. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200003D RID: 61
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class ParamArrayAttribute : Attribute
	{
	}
}
