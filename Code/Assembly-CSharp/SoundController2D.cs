using System;
using UnityEngine;

// Token: 0x020006AB RID: 1707
internal class SoundController2D : MonoBehaviour
{
	// Token: 0x06002933 RID: 10547 RVA: 0x000B1E18 File Offset: 0x000B0018
	public void OnStart()
	{
		this.m_audio = base.GetComponent<AudioSource>();
		this.m_transform = base.transform;
		this.m_audio.maxDistance = 999999f;
		this.m_audio.rolloffMode = AudioRolloffMode.Linear;
		this.Volume = this.m_audio.volume;
		this.MinDistance = this.m_audio.minDistance;
		this.MaxDistance = this.m_audio.maxDistance;
		this.LateUpdate();
	}

	// Token: 0x06002934 RID: 10548 RVA: 0x000B1E94 File Offset: 0x000B0094
	private void LateUpdate()
	{
		Vector3 position = Camera.main.transform.position;
		Vector3 v = this.m_transform.position - position;
		float num = v.magnitude;
		num = Mathf.InverseLerp(this.MinDistance, this.MaxDistance, num);
		num = Mathf.Clamp(num, 0f, 1f);
		if (this.RolloffMode == AudioRolloffMode.Linear)
		{
			num = 1f - num;
		}
		if (this.RolloffMode == AudioRolloffMode.Logarithmic)
		{
			num = 1f - Mathf.Log(num * 9f + 1f);
		}
		num *= this.Volume;
		this.m_audio.volume = num;
	}

	// Token: 0x040024B6 RID: 9398
	public float Volume = 1f;

	// Token: 0x040024B7 RID: 9399
	public float MinDistance = 1f;

	// Token: 0x040024B8 RID: 9400
	public float MaxDistance = 200f;

	// Token: 0x040024B9 RID: 9401
	public AudioRolloffMode RolloffMode;

	// Token: 0x040024BA RID: 9402
	private AudioSource m_audio;

	// Token: 0x040024BB RID: 9403
	private Transform m_transform;
}
