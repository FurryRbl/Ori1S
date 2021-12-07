using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Mono.Xml.Xsl.Operations;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000097 RID: 151
	internal class XslStylesheet
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0001FDC0 File Offset: 0x0001DFC0
		public XmlQualifiedName[] ExtensionElementPrefixes
		{
			get
			{
				return this.extensionElementPrefixes;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0001FDC8 File Offset: 0x0001DFC8
		public XmlQualifiedName[] ExcludeResultPrefixes
		{
			get
			{
				return this.excludeResultPrefixes;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0001FDD0 File Offset: 0x0001DFD0
		public ArrayList StylesheetNamespaces
		{
			get
			{
				return this.stylesheetNamespaces;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0001FDD8 File Offset: 0x0001DFD8
		public ArrayList Imports
		{
			get
			{
				return this.imports;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001FDE0 File Offset: 0x0001DFE0
		public Hashtable SpaceControls
		{
			get
			{
				return this.spaceControls;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0001FDE8 File Offset: 0x0001DFE8
		public NameValueCollection NamespaceAliases
		{
			get
			{
				return this.namespaceAliases;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0001FDF0 File Offset: 0x0001DFF0
		public Hashtable Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0001FDF8 File Offset: 0x0001DFF8
		public XslTemplateTable Templates
		{
			get
			{
				return this.templates;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0001FE00 File Offset: 0x0001E000
		public string BaseURI
		{
			get
			{
				return this.baseURI;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0001FE08 File Offset: 0x0001E008
		public string Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001FE10 File Offset: 0x0001E010
		internal void Compile(Compiler c)
		{
			c.PushStylesheet(this);
			this.templates = new XslTemplateTable(this);
			this.baseURI = c.Input.BaseURI;
			while (c.Input.NodeType != XPathNodeType.Element)
			{
				if (!c.Input.MoveToNext())
				{
					throw new XsltCompileException("Stylesheet root element must be either \"stylesheet\" or \"transform\" or any literal element", null, c.Input);
				}
			}
			if (c.Input.NamespaceURI != "http://www.w3.org/1999/XSL/Transform")
			{
				if (c.Input.GetAttribute("version", "http://www.w3.org/1999/XSL/Transform") == string.Empty)
				{
					throw new XsltCompileException("Mandatory global attribute version is missing", null, c.Input);
				}
				this.templates.Add(new XslTemplate(c));
			}
			else
			{
				if (c.Input.LocalName != "stylesheet" && c.Input.LocalName != "transform")
				{
					throw new XsltCompileException("Stylesheet root element must be either \"stylesheet\" or \"transform\" or any literal element", null, c.Input);
				}
				this.version = c.Input.GetAttribute("version", string.Empty);
				if (this.version == string.Empty)
				{
					throw new XsltCompileException("Mandatory attribute version is missing", null, c.Input);
				}
				this.extensionElementPrefixes = this.ParseMappedPrefixes(c.GetAttribute("extension-element-prefixes"), c.Input);
				this.excludeResultPrefixes = this.ParseMappedPrefixes(c.GetAttribute("exclude-result-prefixes"), c.Input);
				if (c.Input.MoveToFirstNamespace(XPathNamespaceScope.Local))
				{
					do
					{
						if (!(c.Input.Value == "http://www.w3.org/1999/XSL/Transform"))
						{
							this.stylesheetNamespaces.Insert(0, new XmlQualifiedName(c.Input.Name, c.Input.Value));
						}
					}
					while (c.Input.MoveToNextNamespace(XPathNamespaceScope.Local));
					c.Input.MoveToParent();
				}
				this.ProcessTopLevelElements(c);
			}
			foreach (object obj in this.variables.Values)
			{
				XslGlobalVariable var = (XslGlobalVariable)obj;
				c.AddGlobalVariable(var);
			}
			foreach (object obj2 in this.keys.Values)
			{
				ArrayList arrayList = (ArrayList)obj2;
				for (int i = 0; i < arrayList.Count; i++)
				{
					c.AddKey((XslKey)arrayList[i]);
				}
			}
			c.PopStylesheet();
			this.inProcessIncludes = null;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00020120 File Offset: 0x0001E320
		private XmlQualifiedName[] ParseMappedPrefixes(string list, XPathNavigator nav)
		{
			if (list == null)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList();
			foreach (string text in list.Split(XmlChar.WhitespaceChars))
			{
				if (text.Length != 0)
				{
					if (text == "#default")
					{
						arrayList.Add(new XmlQualifiedName(string.Empty, string.Empty));
					}
					else
					{
						string @namespace = nav.GetNamespace(text);
						if (@namespace != string.Empty)
						{
							arrayList.Add(new XmlQualifiedName(text, @namespace));
						}
					}
				}
			}
			return (XmlQualifiedName[])arrayList.ToArray(typeof(XmlQualifiedName));
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x000201D8 File Offset: 0x0001E3D8
		public bool HasSpaceControls
		{
			get
			{
				if (!this.countedSpaceControlExistence)
				{
					this.countedSpaceControlExistence = true;
					this.cachedHasSpaceControls = this.ComputeHasSpaceControls();
				}
				return this.cachedHasSpaceControls;
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0002020C File Offset: 0x0001E40C
		private bool ComputeHasSpaceControls()
		{
			if (this.spaceControls.Count > 0 && this.HasStripSpace(this.spaceControls))
			{
				return true;
			}
			if (this.imports.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < this.imports.Count; i++)
			{
				XslStylesheet xslStylesheet = (XslStylesheet)this.imports[i];
				if (xslStylesheet.spaceControls.Count > 0 && this.HasStripSpace(xslStylesheet.spaceControls))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000202A4 File Offset: 0x0001E4A4
		private bool HasStripSpace(IDictionary table)
		{
			foreach (object obj in table.Values)
			{
				XmlSpace xmlSpace = (XmlSpace)((int)obj);
				if (xmlSpace == XmlSpace.Default)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00020320 File Offset: 0x0001E520
		public bool GetPreserveWhitespace(XPathNavigator nav)
		{
			if (!this.HasSpaceControls)
			{
				return true;
			}
			nav = nav.Clone();
			if (!nav.MoveToParent() || nav.NodeType != XPathNodeType.Element)
			{
				object defaultXmlSpace = this.GetDefaultXmlSpace();
				return defaultXmlSpace == null || (int)defaultXmlSpace == 2;
			}
			string localName = nav.LocalName;
			string namespaceURI = nav.NamespaceURI;
			XmlQualifiedName key = new XmlQualifiedName(localName, namespaceURI);
			object obj = this.spaceControls[key];
			if (obj == null)
			{
				for (int i = 0; i < this.imports.Count; i++)
				{
					obj = ((XslStylesheet)this.imports[i]).SpaceControls[key];
					if (obj != null)
					{
						break;
					}
				}
			}
			if (obj == null)
			{
				key = new XmlQualifiedName("*", namespaceURI);
				obj = this.spaceControls[key];
				if (obj == null)
				{
					for (int j = 0; j < this.imports.Count; j++)
					{
						obj = ((XslStylesheet)this.imports[j]).SpaceControls[key];
						if (obj != null)
						{
							break;
						}
					}
				}
			}
			if (obj == null)
			{
				obj = this.GetDefaultXmlSpace();
			}
			if (obj != null)
			{
				XmlSpace xmlSpace = (XmlSpace)((int)obj);
				if (xmlSpace == XmlSpace.Default)
				{
					return false;
				}
				if (xmlSpace == XmlSpace.Preserve)
				{
					return true;
				}
			}
			throw new SystemException("Mono BUG: should not reach here");
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000204A0 File Offset: 0x0001E6A0
		private object GetDefaultXmlSpace()
		{
			object obj = this.spaceControls[XslStylesheet.allMatchName];
			if (obj == null)
			{
				for (int i = 0; i < this.imports.Count; i++)
				{
					obj = ((XslStylesheet)this.imports[i]).SpaceControls[XslStylesheet.allMatchName];
					if (obj != null)
					{
						break;
					}
				}
			}
			return obj;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00020510 File Offset: 0x0001E710
		public bool HasNamespaceAliases
		{
			get
			{
				if (!this.countedNamespaceAliases)
				{
					this.countedNamespaceAliases = true;
					if (this.namespaceAliases.Count > 0)
					{
						this.cachedHasNamespaceAliases = true;
					}
					else if (this.imports.Count == 0)
					{
						this.cachedHasNamespaceAliases = false;
					}
					else
					{
						for (int i = 0; i < this.imports.Count; i++)
						{
							if (((XslStylesheet)this.imports[i]).namespaceAliases.Count > 0)
							{
								this.countedNamespaceAliases = true;
							}
						}
						this.cachedHasNamespaceAliases = false;
					}
				}
				return this.cachedHasNamespaceAliases;
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000205BC File Offset: 0x0001E7BC
		public string GetActualPrefix(string prefix)
		{
			if (!this.HasNamespaceAliases)
			{
				return prefix;
			}
			string text = this.namespaceAliases[prefix];
			if (text == null)
			{
				for (int i = 0; i < this.imports.Count; i++)
				{
					text = ((XslStylesheet)this.imports[i]).namespaceAliases[prefix];
					if (text != null)
					{
						break;
					}
				}
			}
			return (text == null) ? prefix : text;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0002063C File Offset: 0x0001E83C
		private void StoreInclude(Compiler c)
		{
			XPathNavigator key = c.Input.Clone();
			c.PushInputDocument(c.Input.GetAttribute("href", string.Empty));
			this.inProcessIncludes[key] = c.Input;
			this.HandleImportsInInclude(c);
			c.PopInputDocument();
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00020690 File Offset: 0x0001E890
		private void HandleImportsInInclude(Compiler c)
		{
			if (c.Input.NamespaceURI != "http://www.w3.org/1999/XSL/Transform")
			{
				if (c.Input.GetAttribute("version", "http://www.w3.org/1999/XSL/Transform") == string.Empty)
				{
					throw new XsltCompileException("Mandatory global attribute version is missing", null, c.Input);
				}
				return;
			}
			else
			{
				if (!c.Input.MoveToFirstChild())
				{
					c.Input.MoveToRoot();
					return;
				}
				this.HandleIncludesImports(c);
				return;
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00020714 File Offset: 0x0001E914
		private void HandleInclude(Compiler c)
		{
			XPathNavigator xpathNavigator = null;
			foreach (object obj in this.inProcessIncludes.Keys)
			{
				XPathNavigator xpathNavigator2 = (XPathNavigator)obj;
				if (xpathNavigator2.IsSamePosition(c.Input))
				{
					xpathNavigator = (XPathNavigator)this.inProcessIncludes[xpathNavigator2];
					break;
				}
			}
			if (xpathNavigator == null)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Should not happen. Current input is ",
					c.Input.BaseURI,
					" / ",
					c.Input.Name,
					", ",
					this.inProcessIncludes.Count
				}));
			}
			if (xpathNavigator.NodeType == XPathNodeType.Root)
			{
				return;
			}
			c.PushInputDocument(xpathNavigator);
			while (c.Input.NodeType != XPathNodeType.Element)
			{
				if (!c.Input.MoveToNext())
				{
					break;
				}
			}
			if (c.Input.NamespaceURI != "http://www.w3.org/1999/XSL/Transform" && c.Input.NodeType == XPathNodeType.Element)
			{
				this.templates.Add(new XslTemplate(c));
			}
			else
			{
				do
				{
					if (c.Input.NodeType == XPathNodeType.Element)
					{
						this.HandleTopLevelElement(c);
					}
				}
				while (c.Input.MoveToNext());
			}
			c.Input.MoveToParent();
			c.PopInputDocument();
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000208C8 File Offset: 0x0001EAC8
		private void HandleImport(Compiler c, string href)
		{
			c.PushInputDocument(href);
			XslStylesheet xslStylesheet = new XslStylesheet();
			xslStylesheet.Compile(c);
			this.imports.Add(xslStylesheet);
			c.PopInputDocument();
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000208FC File Offset: 0x0001EAFC
		private void HandleTopLevelElement(Compiler c)
		{
			XPathNavigator input = c.Input;
			string namespaceURI = input.NamespaceURI;
			if (namespaceURI != null)
			{
				if (XslStylesheet.<>f__switch$map23 == null)
				{
					XslStylesheet.<>f__switch$map23 = new Dictionary<string, int>(2)
					{
						{
							"http://www.w3.org/1999/XSL/Transform",
							0
						},
						{
							"urn:schemas-microsoft-com:xslt",
							1
						}
					};
				}
				int num;
				if (XslStylesheet.<>f__switch$map23.TryGetValue(namespaceURI, out num))
				{
					if (num != 0)
					{
						if (num == 1)
						{
							string localName = input.LocalName;
							if (localName != null)
							{
								if (XslStylesheet.<>f__switch$map22 == null)
								{
									XslStylesheet.<>f__switch$map22 = new Dictionary<string, int>(1)
									{
										{
											"script",
											0
										}
									};
								}
								int num2;
								if (XslStylesheet.<>f__switch$map22.TryGetValue(localName, out num2))
								{
									if (num2 == 0)
									{
										c.ScriptManager.AddScript(c);
									}
								}
							}
						}
					}
					else
					{
						string localName = input.LocalName;
						switch (localName)
						{
						case "include":
							this.HandleInclude(c);
							goto IL_2B0;
						case "preserve-space":
							this.AddSpaceControls(c.ParseQNameListAttribute("elements"), XmlSpace.Preserve, input);
							goto IL_2B0;
						case "strip-space":
							this.AddSpaceControls(c.ParseQNameListAttribute("elements"), XmlSpace.Default, input);
							goto IL_2B0;
						case "namespace-alias":
							goto IL_2B0;
						case "attribute-set":
							c.AddAttributeSet(new XslAttributeSet(c));
							goto IL_2B0;
						case "key":
						{
							XslKey xslKey = new XslKey(c);
							if (this.keys[xslKey.Name] == null)
							{
								this.keys[xslKey.Name] = new ArrayList();
							}
							((ArrayList)this.keys[xslKey.Name]).Add(xslKey);
							goto IL_2B0;
						}
						case "output":
							c.CompileOutput();
							goto IL_2B0;
						case "decimal-format":
							c.CompileDecimalFormat();
							goto IL_2B0;
						case "template":
							this.templates.Add(new XslTemplate(c));
							goto IL_2B0;
						case "variable":
						{
							XslGlobalVariable xslGlobalVariable = new XslGlobalVariable(c);
							this.variables[xslGlobalVariable.Name] = xslGlobalVariable;
							goto IL_2B0;
						}
						case "param":
						{
							XslGlobalParam xslGlobalParam = new XslGlobalParam(c);
							this.variables[xslGlobalParam.Name] = xslGlobalParam;
							goto IL_2B0;
						}
						}
						if (this.version == "1.0")
						{
							throw new XsltCompileException("Unrecognized top level element after imports", null, c.Input);
						}
						IL_2B0:;
					}
				}
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00020C28 File Offset: 0x0001EE28
		private XPathNavigator HandleIncludesImports(Compiler c)
		{
			do
			{
				if (c.Input.NodeType == XPathNodeType.Element)
				{
					if (c.Input.LocalName != "import" || c.Input.NamespaceURI != "http://www.w3.org/1999/XSL/Transform")
					{
						break;
					}
					this.HandleImport(c, c.GetAttribute("href"));
				}
			}
			while (c.Input.MoveToNext());
			XPathNavigator xpathNavigator = c.Input.Clone();
			do
			{
				if (c.Input.NodeType == XPathNodeType.Element && !(c.Input.LocalName != "include") && !(c.Input.NamespaceURI != "http://www.w3.org/1999/XSL/Transform"))
				{
					this.StoreInclude(c);
				}
			}
			while (c.Input.MoveToNext());
			c.Input.MoveTo(xpathNavigator);
			return xpathNavigator;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00020D24 File Offset: 0x0001EF24
		private void ProcessTopLevelElements(Compiler c)
		{
			if (!c.Input.MoveToFirstChild())
			{
				return;
			}
			XPathNavigator other = this.HandleIncludesImports(c);
			do
			{
				if (c.Input.NodeType == XPathNodeType.Element && !(c.Input.LocalName != "namespace-alias") && !(c.Input.NamespaceURI != "http://www.w3.org/1999/XSL/Transform"))
				{
					string text = c.GetAttribute("stylesheet-prefix", string.Empty);
					if (text == "#default")
					{
						text = string.Empty;
					}
					string text2 = c.GetAttribute("result-prefix", string.Empty);
					if (text2 == "#default")
					{
						text2 = string.Empty;
					}
					this.namespaceAliases.Set(text, text2);
				}
			}
			while (c.Input.MoveToNext());
			c.Input.MoveTo(other);
			do
			{
				if (c.Input.NodeType == XPathNodeType.Element)
				{
					this.HandleTopLevelElement(c);
				}
			}
			while (c.Input.MoveToNext());
			c.Input.MoveToParent();
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00020E48 File Offset: 0x0001F048
		private void AddSpaceControls(XmlQualifiedName[] names, XmlSpace result, XPathNavigator styleElem)
		{
			foreach (XmlQualifiedName key in names)
			{
				this.spaceControls[key] = result;
			}
		}

		// Token: 0x04000348 RID: 840
		public const string XsltNamespace = "http://www.w3.org/1999/XSL/Transform";

		// Token: 0x04000349 RID: 841
		public const string MSXsltNamespace = "urn:schemas-microsoft-com:xslt";

		// Token: 0x0400034A RID: 842
		private ArrayList imports = new ArrayList();

		// Token: 0x0400034B RID: 843
		private Hashtable spaceControls = new Hashtable();

		// Token: 0x0400034C RID: 844
		private NameValueCollection namespaceAliases = new NameValueCollection();

		// Token: 0x0400034D RID: 845
		private Hashtable parameters = new Hashtable();

		// Token: 0x0400034E RID: 846
		private Hashtable keys = new Hashtable();

		// Token: 0x0400034F RID: 847
		private Hashtable variables = new Hashtable();

		// Token: 0x04000350 RID: 848
		private XslTemplateTable templates;

		// Token: 0x04000351 RID: 849
		private string baseURI;

		// Token: 0x04000352 RID: 850
		private string version;

		// Token: 0x04000353 RID: 851
		private XmlQualifiedName[] extensionElementPrefixes;

		// Token: 0x04000354 RID: 852
		private XmlQualifiedName[] excludeResultPrefixes;

		// Token: 0x04000355 RID: 853
		private ArrayList stylesheetNamespaces = new ArrayList();

		// Token: 0x04000356 RID: 854
		private Hashtable inProcessIncludes = new Hashtable();

		// Token: 0x04000357 RID: 855
		private bool countedSpaceControlExistence;

		// Token: 0x04000358 RID: 856
		private bool cachedHasSpaceControls;

		// Token: 0x04000359 RID: 857
		private static readonly XmlQualifiedName allMatchName = new XmlQualifiedName("*");

		// Token: 0x0400035A RID: 858
		private bool countedNamespaceAliases;

		// Token: 0x0400035B RID: 859
		private bool cachedHasNamespaceAliases;
	}
}
