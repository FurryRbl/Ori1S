using System;

namespace System.Xml.XPath
{
	// Token: 0x02000159 RID: 345
	internal class XPathFunctionBoolean : XPathBooleanFunction
	{
		// Token: 0x06000F90 RID: 3984 RVA: 0x0004A9C0 File Offset: 0x00048BC0
		public XPathFunctionBoolean(FunctionArguments args) : base(args)
		{
			if (args != null)
			{
				this.arg0 = args.Arg;
				if (args.Tail != null)
				{
					throw new XPathException("boolean takes 1 or zero args");
				}
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0004A9F4 File Offset: 0x00048BF4
		internal override bool Peer
		{
			get
			{
				return this.arg0 == null || this.arg0.Peer;
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0004AA14 File Offset: 0x00048C14
		public override object Evaluate(BaseIterator iter)
		{
			if (this.arg0 == null)
			{
				return XPathFunctions.ToBoolean(iter.Current.Value);
			}
			return this.arg0.EvaluateBoolean(iter);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0004AA54 File Offset: 0x00048C54
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"boolean(",
				this.arg0.ToString(),
				")"
			});
		}

		// Token: 0x040006C7 RID: 1735
		private Expression arg0;
	}
}
