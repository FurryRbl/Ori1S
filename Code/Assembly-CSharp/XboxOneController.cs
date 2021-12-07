using System;
using SmartInput;
using UnityEngine;

// Token: 0x02000670 RID: 1648
public class XboxOneController : MonoBehaviour
{
	// Token: 0x17000660 RID: 1632
	// (get) Token: 0x06002822 RID: 10274 RVA: 0x000AE97B File Offset: 0x000ACB7B
	// (set) Token: 0x06002823 RID: 10275 RVA: 0x000AE982 File Offset: 0x000ACB82
	public static Action OnLastControllerDisconnect { get; set; }

	// Token: 0x17000661 RID: 1633
	// (get) Token: 0x06002824 RID: 10276 RVA: 0x000AE98A File Offset: 0x000ACB8A
	// (set) Token: 0x06002825 RID: 10277 RVA: 0x000AE991 File Offset: 0x000ACB91
	public static Action OnActiveControllerDisconnect { get; set; }

	// Token: 0x17000662 RID: 1634
	// (get) Token: 0x06002826 RID: 10278 RVA: 0x000AE999 File Offset: 0x000ACB99
	// (set) Token: 0x06002827 RID: 10279 RVA: 0x000AE9A0 File Offset: 0x000ACBA0
	public static Action<int> OnWillSwitchController { get; set; }

	// Token: 0x06002828 RID: 10280 RVA: 0x000AE9A8 File Offset: 0x000ACBA8
	public static int GetGamepadPressingStart()
	{
		return -1;
	}

	// Token: 0x06002829 RID: 10281 RVA: 0x000AE9AB File Offset: 0x000ACBAB
	public static bool RequireGamepad(Action success = null)
	{
		return false;
	}

	// Token: 0x0600282A RID: 10282 RVA: 0x000AE9AE File Offset: 0x000ACBAE
	public static void UpdateGamepads()
	{
	}

	// Token: 0x17000663 RID: 1635
	// (get) Token: 0x0600282B RID: 10283 RVA: 0x000AE9B0 File Offset: 0x000ACBB0
	// (set) Token: 0x0600282C RID: 10284 RVA: 0x000AE9B3 File Offset: 0x000ACBB3
	public static int ActiveGamepad
	{
		get
		{
			return -1;
		}
		set
		{
		}
	}

	// Token: 0x17000664 RID: 1636
	// (get) Token: 0x0600282D RID: 10285 RVA: 0x000AE9B5 File Offset: 0x000ACBB5
	public static ulong ActiveController
	{
		get
		{
			return 0UL;
		}
	}

	// Token: 0x17000665 RID: 1637
	// (get) Token: 0x0600282E RID: 10286 RVA: 0x000AE9B9 File Offset: 0x000ACBB9
	// (set) Token: 0x0600282F RID: 10287 RVA: 0x000AE9BC File Offset: 0x000ACBBC
	public static bool AutoSwitchController
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	// Token: 0x06002830 RID: 10288 RVA: 0x000AE9BE File Offset: 0x000ACBBE
	public static void ResetCurrentGamepad()
	{
	}

	// Token: 0x06002831 RID: 10289 RVA: 0x000AE9C0 File Offset: 0x000ACBC0
	public static bool MakeFirstGamepadCurrent()
	{
		return false;
	}

	// Token: 0x06002832 RID: 10290 RVA: 0x000AE9C3 File Offset: 0x000ACBC3
	public static bool Vibrate(float left, float right, float leftTrigger, float rightTrigger, float duration)
	{
		return false;
	}

	// Token: 0x06002833 RID: 10291 RVA: 0x000AE9C6 File Offset: 0x000ACBC6
	public static bool ResetControllerVibration()
	{
		return false;
	}

	// Token: 0x06002834 RID: 10292 RVA: 0x000AE9C9 File Offset: 0x000ACBC9
	public static float GetAxis(XboxOneController.Axis axis)
	{
		return 0f;
	}

	// Token: 0x040022DD RID: 8925
	public const float kTriggerPressLimit = 0.5f;

	// Token: 0x040022DE RID: 8926
	private const float kStickSwitchLimit = 0.1f;

	// Token: 0x040022DF RID: 8927
	private const int kButtonCount = 14;

	// Token: 0x040022E0 RID: 8928
	private bool m_lockPressingStart = true;

	// Token: 0x02000671 RID: 1649
	public enum Button
	{
		// Token: 0x040022E5 RID: 8933
		GamepadButtonA = 330,
		// Token: 0x040022E6 RID: 8934
		GamepadButtonB,
		// Token: 0x040022E7 RID: 8935
		GamepadButtonX,
		// Token: 0x040022E8 RID: 8936
		GamepadButtonY,
		// Token: 0x040022E9 RID: 8937
		GamepadButtonLeftShoulder,
		// Token: 0x040022EA RID: 8938
		GamepadButtonRightShoulder,
		// Token: 0x040022EB RID: 8939
		GamepadButtonView,
		// Token: 0x040022EC RID: 8940
		GamepadButtonMenu,
		// Token: 0x040022ED RID: 8941
		GamepadButtonLeftThumbstick,
		// Token: 0x040022EE RID: 8942
		GamepadButtonRightThumbstick,
		// Token: 0x040022EF RID: 8943
		GamepadButtonDPadUp = 342,
		// Token: 0x040022F0 RID: 8944
		GamepadButtonDPadDown,
		// Token: 0x040022F1 RID: 8945
		GamepadButtonDPadLeft,
		// Token: 0x040022F2 RID: 8946
		GamepadButtonDPadRight,
		// Token: 0x040022F3 RID: 8947
		Gamepad1ButtonA = 350,
		// Token: 0x040022F4 RID: 8948
		Gamepad1ButtonB,
		// Token: 0x040022F5 RID: 8949
		Gamepad1ButtonX,
		// Token: 0x040022F6 RID: 8950
		Gamepad1ButtonY,
		// Token: 0x040022F7 RID: 8951
		Gamepad1ButtonLeftShoulder,
		// Token: 0x040022F8 RID: 8952
		Gamepad1ButtonRightShoulder,
		// Token: 0x040022F9 RID: 8953
		Gamepad1ButtonView,
		// Token: 0x040022FA RID: 8954
		Gamepad1ButtonMenu,
		// Token: 0x040022FB RID: 8955
		Gamepad1ButtonLeftThumbstick,
		// Token: 0x040022FC RID: 8956
		Gamepad1ButtonRightThumbstick,
		// Token: 0x040022FD RID: 8957
		Gamepad1ButtonDPadUp = 362,
		// Token: 0x040022FE RID: 8958
		Gamepad1ButtonDPadDown,
		// Token: 0x040022FF RID: 8959
		Gamepad1ButtonDPadLeft,
		// Token: 0x04002300 RID: 8960
		Gamepad1ButtonDPadRight,
		// Token: 0x04002301 RID: 8961
		Gamepad2ButtonA = 370,
		// Token: 0x04002302 RID: 8962
		Gamepad2ButtonB,
		// Token: 0x04002303 RID: 8963
		Gamepad2ButtonX,
		// Token: 0x04002304 RID: 8964
		Gamepad2ButtonY,
		// Token: 0x04002305 RID: 8965
		Gamepad2ButtonLeftShoulder,
		// Token: 0x04002306 RID: 8966
		Gamepad2ButtonRightShoulder,
		// Token: 0x04002307 RID: 8967
		Gamepad2ButtonView,
		// Token: 0x04002308 RID: 8968
		Gamepad2ButtonMenu,
		// Token: 0x04002309 RID: 8969
		Gamepad2ButtonLeftThumbstick,
		// Token: 0x0400230A RID: 8970
		Gamepad2ButtonRightThumbstick,
		// Token: 0x0400230B RID: 8971
		Gamepad2ButtonDPadUp = 382,
		// Token: 0x0400230C RID: 8972
		Gamepad2ButtonDPadDown,
		// Token: 0x0400230D RID: 8973
		Gamepad2ButtonDPadLeft,
		// Token: 0x0400230E RID: 8974
		Gamepad2ButtonDPadRight,
		// Token: 0x0400230F RID: 8975
		Gamepad3ButtonA = 390,
		// Token: 0x04002310 RID: 8976
		Gamepad3ButtonB,
		// Token: 0x04002311 RID: 8977
		Gamepad3ButtonX,
		// Token: 0x04002312 RID: 8978
		Gamepad3ButtonY,
		// Token: 0x04002313 RID: 8979
		Gamepad3ButtonLeftShoulder,
		// Token: 0x04002314 RID: 8980
		Gamepad3ButtonRightShoulder,
		// Token: 0x04002315 RID: 8981
		Gamepad3ButtonView,
		// Token: 0x04002316 RID: 8982
		Gamepad3ButtonMenu,
		// Token: 0x04002317 RID: 8983
		Gamepad3ButtonLeftThumbstick,
		// Token: 0x04002318 RID: 8984
		Gamepad3ButtonRightThumbstick,
		// Token: 0x04002319 RID: 8985
		Gamepad3ButtonDPadUp = 402,
		// Token: 0x0400231A RID: 8986
		Gamepad3ButtonDPadDown,
		// Token: 0x0400231B RID: 8987
		Gamepad3ButtonDPadLeft,
		// Token: 0x0400231C RID: 8988
		Gamepad3ButtonDPadRight,
		// Token: 0x0400231D RID: 8989
		Gamepad4ButtonA = 410,
		// Token: 0x0400231E RID: 8990
		Gamepad4ButtonB,
		// Token: 0x0400231F RID: 8991
		Gamepad4ButtonX,
		// Token: 0x04002320 RID: 8992
		Gamepad4ButtonY,
		// Token: 0x04002321 RID: 8993
		Gamepad4ButtonLeftShoulder,
		// Token: 0x04002322 RID: 8994
		Gamepad4ButtonRightShoulder,
		// Token: 0x04002323 RID: 8995
		Gamepad4ButtonView,
		// Token: 0x04002324 RID: 8996
		Gamepad4ButtonMenu,
		// Token: 0x04002325 RID: 8997
		Gamepad4ButtonLeftThumbstick,
		// Token: 0x04002326 RID: 8998
		Gamepad4ButtonRightThumbstick,
		// Token: 0x04002327 RID: 8999
		Gamepad4ButtonDPadUp = 422,
		// Token: 0x04002328 RID: 9000
		Gamepad4ButtonDPadDown,
		// Token: 0x04002329 RID: 9001
		Gamepad4ButtonDPadLeft,
		// Token: 0x0400232A RID: 9002
		Gamepad4ButtonDPadRight,
		// Token: 0x0400232B RID: 9003
		Gamepad5ButtonA = 430,
		// Token: 0x0400232C RID: 9004
		Gamepad5ButtonB,
		// Token: 0x0400232D RID: 9005
		Gamepad5ButtonX,
		// Token: 0x0400232E RID: 9006
		Gamepad5ButtonY,
		// Token: 0x0400232F RID: 9007
		Gamepad5ButtonLeftShoulder,
		// Token: 0x04002330 RID: 9008
		Gamepad5ButtonRightShoulder,
		// Token: 0x04002331 RID: 9009
		Gamepad5ButtonView,
		// Token: 0x04002332 RID: 9010
		Gamepad5ButtonMenu,
		// Token: 0x04002333 RID: 9011
		Gamepad5ButtonLeftThumbstick,
		// Token: 0x04002334 RID: 9012
		Gamepad5ButtonRightThumbstick,
		// Token: 0x04002335 RID: 9013
		Gamepad5ButtonDPadUp = 442,
		// Token: 0x04002336 RID: 9014
		Gamepad5ButtonDPadDown,
		// Token: 0x04002337 RID: 9015
		Gamepad5ButtonDPadLeft,
		// Token: 0x04002338 RID: 9016
		Gamepad5ButtonDPadRight,
		// Token: 0x04002339 RID: 9017
		Gamepad6ButtonA = 450,
		// Token: 0x0400233A RID: 9018
		Gamepad6ButtonB,
		// Token: 0x0400233B RID: 9019
		Gamepad6ButtonX,
		// Token: 0x0400233C RID: 9020
		Gamepad6ButtonY,
		// Token: 0x0400233D RID: 9021
		Gamepad6ButtonLeftShoulder,
		// Token: 0x0400233E RID: 9022
		Gamepad6ButtonRightShoulder,
		// Token: 0x0400233F RID: 9023
		Gamepad6ButtonView,
		// Token: 0x04002340 RID: 9024
		Gamepad6ButtonMenu,
		// Token: 0x04002341 RID: 9025
		Gamepad6ButtonLeftThumbstick,
		// Token: 0x04002342 RID: 9026
		Gamepad6ButtonRightThumbstick,
		// Token: 0x04002343 RID: 9027
		Gamepad6ButtonDPadUp = 462,
		// Token: 0x04002344 RID: 9028
		Gamepad6ButtonDPadDown,
		// Token: 0x04002345 RID: 9029
		Gamepad6ButtonDPadLeft,
		// Token: 0x04002346 RID: 9030
		Gamepad6ButtonDPadRight,
		// Token: 0x04002347 RID: 9031
		Gamepad7ButtonA = 470,
		// Token: 0x04002348 RID: 9032
		Gamepad7ButtonB,
		// Token: 0x04002349 RID: 9033
		Gamepad7ButtonX,
		// Token: 0x0400234A RID: 9034
		Gamepad7ButtonY,
		// Token: 0x0400234B RID: 9035
		Gamepad7ButtonLeftShoulder,
		// Token: 0x0400234C RID: 9036
		Gamepad7ButtonRightShoulder,
		// Token: 0x0400234D RID: 9037
		Gamepad7ButtonView,
		// Token: 0x0400234E RID: 9038
		Gamepad7ButtonMenu,
		// Token: 0x0400234F RID: 9039
		Gamepad7ButtonLeftThumbstick,
		// Token: 0x04002350 RID: 9040
		Gamepad7ButtonRightThumbstick,
		// Token: 0x04002351 RID: 9041
		Gamepad7ButtonDPadUp = 482,
		// Token: 0x04002352 RID: 9042
		Gamepad7ButtonDPadDown,
		// Token: 0x04002353 RID: 9043
		Gamepad7ButtonDPadLeft,
		// Token: 0x04002354 RID: 9044
		Gamepad7ButtonDPadRight,
		// Token: 0x04002355 RID: 9045
		Gamepad8ButtonA = 490,
		// Token: 0x04002356 RID: 9046
		Gamepad8ButtonB,
		// Token: 0x04002357 RID: 9047
		Gamepad8ButtonX,
		// Token: 0x04002358 RID: 9048
		Gamepad8ButtonY,
		// Token: 0x04002359 RID: 9049
		Gamepad8ButtonLeftShoulder,
		// Token: 0x0400235A RID: 9050
		Gamepad8ButtonRightShoulder,
		// Token: 0x0400235B RID: 9051
		Gamepad8ButtonView,
		// Token: 0x0400235C RID: 9052
		Gamepad8ButtonMenu,
		// Token: 0x0400235D RID: 9053
		Gamepad8ButtonLeftThumbstick,
		// Token: 0x0400235E RID: 9054
		Gamepad8ButtonRightThumbstick,
		// Token: 0x0400235F RID: 9055
		Gamepad8ButtonDPadUp = 502,
		// Token: 0x04002360 RID: 9056
		Gamepad8ButtonDPadDown,
		// Token: 0x04002361 RID: 9057
		Gamepad8ButtonDPadLeft,
		// Token: 0x04002362 RID: 9058
		Gamepad8ButtonDPadRight
	}

	// Token: 0x02000672 RID: 1650
	public enum Axis
	{
		// Token: 0x04002364 RID: 9060
		LeftStickX,
		// Token: 0x04002365 RID: 9061
		LeftStickY,
		// Token: 0x04002366 RID: 9062
		RightStickX,
		// Token: 0x04002367 RID: 9063
		RightStickY,
		// Token: 0x04002368 RID: 9064
		Trigger,
		// Token: 0x04002369 RID: 9065
		LeftTrigger,
		// Token: 0x0400236A RID: 9066
		RightTrigger,
		// Token: 0x0400236B RID: 9067
		DpadX,
		// Token: 0x0400236C RID: 9068
		DpadY,
		// Token: 0x0400236D RID: 9069
		Gamepad1LeftStickX,
		// Token: 0x0400236E RID: 9070
		Gamepad1LeftStickY,
		// Token: 0x0400236F RID: 9071
		Gamepad1RightStickX,
		// Token: 0x04002370 RID: 9072
		Gamepad1RightStickY,
		// Token: 0x04002371 RID: 9073
		Gamepad1Trigger,
		// Token: 0x04002372 RID: 9074
		Gamepad1LeftTrigger,
		// Token: 0x04002373 RID: 9075
		Gamepad1RightTrigger,
		// Token: 0x04002374 RID: 9076
		Gamepad1DpadX,
		// Token: 0x04002375 RID: 9077
		Gamepad1DpadY,
		// Token: 0x04002376 RID: 9078
		Gamepad2LeftStickX,
		// Token: 0x04002377 RID: 9079
		Gamepad2LeftStickY,
		// Token: 0x04002378 RID: 9080
		Gamepad2RightStickX,
		// Token: 0x04002379 RID: 9081
		Gamepad2RightStickY,
		// Token: 0x0400237A RID: 9082
		Gamepad2Trigger,
		// Token: 0x0400237B RID: 9083
		Gamepad2LeftTrigger,
		// Token: 0x0400237C RID: 9084
		Gamepad2RightTrigger,
		// Token: 0x0400237D RID: 9085
		Gamepad2DpadX,
		// Token: 0x0400237E RID: 9086
		Gamepad2DpadY,
		// Token: 0x0400237F RID: 9087
		Gamepad3LeftStickX,
		// Token: 0x04002380 RID: 9088
		Gamepad3LeftStickY,
		// Token: 0x04002381 RID: 9089
		Gamepad3RightStickX,
		// Token: 0x04002382 RID: 9090
		Gamepad3RightStickY,
		// Token: 0x04002383 RID: 9091
		Gamepad3Trigger,
		// Token: 0x04002384 RID: 9092
		Gamepad3LeftTrigger,
		// Token: 0x04002385 RID: 9093
		Gamepad3RightTrigger,
		// Token: 0x04002386 RID: 9094
		Gamepad3DpadX,
		// Token: 0x04002387 RID: 9095
		Gamepad3DpadY,
		// Token: 0x04002388 RID: 9096
		Gamepad4LeftStickX,
		// Token: 0x04002389 RID: 9097
		Gamepad4LeftStickY,
		// Token: 0x0400238A RID: 9098
		Gamepad4RightStickX,
		// Token: 0x0400238B RID: 9099
		Gamepad4RightStickY,
		// Token: 0x0400238C RID: 9100
		Gamepad4Trigger,
		// Token: 0x0400238D RID: 9101
		Gamepad4LeftTrigger,
		// Token: 0x0400238E RID: 9102
		Gamepad4RightTrigger,
		// Token: 0x0400238F RID: 9103
		Gamepad4DpadX,
		// Token: 0x04002390 RID: 9104
		Gamepad4DpadY,
		// Token: 0x04002391 RID: 9105
		Gamepad5LeftStickX,
		// Token: 0x04002392 RID: 9106
		Gamepad5LeftStickY,
		// Token: 0x04002393 RID: 9107
		Gamepad5RightStickX,
		// Token: 0x04002394 RID: 9108
		Gamepad5RightStickY,
		// Token: 0x04002395 RID: 9109
		Gamepad5Trigger,
		// Token: 0x04002396 RID: 9110
		Gamepad5LeftTrigger,
		// Token: 0x04002397 RID: 9111
		Gamepad5RightTrigger,
		// Token: 0x04002398 RID: 9112
		Gamepad5DpadX,
		// Token: 0x04002399 RID: 9113
		Gamepad5DpadY,
		// Token: 0x0400239A RID: 9114
		Gamepad6LeftStickX,
		// Token: 0x0400239B RID: 9115
		Gamepad6LeftStickY,
		// Token: 0x0400239C RID: 9116
		Gamepad6RightStickX,
		// Token: 0x0400239D RID: 9117
		Gamepad6RightStickY,
		// Token: 0x0400239E RID: 9118
		Gamepad6Trigger,
		// Token: 0x0400239F RID: 9119
		Gamepad6LeftTrigger,
		// Token: 0x040023A0 RID: 9120
		Gamepad6RightTrigger,
		// Token: 0x040023A1 RID: 9121
		Gamepad6DpadX,
		// Token: 0x040023A2 RID: 9122
		Gamepad6DpadY,
		// Token: 0x040023A3 RID: 9123
		Gamepad7LeftStickX,
		// Token: 0x040023A4 RID: 9124
		Gamepad7LeftStickY,
		// Token: 0x040023A5 RID: 9125
		Gamepad7RightStickX,
		// Token: 0x040023A6 RID: 9126
		Gamepad7RightStickY,
		// Token: 0x040023A7 RID: 9127
		Gamepad7Trigger,
		// Token: 0x040023A8 RID: 9128
		Gamepad7LeftTrigger,
		// Token: 0x040023A9 RID: 9129
		Gamepad7RightTrigger,
		// Token: 0x040023AA RID: 9130
		Gamepad7DpadX,
		// Token: 0x040023AB RID: 9131
		Gamepad7DpadY,
		// Token: 0x040023AC RID: 9132
		Gamepad8LeftStickX,
		// Token: 0x040023AD RID: 9133
		Gamepad8LeftStickY,
		// Token: 0x040023AE RID: 9134
		Gamepad8RightStickX,
		// Token: 0x040023AF RID: 9135
		Gamepad8RightStickY,
		// Token: 0x040023B0 RID: 9136
		Gamepad8Trigger,
		// Token: 0x040023B1 RID: 9137
		Gamepad8LeftTrigger,
		// Token: 0x040023B2 RID: 9138
		Gamepad8RightTrigger,
		// Token: 0x040023B3 RID: 9139
		Gamepad8DpadX,
		// Token: 0x040023B4 RID: 9140
		Gamepad8DpadY
	}

	// Token: 0x02000673 RID: 1651
	public class ButtonInput : IButtonInput
	{
		// Token: 0x06002835 RID: 10293 RVA: 0x000AE9D0 File Offset: 0x000ACBD0
		public ButtonInput(XboxOneController.Button button, bool anyPad = false)
		{
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x000AE9D8 File Offset: 0x000ACBD8
		public XboxOneController.Button Button
		{
			get
			{
				return (XboxOneController.Button)0;
			}
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x000AE9DB File Offset: 0x000ACBDB
		public bool GetButton()
		{
			return false;
		}
	}

	// Token: 0x02000674 RID: 1652
	public class AxisInput : IAxisInput
	{
		// Token: 0x06002838 RID: 10296 RVA: 0x000AE9DE File Offset: 0x000ACBDE
		public AxisInput(XboxOneController.Axis axis, bool anyPad = false)
		{
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06002839 RID: 10297 RVA: 0x000AE9E6 File Offset: 0x000ACBE6
		public XboxOneController.Axis Axis
		{
			get
			{
				return XboxOneController.Axis.LeftStickX;
			}
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x000AE9E9 File Offset: 0x000ACBE9
		public float AxisValue()
		{
			return 0f;
		}
	}
}
