using System;

namespace System.Xml.XPath
{
	// Token: 0x02000149 RID: 329
	internal class XPathFunctionCount : XPathFunction
	{
		// Token: 0x06000F3F RID: 3903 RVA: 0x0004984C File Offset: 0x00047A4C
		public XPathFunctionCount(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail != null)
			{
				throw new XPathException("count takes 1 arg");
			}
			this.arg0 = args.Arg;
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00049880 File Offset: 0x00047A80
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00049884 File Offset: 0x00047A84
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer;
			}
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00049894 File Offset: 0x00047A94
		public override object Evaluate(BaseIterator iter)
		{
			return (double)this.arg0.EvaluateNodeSet(iter).Count;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x000498B0 File Offset: 0x00047AB0
		public override bool EvaluateBoolean(BaseIterator iter)
		{
			if (this.arg0.GetReturnType(iter) == XPathResultType.NodeSet)
			{
				return this.arg0.EvaluateBoolean(iter);
			}
			return this.arg0.EvaluateNodeSet(iter).MoveNext();
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x000498F0 File Offset: 0x00047AF0
		public override string ToString()
		{
			return "count(" + this.arg0.ToString() + ")";
		}

		// Token: 0x040006AF RID: 1711
		private Expression arg0;
	}
}
