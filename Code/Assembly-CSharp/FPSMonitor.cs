using System;
using UnityEngine;

// Token: 0x0200074C RID: 1868
public class FPSMonitor : MonoBehaviour
{
	// Token: 0x06002BCB RID: 11211 RVA: 0x000BBA72 File Offset: 0x000B9C72
	private void Start()
	{
		this.m_maxFrameTime = -10000f;
		this.m_renderedFrames = 0;
		this.m_lastTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06002BCC RID: 11212 RVA: 0x000BBA94 File Offset: 0x000B9C94
	private void Update()
	{
		if (this.m_lastTime == 0f)
		{
			this.m_lastTime = Time.realtimeSinceStartup;
			return;
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - this.m_lastTime;
		this.m_lastTime = realtimeSinceStartup;
		this.m_timePassed += num;
		this.m_currentSampleTime += num;
		this.m_maxFrameTime = Mathf.Max(num, this.m_maxFrameTime);
		this.m_renderedFrames++;
		if (this.m_currentSampleTime > 1f)
		{
			this.AverageFPS = (int)((float)this.m_renderedFrames / this.m_timePassed);
			this.MinimumFPS = (int)(1f / this.m_maxFrameTime);
			this.m_currentSampleTime = 0f;
			this.m_maxFrameTime = 0f;
		}
	}

	// Token: 0x06002BCD RID: 11213 RVA: 0x000BBB60 File Offset: 0x000B9D60
	public void Reset()
	{
		this.AverageFPS = 0;
		this.MinimumFPS = 0;
		this.m_timePassed = 0f;
		this.m_currentSampleTime = 0f;
		this.m_renderedFrames = 0;
		this.m_maxFrameTime = -10000f;
		this.m_lastTime = 0f;
	}

	// Token: 0x0400277B RID: 10107
	private int m_renderedFrames;

	// Token: 0x0400277C RID: 10108
	private float m_lastTime;

	// Token: 0x0400277D RID: 10109
	private float m_timePassed;

	// Token: 0x0400277E RID: 10110
	private float m_currentSampleTime;

	// Token: 0x0400277F RID: 10111
	private float m_maxFrameTime;

	// Token: 0x04002780 RID: 10112
	public int AverageFPS = -1;

	// Token: 0x04002781 RID: 10113
	public int MinimumFPS = -1;
}
