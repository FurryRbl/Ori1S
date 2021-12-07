using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200010D RID: 269
	internal sealed class ParticleSystemExtensionsImpl
	{
		// Token: 0x0600112D RID: 4397
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetSafeCollisionEventSize(ParticleSystem ps);

		// Token: 0x0600112E RID: 4398
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetCollisionEvents(ParticleSystem ps, GameObject go, ParticleCollisionEvent[] collisionEvents);
	}
}
