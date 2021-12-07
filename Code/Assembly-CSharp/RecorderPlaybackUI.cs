using System;
using System.Collections;
using System.IO;
using Core;
using SmartInput;
using UnityEngine;

// Token: 0x02000183 RID: 387
public class RecorderPlaybackUI : MonoBehaviour
{
	// Token: 0x170002C4 RID: 708
	// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00045B7F File Offset: 0x00043D7F
	public Recorder Recorder
	{
		get
		{
			return this.Timeline.Recorder;
		}
	}

	// Token: 0x170002C5 RID: 709
	// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00045B8C File Offset: 0x00043D8C
	// (set) Token: 0x06000F36 RID: 3894 RVA: 0x00045B94 File Offset: 0x00043D94
	public bool DoBlast
	{
		get
		{
			return this.m_isBlast;
		}
		set
		{
			if (this.m_isBlast == value)
			{
				return;
			}
			this.m_isBlast = value;
			this.UpdateTimeScale();
		}
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x00045BB0 File Offset: 0x00043DB0
	private void FixedUpdate()
	{
		if (this.m_shouldExit)
		{
			UnityEngine.Object.DestroyObject(this.Timeline);
			InstantiateUtility.Destroy(base.gameObject);
			Recorder.Instance.Exit();
		}
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x00045BE0 File Offset: 0x00043DE0
	private void Update()
	{
		if (this.m_shouldExit)
		{
			return;
		}
		this.PlayPauseButton.Update(this.m_togglePlayPauseButtonInput.GetButton());
		this.BlastButton.Update(this.m_blastButtonInput.GetButton());
		this.BreakButton.Update(this.m_breakButtonInput.GetButton());
		this.VisibilityButton.Update(this.m_toggleVisibilityButtonInput.GetButton());
		RecorderPlaybackUI.LeftTrigger.Update(this.m_leftTrigger.GetButton());
		RecorderPlaybackUI.RightTrigger.Update(this.m_rightTrigger.GetButton());
		if (this.PlayPauseButton.OnPressed)
		{
			this.TogglePlaying();
		}
		if (this.VisibilityButton.OnPressed)
		{
			this.ToggleTimeline();
		}
		if (this.BreakButton.OnPressed)
		{
			this.DoExit();
		}
		if (RecorderPlaybackUI.LeftTrigger.OnPressed)
		{
			this.DecreaseSpeed();
		}
		this.DoBlast = this.BlastButton.IsPressed;
		if (RecorderPlaybackUI.RightTrigger.OnPressed)
		{
			if (this.DoBlast)
			{
				this.m_speed = 128;
			}
			this.IncreaseSpeed();
		}
		if (this.FrameToStopAt != 0)
		{
			if (this.m_speed != 60)
			{
				this.m_speed = 60;
				this.UpdateTimeScale();
			}
			if (this.Recorder.CurrentFrameIndex >= this.FrameToStopAt)
			{
				this.m_speed = 0;
				this.UpdateTimeScale();
				this.FrameToStopAt = 0;
				this.IsJumpingToFrame = false;
				if (this.ShouldStopAfterFrameToStopAt)
				{
					this.Stop();
				}
			}
		}
		if (MoonInput.GetKeyDown(KeyCode.JoystickButton4))
		{
			this.PreviousKeyframe();
		}
		if (MoonInput.GetKeyDown(KeyCode.JoystickButton5))
		{
			this.NextKeyframe();
		}
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x00045DA0 File Offset: 0x00043FA0
	public void NextKeyframe()
	{
		int num = this.Timeline.KeyframeForward();
		if (num == -1)
		{
			return;
		}
		this.GoToKeyframe(num);
		this.Play();
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x00045DD0 File Offset: 0x00043FD0
	public void PreviousKeyframe()
	{
		int num = this.Timeline.KeyframeBack();
		if (num == -1)
		{
			return;
		}
		this.GoToKeyframe(num);
		this.Stop();
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x00045E00 File Offset: 0x00044000
	private void GoToKeyframe(int keyframe)
	{
		UnityEngine.Object x = UnityEngine.Object.FindObjectOfType(typeof(Fader));
		if (x != null)
		{
			return;
		}
		if (GameController.Instance.InputLocked)
		{
			return;
		}
		CheckpointPlugin component = this.Recorder.GetComponent<CheckpointPlugin>();
		if (component)
		{
			component.PerformLoad(keyframe);
		}
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x00045E58 File Offset: 0x00044058
	public void SetSpeed(int speed)
	{
		this.m_speed = speed;
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x00045E61 File Offset: 0x00044061
	public int GetSpeed()
	{
		return this.m_speed;
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x00045E69 File Offset: 0x00044069
	public void TogglePlaying()
	{
		this.m_isStopped = !this.m_isStopped;
		this.UpdateTimeScale();
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x00045E80 File Offset: 0x00044080
	public void Play()
	{
		this.m_isStopped = false;
		this.UpdateTimeScale();
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x00045E8F File Offset: 0x0004408F
	public void Stop()
	{
		this.m_isStopped = true;
		this.UpdateTimeScale();
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x00045E9E File Offset: 0x0004409E
	private void ToggleTimeline()
	{
		this.m_hideTimeline = !this.m_hideTimeline;
		this.Timeline.enabled = !this.m_hideTimeline;
		this.HelpTexture.SetActive(!this.m_hideTimeline);
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x00045ED7 File Offset: 0x000440D7
	public bool IsUIVisible()
	{
		return !this.m_hideTimeline;
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x00045EE2 File Offset: 0x000440E2
	public void DoExit()
	{
		if (this.Recorder.State == Recorder.RecorderState.Playing)
		{
			this.m_speed = 0;
			this.UpdateTimeScale();
			this.m_shouldExit = true;
		}
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x00045F09 File Offset: 0x00044109
	private void IncreaseSpeed()
	{
		this.m_speed = Mathf.Min(this.m_speed + 1, 128);
		this.UpdateTimeScale();
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x00045F29 File Offset: 0x00044129
	private void DecreaseSpeed()
	{
		this.m_speed--;
		this.UpdateTimeScale();
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x00045F40 File Offset: 0x00044140
	public void UpdateTimeScale()
	{
		if (this.m_speed >= 0)
		{
			Time.timeScale = (float)this.m_speed / 2f + 1f;
		}
		else
		{
			Time.timeScale = -1f / (float)this.m_speed;
		}
		if (this.m_isStopped)
		{
			Time.timeScale = 0.001f;
		}
		if (this.m_isBlast)
		{
			if (this.m_isStopped)
			{
				Time.timeScale = 0.25f;
			}
			else
			{
				Time.timeScale = this.BlastTimeScale;
			}
		}
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x00045FD0 File Offset: 0x000441D0
	public void Awake()
	{
		RecorderPlaybackUI.Instance = this;
		this.m_togglePlayPauseButtonInput = new CompoundButtonInput(new IButtonInput[]
		{
			new ControllerButtonInput(XboxControllerInput.Button.ButtonA)
		});
		this.m_blastButtonInput = new CompoundButtonInput(new IButtonInput[]
		{
			new ControllerButtonInput(XboxControllerInput.Button.ButtonX)
		});
		this.m_breakButtonInput = new CompoundButtonInput(new IButtonInput[]
		{
			new ControllerButtonInput(XboxControllerInput.Button.ButtonB)
		});
		this.m_toggleVisibilityButtonInput = new CompoundButtonInput(new IButtonInput[]
		{
			new ControllerButtonInput(XboxControllerInput.Button.ButtonY),
			new KeyCodeButtonInput(KeyCode.Q)
		});
		this.m_leftTrigger = new CompoundButtonInput(new IButtonInput[]
		{
			new ControllerButtonInput(XboxControllerInput.Button.LeftTrigger)
		});
		this.m_rightTrigger = new CompoundButtonInput(new IButtonInput[]
		{
			new ControllerButtonInput(XboxControllerInput.Button.RightTrigger)
		});
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x0004608C File Offset: 0x0004428C
	public IEnumerator Start()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		if (File.Exists(Path.Combine(OutputFolder.BuildOutputPath, "hideUI.txt")))
		{
			this.ToggleTimeline();
		}
		if (Frapser.IsFrapserActive())
		{
			this.ToggleTimeline();
		}
		if (this.FrameToStopAt != 0)
		{
			this.JumpToFrame(this.FrameToStopAt);
		}
		yield break;
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x000460A8 File Offset: 0x000442A8
	public void JumpToFrame(int frameIndex)
	{
		this.IsJumpingToFrame = true;
		this.FrameToStopAt = frameIndex;
		int keyframe = 0;
		for (int i = 0; i < this.Timeline.Keyframes.Count; i++)
		{
			int num = this.Timeline.Keyframes[i];
			if (num < frameIndex)
			{
				keyframe = i;
			}
		}
		this.GoToKeyframe(keyframe);
	}

	// Token: 0x04000C0A RID: 3082
	public static RecorderPlaybackUI Instance = null;

	// Token: 0x04000C0B RID: 3083
	public float BlastTimeScale = 20f;

	// Token: 0x04000C0C RID: 3084
	public int FrameToStopAt;

	// Token: 0x04000C0D RID: 3085
	public bool ShouldStopAfterFrameToStopAt;

	// Token: 0x04000C0E RID: 3086
	public RecorderPlaybackUITimeline Timeline;

	// Token: 0x04000C0F RID: 3087
	public bool IsJumpingToFrame;

	// Token: 0x04000C10 RID: 3088
	public GameObject HelpTexture;

	// Token: 0x04000C11 RID: 3089
	private bool m_hideTimeline;

	// Token: 0x04000C12 RID: 3090
	private bool m_isBlast;

	// Token: 0x04000C13 RID: 3091
	private bool m_isStopped;

	// Token: 0x04000C14 RID: 3092
	private bool m_shouldExit;

	// Token: 0x04000C15 RID: 3093
	private int m_speed;

	// Token: 0x04000C16 RID: 3094
	private IButtonInput m_togglePlayPauseButtonInput;

	// Token: 0x04000C17 RID: 3095
	private IButtonInput m_blastButtonInput;

	// Token: 0x04000C18 RID: 3096
	private IButtonInput m_breakButtonInput;

	// Token: 0x04000C19 RID: 3097
	private IButtonInput m_toggleVisibilityButtonInput;

	// Token: 0x04000C1A RID: 3098
	private IButtonInput m_leftTrigger;

	// Token: 0x04000C1B RID: 3099
	private IButtonInput m_rightTrigger;

	// Token: 0x04000C1C RID: 3100
	private Core.Input.InputButtonProcessor PlayPauseButton = new Core.Input.InputButtonProcessor();

	// Token: 0x04000C1D RID: 3101
	private Core.Input.InputButtonProcessor BlastButton = new Core.Input.InputButtonProcessor();

	// Token: 0x04000C1E RID: 3102
	private Core.Input.InputButtonProcessor BreakButton = new Core.Input.InputButtonProcessor();

	// Token: 0x04000C1F RID: 3103
	private Core.Input.InputButtonProcessor VisibilityButton = new Core.Input.InputButtonProcessor();

	// Token: 0x04000C20 RID: 3104
	public static Core.Input.InputButtonProcessor LeftTrigger = new Core.Input.InputButtonProcessor();

	// Token: 0x04000C21 RID: 3105
	public static Core.Input.InputButtonProcessor RightTrigger = new Core.Input.InputButtonProcessor();
}
