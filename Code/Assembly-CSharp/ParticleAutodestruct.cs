using System;
using UnityEngine;

// Token: 0x020006E7 RID: 1767
public class ParticleAutodestruct : MonoBehaviour
{
	// Token: 0x06002A27 RID: 10791 RVA: 0x000B52CF File Offset: 0x000B34CF
	private void Start()
	{
		this.m_particleSystem = base.GetComponent<ParticleEmitter>();
		this.m_startTime = Time.time;
	}

	// Token: 0x06002A28 RID: 10792 RVA: 0x000B52E8 File Offset: 0x000B34E8
	private void Update()
	{
		if (this.m_particleSystem.particleCount == 0 && Time.time - this.m_startTime > this.m_particleSystem.minEnergy)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400259E RID: 9630
	private ParticleEmitter m_particleSystem;

	// Token: 0x0400259F RID: 9631
	private float m_startTime;
}
