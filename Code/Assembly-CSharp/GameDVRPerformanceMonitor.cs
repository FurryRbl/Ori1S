using System;
using UnityEngine;

// Token: 0x0200074B RID: 1867
public class GameDVRPerformanceMonitor : MonoBehaviour
{
	// Token: 0x170006F4 RID: 1780
	// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x000BB90F File Offset: 0x000B9B0F
	// (set) Token: 0x06002BC8 RID: 11208 RVA: 0x000BB91C File Offset: 0x000B9B1C
	public static bool Enabled
	{
		get
		{
			return GameDVRPerformanceMonitor.m_instance != null;
		}
		set
		{
			if (GameDVRPerformanceMonitor.m_instance && !value)
			{
				InstantiateUtility.Destroy(GameDVRPerformanceMonitor.m_instance.gameObject);
			}
			if (GameDVRPerformanceMonitor.m_instance == null && value)
			{
				GameObject gameObject = new GameObject("gameDVRPerformanceMonitor");
				gameObject.AddComponent<GameDVRPerformanceMonitor>();
			}
		}
	}

	// Token: 0x06002BC9 RID: 11209 RVA: 0x000BB978 File Offset: 0x000B9B78
	private void Update()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		this.m_frameCount++;
		bool flag = this.m_lastRecordedTime + 8f > realtimeSinceStartup;
		if (realtimeSinceStartup - this.m_lastTime > 4f)
		{
			this.m_lastTime = realtimeSinceStartup;
			float num = (float)this.m_frameCount / 4f;
			if (!flag && num < 55f)
			{
				XboxOneDVRManager.RecordPastDelayed(2f, 2f, "SlowMotion-" + Time.renderedFrameCount);
				this.m_lastRecordedTime = realtimeSinceStartup;
			}
		}
		float num2 = (realtimeSinceStartup - this.m_previousFrameTime) * 1000f;
		if (!flag && num2 > 60f)
		{
			XboxOneDVRManager.RecordPastDelayed(2f, 2f, "Spike-" + Time.renderedFrameCount);
			this.m_lastRecordedTime = realtimeSinceStartup;
		}
		this.m_previousFrameTime = realtimeSinceStartup;
	}

	// Token: 0x04002775 RID: 10101
	private const float FPS_INTERVAL = 4f;

	// Token: 0x04002776 RID: 10102
	private static GameDVRPerformanceMonitor m_instance;

	// Token: 0x04002777 RID: 10103
	private int m_frameCount;

	// Token: 0x04002778 RID: 10104
	private float m_lastTime;

	// Token: 0x04002779 RID: 10105
	private float m_lastRecordedTime;

	// Token: 0x0400277A RID: 10106
	private float m_previousFrameTime;
}
