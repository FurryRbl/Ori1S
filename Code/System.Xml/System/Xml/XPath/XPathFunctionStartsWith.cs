using System;

namespace System.Xml.XPath
{
	// Token: 0x02000150 RID: 336
	internal class XPathFunctionStartsWith : XPathFunction
	{
		// Token: 0x06000F65 RID: 3941 RVA: 0x00049F08 File Offset: 0x00048108
		public XPathFunctionStartsWith(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail == null || args.Tail.Tail != null)
			{
				throw new XPathException("starts-with takes 2 args");
			}
			this.arg0 = args.Arg;
			this.arg1 = args.Tail.Arg;
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x00049F68 File Offset: 0x00048168
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x00049F6C File Offset: 0x0004816C
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer && this.arg1.Peer;
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00049F8C File Offset: 0x0004818C
		public override object Evaluate(BaseIterator iter)
		{
			return this.arg0.EvaluateString(iter).StartsWith(this.arg1.EvaluateString(iter));
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00049FBC File Offset: 0x000481BC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"starts-with(",
				this.arg0.ToString(),
				",",
				this.arg1.ToString(),
				")"
			});
		}

		// Token: 0x040006B7 RID: 1719
		private Expression arg0;

		// Token: 0x040006B8 RID: 1720
		private Expression arg1;
	}
}
