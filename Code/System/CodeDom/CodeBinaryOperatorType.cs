using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Defines identifiers for supported binary operators.</summary>
	// Token: 0x0200002C RID: 44
	[ComVisible(true)]
	[Serializable]
	public enum CodeBinaryOperatorType
	{
		/// <summary>Addition operator.</summary>
		// Token: 0x04000073 RID: 115
		Add,
		/// <summary>Subtraction operator.</summary>
		// Token: 0x04000074 RID: 116
		Subtract,
		/// <summary>Multiplication operator.</summary>
		// Token: 0x04000075 RID: 117
		Multiply,
		/// <summary>Division operator.</summary>
		// Token: 0x04000076 RID: 118
		Divide,
		/// <summary>Modulus operator.</summary>
		// Token: 0x04000077 RID: 119
		Modulus,
		/// <summary>Assignment operator.</summary>
		// Token: 0x04000078 RID: 120
		Assign,
		/// <summary>Identity not equal operator.</summary>
		// Token: 0x04000079 RID: 121
		IdentityInequality,
		/// <summary>Identity equal operator.</summary>
		// Token: 0x0400007A RID: 122
		IdentityEquality,
		/// <summary>Value equal operator.</summary>
		// Token: 0x0400007B RID: 123
		ValueEquality,
		/// <summary>Bitwise or operator.</summary>
		// Token: 0x0400007C RID: 124
		BitwiseOr,
		/// <summary>Bitwise and operator.</summary>
		// Token: 0x0400007D RID: 125
		BitwiseAnd,
		/// <summary>Boolean or operator. This represents a short circuiting operator. A short circuiting operator will evaluate only as many expressions as necessary before returning a correct value.</summary>
		// Token: 0x0400007E RID: 126
		BooleanOr,
		/// <summary>Boolean and operator. This represents a short circuiting operator. A short circuiting operator will evaluate only as many expressions as necessary before returning a correct value.</summary>
		// Token: 0x0400007F RID: 127
		BooleanAnd,
		/// <summary>Less than operator.</summary>
		// Token: 0x04000080 RID: 128
		LessThan,
		/// <summary>Less than or equal operator.</summary>
		// Token: 0x04000081 RID: 129
		LessThanOrEqual,
		/// <summary>Greater than operator.</summary>
		// Token: 0x04000082 RID: 130
		GreaterThan,
		/// <summary>Greater than or equal operator.</summary>
		// Token: 0x04000083 RID: 131
		GreaterThanOrEqual
	}
}
