using System;

namespace System.Xml.XPath
{
	// Token: 0x02000151 RID: 337
	internal class XPathFunctionContains : XPathFunction
	{
		// Token: 0x06000F6A RID: 3946 RVA: 0x0004A008 File Offset: 0x00048208
		public XPathFunctionContains(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail == null || args.Tail.Tail != null)
			{
				throw new XPathException("contains takes 2 args");
			}
			this.arg0 = args.Arg;
			this.arg1 = args.Tail.Arg;
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0004A068 File Offset: 0x00048268
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0004A06C File Offset: 0x0004826C
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer && this.arg1.Peer;
			}
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0004A08C File Offset: 0x0004828C
		public override object Evaluate(BaseIterator iter)
		{
			return this.arg0.EvaluateString(iter).IndexOf(this.arg1.EvaluateString(iter)) != -1;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0004A0C4 File Offset: 0x000482C4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"contains(",
				this.arg0.ToString(),
				",",
				this.arg1.ToString(),
				")"
			});
		}

		// Token: 0x040006B9 RID: 1721
		private Expression arg0;

		// Token: 0x040006BA RID: 1722
		private Expression arg1;
	}
}
