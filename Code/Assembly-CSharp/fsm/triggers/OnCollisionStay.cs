using System;
using UnityEngine;

namespace fsm.triggers
{
	// Token: 0x0200052B RID: 1323
	public class OnCollisionStay : ITrigger
	{
		// Token: 0x06002317 RID: 8983 RVA: 0x00099D63 File Offset: 0x00097F63
		public OnCollisionStay(Collision collision)
		{
			this.Collision = collision;
		}

		// Token: 0x04001D91 RID: 7569
		public readonly Collision Collision;
	}
}
