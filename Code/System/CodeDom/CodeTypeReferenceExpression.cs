using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a data type.</summary>
	// Token: 0x02000071 RID: 113
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeTypeReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class.</summary>
		// Token: 0x060003C5 RID: 965 RVA: 0x0000DE04 File Offset: 0x0000C004
		public CodeTypeReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified type.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type to reference. </param>
		// Token: 0x060003C6 RID: 966 RVA: 0x0000DE0C File Offset: 0x0000C00C
		public CodeTypeReferenceExpression(CodeTypeReference type)
		{
			this.type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified data type name.</summary>
		/// <param name="type">The name of the data type to reference. </param>
		// Token: 0x060003C7 RID: 967 RVA: 0x0000DE1C File Offset: 0x0000C01C
		public CodeTypeReferenceExpression(string type)
		{
			this.type = new CodeTypeReference(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified data type.</summary>
		/// <param name="type">An instance of the data type to reference. </param>
		// Token: 0x060003C8 RID: 968 RVA: 0x0000DE30 File Offset: 0x0000C030
		public CodeTypeReferenceExpression(Type type)
		{
			this.type = new CodeTypeReference(type);
		}

		/// <summary>Gets or sets the data type to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type to reference.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000DE44 File Offset: 0x0000C044
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000DE64 File Offset: 0x0000C064
		public CodeTypeReference Type
		{
			get
			{
				if (this.type == null)
				{
					return new CodeTypeReference(string.Empty);
				}
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000DE70 File Offset: 0x0000C070
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000119 RID: 281
		private CodeTypeReference type;
	}
}
