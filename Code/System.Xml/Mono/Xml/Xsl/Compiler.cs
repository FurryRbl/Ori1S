using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Mono.Xml.XPath;
using Mono.Xml.Xsl.Operations;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000078 RID: 120
	internal class Compiler : IStaticXsltContext
	{
		// Token: 0x060003DD RID: 989 RVA: 0x00019C94 File Offset: 0x00017E94
		public Compiler(object debugger)
		{
			if (debugger != null)
			{
				this.debugger = new XsltDebuggerWrapper(debugger);
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00019D24 File Offset: 0x00017F24
		Expression IStaticXsltContext.TryGetVariable(string nm)
		{
			if (this.curVarScope == null)
			{
				return null;
			}
			XslLocalVariable xslLocalVariable = this.curVarScope.ResolveStatic(XslNameUtil.FromString(nm, this.Input));
			if (xslLocalVariable == null)
			{
				return null;
			}
			return new XPathVariableBinding(xslLocalVariable);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00019D64 File Offset: 0x00017F64
		Expression IStaticXsltContext.TryGetFunction(XmlQualifiedName name, FunctionArguments args)
		{
			string a = this.LookupNamespace(name.Namespace);
			if (a == "urn:schemas-microsoft-com:xslt" && name.Name == "node-set")
			{
				return new MSXslNodeSet(args);
			}
			if (a != string.Empty)
			{
				return null;
			}
			string name2 = name.Name;
			switch (name2)
			{
			case "current":
				return new XsltCurrent(args);
			case "unparsed-entity-uri":
				return new XsltUnparsedEntityUri(args);
			case "element-available":
				return new XsltElementAvailable(args, this);
			case "system-property":
				return new XsltSystemProperty(args, this);
			case "function-available":
				return new XsltFunctionAvailable(args, this);
			case "generate-id":
				return new XsltGenerateId(args);
			case "format-number":
				return new XsltFormatNumber(args, this);
			case "key":
				if (this.KeyCompilationMode)
				{
					throw new XsltCompileException("Cannot use key() function inside key definition", null, this.Input);
				}
				return new XsltKey(args, this);
			case "document":
				return new XsltDocument(args, this);
			}
			return null;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00019EF4 File Offset: 0x000180F4
		XmlQualifiedName IStaticXsltContext.LookupQName(string s)
		{
			return XslNameUtil.FromString(s, this.Input);
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00019F04 File Offset: 0x00018104
		public XsltDebuggerWrapper Debugger
		{
			get
			{
				return this.debugger;
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00019F0C File Offset: 0x0001810C
		public void CheckExtraAttributes(string element, params string[] validNames)
		{
			if (this.Input.MoveToFirstAttribute())
			{
				for (;;)
				{
					if (this.Input.NamespaceURI.Length <= 0)
					{
						bool flag = false;
						foreach (string b in validNames)
						{
							if (this.Input.LocalName == b)
							{
								flag = true;
							}
						}
						if (!flag)
						{
							break;
						}
					}
					if (!this.Input.MoveToNextAttribute())
					{
						goto Block_5;
					}
				}
				throw new XsltCompileException(string.Format("Invalid attribute '{0}' on element '{1}'", this.Input.LocalName, element), null, this.Input);
				Block_5:
				this.Input.MoveToParent();
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00019FC4 File Offset: 0x000181C4
		public CompiledStylesheet Compile(XPathNavigator nav, XmlResolver res, Evidence evidence)
		{
			this.xpathParser = new XPathParser(this);
			this.patternParser = new XsltPatternParser(this);
			this.res = res;
			if (res == null)
			{
				this.res = new XmlUrlResolver();
			}
			this.evidence = evidence;
			if (nav.NodeType == XPathNodeType.Root && !nav.MoveToFirstChild())
			{
				throw new XsltCompileException("Stylesheet root element must be either \"stylesheet\" or \"transform\" or any literal element", null, nav);
			}
			while (nav.NodeType != XPathNodeType.Element)
			{
				nav.MoveToNext();
			}
			this.stylesheetVersion = nav.GetAttribute("version", (!(nav.NamespaceURI != "http://www.w3.org/1999/XSL/Transform")) ? string.Empty : "http://www.w3.org/1999/XSL/Transform");
			this.outputs[string.Empty] = new XslOutput(string.Empty, this.stylesheetVersion);
			this.PushInputDocument(nav);
			if (nav.MoveToFirstNamespace(XPathNamespaceScope.ExcludeXml))
			{
				do
				{
					this.nsMgr.AddNamespace(nav.LocalName, nav.Value);
				}
				while (nav.MoveToNextNamespace(XPathNamespaceScope.ExcludeXml));
				nav.MoveToParent();
			}
			try
			{
				this.rootStyle = new XslStylesheet();
				this.rootStyle.Compile(this);
			}
			catch (XsltCompileException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new XsltCompileException("XSLT compile error. " + ex.Message, ex, this.Input);
			}
			return new CompiledStylesheet(this.rootStyle, this.globalVariables, this.attrSets, this.nsMgr, this.keys, this.outputs, this.decimalFormats, this.msScripts);
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0001A188 File Offset: 0x00018388
		public MSXslScriptManager ScriptManager
		{
			get
			{
				return this.msScripts;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0001A190 File Offset: 0x00018390
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0001A198 File Offset: 0x00018398
		public bool KeyCompilationMode
		{
			get
			{
				return this.keyCompilationMode;
			}
			set
			{
				this.keyCompilationMode = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0001A1A4 File Offset: 0x000183A4
		internal Evidence Evidence
		{
			get
			{
				return this.evidence;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0001A1AC File Offset: 0x000183AC
		public XPathNavigator Input
		{
			get
			{
				return this.currentInput;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0001A1B4 File Offset: 0x000183B4
		public XslStylesheet CurrentStylesheet
		{
			get
			{
				return this.currentStyle;
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001A1BC File Offset: 0x000183BC
		public void PushStylesheet(XslStylesheet style)
		{
			if (this.currentStyle != null)
			{
				this.styleStack.Push(this.currentStyle);
			}
			this.currentStyle = style;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001A1E4 File Offset: 0x000183E4
		public void PopStylesheet()
		{
			if (this.styleStack.Count == 0)
			{
				this.currentStyle = null;
			}
			else
			{
				this.currentStyle = (XslStylesheet)this.styleStack.Pop();
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001A224 File Offset: 0x00018424
		public void PushInputDocument(string url)
		{
			Uri baseUri = (!(this.Input.BaseURI == string.Empty)) ? new Uri(this.Input.BaseURI) : null;
			Uri uri = this.res.ResolveUri(baseUri, url);
			string url2 = (!(uri != null)) ? string.Empty : uri.ToString();
			using (Stream stream = (Stream)this.res.GetEntity(uri, null, typeof(Stream)))
			{
				if (stream == null)
				{
					throw new XsltCompileException("Can not access URI " + uri.ToString(), null, this.Input);
				}
				XmlValidatingReader xmlValidatingReader = new XmlValidatingReader(new XmlTextReader(url2, stream, this.nsMgr.NameTable));
				xmlValidatingReader.ValidationType = ValidationType.None;
				XPathNavigator xpathNavigator = new XPathDocument(xmlValidatingReader, XmlSpace.Preserve).CreateNavigator();
				xmlValidatingReader.Close();
				xpathNavigator.MoveToFirstChild();
				while (xpathNavigator.NodeType != XPathNodeType.Element)
				{
					if (!xpathNavigator.MoveToNext())
					{
						IL_F9:
						this.PushInputDocument(xpathNavigator);
						return;
					}
				}
				goto IL_F9;
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001A360 File Offset: 0x00018560
		public void PushInputDocument(XPathNavigator nav)
		{
			IXmlLineInfo xmlLineInfo = this.currentInput as IXmlLineInfo;
			bool flag = xmlLineInfo != null && !xmlLineInfo.HasLineInfo();
			for (int i = 0; i < this.inputStack.Count; i++)
			{
				XPathNavigator xpathNavigator = (XPathNavigator)this.inputStack[i];
				if (xpathNavigator.BaseURI == nav.BaseURI)
				{
					throw new XsltCompileException(null, this.currentInput.BaseURI, (!flag) ? 0 : xmlLineInfo.LineNumber, (!flag) ? 0 : xmlLineInfo.LinePosition);
				}
			}
			if (this.currentInput != null)
			{
				this.inputStack.Add(this.currentInput);
			}
			this.currentInput = nav;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001A42C File Offset: 0x0001862C
		public void PopInputDocument()
		{
			int index = this.inputStack.Count - 1;
			this.currentInput = (XPathNavigator)this.inputStack[index];
			this.inputStack.RemoveAt(index);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001A46C File Offset: 0x0001866C
		public XmlQualifiedName ParseQNameAttribute(string localName)
		{
			return this.ParseQNameAttribute(localName, string.Empty);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001A47C File Offset: 0x0001867C
		public XmlQualifiedName ParseQNameAttribute(string localName, string ns)
		{
			return XslNameUtil.FromString(this.Input.GetAttribute(localName, ns), this.Input);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001A4A4 File Offset: 0x000186A4
		public XmlQualifiedName[] ParseQNameListAttribute(string localName)
		{
			return this.ParseQNameListAttribute(localName, string.Empty);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001A4B4 File Offset: 0x000186B4
		public XmlQualifiedName[] ParseQNameListAttribute(string localName, string ns)
		{
			string attribute = this.GetAttribute(localName, ns);
			if (attribute == null)
			{
				return null;
			}
			string[] array = attribute.Split(new char[]
			{
				' ',
				'\r',
				'\n',
				'\t'
			});
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Length != 0)
				{
					num++;
				}
			}
			XmlQualifiedName[] array2 = new XmlQualifiedName[num];
			int j = 0;
			int num2 = 0;
			while (j < array.Length)
			{
				if (array[j].Length != 0)
				{
					array2[num2++] = XslNameUtil.FromString(array[j], this.Input);
				}
				j++;
			}
			return array2;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001A560 File Offset: 0x00018760
		public bool ParseYesNoAttribute(string localName, bool defaultVal)
		{
			return this.ParseYesNoAttribute(localName, string.Empty, defaultVal);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001A570 File Offset: 0x00018770
		public bool ParseYesNoAttribute(string localName, string ns, bool defaultVal)
		{
			string attribute = this.GetAttribute(localName, ns);
			string text = attribute;
			if (text != null)
			{
				if (Compiler.<>f__switch$mapF == null)
				{
					Compiler.<>f__switch$mapF = new Dictionary<string, int>(2)
					{
						{
							"yes",
							0
						},
						{
							"no",
							1
						}
					};
				}
				int num;
				if (Compiler.<>f__switch$mapF.TryGetValue(text, out num))
				{
					if (num == 0)
					{
						return true;
					}
					if (num == 1)
					{
						return false;
					}
				}
				throw new XsltCompileException("Invalid value for " + localName, null, this.Input);
			}
			return defaultVal;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001A600 File Offset: 0x00018800
		public string GetAttribute(string localName)
		{
			return this.GetAttribute(localName, string.Empty);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001A610 File Offset: 0x00018810
		public string GetAttribute(string localName, string ns)
		{
			if (!this.Input.MoveToAttribute(localName, ns))
			{
				return null;
			}
			string value = this.Input.Value;
			this.Input.MoveToParent();
			return value;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001A64C File Offset: 0x0001884C
		public XslAvt ParseAvtAttribute(string localName)
		{
			return this.ParseAvtAttribute(localName, string.Empty);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001A65C File Offset: 0x0001885C
		public XslAvt ParseAvtAttribute(string localName, string ns)
		{
			return this.ParseAvt(this.GetAttribute(localName, ns));
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001A66C File Offset: 0x0001886C
		public void AssertAttribute(string localName)
		{
			this.AssertAttribute(localName, string.Empty);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001A67C File Offset: 0x0001887C
		public void AssertAttribute(string localName, string ns)
		{
			if (this.Input.GetAttribute(localName, ns) == null)
			{
				throw new XsltCompileException("Was expecting the " + localName + " attribute", null, this.Input);
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001A6B8 File Offset: 0x000188B8
		public XslAvt ParseAvt(string s)
		{
			if (s == null)
			{
				return null;
			}
			return new XslAvt(s, this);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001A6CC File Offset: 0x000188CC
		public Pattern CompilePattern(string pattern, XPathNavigator loc)
		{
			if (pattern == null || pattern == string.Empty)
			{
				return null;
			}
			Pattern pattern2 = Pattern.Compile(pattern, this);
			if (pattern2 == null)
			{
				throw new XsltCompileException(string.Format("Invalid pattern '{0}'", pattern), null, loc);
			}
			return pattern2;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001A714 File Offset: 0x00018914
		internal CompiledExpression CompileExpression(string expression)
		{
			return this.CompileExpression(expression, false);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001A720 File Offset: 0x00018920
		internal CompiledExpression CompileExpression(string expression, bool isKey)
		{
			if (expression == null || expression == string.Empty)
			{
				return null;
			}
			Expression expr = this.xpathParser.Compile(expression);
			if (isKey)
			{
				expr = new ExprKeyContainer(expr);
			}
			return new CompiledExpression(expression, expr);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001A768 File Offset: 0x00018968
		public XslOperation CompileTemplateContent()
		{
			return this.CompileTemplateContent(XPathNodeType.All, false);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001A774 File Offset: 0x00018974
		public XslOperation CompileTemplateContent(XPathNodeType parentType)
		{
			return this.CompileTemplateContent(parentType, false);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001A780 File Offset: 0x00018980
		public XslOperation CompileTemplateContent(XPathNodeType parentType, bool xslForEach)
		{
			return new XslTemplateContent(this, parentType, xslForEach);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001A78C File Offset: 0x0001898C
		public void AddGlobalVariable(XslGlobalVariable var)
		{
			this.globalVariables[var.Name] = var;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001A7A0 File Offset: 0x000189A0
		public void AddKey(XslKey key)
		{
			if (this.keys[key.Name] == null)
			{
				this.keys[key.Name] = new ArrayList();
			}
			((ArrayList)this.keys[key.Name]).Add(key);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001A7F8 File Offset: 0x000189F8
		public void AddAttributeSet(XslAttributeSet set)
		{
			XslAttributeSet xslAttributeSet = this.attrSets[set.Name] as XslAttributeSet;
			if (xslAttributeSet != null)
			{
				xslAttributeSet.Merge(set);
				this.attrSets[set.Name] = xslAttributeSet;
			}
			else
			{
				this.attrSets[set.Name] = set;
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001A854 File Offset: 0x00018A54
		public void PushScope()
		{
			this.curVarScope = new VariableScope(this.curVarScope);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001A868 File Offset: 0x00018A68
		public VariableScope PopScope()
		{
			this.curVarScope.giveHighTideToParent();
			VariableScope result = this.curVarScope;
			this.curVarScope = this.curVarScope.Parent;
			return result;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001A89C File Offset: 0x00018A9C
		public int AddVariable(XslLocalVariable v)
		{
			if (this.curVarScope == null)
			{
				throw new XsltCompileException("Not initialized variable", null, this.Input);
			}
			return this.curVarScope.AddVariable(v);
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0001A8C8 File Offset: 0x00018AC8
		public VariableScope CurrentVariableScope
		{
			get
			{
				return this.curVarScope;
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001A8D0 File Offset: 0x00018AD0
		public bool IsExtensionNamespace(string nsUri)
		{
			if (nsUri == "http://www.w3.org/1999/XSL/Transform")
			{
				return true;
			}
			XPathNavigator xpathNavigator = this.Input.Clone();
			XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
			for (;;)
			{
				bool flag = xpathNavigator.NamespaceURI == "http://www.w3.org/1999/XSL/Transform";
				xpathNavigator2.MoveTo(xpathNavigator);
				if (xpathNavigator.MoveToFirstAttribute())
				{
					do
					{
						if (xpathNavigator.LocalName == "extension-element-prefixes" && xpathNavigator.NamespaceURI == ((!flag) ? "http://www.w3.org/1999/XSL/Transform" : string.Empty))
						{
							foreach (string text in xpathNavigator.Value.Split(new char[]
							{
								' '
							}))
							{
								if (xpathNavigator2.GetNamespace((!(text == "#default")) ? text : string.Empty) == nsUri)
								{
									return true;
								}
							}
						}
					}
					while (xpathNavigator.MoveToNextAttribute());
					xpathNavigator.MoveToParent();
				}
				if (!xpathNavigator.MoveToParent())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001A9E4 File Offset: 0x00018BE4
		public Hashtable GetNamespacesToCopy()
		{
			Hashtable hashtable = new Hashtable();
			XPathNavigator xpathNavigator = this.Input.Clone();
			XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
			if (xpathNavigator.MoveToFirstNamespace(XPathNamespaceScope.ExcludeXml))
			{
				do
				{
					if (xpathNavigator.Value != "http://www.w3.org/1999/XSL/Transform" && !hashtable.Contains(xpathNavigator.Name))
					{
						hashtable.Add(xpathNavigator.Name, xpathNavigator.Value);
					}
				}
				while (xpathNavigator.MoveToNextNamespace(XPathNamespaceScope.ExcludeXml));
				xpathNavigator.MoveToParent();
			}
			do
			{
				bool flag = xpathNavigator.NamespaceURI == "http://www.w3.org/1999/XSL/Transform";
				xpathNavigator2.MoveTo(xpathNavigator);
				if (xpathNavigator.MoveToFirstAttribute())
				{
					do
					{
						if ((xpathNavigator.LocalName == "extension-element-prefixes" || xpathNavigator.LocalName == "exclude-result-prefixes") && xpathNavigator.NamespaceURI == ((!flag) ? "http://www.w3.org/1999/XSL/Transform" : string.Empty))
						{
							foreach (string text in xpathNavigator.Value.Split(new char[]
							{
								' '
							}))
							{
								string text2 = (!(text == "#default")) ? text : string.Empty;
								if ((string)hashtable[text2] == xpathNavigator2.GetNamespace(text2))
								{
									hashtable.Remove(text2);
								}
							}
						}
					}
					while (xpathNavigator.MoveToNextAttribute());
					xpathNavigator.MoveToParent();
				}
			}
			while (xpathNavigator.MoveToParent());
			return hashtable;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001AB70 File Offset: 0x00018D70
		public void CompileDecimalFormat()
		{
			XmlQualifiedName xmlQualifiedName = this.ParseQNameAttribute("name");
			try
			{
				if (xmlQualifiedName.Name != string.Empty)
				{
					XmlConvert.VerifyNCName(xmlQualifiedName.Name);
				}
			}
			catch (XmlException innerException)
			{
				throw new XsltCompileException("Invalid qualified name", innerException, this.Input);
			}
			XslDecimalFormat xslDecimalFormat = new XslDecimalFormat(this);
			if (this.decimalFormats.Contains(xmlQualifiedName))
			{
				((XslDecimalFormat)this.decimalFormats[xmlQualifiedName]).CheckSameAs(xslDecimalFormat);
			}
			else
			{
				this.decimalFormats[xmlQualifiedName] = xslDecimalFormat;
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001AC24 File Offset: 0x00018E24
		public string LookupNamespace(string prefix)
		{
			if (prefix == string.Empty || prefix == null)
			{
				return string.Empty;
			}
			XPathNavigator xpathNavigator = this.Input;
			if (this.Input.NodeType == XPathNodeType.Attribute)
			{
				xpathNavigator = this.Input.Clone();
				xpathNavigator.MoveToParent();
			}
			return xpathNavigator.GetNamespace(prefix);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001AC80 File Offset: 0x00018E80
		public void CompileOutput()
		{
			XPathNavigator input = this.Input;
			string attribute = input.GetAttribute("href", string.Empty);
			XslOutput xslOutput = this.outputs[attribute] as XslOutput;
			if (xslOutput == null)
			{
				xslOutput = new XslOutput(attribute, this.stylesheetVersion);
				this.outputs.Add(attribute, xslOutput);
			}
			xslOutput.Fill(input);
		}

		// Token: 0x040002B2 RID: 690
		public const string XsltNamespace = "http://www.w3.org/1999/XSL/Transform";

		// Token: 0x040002B3 RID: 691
		private ArrayList inputStack = new ArrayList();

		// Token: 0x040002B4 RID: 692
		private XPathNavigator currentInput;

		// Token: 0x040002B5 RID: 693
		private Stack styleStack = new Stack();

		// Token: 0x040002B6 RID: 694
		private XslStylesheet currentStyle;

		// Token: 0x040002B7 RID: 695
		private Hashtable keys = new Hashtable();

		// Token: 0x040002B8 RID: 696
		private Hashtable globalVariables = new Hashtable();

		// Token: 0x040002B9 RID: 697
		private Hashtable attrSets = new Hashtable();

		// Token: 0x040002BA RID: 698
		private XmlNamespaceManager nsMgr = new XmlNamespaceManager(new NameTable());

		// Token: 0x040002BB RID: 699
		private XmlResolver res;

		// Token: 0x040002BC RID: 700
		private Evidence evidence;

		// Token: 0x040002BD RID: 701
		private XslStylesheet rootStyle;

		// Token: 0x040002BE RID: 702
		private Hashtable outputs = new Hashtable();

		// Token: 0x040002BF RID: 703
		private bool keyCompilationMode;

		// Token: 0x040002C0 RID: 704
		private string stylesheetVersion;

		// Token: 0x040002C1 RID: 705
		private XsltDebuggerWrapper debugger;

		// Token: 0x040002C2 RID: 706
		private MSXslScriptManager msScripts = new MSXslScriptManager();

		// Token: 0x040002C3 RID: 707
		internal XPathParser xpathParser;

		// Token: 0x040002C4 RID: 708
		internal XsltPatternParser patternParser;

		// Token: 0x040002C5 RID: 709
		private VariableScope curVarScope;

		// Token: 0x040002C6 RID: 710
		private Hashtable decimalFormats = new Hashtable();
	}
}
