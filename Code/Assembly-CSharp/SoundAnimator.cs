using System;
using Core;
using UnityEngine;

// Token: 0x02000667 RID: 1639
public class SoundAnimator : BaseAnimator
{
	// Token: 0x060027F5 RID: 10229 RVA: 0x000AD9C4 File Offset: 0x000ABBC4
	private SoundDescriptor GetSoundDescriptor()
	{
		if (this.m_soundDescriptor == null)
		{
			if (this.SoundProvider == null)
			{
				return null;
			}
			this.m_soundDescriptor = this.SoundProvider.GetSound(null);
		}
		return this.m_soundDescriptor;
	}

	// Token: 0x060027F6 RID: 10230 RVA: 0x000ADA07 File Offset: 0x000ABC07
	public override void CacheOriginals()
	{
	}

	// Token: 0x060027F7 RID: 10231 RVA: 0x000ADA0C File Offset: 0x000ABC0C
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		if (forceSample || value <= 0f)
		{
			this.m_started = false;
		}
		if (value <= 0f && this.m_soundPlayer)
		{
			this.m_soundPlayer.Stop();
			InstantiateUtility.Destroy(this.m_soundPlayer.gameObject);
		}
		if (!this.m_started && value > 0f)
		{
			this.m_started = true;
			if (!this.m_soundPlayer && value < this.Duration)
			{
				this.m_soundPlayer = Sound.Play(this.GetSoundDescriptor(), (!this.Target) ? Vector3.zero : this.Target.position, delegate()
				{
					this.m_soundPlayer = null;
				});
				if (value > 0.2f)
				{
					this.m_soundPlayer.SetTime(value);
				}
			}
		}
	}

	// Token: 0x060027F8 RID: 10232 RVA: 0x000ADB02 File Offset: 0x000ABD02
	public void OnDisable()
	{
		if (this.m_soundPlayer)
		{
			this.m_soundPlayer.Stop();
			InstantiateUtility.Destroy(this.m_soundPlayer.gameObject);
		}
	}

	// Token: 0x1700065C RID: 1628
	// (get) Token: 0x060027F9 RID: 10233 RVA: 0x000ADB2F File Offset: 0x000ABD2F
	public override bool IsLooping
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700065D RID: 1629
	// (get) Token: 0x060027FA RID: 10234 RVA: 0x000ADB34 File Offset: 0x000ABD34
	public override float Duration
	{
		get
		{
			if (this.m_length == 0f)
			{
				SoundDescriptor soundDescriptor = this.GetSoundDescriptor();
				if (soundDescriptor != null && soundDescriptor.AudioClip)
				{
					this.m_length = soundDescriptor.AudioClip.length;
				}
			}
			return this.m_length;
		}
	}

	// Token: 0x060027FB RID: 10235 RVA: 0x000ADB85 File Offset: 0x000ABD85
	public override void RestoreToOriginalState()
	{
	}

	// Token: 0x04002286 RID: 8838
	public SoundProvider SoundProvider;

	// Token: 0x04002287 RID: 8839
	public Transform Target;

	// Token: 0x04002288 RID: 8840
	private SoundPlayer m_soundPlayer;

	// Token: 0x04002289 RID: 8841
	private SoundDescriptor m_soundDescriptor;

	// Token: 0x0400228A RID: 8842
	private bool m_started;

	// Token: 0x0400228B RID: 8843
	private float m_length;
}
