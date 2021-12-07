using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Policy;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000085 RID: 133
	internal abstract class ScriptCompilerInfo
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001D894 File Offset: 0x0001BA94
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0001D89C File Offset: 0x0001BA9C
		public virtual string CompilerCommand
		{
			get
			{
				return this.compilerCommand;
			}
			set
			{
				this.compilerCommand = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001D8A8 File Offset: 0x0001BAA8
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0001D8B0 File Offset: 0x0001BAB0
		public virtual string DefaultCompilerOptions
		{
			get
			{
				return this.defaultCompilerOptions;
			}
			set
			{
				this.defaultCompilerOptions = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000487 RID: 1159
		public abstract CodeDomProvider CodeDomProvider { get; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000488 RID: 1160
		public abstract string Extension { get; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000489 RID: 1161
		public abstract string SourceTemplate { get; }

		// Token: 0x0600048A RID: 1162
		public abstract string FormatSource(IXmlLineInfo li, string file, string code);

		// Token: 0x0600048B RID: 1163 RVA: 0x0001D8BC File Offset: 0x0001BABC
		public virtual string GetCompilerArguments(string targetFileName)
		{
			return this.DefaultCompilerOptions + " " + targetFileName;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001D8D0 File Offset: 0x0001BAD0
		public virtual Type GetScriptClass(string code, string classSuffix, XPathNavigator scriptNode, Evidence evidence)
		{
			PermissionSet permissionSet = SecurityManager.ResolvePolicy(evidence);
			if (permissionSet != null)
			{
				permissionSet.Demand();
			}
			string text = "Script" + classSuffix;
			string text2 = "GeneratedAssembly." + text;
			try
			{
				Type type = Type.GetType(text2);
				if (type != null)
				{
					return type;
				}
			}
			catch
			{
			}
			try
			{
				Type type2 = Assembly.LoadFrom(text + ".dll").GetType(text2);
				if (type2 != null)
				{
					return type2;
				}
			}
			catch
			{
			}
			ICodeCompiler codeCompiler = this.CodeDomProvider.CreateCompiler();
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.CompilerOptions = this.DefaultCompilerOptions;
			string text3 = string.Empty;
			try
			{
				if (scriptNode.BaseURI != string.Empty)
				{
					text3 = new Uri(scriptNode.BaseURI).LocalPath;
				}
			}
			catch (FormatException)
			{
			}
			if (text3 == string.Empty)
			{
				text3 = "__baseURI_not_supplied__";
			}
			IXmlLineInfo li = scriptNode as IXmlLineInfo;
			string text4 = this.SourceTemplate.Replace("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture)).Replace("{1}", classSuffix).Replace("{2}", code);
			text4 = this.FormatSource(li, text3, text4);
			CompilerResults compilerResults = codeCompiler.CompileAssemblyFromSource(compilerParameters, text4);
			foreach (object obj in compilerResults.Errors)
			{
				CompilerError compilerError = (CompilerError)obj;
				if (!compilerError.IsWarning)
				{
					throw new XsltException("Stylesheet script compile error: \n" + this.FormatErrorMessage(compilerResults), null, scriptNode);
				}
			}
			if (compilerResults.CompiledAssembly == null)
			{
				throw new XsltCompileException("Cannot compile stylesheet script", null, scriptNode);
			}
			return compilerResults.CompiledAssembly.GetType(text2);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001DB28 File Offset: 0x0001BD28
		private string FormatErrorMessage(CompilerResults res)
		{
			string text = string.Empty;
			foreach (object obj in res.Errors)
			{
				CompilerError compilerError = (CompilerError)obj;
				object[] args = new object[]
				{
					"\n",
					compilerError.FileName,
					(compilerError.Line <= 0) ? string.Empty : (" line " + compilerError.Line),
					(!compilerError.IsWarning) ? " ERROR: " : " WARNING: ",
					compilerError.ErrorNumber,
					": ",
					compilerError.ErrorText
				};
				text += string.Concat(args);
			}
			return text;
		}

		// Token: 0x04000304 RID: 772
		private string compilerCommand;

		// Token: 0x04000305 RID: 773
		private string defaultCompilerOptions;
	}
}
