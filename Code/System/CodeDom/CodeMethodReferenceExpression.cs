using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a method.</summary>
	// Token: 0x0200004D RID: 77
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMethodReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class.</summary>
		// Token: 0x06000287 RID: 647 RVA: 0x0000BE94 File Offset: 0x0000A094
		public CodeMethodReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class using the specified target object and method name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object to target. </param>
		/// <param name="methodName">The name of the method to call. </param>
		// Token: 0x06000288 RID: 648 RVA: 0x0000BE9C File Offset: 0x0000A09C
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName)
		{
			this.targetObject = targetObject;
			this.methodName = methodName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class using the specified target object, method name, and generic type arguments.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object to target. </param>
		/// <param name="methodName">The name of the method to call. </param>
		/// <param name="typeParameters">An array of <see cref="T:System.CodeDom.CodeTypeReference" /> values that specify the <see cref="P:System.CodeDom.CodeMethodReferenceExpression.TypeArguments" /> for this <see cref="T:System.CodeDom.CodeMethodReferenceExpression" />.</param>
		// Token: 0x06000289 RID: 649 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName, params CodeTypeReference[] typeParameters) : this(targetObject, methodName)
		{
			if (typeParameters != null && typeParameters.Length > 0)
			{
				this.TypeArguments.AddRange(typeParameters);
			}
		}

		/// <summary>Gets or sets the name of the method to reference.</summary>
		/// <returns>The name of the method to reference.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		public string MethodName
		{
			get
			{
				if (this.methodName == null)
				{
					return string.Empty;
				}
				return this.methodName;
			}
			set
			{
				this.methodName = value;
			}
		}

		/// <summary>Gets or sets the expression that indicates the method to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that represents the method to reference.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000BF04 File Offset: 0x0000A104
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000BF0C File Offset: 0x0000A10C
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

		/// <summary>Gets the type arguments for the current generic method reference expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> containing the type arguments for the current code <see cref="T:System.CodeDom.CodeMethodReferenceExpression" />.</returns>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000BF18 File Offset: 0x0000A118
		[ComVisible(false)]
		public CodeTypeReferenceCollection TypeArguments
		{
			get
			{
				if (this.typeArguments == null)
				{
					this.typeArguments = new CodeTypeReferenceCollection();
				}
				return this.typeArguments;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000BF38 File Offset: 0x0000A138
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000CB RID: 203
		private string methodName;

		// Token: 0x040000CC RID: 204
		private CodeExpression targetObject;

		// Token: 0x040000CD RID: 205
		private CodeTypeReferenceCollection typeArguments;
	}
}
