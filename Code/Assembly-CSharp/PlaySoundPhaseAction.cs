using System;
using Core;
using UnityEngine;

// Token: 0x020006B2 RID: 1714
public class PlaySoundPhaseAction : PerformingAction
{
	// Token: 0x1700068F RID: 1679
	// (get) Token: 0x0600294D RID: 10573 RVA: 0x000B242F File Offset: 0x000B062F
	public override bool IsPerforming
	{
		get
		{
			return !InstantiateUtility.IsDestroyed(this.m_soundPlayer);
		}
	}

	// Token: 0x0600294E RID: 10574 RVA: 0x000B243F File Offset: 0x000B063F
	public override void Stop()
	{
	}

	// Token: 0x0600294F RID: 10575 RVA: 0x000B2444 File Offset: 0x000B0644
	public override void Perform(IContext context)
	{
		if (this.Transition)
		{
			Core.SoundComposition.Manager.PlaySound(null, this.Transition);
		}
		if (!this.SoundProvider)
		{
			SoundDescriptor soundDescriptor = new SoundDescriptor(this.Sound, 1f);
			soundDescriptor.SoundSize.Radius = 0f;
			soundDescriptor.SoundSize.FalloffMargin = 0f;
			soundDescriptor.ShouldBePanned = false;
			soundDescriptor.MixerGroup = MixerGroupType.MusicStingers;
			if (this.m_soundPlayer)
			{
				UberPoolManager.Instance.RemoveOnDestroyed(this.m_soundPlayer.gameObject);
			}
			this.m_soundPlayer = Core.Sound.Play(soundDescriptor, Vector3.zero, delegate()
			{
				this.m_soundPlayer = null;
			});
			if (this.m_soundPlayer)
			{
				this.m_soundPlayer.SoundType = SoundType.Music;
				this.m_soundPlayer.DestroyOnRestart = true;
			}
		}
		else
		{
			SoundDescriptor sound = this.SoundProvider.GetSound(null);
			sound.SoundSize.Radius = 0f;
			sound.SoundSize.FalloffMargin = 0f;
			sound.ShouldBePanned = false;
			sound.MixerGroup = MixerGroupType.MusicStingers;
			if (this.m_soundPlayer)
			{
				UberPoolManager.Instance.RemoveOnDestroyed(this.m_soundPlayer.gameObject);
			}
			this.m_soundPlayer = Core.Sound.Play(sound, Vector3.zero, delegate()
			{
				this.m_soundPlayer = null;
			});
			if (this.m_soundPlayer)
			{
				this.m_soundPlayer.SoundType = SoundType.Music;
				this.m_soundPlayer.DestroyOnRestart = true;
			}
		}
	}

	// Token: 0x040024D0 RID: 9424
	public AudioClip Sound;

	// Token: 0x040024D1 RID: 9425
	public SoundProvider SoundProvider;

	// Token: 0x040024D2 RID: 9426
	public SoundCompositionTransition Transition;

	// Token: 0x040024D3 RID: 9427
	private SoundPlayer m_soundPlayer;
}
