using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that raises an event.</summary>
	// Token: 0x02000039 RID: 57
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeDelegateInvokeExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateInvokeExpression" /> class.</summary>
		// Token: 0x060001EF RID: 495 RVA: 0x0000B278 File Offset: 0x00009478
		public CodeDelegateInvokeExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateInvokeExpression" /> class using the specified target object.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the target object. </param>
		// Token: 0x060001F0 RID: 496 RVA: 0x0000B280 File Offset: 0x00009480
		public CodeDelegateInvokeExpression(CodeExpression targetObject)
		{
			this.targetObject = targetObject;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateInvokeExpression" /> class using the specified target object and parameters.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the target object. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters. </param>
		// Token: 0x060001F1 RID: 497 RVA: 0x0000B290 File Offset: 0x00009490
		public CodeDelegateInvokeExpression(CodeExpression targetObject, params CodeExpression[] parameters)
		{
			this.targetObject = targetObject;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the parameters to pass to the event handling methods attached to the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the parameters to pass to the event handling methods attached to the event.</returns>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000B2AC File Offset: 0x000094AC
		public CodeExpressionCollection Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new CodeExpressionCollection();
				}
				return this.parameters;
			}
		}

		/// <summary>Gets or sets the event to invoke.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event to invoke.</returns>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000B2CC File Offset: 0x000094CC
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000B2D4 File Offset: 0x000094D4
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

		// Token: 0x060001F5 RID: 501 RVA: 0x0000B2E0 File Offset: 0x000094E0
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0400009D RID: 157
		private CodeExpressionCollection parameters;

		// Token: 0x0400009E RID: 158
		private CodeExpression targetObject;
	}
}
