using System;
using UnityEngine;

// Token: 0x0200094B RID: 2379
public class StormZone : MonoBehaviour
{
	// Token: 0x0600346B RID: 13419 RVA: 0x000DC35C File Offset: 0x000DA55C
	private void OnTriggerEnter(Collider collider)
	{
		SeinController component = collider.GetComponent<SeinController>();
		if (component)
		{
			this.Active = true;
			this.m_currentTime = 0f;
		}
	}

	// Token: 0x0600346C RID: 13420 RVA: 0x000DC390 File Offset: 0x000DA590
	private void OnTriggerExit(Collider collider)
	{
		SeinController component = collider.GetComponent<SeinController>();
		if (component)
		{
			this.Active = false;
		}
	}

	// Token: 0x0600346D RID: 13421 RVA: 0x000DC3B8 File Offset: 0x000DA5B8
	private void FixedUpdate()
	{
		this.Quiet.FadeOut();
		this.Anticipation.FadeOut();
		this.Storm.FadeOut();
		if (this.Active)
		{
			this.m_currentTime += Time.deltaTime;
			if (this.m_currentTime < this.QuietDuration)
			{
				this.Quiet.FadeIn();
			}
			else if (this.m_currentTime < this.QuietDuration + this.AnticipationDuration)
			{
				this.Anticipation.FadeIn();
			}
			else if (this.m_currentTime < this.QuietDuration + this.AnticipationDuration + this.StormDuration)
			{
				this.Storm.FadeIn();
			}
			else
			{
				this.m_currentTime -= this.QuietDuration + this.AnticipationDuration + this.StormDuration;
			}
		}
		this.Quiet.Update();
		this.Anticipation.Update();
		this.Storm.Update();
	}

	// Token: 0x04002F49 RID: 12105
	public float QuietDuration = 5f;

	// Token: 0x04002F4A RID: 12106
	public float AnticipationDuration = 2f;

	// Token: 0x04002F4B RID: 12107
	public float StormDuration = 4f;

	// Token: 0x04002F4C RID: 12108
	public bool Active;

	// Token: 0x04002F4D RID: 12109
	public StormZone.SoundSource Quiet;

	// Token: 0x04002F4E RID: 12110
	public StormZone.SoundSource Anticipation;

	// Token: 0x04002F4F RID: 12111
	public StormZone.SoundSource Storm;

	// Token: 0x04002F50 RID: 12112
	private float m_currentTime;

	// Token: 0x0200094C RID: 2380
	[Serializable]
	public class SoundSource
	{
		// Token: 0x0600346F RID: 13423 RVA: 0x000DC4E6 File Offset: 0x000DA6E6
		public void FadeIn()
		{
			this.Volume.Speed = 1f / this.FadeInDuration;
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x000DC4FF File Offset: 0x000DA6FF
		public void FadeOut()
		{
			this.Volume.Speed = -1f / this.FadeOutDuration;
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000DC518 File Offset: 0x000DA718
		public void Update()
		{
			this.Volume.Update(Time.deltaTime);
			this.Source.volume = this.Volume.Value;
		}

		// Token: 0x04002F51 RID: 12113
		public AnimatingFloat Volume = new AnimatingFloat();

		// Token: 0x04002F52 RID: 12114
		public AudioSource Source;

		// Token: 0x04002F53 RID: 12115
		public float FadeInDuration = 1f;

		// Token: 0x04002F54 RID: 12116
		public float FadeOutDuration = 1f;
	}
}
