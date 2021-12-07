using System;

namespace System.Xml.XPath
{
	// Token: 0x02000152 RID: 338
	internal class XPathFunctionSubstringBefore : XPathFunction
	{
		// Token: 0x06000F6F RID: 3951 RVA: 0x0004A110 File Offset: 0x00048310
		public XPathFunctionSubstringBefore(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail == null || args.Tail.Tail != null)
			{
				throw new XPathException("substring-before takes 2 args");
			}
			this.arg0 = args.Arg;
			this.arg1 = args.Tail.Arg;
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0004A170 File Offset: 0x00048370
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0004A174 File Offset: 0x00048374
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer && this.arg1.Peer;
			}
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0004A194 File Offset: 0x00048394
		public override object Evaluate(BaseIterator iter)
		{
			string text = this.arg0.EvaluateString(iter);
			string value = this.arg1.EvaluateString(iter);
			int num = text.IndexOf(value);
			if (num <= 0)
			{
				return string.Empty;
			}
			return text.Substring(0, num);
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0004A1D8 File Offset: 0x000483D8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"substring-before(",
				this.arg0.ToString(),
				",",
				this.arg1.ToString(),
				")"
			});
		}

		// Token: 0x040006BB RID: 1723
		private Expression arg0;

		// Token: 0x040006BC RID: 1724
		private Expression arg1;
	}
}
