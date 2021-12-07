using System;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000057 RID: 87
	internal abstract class XslCompiledElement : XslCompiledElementBase
	{
		// Token: 0x0600035D RID: 861 RVA: 0x0001684C File Offset: 0x00014A4C
		public XslCompiledElement(Compiler c) : base(c)
		{
			this.Compile(c);
		}
	}
}
