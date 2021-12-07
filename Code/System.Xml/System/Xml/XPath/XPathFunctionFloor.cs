using System;

namespace System.Xml.XPath
{
	// Token: 0x02000161 RID: 353
	internal class XPathFunctionFloor : XPathNumericFunction
	{
		// Token: 0x06000FB8 RID: 4024 RVA: 0x0004AE9C File Offset: 0x0004909C
		public XPathFunctionFloor(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail != null)
			{
				throw new XPathException("floor takes one arg");
			}
			this.arg0 = args.Arg;
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0004AED0 File Offset: 0x000490D0
		public override bool HasStaticValue
		{
			get
			{
				return this.arg0.HasStaticValue;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x0004AEE0 File Offset: 0x000490E0
		public override double StaticValueAsNumber
		{
			get
			{
				return (!this.HasStaticValue) ? 0.0 : Math.Floor(this.arg0.StaticValueAsNumber);
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x0004AF0C File Offset: 0x0004910C
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer;
			}
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0004AF1C File Offset: 0x0004911C
		public override object Evaluate(BaseIterator iter)
		{
			return Math.Floor(this.arg0.EvaluateNumber(iter));
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0004AF34 File Offset: 0x00049134
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"floor(",
				this.arg0.ToString(),
				")"
			});
		}

		// Token: 0x040006CC RID: 1740
		private Expression arg0;
	}
}
