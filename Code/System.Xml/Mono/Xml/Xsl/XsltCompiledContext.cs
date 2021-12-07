using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Mono.Xml.XPath;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000A0 RID: 160
	internal class XsltCompiledContext : XsltContext
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x00022718 File Offset: 0x00020918
		public XsltCompiledContext(XslTransformProcessor p) : base(new NameTable())
		{
			this.p = p;
			this.scopes = new XsltCompiledContext.XsltContextInfo[10];
			for (int i = 0; i < 10; i++)
			{
				this.scopes[i] = new XsltCompiledContext.XsltContextInfo();
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00022788 File Offset: 0x00020988
		public XslTransformProcessor Processor
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00022790 File Offset: 0x00020990
		public override string DefaultNamespace
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00022798 File Offset: 0x00020998
		public XPathNavigator GetNavCache(Pattern p, XPathNavigator node)
		{
			XPathNavigator xpathNavigator = this.patternNavCaches[p] as XPathNavigator;
			if (xpathNavigator == null || !xpathNavigator.MoveTo(node))
			{
				xpathNavigator = node.Clone();
				this.patternNavCaches[p] = xpathNavigator;
			}
			return xpathNavigator;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000227E0 File Offset: 0x000209E0
		public object EvaluateKey(IStaticXsltContext staticContext, BaseIterator iter, Expression nameExpr, Expression valueExpr)
		{
			XmlQualifiedName keyName = this.GetKeyName(staticContext, iter, nameExpr);
			KeyIndexTable indexTable = this.GetIndexTable(keyName);
			return indexTable.Evaluate(iter, valueExpr);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00022808 File Offset: 0x00020A08
		public bool MatchesKey(XPathNavigator nav, IStaticXsltContext staticContext, string name, string value)
		{
			XmlQualifiedName name2 = XslNameUtil.FromString(name, staticContext);
			KeyIndexTable indexTable = this.GetIndexTable(name2);
			return indexTable.Matches(nav, value, this);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00022830 File Offset: 0x00020A30
		private XmlQualifiedName GetKeyName(IStaticXsltContext staticContext, BaseIterator iter, Expression nameExpr)
		{
			XmlQualifiedName xmlQualifiedName;
			if (nameExpr.HasStaticValue)
			{
				xmlQualifiedName = (XmlQualifiedName)this.keyNameCache[nameExpr];
				if (xmlQualifiedName == null)
				{
					xmlQualifiedName = XslNameUtil.FromString(nameExpr.EvaluateString(iter), staticContext);
					this.keyNameCache[nameExpr] = xmlQualifiedName;
				}
			}
			else
			{
				xmlQualifiedName = XslNameUtil.FromString(nameExpr.EvaluateString(iter), this);
			}
			return xmlQualifiedName;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00022898 File Offset: 0x00020A98
		private KeyIndexTable GetIndexTable(XmlQualifiedName name)
		{
			KeyIndexTable keyIndexTable = this.keyIndexTables[name] as KeyIndexTable;
			if (keyIndexTable == null)
			{
				keyIndexTable = new KeyIndexTable(this, this.p.CompiledStyle.ResolveKey(name));
				this.keyIndexTables[name] = keyIndexTable;
			}
			return keyIndexTable;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000228E4 File Offset: 0x00020AE4
		public override string LookupNamespace(string prefix)
		{
			throw new InvalidOperationException("we should never get here");
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000228F0 File Offset: 0x00020AF0
		internal override IXsltContextFunction ResolveFunction(XmlQualifiedName name, XPathResultType[] argTypes)
		{
			string @namespace = name.Namespace;
			if (@namespace == null)
			{
				return null;
			}
			object obj = null;
			if (this.p.Arguments != null)
			{
				obj = this.p.Arguments.GetExtensionObject(@namespace);
			}
			bool isScript = false;
			if (obj == null)
			{
				obj = this.p.ScriptManager.GetExtensionObject(@namespace);
				if (obj == null)
				{
					return null;
				}
				isScript = true;
			}
			MethodInfo methodInfo = this.FindBestMethod(obj.GetType(), name.Name, argTypes, isScript);
			if (methodInfo != null)
			{
				return new XsltExtensionFunction(obj, methodInfo, this.p.CurrentNode);
			}
			return null;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00022984 File Offset: 0x00020B84
		private MethodInfo FindBestMethod(Type t, string name, XPathResultType[] argTypes, bool isScript)
		{
			MethodInfo[] methods = t.GetMethods(((!isScript) ? BindingFlags.Public : (BindingFlags.Public | BindingFlags.NonPublic)) | BindingFlags.Instance | BindingFlags.Static);
			if (methods.Length == 0)
			{
				return null;
			}
			if (argTypes == null)
			{
				return methods[0];
			}
			int num = 0;
			int num2 = argTypes.Length;
			for (int i = 0; i < methods.Length; i++)
			{
				if (methods[i].Name == name && methods[i].GetParameters().Length == num2)
				{
					methods[num++] = methods[i];
				}
			}
			int num3 = num;
			if (num3 == 0)
			{
				return null;
			}
			if (num3 == 1)
			{
				return methods[0];
			}
			for (int j = 0; j < num3; j++)
			{
				bool flag = true;
				ParameterInfo[] parameters = methods[j].GetParameters();
				for (int k = 0; k < parameters.Length; k++)
				{
					XPathResultType xpathResultType = argTypes[k];
					if (xpathResultType != XPathResultType.Any)
					{
						XPathResultType xpathType = XPFuncImpl.GetXPathType(parameters[k].ParameterType, this.p.CurrentNode);
						if (xpathType != xpathResultType && xpathType != XPathResultType.Any)
						{
							flag = false;
							break;
						}
						if (xpathType == XPathResultType.Any && xpathResultType != XPathResultType.NodeSet && parameters[k].ParameterType != typeof(object))
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					return methods[j];
				}
			}
			return null;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00022AE8 File Offset: 0x00020CE8
		public override IXsltContextVariable ResolveVariable(string prefix, string name)
		{
			throw new InvalidOperationException("shouldn't get here");
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00022AF4 File Offset: 0x00020CF4
		public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] ArgTypes)
		{
			throw new InvalidOperationException("XsltCompiledContext exception: shouldn't get here.");
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00022B00 File Offset: 0x00020D00
		internal override IXsltContextVariable ResolveVariable(XmlQualifiedName q)
		{
			return this.p.CompiledStyle.ResolveVariable(q);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00022B14 File Offset: 0x00020D14
		public override int CompareDocument(string baseUri, string nextBaseUri)
		{
			return baseUri.GetHashCode().CompareTo(nextBaseUri.GetHashCode());
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00022B38 File Offset: 0x00020D38
		public override bool PreserveWhitespace(XPathNavigator nav)
		{
			return this.p.CompiledStyle.Style.GetPreserveWhitespace(nav);
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00022B50 File Offset: 0x00020D50
		public override bool Whitespace
		{
			get
			{
				return this.WhitespaceHandling;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00022B58 File Offset: 0x00020D58
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x00022B6C File Offset: 0x00020D6C
		public bool IsCData
		{
			get
			{
				return this.scopes[this.scopeAt].IsCData;
			}
			set
			{
				this.scopes[this.scopeAt].IsCData = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00022B84 File Offset: 0x00020D84
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00022B98 File Offset: 0x00020D98
		public bool WhitespaceHandling
		{
			get
			{
				return this.scopes[this.scopeAt].PreserveWhitespace;
			}
			set
			{
				this.scopes[this.scopeAt].PreserveWhitespace = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00022BB0 File Offset: 0x00020DB0
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x00022BC4 File Offset: 0x00020DC4
		public string ElementPrefix
		{
			get
			{
				return this.scopes[this.scopeAt].ElementPrefix;
			}
			set
			{
				this.scopes[this.scopeAt].ElementPrefix = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00022BDC File Offset: 0x00020DDC
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x00022BF0 File Offset: 0x00020DF0
		public string ElementNamespace
		{
			get
			{
				return this.scopes[this.scopeAt].ElementNamespace;
			}
			set
			{
				this.scopes[this.scopeAt].ElementNamespace = value;
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00022C08 File Offset: 0x00020E08
		private void ExtendScope()
		{
			XsltCompiledContext.XsltContextInfo[] sourceArray = this.scopes;
			this.scopes = new XsltCompiledContext.XsltContextInfo[this.scopeAt * 2 + 1];
			if (this.scopeAt > 0)
			{
				Array.Copy(sourceArray, 0, this.scopes, 0, this.scopeAt);
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00022C54 File Offset: 0x00020E54
		public override bool PopScope()
		{
			base.PopScope();
			if (this.scopeAt == -1)
			{
				return false;
			}
			this.scopeAt--;
			return true;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00022C88 File Offset: 0x00020E88
		public override void PushScope()
		{
			base.PushScope();
			this.scopeAt++;
			if (this.scopeAt == this.scopes.Length)
			{
				this.ExtendScope();
			}
			if (this.scopes[this.scopeAt] == null)
			{
				this.scopes[this.scopeAt] = new XsltCompiledContext.XsltContextInfo();
			}
			else
			{
				this.scopes[this.scopeAt].Clear();
			}
		}

		// Token: 0x0400038B RID: 907
		private Hashtable keyNameCache = new Hashtable();

		// Token: 0x0400038C RID: 908
		private Hashtable keyIndexTables = new Hashtable();

		// Token: 0x0400038D RID: 909
		private Hashtable patternNavCaches = new Hashtable();

		// Token: 0x0400038E RID: 910
		private XslTransformProcessor p;

		// Token: 0x0400038F RID: 911
		private XsltCompiledContext.XsltContextInfo[] scopes;

		// Token: 0x04000390 RID: 912
		private int scopeAt;

		// Token: 0x020000A1 RID: 161
		private class XsltContextInfo
		{
			// Token: 0x0600059C RID: 1436 RVA: 0x00022D10 File Offset: 0x00020F10
			public void Clear()
			{
				this.IsCData = false;
				this.PreserveWhitespace = true;
				this.ElementPrefix = (this.ElementNamespace = null);
			}

			// Token: 0x04000391 RID: 913
			public bool IsCData;

			// Token: 0x04000392 RID: 914
			public bool PreserveWhitespace = true;

			// Token: 0x04000393 RID: 915
			public string ElementPrefix;

			// Token: 0x04000394 RID: 916
			public string ElementNamespace;
		}
	}
}
