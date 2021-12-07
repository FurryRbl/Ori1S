using System;

namespace System.Xml.XPath
{
	// Token: 0x02000153 RID: 339
	internal class XPathFunctionSubstringAfter : XPathFunction
	{
		// Token: 0x06000F74 RID: 3956 RVA: 0x0004A224 File Offset: 0x00048424
		public XPathFunctionSubstringAfter(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail == null || args.Tail.Tail != null)
			{
				throw new XPathException("substring-after takes 2 args");
			}
			this.arg0 = args.Arg;
			this.arg1 = args.Tail.Arg;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0004A284 File Offset: 0x00048484
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0004A288 File Offset: 0x00048488
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer && this.arg1.Peer;
			}
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0004A2A8 File Offset: 0x000484A8
		public override object Evaluate(BaseIterator iter)
		{
			string text = this.arg0.EvaluateString(iter);
			string text2 = this.arg1.EvaluateString(iter);
			int num = text.IndexOf(text2);
			if (num < 0)
			{
				return string.Empty;
			}
			return text.Substring(num + text2.Length);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0004A2F4 File Offset: 0x000484F4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"substring-after(",
				this.arg0.ToString(),
				",",
				this.arg1.ToString(),
				")"
			});
		}

		// Token: 0x040006BD RID: 1725
		private Expression arg0;

		// Token: 0x040006BE RID: 1726
		private Expression arg1;
	}
}
