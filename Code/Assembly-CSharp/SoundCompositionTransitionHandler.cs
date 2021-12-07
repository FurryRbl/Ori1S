using System;
using Core;
using UnityEngine;

// Token: 0x020006BA RID: 1722
public class SoundCompositionTransitionHandler : MonoBehaviour
{
	// Token: 0x06002963 RID: 10595 RVA: 0x000B2D60 File Offset: 0x000B0F60
	public void FixedUpdate()
	{
		this.m_time += Time.deltaTime;
		if (this.From)
		{
			for (int i = 0; i < this.From.Loops.Count; i++)
			{
				SoundPlayer soundPlayer = this.From.Loops[i];
				global::SoundComposition.SoundLoop soundLoop = this.From.SoundComposition.Loops[i];
				if (soundPlayer)
				{
					this.ApplyFadeOut(soundPlayer, soundLoop.Sound);
				}
			}
			for (int j = 0; j < this.From.Layers.Count; j++)
			{
				SoundPlayer soundPlayer2 = this.From.Layers[j];
				global::SoundComposition.SoundLayer soundLayer = this.From.SoundComposition.Layers[j];
				if (soundPlayer2)
				{
					this.ApplyFadeOut(soundPlayer2, soundLayer.Sound);
				}
			}
		}
		if (this.To && this.m_time > this.Transition.NextSoundDelay)
		{
			if (!this.m_playedNext)
			{
				this.To.Play();
				this.m_playedNext = true;
			}
			for (int k = 0; k < this.To.Loops.Count; k++)
			{
				SoundPlayer soundPlayer3 = this.To.Loops[k];
				global::SoundComposition.SoundLoop soundLoop2 = this.To.SoundComposition.Loops[k];
				if (soundPlayer3)
				{
					this.ApplyFadeIn(soundPlayer3, soundLoop2.Sound);
				}
			}
			for (int l = 0; l < this.To.Layers.Count; l++)
			{
				SoundPlayer soundPlayer4 = this.To.Layers[l];
				global::SoundComposition.SoundLayer soundLayer2 = this.To.SoundComposition.Layers[l];
				if (soundPlayer4)
				{
					this.ApplyFadeIn(soundPlayer4, soundLayer2.Sound);
				}
			}
		}
		if (this.m_time >= this.Transition.TransitionDelay && this.Transition.Sound)
		{
			if (!this.m_playedTransition)
			{
				this.m_playedTransition = true;
				SoundDescriptor soundDescriptor = new SoundDescriptor(this.Transition.Sound, 1f);
				soundDescriptor.SoundSize.Radius = 0f;
				soundDescriptor.SoundSize.FalloffMargin = 0f;
				soundDescriptor.ShouldBePanned = false;
				soundDescriptor.Volume = 1f;
				soundDescriptor.MixerGroup = MixerGroupType.MusicLoops;
				if (this.m_transitionPlayer)
				{
					UberPoolManager.Instance.RemoveOnDestroyed(this.m_transitionPlayer.gameObject);
				}
				this.m_transitionPlayer = Sound.Play(soundDescriptor, Vector3.zero, delegate()
				{
					this.m_transitionPlayer = null;
				});
				if (this.m_transitionPlayer)
				{
					this.m_transitionPlayer.SoundType = SoundType.Music;
					this.m_transitionPlayer.DestroyOnRestart = true;
				}
			}
			if (this.m_transitionPlayer)
			{
				this.m_transitionPlayer.Volume = this.Transition.Volume;
			}
		}
		if (this.From && this.m_time >= this.Transition.EndPreviousSoundTime)
		{
			UnityEngine.Object.DestroyObject(this.From);
		}
		if (this.m_time >= this.Transition.EndTransitionTime)
		{
			if (this.From)
			{
				UnityEngine.Object.DestroyObject(this.From);
			}
			UnityEngine.Object.DestroyObject(this);
		}
	}

	// Token: 0x06002964 RID: 10596 RVA: 0x000B3104 File Offset: 0x000B1304
	private void ApplyFadeOut(SoundPlayer soundPlayer, AudioClip sound)
	{
		foreach (SoundCompositionTransition.SoundFadeInformation soundFadeInformation in this.Transition.FadeOut)
		{
			if (soundFadeInformation.Sound == sound)
			{
				if (soundFadeInformation.FadeDuration != 0f)
				{
					soundPlayer.Volume = soundFadeInformation.FadeCurve.Evaluate((this.m_time - soundFadeInformation.FadeStartTime) / soundFadeInformation.FadeDuration);
				}
				return;
			}
		}
		if (this.Transition.DefaultFadeOut.FadeDuration != 0f)
		{
			soundPlayer.Volume = this.Transition.DefaultFadeOut.FadeCurve.Evaluate((this.m_time - this.Transition.DefaultFadeOut.FadeStartTime) / this.Transition.DefaultFadeOut.FadeDuration);
		}
	}

	// Token: 0x06002965 RID: 10597 RVA: 0x000B3204 File Offset: 0x000B1404
	private void ApplyFadeIn(SoundPlayer soundPlayer, AudioClip sound)
	{
		foreach (SoundCompositionTransition.SoundFadeInformation soundFadeInformation in this.Transition.FadeIn)
		{
			if (soundFadeInformation.Sound == sound)
			{
				if (soundFadeInformation.FadeDuration != 0f && soundFadeInformation.FadeDuration != 0f)
				{
					soundPlayer.Volume = soundFadeInformation.FadeCurve.Evaluate((this.m_time - soundFadeInformation.FadeStartTime - this.Transition.NextSoundDelay) / soundFadeInformation.FadeDuration);
				}
				return;
			}
		}
		if (this.Transition.DefaultFadeIn.FadeDuration != 0f)
		{
			soundPlayer.Volume = this.Transition.DefaultFadeIn.FadeCurve.Evaluate((this.m_time - this.Transition.DefaultFadeIn.FadeStartTime - this.Transition.NextSoundDelay) / this.Transition.DefaultFadeIn.FadeDuration);
		}
	}

	// Token: 0x040024F1 RID: 9457
	public SoundCompositionPlayer From;

	// Token: 0x040024F2 RID: 9458
	public SoundCompositionPlayer To;

	// Token: 0x040024F3 RID: 9459
	public SoundCompositionTransition Transition;

	// Token: 0x040024F4 RID: 9460
	private float m_time;

	// Token: 0x040024F5 RID: 9461
	private SoundPlayer m_transitionPlayer;

	// Token: 0x040024F6 RID: 9462
	private bool m_playedTransition;

	// Token: 0x040024F7 RID: 9463
	private bool m_playedNext;
}
