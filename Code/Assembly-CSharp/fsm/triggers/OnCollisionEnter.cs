using System;
using UnityEngine;

namespace fsm.triggers
{
	// Token: 0x020004FC RID: 1276
	public class OnCollisionEnter : ITrigger
	{
		// Token: 0x06002268 RID: 8808 RVA: 0x00096D35 File Offset: 0x00094F35
		public OnCollisionEnter(Collision collision)
		{
			this.Collision = collision;
		}

		// Token: 0x04001CDA RID: 7386
		public readonly Collision Collision;
	}
}
