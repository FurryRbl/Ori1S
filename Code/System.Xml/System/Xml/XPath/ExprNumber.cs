using System;

namespace System.Xml.XPath
{
	// Token: 0x0200018A RID: 394
	internal class ExprNumber : Expression
	{
		// Token: 0x0600109F RID: 4255 RVA: 0x0004D994 File Offset: 0x0004BB94
		public ExprNumber(double value)
		{
			this._value = value;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0004D9A4 File Offset: 0x0004BBA4
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x0004D9B4 File Offset: 0x0004BBB4
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x0004D9B8 File Offset: 0x0004BBB8
		internal override bool Peer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x0004D9BC File Offset: 0x0004BBBC
		public override bool HasStaticValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x0004D9C0 File Offset: 0x0004BBC0
		public override double StaticValueAsNumber
		{
			get
			{
				return XPathFunctions.ToNumber(this._value);
			}
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0004D9D4 File Offset: 0x0004BBD4
		public override object Evaluate(BaseIterator iter)
		{
			return this._value;
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0004D9E4 File Offset: 0x0004BBE4
		public override double EvaluateNumber(BaseIterator iter)
		{
			return this._value;
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x0004D9EC File Offset: 0x0004BBEC
		internal override bool IsPositional
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000702 RID: 1794
		protected double _value;
	}
}
