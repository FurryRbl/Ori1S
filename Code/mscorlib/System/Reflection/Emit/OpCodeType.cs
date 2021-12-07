using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes the types of the Microsoft intermediate language (MSIL) instructions.</summary>
	// Token: 0x020002F1 RID: 753
	[ComVisible(true)]
	[Serializable]
	public enum OpCodeType
	{
		/// <summary>This enumerator value is reserved and should not be used.</summary>
		// Token: 0x04000F6B RID: 3947
		[Obsolete("This API has been deprecated.")]
		Annotation,
		/// <summary>These are Microsoft intermediate language (MSIL) instructions that are used as a synonym for other MSIL instructions. For example, ldarg.0 represents the ldarg instruction with an argument of 0.</summary>
		// Token: 0x04000F6C RID: 3948
		Macro,
		/// <summary>Describes a reserved Microsoft intermediate language (MSIL) instruction.</summary>
		// Token: 0x04000F6D RID: 3949
		Nternal,
		/// <summary>Describes a Microsoft intermediate language (MSIL) instruction that applies to objects.</summary>
		// Token: 0x04000F6E RID: 3950
		Objmodel,
		/// <summary>Describes a prefix instruction that modifies the behavior of the following instruction.</summary>
		// Token: 0x04000F6F RID: 3951
		Prefix,
		/// <summary>Describes a built-in instruction.</summary>
		// Token: 0x04000F70 RID: 3952
		Primitive
	}
}
