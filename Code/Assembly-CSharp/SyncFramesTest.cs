using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class SyncFramesTest : MonoBehaviour
{
	// Token: 0x0600003D RID: 61 RVA: 0x00002F98 File Offset: 0x00001198
	public void ResetSync()
	{
		this.m_timeOffset = Time.time;
		this.m_realTimeOffset = Time.realtimeSinceStartup;
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x0600003E RID: 62 RVA: 0x00002FB0 File Offset: 0x000011B0
	public bool TimeIsScaled
	{
		get
		{
			return Time.timeScale < 0.5f || Time.timeScale > 2f;
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00002FD0 File Offset: 0x000011D0
	public void Update()
	{
		if (this.TimeIsScaled)
		{
			this.ResetSync();
			return;
		}
		float num = Time.time - this.m_timeOffset;
		float num2 = Time.realtimeSinceStartup - this.m_realTimeOffset;
		Time.timeScale = 1f;
		if (num < num2 - 0.16666667f)
		{
			this.ResetSync();
		}
		if (!PhysicsLimitTest.IsSynced)
		{
			return;
		}
		if (SyncFramesTest.EnableSync && num < num2 - 0.05f)
		{
			Time.timeScale = 2f;
		}
		else if (SyncFramesTest.EnabledForceFixedUpdate)
		{
			float num3 = Time.time - Time.fixedTime;
			if (num3 < 0.00416666f)
			{
				Time.timeScale = 1.5f;
			}
			if (num3 > 0.0125f)
			{
				Time.timeScale = 0.5f;
			}
		}
	}

	// Token: 0x04000020 RID: 32
	private float m_timeOffset;

	// Token: 0x04000021 RID: 33
	private float m_realTimeOffset;

	// Token: 0x04000022 RID: 34
	public static bool EnabledForceFixedUpdate = true;

	// Token: 0x04000023 RID: 35
	public static bool EnableSync;
}
