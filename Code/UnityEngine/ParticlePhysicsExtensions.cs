using System;

namespace UnityEngine
{
	// Token: 0x0200010E RID: 270
	public static class ParticlePhysicsExtensions
	{
		// Token: 0x0600112F RID: 4399 RVA: 0x000140F4 File Offset: 0x000122F4
		public static int GetSafeCollisionEventSize(this ParticleSystem ps)
		{
			return ParticleSystemExtensionsImpl.GetSafeCollisionEventSize(ps);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x000140FC File Offset: 0x000122FC
		public static int GetCollisionEvents(this ParticleSystem ps, GameObject go, ParticleCollisionEvent[] collisionEvents)
		{
			return ParticleSystemExtensionsImpl.GetCollisionEvents(ps, go, collisionEvents);
		}
	}
}
