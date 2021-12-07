using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents the value argument of a property set method call within a property set method.</summary>
	// Token: 0x02000059 RID: 89
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodePropertySetValueReferenceExpression : CodeExpression
	{
		// Token: 0x060002FF RID: 767 RVA: 0x0000CA2C File Offset: 0x0000AC2C
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
