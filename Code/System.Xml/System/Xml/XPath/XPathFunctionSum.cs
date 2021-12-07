using System;

namespace System.Xml.XPath
{
	// Token: 0x02000160 RID: 352
	internal class XPathFunctionSum : XPathNumericFunction
	{
		// Token: 0x06000FB4 RID: 4020 RVA: 0x0004ADDC File Offset: 0x00048FDC
		public XPathFunctionSum(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail != null)
			{
				throw new XPathException("sum takes one arg");
			}
			this.arg0 = args.Arg;
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x0004AE10 File Offset: 0x00049010
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer;
			}
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0004AE20 File Offset: 0x00049020
		public override object Evaluate(BaseIterator iter)
		{
			XPathNodeIterator xpathNodeIterator = this.arg0.EvaluateNodeSet(iter);
			double num = 0.0;
			while (xpathNodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = xpathNodeIterator.Current;
				num += XPathFunctions.ToNumber(xpathNavigator.Value);
			}
			return num;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0004AE70 File Offset: 0x00049070
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"sum(",
				this.arg0.ToString(),
				")"
			});
		}

		// Token: 0x040006CB RID: 1739
		private Expression arg0;
	}
}
