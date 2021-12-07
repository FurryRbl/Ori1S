using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Xml;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000088 RID: 136
	internal class JScriptCompilerInfo : ScriptCompilerInfo
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x0001DD20 File Offset: 0x0001BF20
		public JScriptCompilerInfo()
		{
			this.CompilerCommand = "mjs";
			this.DefaultCompilerOptions = "/t:library /r:Microsoft.VisualBasic.dll";
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0001DD40 File Offset: 0x0001BF40
		public override CodeDomProvider CodeDomProvider
		{
			get
			{
				if (JScriptCompilerInfo.providerType == null)
				{
					Assembly assembly = Assembly.LoadWithPartialName("Microsoft.JScript", null);
					if (assembly != null)
					{
						JScriptCompilerInfo.providerType = assembly.GetType("Microsoft.JScript.JScriptCodeProvider");
					}
				}
				return (CodeDomProvider)Activator.CreateInstance(JScriptCompilerInfo.providerType);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0001DD88 File Offset: 0x0001BF88
		public override string Extension
		{
			get
			{
				return ".js";
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0001DD90 File Offset: 0x0001BF90
		public override string SourceTemplate
		{
			get
			{
				return "// This file is automatically created by Mono managed XSLT engine.\n// Created time: {0}\nimport System;\nimport System.Collections;\nimport System.Text;\nimport System.Text.RegularExpressions;\nimport System.Xml;\nimport System.Xml.XPath;\nimport System.Xml.Xsl;\nimport Microsoft.VisualBasic;\n\npackage GeneratedAssembly\n{\nclass Script{1} {\n\t{2}\n}\n}\n";
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001DD98 File Offset: 0x0001BF98
		public override string FormatSource(IXmlLineInfo li, string file, string source)
		{
			return source;
		}

		// Token: 0x04000306 RID: 774
		private static Type providerType;
	}
}
