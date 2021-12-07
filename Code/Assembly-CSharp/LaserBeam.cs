using System;
using UnityEngine;

// Token: 0x02000904 RID: 2308
public class LaserBeam : MonoBehaviour
{
	// Token: 0x04002E31 RID: 11825
	public GameObject AnticipationEffect;

	// Token: 0x04002E32 RID: 11826
	public GameObject BurstEffect;

	// Token: 0x04002E33 RID: 11827
	public GameObject StopEffect;

	// Token: 0x04002E34 RID: 11828
	public GameObject ImpactEffect;

	// Token: 0x04002E35 RID: 11829
	public GameObject ImpactPointLoopEffect;

	// Token: 0x04002E36 RID: 11830
	public Transform BeamLenghtScaleTransform;

	// Token: 0x04002E37 RID: 11831
	public SoundProvider BeamLoopSoundProvider;

	// Token: 0x04002E38 RID: 11832
	public DamageType LaserDamageType = DamageType.Laser;

	// Token: 0x04002E39 RID: 11833
	public int DamageAmount = 10000;

	// Token: 0x04002E3A RID: 11834
	public float LaserBeamSizeToTilingRatio = 15.4402f;

	// Token: 0x04002E3B RID: 11835
	public BaseAnimator BeamEngageAnimator;

	// Token: 0x04002E3C RID: 11836
	public ParticleSystem[] BeamParticleSystems;
}
