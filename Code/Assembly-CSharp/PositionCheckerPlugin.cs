using System;
using Game;
using UnityEngine;

// Token: 0x0200017F RID: 383
public class PositionCheckerPlugin : MonoBehaviour, IRecorderPlugin
{
	// Token: 0x06000F0D RID: 3853 RVA: 0x000450CD File Offset: 0x000432CD
	public void Awake()
	{
		Recorder.Instance.RegisterPlugin(this);
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x000450DC File Offset: 0x000432DC
	public void PlayCycle(int frame)
	{
		CharacterData characterData = new CharacterData();
		CharacterData characterData2 = null;
		foreach (IFrameData frameData in Recorder.Instance.RecorderData.GetFrame(frame).FrameData)
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
		if (Characters.Current as Component == null)
		{
			return;
		}
		bool flag = Recorder.Instance.CorrectWrongPositions && this.m_autoFixFrameCount > 0;
		bool flag2 = false;
		float num = Vector3.Distance(characterData2.Position, characterData.Position);
		if (num > Recorder.Instance.PositionTolerance)
		{
			if (flag)
			{
				flag2 = true;
				this.m_autoFixFrameCount--;
			}
			else
			{
				RecorderPlaybackUI.Instance.Stop();
			}
			if (Recorder.Instance.CorrectWrongPositions)
			{
				Characters.Current.Position = characterData2.Position;
			}
		}
		float num2 = Vector2.Distance(characterData2.Velocity, characterData.Velocity);
		if (num2 > Recorder.Instance.PositionTolerance)
		{
			if (flag)
			{
				flag2 = true;
				this.m_autoFixFrameCount--;
			}
			else
			{
				RecorderPlaybackUI.Instance.Stop();
			}
			if (Recorder.Instance.CorrectWrongPositions)
			{
				Characters.Sein.PlatformBehaviour.PlatformMovement.WorldSpeed = characterData2.Velocity;
			}
		}
		if (!flag2)
		{
			this.m_autoFixFrameCount = 900;
		}
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x00045290 File Offset: 0x00043490
	public void RecordCycle(int frame)
	{
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x00045292 File Offset: 0x00043492
	public void Exit()
	{
		UnityEngine.Object.DestroyObject(this);
	}

	// Token: 0x04000BF3 RID: 3059
	private const int AutoFixMaxTime = 900;

	// Token: 0x04000BF4 RID: 3060
	private int m_autoFixFrameCount = 900;
}
