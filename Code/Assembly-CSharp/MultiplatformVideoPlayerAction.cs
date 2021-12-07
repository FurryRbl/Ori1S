using System;
using UnityEngine;

// Token: 0x02000698 RID: 1688
public class MultiplatformVideoPlayerAction : PerformingAction
{
	// Token: 0x060028D6 RID: 10454 RVA: 0x000B1027 File Offset: 0x000AF227
	public override void Stop()
	{
		if (this.m_multiplatformVideoPlayer)
		{
			this.m_multiplatformVideoPlayer.Stop();
		}
	}

	// Token: 0x17000682 RID: 1666
	// (get) Token: 0x060028D7 RID: 10455 RVA: 0x000B1044 File Offset: 0x000AF244
	public override bool IsPerforming
	{
		get
		{
			return this.m_multiplatformVideoPlayer != null;
		}
	}

	// Token: 0x060028D8 RID: 10456 RVA: 0x000B1054 File Offset: 0x000AF254
	public override void Perform(IContext context)
	{
		GameObject gameObject = InstantiateUtility.Instantiate(this.MultiplatformVideoPlayer) as GameObject;
		this.m_multiplatformVideoPlayer = gameObject.GetComponent<MultiplatformVideoPlayer>();
		this.m_multiplatformVideoPlayer.Filename = this.Filename;
		this.m_multiplatformVideoPlayer.AudioTrack = this.AudioTrack;
		this.m_multiplatformVideoPlayer.Perform();
	}

	// Token: 0x0400246D RID: 9325
	public string Filename;

	// Token: 0x0400246E RID: 9326
	public GameObject MultiplatformVideoPlayer;

	// Token: 0x0400246F RID: 9327
	public AudioClip AudioTrack;

	// Token: 0x04002470 RID: 9328
	private MultiplatformVideoPlayer m_multiplatformVideoPlayer;
}
