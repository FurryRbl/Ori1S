using System;
using System.Collections;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000A6 RID: 166
	internal class XsltDocument : XPathFunction
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x000232AC File Offset: 0x000214AC
		public XsltDocument(FunctionArguments args, Compiler c) : base(args)
		{
			if (args == null || (args.Tail != null && args.Tail.Tail != null))
			{
				throw new XPathException("document takes one or two args");
			}
			this.arg0 = args.Arg;
			if (args.Tail != null)
			{
				this.arg1 = args.Tail.Arg;
			}
			this.doc = c.Input.Clone();
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00023334 File Offset: 0x00021534
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00023338 File Offset: 0x00021538
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer && (this.arg1 == null || this.arg1.Peer);
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0002336C File Offset: 0x0002156C
		public override object Evaluate(BaseIterator iter)
		{
			string baseUri = null;
			if (this.arg1 != null)
			{
				XPathNodeIterator xpathNodeIterator = this.arg1.EvaluateNodeSet(iter);
				if (xpathNodeIterator.MoveNext())
				{
					baseUri = xpathNodeIterator.Current.BaseURI;
				}
				else
				{
					baseUri = XsltDocument.VoidBaseUriFlag;
				}
			}
			object obj = this.arg0.Evaluate(iter);
			if (obj is XPathNodeIterator)
			{
				return this.GetDocument(iter.NamespaceManager as XsltCompiledContext, (XPathNodeIterator)obj, baseUri);
			}
			return this.GetDocument(iter.NamespaceManager as XsltCompiledContext, (!(obj is IFormattable)) ? obj.ToString() : ((IFormattable)obj).ToString(null, CultureInfo.InvariantCulture), baseUri);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00023420 File Offset: 0x00021620
		private Uri Resolve(string thisUri, string baseUri, XslTransformProcessor p)
		{
			XmlResolver resolver = p.Resolver;
			if (resolver == null)
			{
				return null;
			}
			Uri baseUri2 = null;
			if (!object.ReferenceEquals(baseUri, XsltDocument.VoidBaseUriFlag) && baseUri != string.Empty)
			{
				baseUri2 = resolver.ResolveUri(null, baseUri);
			}
			return resolver.ResolveUri(baseUri2, thisUri);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00023470 File Offset: 0x00021670
		private XPathNodeIterator GetDocument(XsltCompiledContext xsltContext, XPathNodeIterator itr, string baseUri)
		{
			ArrayList arrayList = new ArrayList();
			try
			{
				Hashtable hashtable = new Hashtable();
				while (itr.MoveNext())
				{
					XPathNavigator xpathNavigator = itr.Current;
					Uri uri = this.Resolve(xpathNavigator.Value, (baseUri == null) ? this.doc.BaseURI : baseUri, xsltContext.Processor);
					if (!hashtable.ContainsKey(uri))
					{
						hashtable.Add(uri, null);
						if (uri != null && uri.ToString() == string.Empty)
						{
							XPathNavigator xpathNavigator2 = this.doc.Clone();
							xpathNavigator2.MoveToRoot();
							arrayList.Add(xpathNavigator2);
						}
						else
						{
							arrayList.Add(xsltContext.Processor.GetDocument(uri));
						}
					}
				}
			}
			catch (Exception)
			{
				arrayList.Clear();
			}
			return new ListIterator(arrayList, xsltContext);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00023564 File Offset: 0x00021764
		private XPathNodeIterator GetDocument(XsltCompiledContext xsltContext, string arg0, string baseUri)
		{
			XPathNodeIterator result;
			try
			{
				Uri uri = this.Resolve(arg0, (baseUri == null) ? this.doc.BaseURI : baseUri, xsltContext.Processor);
				XPathNavigator xpathNavigator;
				if (uri != null && uri.ToString() == string.Empty)
				{
					xpathNavigator = this.doc.Clone();
					xpathNavigator.MoveToRoot();
				}
				else
				{
					xpathNavigator = xsltContext.Processor.GetDocument(uri);
				}
				result = new SelfIterator(xpathNavigator, xsltContext);
			}
			catch (Exception)
			{
				result = new ListIterator(new ArrayList(), xsltContext);
			}
			return result;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00023624 File Offset: 0x00021824
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"document(",
				this.arg0.ToString(),
				(this.arg1 == null) ? string.Empty : ",",
				(this.arg1 == null) ? string.Empty : this.arg1.ToString(),
				")"
			});
		}

		// Token: 0x040003A1 RID: 929
		private Expression arg0;

		// Token: 0x040003A2 RID: 930
		private Expression arg1;

		// Token: 0x040003A3 RID: 931
		private XPathNavigator doc;

		// Token: 0x040003A4 RID: 932
		private static string VoidBaseUriFlag = "&^)(*&%*^$&$VOID!BASE!URI!";
	}
}
