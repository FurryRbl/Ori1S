using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x0200017B RID: 379
public class DeathsPlugin : MonoBehaviour, IRecorderPlugin
{
	// Token: 0x06000EEC RID: 3820 RVA: 0x00044AD8 File Offset: 0x00042CD8
	public void Awake()
	{
		Events.Scheduler.OnPlayerDeath.Add(new Action(this.OnPlayerDeath));
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x00044AF5 File Offset: 0x00042CF5
	public void PlayCycle(int frame)
	{
	}

	// Token: 0x06000EEE RID: 3822 RVA: 0x00044AF7 File Offset: 0x00042CF7
	public void RecordCycle(int frame)
	{
	}

	// Token: 0x06000EEF RID: 3823 RVA: 0x00044AF9 File Offset: 0x00042CF9
	public void Exit()
	{
	}

	// Token: 0x06000EF0 RID: 3824 RVA: 0x00044AFB File Offset: 0x00042CFB
	public void OnPlayerDeath()
	{
		DeathsData.Record(Recorder.Instance.RecorderStream);
	}

	// Token: 0x06000EF1 RID: 3825 RVA: 0x00044B0C File Offset: 0x00042D0C
	public static List<Vector3> ExtractDataFromRecorder(RecorderData recorderData)
	{
		List<Vector3> list = new List<Vector3>();
		foreach (RecorderFrame recorderFrame in recorderData.Frames)
		{
			DeathsData frameDataOfType = recorderFrame.GetFrameDataOfType<DeathsData>();
			if (frameDataOfType != null)
			{
				list.Add(frameDataOfType.Position);
			}
		}
		return list;
	}
}
