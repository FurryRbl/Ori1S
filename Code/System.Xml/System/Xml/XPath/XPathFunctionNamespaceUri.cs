using System;

namespace System.Xml.XPath
{
	// Token: 0x0200014C RID: 332
	internal class XPathFunctionNamespaceUri : XPathFunction
	{
		// Token: 0x06000F51 RID: 3921 RVA: 0x00049B24 File Offset: 0x00047D24
		public XPathFunctionNamespaceUri(FunctionArguments args) : base(args)
		{
			if (args != null)
			{
				this.arg0 = args.Arg;
				if (args.Tail != null)
				{
					throw new XPathException("namespace-uri takes 1 or zero args");
				}
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x00049B58 File Offset: 0x00047D58
		internal override bool Peer
		{
			get
			{
				return this.arg0 == null || this.arg0.Peer;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x00049B78 File Offset: 0x00047D78
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00049B7C File Offset: 0x00047D7C
		public override object Evaluate(BaseIterator iter)
		{
			if (this.arg0 == null)
			{
				return iter.Current.NamespaceURI;
			}
			BaseIterator baseIterator = this.arg0.EvaluateNodeSet(iter);
			if (baseIterator == null || !baseIterator.MoveNext())
			{
				return string.Empty;
			}
			return baseIterator.Current.NamespaceURI;
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00049BD0 File Offset: 0x00047DD0
		public override string ToString()
		{
			return "namespace-uri(" + this.arg0.ToString() + ")";
		}

		// Token: 0x040006B3 RID: 1715
		private Expression arg0;
	}
}
