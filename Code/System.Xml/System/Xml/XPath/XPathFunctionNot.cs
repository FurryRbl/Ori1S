using System;

namespace System.Xml.XPath
{
	// Token: 0x0200015A RID: 346
	internal class XPathFunctionNot : XPathBooleanFunction
	{
		// Token: 0x06000F94 RID: 3988 RVA: 0x0004AA80 File Offset: 0x00048C80
		public XPathFunctionNot(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail != null)
			{
				throw new XPathException("not takes one arg");
			}
			this.arg0 = args.Arg;
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0004AAB4 File Offset: 0x00048CB4
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer;
			}
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0004AAC4 File Offset: 0x00048CC4
		public override object Evaluate(BaseIterator iter)
		{
			return !this.arg0.EvaluateBoolean(iter);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0004AADC File Offset: 0x00048CDC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"not(",
				this.arg0.ToString(),
				")"
			});
		}

		// Token: 0x040006C8 RID: 1736
		private Expression arg0;
	}
}
