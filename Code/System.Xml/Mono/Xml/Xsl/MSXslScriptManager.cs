using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000081 RID: 129
	internal class MSXslScriptManager
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x0001D544 File Offset: 0x0001B744
		public void AddScript(Compiler c)
		{
			MSXslScriptManager.MSXslScript msxslScript = new MSXslScriptManager.MSXslScript(c.Input, c.Evidence);
			string @namespace = c.Input.GetNamespace(msxslScript.ImplementsPrefix);
			if (@namespace == null)
			{
				throw new XsltCompileException("Specified prefix for msxsl:script was not found: " + msxslScript.ImplementsPrefix, null, c.Input);
			}
			this.scripts.Add(@namespace, msxslScript.Compile(c.Input));
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001D5B0 File Offset: 0x0001B7B0
		public object GetExtensionObject(string ns)
		{
			if (!this.scripts.ContainsKey(ns))
			{
				return null;
			}
			return Activator.CreateInstance((Type)this.scripts[ns]);
		}

		// Token: 0x040002F9 RID: 761
		private Hashtable scripts = new Hashtable();

		// Token: 0x02000082 RID: 130
		private enum ScriptingLanguage
		{
			// Token: 0x040002FB RID: 763
			JScript,
			// Token: 0x040002FC RID: 764
			VisualBasic,
			// Token: 0x040002FD RID: 765
			CSharp
		}

		// Token: 0x02000083 RID: 131
		private class MSXslScript
		{
			// Token: 0x0600046C RID: 1132 RVA: 0x0001D5DC File Offset: 0x0001B7DC
			public MSXslScript(XPathNavigator nav, Evidence evidence)
			{
				this.evidence = evidence;
				this.code = nav.Value;
				if (nav.MoveToFirstAttribute())
				{
					for (;;)
					{
						string localName = nav.LocalName;
						if (localName != null)
						{
							if (MSXslScriptManager.MSXslScript.<>f__switch$map1A == null)
							{
								MSXslScriptManager.MSXslScript.<>f__switch$map1A = new Dictionary<string, int>(2)
								{
									{
										"language",
										0
									},
									{
										"implements-prefix",
										1
									}
								};
							}
							int num;
							if (MSXslScriptManager.MSXslScript.<>f__switch$map1A.TryGetValue(localName, out num))
							{
								if (num != 0)
								{
									if (num == 1)
									{
										this.implementsPrefix = nav.Value;
									}
								}
								else
								{
									string text = nav.Value.ToLower(CultureInfo.InvariantCulture);
									if (text == null)
									{
										break;
									}
									if (MSXslScriptManager.MSXslScript.<>f__switch$map19 == null)
									{
										MSXslScriptManager.MSXslScript.<>f__switch$map19 = new Dictionary<string, int>(6)
										{
											{
												"jscript",
												0
											},
											{
												"javascript",
												0
											},
											{
												"vb",
												1
											},
											{
												"visualbasic",
												1
											},
											{
												"c#",
												2
											},
											{
												"csharp",
												2
											}
										};
									}
									int num2;
									if (!MSXslScriptManager.MSXslScript.<>f__switch$map19.TryGetValue(text, out num2))
									{
										break;
									}
									switch (num2)
									{
									case 0:
										this.language = MSXslScriptManager.ScriptingLanguage.JScript;
										goto IL_154;
									case 1:
										this.language = MSXslScriptManager.ScriptingLanguage.VisualBasic;
										goto IL_154;
									case 2:
										this.language = MSXslScriptManager.ScriptingLanguage.CSharp;
										goto IL_154;
									}
									break;
									IL_154:;
								}
							}
						}
						if (!nav.MoveToNextAttribute())
						{
							goto Block_10;
						}
					}
					throw new XsltException("Invalid scripting language!", null);
					Block_10:
					nav.MoveToParent();
				}
				if (this.implementsPrefix == null)
				{
					throw new XsltException("need implements-prefix attr", null);
				}
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x0600046D RID: 1133 RVA: 0x0001D77C File Offset: 0x0001B97C
			public MSXslScriptManager.ScriptingLanguage Language
			{
				get
				{
					return this.language;
				}
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x0600046E RID: 1134 RVA: 0x0001D784 File Offset: 0x0001B984
			public string ImplementsPrefix
			{
				get
				{
					return this.implementsPrefix;
				}
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x0600046F RID: 1135 RVA: 0x0001D78C File Offset: 0x0001B98C
			public string Code
			{
				get
				{
					return this.code;
				}
			}

			// Token: 0x06000470 RID: 1136 RVA: 0x0001D794 File Offset: 0x0001B994
			public object Compile(XPathNavigator node)
			{
				string text = string.Empty;
				foreach (byte b in MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(this.code)))
				{
					text += b.ToString("x2");
				}
				switch (this.language)
				{
				case MSXslScriptManager.ScriptingLanguage.JScript:
					return new JScriptCompilerInfo().GetScriptClass(this.Code, text, node, this.evidence);
				case MSXslScriptManager.ScriptingLanguage.VisualBasic:
					return new VBCompilerInfo().GetScriptClass(this.Code, text, node, this.evidence);
				case MSXslScriptManager.ScriptingLanguage.CSharp:
					return new CSharpCompilerInfo().GetScriptClass(this.Code, text, node, this.evidence);
				default:
					return null;
				}
			}

			// Token: 0x040002FE RID: 766
			private MSXslScriptManager.ScriptingLanguage language;

			// Token: 0x040002FF RID: 767
			private string implementsPrefix;

			// Token: 0x04000300 RID: 768
			private string code;

			// Token: 0x04000301 RID: 769
			private Evidence evidence;
		}
	}
}
