using System;
using Core;
using UnityEngine;

// Token: 0x020006A6 RID: 1702
public class MusicSource : MonoBehaviour
{
	// Token: 0x06002923 RID: 10531 RVA: 0x000B1B29 File Offset: 0x000AFD29
	public void Start()
	{
		if (this.PlayAtStart)
		{
			this.Play();
		}
	}

	// Token: 0x06002924 RID: 10532 RVA: 0x000B1B3C File Offset: 0x000AFD3C
	public void Play()
	{
		this.m_musicLayer = new Music.Layer(this.SoundProvider, this.FadeInDuration, this.FadeOutDuration);
		Music.AddMusicLayer(this.m_musicLayer);
	}

	// Token: 0x06002925 RID: 10533 RVA: 0x000B1B71 File Offset: 0x000AFD71
	public void Stop()
	{
		Music.RemoveMusicLayer(this.m_musicLayer);
	}

	// Token: 0x06002926 RID: 10534 RVA: 0x000B1B7E File Offset: 0x000AFD7E
	public void OnDestroy()
	{
		if (this.m_musicLayer != null)
		{
			Music.RemoveMusicLayer(this.m_musicLayer);
		}
	}

	// Token: 0x040024AC RID: 9388
	public SoundProvider SoundProvider;

	// Token: 0x040024AD RID: 9389
	private Music.Layer m_musicLayer;

	// Token: 0x040024AE RID: 9390
	public float FadeInDuration;

	// Token: 0x040024AF RID: 9391
	public float FadeOutDuration;

	// Token: 0x040024B0 RID: 9392
	public bool PlayAtStart;
}
