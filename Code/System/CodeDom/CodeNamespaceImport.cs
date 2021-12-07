using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a namespace import directive that indicates a namespace to use.</summary>
	// Token: 0x02000052 RID: 82
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeNamespaceImport : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImport" /> class.</summary>
		// Token: 0x060002C7 RID: 711 RVA: 0x0000C614 File Offset: 0x0000A814
		public CodeNamespaceImport()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImport" /> class using the specified namespace to import.</summary>
		/// <param name="nameSpace">The name of the namespace to import. </param>
		// Token: 0x060002C8 RID: 712 RVA: 0x0000C61C File Offset: 0x0000A81C
		public CodeNamespaceImport(string nameSpace)
		{
			this.nameSpace = nameSpace;
		}

		/// <summary>Gets or sets the line and file the statement occurs on.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the context of the statement.</returns>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000C62C File Offset: 0x0000A82C
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0000C634 File Offset: 0x0000A834
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

		/// <summary>Gets or sets the namespace to import.</summary>
		/// <returns>The name of the namespace to import.</returns>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000C640 File Offset: 0x0000A840
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000C65C File Offset: 0x0000A85C
		public string Namespace
		{
			get
			{
				if (this.nameSpace == null)
				{
					return string.Empty;
				}
				return this.nameSpace;
			}
			set
			{
				this.nameSpace = value;
			}
		}

		// Token: 0x040000DA RID: 218
		private CodeLinePragma linePragma;

		// Token: 0x040000DB RID: 219
		private string nameSpace;
	}
}
