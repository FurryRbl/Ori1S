using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x02000178 RID: 376
public class CharacterPlugin : MonoBehaviour, IRecorderPlugin
{
	// Token: 0x06000ED6 RID: 3798 RVA: 0x000444D0 File Offset: 0x000426D0
	public void Awake()
	{
		Recorder.Instance.RegisterPlugin(this);
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x000444DD File Offset: 0x000426DD
	public void PlayCycle(int frame)
	{
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x000444DF File Offset: 0x000426DF
	public void RecordCycle(int frame)
	{
		if (Characters.Current as Component)
		{
			CharacterData.Record(Recorder.Instance.RecorderStream);
		}
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x00044504 File Offset: 0x00042704
	public void Exit()
	{
		UnityEngine.Object.DestroyObject(this);
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x0004450C File Offset: 0x0004270C
	public static List<Vector3> ExtractDataFromRecorder(RecorderData recorderData)
	{
		List<Vector3> list = new List<Vector3>();
		foreach (RecorderFrame recorderFrame in recorderData.Frames)
		{
			CharacterData frameDataOfType = recorderFrame.GetFrameDataOfType<CharacterData>();
			if (frameDataOfType != null)
			{
				list.Add(frameDataOfType.Position);
			}
		}
		return list;
	}
}
