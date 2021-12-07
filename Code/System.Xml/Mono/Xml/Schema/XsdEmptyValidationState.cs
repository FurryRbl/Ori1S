using System;
using System.Collections;

namespace Mono.Xml.Schema
{
	// Token: 0x0200002D RID: 45
	internal class XsdEmptyValidationState : XsdValidationState
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00009BB4 File Offset: 0x00007DB4
		public XsdEmptyValidationState(XsdParticleStateManager manager) : base(manager)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009BC0 File Offset: 0x00007DC0
		public override void GetExpectedParticles(ArrayList al)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009BC4 File Offset: 0x00007DC4
		public override XsdValidationState EvaluateStartElement(string name, string ns)
		{
			return XsdValidationState.Invalid;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00009BCC File Offset: 0x00007DCC
		public override bool EvaluateEndElement()
		{
			return true;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00009BD0 File Offset: 0x00007DD0
		internal override bool EvaluateIsEmptiable()
		{
			return true;
		}
	}
}
