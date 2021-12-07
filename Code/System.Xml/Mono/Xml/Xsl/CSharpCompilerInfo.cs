using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Xml;
using Microsoft.CSharp;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000086 RID: 134
	internal class CSharpCompilerInfo : ScriptCompilerInfo
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x0001DC24 File Offset: 0x0001BE24
		public CSharpCompilerInfo()
		{
			this.CompilerCommand = "mcs";
			this.DefaultCompilerOptions = "/t:library /r:System.dll /r:System.Xml.dll /r:Microsoft.VisualBasic.dll";
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0001DC44 File Offset: 0x0001BE44
		public override CodeDomProvider CodeDomProvider
		{
			get
			{
				return new CSharpCodeProvider();
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0001DC4C File Offset: 0x0001BE4C
		public override string Extension
		{
			get
			{
				return ".cs";
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0001DC54 File Offset: 0x0001BE54
		public override string SourceTemplate
		{
			get
			{
				return "// This file is automatically created by Mono managed XSLT engine.\n// Created time: {0}\nusing System;\nusing System.Collections;\nusing System.Text;\nusing System.Text.RegularExpressions;\nusing System.Xml;\nusing System.Xml.XPath;\nusing System.Xml.Xsl;\nusing Microsoft.VisualBasic;\n\nnamespace GeneratedAssembly\n{\npublic class Script{1}\n{\n\t{2}\n}\n}";
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001DC5C File Offset: 0x0001BE5C
		public override string FormatSource(IXmlLineInfo li, string file, string source)
		{
			if (li == null)
			{
				return source;
			}
			return string.Format(CultureInfo.InvariantCulture, "#line {0} \"{1}\"\n{2}", new object[]
			{
				li.LineNumber,
				file,
				source
			});
		}
	}
}
