using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes how an instruction alters the flow of control.</summary>
	// Token: 0x020002DA RID: 730
	[ComVisible(true)]
	[Serializable]
	public enum FlowControl
	{
		/// <summary>Branch instruction.</summary>
		// Token: 0x04000DF4 RID: 3572
		Branch,
		/// <summary>Break instruction.</summary>
		// Token: 0x04000DF5 RID: 3573
		Break,
		/// <summary>Call instruction.</summary>
		// Token: 0x04000DF6 RID: 3574
		Call,
		/// <summary>Conditional branch instruction.</summary>
		// Token: 0x04000DF7 RID: 3575
		Cond_Branch,
		/// <summary>Provides information about a subsequent instruction. For example, the Unaligned instruction of Reflection.Emit.Opcodes has FlowControl.Meta and specifies that the subsequent pointer instruction might be unaligned.</summary>
		// Token: 0x04000DF8 RID: 3576
		Meta,
		/// <summary>Normal flow of control.</summary>
		// Token: 0x04000DF9 RID: 3577
		Next,
		/// <summary>This enumerator value is reserved and should not be used.</summary>
		// Token: 0x04000DFA RID: 3578
		[Obsolete("This API has been deprecated.")]
		Phi,
		/// <summary>Return instruction.</summary>
		// Token: 0x04000DFB RID: 3579
		Return,
		/// <summary>Exception throw instruction.</summary>
		// Token: 0x04000DFC RID: 3580
		Throw
	}
}
