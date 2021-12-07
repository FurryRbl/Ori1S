using System;

namespace System.Xml.XPath
{
	// Token: 0x0200018B RID: 395
	internal class BooleanConstant : Expression
	{
		// Token: 0x060010A8 RID: 4264 RVA: 0x0004D9F0 File Offset: 0x0004BBF0
		public BooleanConstant(bool value)
		{
			this._value = value;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0004DA00 File Offset: 0x0004BC00
		public override string ToString()
		{
			return (!this._value) ? "false()" : "true()";
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x0004DA1C File Offset: 0x0004BC1C
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x0004DA20 File Offset: 0x0004BC20
		internal override bool Peer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x0004DA24 File Offset: 0x0004BC24
		public override bool HasStaticValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x0004DA28 File Offset: 0x0004BC28
		public override bool StaticValueAsBoolean
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0004DA30 File Offset: 0x0004BC30
		public override object Evaluate(BaseIterator iter)
		{
			return this._value;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0004DA40 File Offset: 0x0004BC40
		public override bool EvaluateBoolean(BaseIterator iter)
		{
			return this._value;
		}

		// Token: 0x04000703 RID: 1795
		private bool _value;
	}
}
