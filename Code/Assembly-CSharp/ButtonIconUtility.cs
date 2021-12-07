using System;
using System.Collections.Generic;
using SmartInput;
using UnityEngine;

// Token: 0x0200066F RID: 1647
public static class ButtonIconUtility
{
	// Token: 0x06002816 RID: 10262 RVA: 0x000AE538 File Offset: 0x000AC738
	private static string KeyCodeToString(KeyCode keyCode)
	{
		if (keyCode == KeyCode.Alpha7 && GameSettings.Instance.KeyboardScheme == ControlScheme.Keyboard)
		{
			return "<icon>K</>";
		}
		if (ButtonIconUtility.m_keycodeToIconString.ContainsKey(keyCode))
		{
			return ButtonIconUtility.m_keycodeToIconString[keyCode];
		}
		string text = keyCode.ToString();
		ButtonIconUtility.m_keycodeToIconString[keyCode] = text;
		return text;
	}

	// Token: 0x06002817 RID: 10263 RVA: 0x000AE598 File Offset: 0x000AC798
	private static string ControllerButtonToString(XboxControllerInput.Button button)
	{
		if (ButtonIconUtility.m_controllerButtonToIconString.ContainsKey(button))
		{
			return ButtonIconUtility.m_controllerButtonToIconString[button];
		}
		string text = button.ToString();
		ButtonIconUtility.m_controllerButtonToIconString[button] = text;
		return text;
	}

	// Token: 0x06002818 RID: 10264 RVA: 0x000AE5DC File Offset: 0x000AC7DC
	private static string XboxOneControllerButtonToString(XboxOneController.Button button)
	{
		if (ButtonIconUtility.m_xboxOneControllerButtonToIconString.ContainsKey(button))
		{
			return ButtonIconUtility.m_xboxOneControllerButtonToIconString[button];
		}
		string text = button.ToString();
		ButtonIconUtility.m_xboxOneControllerButtonToIconString[button] = text;
		return text;
	}

	// Token: 0x06002819 RID: 10265 RVA: 0x000AE620 File Offset: 0x000AC820
	private static string XboxOneControllerAxisToString(XboxOneController.Axis axis)
	{
		if (ButtonIconUtility.m_xboxOneControllerAxisToIconString.ContainsKey(axis))
		{
			return ButtonIconUtility.m_xboxOneControllerAxisToIconString[axis];
		}
		string text = axis.ToString();
		ButtonIconUtility.m_xboxOneControllerAxisToIconString[axis] = text;
		return text;
	}

	// Token: 0x0600281A RID: 10266 RVA: 0x000AE662 File Offset: 0x000AC862
	private static string GetButtonString(KeyCodeButtonInput keyCodeButtonInput)
	{
		return ButtonIconUtility.KeyCodeToString(keyCodeButtonInput.KeyCode);
	}

	// Token: 0x0600281B RID: 10267 RVA: 0x000AE66F File Offset: 0x000AC86F
	private static string GetButtonString(XboxOneController.ButtonInput xboxOneButtonInput)
	{
		return ButtonIconUtility.XboxOneControllerButtonToString(xboxOneButtonInput.Button);
	}

	// Token: 0x0600281C RID: 10268 RVA: 0x000AE67C File Offset: 0x000AC87C
	private static string GetAxisString(XboxOneController.AxisInput xboxOneAxisInput)
	{
		return ButtonIconUtility.XboxOneControllerAxisToString(xboxOneAxisInput.Axis);
	}

	// Token: 0x0600281D RID: 10269 RVA: 0x000AE689 File Offset: 0x000AC889
	private static string GetButtonString(ControllerButtonInput controllerButtonInput)
	{
		return ButtonIconUtility.ControllerButtonToString(controllerButtonInput.Button);
	}

	// Token: 0x0600281E RID: 10270 RVA: 0x000AE698 File Offset: 0x000AC898
	private static string GetButtonString(AxisButtonInput axisButtonInput)
	{
		XboxOneController.AxisInput axisInput = axisButtonInput.GetAxisInput() as XboxOneController.AxisInput;
		XboxOneController.Axis axis = axisInput.Axis;
		if (axis == XboxOneController.Axis.LeftTrigger)
		{
			return ButtonIconUtility.ControllerButtonToString(XboxControllerInput.Button.LeftTrigger);
		}
		if (axis != XboxOneController.Axis.RightTrigger)
		{
			return string.Empty;
		}
		return ButtonIconUtility.ControllerButtonToString(XboxControllerInput.Button.RightTrigger);
	}

	// Token: 0x0600281F RID: 10271 RVA: 0x000AE6E0 File Offset: 0x000AC8E0
	public static string GetAxisIcon(IAxisInput axisInput, bool positive)
	{
		bool wasKeyboardUsedLast = PlayerInput.Instance.WasKeyboardUsedLast;
		CompoundAxisInput compoundAxisInput = axisInput as CompoundAxisInput;
		if (compoundAxisInput.Axis != null)
		{
			for (int i = 0; i < compoundAxisInput.Axis.Length; i++)
			{
				IAxisInput axisInput2 = compoundAxisInput.Axis[i];
				if (wasKeyboardUsedLast)
				{
					ButtonAxisInput buttonAxisInput = axisInput2 as ButtonAxisInput;
					if (buttonAxisInput != null && buttonAxisInput.Positive == positive)
					{
						IButtonInput buttonInput = buttonAxisInput.GetButtonInput();
						KeyCodeButtonInput keyCodeButtonInput = buttonInput as KeyCodeButtonInput;
						if (keyCodeButtonInput != null)
						{
							return ButtonIconUtility.GetButtonString(keyCodeButtonInput);
						}
					}
				}
				else if (XboxOne.ControllerReady)
				{
					XboxOneController.AxisInput axisInput3 = axisInput2 as XboxOneController.AxisInput;
					if (axisInput3 != null)
					{
						if (axisInput3.Axis == XboxOneController.Axis.DpadX || axisInput3.Axis == XboxOneController.Axis.LeftStickX || axisInput3.Axis == XboxOneController.Axis.RightStickX)
						{
							return (!positive) ? "<icon>c</>" : "<icon>d</>";
						}
						if (axisInput3.Axis == XboxOneController.Axis.DpadY || axisInput3.Axis == XboxOneController.Axis.LeftStickY || axisInput3.Axis == XboxOneController.Axis.RightStickY)
						{
							return (!positive) ? "<icon>b</>" : "<icon>a</>";
						}
					}
				}
				else
				{
					ControllerAxisInput controllerAxisInput = axisInput2 as ControllerAxisInput;
					if (controllerAxisInput != null)
					{
						if (controllerAxisInput.Axis == XboxControllerInput.Axis.DpadX || controllerAxisInput.Axis == XboxControllerInput.Axis.LeftStickX || controllerAxisInput.Axis == XboxControllerInput.Axis.RightStickX)
						{
							return (!positive) ? "<icon>c</>" : "<icon>d</>";
						}
						if (controllerAxisInput.Axis == XboxControllerInput.Axis.DpadY || controllerAxisInput.Axis == XboxControllerInput.Axis.LeftStickY || controllerAxisInput.Axis == XboxControllerInput.Axis.RightStickY)
						{
							return (!positive) ? "<icon>b</>" : "<icon>a</>";
						}
					}
				}
			}
		}
		return string.Empty;
	}

	// Token: 0x06002820 RID: 10272 RVA: 0x000AE898 File Offset: 0x000ACA98
	public static string GetButtonString(CompoundButtonInput compoundButtonInput)
	{
		bool wasKeyboardUsedLast = PlayerInput.Instance.WasKeyboardUsedLast;
		if (compoundButtonInput.Buttons != null)
		{
			for (int i = 0; i < compoundButtonInput.Buttons.Length; i++)
			{
				IButtonInput buttonInput = compoundButtonInput.Buttons[i];
				if (wasKeyboardUsedLast)
				{
					KeyCodeButtonInput keyCodeButtonInput = buttonInput as KeyCodeButtonInput;
					if (keyCodeButtonInput != null)
					{
						return ButtonIconUtility.GetButtonString(keyCodeButtonInput);
					}
				}
				else if (XboxOne.ControllerReady)
				{
					XboxOneController.ButtonInput buttonInput2 = buttonInput as XboxOneController.ButtonInput;
					if (buttonInput2 != null)
					{
						return ButtonIconUtility.GetButtonString(buttonInput2);
					}
					XboxOneController.AxisInput axisInput = buttonInput as XboxOneController.AxisInput;
					if (axisInput != null)
					{
						return ButtonIconUtility.GetAxisString(axisInput);
					}
					AxisButtonInput axisButtonInput = buttonInput as AxisButtonInput;
					if (axisButtonInput != null)
					{
						return ButtonIconUtility.GetButtonString(axisButtonInput);
					}
				}
				else
				{
					ControllerButtonInput controllerButtonInput = buttonInput as ControllerButtonInput;
					if (controllerButtonInput != null)
					{
						return ButtonIconUtility.GetButtonString(controllerButtonInput);
					}
				}
			}
		}
		return string.Empty;
	}

	// Token: 0x040022AC RID: 8876
	private const string IconAnalogueUp = "<icon>a</>";

	// Token: 0x040022AD RID: 8877
	private const string IconAnalogueDown = "<icon>b</>";

	// Token: 0x040022AE RID: 8878
	private const string IconAnalogueLeft = "<icon>c</>";

	// Token: 0x040022AF RID: 8879
	private const string IconAnalogueRight = "<icon>d</>";

	// Token: 0x040022B0 RID: 8880
	private const string IconAnalogueStick = "<icon>J</>";

	// Token: 0x040022B1 RID: 8881
	private const string IconButtonA = "<icon>e</>";

	// Token: 0x040022B2 RID: 8882
	private const string IconButtonB = "<icon>f</>";

	// Token: 0x040022B3 RID: 8883
	private const string IconButtonMenu = "<icon>g</>";

	// Token: 0x040022B4 RID: 8884
	private const string IconButtonX = "<icon>h</>";

	// Token: 0x040022B5 RID: 8885
	private const string IconButtonY = "<icon>i</>";

	// Token: 0x040022B6 RID: 8886
	private const string IconRightTrigger = "<icon>k</>";

	// Token: 0x040022B7 RID: 8887
	private const string IconButtonSelect = "<icon>l</>";

	// Token: 0x040022B8 RID: 8888
	private const string IconLeftTrigger = "<icon>m</>";

	// Token: 0x040022B9 RID: 8889
	private const string IconLeftShoulder = "<icon>R</>";

	// Token: 0x040022BA RID: 8890
	private const string IconRightShoulder = "<icon>S</>";

	// Token: 0x040022BB RID: 8891
	private const string IconKeyboardA = "<icon>n</>";

	// Token: 0x040022BC RID: 8892
	private const string IconKeyboardB = "<icon>o</>";

	// Token: 0x040022BD RID: 8893
	private const string IconKeyboardC = "<icon>p</>";

	// Token: 0x040022BE RID: 8894
	private const string IconKeyboardD = "<icon>q</>";

	// Token: 0x040022BF RID: 8895
	private const string IconKeyboardE = "<icon>H</>";

	// Token: 0x040022C0 RID: 8896
	private const string IconKeyboardF = "<icon>N</>";

	// Token: 0x040022C1 RID: 8897
	private const string IconKeyboardK = "<icon>O</>";

	// Token: 0x040022C2 RID: 8898
	private const string IconKeyboardL = "<icon>P</>";

	// Token: 0x040022C3 RID: 8899
	private const string IconKeyboardQ = "<icon>I</>";

	// Token: 0x040022C4 RID: 8900
	private const string IconKeyboardR = "<icon>T</>";

	// Token: 0x040022C5 RID: 8901
	private const string IconKeyboardS = "<icon>u</>";

	// Token: 0x040022C6 RID: 8902
	private const string IconKeyboardW = "<icon>w</>";

	// Token: 0x040022C7 RID: 8903
	private const string IconKeyboardX = "<icon>x</>";

	// Token: 0x040022C8 RID: 8904
	private const string IconKeyboardV = "<icon>A</>";

	// Token: 0x040022C9 RID: 8905
	private const string IconKeyboardZ = "<icon>B</>";

	// Token: 0x040022CA RID: 8906
	private const string IconKeyboardUp = "<icon>v</>";

	// Token: 0x040022CB RID: 8907
	private const string IconKeyboardDown = "<icon>r</>";

	// Token: 0x040022CC RID: 8908
	private const string IconKeyboardLeft = "<icon>s</>";

	// Token: 0x040022CD RID: 8909
	private const string IconKeyboardRight = "<icon>t</>";

	// Token: 0x040022CE RID: 8910
	private const string IconKeyboardArrows = "<icon>Q</>";

	// Token: 0x040022CF RID: 8911
	private const string IconKeyboardNavigation = "<icon>K</>";

	// Token: 0x040022D0 RID: 8912
	private const string IconKeyboardEsc = "<icon>y</>";

	// Token: 0x040022D1 RID: 8913
	private const string IconKeyboardTab = "<icon>z</>";

	// Token: 0x040022D2 RID: 8914
	private const string IconKeyboardSpace = "<icon>C</>";

	// Token: 0x040022D3 RID: 8915
	private const string IconKeyboardEnter = "<icon>D</>";

	// Token: 0x040022D4 RID: 8916
	private const string IconKeyboardControl = "<icon>G</>";

	// Token: 0x040022D5 RID: 8917
	private const string IconKeyboardShift = "<icon>L</>";

	// Token: 0x040022D6 RID: 8918
	private const string IconKeyboardDelete = "<icon>M</>";

	// Token: 0x040022D7 RID: 8919
	private const string IconMouseLeft = "<icon>E</>";

	// Token: 0x040022D8 RID: 8920
	private const string IconMouseRight = "<icon>F</>";

	// Token: 0x040022D9 RID: 8921
	private static Dictionary<KeyCode, string> m_keycodeToIconString = new Dictionary<KeyCode, string>
	{
		{
			KeyCode.A,
			"<icon>n</>"
		},
		{
			KeyCode.B,
			"<icon>o</>"
		},
		{
			KeyCode.C,
			"<icon>p</>"
		},
		{
			KeyCode.D,
			"<icon>q</>"
		},
		{
			KeyCode.DownArrow,
			"<icon>r</>"
		},
		{
			KeyCode.LeftArrow,
			"<icon>s</>"
		},
		{
			KeyCode.RightArrow,
			"<icon>t</>"
		},
		{
			KeyCode.S,
			"<icon>u</>"
		},
		{
			KeyCode.UpArrow,
			"<icon>v</>"
		},
		{
			KeyCode.W,
			"<icon>w</>"
		},
		{
			KeyCode.X,
			"<icon>x</>"
		},
		{
			KeyCode.Escape,
			"<icon>y</>"
		},
		{
			KeyCode.Tab,
			"<icon>z</>"
		},
		{
			KeyCode.V,
			"<icon>A</>"
		},
		{
			KeyCode.Z,
			"<icon>B</>"
		},
		{
			KeyCode.Space,
			"<icon>C</>"
		},
		{
			KeyCode.Return,
			"<icon>D</>"
		},
		{
			KeyCode.Mouse0,
			"<icon>E</>"
		},
		{
			KeyCode.Mouse1,
			"<icon>F</>"
		},
		{
			KeyCode.LeftControl,
			"<icon>G</>"
		},
		{
			KeyCode.RightControl,
			"<icon>G</>"
		},
		{
			KeyCode.E,
			"<icon>H</>"
		},
		{
			KeyCode.Q,
			"<icon>I</>"
		},
		{
			KeyCode.Alpha7,
			"<icon>Q</>"
		},
		{
			KeyCode.Alpha8,
			"<icon>Q</>"
		},
		{
			KeyCode.LeftShift,
			"<icon>L</>"
		},
		{
			KeyCode.RightShift,
			"<icon>L</>"
		},
		{
			KeyCode.Delete,
			"<icon>M</>"
		},
		{
			KeyCode.K,
			"<icon>O</>"
		},
		{
			KeyCode.L,
			"<icon>P</>"
		},
		{
			KeyCode.F,
			"<icon>N</>"
		},
		{
			KeyCode.R,
			"<icon>T</>"
		}
	};

	// Token: 0x040022DA RID: 8922
	private static Dictionary<XboxControllerInput.Button, string> m_controllerButtonToIconString = new Dictionary<XboxControllerInput.Button, string>
	{
		{
			XboxControllerInput.Button.ButtonA,
			"<icon>e</>"
		},
		{
			XboxControllerInput.Button.ButtonB,
			"<icon>f</>"
		},
		{
			XboxControllerInput.Button.Start,
			"<icon>g</>"
		},
		{
			XboxControllerInput.Button.ButtonX,
			"<icon>h</>"
		},
		{
			XboxControllerInput.Button.ButtonY,
			"<icon>i</>"
		},
		{
			XboxControllerInput.Button.RightTrigger,
			"<icon>k</>"
		},
		{
			XboxControllerInput.Button.Select,
			"<icon>l</>"
		},
		{
			XboxControllerInput.Button.LeftTrigger,
			"<icon>m</>"
		},
		{
			XboxControllerInput.Button.LeftStick,
			"<icon>J</>"
		},
		{
			XboxControllerInput.Button.RightShoulder,
			"<icon>S</>"
		},
		{
			XboxControllerInput.Button.LeftShoulder,
			"<icon>R</>"
		}
	};

	// Token: 0x040022DB RID: 8923
	private static Dictionary<XboxOneController.Button, string> m_xboxOneControllerButtonToIconString = new Dictionary<XboxOneController.Button, string>
	{
		{
			XboxOneController.Button.GamepadButtonA,
			"<icon>e</>"
		},
		{
			XboxOneController.Button.GamepadButtonB,
			"<icon>f</>"
		},
		{
			XboxOneController.Button.GamepadButtonMenu,
			"<icon>g</>"
		},
		{
			XboxOneController.Button.GamepadButtonX,
			"<icon>h</>"
		},
		{
			XboxOneController.Button.GamepadButtonY,
			"<icon>i</>"
		},
		{
			XboxOneController.Button.GamepadButtonView,
			"<icon>l</>"
		},
		{
			XboxOneController.Button.GamepadButtonLeftThumbstick,
			"<icon>J</>"
		},
		{
			XboxOneController.Button.GamepadButtonRightShoulder,
			"<icon>S</>"
		},
		{
			XboxOneController.Button.GamepadButtonLeftShoulder,
			"<icon>R</>"
		}
	};

	// Token: 0x040022DC RID: 8924
	private static Dictionary<XboxOneController.Axis, string> m_xboxOneControllerAxisToIconString = new Dictionary<XboxOneController.Axis, string>
	{
		{
			XboxOneController.Axis.Gamepad1LeftTrigger,
			"<icon>m</>"
		},
		{
			XboxOneController.Axis.Gamepad1RightTrigger,
			"<icon>k</>"
		}
	};
}
