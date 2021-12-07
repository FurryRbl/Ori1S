using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents the abstract base class from which all code statements derive.</summary>
	// Token: 0x02000062 RID: 98
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeStatement : CodeObject
	{
		/// <summary>Gets or sets the line on which the code statement occurs. </summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> object that indicates the context of the code statement.</returns>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000CD90 File Offset: 0x0000AF90
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000CD98 File Offset: 0x0000AF98
		public CodeLinePragma LinePragma
		{
			get
			{
				return this.linePragma;
			}
			set
			{
				this.linePragma = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object that contains end directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		public CodeDirectiveCollection EndDirectives
		{
			get
			{
				if (this.endDirectives == null)
				{
					this.endDirectives = new CodeDirectiveCollection();
				}
				return this.endDirectives;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object that contains start directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
		public CodeDirectiveCollection StartDirectives
		{
			get
			{
				if (this.startDirectives == null)
				{
					this.startDirectives = new CodeDirectiveCollection();
				}
				return this.startDirectives;
			}
		}

		// Token: 0x040000F3 RID: 243
		private CodeLinePragma linePragma;

		// Token: 0x040000F4 RID: 244
		private CodeDirectiveCollection endDirectives;

		// Token: 0x040000F5 RID: 245
		private CodeDirectiveCollection startDirectives;
	}
}
