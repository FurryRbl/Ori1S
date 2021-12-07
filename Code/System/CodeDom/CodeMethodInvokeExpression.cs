using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that invokes a method.</summary>
	// Token: 0x0200004C RID: 76
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeMethodInvokeExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class.</summary>
		// Token: 0x06000280 RID: 640 RVA: 0x0000BDF4 File Offset: 0x00009FF4
		public CodeMethodInvokeExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class using the specified method and parameters.</summary>
		/// <param name="method">A <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> that indicates the method to invoke. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters with which to invoke the method. </param>
		// Token: 0x06000281 RID: 641 RVA: 0x0000BDFC File Offset: 0x00009FFC
		public CodeMethodInvokeExpression(CodeMethodReferenceExpression method, params CodeExpression[] parameters)
		{
			this.method = method;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class using the specified target object, method name, and parameters.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the target object with the method to invoke. </param>
		/// <param name="methodName">The name of the method to invoke. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters to call the method with. </param>
		// Token: 0x06000282 RID: 642 RVA: 0x0000BE18 File Offset: 0x0000A018
		public CodeMethodInvokeExpression(CodeExpression targetObject, string methodName, params CodeExpression[] parameters)
		{
			this.method = new CodeMethodReferenceExpression(targetObject, methodName);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the method to invoke.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> that indicates the method to invoke.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000BE3C File Offset: 0x0000A03C
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000BE5C File Offset: 0x0000A05C
		public CodeMethodReferenceExpression Method
		{
			get
			{
				if (this.method == null)
				{
					this.method = new CodeMethodReferenceExpression();
				}
				return this.method;
			}
			set
			{
				this.method = value;
			}
		}

		/// <summary>Gets the parameters to invoke the method with.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the parameters to invoke the method with.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000BE68 File Offset: 0x0000A068
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

		// Token: 0x06000286 RID: 646 RVA: 0x0000BE88 File Offset: 0x0000A088
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000C9 RID: 201
		private CodeMethodReferenceExpression method;

		// Token: 0x040000CA RID: 202
		private CodeExpressionCollection parameters;
	}
}
