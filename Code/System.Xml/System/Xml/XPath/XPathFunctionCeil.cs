using System;

namespace System.Xml.XPath
{
	// Token: 0x02000162 RID: 354
	internal class XPathFunctionCeil : XPathNumericFunction
	{
		// Token: 0x06000FBE RID: 4030 RVA: 0x0004AF60 File Offset: 0x00049160
		public XPathFunctionCeil(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail != null)
			{
				throw new XPathException("ceil takes one arg");
			}
			this.arg0 = args.Arg;
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0004AF94 File Offset: 0x00049194
		public override bool HasStaticValue
		{
			get
			{
				return this.arg0.HasStaticValue;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0004AFA4 File Offset: 0x000491A4
		public override double StaticValueAsNumber
		{
			get
			{
				return (!this.HasStaticValue) ? 0.0 : Math.Ceiling(this.arg0.StaticValueAsNumber);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x0004AFD0 File Offset: 0x000491D0
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer;
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0004AFE0 File Offset: 0x000491E0
		public override object Evaluate(BaseIterator iter)
		{
			return Math.Ceiling(this.arg0.EvaluateNumber(iter));
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0004AFF8 File Offset: 0x000491F8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"ceil(",
				this.arg0.ToString(),
				")"
			});
		}

		// Token: 0x040006CD RID: 1741
		private Expression arg0;
	}
}
