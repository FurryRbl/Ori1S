using System;
using UnityEngine;

// Token: 0x0200069B RID: 1691
public class XboxOneVideoPlayer : MonoBehaviour, IVideoPlayer
{
	// Token: 0x060028EB RID: 10475 RVA: 0x000B11CC File Offset: 0x000AF3CC
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		GL.Clear(true, true, Color.black);
		if (this.m_Texture == null)
		{
			Graphics.Blit(source, destination);
		}
		else if (this.m_frame > 2)
		{
			Graphics.Blit(this.m_Texture, destination, this.Material);
		}
		this.m_frame++;
	}

	// Token: 0x060028EC RID: 10476 RVA: 0x000B122E File Offset: 0x000AF42E
	private void Update()
	{
	}

	// Token: 0x17000687 RID: 1671
	// (set) Token: 0x060028ED RID: 10477 RVA: 0x000B1230 File Offset: 0x000AF430
	public string Filename
	{
		set
		{
			this.m_Filename = value + ".mp4";
		}
	}

	// Token: 0x17000688 RID: 1672
	// (get) Token: 0x060028EE RID: 10478 RVA: 0x000B1243 File Offset: 0x000AF443
	public bool IsPlaying
	{
		get
		{
			return this.m_IsPlaying;
		}
	}

	// Token: 0x17000689 RID: 1673
	// (get) Token: 0x060028EF RID: 10479 RVA: 0x000B124B File Offset: 0x000AF44B
	public bool IsPaused
	{
		get
		{
			return this.m_IsPaused;
		}
	}

	// Token: 0x1700068A RID: 1674
	// (get) Token: 0x060028F0 RID: 10480 RVA: 0x000B1253 File Offset: 0x000AF453
	// (set) Token: 0x060028F1 RID: 10481 RVA: 0x000B125B File Offset: 0x000AF45B
	public OnDelegate OnPlaybackFinished { get; set; }

	// Token: 0x060028F2 RID: 10482 RVA: 0x000B1264 File Offset: 0x000AF464
	public void Play()
	{
	}

	// Token: 0x060028F3 RID: 10483 RVA: 0x000B1266 File Offset: 0x000AF466
	public void Pause()
	{
	}

	// Token: 0x060028F4 RID: 10484 RVA: 0x000B1268 File Offset: 0x000AF468
	public void Stop()
	{
	}

	// Token: 0x04002479 RID: 9337
	public Material Material;

	// Token: 0x0400247A RID: 9338
	[HideInInspector]
	public int MovieWidth = 1280;

	// Token: 0x0400247B RID: 9339
	[HideInInspector]
	public int MovieHeight = 720;

	// Token: 0x0400247C RID: 9340
	[HideInInspector]
	public AudioClip AudioTrack;

	// Token: 0x0400247D RID: 9341
	private bool m_IssuePluginEvent;

	// Token: 0x0400247E RID: 9342
	private string m_Filename;

	// Token: 0x0400247F RID: 9343
	private Texture2D m_Texture;

	// Token: 0x04002480 RID: 9344
	private SoundPlayer m_MusicTrack;

	// Token: 0x04002481 RID: 9345
	private bool m_IsPlaying;

	// Token: 0x04002482 RID: 9346
	private bool m_IsPaused;

	// Token: 0x04002483 RID: 9347
	private int m_frame;
}
