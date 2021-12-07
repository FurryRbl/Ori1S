using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an index of an array.</summary>
	// Token: 0x02000023 RID: 35
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeArrayIndexerExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayIndexerExpression" /> class.</summary>
		// Token: 0x06000143 RID: 323 RVA: 0x0000A500 File Offset: 0x00008700
		public CodeArrayIndexerExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayIndexerExpression" /> class using the specified target object and indexes.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the array the indexer targets. </param>
		/// <param name="indices">The index or indexes to reference. </param>
		// Token: 0x06000144 RID: 324 RVA: 0x0000A508 File Offset: 0x00008708
		public CodeArrayIndexerExpression(CodeExpression targetObject, params CodeExpression[] indices)
		{
			this.targetObject = targetObject;
			this.Indices.AddRange(indices);
		}

		/// <summary>Gets or sets the index or indexes of the indexer expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the index or indexes of the indexer expression.</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000A524 File Offset: 0x00008724
		public CodeExpressionCollection Indices
		{
			get
			{
				if (this.indices == null)
				{
					this.indices = new CodeExpressionCollection();
				}
				return this.indices;
			}
		}

		/// <summary>Gets or sets the target object of the array indexer.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that represents the array being indexed.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000A544 File Offset: 0x00008744
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000A54C File Offset: 0x0000874C
		public CodeExpression TargetObject
		{
			get
			{
				return this.targetObject;
			}
			set
			{
				this.targetObject = value;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000064 RID: 100
		private CodeExpressionCollection indices;

		// Token: 0x04000065 RID: 101
		private CodeExpression targetObject;
	}
}
