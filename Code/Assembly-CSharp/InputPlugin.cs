using System;
using Core;
using UnityEngine;

// Token: 0x02000177 RID: 375
public class InputPlugin : MonoBehaviour, IRecorderPlugin
{
	// Token: 0x06000ECE RID: 3790 RVA: 0x00044199 File Offset: 0x00042399
	public void Awake()
	{
		Recorder.Instance.RegisterPlugin(this);
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x000441A6 File Offset: 0x000423A6
	public void OnDestroy()
	{
		Recorder.Instance.DeregisterPlugin(this);
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x000441B4 File Offset: 0x000423B4
	public void Apply(InputData inputData)
	{
		this.m_inputData = inputData;
		Core.Input.HorizontalDigiPad = inputData.HorizontalDigiPad;
		Core.Input.VerticalDigiPad = inputData.VerticalDigiPad;
		Core.Input.Jump.Update(inputData.Jump);
		Core.Input.SpiritFlame.Update(inputData.SpiritFlame);
		Core.Input.SoulFlame.Update(inputData.SoulFlame);
		Core.Input.Bash.Update(inputData.Bash);
		Core.Input.ChargeJump.Update(inputData.ChargeJump);
		Core.Input.Glide.Update(inputData.Glide);
		Core.Input.Grab.Update(inputData.Grab);
		Core.Input.LeftShoulder.Update(inputData.LeftShoulder);
		Core.Input.RightShoulder.Update(inputData.RightShoulder);
		Core.Input.Select.Update(inputData.Select);
		Core.Input.Start.Update(inputData.Start);
		Core.Input.AnyStart.Update(inputData.AnyStart);
		Core.Input.LeftStick.Update(inputData.LeftStick);
		Core.Input.RightStick.Update(inputData.RightStick);
		Core.Input.MenuDown.Update(inputData.MenuDown);
		Core.Input.MenuUp.Update(inputData.MenuUp);
		Core.Input.MenuLeft.Update(inputData.MenuLeft);
		Core.Input.MenuRight.Update(inputData.MenuRight);
		Core.Input.MenuPageLeft.Update(inputData.MenuPageLeft);
		Core.Input.MenuPageRight.Update(inputData.MenuPageRight);
		Core.Input.ActionButtonA.Update(inputData.ActionButtonA);
		Core.Input.Cancel.Update(inputData.Cancel);
		Core.Input.LeftClick.Update(inputData.LeftClick);
		Core.Input.RightClick.Update(inputData.RightClick);
		Core.Input.ZoomIn.Update(inputData.ZoomIn);
		Core.Input.ZoomOut.Update(inputData.ZoomOut);
		Core.Input.CursorMoved = this.m_cursorMoved;
		Core.Input.CursorPosition = this.m_cursorInputData.Position;
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x00044399 File Offset: 0x00042599
	public void Apply(AnalogueInputData inputData)
	{
		this.m_analogueInputData = inputData;
		Core.Input.HorizontalAnalogLeft = inputData.HorizontalAnalogLeft;
		Core.Input.VerticalAnalogLeft = inputData.VerticalAnalogLeft;
		Core.Input.HorizontalAnalogRight = inputData.HorizontalAnalogRight;
		Core.Input.VerticalAnalogRight = inputData.VerticalAnalogRight;
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x000443D0 File Offset: 0x000425D0
	public void PlayCycle(int frame)
	{
		RecorderFrame currentFrame = Recorder.Instance.CurrentFrame;
		InputData frameDataOfType = currentFrame.GetFrameDataOfType<InputData>();
		if (frameDataOfType != null)
		{
			this.m_inputData = frameDataOfType;
		}
		this.Apply(this.m_inputData);
		AnalogueInputData frameDataOfType2 = currentFrame.GetFrameDataOfType<AnalogueInputData>();
		if (frameDataOfType2 != null)
		{
			this.m_analogueInputData = frameDataOfType2;
		}
		this.Apply(this.m_analogueInputData);
		CursorInputData frameDataOfType3 = currentFrame.GetFrameDataOfType<CursorInputData>();
		if (frameDataOfType3 != null)
		{
			this.m_cursorInputData = frameDataOfType3;
			this.m_cursorMoved = true;
		}
		else
		{
			this.m_cursorMoved = false;
		}
		PlayerInput.Instance.RefreshControls();
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x0004445C File Offset: 0x0004265C
	public void RecordCycle(int frame)
	{
		if (this.m_inputData.UpdateInputs())
		{
			InputData.Record(Recorder.Instance.RecorderStream);
		}
		if (Core.Input.CursorMoved)
		{
			CursorInputData.Record(Recorder.Instance.RecorderStream);
		}
		if (this.m_analogueInputData.UpdateInputs())
		{
			AnalogueInputData.Record(Recorder.Instance.RecorderStream);
		}
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x000444C0 File Offset: 0x000426C0
	public void Exit()
	{
		UnityEngine.Object.DestroyObject(this);
	}

	// Token: 0x04000BD8 RID: 3032
	private InputData m_inputData = new InputData();

	// Token: 0x04000BD9 RID: 3033
	private AnalogueInputData m_analogueInputData = new AnalogueInputData();

	// Token: 0x04000BDA RID: 3034
	private CursorInputData m_cursorInputData = new CursorInputData();

	// Token: 0x04000BDB RID: 3035
	private bool m_cursorMoved;
}
