using System;
using UnityEngine;

// Token: 0x020006A0 RID: 1696
[RequireComponent(typeof(AudioSource))]
public class FadingSound : MonoBehaviour
{
	// Token: 0x06002914 RID: 10516 RVA: 0x000B18B5 File Offset: 0x000AFAB5
	private void Start()
	{
		this.m_audioSource = base.GetComponent<AudioSource>();
		this.m_originalVolume = this.m_audioSource.volume;
		this.m_audioSource.volume = 0f;
	}

	// Token: 0x06002915 RID: 10517 RVA: 0x000B18E4 File Offset: 0x000AFAE4
	public void ChangeVolume(float value)
	{
		this.m_audioSource.volume = this.m_originalVolume * value;
	}

	// Token: 0x040024A0 RID: 9376
	private AudioSource m_audioSource;

	// Token: 0x040024A1 RID: 9377
	private float m_originalVolume;
}
