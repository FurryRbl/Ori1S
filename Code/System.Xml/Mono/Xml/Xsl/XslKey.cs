using System;
using System.Xml;
using System.Xml.XPath;
using Mono.Xml.XPath;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000091 RID: 145
	internal class XslKey
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x0001EEB0 File Offset: 0x0001D0B0
		public XslKey(Compiler c)
		{
			this.name = c.ParseQNameAttribute("name");
			c.KeyCompilationMode = true;
			this.useExpr = c.CompileExpression(c.GetAttribute("use"));
			if (this.useExpr == null)
			{
				this.useExpr = c.CompileExpression(".");
			}
			c.AssertAttribute("match");
			string attribute = c.GetAttribute("match");
			this.matchPattern = c.CompilePattern(attribute, c.Input);
			c.KeyCompilationMode = false;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0001EF40 File Offset: 0x0001D140
		public XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0001EF48 File Offset: 0x0001D148
		internal CompiledExpression Use
		{
			get
			{
				return this.useExpr;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0001EF50 File Offset: 0x0001D150
		internal Pattern Match
		{
			get
			{
				return this.matchPattern;
			}
		}

		// Token: 0x04000320 RID: 800
		private XmlQualifiedName name;

		// Token: 0x04000321 RID: 801
		private CompiledExpression useExpr;

		// Token: 0x04000322 RID: 802
		private Pattern matchPattern;
	}
}
