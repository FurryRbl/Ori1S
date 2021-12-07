using System;

namespace System.Xml.XPath
{
	// Token: 0x0200016C RID: 364
	internal abstract class ExprBinary : Expression
	{
		// Token: 0x06001005 RID: 4101 RVA: 0x0004BE78 File Offset: 0x0004A078
		public ExprBinary(Expression left, Expression right)
		{
			this._left = left;
			this._right = right;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0004BE90 File Offset: 0x0004A090
		public override Expression Optimize()
		{
			this._left = this._left.Optimize();
			this._right = this._right.Optimize();
			return this;
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x0004BEB8 File Offset: 0x0004A0B8
		public override bool HasStaticValue
		{
			get
			{
				return this._left.HasStaticValue && this._right.HasStaticValue;
			}
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0004BED8 File Offset: 0x0004A0D8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this._left.ToString(),
				' ',
				this.Operator,
				' ',
				this._right.ToString()
			});
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001009 RID: 4105
		protected abstract string Operator { get; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x0004BF2C File Offset: 0x0004A12C
		internal override XPathNodeType EvaluatedNodeType
		{
			get
			{
				if (this._left.EvaluatedNodeType == this._right.EvaluatedNodeType)
				{
					return this._left.EvaluatedNodeType;
				}
				return XPathNodeType.All;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x0004BF58 File Offset: 0x0004A158
		internal override bool IsPositional
		{
			get
			{
				return this._left.IsPositional || this._right.IsPositional;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0004BF78 File Offset: 0x0004A178
		internal override bool Peer
		{
			get
			{
				return this._left.Peer && this._right.Peer;
			}
		}

		// Token: 0x040006E1 RID: 1761
		protected Expression _left;

		// Token: 0x040006E2 RID: 1762
		protected Expression _right;
	}
}
