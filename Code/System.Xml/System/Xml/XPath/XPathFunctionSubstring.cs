using System;

namespace System.Xml.XPath
{
	// Token: 0x02000154 RID: 340
	internal class XPathFunctionSubstring : XPathFunction
	{
		// Token: 0x06000F79 RID: 3961 RVA: 0x0004A340 File Offset: 0x00048540
		public XPathFunctionSubstring(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail == null || (args.Tail.Tail != null && args.Tail.Tail.Tail != null))
			{
				throw new XPathException("substring takes 2 or 3 args");
			}
			this.arg0 = args.Arg;
			this.arg1 = args.Tail.Arg;
			if (args.Tail.Tail != null)
			{
				this.arg2 = args.Tail.Tail.Arg;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0004A3D8 File Offset: 0x000485D8
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0004A3DC File Offset: 0x000485DC
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer && this.arg1.Peer && (this.arg2 == null || this.arg2.Peer);
			}
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0004A428 File Offset: 0x00048628
		public override object Evaluate(BaseIterator iter)
		{
			string text = this.arg0.EvaluateString(iter);
			double num = Math.Round(this.arg1.EvaluateNumber(iter)) - 1.0;
			if (double.IsNaN(num) || double.IsNegativeInfinity(num) || num >= (double)text.Length)
			{
				return string.Empty;
			}
			if (this.arg2 == null)
			{
				if (num < 0.0)
				{
					num = 0.0;
				}
				return text.Substring((int)num);
			}
			double num2 = Math.Round(this.arg2.EvaluateNumber(iter));
			if (double.IsNaN(num2))
			{
				return string.Empty;
			}
			if (num < 0.0 || num2 < 0.0)
			{
				num2 = num + num2;
				if (num2 <= 0.0)
				{
					return string.Empty;
				}
				num = 0.0;
			}
			double num3 = (double)text.Length - num;
			if (num2 > num3)
			{
				num2 = num3;
			}
			return text.Substring((int)num, (int)num2);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0004A538 File Offset: 0x00048738
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"substring(",
				this.arg0.ToString(),
				",",
				this.arg1.ToString(),
				",",
				this.arg2.ToString(),
				")"
			});
		}

		// Token: 0x040006BF RID: 1727
		private Expression arg0;

		// Token: 0x040006C0 RID: 1728
		private Expression arg1;

		// Token: 0x040006C1 RID: 1729
		private Expression arg2;
	}
}
