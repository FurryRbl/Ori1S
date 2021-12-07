using System;
using System.Collections;

namespace Mono.Xml.Schema
{
	// Token: 0x0200002C RID: 44
	internal class XsdAppendedValidationState : XsdValidationState
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00009AB0 File Offset: 0x00007CB0
		public XsdAppendedValidationState(XsdParticleStateManager manager, XsdValidationState head, XsdValidationState rest) : base(manager)
		{
			this.head = head;
			this.rest = rest;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00009AC8 File Offset: 0x00007CC8
		public override void GetExpectedParticles(ArrayList al)
		{
			this.head.GetExpectedParticles(al);
			this.rest.GetExpectedParticles(al);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00009AE4 File Offset: 0x00007CE4
		public override XsdValidationState EvaluateStartElement(string name, string ns)
		{
			XsdValidationState xsdValidationState = this.head.EvaluateStartElement(name, ns);
			if (xsdValidationState != XsdValidationState.Invalid)
			{
				this.head = xsdValidationState;
				return (!(xsdValidationState is XsdEmptyValidationState)) ? this : this.rest;
			}
			if (!this.head.EvaluateIsEmptiable())
			{
				return XsdValidationState.Invalid;
			}
			return this.rest.EvaluateStartElement(name, ns);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009B4C File Offset: 0x00007D4C
		public override bool EvaluateEndElement()
		{
			if (this.head.EvaluateEndElement())
			{
				return this.rest.EvaluateIsEmptiable();
			}
			return this.head.EvaluateIsEmptiable() && this.rest.EvaluateEndElement();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00009B94 File Offset: 0x00007D94
		internal override bool EvaluateIsEmptiable()
		{
			return this.head.EvaluateIsEmptiable() && this.rest.EvaluateIsEmptiable();
		}

		// Token: 0x04000129 RID: 297
		private XsdValidationState head;

		// Token: 0x0400012A RID: 298
		private XsdValidationState rest;
	}
}
