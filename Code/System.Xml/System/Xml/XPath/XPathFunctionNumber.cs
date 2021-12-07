using System;

namespace System.Xml.XPath
{
	// Token: 0x0200015F RID: 351
	internal class XPathFunctionNumber : XPathNumericFunction
	{
		// Token: 0x06000FAD RID: 4013 RVA: 0x0004AC80 File Offset: 0x00048E80
		public XPathFunctionNumber(FunctionArguments args) : base(args)
		{
			if (args != null)
			{
				this.arg0 = args.Arg;
				if (args.Tail != null)
				{
					throw new XPathException("number takes 1 or zero args");
				}
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0004ACB4 File Offset: 0x00048EB4
		public override Expression Optimize()
		{
			if (this.arg0 == null)
			{
				return this;
			}
			this.arg0 = this.arg0.Optimize();
			return this.arg0.HasStaticValue ? new ExprNumber(this.StaticValueAsNumber) : this;
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0004AD00 File Offset: 0x00048F00
		public override bool HasStaticValue
		{
			get
			{
				return this.arg0 != null && this.arg0.HasStaticValue;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0004AD1C File Offset: 0x00048F1C
		public override double StaticValueAsNumber
		{
			get
			{
				return (this.arg0 == null) ? 0.0 : this.arg0.StaticValueAsNumber;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0004AD50 File Offset: 0x00048F50
		internal override bool Peer
		{
			get
			{
				return this.arg0 == null || this.arg0.Peer;
			}
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0004AD70 File Offset: 0x00048F70
		public override object Evaluate(BaseIterator iter)
		{
			if (this.arg0 == null)
			{
				return XPathFunctions.ToNumber(iter.Current.Value);
			}
			return this.arg0.EvaluateNumber(iter);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0004ADB0 File Offset: 0x00048FB0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"number(",
				this.arg0.ToString(),
				")"
			});
		}

		// Token: 0x040006CA RID: 1738
		private Expression arg0;
	}
}
