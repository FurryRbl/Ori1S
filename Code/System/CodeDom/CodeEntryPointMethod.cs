using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents the entry point method of an executable.</summary>
	// Token: 0x0200003D RID: 61
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeEntryPointMethod : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeEntryPointMethod" /> class.</summary>
		// Token: 0x0600020B RID: 523 RVA: 0x0000B478 File Offset: 0x00009678
		public CodeEntryPointMethod()
		{
			base.Attributes = (MemberAttributes)24579;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B48C File Offset: 0x0000968C
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
