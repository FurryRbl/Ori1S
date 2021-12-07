using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class AnimatorDriver
{
	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06000439 RID: 1081 RVA: 0x000119CD File Offset: 0x0000FBCD
	// (set) Token: 0x0600043A RID: 1082 RVA: 0x000119D5 File Offset: 0x0000FBD5
	public bool IsPlaying
	{
		get
		{
			return this.m_isPlaying;
		}
		set
		{
			this.m_isPlaying = value;
		}
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x0600043B RID: 1083 RVA: 0x000119DE File Offset: 0x0000FBDE
	public float Duration
	{
		get
		{
			return this.Animator.Duration;
		}
	}

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x0600043C RID: 1084 RVA: 0x000119EB File Offset: 0x0000FBEB
	public bool IsReversed
	{
		get
		{
			return this.Speed < 0f;
		}
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x0600043D RID: 1085 RVA: 0x000119FA File Offset: 0x0000FBFA
	// (set) Token: 0x0600043E RID: 1086 RVA: 0x00011A02 File Offset: 0x0000FC02
	public float CurrentTime
	{
		get
		{
			return this.m_time;
		}
		set
		{
			this.m_time = value;
		}
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x00011A0C File Offset: 0x0000FC0C
	public void Restart()
	{
		this.IsPlaying = true;
		this.m_time = ((this.Speed <= 0f) ? this.Duration : 0f);
		this.Animator.SampleValue(this.m_time, true);
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x00011A58 File Offset: 0x0000FC58
	public void RestartForward()
	{
		this.SetForward();
		this.Restart();
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x00011A66 File Offset: 0x0000FC66
	public void RestartBackwards()
	{
		this.SetBackwards();
		this.Restart();
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x00011A74 File Offset: 0x0000FC74
	public void Stop()
	{
		this.IsPlaying = false;
		this.m_time = ((this.Speed <= 0f) ? this.Duration : 0f);
		this.Animator.SampleValue(this.m_time, true);
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x00011AC0 File Offset: 0x0000FCC0
	public void Pause()
	{
		this.IsPlaying = false;
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x00011AC9 File Offset: 0x0000FCC9
	public void Resume()
	{
		this.IsPlaying = true;
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00011AD2 File Offset: 0x0000FCD2
	public void Reverse()
	{
		this.Speed *= -1f;
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x00011AE6 File Offset: 0x0000FCE6
	public void SetForward()
	{
		this.Speed = Mathf.Abs(this.Speed);
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x00011AF9 File Offset: 0x0000FCF9
	public void SetBackwards()
	{
		this.Speed = -Mathf.Abs(this.Speed);
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x00011B0D File Offset: 0x0000FD0D
	public void ContinueForward()
	{
		this.SetForward();
		this.Resume();
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x00011B1B File Offset: 0x0000FD1B
	public void ContinueBackwards()
	{
		this.SetBackwards();
		this.Resume();
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x00011B29 File Offset: 0x0000FD29
	public void GoToStart()
	{
		this.m_time = 0f;
		this.Animator.SampleValue(this.m_time, true);
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00011B48 File Offset: 0x0000FD48
	public void GoToEnd()
	{
		this.m_time = this.Duration;
		this.Animator.SampleValue(this.m_time, true);
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x00011B68 File Offset: 0x0000FD68
	public void FixedUpdate()
	{
		if (this.m_isPlaying)
		{
			this.m_time += Time.deltaTime * this.Speed;
			if (!this.Animator.IsLooping)
			{
				if (this.m_time >= this.Duration && this.Speed > 0f)
				{
					this.IsPlaying = false;
					this.m_time = this.Duration;
				}
				if (this.m_time < 0f && this.Speed < 0f)
				{
					this.IsPlaying = false;
					this.m_time = 0f;
				}
			}
			this.Sample();
		}
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x00011C15 File Offset: 0x0000FE15
	public void OnPoolSpawned()
	{
		this.m_time = 0f;
		this.Speed = 1f;
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x00011C2D File Offset: 0x0000FE2D
	public void Sample()
	{
		this.Animator.SampleValue(this.m_time, false);
	}

	// Token: 0x0400038A RID: 906
	public bool PlayOnStart;

	// Token: 0x0400038B RID: 907
	private bool m_isPlaying;

	// Token: 0x0400038C RID: 908
	public BaseAnimator Animator;

	// Token: 0x0400038D RID: 909
	public float Speed = 1f;

	// Token: 0x0400038E RID: 910
	private float m_time;
}
