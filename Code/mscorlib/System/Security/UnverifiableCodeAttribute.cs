using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Marks modules containing unverifiable code. This class cannot be inherited.</summary>
	// Token: 0x02000551 RID: 1361
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Module, AllowMultiple = true, Inherited = false)]
	public sealed class UnverifiableCodeAttribute : Attribute
	{
	}
}
