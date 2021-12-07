using System;

// Token: 0x02000694 RID: 1684
public interface IVideoPlayer
{
	// Token: 0x060028B4 RID: 10420
	void Play();

	// Token: 0x060028B5 RID: 10421
	void Pause();

	// Token: 0x060028B6 RID: 10422
	void Stop();

	// Token: 0x1700067A RID: 1658
	// (get) Token: 0x060028B7 RID: 10423
	// (set) Token: 0x060028B8 RID: 10424
	OnDelegate OnPlaybackFinished { get; set; }

	// Token: 0x1700067B RID: 1659
	// (set) Token: 0x060028B9 RID: 10425
	string Filename { set; }

	// Token: 0x1700067C RID: 1660
	// (get) Token: 0x060028BA RID: 10426
	bool IsPlaying { get; }

	// Token: 0x1700067D RID: 1661
	// (get) Token: 0x060028BB RID: 10427
	bool IsPaused { get; }
}
