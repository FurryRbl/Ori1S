using System;
using System.Collections;

namespace Mono.Xml.Schema
{
	// Token: 0x0200002E RID: 46
	internal class XsdInvalidValidationState : XsdValidationState
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00009BD4 File Offset: 0x00007DD4
		internal XsdInvalidValidationState(XsdParticleStateManager manager) : base(manager)
		{
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00009BE0 File Offset: 0x00007DE0
		public override void GetExpectedParticles(ArrayList al)
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00009BE4 File Offset: 0x00007DE4
		public override XsdValidationState EvaluateStartElement(string name, string ns)
		{
			return this;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00009BE8 File Offset: 0x00007DE8
		public override bool EvaluateEndElement()
		{
			return false;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00009BEC File Offset: 0x00007DEC
		internal override bool EvaluateIsEmptiable()
		{
			return false;
		}
	}
}
