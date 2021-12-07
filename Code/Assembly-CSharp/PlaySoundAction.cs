using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000313 RID: 787
[Category("Sound")]
public class PlaySoundAction : ActionMethod
{
	// Token: 0x0600174C RID: 5964 RVA: 0x00064AA4 File Offset: 0x00062CA4
	public override void Perform(IContext context)
	{
		Vector3 position = (!this.Target) ? UI.Cameras.Current.CameraTarget.TargetPosition : this.Target.position;
		SoundDescriptor soundDescriptor = (!this.SoundProvider) ? new SoundDescriptor(this.Audio.Clip, this.Audio.Volume) : this.SoundProvider.GetSound(context);
		if (this.m_soundPlayer)
		{
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_soundPlayer.gameObject);
		}
		this.m_soundPlayer = ((!this.ShouldPlayLooping) ? Sound.Play(soundDescriptor, position, delegate()
		{
			this.m_soundPlayer = null;
		}) : Sound.PlayLooping(soundDescriptor, position, delegate()
		{
			this.m_soundPlayer = null;
		}));
		if (!InstantiateUtility.IsDestroyed(this.m_soundPlayer))
		{
			this.m_soundPlayer.AttachTo = this.Target;
			this.m_soundPlayer.PauseOnSuspend = this.PauseOnSuspend;
			this.m_soundPlayer.DestroyOnRestart = true;
			if (this.ShouldFadeIn)
			{
				this.m_soundPlayer.FadeIn(0.4f, true);
			}
		}
	}

	// Token: 0x1700041E RID: 1054
	// (get) Token: 0x0600174D RID: 5965 RVA: 0x00064BDC File Offset: 0x00062DDC
	private string AudioClipName
	{
		get
		{
			return (this.Audio == null) ? "unknown" : ((!(this.Audio.Clip != null)) ? "unkown" : this.Audio.Clip.name);
		}
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x00064C30 File Offset: 0x00062E30
	public override string GetNiceName()
	{
		return (!this.SoundProvider) ? ("Play Sound: " + this.AudioClipName) : ("Play Sound: " + this.SoundProvider.name);
	}

	// Token: 0x0600174F RID: 5967 RVA: 0x00064C77 File Offset: 0x00062E77
	public override void OnDestroy()
	{
		if (!InstantiateUtility.IsDestroyed(this.m_soundPlayer))
		{
			this.m_soundPlayer.FadeOut(1f, true);
		}
		base.OnDestroy();
	}

	// Token: 0x04001405 RID: 5125
	public AudioProperties Audio;

	// Token: 0x04001406 RID: 5126
	public SoundProvider SoundProvider;

	// Token: 0x04001407 RID: 5127
	public bool ShouldPlayLooping;

	// Token: 0x04001408 RID: 5128
	public bool PauseOnSuspend;

	// Token: 0x04001409 RID: 5129
	public bool ShouldFadeIn;

	// Token: 0x0400140A RID: 5130
	public Transform Target;

	// Token: 0x0400140B RID: 5131
	private SoundPlayer m_soundPlayer;
}
