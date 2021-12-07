using System;

namespace System.Xml.XPath
{
	// Token: 0x0200014D RID: 333
	internal class XPathFunctionName : XPathFunction
	{
		// Token: 0x06000F56 RID: 3926 RVA: 0x00049BEC File Offset: 0x00047DEC
		public XPathFunctionName(FunctionArguments args) : base(args)
		{
			if (args != null)
			{
				this.arg0 = args.Arg;
				if (args.Tail != null)
				{
					throw new XPathException("name takes 1 or zero args");
				}
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00049C20 File Offset: 0x00047E20
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00049C24 File Offset: 0x00047E24
		internal override bool Peer
		{
			get
			{
				return this.arg0 == null || this.arg0.Peer;
			}
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00049C44 File Offset: 0x00047E44
		public override object Evaluate(BaseIterator iter)
		{
			if (this.arg0 == null)
			{
				return iter.Current.Name;
			}
			BaseIterator baseIterator = this.arg0.EvaluateNodeSet(iter);
			if (baseIterator == null || !baseIterator.MoveNext())
			{
				return string.Empty;
			}
			return baseIterator.Current.Name;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00049C98 File Offset: 0x00047E98
		public override string ToString()
		{
			return "name(" + ((this.arg0 == null) ? string.Empty : this.arg0.ToString()) + ")";
		}

		// Token: 0x040006B4 RID: 1716
		private Expression arg0;
	}
}
