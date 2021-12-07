using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Prevents the MSIL Disassembler (Ildasm.exe) from disassembling an assembly. This class cannot be inherited.</summary>
	// Token: 0x02000345 RID: 837
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module)]
	public sealed class SuppressIldasmAttribute : Attribute
	{
	}
}
