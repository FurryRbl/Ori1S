using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
[Serializable]
public class SoundDescriptor
{
	// Token: 0x060001FF RID: 511 RVA: 0x000086D4 File Offset: 0x000068D4
	public SoundDescriptor()
	{
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00008724 File Offset: 0x00006924
	public SoundDescriptor(AudioClip audioClip, float volume)
	{
		this.AudioClip = audioClip;
		this.Volume = volume;
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0000877F File Offset: 0x0000697F
	public void SetSoundSize(SoundSize size)
	{
		if (this.m_ownSoundSize && size != this.SoundSize)
		{
			this.m_ownSoundSize = false;
		}
		this.SoundSize = size;
	}

	// Token: 0x06000202 RID: 514 RVA: 0x000087A6 File Offset: 0x000069A6
	public void SetLowPassFilter(LowPassFilterSettings settings)
	{
		if (this.m_ownLowPass && settings != this.LowPassFilterSettings)
		{
			this.m_ownLowPass = false;
		}
		this.LowPassFilterSettings = settings;
	}

	// Token: 0x06000203 RID: 515 RVA: 0x000087D0 File Offset: 0x000069D0
	public void Reset()
	{
		this.AudioClip = null;
		this.Volume = 0.6f;
		if (this.m_ownSoundSize)
		{
			this.SoundSize.Radius = 5f;
			this.SoundSize.FalloffMargin = 10f;
		}
		this.ShouldBePanned = false;
		this.Pitch = 1f;
		if (this.m_ownLowPass)
		{
			this.LowPassFilterSettings.Active = false;
			this.LowPassFilterSettings.CutoffFrequency = AnimationCurve.Linear(0f, 0f, 30f, 22000f);
		}
		this.SoundProvider = null;
		this.SyncToTime = false;
		this.MixerGroup = MixerGroupType.Unspecified;
	}

	// Token: 0x040001A3 RID: 419
	public AudioClip AudioClip;

	// Token: 0x040001A4 RID: 420
	public SoundSize SoundSize = new SoundSize();

	// Token: 0x040001A5 RID: 421
	public LowPassFilterSettings LowPassFilterSettings = new LowPassFilterSettings();

	// Token: 0x040001A6 RID: 422
	public SoundProvider SoundProvider;

	// Token: 0x040001A7 RID: 423
	public float Volume = 0.6f;

	// Token: 0x040001A8 RID: 424
	private bool m_ownSoundSize = true;

	// Token: 0x040001A9 RID: 425
	public bool ShouldBePanned;

	// Token: 0x040001AA RID: 426
	public float Pitch = 1f;

	// Token: 0x040001AB RID: 427
	private bool m_ownLowPass = true;

	// Token: 0x040001AC RID: 428
	public bool SyncToTime;

	// Token: 0x040001AD RID: 429
	public MixerGroupType MixerGroup;
}
