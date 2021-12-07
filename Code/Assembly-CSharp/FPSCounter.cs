using System;
using UnityEngine;

// Token: 0x02000496 RID: 1174
[RequireComponent(typeof(GUIText))]
public class FPSCounter : MonoBehaviour
{
	// Token: 0x06001FCF RID: 8143 RVA: 0x0008BB06 File Offset: 0x00089D06
	private void Start()
	{
		base.GetComponent<GUIText>().text = "Unknown";
		this.m_maxFrameTime = -10000f;
		this.m_renderedFrames = 0f;
		this.m_lastTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06001FD0 RID: 8144 RVA: 0x0008BB39 File Offset: 0x00089D39
	private void FixedUpdate()
	{
		this.m_fixedUpdateHappened = true;
	}

	// Token: 0x06001FD1 RID: 8145 RVA: 0x0008BB44 File Offset: 0x00089D44
	private void Update()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - this.m_lastTime;
		this.m_lastTime = realtimeSinceStartup;
		if (this.m_fixedUpdateHappened)
		{
			this.m_fixedUpdateHappened = false;
		}
		if (this.m_visible)
		{
			this.m_timePassed += num;
			this.m_maxFrameTime = Mathf.Max(num, this.m_maxFrameTime);
			this.m_renderedFrames += 1f;
			if (this.m_timePassed > this.UpdateInterval || this.m_isSynced != PhysicsLimitTest.IsSynced)
			{
				this.m_isSynced = PhysicsLimitTest.IsSynced;
				this.AverageFPS = (int)(this.m_renderedFrames / this.m_timePassed);
				this.MinimumFPS = (int)(1f / this.m_maxFrameTime);
				int num2 = (int)(this.m_maxFrameTime * 1000f);
				base.GetComponent<GUIText>().text = string.Concat(new string[]
				{
					" ( ",
					num2.ToString(),
					" ms ) ",
					this.MinimumFPS.ToString(),
					" / ",
					this.AverageFPS.ToString(),
					(!this.m_isSynced) ? " (no sync)" : string.Empty
				});
				this.m_timePassed = 0f;
				this.m_renderedFrames = 0f;
				this.m_maxFrameTime = -10000f;
			}
		}
		else
		{
			base.GetComponent<GUIText>().text = string.Empty;
		}
	}

	// Token: 0x04001B66 RID: 7014
	public float UpdateInterval = 0.5f;

	// Token: 0x04001B67 RID: 7015
	private float m_renderedFrames;

	// Token: 0x04001B68 RID: 7016
	private float m_lastTime;

	// Token: 0x04001B69 RID: 7017
	private float m_timePassed;

	// Token: 0x04001B6A RID: 7018
	private bool m_visible = true;

	// Token: 0x04001B6B RID: 7019
	private float m_maxFrameTime;

	// Token: 0x04001B6C RID: 7020
	public int AverageFPS;

	// Token: 0x04001B6D RID: 7021
	public int MinimumFPS;

	// Token: 0x04001B6E RID: 7022
	private bool m_fixedUpdateHappened;

	// Token: 0x04001B6F RID: 7023
	private bool m_isSynced;
}
