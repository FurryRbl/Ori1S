using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Distinguishes a compiler-generated element from a user-generated element. This class cannot be inherited.</summary>
	// Token: 0x02000051 RID: 81
	[AttributeUsage(AttributeTargets.All)]
	[Serializable]
	public sealed class CompilerGeneratedAttribute : Attribute
	{
	}
}
