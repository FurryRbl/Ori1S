using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates a delegate.</summary>
	// Token: 0x02000038 RID: 56
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeDelegateCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateCreateExpression" /> class.</summary>
		// Token: 0x060001E6 RID: 486 RVA: 0x0000B1D8 File Offset: 0x000093D8
		public CodeDelegateCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateCreateExpression" /> class.</summary>
		/// <param name="delegateType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the delegate. </param>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object containing the event-handler method. </param>
		/// <param name="methodName">The name of the event-handler method. </param>
		// Token: 0x060001E7 RID: 487 RVA: 0x0000B1E0 File Offset: 0x000093E0
		public CodeDelegateCreateExpression(CodeTypeReference delegateType, CodeExpression targetObject, string methodName)
		{
			this.delegateType = delegateType;
			this.targetObject = targetObject;
			this.methodName = methodName;
		}

		/// <summary>Gets or sets the data type of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the delegate.</returns>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000B200 File Offset: 0x00009400
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000B224 File Offset: 0x00009424
		public CodeTypeReference DelegateType
		{
			get
			{
				if (this.delegateType == null)
				{
					this.delegateType = new CodeTypeReference(string.Empty);
				}
				return this.delegateType;
			}
			set
			{
				this.delegateType = value;
			}
		}

		/// <summary>Gets or sets the name of the event handler method.</summary>
		/// <returns>The name of the event handler method.</returns>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000B230 File Offset: 0x00009430
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000B24C File Offset: 0x0000944C
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

		/// <summary>Gets or sets the object that contains the event-handler method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object containing the event-handler method.</returns>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000B258 File Offset: 0x00009458
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000B260 File Offset: 0x00009460
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

		// Token: 0x060001EE RID: 494 RVA: 0x0000B26C File Offset: 0x0000946C
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0400009A RID: 154
		private CodeTypeReference delegateType;

		// Token: 0x0400009B RID: 155
		private string methodName;

		// Token: 0x0400009C RID: 156
		private CodeExpression targetObject;
	}
}
