using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Provides a container for a CodeDOM program graph.</summary>
	// Token: 0x02000034 RID: 52
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeCompileUnit : CodeObject
	{
		/// <summary>Gets a collection of custom attributes for the generated assembly.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes for the generated assembly.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000AFCC File Offset: 0x000091CC
		public CodeAttributeDeclarationCollection AssemblyCustomAttributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new CodeAttributeDeclarationCollection();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets the collection of namespaces.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceCollection" /> that indicates the namespaces that the compile unit uses.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000AFEC File Offset: 0x000091EC
		public CodeNamespaceCollection Namespaces
		{
			get
			{
				if (this.namespaces == null)
				{
					this.namespaces = new CodeNamespaceCollection();
				}
				return this.namespaces;
			}
		}

		/// <summary>Gets the referenced assemblies.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the file names of the referenced assemblies.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000B00C File Offset: 0x0000920C
		public System.Collections.Specialized.StringCollection ReferencedAssemblies
		{
			get
			{
				if (this.assemblies == null)
				{
					this.assemblies = new System.Collections.Specialized.StringCollection();
				}
				return this.assemblies;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000B02C File Offset: 0x0000922C
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

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000B04C File Offset: 0x0000924C
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

		// Token: 0x0400008F RID: 143
		private CodeAttributeDeclarationCollection attributes;

		// Token: 0x04000090 RID: 144
		private CodeNamespaceCollection namespaces;

		// Token: 0x04000091 RID: 145
		private System.Collections.Specialized.StringCollection assemblies;

		// Token: 0x04000092 RID: 146
		private CodeDirectiveCollection startDirectives;

		// Token: 0x04000093 RID: 147
		private CodeDirectiveCollection endDirectives;
	}
}
