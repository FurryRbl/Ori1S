using System;
using Core;
using UnityEngine;

// Token: 0x0200015F RID: 351
public class SoundCompositionManager : MonoBehaviour
{
	// Token: 0x06000E25 RID: 3621 RVA: 0x00041BD7 File Offset: 0x0003FDD7
	public void Awake()
	{
		Core.SoundComposition.Manager = this;
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x00041BDF File Offset: 0x0003FDDF
	public void OnDestroy()
	{
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x00041BE4 File Offset: 0x0003FDE4
	public void PlaySound(global::SoundComposition soundComposition, SoundCompositionTransition transition)
	{
		if (this.m_soundCompositionPlayer && this.m_soundCompositionPlayer.SoundComposition == soundComposition)
		{
			return;
		}
		SoundCompositionTransitionHandler soundCompositionTransitionHandler = base.gameObject.AddComponent<SoundCompositionTransitionHandler>();
		soundCompositionTransitionHandler.From = this.m_soundCompositionPlayer;
		if (soundComposition)
		{
			SoundCompositionPlayer soundCompositionPlayer = base.gameObject.AddComponent<SoundCompositionPlayer>();
			soundCompositionPlayer.SetSoundComposition(soundComposition);
			soundCompositionTransitionHandler.To = soundCompositionPlayer;
			this.m_soundCompositionPlayer = soundCompositionPlayer;
		}
		else
		{
			this.m_soundCompositionPlayer = null;
		}
		soundCompositionTransitionHandler.Transition = ((!transition) ? this.DefaultSilenceTransition : transition);
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x00041C87 File Offset: 0x0003FE87
	public void StopMusic()
	{
		this.PlaySound(null, null);
	}

	// Token: 0x04000B6B RID: 2923
	private SoundCompositionPlayer m_soundCompositionPlayer;

	// Token: 0x04000B6C RID: 2924
	public SoundCompositionTransition DefaultSilenceTransition;
}
