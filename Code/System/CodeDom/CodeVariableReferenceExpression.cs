using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a local variable.</summary>
	// Token: 0x02000074 RID: 116
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeVariableReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableReferenceExpression" /> class.</summary>
		// Token: 0x060003DA RID: 986 RVA: 0x0000DFCC File Offset: 0x0000C1CC
		public CodeVariableReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableReferenceExpression" /> class using the specified local variable name.</summary>
		/// <param name="variableName">The name of the local variable to reference. </param>
		// Token: 0x060003DB RID: 987 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		public CodeVariableReferenceExpression(string variableName)
		{
			this.variableName = variableName;
		}

		/// <summary>Gets or sets the name of the local variable to reference.</summary>
		/// <returns>The name of the local variable to reference.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000DFE4 File Offset: 0x0000C1E4
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0000E000 File Offset: 0x0000C200
		public string VariableName
		{
			get
			{
				if (this.variableName == null)
				{
					return string.Empty;
				}
				return this.variableName;
			}
			set
			{
				this.variableName = value;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000E00C File Offset: 0x0000C20C
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000120 RID: 288
		private string variableName;
	}
}
