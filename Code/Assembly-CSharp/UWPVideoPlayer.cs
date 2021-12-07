using System;

// Token: 0x02000699 RID: 1689
public class UWPVideoPlayer : IVideoPlayer
{
	// Token: 0x060028DA RID: 10458 RVA: 0x000B10B4 File Offset: 0x000AF2B4
	public void Play()
	{
		this.IsPlaying = true;
		this.IsPaused = false;
		UWPVideoPlayer.OnPlayXAML(this.Filename);
		UWPVideoPlayer.OnXAMLFinished = new OnDelegate(this.XAMLFinished);
	}

	// Token: 0x060028DB RID: 10459 RVA: 0x000B10F0 File Offset: 0x000AF2F0
	public void Pause()
	{
		if (this.IsPaused)
		{
			UWPVideoPlayer.OnPlayXAML(this.Filename);
		}
		else
		{
			UWPVideoPlayer.OnPauseXAML();
		}
		this.IsPaused = !this.IsPaused;
	}

	// Token: 0x060028DC RID: 10460 RVA: 0x000B1136 File Offset: 0x000AF336
	public void Stop()
	{
		this.IsPlaying = false;
		UWPVideoPlayer.OnStopXAML();
		this.OnPlaybackFinished();
	}

	// Token: 0x060028DD RID: 10461 RVA: 0x000B1154 File Offset: 0x000AF354
	private void XAMLFinished()
	{
		this.IsPlaying = false;
		this.OnPlaybackFinished();
	}

	// Token: 0x17000683 RID: 1667
	// (get) Token: 0x060028DE RID: 10462 RVA: 0x000B1168 File Offset: 0x000AF368
	// (set) Token: 0x060028DF RID: 10463 RVA: 0x000B1170 File Offset: 0x000AF370
	public OnDelegate OnPlaybackFinished { get; set; }

	// Token: 0x17000684 RID: 1668
	// (get) Token: 0x060028E0 RID: 10464 RVA: 0x000B1179 File Offset: 0x000AF379
	// (set) Token: 0x060028E1 RID: 10465 RVA: 0x000B1181 File Offset: 0x000AF381
	public string Filename { get; set; }

	// Token: 0x17000685 RID: 1669
	// (get) Token: 0x060028E2 RID: 10466 RVA: 0x000B118A File Offset: 0x000AF38A
	// (set) Token: 0x060028E3 RID: 10467 RVA: 0x000B1192 File Offset: 0x000AF392
	public bool IsPlaying { get; set; }

	// Token: 0x17000686 RID: 1670
	// (get) Token: 0x060028E4 RID: 10468 RVA: 0x000B119B File Offset: 0x000AF39B
	// (set) Token: 0x060028E5 RID: 10469 RVA: 0x000B11A3 File Offset: 0x000AF3A3
	public bool IsPaused { get; set; }

	// Token: 0x04002471 RID: 9329
	public static UWPVideoPlayer.OnPlayDelegate OnPlayXAML;

	// Token: 0x04002472 RID: 9330
	public static OnDelegate OnPauseXAML;

	// Token: 0x04002473 RID: 9331
	public static OnDelegate OnStopXAML;

	// Token: 0x04002474 RID: 9332
	public static OnDelegate OnXAMLFinished;

	// Token: 0x0200069A RID: 1690
	// (Invoke) Token: 0x060028E7 RID: 10471
	public delegate void OnPlayDelegate(string filename);
}
