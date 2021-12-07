using System;
using UnityEngine;

// Token: 0x0200097C RID: 2428
public class KillCounter : MonoBehaviour
{
	// Token: 0x1700085C RID: 2140
	// (get) Token: 0x0600353A RID: 13626 RVA: 0x000DF1CC File Offset: 0x000DD3CC
	public int KillCount
	{
		get
		{
			return SeinDeathCounter.Count;
		}
	}

	// Token: 0x0600353B RID: 13627 RVA: 0x000DF1D4 File Offset: 0x000DD3D4
	private void FixedUpdate()
	{
		if (this.KillCount != this.m_lastKillCount)
		{
			base.GetComponent<GUIText>().text = this.KillCount.ToString();
			this.m_lastKillCount = this.KillCount;
		}
	}

	// Token: 0x04002FD5 RID: 12245
	private int m_lastKillCount;
}
