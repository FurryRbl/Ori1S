using System;

// Token: 0x02000697 RID: 1687
public class WindowsVideoPlayer : AVProWindowsMediaMovie, IVideoPlayer
{
	// Token: 0x1700067E RID: 1662
	// (set) Token: 0x060028CB RID: 10443 RVA: 0x000B0F58 File Offset: 0x000AF158
	public string Filename
	{
		set
		{
			this._useStreamingAssetsPath = true;
			this._folder = string.Empty;
			this._filename = value + ".avi";
			this.LoadMovie(false);
		}
	}

	// Token: 0x1700067F RID: 1663
	// (get) Token: 0x060028CC RID: 10444 RVA: 0x000B0F85 File Offset: 0x000AF185
	// (set) Token: 0x060028CD RID: 10445 RVA: 0x000B0F8D File Offset: 0x000AF18D
	public OnDelegate OnPlaybackFinished { get; set; }

	// Token: 0x17000680 RID: 1664
	// (get) Token: 0x060028CE RID: 10446 RVA: 0x000B0F96 File Offset: 0x000AF196
	public bool IsPlaying
	{
		get
		{
			return this._moviePlayer != null && this._moviePlayer.IsPlaying;
		}
	}

	// Token: 0x17000681 RID: 1665
	// (get) Token: 0x060028CF RID: 10447 RVA: 0x000B0FB0 File Offset: 0x000AF1B0
	// (set) Token: 0x060028D0 RID: 10448 RVA: 0x000B0FB8 File Offset: 0x000AF1B8
	public bool IsPaused { get; set; }

	// Token: 0x060028D1 RID: 10449 RVA: 0x000B0FC1 File Offset: 0x000AF1C1
	public new void Play()
	{
		base.Play();
	}

	// Token: 0x060028D2 RID: 10450 RVA: 0x000B0FCC File Offset: 0x000AF1CC
	public new void Pause()
	{
		if (this.IsPaused)
		{
			base.Play();
		}
		else
		{
			base.Pause();
		}
		this.IsPaused = !this.IsPaused;
	}

	// Token: 0x060028D3 RID: 10451 RVA: 0x000B1004 File Offset: 0x000AF204
	public void MovieFinished()
	{
		this.OnPlaybackFinished();
	}

	// Token: 0x060028D4 RID: 10452 RVA: 0x000B1011 File Offset: 0x000AF211
	public void Stop()
	{
		base.UnloadMovie();
		this.MovieFinished();
	}
}
