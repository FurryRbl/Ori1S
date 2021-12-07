using System;
using Game;
using UnityEngine;

// Token: 0x0200098B RID: 2443
public class PlayerParticlesController : MonoBehaviour
{
	// Token: 0x06003571 RID: 13681 RVA: 0x000DFFBC File Offset: 0x000DE1BC
	private void Update()
	{
		base.GetComponent<ParticleEmitter>().emit = (Characters.Sein.PlatformBehaviour.PlatformMovement.LocalSpeed != Vector2.zero);
	}
}
