using System;
using System.CodeDom;

namespace System.Xml.Serialization
{
	/// <summary>Represents a class that can generate proxy code from an XML representation of a data structure.</summary>
	// Token: 0x02000253 RID: 595
	public abstract class CodeExporter
	{
		// Token: 0x06001828 RID: 6184 RVA: 0x0007A00C File Offset: 0x0007820C
		internal CodeExporter()
		{
		}

		/// <summary>Gets a collection of code attribute metadata that is included when the code is exported.</summary>
		/// <returns>A collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects that represent metadata that is included when the code is exported.</returns>
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x0007A014 File Offset: 0x00078214
		public CodeAttributeDeclarationCollection IncludeMetadata
		{
			get
			{
				return this.codeGenerator.IncludeMetadata;
			}
		}

		// Token: 0x040009FE RID: 2558
		internal MapCodeGenerator codeGenerator;
	}
}
