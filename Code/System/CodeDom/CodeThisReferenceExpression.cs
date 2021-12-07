using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to the current local class instance.</summary>
	// Token: 0x02000063 RID: 99
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeThisReferenceExpression : CodeExpression
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000CDEC File Offset: 0x0000AFEC
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
