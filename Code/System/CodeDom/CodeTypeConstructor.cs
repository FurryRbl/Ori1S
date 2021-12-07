using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a static constructor for a class.</summary>
	// Token: 0x02000066 RID: 102
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeConstructor : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeConstructor" /> class.</summary>
		// Token: 0x06000343 RID: 835 RVA: 0x0000CF08 File Offset: 0x0000B108
		public CodeTypeConstructor()
		{
			base.Name = ".cctor";
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000CF1C File Offset: 0x0000B11C
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
