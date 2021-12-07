using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an indexer property of an object.</summary>
	// Token: 0x02000044 RID: 68
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeIndexerExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIndexerExpression" /> class.</summary>
		// Token: 0x06000233 RID: 563 RVA: 0x0000B724 File Offset: 0x00009924
		public CodeIndexerExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIndexerExpression" /> class using the specified target object and index.</summary>
		/// <param name="targetObject">The target object. </param>
		/// <param name="indices">The index or indexes of the indexer expression. </param>
		// Token: 0x06000234 RID: 564 RVA: 0x0000B72C File Offset: 0x0000992C
		public CodeIndexerExpression(CodeExpression targetObject, params CodeExpression[] indices)
		{
			this.targetObject = targetObject;
			this.Indices.AddRange(indices);
		}

		/// <summary>Gets the collection of indexes of the indexer expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the index or indexes of the indexer expression.</returns>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000B748 File Offset: 0x00009948
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

		/// <summary>Gets or sets the target object that can be indexed.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the indexer object.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000B768 File Offset: 0x00009968
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000B770 File Offset: 0x00009970
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

		// Token: 0x06000238 RID: 568 RVA: 0x0000B77C File Offset: 0x0000997C
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000A7 RID: 167
		private CodeExpression targetObject;

		// Token: 0x040000A8 RID: 168
		private CodeExpressionCollection indices;
	}
}
