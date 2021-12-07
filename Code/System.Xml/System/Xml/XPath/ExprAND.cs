using System;

namespace System.Xml.XPath
{
	// Token: 0x0200016F RID: 367
	internal class ExprAND : ExprBoolean
	{
		// Token: 0x06001017 RID: 4119 RVA: 0x0004C0A0 File Offset: 0x0004A2A0
		public ExprAND(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0004C0AC File Offset: 0x0004A2AC
		protected override string Operator
		{
			get
			{
				return "and";
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x0004C0B4 File Offset: 0x0004A2B4
		public override bool StaticValueAsBoolean
		{
			get
			{
				return this.HasStaticValue && this._left.StaticValueAsBoolean && this._right.StaticValueAsBoolean;
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0004C0F0 File Offset: 0x0004A2F0
		public override bool EvaluateBoolean(BaseIterator iter)
		{
			return this._left.EvaluateBoolean(iter) && this._right.EvaluateBoolean(iter);
		}
	}
}
