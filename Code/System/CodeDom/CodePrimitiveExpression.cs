using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a primitive data type value.</summary>
	// Token: 0x02000057 RID: 87
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodePrimitiveExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePrimitiveExpression" /> class.</summary>
		// Token: 0x060002F2 RID: 754 RVA: 0x0000C984 File Offset: 0x0000AB84
		public CodePrimitiveExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePrimitiveExpression" /> class using the specified object.</summary>
		/// <param name="value">The object to represent. </param>
		// Token: 0x060002F3 RID: 755 RVA: 0x0000C98C File Offset: 0x0000AB8C
		public CodePrimitiveExpression(object value)
		{
			this.value = value;
		}

		/// <summary>Gets or sets the primitive data type to represent.</summary>
		/// <returns>The primitive data type instance to represent the value of.</returns>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000C99C File Offset: 0x0000AB9C
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000E3 RID: 227
		private object value;
	}
}
