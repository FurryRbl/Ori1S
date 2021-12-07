using System;
using UnityEngine;

// Token: 0x0200085C RID: 2140
[AddComponentMenu("Uber Water/Splash")]
public class UberWaterSplash : MonoBehaviour
{
	// Token: 0x06003084 RID: 12420 RVA: 0x000CE630 File Offset: 0x000CC830
	private void OnSplash(SeinSplashInfo splash)
	{
		if (this.AutoDeleteSplash)
		{
			UnityEngine.Object.Destroy(base.gameObject, this.SplashLifetime);
		}
		int num = UnityEngine.Random.Range(0, this.SplashClips.Length);
		this.m_audioSource = (base.GetComponent<AudioSource>() ?? base.gameObject.AddComponent<AudioSource>());
		this.m_audioSource.clip = this.SplashClips[num];
		this.m_audioSource.volume = Mathf.Clamp01(splash.Power / 2f);
		this.m_audioSource.Play();
		this.ParticleEmitter.minEmission *= splash.Power;
		this.ParticleEmitter.maxEmission *= splash.Power;
		this.ParticleEmitter.Emit();
	}

	// Token: 0x04002BD3 RID: 11219
	public bool AutoDeleteSplash;

	// Token: 0x04002BD4 RID: 11220
	public float SplashLifetime;

	// Token: 0x04002BD5 RID: 11221
	public AudioClip[] SplashClips;

	// Token: 0x04002BD6 RID: 11222
	public ParticleEmitter ParticleEmitter;

	// Token: 0x04002BD7 RID: 11223
	private AudioSource m_audioSource;
}
