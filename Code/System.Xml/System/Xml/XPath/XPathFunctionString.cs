using System;

namespace System.Xml.XPath
{
	// Token: 0x0200014E RID: 334
	internal class XPathFunctionString : XPathFunction
	{
		// Token: 0x06000F5B RID: 3931 RVA: 0x00049CCC File Offset: 0x00047ECC
		public XPathFunctionString(FunctionArguments args) : base(args)
		{
			if (args != null)
			{
				this.arg0 = args.Arg;
				if (args.Tail != null)
				{
					throw new XPathException("string takes 1 or zero args");
				}
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00049D00 File Offset: 0x00047F00
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x00049D04 File Offset: 0x00047F04
		internal override bool Peer
		{
			get
			{
				return this.arg0 == null || this.arg0.Peer;
			}
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00049D24 File Offset: 0x00047F24
		public override object Evaluate(BaseIterator iter)
		{
			if (this.arg0 == null)
			{
				return iter.Current.Value;
			}
			return this.arg0.EvaluateString(iter);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00049D4C File Offset: 0x00047F4C
		public override string ToString()
		{
			return "string(" + this.arg0.ToString() + ")";
		}

		// Token: 0x040006B5 RID: 1717
		private Expression arg0;
	}
}
