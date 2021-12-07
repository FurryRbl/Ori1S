using System;

namespace System.Xml.XPath
{
	// Token: 0x0200018C RID: 396
	internal class ExprLiteral : Expression
	{
		// Token: 0x060010B0 RID: 4272 RVA: 0x0004DA48 File Offset: 0x0004BC48
		public ExprLiteral(string value)
		{
			this._value = value;
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x0004DA58 File Offset: 0x0004BC58
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0004DA60 File Offset: 0x0004BC60
		public override string ToString()
		{
			return "'" + this._value + "'";
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0004DA78 File Offset: 0x0004BC78
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x0004DA7C File Offset: 0x0004BC7C
		internal override bool Peer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x0004DA80 File Offset: 0x0004BC80
		public override bool HasStaticValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x0004DA84 File Offset: 0x0004BC84
		public override string StaticValueAsString
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0004DA8C File Offset: 0x0004BC8C
		public override object Evaluate(BaseIterator iter)
		{
			return this._value;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0004DA94 File Offset: 0x0004BC94
		public override string EvaluateString(BaseIterator iter)
		{
			return this._value;
		}

		// Token: 0x04000704 RID: 1796
		protected string _value;
	}
}
