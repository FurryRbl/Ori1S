using System;

namespace System.Xml.XPath
{
	// Token: 0x02000163 RID: 355
	internal class XPathFunctionRound : XPathNumericFunction
	{
		// Token: 0x06000FC4 RID: 4036 RVA: 0x0004B024 File Offset: 0x00049224
		public XPathFunctionRound(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail != null)
			{
				throw new XPathException("round takes one arg");
			}
			this.arg0 = args.Arg;
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0004B058 File Offset: 0x00049258
		public override bool HasStaticValue
		{
			get
			{
				return this.arg0.HasStaticValue;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x0004B068 File Offset: 0x00049268
		public override double StaticValueAsNumber
		{
			get
			{
				return (!this.HasStaticValue) ? 0.0 : this.Round(this.arg0.StaticValueAsNumber);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0004B0A0 File Offset: 0x000492A0
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer;
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0004B0B0 File Offset: 0x000492B0
		public override object Evaluate(BaseIterator iter)
		{
			return this.Round(this.arg0.EvaluateNumber(iter));
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0004B0CC File Offset: 0x000492CC
		private double Round(double arg)
		{
			if (arg < -0.5 || arg > 0.0)
			{
				return Math.Floor(arg + 0.5);
			}
			return Math.Round(arg);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0004B104 File Offset: 0x00049304
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"round(",
				this.arg0.ToString(),
				")"
			});
		}

		// Token: 0x040006CE RID: 1742
		private Expression arg0;
	}
}
