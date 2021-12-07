using System;
using UnityEngine;

// Token: 0x0200017E RID: 382
public class TesterPlugin : MonoBehaviour, IRecorderPlugin
{
	// Token: 0x06000F08 RID: 3848 RVA: 0x00044FB5 File Offset: 0x000431B5
	public void Awake()
	{
		Recorder.Instance.RegisterPlugin(this);
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x00044FC4 File Offset: 0x000431C4
	public void PlayCycle(int frame)
	{
		if (!TestSetManager.IsPerformingTests)
		{
			Recorder.Instance.DeregisterPlugin(this);
			return;
		}
		if (Recorder.Instance.RecorderData.Frames.Count == frame + 1)
		{
			TestSetManager.FinishedTest(true);
		}
		CharacterData characterData = new CharacterData();
		CharacterData characterData2 = null;
		foreach (IFrameData frameData in Recorder.Instance.RecorderData.Frames[frame].FrameData)
		{
			if (frameData is CharacterData)
			{
				characterData2 = (frameData as CharacterData);
				break;
			}
		}
		if (characterData2 == null)
		{
			return;
		}
		float num = Vector3.Distance(characterData2.Position, characterData.Position);
		if (num > 0.0001f)
		{
			TestSetManager.FinishedTest(false);
		}
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x000450B0 File Offset: 0x000432B0
	public void RecordCycle(int frame)
	{
	}

	// Token: 0x06000F0B RID: 3851 RVA: 0x000450B2 File Offset: 0x000432B2
	public void Exit()
	{
		UnityEngine.Object.DestroyObject(this);
	}
}
