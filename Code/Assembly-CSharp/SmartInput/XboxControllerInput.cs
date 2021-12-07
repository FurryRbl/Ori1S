using System;
using J2i.Net.XInputWrapper;

namespace SmartInput
{
	// Token: 0x02000193 RID: 403
	public static class XboxControllerInput
	{
		// Token: 0x06000FB2 RID: 4018 RVA: 0x0004831B File Offset: 0x0004651B
		private static float ToAxisFloat(int axis)
		{
			return (float)axis / 32768f;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00048328 File Offset: 0x00046528
		public static float GetAxis(XboxControllerInput.Axis axis)
		{
			int axisUserIndex = XboxControllerInput.GetAxisUserIndex();
			XboxController xboxController = XboxController.RetrieveController((axisUserIndex != -1) ? axisUserIndex : 0);
			bool isConnected = xboxController.IsConnected;
			if (isConnected)
			{
				switch (axis)
				{
				case XboxControllerInput.Axis.LeftStickX:
					return XboxControllerInput.ToAxisFloat(xboxController.LeftThumbStickX);
				case XboxControllerInput.Axis.LeftStickY:
					return XboxControllerInput.ToAxisFloat(xboxController.LeftThumbStickY);
				case XboxControllerInput.Axis.RightStickX:
					return XboxControllerInput.ToAxisFloat(xboxController.RightThumbStickX);
				case XboxControllerInput.Axis.RightStickY:
					return XboxControllerInput.ToAxisFloat(xboxController.RightThumbStickY);
				case XboxControllerInput.Axis.DpadX:
					return (float)(((!xboxController.IsDPadRightPressed) ? 0 : 1) - ((!xboxController.IsDPadLeftPressed) ? 0 : 1));
				case XboxControllerInput.Axis.DpadY:
					return (float)(((!xboxController.IsDPadUpPressed) ? 0 : 1) - ((!xboxController.IsDPadDownPressed) ? 0 : 1));
				}
			}
			if (!isConnected)
			{
				switch (axis)
				{
				case XboxControllerInput.Axis.LeftStickX:
					return MoonInput.GetAxis(XboxControllerInput.m_joystickAxis1Map[axisUserIndex]);
				case XboxControllerInput.Axis.LeftStickY:
					return MoonInput.GetAxis(XboxControllerInput.m_joystickAxis2Map[axisUserIndex]);
				case XboxControllerInput.Axis.RightStickX:
					return MoonInput.GetAxis(XboxControllerInput.m_joystickAxis4Map[axisUserIndex]);
				case XboxControllerInput.Axis.RightStickY:
					return MoonInput.GetAxis(XboxControllerInput.m_joystickAxis5Map[axisUserIndex]);
				case XboxControllerInput.Axis.DpadX:
					return MoonInput.GetAxis(XboxControllerInput.m_joystickAxis6Map[axisUserIndex]);
				case XboxControllerInput.Axis.DpadY:
					return MoonInput.GetAxis(XboxControllerInput.m_joystickAxis7Map[axisUserIndex]);
				}
			}
			return 0f;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00048480 File Offset: 0x00046680
		public static bool GetButton(XboxControllerInput.Button button, int userIndex = -1)
		{
			int num = XboxControllerInput.GetAxisUserIndex();
			if (userIndex != -1)
			{
				num = XboxControllerInput.UserIndexToAxisUserIndex(userIndex);
			}
			int num2 = XboxControllerInput.AxisIndexToButtonUserIndex(num);
			XboxController xboxController = XboxController.RetrieveController((userIndex != -1) ? userIndex : 0);
			bool isConnected = xboxController.IsConnected;
			if (isConnected)
			{
				switch (button)
				{
				case XboxControllerInput.Button.ButtonA:
					return xboxController.IsAPressed;
				case XboxControllerInput.Button.ButtonX:
					return xboxController.IsXPressed;
				case XboxControllerInput.Button.ButtonY:
					return xboxController.IsYPressed;
				case XboxControllerInput.Button.ButtonB:
					return xboxController.IsBPressed;
				case XboxControllerInput.Button.LeftTrigger:
					return xboxController.LeftTrigger > 20;
				case XboxControllerInput.Button.RightTrigger:
					return xboxController.RightTrigger > 20;
				case XboxControllerInput.Button.LeftShoulder:
					return xboxController.IsLeftShoulderPressed;
				case XboxControllerInput.Button.RightShoulder:
					return xboxController.IsRightShoulderPressed;
				case XboxControllerInput.Button.LeftStick:
					return xboxController.IsLeftStickPressed;
				case XboxControllerInput.Button.RightStick:
					return xboxController.IsRightStickPressed;
				case XboxControllerInput.Button.Select:
					return xboxController.IsBackPressed;
				case XboxControllerInput.Button.Start:
					return xboxController.IsStartPressed;
				}
			}
			if (!isConnected)
			{
				button = PlayerInputRebinding.GetRemappedJoystickButton(button);
				switch (button)
				{
				case XboxControllerInput.Button.ButtonA:
					return MoonInput.GetButton(XboxControllerInput.m_joystickButton0Map[num2]);
				case XboxControllerInput.Button.ButtonX:
					return MoonInput.GetButton(XboxControllerInput.m_joystickButton2Map[num2]);
				case XboxControllerInput.Button.ButtonY:
					return MoonInput.GetButton(XboxControllerInput.m_joystickButton3Map[num2]);
				case XboxControllerInput.Button.ButtonB:
					return MoonInput.GetButton(XboxControllerInput.m_joystickButton1Map[num2]);
				case XboxControllerInput.Button.LeftTrigger:
				{
					bool flag = MoonInput.GetAxis(XboxControllerInput.m_joystickAxis9Map[num]) > 0.5f;
					bool flag2 = MoonInput.GetAxis(XboxControllerInput.m_joystickAxis3Map[num]) > 0.5f;
					return flag || flag2;
				}
				case XboxControllerInput.Button.RightTrigger:
				{
					bool flag3 = MoonInput.GetAxis(XboxControllerInput.m_joystickAxis10Map[num]) > 0.5f;
					bool flag4 = MoonInput.GetAxis(XboxControllerInput.m_joystickAxis3Map[num]) < -0.5f;
					return flag3 || flag4;
				}
				case XboxControllerInput.Button.LeftShoulder:
					return MoonInput.GetButton(XboxControllerInput.m_joystickButton4Map[num2]);
				case XboxControllerInput.Button.RightShoulder:
					return MoonInput.GetButton(XboxControllerInput.m_joystickButton5Map[num2]);
				case XboxControllerInput.Button.LeftStick:
					return MoonInput.GetButton(XboxControllerInput.m_joystickButton8Map[num2]);
				case XboxControllerInput.Button.RightStick:
					return MoonInput.GetButton(XboxControllerInput.m_joystickButton9Map[num2]);
				case XboxControllerInput.Button.Select:
					return MoonInput.GetButton(XboxControllerInput.m_joystickButton6Map[num2]);
				case XboxControllerInput.Button.Start:
					return MoonInput.GetButton(XboxControllerInput.m_joystickButton7Map[num2]);
				case XboxControllerInput.Button.Button10:
					return MoonInput.GetButton(XboxControllerInput.m_joystickButton10Map[num2]);
				case XboxControllerInput.Button.Button11:
					return MoonInput.GetButton(XboxControllerInput.m_joystickButton11Map[num2]);
				}
			}
			return false;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000486C6 File Offset: 0x000468C6
		public static int UserIndexToAxisUserIndex(int userIndex)
		{
			return userIndex;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x000486C9 File Offset: 0x000468C9
		public static int GetAxisUserIndex()
		{
			return XboxControllerInput.UserIndexToAxisUserIndex(XboxLiveController.Instance.GetCurrentUserIndex());
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x000486DC File Offset: 0x000468DC
		public static int AxisIndexToButtonUserIndex(int userIndex)
		{
			int num = userIndex;
			if (num > 0)
			{
				num--;
			}
			return num;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x000486F8 File Offset: 0x000468F8
		public static string GetJoystickXAxisYString(int x, int y)
		{
			try
			{
				switch (y)
				{
				case 1:
					return XboxControllerInput.m_joystickAxis1Map[x];
				case 2:
					return XboxControllerInput.m_joystickAxis2Map[x];
				case 3:
					return XboxControllerInput.m_joystickAxis3Map[x];
				case 4:
					return XboxControllerInput.m_joystickAxis4Map[x];
				case 5:
					return XboxControllerInput.m_joystickAxis5Map[x];
				case 6:
					return XboxControllerInput.m_joystickAxis6Map[x];
				case 7:
					return XboxControllerInput.m_joystickAxis7Map[x];
				case 8:
					return XboxControllerInput.m_joystickAxis8Map[x];
				case 9:
					return XboxControllerInput.m_joystickAxis9Map[x];
				case 10:
					return XboxControllerInput.m_joystickAxis10Map[x];
				default:
					return string.Empty;
				}
			}
			catch (Exception ex)
			{
			}
			return string.Empty;
		}

		// Token: 0x04000C61 RID: 3169
		private const string m_joystick0Axis1 = "Joystick0Axis1";

		// Token: 0x04000C62 RID: 3170
		private const string m_joystick0Axis2 = "Joystick0Axis2";

		// Token: 0x04000C63 RID: 3171
		private const string m_joystick0Axis3 = "Joystick0Axis3";

		// Token: 0x04000C64 RID: 3172
		private const string m_joystick0Axis4 = "Joystick0Axis4";

		// Token: 0x04000C65 RID: 3173
		private const string m_joystick0Axis5 = "Joystick0Axis5";

		// Token: 0x04000C66 RID: 3174
		private const string m_joystick0Axis6 = "Joystick0Axis6";

		// Token: 0x04000C67 RID: 3175
		private const string m_joystick0Axis7 = "Joystick0Axis7";

		// Token: 0x04000C68 RID: 3176
		private const string m_joystick0Axis8 = "Joystick0Axis8";

		// Token: 0x04000C69 RID: 3177
		private const string m_joystick0Axis9 = "Joystick0Axis9";

		// Token: 0x04000C6A RID: 3178
		private const string m_joystick0Axis10 = "Joystick0Axis10";

		// Token: 0x04000C6B RID: 3179
		private const string m_joystick1Axis1 = "Joystick1Axis1";

		// Token: 0x04000C6C RID: 3180
		private const string m_joystick1Axis2 = "Joystick1Axis2";

		// Token: 0x04000C6D RID: 3181
		private const string m_joystick1Axis3 = "Joystick1Axis3";

		// Token: 0x04000C6E RID: 3182
		private const string m_joystick1Axis4 = "Joystick1Axis4";

		// Token: 0x04000C6F RID: 3183
		private const string m_joystick1Axis5 = "Joystick1Axis5";

		// Token: 0x04000C70 RID: 3184
		private const string m_joystick1Axis6 = "Joystick1Axis6";

		// Token: 0x04000C71 RID: 3185
		private const string m_joystick1Axis7 = "Joystick1Axis7";

		// Token: 0x04000C72 RID: 3186
		private const string m_joystick1Axis8 = "Joystick1Axis8";

		// Token: 0x04000C73 RID: 3187
		private const string m_joystick1Axis9 = "Joystick1Axis9";

		// Token: 0x04000C74 RID: 3188
		private const string m_joystick1Axis10 = "Joystick1Axis10";

		// Token: 0x04000C75 RID: 3189
		private const string m_joystick2Axis1 = "Joystick2Axis1";

		// Token: 0x04000C76 RID: 3190
		private const string m_joystick2Axis2 = "Joystick2Axis2";

		// Token: 0x04000C77 RID: 3191
		private const string m_joystick2Axis3 = "Joystick2Axis3";

		// Token: 0x04000C78 RID: 3192
		private const string m_joystick2Axis4 = "Joystick2Axis4";

		// Token: 0x04000C79 RID: 3193
		private const string m_joystick2Axis5 = "Joystick2Axis5";

		// Token: 0x04000C7A RID: 3194
		private const string m_joystick2Axis6 = "Joystick2Axis6";

		// Token: 0x04000C7B RID: 3195
		private const string m_joystick2Axis7 = "Joystick2Axis7";

		// Token: 0x04000C7C RID: 3196
		private const string m_joystick2Axis8 = "Joystick2Axis8";

		// Token: 0x04000C7D RID: 3197
		private const string m_joystick2Axis9 = "Joystick2Axis9";

		// Token: 0x04000C7E RID: 3198
		private const string m_joystick2Axis10 = "Joystick2Axis10";

		// Token: 0x04000C7F RID: 3199
		private const string m_joystick3Axis1 = "Joystick3Axis1";

		// Token: 0x04000C80 RID: 3200
		private const string m_joystick3Axis2 = "Joystick3Axis2";

		// Token: 0x04000C81 RID: 3201
		private const string m_joystick3Axis3 = "Joystick3Axis3";

		// Token: 0x04000C82 RID: 3202
		private const string m_joystick3Axis4 = "Joystick3Axis4";

		// Token: 0x04000C83 RID: 3203
		private const string m_joystick3Axis5 = "Joystick3Axis5";

		// Token: 0x04000C84 RID: 3204
		private const string m_joystick3Axis6 = "Joystick3Axis6";

		// Token: 0x04000C85 RID: 3205
		private const string m_joystick3Axis7 = "Joystick3Axis7";

		// Token: 0x04000C86 RID: 3206
		private const string m_joystick3Axis8 = "Joystick3Axis8";

		// Token: 0x04000C87 RID: 3207
		private const string m_joystick3Axis9 = "Joystick3Axis9";

		// Token: 0x04000C88 RID: 3208
		private const string m_joystick3Axis10 = "Joystick3Axis10";

		// Token: 0x04000C89 RID: 3209
		private const string m_joystick4Axis1 = "Joystick4Axis1";

		// Token: 0x04000C8A RID: 3210
		private const string m_joystick4Axis2 = "Joystick4Axis2";

		// Token: 0x04000C8B RID: 3211
		private const string m_joystick4Axis3 = "Joystick4Axis3";

		// Token: 0x04000C8C RID: 3212
		private const string m_joystick4Axis4 = "Joystick4Axis4";

		// Token: 0x04000C8D RID: 3213
		private const string m_joystick4Axis5 = "Joystick4Axis5";

		// Token: 0x04000C8E RID: 3214
		private const string m_joystick4Axis6 = "Joystick4Axis6";

		// Token: 0x04000C8F RID: 3215
		private const string m_joystick4Axis7 = "Joystick4Axis7";

		// Token: 0x04000C90 RID: 3216
		private const string m_joystick4Axis8 = "Joystick4Axis8";

		// Token: 0x04000C91 RID: 3217
		private const string m_joystick4Axis9 = "Joystick4Axis9";

		// Token: 0x04000C92 RID: 3218
		private const string m_joystick4Axis10 = "Joystick4Axis10";

		// Token: 0x04000C93 RID: 3219
		private const string m_joystick1Button0 = "Joystick1Button0";

		// Token: 0x04000C94 RID: 3220
		private const string m_joystick1Button1 = "Joystick1Button1";

		// Token: 0x04000C95 RID: 3221
		private const string m_joystick1Button2 = "Joystick1Button2";

		// Token: 0x04000C96 RID: 3222
		private const string m_joystick1Button3 = "Joystick1Button3";

		// Token: 0x04000C97 RID: 3223
		private const string m_joystick1Button4 = "Joystick1Button4";

		// Token: 0x04000C98 RID: 3224
		private const string m_joystick1Button5 = "Joystick1Button5";

		// Token: 0x04000C99 RID: 3225
		private const string m_joystick1Button6 = "Joystick1Button6";

		// Token: 0x04000C9A RID: 3226
		private const string m_joystick1Button7 = "Joystick1Button7";

		// Token: 0x04000C9B RID: 3227
		private const string m_joystick1Button8 = "Joystick1Button8";

		// Token: 0x04000C9C RID: 3228
		private const string m_joystick1Button9 = "Joystick1Button9";

		// Token: 0x04000C9D RID: 3229
		private const string m_joystick1Button10 = "Joystick1Button10";

		// Token: 0x04000C9E RID: 3230
		private const string m_joystick1Button11 = "Joystick1Button11";

		// Token: 0x04000C9F RID: 3231
		private const string m_joystick2Button0 = "Joystick2Button0";

		// Token: 0x04000CA0 RID: 3232
		private const string m_joystick2Button1 = "Joystick2Button1";

		// Token: 0x04000CA1 RID: 3233
		private const string m_joystick2Button2 = "Joystick2Button2";

		// Token: 0x04000CA2 RID: 3234
		private const string m_joystick2Button3 = "Joystick2Button3";

		// Token: 0x04000CA3 RID: 3235
		private const string m_joystick2Button4 = "Joystick2Button4";

		// Token: 0x04000CA4 RID: 3236
		private const string m_joystick2Button5 = "Joystick2Button5";

		// Token: 0x04000CA5 RID: 3237
		private const string m_joystick2Button6 = "Joystick2Button6";

		// Token: 0x04000CA6 RID: 3238
		private const string m_joystick2Button7 = "Joystick2Button7";

		// Token: 0x04000CA7 RID: 3239
		private const string m_joystick2Button8 = "Joystick2Button8";

		// Token: 0x04000CA8 RID: 3240
		private const string m_joystick2Button9 = "Joystick2Button9";

		// Token: 0x04000CA9 RID: 3241
		private const string m_joystick2Button10 = "Joystick2Button10";

		// Token: 0x04000CAA RID: 3242
		private const string m_joystick2Button11 = "Joystick2Button11";

		// Token: 0x04000CAB RID: 3243
		private const string m_joystick3Button0 = "Joystick3Button0";

		// Token: 0x04000CAC RID: 3244
		private const string m_joystick3Button1 = "Joystick3Button1";

		// Token: 0x04000CAD RID: 3245
		private const string m_joystick3Button2 = "Joystick3Button2";

		// Token: 0x04000CAE RID: 3246
		private const string m_joystick3Button3 = "Joystick3Button3";

		// Token: 0x04000CAF RID: 3247
		private const string m_joystick3Button4 = "Joystick3Button4";

		// Token: 0x04000CB0 RID: 3248
		private const string m_joystick3Button5 = "Joystick3Button5";

		// Token: 0x04000CB1 RID: 3249
		private const string m_joystick3Button6 = "Joystick3Button6";

		// Token: 0x04000CB2 RID: 3250
		private const string m_joystick3Button7 = "Joystick3Button7";

		// Token: 0x04000CB3 RID: 3251
		private const string m_joystick3Button8 = "Joystick3Button8";

		// Token: 0x04000CB4 RID: 3252
		private const string m_joystick3Button9 = "Joystick3Button9";

		// Token: 0x04000CB5 RID: 3253
		private const string m_joystick3Button10 = "Joystick3Button10";

		// Token: 0x04000CB6 RID: 3254
		private const string m_joystick3Button11 = "Joystick3Button11";

		// Token: 0x04000CB7 RID: 3255
		private const string m_joystick4Button0 = "Joystick4Button0";

		// Token: 0x04000CB8 RID: 3256
		private const string m_joystick4Button1 = "Joystick4Button1";

		// Token: 0x04000CB9 RID: 3257
		private const string m_joystick4Button2 = "Joystick4Button2";

		// Token: 0x04000CBA RID: 3258
		private const string m_joystick4Button3 = "Joystick4Button3";

		// Token: 0x04000CBB RID: 3259
		private const string m_joystick4Button4 = "Joystick4Button4";

		// Token: 0x04000CBC RID: 3260
		private const string m_joystick4Button5 = "Joystick4Button5";

		// Token: 0x04000CBD RID: 3261
		private const string m_joystick4Button6 = "Joystick4Button6";

		// Token: 0x04000CBE RID: 3262
		private const string m_joystick4Button7 = "Joystick4Button7";

		// Token: 0x04000CBF RID: 3263
		private const string m_joystick4Button8 = "Joystick4Button8";

		// Token: 0x04000CC0 RID: 3264
		private const string m_joystick4Button9 = "Joystick4Button9";

		// Token: 0x04000CC1 RID: 3265
		private const string m_joystick4Button10 = "Joystick4Button10";

		// Token: 0x04000CC2 RID: 3266
		private const string m_joystick4Button11 = "Joystick4Button11";

		// Token: 0x04000CC3 RID: 3267
		private static readonly string[] m_joystickAxis1Map = new string[]
		{
			"Joystick0Axis1",
			"Joystick1Axis1",
			"Joystick2Axis1",
			"Joystick3Axis1",
			"Joystick4Axis1"
		};

		// Token: 0x04000CC4 RID: 3268
		private static readonly string[] m_joystickAxis2Map = new string[]
		{
			"Joystick0Axis2",
			"Joystick1Axis2",
			"Joystick2Axis2",
			"Joystick3Axis2",
			"Joystick4Axis2"
		};

		// Token: 0x04000CC5 RID: 3269
		private static readonly string[] m_joystickAxis3Map = new string[]
		{
			"Joystick0Axis3",
			"Joystick1Axis3",
			"Joystick2Axis3",
			"Joystick3Axis3",
			"Joystick4Axis3"
		};

		// Token: 0x04000CC6 RID: 3270
		private static readonly string[] m_joystickAxis4Map = new string[]
		{
			"Joystick0Axis4",
			"Joystick1Axis4",
			"Joystick2Axis4",
			"Joystick3Axis4",
			"Joystick4Axis4"
		};

		// Token: 0x04000CC7 RID: 3271
		private static readonly string[] m_joystickAxis5Map = new string[]
		{
			"Joystick0Axis5",
			"Joystick1Axis5",
			"Joystick2Axis5",
			"Joystick3Axis5",
			"Joystick4Axis5"
		};

		// Token: 0x04000CC8 RID: 3272
		private static readonly string[] m_joystickAxis6Map = new string[]
		{
			"Joystick0Axis6",
			"Joystick1Axis6",
			"Joystick2Axis6",
			"Joystick3Axis6",
			"Joystick4Axis6"
		};

		// Token: 0x04000CC9 RID: 3273
		private static readonly string[] m_joystickAxis7Map = new string[]
		{
			"Joystick0Axis7",
			"Joystick1Axis7",
			"Joystick2Axis7",
			"Joystick3Axis7",
			"Joystick4Axis7"
		};

		// Token: 0x04000CCA RID: 3274
		private static readonly string[] m_joystickAxis8Map = new string[]
		{
			"Joystick0Axis8",
			"Joystick1Axis8",
			"Joystick2Axis8",
			"Joystick3Axis8",
			"Joystick4Axis8"
		};

		// Token: 0x04000CCB RID: 3275
		private static readonly string[] m_joystickAxis9Map = new string[]
		{
			"Joystick0Axis9",
			"Joystick1Axis9",
			"Joystick2Axis9",
			"Joystick3Axis9",
			"Joystick4Axis9"
		};

		// Token: 0x04000CCC RID: 3276
		private static readonly string[] m_joystickAxis10Map = new string[]
		{
			"Joystick0Axis10",
			"Joystick1Axis10",
			"Joystick2Axis10",
			"Joystick3Axis10",
			"Joystick4Axis10"
		};

		// Token: 0x04000CCD RID: 3277
		private static readonly string[] m_joystickButton0Map = new string[]
		{
			"Joystick1Button0",
			"Joystick2Button0",
			"Joystick3Button0",
			"Joystick4Button0"
		};

		// Token: 0x04000CCE RID: 3278
		private static readonly string[] m_joystickButton1Map = new string[]
		{
			"Joystick1Button1",
			"Joystick2Button1",
			"Joystick3Button1",
			"Joystick4Button1"
		};

		// Token: 0x04000CCF RID: 3279
		private static readonly string[] m_joystickButton2Map = new string[]
		{
			"Joystick1Button2",
			"Joystick2Button2",
			"Joystick3Button2",
			"Joystick4Button2"
		};

		// Token: 0x04000CD0 RID: 3280
		private static readonly string[] m_joystickButton3Map = new string[]
		{
			"Joystick1Button3",
			"Joystick2Button3",
			"Joystick3Button3",
			"Joystick4Button3"
		};

		// Token: 0x04000CD1 RID: 3281
		private static readonly string[] m_joystickButton4Map = new string[]
		{
			"Joystick1Button4",
			"Joystick2Button4",
			"Joystick3Button4",
			"Joystick4Button4"
		};

		// Token: 0x04000CD2 RID: 3282
		private static readonly string[] m_joystickButton5Map = new string[]
		{
			"Joystick1Button5",
			"Joystick2Button5",
			"Joystick3Button5",
			"Joystick4Button5"
		};

		// Token: 0x04000CD3 RID: 3283
		private static readonly string[] m_joystickButton6Map = new string[]
		{
			"Joystick1Button6",
			"Joystick2Button6",
			"Joystick3Button6",
			"Joystick4Button6"
		};

		// Token: 0x04000CD4 RID: 3284
		private static readonly string[] m_joystickButton7Map = new string[]
		{
			"Joystick1Button7",
			"Joystick2Button7",
			"Joystick3Button7",
			"Joystick4Button7"
		};

		// Token: 0x04000CD5 RID: 3285
		private static readonly string[] m_joystickButton8Map = new string[]
		{
			"Joystick1Button8",
			"Joystick2Button8",
			"Joystick3Button8",
			"Joystick4Button8"
		};

		// Token: 0x04000CD6 RID: 3286
		private static readonly string[] m_joystickButton9Map = new string[]
		{
			"Joystick1Button9",
			"Joystick2Button9",
			"Joystick3Button9",
			"Joystick4Button9"
		};

		// Token: 0x04000CD7 RID: 3287
		private static readonly string[] m_joystickButton10Map = new string[]
		{
			"Joystick1Button10",
			"Joystick2Button10",
			"Joystick3Button10",
			"Joystick4Button10"
		};

		// Token: 0x04000CD8 RID: 3288
		private static readonly string[] m_joystickButton11Map = new string[]
		{
			"Joystick1Button11",
			"Joystick2Button11",
			"Joystick3Button11",
			"Joystick4Button11"
		};

		// Token: 0x02000194 RID: 404
		public enum Button
		{
			// Token: 0x04000CDA RID: 3290
			ButtonA,
			// Token: 0x04000CDB RID: 3291
			ButtonX,
			// Token: 0x04000CDC RID: 3292
			ButtonY,
			// Token: 0x04000CDD RID: 3293
			ButtonB,
			// Token: 0x04000CDE RID: 3294
			LeftTrigger,
			// Token: 0x04000CDF RID: 3295
			RightTrigger,
			// Token: 0x04000CE0 RID: 3296
			LeftShoulder,
			// Token: 0x04000CE1 RID: 3297
			RightShoulder,
			// Token: 0x04000CE2 RID: 3298
			LeftStick,
			// Token: 0x04000CE3 RID: 3299
			RightStick,
			// Token: 0x04000CE4 RID: 3300
			Select,
			// Token: 0x04000CE5 RID: 3301
			Start,
			// Token: 0x04000CE6 RID: 3302
			Button10,
			// Token: 0x04000CE7 RID: 3303
			Button11
		}

		// Token: 0x02000402 RID: 1026
		public enum Axis
		{
			// Token: 0x04001829 RID: 6185
			LeftStickX,
			// Token: 0x0400182A RID: 6186
			LeftStickY,
			// Token: 0x0400182B RID: 6187
			RightStickX,
			// Token: 0x0400182C RID: 6188
			RightStickY,
			// Token: 0x0400182D RID: 6189
			DpadX,
			// Token: 0x0400182E RID: 6190
			DpadY
		}
	}
}
