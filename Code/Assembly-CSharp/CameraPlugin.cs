using System;
using UnityEngine;

// Token: 0x02000179 RID: 377
public class CameraPlugin : MonoBehaviour, IRecorderPlugin
{
	// Token: 0x06000EDC RID: 3804 RVA: 0x00044588 File Offset: 0x00042788
	public void Awake()
	{
		Recorder.Instance.RegisterPlugin(this);
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x00044595 File Offset: 0x00042795
	public void PlayCycle(int frame)
	{
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x00044597 File Offset: 0x00042797
	public void RecordCycle(int frame)
	{
		CameraData.Record(Recorder.Instance.RecorderStream);
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x000445A8 File Offset: 0x000427A8
	public void Exit()
	{
		UnityEngine.Object.DestroyObject(this);
	}
}
