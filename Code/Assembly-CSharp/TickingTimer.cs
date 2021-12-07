using System;
using System.Collections;
using UnityEngine;

// Token: 0x020008DB RID: 2267
public class TickingTimer : MonoBehaviour
{
	// Token: 0x1700080C RID: 2060
	// (get) Token: 0x06003296 RID: 12950 RVA: 0x000D57D7 File Offset: 0x000D39D7
	public bool isTimedOut
	{
		get
		{
			return this.m_time >= this.Timeout;
		}
	}

	// Token: 0x06003297 RID: 12951 RVA: 0x000D57EA File Offset: 0x000D39EA
	public void RestartTimer()
	{
		this.m_time = 0f;
		this.m_lastTickTime = 0f;
		base.StopCoroutine("TimerRoutine");
		base.StartCoroutine("TimerRoutine");
	}

	// Token: 0x06003298 RID: 12952 RVA: 0x000D581C File Offset: 0x000D3A1C
	private IEnumerator TimerRoutine()
	{
		while (!this.isTimedOut)
		{
			float tickFrequency = Mathf.Max(this.MinTickFrequency, this.StartingTickFrequency * ((this.Timeout - this.m_time) / this.Timeout));
			if (this.m_time - this.m_lastTickTime > tickFrequency)
			{
				this.m_shouldTick = !this.m_shouldTick;
				this.m_lastTickTime = this.m_time;
			}
			this.m_time += Time.deltaTime;
			yield return null;
		}
		yield break;
	}

	// Token: 0x06003299 RID: 12953 RVA: 0x000D5837 File Offset: 0x000D3A37
	public void StopTimer()
	{
		base.StopCoroutine("TimerRoutine");
	}

	// Token: 0x04002D74 RID: 11636
	public AudioClip TickSound;

	// Token: 0x04002D75 RID: 11637
	public AudioClip TockSound;

	// Token: 0x04002D76 RID: 11638
	public float Timeout = 10f;

	// Token: 0x04002D77 RID: 11639
	public float StartingTickFrequency = 1f;

	// Token: 0x04002D78 RID: 11640
	public float MinTickFrequency = 1f;

	// Token: 0x04002D79 RID: 11641
	private float m_time;

	// Token: 0x04002D7A RID: 11642
	private bool m_shouldTick = true;

	// Token: 0x04002D7B RID: 11643
	private float m_lastTickTime;
}
