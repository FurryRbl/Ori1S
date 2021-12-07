using System;
using UnityEngine;

// Token: 0x0200069F RID: 1695
public class AmbientSoundZone : MonoBehaviour
{
	// Token: 0x0600290D RID: 10509 RVA: 0x000B16E7 File Offset: 0x000AF8E7
	private void Awake()
	{
		this.m_fadingSounds = base.GetComponentsInChildren<FadingSound>();
		this.existed = true;
	}

	// Token: 0x0600290E RID: 10510 RVA: 0x000B16FC File Offset: 0x000AF8FC
	private void Start()
	{
		this.m_fadingSounds = base.GetComponentsInChildren<FadingSound>();
	}

	// Token: 0x0600290F RID: 10511 RVA: 0x000B170C File Offset: 0x000AF90C
	private void OnTriggerEnter(Collider collider)
	{
		if (!this.m_enabled)
		{
			return;
		}
		if (this.Playing)
		{
			return;
		}
		SeinController component = collider.GetComponent<SeinController>();
		if (component == null)
		{
			return;
		}
		this.Playing = true;
		if (this.PlayFromStart)
		{
			for (int i = 0; i < this.m_fadingSounds.Length; i++)
			{
				FadingSound fadingSound = this.m_fadingSounds[i];
				fadingSound.GetComponent<AudioSource>().Play();
			}
		}
	}

	// Token: 0x06002910 RID: 10512 RVA: 0x000B1784 File Offset: 0x000AF984
	private void OnTriggerExit(Collider collider)
	{
		if (!this.m_enabled)
		{
			return;
		}
		if (!this.Playing)
		{
			return;
		}
		if (base.GetComponent<Collider>().bounds.Contains(collider.transform.position))
		{
			return;
		}
		SeinController component = collider.GetComponent<SeinController>();
		if (component == null)
		{
			return;
		}
		this.Playing = false;
	}

	// Token: 0x06002911 RID: 10513 RVA: 0x000B17E8 File Offset: 0x000AF9E8
	private void OnLevelWasLoaded(int level)
	{
		if (this.existed)
		{
			this.m_enabled = false;
			this.Playing = false;
			UnityEngine.Object.DestroyObject(base.gameObject, this.FadeOutDuration);
		}
	}

	// Token: 0x06002912 RID: 10514 RVA: 0x000B1820 File Offset: 0x000AFA20
	private void Update()
	{
		this.m_fade.Speed = 1f / ((!this.Playing || AmbientSoundZone.Muted) ? (-this.FadeOutDuration) : this.FadeInDuration);
		if (this.m_fade.Update(Time.deltaTime))
		{
			for (int i = 0; i < this.m_fadingSounds.Length; i++)
			{
				FadingSound fadingSound = this.m_fadingSounds[i];
				fadingSound.ChangeVolume(this.m_fade.Value);
			}
		}
	}

	// Token: 0x04002497 RID: 9367
	public float FadeInDuration = 3f;

	// Token: 0x04002498 RID: 9368
	public float FadeOutDuration = 3f;

	// Token: 0x04002499 RID: 9369
	public bool Playing;

	// Token: 0x0400249A RID: 9370
	public bool PlayFromStart = true;

	// Token: 0x0400249B RID: 9371
	private FadingSound[] m_fadingSounds;

	// Token: 0x0400249C RID: 9372
	private bool m_enabled = true;

	// Token: 0x0400249D RID: 9373
	private AnimatingFloat m_fade = new AnimatingFloat();

	// Token: 0x0400249E RID: 9374
	private bool existed;

	// Token: 0x0400249F RID: 9375
	public static bool Muted;
}
