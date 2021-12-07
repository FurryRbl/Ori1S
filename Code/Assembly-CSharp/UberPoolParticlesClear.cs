using System;
using UnityEngine;

// Token: 0x020006F5 RID: 1781
public class UberPoolParticlesClear : MonoBehaviour, IPooled
{
	// Token: 0x06002A80 RID: 10880 RVA: 0x000B685E File Offset: 0x000B4A5E
	public void OnPoolSpawned()
	{
		base.GetComponent<ParticleEmitter>().ClearParticles();
	}
}
