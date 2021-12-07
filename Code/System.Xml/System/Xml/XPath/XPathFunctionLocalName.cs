using System;

namespace System.Xml.XPath
{
	// Token: 0x0200014B RID: 331
	internal class XPathFunctionLocalName : XPathFunction
	{
		// Token: 0x06000F4C RID: 3916 RVA: 0x00049A5C File Offset: 0x00047C5C
		public XPathFunctionLocalName(FunctionArguments args) : base(args)
		{
			if (args != null)
			{
				this.arg0 = args.Arg;
				if (args.Tail != null)
				{
					throw new XPathException("local-name takes 1 or zero args");
				}
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00049A90 File Offset: 0x00047C90
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00049A94 File Offset: 0x00047C94
		internal override bool Peer
		{
			get
			{
				return this.arg0 == null || this.arg0.Peer;
			}
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00049AB4 File Offset: 0x00047CB4
		public override object Evaluate(BaseIterator iter)
		{
			if (this.arg0 == null)
			{
				return iter.Current.LocalName;
			}
			BaseIterator baseIterator = this.arg0.EvaluateNodeSet(iter);
			if (baseIterator == null || !baseIterator.MoveNext())
			{
				return string.Empty;
			}
			return baseIterator.Current.LocalName;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00049B08 File Offset: 0x00047D08
		public override string ToString()
		{
			return "local-name(" + this.arg0.ToString() + ")";
		}

		// Token: 0x040006B2 RID: 1714
		private Expression arg0;
	}
}
