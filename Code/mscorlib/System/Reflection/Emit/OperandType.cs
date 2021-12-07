using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes the operand type of Microsoft intermediate language (MSIL) instruction.</summary>
	// Token: 0x020002F2 RID: 754
	[ComVisible(true)]
	[Serializable]
	public enum OperandType
	{
		/// <summary>The operand is a 32-bit integer branch target.</summary>
		// Token: 0x04000F72 RID: 3954
		InlineBrTarget,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x04000F73 RID: 3955
		InlineField,
		/// <summary>The operand is a 32-bit integer.</summary>
		// Token: 0x04000F74 RID: 3956
		InlineI,
		/// <summary>The operand is a 64-bit integer.</summary>
		// Token: 0x04000F75 RID: 3957
		InlineI8,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x04000F76 RID: 3958
		InlineMethod,
		/// <summary>No operand.</summary>
		// Token: 0x04000F77 RID: 3959
		InlineNone,
		/// <summary>The operand is reserved and should not be used.</summary>
		// Token: 0x04000F78 RID: 3960
		[Obsolete("This API has been deprecated.")]
		InlinePhi,
		/// <summary>The operand is a 64-bit IEEE floating point number.</summary>
		// Token: 0x04000F79 RID: 3961
		InlineR,
		/// <summary>The operand is a 32-bit metadata signature token.</summary>
		// Token: 0x04000F7A RID: 3962
		InlineSig = 9,
		/// <summary>The operand is a 32-bit metadata string token.</summary>
		// Token: 0x04000F7B RID: 3963
		InlineString,
		/// <summary>The operand is the 32-bit integer argument to a switch instruction.</summary>
		// Token: 0x04000F7C RID: 3964
		InlineSwitch,
		/// <summary>The operand is a FieldRef, MethodRef, or TypeRef token.</summary>
		// Token: 0x04000F7D RID: 3965
		InlineTok,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x04000F7E RID: 3966
		InlineType,
		/// <summary>The operand is 16-bit integer containing the ordinal of a local variable or an argument.</summary>
		// Token: 0x04000F7F RID: 3967
		InlineVar,
		/// <summary>The operand is an 8-bit integer branch target.</summary>
		// Token: 0x04000F80 RID: 3968
		ShortInlineBrTarget,
		/// <summary>The operand is an 8-bit integer.</summary>
		// Token: 0x04000F81 RID: 3969
		ShortInlineI,
		/// <summary>The operand is a 32-bit IEEE floating point number.</summary>
		// Token: 0x04000F82 RID: 3970
		ShortInlineR,
		/// <summary>The operand is an 8-bit integer containing the ordinal of a local variable or an argumenta.</summary>
		// Token: 0x04000F83 RID: 3971
		ShortInlineVar
	}
}
