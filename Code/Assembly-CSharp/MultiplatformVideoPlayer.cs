using System;
using Core;
using UnityEngine;

// Token: 0x02000696 RID: 1686
public class MultiplatformVideoPlayer : MonoBehaviour
{
	// Token: 0x060028C1 RID: 10433 RVA: 0x000B0E16 File Offset: 0x000AF016
	private void OnValidate()
	{
		this.AudioTrack = null;
	}

	// Token: 0x060028C2 RID: 10434 RVA: 0x000B0E20 File Offset: 0x000AF020
	public void Perform()
	{
		this.windowsVideoPlayer.SetActive(true);
		this.videoPlayer = this.windowsVideoPlayer.GetComponent<WindowsVideoPlayer>();
		IVideoPlayer videoPlayer = this.videoPlayer;
		videoPlayer.OnPlaybackFinished = (OnDelegate)Delegate.Combine(videoPlayer.OnPlaybackFinished, new OnDelegate(this.OnPlaybackFinished));
		this.videoPlayer.Filename = this.Filename;
		this.videoPlayer.Play();
	}

	// Token: 0x060028C3 RID: 10435 RVA: 0x000B0E8D File Offset: 0x000AF08D
	private void Update()
	{
		if (Core.Input.ActionButtonA.OnPressed)
		{
			this.Pause();
		}
		if (Core.Input.Cancel.OnPressed)
		{
			this.Stop();
		}
	}

	// Token: 0x060028C4 RID: 10436 RVA: 0x000B0EBC File Offset: 0x000AF0BC
	private void OnApplicationFocus(bool focusStatus)
	{
		if (!focusStatus && !this.videoPlayer.IsPaused)
		{
			this.Pause();
			this.returnPlaybackWhenFocused = true;
		}
		if (focusStatus && this.returnPlaybackWhenFocused)
		{
			this.Pause();
			this.returnPlaybackWhenFocused = false;
		}
	}

	// Token: 0x060028C5 RID: 10437 RVA: 0x000B0F0A File Offset: 0x000AF10A
	public void Stop()
	{
		this.videoPlayer.Stop();
	}

	// Token: 0x060028C6 RID: 10438 RVA: 0x000B0F17 File Offset: 0x000AF117
	public void Pause()
	{
		this.videoPlayer.Pause();
	}

	// Token: 0x060028C7 RID: 10439 RVA: 0x000B0F24 File Offset: 0x000AF124
	public bool IsPaused()
	{
		return this.videoPlayer.IsPaused;
	}

	// Token: 0x060028C8 RID: 10440 RVA: 0x000B0F31 File Offset: 0x000AF131
	public bool IsPlaying()
	{
		return this.videoPlayer.IsPlaying;
	}

	// Token: 0x060028C9 RID: 10441 RVA: 0x000B0F3E File Offset: 0x000AF13E
	private void OnPlaybackFinished()
	{
		UnityEngine.Object.Destroy(base.gameObject, 0.001f);
	}

	// Token: 0x04002465 RID: 9317
	public string Filename;

	// Token: 0x04002466 RID: 9318
	public AudioClip AudioTrack;

	// Token: 0x04002467 RID: 9319
	public GameObject windowsVideoPlayer;

	// Token: 0x04002468 RID: 9320
	public GameObject xboxOneVideoPlayer;

	// Token: 0x04002469 RID: 9321
	private IVideoPlayer videoPlayer;

	// Token: 0x0400246A RID: 9322
	private bool returnPlaybackWhenFocused;
}
