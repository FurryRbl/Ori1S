using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for an instance constructor of a type.</summary>
	// Token: 0x02000036 RID: 54
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeConstructor : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConstructor" /> class.</summary>
		// Token: 0x060001DD RID: 477 RVA: 0x0000B124 File Offset: 0x00009324
		public CodeConstructor()
		{
			base.Name = ".ctor";
		}

		/// <summary>Gets the collection of base constructor arguments.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the base constructor arguments.</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000B138 File Offset: 0x00009338
		public CodeExpressionCollection BaseConstructorArgs
		{
			get
			{
				if (this.baseConstructorArgs == null)
				{
					this.baseConstructorArgs = new CodeExpressionCollection();
				}
				return this.baseConstructorArgs;
			}
		}

		/// <summary>Gets the collection of chained constructor arguments.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the chained constructor arguments.</returns>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000B158 File Offset: 0x00009358
		public CodeExpressionCollection ChainedConstructorArgs
		{
			get
			{
				if (this.chainedConstructorArgs == null)
				{
					this.chainedConstructorArgs = new CodeExpressionCollection();
				}
				return this.chainedConstructorArgs;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000B178 File Offset: 0x00009378
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000097 RID: 151
		private CodeExpressionCollection baseConstructorArgs;

		// Token: 0x04000098 RID: 152
		private CodeExpressionCollection chainedConstructorArgs;
	}
}
