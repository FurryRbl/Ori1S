using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>When applied to a method, specifies that the method is called before serialization of an object.</summary>
	// Token: 0x02000504 RID: 1284
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class OnSerializingAttribute : Attribute
	{
	}
}
