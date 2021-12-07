using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to the base class.</summary>
	// Token: 0x0200002A RID: 42
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeBaseReferenceExpression : CodeExpression
	{
		// Token: 0x06000183 RID: 387 RVA: 0x0000AA10 File Offset: 0x00008C10
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
