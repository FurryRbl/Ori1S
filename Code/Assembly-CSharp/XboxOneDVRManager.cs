using System;
using UnityEngine;

// Token: 0x0200024A RID: 586
public class XboxOneDVRManager : MonoBehaviour
{
	// Token: 0x060013F1 RID: 5105 RVA: 0x0005B133 File Offset: 0x00059333
	public void Awake()
	{
		XboxOneDVRManager.Instance = this;
	}

	// Token: 0x060013F2 RID: 5106 RVA: 0x0005B13B File Offset: 0x0005933B
	public void OnDestroy()
	{
		XboxOneDVRManager.Instance = null;
	}

	// Token: 0x060013F3 RID: 5107 RVA: 0x0005B144 File Offset: 0x00059344
	public static void RecordPastDelayed(float delay, float past, string clip)
	{
		if (XboxOneDVRManager.Instance)
		{
			XboxOneDVRManager.Instance.m_time = delay;
			XboxOneDVRManager.Instance.m_past = past + delay;
			if (XboxOneDVRManager.Instance.m_past > 300f)
			{
				XboxOneDVRManager.Instance.m_past = 300f;
			}
			XboxOneDVRManager.Instance.m_clipName = clip;
		}
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x0005B1A8 File Offset: 0x000593A8
	public void Update()
	{
		if (this.m_time == 0f)
		{
			return;
		}
		this.m_time -= Time.deltaTime;
		if (this.m_time <= 0f)
		{
			this.m_time = 0f;
			XboxOneDVR.RecordPast(this.m_past, this.m_clipName, false);
		}
	}

	// Token: 0x0400118F RID: 4495
	public static XboxOneDVRManager Instance;

	// Token: 0x04001190 RID: 4496
	private float m_time;

	// Token: 0x04001191 RID: 4497
	private float m_past;

	// Token: 0x04001192 RID: 4498
	private string m_clipName;
}
