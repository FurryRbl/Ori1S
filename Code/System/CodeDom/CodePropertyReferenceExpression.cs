using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to the value of a property.</summary>
	// Token: 0x02000058 RID: 88
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodePropertyReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePropertyReferenceExpression" /> class.</summary>
		// Token: 0x060002F7 RID: 759 RVA: 0x0000C9BC File Offset: 0x0000ABBC
		public CodePropertyReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePropertyReferenceExpression" /> class using the specified target object and property name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the property to reference. </param>
		/// <param name="propertyName">The name of the property to reference. </param>
		// Token: 0x060002F8 RID: 760 RVA: 0x0000C9C4 File Offset: 0x0000ABC4
		public CodePropertyReferenceExpression(CodeExpression targetObject, string propertyName)
		{
			this.targetObject = targetObject;
			this.propertyName = propertyName;
		}

		/// <summary>Gets or sets the name of the property to reference.</summary>
		/// <returns>The name of the property to reference.</returns>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000C9DC File Offset: 0x0000ABDC
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		public string PropertyName
		{
			get
			{
				if (this.propertyName == null)
				{
					return string.Empty;
				}
				return this.propertyName;
			}
			set
			{
				this.propertyName = value;
			}
		}

		/// <summary>Gets or sets the object that contains the property to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the property to reference.</returns>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000CA04 File Offset: 0x0000AC04
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000CA0C File Offset: 0x0000AC0C
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

		// Token: 0x060002FD RID: 765 RVA: 0x0000CA18 File Offset: 0x0000AC18
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000E4 RID: 228
		private CodeExpression targetObject;

		// Token: 0x040000E5 RID: 229
		private string propertyName;
	}
}
