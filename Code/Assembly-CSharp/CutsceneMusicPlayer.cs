using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class CutsceneMusicPlayer : MonoBehaviour
{
	// Token: 0x060008BD RID: 2237 RVA: 0x000257DC File Offset: 0x000239DC
	public void Play(bool pauseOnSuspend)
	{
		if (this.Cutscene.CutsceneMusicPlayer)
		{
			if (this.KeepPreviousLoop && !this.Cutscene.CutsceneMusicPlayer.name.StartsWith(base.name))
			{
				this.m_lastLoop = this.Cutscene.CutsceneMusicPlayer.GetLastLoop();
				this.m_originalLastLoopVolume = this.Cutscene.CutsceneMusicPlayer.GetLastLoopOriginalVolume();
				this.m_loopIndex = 2;
			}
			if (!base.name.Contains(this.Cutscene.CutsceneMusicPlayer.name))
			{
				this.Cutscene.CutsceneMusicPlayer.EndMusic(this.KeepPreviousLoop);
			}
		}
		this.Cutscene.CutsceneMusicPlayer = this;
		if (this.m_musicPhrase)
		{
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_musicPhrase.gameObject);
		}
		this.m_musicPhrase = Sound.Play(this.MusicPhrase, base.transform.position, delegate()
		{
			this.m_musicPhrase = null;
		});
		if (!InstantiateUtility.IsDestroyed(this.m_musicPhrase))
		{
			this.m_musicPhrase.SoundType = SoundType.Music;
			this.m_originalMusicPhraseVolume = this.m_musicPhrase.Volume;
			this.m_musicPhrase.PauseOnSuspend = pauseOnSuspend;
		}
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x00025924 File Offset: 0x00023B24
	public void Start()
	{
		if (this.LoopMusicSoundProvider == null)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00025944 File Offset: 0x00023B44
	private void FixedUpdate()
	{
		if (UI.MainMenuVisible)
		{
			return;
		}
		this.m_timePlaying += Time.fixedDeltaTime;
		if (this.m_timePlaying >= this.LoopDelay && this.m_lastLoop == null && this.LoopMusicSoundProvider)
		{
			this.m_lastLoop = Sound.PlayLooping(this.LoopMusicSoundProvider.GetSound(null), base.transform.position, delegate()
			{
				this.m_lastLoop = null;
			});
			this.m_lastLoop.SoundType = SoundType.Music;
			this.m_originalLastLoopVolume = this.m_lastLoop.Volume;
			this.m_timePlayingLoop = 0f;
			this.m_loopIndex++;
		}
		if (!InstantiateUtility.IsDestroyed(this.m_lastLoop) && this.m_loopIndex == 1)
		{
			this.m_lastLoop.Volume = this.m_originalLastLoopVolume * this.LoopFadeIn.Evaluate(this.m_timePlayingLoop);
			this.m_timePlayingLoop += Time.fixedDeltaTime;
		}
		if (!InstantiateUtility.IsDestroyed(this.m_lastLoop) && this.m_fadingOutLoop)
		{
			this.m_lastLoop.Volume = this.m_originalLastLoopVolume * this.LoopFadeOut.Evaluate(this.m_timeFadingOut);
			if (!InstantiateUtility.IsDestroyed(this.m_musicPhrase))
			{
				this.m_musicPhrase.Volume = this.m_originalMusicPhraseVolume * this.LoopFadeOut.Evaluate(this.m_timeFadingOut);
				if (this.m_musicPhrase.Volume <= 0f)
				{
					InstantiateUtility.Destroy(this.m_musicPhrase.gameObject);
					this.m_musicPhrase = null;
				}
			}
			if (this.m_lastLoop.Volume <= 0f)
			{
				InstantiateUtility.Destroy(this.m_lastLoop.gameObject);
				InstantiateUtility.Destroy(base.gameObject);
				this.m_lastLoop = null;
			}
			this.m_timeFadingOut += Time.fixedDeltaTime;
		}
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x00025B3C File Offset: 0x00023D3C
	public void EndMusic(bool keepPlayingLoop)
	{
		if (keepPlayingLoop)
		{
			InstantiateUtility.Destroy(base.gameObject);
			UnityEngine.Object.DestroyObject(this);
			return;
		}
		this.m_fadingOutLoop = true;
		this.m_timeFadingOut = 0f;
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x00025B73 File Offset: 0x00023D73
	public SoundPlayer GetLastLoop()
	{
		return this.m_lastLoop;
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x00025B7B File Offset: 0x00023D7B
	public float GetLastLoopOriginalVolume()
	{
		return this.m_originalLastLoopVolume;
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x00025B83 File Offset: 0x00023D83
	public void OnDestroy()
	{
		if (this.m_lastLoop)
		{
			this.m_lastLoop.FadeOut(2f, true);
		}
	}

	// Token: 0x0400070A RID: 1802
	public CutsceneController Cutscene;

	// Token: 0x0400070B RID: 1803
	public SoundDescriptor MusicPhrase;

	// Token: 0x0400070C RID: 1804
	public SoundProvider LoopMusicSoundProvider;

	// Token: 0x0400070D RID: 1805
	public float LoopDelay;

	// Token: 0x0400070E RID: 1806
	public AnimationCurve LoopFadeIn;

	// Token: 0x0400070F RID: 1807
	public AnimationCurve LoopFadeOut;

	// Token: 0x04000710 RID: 1808
	public bool KeepPreviousLoop;

	// Token: 0x04000711 RID: 1809
	private SoundPlayer m_lastLoop;

	// Token: 0x04000712 RID: 1810
	private SoundPlayer m_musicPhrase;

	// Token: 0x04000713 RID: 1811
	private float m_timePlaying;

	// Token: 0x04000714 RID: 1812
	private float m_originalLastLoopVolume;

	// Token: 0x04000715 RID: 1813
	private float m_timePlayingLoop;

	// Token: 0x04000716 RID: 1814
	private float m_timeFadingOut;

	// Token: 0x04000717 RID: 1815
	private float m_originalMusicPhraseVolume;

	// Token: 0x04000718 RID: 1816
	private int m_loopIndex;

	// Token: 0x04000719 RID: 1817
	private bool m_fadingOutLoop;
}
