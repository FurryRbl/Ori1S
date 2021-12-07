using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a field.</summary>
	// Token: 0x02000042 RID: 66
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeFieldReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> class.</summary>
		// Token: 0x06000227 RID: 551 RVA: 0x0000B670 File Offset: 0x00009870
		public CodeFieldReferenceExpression()
		{
			this.fieldName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> class using the specified target object and field name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the field. </param>
		/// <param name="fieldName">The name of the field. </param>
		// Token: 0x06000228 RID: 552 RVA: 0x0000B684 File Offset: 0x00009884
		public CodeFieldReferenceExpression(CodeExpression targetObject, string fieldName)
		{
			this.targetObject = targetObject;
			this.fieldName = fieldName;
		}

		/// <summary>Gets or sets the name of the field to reference.</summary>
		/// <returns>A string containing the field name.</returns>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000B69C File Offset: 0x0000989C
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000B6A4 File Offset: 0x000098A4
		public string FieldName
		{
			get
			{
				return this.fieldName;
			}
			set
			{
				this.fieldName = value;
			}
		}

		/// <summary>Gets or sets the object that contains the field to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the field to reference.</returns>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000B6B0 File Offset: 0x000098B0
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000B6B8 File Offset: 0x000098B8
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

		// Token: 0x0600022D RID: 557 RVA: 0x0000B6C4 File Offset: 0x000098C4
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000A4 RID: 164
		private CodeExpression targetObject;

		// Token: 0x040000A5 RID: 165
		private string fieldName;
	}
}
