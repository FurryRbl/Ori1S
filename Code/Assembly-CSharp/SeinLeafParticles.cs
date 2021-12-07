using System;
using Game;
using UnityEngine;

// Token: 0x0200046A RID: 1130
public class SeinLeafParticles : MonoBehaviour
{
	// Token: 0x06001F04 RID: 7940 RVA: 0x00088A55 File Offset: 0x00086C55
	public void Awake()
	{
		this.m_particleRate = this.Particles.emissionRate;
	}

	// Token: 0x06001F05 RID: 7941 RVA: 0x00088A68 File Offset: 0x00086C68
	public void Update()
	{
		SeinCharacter sein = Characters.Sein;
		PlatformMovement platformMovement = sein.PlatformBehaviour.PlatformMovement;
		Vector2 localSpeed = platformMovement.LocalSpeed;
		if (platformMovement.HasWallLeft || platformMovement.HasWallRight)
		{
			localSpeed.x = 0f;
		}
		this.Particles.emissionRate = this.m_particleRate * this.ParticleRateOverSpeed.Evaluate(localSpeed.magnitude);
	}

	// Token: 0x04001AF0 RID: 6896
	public ParticleSystem Particles;

	// Token: 0x04001AF1 RID: 6897
	public AnimationCurve ParticleRateOverSpeed;

	// Token: 0x04001AF2 RID: 6898
	private float m_particleRate;
}
