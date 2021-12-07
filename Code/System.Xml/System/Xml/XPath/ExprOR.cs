using System;

namespace System.Xml.XPath
{
	// Token: 0x0200016E RID: 366
	internal class ExprOR : ExprBoolean
	{
		// Token: 0x06001013 RID: 4115 RVA: 0x0004C02C File Offset: 0x0004A22C
		public ExprOR(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0004C038 File Offset: 0x0004A238
		protected override string Operator
		{
			get
			{
				return "or";
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x0004C040 File Offset: 0x0004A240
		public override bool StaticValueAsBoolean
		{
			get
			{
				return this.HasStaticValue && (this._left.StaticValueAsBoolean || this._right.StaticValueAsBoolean);
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0004C07C File Offset: 0x0004A27C
		public override bool EvaluateBoolean(BaseIterator iter)
		{
			return this._left.EvaluateBoolean(iter) || this._right.EvaluateBoolean(iter);
		}
	}
}
