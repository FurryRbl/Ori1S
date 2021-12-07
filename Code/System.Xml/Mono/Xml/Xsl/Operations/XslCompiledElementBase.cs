using System;
using System.Xml;
using System.Xml.XPath;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000056 RID: 86
	internal abstract class XslCompiledElementBase : XslOperation
	{
		// Token: 0x06000358 RID: 856 RVA: 0x000167D8 File Offset: 0x000149D8
		public XslCompiledElementBase(Compiler c)
		{
			IXmlLineInfo xmlLineInfo = c.Input as IXmlLineInfo;
			if (xmlLineInfo != null)
			{
				this.lineNumber = xmlLineInfo.LineNumber;
				this.linePosition = xmlLineInfo.LinePosition;
			}
			if (c.Debugger != null)
			{
				this.debugInput = c.Input.Clone();
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00016834 File Offset: 0x00014A34
		public XPathNavigator DebugInput
		{
			get
			{
				return this.debugInput;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0001683C File Offset: 0x00014A3C
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00016844 File Offset: 0x00014A44
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x0600035C RID: 860
		protected abstract void Compile(Compiler c);

		// Token: 0x04000249 RID: 585
		private int lineNumber;

		// Token: 0x0400024A RID: 586
		private int linePosition;

		// Token: 0x0400024B RID: 587
		private XPathNavigator debugInput;
	}
}
