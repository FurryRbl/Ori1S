using System;
using UnityEngine;

namespace fsm.triggers
{
	// Token: 0x02000542 RID: 1346
	public class OnCollisionExit : ITrigger
	{
		// Token: 0x06002350 RID: 9040 RVA: 0x0009A5FE File Offset: 0x000987FE
		public OnCollisionExit(Collision collision)
		{
			this.Collision = collision;
		}

		// Token: 0x04001DAF RID: 7599
		public readonly Collision Collision;
	}
}
