using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Mono.Xml.XPath;
using Mono.Xml.Xsl.Operations;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200009F RID: 159
	internal class XslTransformProcessor
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x00021924 File Offset: 0x0001FB24
		public XslTransformProcessor(CompiledStylesheet style, object debugger)
		{
			this.XPathContext = new XsltCompiledContext(this);
			this.compiledStyle = style;
			this.style = style.Style;
			if (debugger != null)
			{
				this.debugger = new XsltDebuggerWrapper(debugger);
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000219C4 File Offset: 0x0001FBC4
		public void Process(XPathNavigator root, Outputter outputtter, XsltArgumentList args, XmlResolver resolver)
		{
			this.args = args;
			this.root = root;
			this.resolver = ((resolver == null) ? new XmlUrlResolver() : resolver);
			this.currentOutputUri = string.Empty;
			this.PushNodeset(new SelfIterator(root, this.XPathContext));
			this.CurrentNodeset.MoveNext();
			if (args != null)
			{
				foreach (object obj in this.CompiledStyle.Variables.Values)
				{
					XslGlobalVariable xslGlobalVariable = (XslGlobalVariable)obj;
					if (xslGlobalVariable is XslGlobalParam)
					{
						object param = args.GetParam(xslGlobalVariable.Name.Name, xslGlobalVariable.Name.Namespace);
						if (param != null)
						{
							((XslGlobalParam)xslGlobalVariable).Override(this, param);
						}
						xslGlobalVariable.Evaluate(this);
					}
				}
			}
			foreach (object obj2 in this.CompiledStyle.Variables.Values)
			{
				XslGlobalVariable xslGlobalVariable2 = (XslGlobalVariable)obj2;
				if (args == null || !(xslGlobalVariable2 is XslGlobalParam))
				{
					xslGlobalVariable2.Evaluate(this);
				}
			}
			this.PopNodeset();
			this.PushOutput(outputtter);
			this.ApplyTemplates(new SelfIterator(root, this.XPathContext), XmlQualifiedName.Empty, null);
			this.PopOutput();
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00021B80 File Offset: 0x0001FD80
		public XsltDebuggerWrapper Debugger
		{
			get
			{
				return this.debugger;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00021B88 File Offset: 0x0001FD88
		public CompiledStylesheet CompiledStyle
		{
			get
			{
				return this.compiledStyle;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00021B90 File Offset: 0x0001FD90
		public XsltArgumentList Arguments
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00021B98 File Offset: 0x0001FD98
		public XPathNavigator Root
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00021BA0 File Offset: 0x0001FDA0
		public MSXslScriptManager ScriptManager
		{
			get
			{
				return this.compiledStyle.ScriptManager;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00021BB0 File Offset: 0x0001FDB0
		public XmlResolver Resolver
		{
			get
			{
				return this.resolver;
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00021BB8 File Offset: 0x0001FDB8
		public XPathNavigator GetDocument(Uri uri)
		{
			XPathNavigator xpathNavigator;
			if (this.docCache != null)
			{
				xpathNavigator = (this.docCache[uri] as XPathNavigator);
				if (xpathNavigator != null)
				{
					return xpathNavigator.Clone();
				}
			}
			else
			{
				this.docCache = new Hashtable();
			}
			XmlReader xmlReader = null;
			try
			{
				xmlReader = new XmlTextReader(uri.ToString(), (Stream)this.resolver.GetEntity(uri, null, null), this.root.NameTable);
				xpathNavigator = new XPathDocument(new XmlValidatingReader(xmlReader)
				{
					ValidationType = ValidationType.None
				}, XmlSpace.Preserve).CreateNavigator();
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
			this.docCache[uri] = xpathNavigator.Clone();
			return xpathNavigator;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00021C88 File Offset: 0x0001FE88
		public Outputter Out
		{
			get
			{
				return (Outputter)this.outputStack.Peek();
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00021C9C File Offset: 0x0001FE9C
		public void PushOutput(Outputter newOutput)
		{
			this.outputStack.Push(newOutput);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00021CAC File Offset: 0x0001FEAC
		public Outputter PopOutput()
		{
			Outputter outputter = (Outputter)this.outputStack.Pop();
			outputter.Done();
			return outputter;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00021CD4 File Offset: 0x0001FED4
		public Hashtable Outputs
		{
			get
			{
				return this.compiledStyle.Outputs;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00021CE4 File Offset: 0x0001FEE4
		public XslOutput Output
		{
			get
			{
				return this.Outputs[this.currentOutputUri] as XslOutput;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00021CFC File Offset: 0x0001FEFC
		public string CurrentOutputUri
		{
			get
			{
				return this.currentOutputUri;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00021D04 File Offset: 0x0001FF04
		public bool InsideCDataElement
		{
			get
			{
				return this.XPathContext.IsCData;
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00021D14 File Offset: 0x0001FF14
		public StringBuilder GetAvtStringBuilder()
		{
			if (this.avtSB == null)
			{
				this.avtSB = new StringBuilder();
			}
			return this.avtSB;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00021D34 File Offset: 0x0001FF34
		public string ReleaseAvtStringBuilder()
		{
			string result = this.avtSB.ToString();
			this.avtSB.Length = 0;
			return result;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00021D5C File Offset: 0x0001FF5C
		private Hashtable GetParams(ArrayList withParams)
		{
			if (withParams == null)
			{
				return null;
			}
			Hashtable hashtable;
			if (this.paramPassingCache.Count != 0)
			{
				hashtable = (Hashtable)this.paramPassingCache.Pop();
				hashtable.Clear();
			}
			else
			{
				hashtable = new Hashtable();
			}
			int count = withParams.Count;
			for (int i = 0; i < count; i++)
			{
				XslVariableInformation xslVariableInformation = (XslVariableInformation)withParams[i];
				hashtable.Add(xslVariableInformation.Name, xslVariableInformation.Evaluate(this));
			}
			return hashtable;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00021DE0 File Offset: 0x0001FFE0
		public void ApplyTemplates(XPathNodeIterator nodes, XmlQualifiedName mode, ArrayList withParams)
		{
			Hashtable @params = this.GetParams(withParams);
			while (this.NodesetMoveNext(nodes))
			{
				this.PushNodeset(nodes);
				XslTemplate xslTemplate = this.FindTemplate(this.CurrentNode, mode);
				this.currentTemplateStack.Push(xslTemplate);
				xslTemplate.Evaluate(this, @params);
				this.currentTemplateStack.Pop();
				this.PopNodeset();
			}
			if (@params != null)
			{
				this.paramPassingCache.Push(@params);
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00021E54 File Offset: 0x00020054
		public void CallTemplate(XmlQualifiedName name, ArrayList withParams)
		{
			Hashtable @params = this.GetParams(withParams);
			XslTemplate xslTemplate = this.FindTemplate(name);
			this.currentTemplateStack.Push(null);
			xslTemplate.Evaluate(this, @params);
			this.currentTemplateStack.Pop();
			if (@params != null)
			{
				this.paramPassingCache.Push(@params);
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00021EA4 File Offset: 0x000200A4
		public void ApplyImports()
		{
			XslTemplate xslTemplate = (XslTemplate)this.currentTemplateStack.Peek();
			if (xslTemplate == null)
			{
				throw new XsltException("Invalid context for apply-imports", null, this.CurrentNode);
			}
			XslTemplate xslTemplate2;
			for (int i = xslTemplate.Parent.Imports.Count - 1; i >= 0; i--)
			{
				XslStylesheet xslStylesheet = (XslStylesheet)xslTemplate.Parent.Imports[i];
				xslTemplate2 = xslStylesheet.Templates.FindMatch(this.CurrentNode, xslTemplate.Mode, this);
				if (xslTemplate2 != null)
				{
					this.currentTemplateStack.Push(xslTemplate2);
					xslTemplate2.Evaluate(this);
					this.currentTemplateStack.Pop();
					return;
				}
			}
			switch (this.CurrentNode.NodeType)
			{
			case XPathNodeType.Root:
			case XPathNodeType.Element:
				if (xslTemplate.Mode == XmlQualifiedName.Empty)
				{
					xslTemplate2 = XslDefaultNodeTemplate.Instance;
				}
				else
				{
					xslTemplate2 = new XslDefaultNodeTemplate(xslTemplate.Mode);
				}
				goto IL_131;
			case XPathNodeType.Attribute:
			case XPathNodeType.Text:
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
				xslTemplate2 = XslDefaultTextTemplate.Instance;
				goto IL_131;
			case XPathNodeType.ProcessingInstruction:
			case XPathNodeType.Comment:
				xslTemplate2 = XslEmptyTemplate.Instance;
				goto IL_131;
			}
			xslTemplate2 = XslEmptyTemplate.Instance;
			IL_131:
			this.currentTemplateStack.Push(xslTemplate2);
			xslTemplate2.Evaluate(this);
			this.currentTemplateStack.Pop();
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00022004 File Offset: 0x00020204
		internal void OutputLiteralNamespaceUriNodes(Hashtable nsDecls, ArrayList excludedPrefixes, string localPrefixInCopy)
		{
			if (nsDecls == null)
			{
				return;
			}
			foreach (object obj in nsDecls)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				string text2 = (string)dictionaryEntry.Value;
				if (!(localPrefixInCopy == text))
				{
					if (localPrefixInCopy == null || text.Length != 0 || this.XPathContext.ElementNamespace.Length != 0)
					{
						bool flag = false;
						if (this.style.ExcludeResultPrefixes != null)
						{
							foreach (XmlQualifiedName xmlQualifiedName in this.style.ExcludeResultPrefixes)
							{
								if (xmlQualifiedName.Namespace == text2)
								{
									flag = true;
								}
							}
						}
						if (!flag)
						{
							if (this.style.NamespaceAliases[text] == null)
							{
								string text3 = text2;
								switch (text3)
								{
								case "http://www.w3.org/1999/XSL/Transform":
									continue;
								case "http://www.w3.org/XML/1998/namespace":
									if ("xml" == text)
									{
										continue;
									}
									break;
								case "http://www.w3.org/2000/xmlns/":
									if ("xmlns" == text)
									{
										continue;
									}
									break;
								}
								if (excludedPrefixes == null || !excludedPrefixes.Contains(text))
								{
									this.Out.WriteNamespaceDecl(text, text2);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0002221C File Offset: 0x0002041C
		private XslTemplate FindTemplate(XPathNavigator node, XmlQualifiedName mode)
		{
			XslTemplate xslTemplate = this.style.Templates.FindMatch(this.CurrentNode, mode, this);
			if (xslTemplate != null)
			{
				return xslTemplate;
			}
			switch (node.NodeType)
			{
			case XPathNodeType.Root:
			case XPathNodeType.Element:
				if (mode == XmlQualifiedName.Empty)
				{
					return XslDefaultNodeTemplate.Instance;
				}
				return new XslDefaultNodeTemplate(mode);
			case XPathNodeType.Attribute:
			case XPathNodeType.Text:
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
				return XslDefaultTextTemplate.Instance;
			case XPathNodeType.ProcessingInstruction:
			case XPathNodeType.Comment:
				return XslEmptyTemplate.Instance;
			}
			return XslEmptyTemplate.Instance;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000222B0 File Offset: 0x000204B0
		private XslTemplate FindTemplate(XmlQualifiedName name)
		{
			XslTemplate xslTemplate = this.style.Templates.FindTemplate(name);
			if (xslTemplate != null)
			{
				return xslTemplate;
			}
			throw new XsltException("Could not resolve named template " + name, null, this.CurrentNode);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000222F0 File Offset: 0x000204F0
		public void PushForEachContext()
		{
			this.currentTemplateStack.Push(null);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00022300 File Offset: 0x00020500
		public void PopForEachContext()
		{
			this.currentTemplateStack.Pop();
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00022310 File Offset: 0x00020510
		public XPathNodeIterator CurrentNodeset
		{
			get
			{
				return (XPathNodeIterator)this.nodesetStack[this.nodesetStack.Count - 1];
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00022330 File Offset: 0x00020530
		public XPathNavigator CurrentNode
		{
			get
			{
				XPathNavigator xpathNavigator = this.CurrentNodeset.Current;
				if (xpathNavigator != null)
				{
					return xpathNavigator;
				}
				for (int i = this.nodesetStack.Count - 2; i >= 0; i--)
				{
					xpathNavigator = ((XPathNodeIterator)this.nodesetStack[i]).Current;
					if (xpathNavigator != null)
					{
						return xpathNavigator;
					}
				}
				return null;
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00022390 File Offset: 0x00020590
		public bool NodesetMoveNext()
		{
			return this.NodesetMoveNext(this.CurrentNodeset);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000223A0 File Offset: 0x000205A0
		public bool NodesetMoveNext(XPathNodeIterator iter)
		{
			return iter.MoveNext() && (iter.Current.NodeType != XPathNodeType.Whitespace || this.XPathContext.PreserveWhitespace(iter.Current) || this.NodesetMoveNext(iter));
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000223EC File Offset: 0x000205EC
		public void PushNodeset(XPathNodeIterator itr)
		{
			BaseIterator baseIterator = itr as BaseIterator;
			baseIterator = ((baseIterator == null) ? new WrapperIterator(itr, null) : baseIterator);
			baseIterator.NamespaceManager = this.XPathContext;
			this.nodesetStack.Add(baseIterator);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00022430 File Offset: 0x00020630
		public void PopNodeset()
		{
			this.nodesetStack.RemoveAt(this.nodesetStack.Count - 1);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0002244C File Offset: 0x0002064C
		public bool Matches(Pattern p, XPathNavigator n)
		{
			return p.Matches(n, this.XPathContext);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0002245C File Offset: 0x0002065C
		public object Evaluate(XPathExpression expr)
		{
			XPathNodeIterator currentNodeset = this.CurrentNodeset;
			BaseIterator baseIterator = (BaseIterator)currentNodeset;
			CompiledExpression compiledExpression = (CompiledExpression)expr;
			if (baseIterator.NamespaceManager == null)
			{
				baseIterator.NamespaceManager = compiledExpression.NamespaceManager;
			}
			return compiledExpression.Evaluate(baseIterator);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0002249C File Offset: 0x0002069C
		public string EvaluateString(XPathExpression expr)
		{
			XPathNodeIterator currentNodeset = this.CurrentNodeset;
			return currentNodeset.Current.EvaluateString(expr, currentNodeset, this.XPathContext);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000224C4 File Offset: 0x000206C4
		public bool EvaluateBoolean(XPathExpression expr)
		{
			XPathNodeIterator currentNodeset = this.CurrentNodeset;
			return currentNodeset.Current.EvaluateBoolean(expr, currentNodeset, this.XPathContext);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000224EC File Offset: 0x000206EC
		public double EvaluateNumber(XPathExpression expr)
		{
			XPathNodeIterator currentNodeset = this.CurrentNodeset;
			return currentNodeset.Current.EvaluateNumber(expr, currentNodeset, this.XPathContext);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00022514 File Offset: 0x00020714
		public XPathNodeIterator Select(XPathExpression expr)
		{
			return this.CurrentNodeset.Current.Select(expr, this.XPathContext);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00022530 File Offset: 0x00020730
		public XslAttributeSet ResolveAttributeSet(XmlQualifiedName name)
		{
			return this.CompiledStyle.ResolveAttributeSet(name);
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00022540 File Offset: 0x00020740
		public int StackItemCount
		{
			get
			{
				if (this.currentStack == null)
				{
					return 0;
				}
				for (int i = 0; i < this.currentStack.Length; i++)
				{
					if (this.currentStack[i] == null)
					{
						return i;
					}
				}
				return this.currentStack.Length;
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0002258C File Offset: 0x0002078C
		public object GetStackItem(int slot)
		{
			return this.currentStack[slot];
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00022598 File Offset: 0x00020798
		public void SetStackItem(int slot, object o)
		{
			this.currentStack[slot] = o;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000225A4 File Offset: 0x000207A4
		public void PushStack(int stackSize)
		{
			this.variableStack.Push(this.currentStack);
			this.currentStack = new object[stackSize];
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000225C4 File Offset: 0x000207C4
		public void PopStack()
		{
			this.currentStack = (object[])this.variableStack.Pop();
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000225DC File Offset: 0x000207DC
		public void SetBusy(object o)
		{
			this.busyTable[o] = XslTransformProcessor.busyObject;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000225F0 File Offset: 0x000207F0
		public void SetFree(object o)
		{
			this.busyTable.Remove(o);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00022600 File Offset: 0x00020800
		public bool IsBusy(object o)
		{
			return this.busyTable[o] == XslTransformProcessor.busyObject;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00022618 File Offset: 0x00020818
		public bool PushElementState(string prefix, string name, string ns, bool preserveWhitespace)
		{
			bool flag = this.IsCData(name, ns);
			this.XPathContext.PushScope();
			Outputter @out = this.Out;
			bool flag2 = flag;
			this.XPathContext.IsCData = flag2;
			@out.InsideCDataSection = flag2;
			this.XPathContext.WhitespaceHandling = true;
			this.XPathContext.ElementPrefix = prefix;
			this.XPathContext.ElementNamespace = ns;
			return flag;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00022678 File Offset: 0x00020878
		private bool IsCData(string name, string ns)
		{
			for (int i = 0; i < this.Output.CDataSectionElements.Length; i++)
			{
				XmlQualifiedName xmlQualifiedName = this.Output.CDataSectionElements[i];
				if (xmlQualifiedName.Name == name && xmlQualifiedName.Namespace == ns)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000226D8 File Offset: 0x000208D8
		public void PopCDataState(bool isCData)
		{
			this.XPathContext.PopScope();
			this.Out.InsideCDataSection = this.XPathContext.IsCData;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00022708 File Offset: 0x00020908
		public bool PreserveOutputWhitespace
		{
			get
			{
				return this.XPathContext.Whitespace;
			}
		}

		// Token: 0x04000377 RID: 887
		private XsltDebuggerWrapper debugger;

		// Token: 0x04000378 RID: 888
		private CompiledStylesheet compiledStyle;

		// Token: 0x04000379 RID: 889
		private XslStylesheet style;

		// Token: 0x0400037A RID: 890
		private Stack currentTemplateStack = new Stack();

		// Token: 0x0400037B RID: 891
		private XPathNavigator root;

		// Token: 0x0400037C RID: 892
		private XsltArgumentList args;

		// Token: 0x0400037D RID: 893
		private XmlResolver resolver;

		// Token: 0x0400037E RID: 894
		private string currentOutputUri;

		// Token: 0x0400037F RID: 895
		internal readonly XsltCompiledContext XPathContext;

		// Token: 0x04000380 RID: 896
		internal Hashtable globalVariableTable = new Hashtable();

		// Token: 0x04000381 RID: 897
		private Hashtable docCache;

		// Token: 0x04000382 RID: 898
		private Stack outputStack = new Stack();

		// Token: 0x04000383 RID: 899
		private StringBuilder avtSB;

		// Token: 0x04000384 RID: 900
		private Stack paramPassingCache = new Stack();

		// Token: 0x04000385 RID: 901
		private ArrayList nodesetStack = new ArrayList();

		// Token: 0x04000386 RID: 902
		private Stack variableStack = new Stack();

		// Token: 0x04000387 RID: 903
		private object[] currentStack;

		// Token: 0x04000388 RID: 904
		private Hashtable busyTable = new Hashtable();

		// Token: 0x04000389 RID: 905
		private static object busyObject = new object();
	}
}
