using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.VisualBasic;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000087 RID: 135
	internal class VBCompilerInfo : ScriptCompilerInfo
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x0001DC9C File Offset: 0x0001BE9C
		public VBCompilerInfo()
		{
			this.CompilerCommand = "mbas";
			this.DefaultCompilerOptions = "/t:library";
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0001DCBC File Offset: 0x0001BEBC
		public override CodeDomProvider CodeDomProvider
		{
			get
			{
				return new VBCodeProvider();
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001DCC4 File Offset: 0x0001BEC4
		public override string Extension
		{
			get
			{
				return ".vb";
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0001DCCC File Offset: 0x0001BECC
		public override string SourceTemplate
		{
			get
			{
				return "' This file is automatically created by Mono managed XSLT engine.\n' Created time: {0}\nimports System\nimports System.Collections\nimports System.Text\nimports System.Text.RegularExpressions\nimports System.Xml\nimports System.Xml.XPath\nimports System.Xml.Xsl\nimports Microsoft.VisualBasic\n\nnamespace GeneratedAssembly\npublic Class Script{1}\n\t{2}\nend Class\nend namespace\n";
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001DCD4 File Offset: 0x0001BED4
		public override string FormatSource(IXmlLineInfo li, string file, string source)
		{
			if (li == null)
			{
				return source;
			}
			return string.Format(CultureInfo.InvariantCulture, "#ExternalSource (\"{1}\", {0})\n{2}\n#end ExternalSource", new object[]
			{
				li.LineNumber,
				new FileInfo(file).Name,
				source
			});
		}
	}
}
