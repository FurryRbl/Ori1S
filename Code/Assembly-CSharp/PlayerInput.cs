using System;
using System.Collections.Generic;
using Core;
using Game;
using SmartInput;
using UnityEngine;

// Token: 0x0200011F RID: 287
public class PlayerInput : MonoBehaviour
{
	// Token: 0x06000B19 RID: 2841 RVA: 0x00030578 File Offset: 0x0002E778
	public void ClearControls()
	{
		this.HorizontalAnalogLeft.Clear();
		this.VerticalAnalogLeft.Clear();
		this.HorizontalAnalogRight.Clear();
		this.VerticalAnalogRight.Clear();
		this.HorizontalDigiPad.Clear();
		this.VerticalDigiPad.Clear();
		this.Jump.Clear();
		this.SpiritFlame.Clear();
		this.SoulFlame.Clear();
		this.Bash.Clear();
		this.ChargeJump.Clear();
		this.Glide.Clear();
		this.Grab.Clear();
		this.LeftShoulder.Clear();
		this.RightShoulder.Clear();
		this.Select.Clear();
		this.Start.Clear();
		this.LeftStick.Clear();
		this.RightStick.Clear();
		this.MenuDown.Clear();
		this.MenuUp.Clear();
		this.MenuLeft.Clear();
		this.MenuRight.Clear();
		this.MenuPageLeft.Clear();
		this.MenuPageRight.Clear();
		this.ActionButtonA.Clear();
		this.ZoomIn.Clear();
		this.ZoomOut.Clear();
		this.Cancel.Clear();
		this.Copy.Clear();
		this.Delete.Clear();
		this.Focus.Clear();
		this.Filter.Clear();
		this.Legend.Clear();
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x000306FB File Offset: 0x0002E8FB
	public void AddXboxOneControls()
	{
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x00030700 File Offset: 0x0002E900
	public void AddControllerControls()
	{
		this.HorizontalAnalogLeft.Add(new ControllerAxisInput(XboxControllerInput.Axis.LeftStickX));
		this.VerticalAnalogLeft.Add(new ControllerAxisInput(XboxControllerInput.Axis.LeftStickY));
		this.HorizontalAnalogRight.Add(new ControllerAxisInput(XboxControllerInput.Axis.RightStickX));
		this.VerticalAnalogRight.Add(new ControllerAxisInput(XboxControllerInput.Axis.RightStickY));
		this.HorizontalDigiPad.Add(new ControllerAxisInput(XboxControllerInput.Axis.DpadX));
		this.VerticalDigiPad.Add(new ControllerAxisInput(XboxControllerInput.Axis.DpadY));
		this.Jump.Add(new ControllerButtonInput(XboxControllerInput.Button.ButtonA));
		this.SpiritFlame.Add(new ControllerButtonInput(XboxControllerInput.Button.ButtonX));
		this.SoulFlame.Add(new ControllerButtonInput(XboxControllerInput.Button.ButtonB));
		this.Bash.Add(new ControllerButtonInput(XboxControllerInput.Button.ButtonY));
		this.ChargeJump.Add(new ControllerButtonInput(XboxControllerInput.Button.LeftTrigger));
		this.ZoomIn.Add(new ControllerButtonInput(XboxControllerInput.Button.RightTrigger));
		this.Glide.Add(new ControllerButtonInput(XboxControllerInput.Button.RightTrigger));
		this.Grab.Add(new ControllerButtonInput(XboxControllerInput.Button.RightTrigger));
		this.ZoomOut.Add(new ControllerButtonInput(XboxControllerInput.Button.LeftTrigger));
		this.LeftShoulder.Add(new ControllerButtonInput(XboxControllerInput.Button.LeftShoulder));
		this.RightShoulder.Add(new ControllerButtonInput(XboxControllerInput.Button.RightShoulder));
		this.Select.Add(new ControllerButtonInput(XboxControllerInput.Button.Select));
		this.Start.Add(new ControllerButtonInput(XboxControllerInput.Button.Start));
		this.LeftStick.Add(new ControllerButtonInput(XboxControllerInput.Button.LeftStick));
		this.RightStick.Add(new ControllerButtonInput(XboxControllerInput.Button.RightStick));
		this.MenuDown.Add(new AxisButtonInput(new ControllerAxisInput(XboxControllerInput.Axis.DpadY), AxisButtonInput.AxisMode.LessThan, -0.5f));
		this.MenuDown.Add(new AxisButtonInput(new ControllerAxisInput(XboxControllerInput.Axis.LeftStickY), AxisButtonInput.AxisMode.LessThan, -0.5f));
		this.MenuUp.Add(new AxisButtonInput(new ControllerAxisInput(XboxControllerInput.Axis.DpadY), AxisButtonInput.AxisMode.GreaterThan, 0.5f));
		this.MenuUp.Add(new AxisButtonInput(new ControllerAxisInput(XboxControllerInput.Axis.LeftStickY), AxisButtonInput.AxisMode.GreaterThan, 0.5f));
		this.MenuLeft.Add(new AxisButtonInput(new ControllerAxisInput(XboxControllerInput.Axis.DpadX), AxisButtonInput.AxisMode.LessThan, -0.5f));
		this.MenuLeft.Add(new AxisButtonInput(new ControllerAxisInput(XboxControllerInput.Axis.LeftStickX), AxisButtonInput.AxisMode.LessThan, -0.5f));
		this.MenuRight.Add(new AxisButtonInput(new ControllerAxisInput(XboxControllerInput.Axis.DpadX), AxisButtonInput.AxisMode.GreaterThan, 0.5f));
		this.MenuRight.Add(new AxisButtonInput(new ControllerAxisInput(XboxControllerInput.Axis.LeftStickX), AxisButtonInput.AxisMode.GreaterThan, 0.5f));
		this.ActionButtonA.Add(new ControllerButtonInput(XboxControllerInput.Button.ButtonA));
		this.Cancel.Add(new ControllerButtonInput(XboxControllerInput.Button.ButtonB));
		this.MenuPageLeft.Add(new ControllerButtonInput(XboxControllerInput.Button.LeftTrigger));
		this.MenuPageRight.Add(new ControllerButtonInput(XboxControllerInput.Button.RightTrigger));
		this.Copy.Add(new ControllerButtonInput(XboxControllerInput.Button.ButtonX));
		this.Delete.Add(new ControllerButtonInput(XboxControllerInput.Button.ButtonY));
		this.Focus.Add(new ControllerButtonInput(XboxControllerInput.Button.ButtonX));
		this.Filter.Add(new ControllerButtonInput(XboxControllerInput.Button.ButtonX));
		this.Legend.Add(new ControllerButtonInput(XboxControllerInput.Button.ButtonY));
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x000309F0 File Offset: 0x0002EBF0
	public void AddKeyboardControls()
	{
		PlayerInputRebinding.KeyBindingSettings keyRebindings = PlayerInputRebinding.KeyRebindings;
		foreach (KeyCode keyCode in keyRebindings.HorizontalDigiPadLeft)
		{
			this.HorizontalDigiPad.Add(new ButtonAxisInput(new KeyCodeButtonInput(keyCode), ButtonAxisInput.Mode.Negative));
		}
		foreach (KeyCode keyCode2 in keyRebindings.HorizontalDigiPadRight)
		{
			this.HorizontalDigiPad.Add(new ButtonAxisInput(new KeyCodeButtonInput(keyCode2), ButtonAxisInput.Mode.Positive));
		}
		foreach (KeyCode keyCode3 in keyRebindings.VerticalDigiPadDown)
		{
			this.VerticalDigiPad.Add(new ButtonAxisInput(new KeyCodeButtonInput(keyCode3), ButtonAxisInput.Mode.Negative));
		}
		foreach (KeyCode keyCode4 in keyRebindings.VerticalDigiPadUp)
		{
			this.VerticalDigiPad.Add(new ButtonAxisInput(new KeyCodeButtonInput(keyCode4), ButtonAxisInput.Mode.Positive));
		}
		this.AddKeyCodesToButtonInput(keyRebindings.MenuLeft, this.MenuLeft);
		this.AddKeyCodesToButtonInput(keyRebindings.MenuRight, this.MenuRight);
		this.AddKeyCodesToButtonInput(keyRebindings.MenuDown, this.MenuDown);
		this.AddKeyCodesToButtonInput(keyRebindings.MenuUp, this.MenuUp);
		this.AddKeyCodesToButtonInput(keyRebindings.MenuPageLeft, this.MenuPageLeft);
		this.AddKeyCodesToButtonInput(keyRebindings.MenuPageRight, this.MenuPageRight);
		this.AddKeyCodesToButtonInput(keyRebindings.ActionButtonA, this.ActionButtonA);
		this.AddKeyCodesToButtonInput(keyRebindings.SoulFlame, this.SoulFlame);
		this.AddKeyCodesToButtonInput(keyRebindings.Jump, this.Jump);
		this.AddKeyCodesToButtonInput(keyRebindings.Grab, this.Grab);
		this.AddKeyCodesToButtonInput(keyRebindings.SpiritFlame, this.SpiritFlame);
		this.AddKeyCodesToButtonInput(keyRebindings.Bash, this.Bash);
		this.AddKeyCodesToButtonInput(keyRebindings.Glide, this.Glide);
		this.AddKeyCodesToButtonInput(keyRebindings.ChargeJump, this.ChargeJump);
		this.AddKeyCodesToButtonInput(keyRebindings.Select, this.Select);
		this.AddKeyCodesToButtonInput(keyRebindings.Start, this.Start);
		this.AddKeyCodesToButtonInput(keyRebindings.Cancel, this.Cancel);
		this.AddKeyCodesToButtonInput(keyRebindings.LeftShoulder, this.LeftShoulder);
		this.AddKeyCodesToButtonInput(keyRebindings.RightShoulder, this.RightShoulder);
		this.AddKeyCodesToButtonInput(keyRebindings.LeftStick, this.LeftStick);
		this.AddKeyCodesToButtonInput(keyRebindings.RightStick, this.RightStick);
		this.AddKeyCodesToButtonInput(keyRebindings.ZoomIn, this.ZoomIn);
		this.AddKeyCodesToButtonInput(keyRebindings.ZoomOut, this.ZoomOut);
		this.AddKeyCodesToButtonInput(keyRebindings.Copy, this.Copy);
		this.AddKeyCodesToButtonInput(keyRebindings.Delete, this.Delete);
		this.AddKeyCodesToButtonInput(keyRebindings.Focus, this.Focus);
		this.AddKeyCodesToButtonInput(keyRebindings.Filter, this.Filter);
		this.AddKeyCodesToButtonInput(keyRebindings.Legend, this.Legend);
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x00030CF4 File Offset: 0x0002EEF4
	private void AddKeyCodesToButtonInput(KeyCode[] keyCodes, CompoundButtonInput buttonInput)
	{
		foreach (KeyCode keyCode in keyCodes)
		{
			buttonInput.Add(new KeyCodeButtonInput(keyCode));
		}
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x00030D28 File Offset: 0x0002EF28
	public void Awake()
	{
		PlayerInput.Instance = this;
		this.RefreshControlScheme();
		this.LeftClick = new KeyCodeButtonInput(KeyCode.Mouse0);
		this.RightClick = new KeyCodeButtonInput(KeyCode.Mouse1);
		this.m_allButtonInput = new List<IButtonInput>
		{
			this.Jump,
			this.SpiritFlame,
			this.SoulFlame,
			this.Bash,
			this.ChargeJump,
			this.Glide,
			this.Grab,
			this.LeftShoulder,
			this.RightShoulder,
			this.Select,
			this.Start,
			this.LeftStick,
			this.RightStick,
			this.MenuDown,
			this.MenuUp,
			this.MenuLeft,
			this.MenuRight,
			this.MenuPageRight,
			this.MenuPageLeft,
			this.ActionButtonA,
			this.Cancel,
			this.Copy,
			this.Delete,
			this.Focus,
			this.Filter,
			this.Legend
		};
		this.m_allButtonProcessor = new List<Core.Input.InputButtonProcessor>
		{
			Core.Input.Jump,
			Core.Input.SpiritFlame,
			Core.Input.SoulFlame,
			Core.Input.Bash,
			Core.Input.ChargeJump,
			Core.Input.Glide,
			Core.Input.Grab,
			Core.Input.LeftShoulder,
			Core.Input.RightShoulder,
			Core.Input.Select,
			Core.Input.Start,
			Core.Input.LeftStick,
			Core.Input.RightStick,
			Core.Input.MenuDown,
			Core.Input.MenuUp,
			Core.Input.MenuLeft,
			Core.Input.MenuRight,
			Core.Input.MenuPageRight,
			Core.Input.MenuPageLeft,
			Core.Input.ActionButtonA,
			Core.Input.Cancel,
			Core.Input.Copy,
			Core.Input.Delete,
			Core.Input.Focus,
			Core.Input.Filter,
			Core.Input.Legend
		};
		this.m_allAxisInput = new List<IAxisInput>
		{
			this.HorizontalAnalogLeft,
			this.VerticalAnalogLeft,
			this.HorizontalAnalogRight,
			this.VerticalAnalogRight,
			this.HorizontalDigiPad,
			this.VerticalDigiPad
		};
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x00031026 File Offset: 0x0002F226
	public float SimplifyAxis(float x)
	{
		return Utility.Round(x, 0.001f);
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x00031033 File Offset: 0x0002F233
	public void ApplyDeadzone(ref float x, ref float y)
	{
		if (x * x + y * y < 0.040000003f)
		{
			x = 0f;
			y = 0f;
		}
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x00031058 File Offset: 0x0002F258
	public void FixedUpdate()
	{
		if (!this.Active)
		{
			return;
		}
		Vector2 vector = UI.Cameras.Current.Camera.ScreenToViewportPoint(UnityEngine.Input.mousePosition);
		Core.Input.CursorMoved = (Vector2.Distance(vector, Core.Input.CursorPosition) > 0.0001f);
		Core.Input.CursorPosition = vector;
		Core.Input.HorizontalAnalogLeft = this.SimplifyAxis(this.HorizontalAnalogLeft.AxisValue());
		Core.Input.VerticalAnalogLeft = this.SimplifyAxis(this.VerticalAnalogLeft.AxisValue());
		this.ApplyDeadzone(ref Core.Input.HorizontalAnalogLeft, ref Core.Input.VerticalAnalogLeft);
		Core.Input.HorizontalAnalogRight = this.SimplifyAxis(this.HorizontalAnalogRight.AxisValue());
		Core.Input.VerticalAnalogRight = this.SimplifyAxis(this.VerticalAnalogRight.AxisValue());
		this.ApplyDeadzone(ref Core.Input.HorizontalAnalogRight, ref Core.Input.VerticalAnalogRight);
		Core.Input.HorizontalDigiPad = Mathf.RoundToInt(this.HorizontalDigiPad.AxisValue());
		Core.Input.VerticalDigiPad = Mathf.RoundToInt(this.VerticalDigiPad.AxisValue());
		Core.Input.AnyStart.Update(this.IsAnyStartPressed());
		Core.Input.ZoomIn.Update(this.ZoomIn.GetButton());
		Core.Input.ZoomOut.Update(this.ZoomOut.GetButton());
		Core.Input.LeftClick.Update(this.LeftClick.GetButton());
		Core.Input.RightClick.Update(this.RightClick.GetButton());
		this.m_lastPressedButtonInput = -1;
		for (int i = 0; i < this.m_allButtonInput.Count; i++)
		{
			bool button = this.m_allButtonInput[i].GetButton();
			if (button)
			{
				this.m_lastPressedButtonInput = i;
			}
			this.m_allButtonProcessor[i].Update(button);
		}
		this.RefreshControls();
		if (!ControlsScreen.IsVisible && this.m_lastPressedButtonInput != -1)
		{
			bool flag = this.WasKeyboardUsedLast;
			if (this.m_lastPressedButtonInput != -1)
			{
				flag = this.KeyboardUsedLast(this.m_allButtonInput[this.m_lastPressedButtonInput]);
			}
			if (flag != this.WasKeyboardUsedLast)
			{
				GameSettings.Instance.CurrentControlScheme = ((!flag) ? ControlScheme.Controller : GameSettings.Instance.KeyboardScheme);
			}
		}
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x00031278 File Offset: 0x0002F478
	public void RefreshControls()
	{
		Core.Input.Horizontal = Mathf.Clamp((float)Core.Input.HorizontalDigiPad + Core.Input.HorizontalAnalogLeft, -1f, 1f);
		Core.Input.Vertical = Mathf.Clamp((float)Core.Input.VerticalDigiPad + Core.Input.VerticalAnalogLeft, -1f, 1f);
		Core.Input.Down.Update(Core.Input.NormalizedVertical == -1f);
		Core.Input.Up.Update(Core.Input.NormalizedVertical == 1f);
		Core.Input.Left.Update(Core.Input.NormalizedHorizontal == -1);
		Core.Input.Right.Update(Core.Input.NormalizedHorizontal == 1);
		for (int i = 0; i < Core.Input.Buttons.Length; i++)
		{
			Core.Input.InputButtonProcessor inputButtonProcessor = Core.Input.Buttons[i];
			inputButtonProcessor.Used = false;
		}
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x0003133C File Offset: 0x0002F53C
	public void RefreshControlScheme()
	{
		this.ClearControls();
		this.AddControllerControls();
		this.AddXboxOneControls();
		this.AddKeyboardControls();
		PlayerInputRebinding.RefreshControllerButtonRemappings();
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x00031368 File Offset: 0x0002F568
	private void RefreshLastPressedButton()
	{
		this.m_lastPressedButtonInput = -1;
		this.m_lastPressedAxisInput = -1;
		for (int i = 0; i < this.m_allButtonInput.Count; i++)
		{
			if (this.m_allButtonInput[i].GetButton())
			{
				this.m_lastPressedButtonInput = i;
				break;
			}
		}
	}

	// Token: 0x17000253 RID: 595
	// (get) Token: 0x06000B25 RID: 2853 RVA: 0x000313C6 File Offset: 0x0002F5C6
	public bool WasKeyboardUsedLast
	{
		get
		{
			return GameSettings.Instance.CurrentControlScheme != ControlScheme.Controller;
		}
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x000313D8 File Offset: 0x0002F5D8
	private bool KeyboardUsedLast(IButtonInput iButtonInput)
	{
		KeyCodeButtonInput keyCodeButtonInput = iButtonInput as KeyCodeButtonInput;
		if (keyCodeButtonInput != null)
		{
			return true;
		}
		AxisButtonInput axisButtonInput = iButtonInput as AxisButtonInput;
		if (axisButtonInput != null)
		{
			return this.KeyboardUsedLast(axisButtonInput.GetAxisInput());
		}
		CompoundButtonInput compoundButtonInput = iButtonInput as CompoundButtonInput;
		if (compoundButtonInput != null)
		{
			return this.KeyboardUsedLast(compoundButtonInput.GetLastPressed());
		}
		ControllerButtonInput controllerButtonInput = iButtonInput as ControllerButtonInput;
		return controllerButtonInput != null && false;
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x00031438 File Offset: 0x0002F638
	private bool KeyboardUsedLast(IAxisInput iAxisInput)
	{
		if (iAxisInput is ButtonAxisInput)
		{
			return this.KeyboardUsedLast((iAxisInput as ButtonAxisInput).GetButtonInput());
		}
		if (iAxisInput is CompoundAxisInput)
		{
			return this.KeyboardUsedLast((iAxisInput as CompoundAxisInput).GetLastPressed());
		}
		return iAxisInput is ControllerAxisInput && false;
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x00031490 File Offset: 0x0002F690
	private bool IsAnyStartPressed()
	{
		return XboxControllerInput.GetButton(XboxControllerInput.Button.Start, -1) || XboxControllerInput.GetButton(XboxControllerInput.Button.ButtonA, -1) || XboxControllerInput.GetButton(XboxControllerInput.Button.ButtonB, -1) || XboxControllerInput.GetButton(XboxControllerInput.Button.ButtonX, -1) || XboxControllerInput.GetButton(XboxControllerInput.Button.ButtonY, -1) || (MoonInput.GetKey(KeyCode.Space) || MoonInput.GetKey(KeyCode.X) || MoonInput.GetKey(KeyCode.Mouse0) || MoonInput.GetKey(KeyCode.Return) || MoonInput.GetKey(KeyCode.Escape)) || MoonInput.anyKey;
	}

	// Token: 0x0400090F RID: 2319
	public static PlayerInput Instance;

	// Token: 0x04000910 RID: 2320
	public bool Active = true;

	// Token: 0x04000911 RID: 2321
	public CompoundAxisInput HorizontalAnalogLeft = new CompoundAxisInput();

	// Token: 0x04000912 RID: 2322
	public CompoundAxisInput VerticalAnalogLeft = new CompoundAxisInput();

	// Token: 0x04000913 RID: 2323
	public CompoundAxisInput HorizontalAnalogRight = new CompoundAxisInput();

	// Token: 0x04000914 RID: 2324
	public CompoundAxisInput VerticalAnalogRight = new CompoundAxisInput();

	// Token: 0x04000915 RID: 2325
	public CompoundAxisInput HorizontalDigiPad = new CompoundAxisInput();

	// Token: 0x04000916 RID: 2326
	public CompoundAxisInput VerticalDigiPad = new CompoundAxisInput();

	// Token: 0x04000917 RID: 2327
	public CompoundButtonInput Jump = new CompoundButtonInput();

	// Token: 0x04000918 RID: 2328
	public CompoundButtonInput SpiritFlame = new CompoundButtonInput();

	// Token: 0x04000919 RID: 2329
	public CompoundButtonInput SoulFlame = new CompoundButtonInput();

	// Token: 0x0400091A RID: 2330
	public CompoundButtonInput Bash = new CompoundButtonInput();

	// Token: 0x0400091B RID: 2331
	public CompoundButtonInput ChargeJump = new CompoundButtonInput();

	// Token: 0x0400091C RID: 2332
	public CompoundButtonInput Glide = new CompoundButtonInput();

	// Token: 0x0400091D RID: 2333
	public CompoundButtonInput Grab = new CompoundButtonInput();

	// Token: 0x0400091E RID: 2334
	public CompoundButtonInput ZoomIn = new CompoundButtonInput();

	// Token: 0x0400091F RID: 2335
	public CompoundButtonInput ZoomOut = new CompoundButtonInput();

	// Token: 0x04000920 RID: 2336
	public CompoundButtonInput LeftShoulder = new CompoundButtonInput();

	// Token: 0x04000921 RID: 2337
	public CompoundButtonInput RightShoulder = new CompoundButtonInput();

	// Token: 0x04000922 RID: 2338
	public CompoundButtonInput Select = new CompoundButtonInput();

	// Token: 0x04000923 RID: 2339
	public CompoundButtonInput Start = new CompoundButtonInput();

	// Token: 0x04000924 RID: 2340
	public CompoundButtonInput LeftStick = new CompoundButtonInput();

	// Token: 0x04000925 RID: 2341
	public CompoundButtonInput RightStick = new CompoundButtonInput();

	// Token: 0x04000926 RID: 2342
	public CompoundButtonInput MenuDown = new CompoundButtonInput();

	// Token: 0x04000927 RID: 2343
	public CompoundButtonInput MenuUp = new CompoundButtonInput();

	// Token: 0x04000928 RID: 2344
	public CompoundButtonInput MenuLeft = new CompoundButtonInput();

	// Token: 0x04000929 RID: 2345
	public CompoundButtonInput MenuRight = new CompoundButtonInput();

	// Token: 0x0400092A RID: 2346
	public CompoundButtonInput MenuPageLeft = new CompoundButtonInput();

	// Token: 0x0400092B RID: 2347
	public CompoundButtonInput MenuPageRight = new CompoundButtonInput();

	// Token: 0x0400092C RID: 2348
	public CompoundButtonInput ActionButtonA = new CompoundButtonInput();

	// Token: 0x0400092D RID: 2349
	public CompoundButtonInput Cancel = new CompoundButtonInput();

	// Token: 0x0400092E RID: 2350
	public CompoundButtonInput Copy = new CompoundButtonInput();

	// Token: 0x0400092F RID: 2351
	public CompoundButtonInput Delete = new CompoundButtonInput();

	// Token: 0x04000930 RID: 2352
	public CompoundButtonInput Focus = new CompoundButtonInput();

	// Token: 0x04000931 RID: 2353
	public CompoundButtonInput Filter = new CompoundButtonInput();

	// Token: 0x04000932 RID: 2354
	public CompoundButtonInput Legend = new CompoundButtonInput();

	// Token: 0x04000933 RID: 2355
	public IButtonInput LeftClick;

	// Token: 0x04000934 RID: 2356
	public IButtonInput RightClick;

	// Token: 0x04000935 RID: 2357
	public List<IButtonInput> m_allButtonInput;

	// Token: 0x04000936 RID: 2358
	public List<Core.Input.InputButtonProcessor> m_allButtonProcessor;

	// Token: 0x04000937 RID: 2359
	public List<IAxisInput> m_allAxisInput;

	// Token: 0x04000938 RID: 2360
	private int m_lastPressedButtonInput = -1;

	// Token: 0x04000939 RID: 2361
	private int m_lastPressedAxisInput = -1;
}
