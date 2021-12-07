using System;

namespace System.Xml.XPath
{
	// Token: 0x02000155 RID: 341
	internal class XPathFunctionStringLength : XPathFunction
	{
		// Token: 0x06000F7E RID: 3966 RVA: 0x0004A59C File Offset: 0x0004879C
		public XPathFunctionStringLength(FunctionArguments args) : base(args)
		{
			if (args != null)
			{
				this.arg0 = args.Arg;
				if (args.Tail != null)
				{
					throw new XPathException("string-length takes 1 or zero args");
				}
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0004A5D0 File Offset: 0x000487D0
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0004A5D4 File Offset: 0x000487D4
		internal override bool Peer
		{
			get
			{
				return this.arg0 == null || this.arg0.Peer;
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0004A5F4 File Offset: 0x000487F4
		public override object Evaluate(BaseIterator iter)
		{
			string text;
			if (this.arg0 != null)
			{
				text = this.arg0.EvaluateString(iter);
			}
			else
			{
				text = iter.Current.Value;
			}
			return (double)text.Length;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0004A638 File Offset: 0x00048838
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"string-length(",
				this.arg0.ToString(),
				")"
			});
		}

		// Token: 0x040006C2 RID: 1730
		private Expression arg0;
	}
}
