using System;
using System.Threading;

namespace J2i.Net.XInputWrapper
{
	// Token: 0x02000003 RID: 3
	public class XboxController
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002081 File Offset: 0x00000281
		public static int UpdateFrequency
		{
			get
			{
				return XboxController.updateFrequency;
			}
			set
			{
				XboxController.updateFrequency = value;
				XboxController.waitTime = 1000 / XboxController.updateFrequency;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002099 File Offset: 0x00000299
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020A1 File Offset: 0x000002A1
		public XInputBatteryInformation BatteryInformationGamepad
		{
			get
			{
				return this._batteryInformationGamepad;
			}
			internal set
			{
				this._batteryInformationGamepad = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020AA File Offset: 0x000002AA
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020B2 File Offset: 0x000002B2
		public XInputBatteryInformation BatteryInformationHeadset
		{
			get
			{
				return this._batterInformationHeadset;
			}
			internal set
			{
				this._batterInformationHeadset = value;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020BC File Offset: 0x000002BC
		static XboxController()
		{
			XboxController.SyncLock = new object();
			for (int i = 0; i <= 3; i++)
			{
				XboxController.Controllers[i] = new XboxController(i);
			}
			XboxController.UpdateFrequency = 25;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000D RID: 13 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x0600000E RID: 14 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler<XboxControllerStateChangedEventArgs> StateChanged;

		// Token: 0x0600000F RID: 15 RVA: 0x0000216D File Offset: 0x0000036D
		public static XboxController RetrieveController(int index)
		{
			return XboxController.Controllers[index];
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002176 File Offset: 0x00000376
		private XboxController(int playerIndex)
		{
			this._playerIndex = playerIndex;
			this.gamepadStatePrev.Copy(this.gamepadStateCurrent);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021B0 File Offset: 0x000003B0
		protected void OnStateChanged()
		{
			if (this.StateChanged != null)
			{
				this.StateChanged(this, new XboxControllerStateChangedEventArgs
				{
					CurrentInputState = this.gamepadStateCurrent,
					PreviousInputState = this.gamepadStatePrev
				});
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021F0 File Offset: 0x000003F0
		public XInputCapabilities GetCapabilities()
		{
			XInputCapabilities result = default(XInputCapabilities);
			XInput.XInputGetCapabilities(this._playerIndex, 1, ref result);
			return result;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002215 File Offset: 0x00000415
		public bool IsDPadUpPressed
		{
			get
			{
				return this.gamepadStateCurrent.Gamepad.IsButtonPressed(1);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002228 File Offset: 0x00000428
		public bool IsDPadDownPressed
		{
			get
			{
				return this.gamepadStateCurrent.Gamepad.IsButtonPressed(2);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000223B File Offset: 0x0000043B
		public bool IsDPadLeftPressed
		{
			get
			{
				return this.gamepadStateCurrent.Gamepad.IsButtonPressed(4);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000224E File Offset: 0x0000044E
		public bool IsDPadRightPressed
		{
			get
			{
				return this.gamepadStateCurrent.Gamepad.IsButtonPressed(8);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002261 File Offset: 0x00000461
		public bool IsAPressed
		{
			get
			{
				return this.gamepadStateCurrent.Gamepad.IsButtonPressed(4096);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002278 File Offset: 0x00000478
		public bool IsBPressed
		{
			get
			{
				return this.gamepadStateCurrent.Gamepad.IsButtonPressed(8192);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000228F File Offset: 0x0000048F
		public bool IsXPressed
		{
			get
			{
				return this.gamepadStateCurrent.Gamepad.IsButtonPressed(16384);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022A6 File Offset: 0x000004A6
		public bool IsYPressed
		{
			get
			{
				return this.gamepadStateCurrent.Gamepad.IsButtonPressed(32768);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022BD File Offset: 0x000004BD
		public bool IsBackPressed
		{
			get
			{
				return this.gamepadStateCurrent.Gamepad.IsButtonPressed(32);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022D1 File Offset: 0x000004D1
		public bool IsStartPressed
		{
			get
			{
				return this.gamepadStateCurrent.Gamepad.IsButtonPressed(16);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022E5 File Offset: 0x000004E5
		public bool IsLeftShoulderPressed
		{
			get
			{
				return this.gamepadStateCurrent.Gamepad.IsButtonPressed(256);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022FC File Offset: 0x000004FC
		public bool IsRightShoulderPressed
		{
			get
			{
				return this.gamepadStateCurrent.Gamepad.IsButtonPressed(512);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002313 File Offset: 0x00000513
		public bool IsLeftStickPressed
		{
			get
			{
				return this.gamepadStateCurrent.Gamepad.IsButtonPressed(64);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002327 File Offset: 0x00000527
		public bool IsRightStickPressed
		{
			get
			{
				return this.gamepadStateCurrent.Gamepad.IsButtonPressed(128);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000233E File Offset: 0x0000053E
		public int LeftTrigger
		{
			get
			{
				return (int)this.gamepadStateCurrent.Gamepad.bLeftTrigger;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002350 File Offset: 0x00000550
		public int RightTrigger
		{
			get
			{
				return (int)this.gamepadStateCurrent.Gamepad.bRightTrigger;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002362 File Offset: 0x00000562
		public int LeftThumbStickX
		{
			get
			{
				return (int)this.gamepadStateCurrent.Gamepad.sThumbLX;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002374 File Offset: 0x00000574
		public int LeftThumbStickY
		{
			get
			{
				return (int)this.gamepadStateCurrent.Gamepad.sThumbLY;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002386 File Offset: 0x00000586
		public int RightThumbStickX
		{
			get
			{
				return (int)this.gamepadStateCurrent.Gamepad.sThumbRX;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002398 File Offset: 0x00000598
		public int RightThumbStickY
		{
			get
			{
				return (int)this.gamepadStateCurrent.Gamepad.sThumbRY;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000023AC File Offset: 0x000005AC
		public Point LeftThumbStick
		{
			get
			{
				return new Point
				{
					X = (int)this.gamepadStateCurrent.Gamepad.sThumbLX,
					Y = (int)this.gamepadStateCurrent.Gamepad.sThumbLY
				};
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023F0 File Offset: 0x000005F0
		public Point RightThumbStick
		{
			get
			{
				return new Point
				{
					X = (int)this.gamepadStateCurrent.Gamepad.sThumbRX,
					Y = (int)this.gamepadStateCurrent.Gamepad.sThumbRY
				};
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002432 File Offset: 0x00000632
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000243A File Offset: 0x0000063A
		public bool IsConnected
		{
			get
			{
				return this._isConnected;
			}
			internal set
			{
				this._isConnected = value;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002444 File Offset: 0x00000644
		public static void StartPolling()
		{
			if (!XboxController.isRunning)
			{
				lock (XboxController.SyncLock)
				{
					if (!XboxController.isRunning)
					{
						XboxController.pollingThread = new Thread(new ThreadStart(XboxController.PollerLoop));
						XboxController.pollingThread.Start();
					}
				}
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024A4 File Offset: 0x000006A4
		public static void StopPolling()
		{
			if (XboxController.isRunning)
			{
				XboxController.keepRunning = false;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024B4 File Offset: 0x000006B4
		private static void PollerLoop()
		{
			lock (XboxController.SyncLock)
			{
				if (XboxController.isRunning)
				{
					return;
				}
				XboxController.isRunning = true;
			}
			XboxController.keepRunning = true;
			while (XboxController.keepRunning)
			{
				for (int i = 0; i <= 3; i++)
				{
					XboxController.Controllers[i].UpdateState();
				}
				Thread.Sleep(XboxController.updateFrequency);
			}
			lock (XboxController.SyncLock)
			{
				XboxController.isRunning = false;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002550 File Offset: 0x00000750
		public void UpdateState()
		{
			int num = XInput.XInputGetState(this._playerIndex, ref this.gamepadStateCurrent);
			this.IsConnected = (num == 0);
			if (this.gamepadStateCurrent.PacketNumber != this.gamepadStatePrev.PacketNumber)
			{
				this.OnStateChanged();
			}
			this.gamepadStatePrev.Copy(this.gamepadStateCurrent);
			if (this._stopMotorTimerActive && DateTime.Now >= this._stopMotorTime)
			{
				XInputVibration xinputVibration = new XInputVibration
				{
					LeftMotorSpeed = 0,
					RightMotorSpeed = 0
				};
				XInput.XInputSetState(this._playerIndex, ref xinputVibration);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025EA File Offset: 0x000007EA
		public void Vibrate(double leftMotor, double rightMotor)
		{
			this.Vibrate(leftMotor, rightMotor, TimeSpan.MinValue);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000025FC File Offset: 0x000007FC
		public void Vibrate(double leftMotor, double rightMotor, TimeSpan length)
		{
			leftMotor = Math.Max(0.0, Math.Min(1.0, leftMotor));
			rightMotor = Math.Max(0.0, Math.Min(1.0, rightMotor));
			XInputVibration strength = new XInputVibration
			{
				LeftMotorSpeed = (ushort)(65535.0 * leftMotor),
				RightMotorSpeed = (ushort)(65535.0 * rightMotor)
			};
			this.Vibrate(strength, length);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000267F File Offset: 0x0000087F
		public void Vibrate(XInputVibration strength)
		{
			this._stopMotorTimerActive = false;
			XInput.XInputSetState(this._playerIndex, ref strength);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002698 File Offset: 0x00000898
		public void Vibrate(XInputVibration strength, TimeSpan length)
		{
			XInput.XInputSetState(this._playerIndex, ref strength);
			if (length != TimeSpan.MinValue)
			{
				this._stopMotorTime = DateTime.Now.Add(length);
				this._stopMotorTimerActive = true;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000026DB File Offset: 0x000008DB
		public override string ToString()
		{
			return this._playerIndex.ToString();
		}

		// Token: 0x04000003 RID: 3
		public const int MAX_CONTROLLER_COUNT = 4;

		// Token: 0x04000004 RID: 4
		public const int FIRST_CONTROLLER_INDEX = 0;

		// Token: 0x04000005 RID: 5
		public const int LAST_CONTROLLER_INDEX = 3;

		// Token: 0x04000006 RID: 6
		private int _playerIndex;

		// Token: 0x04000007 RID: 7
		private static bool keepRunning;

		// Token: 0x04000008 RID: 8
		private static int updateFrequency;

		// Token: 0x04000009 RID: 9
		private static int waitTime;

		// Token: 0x0400000A RID: 10
		private static bool isRunning;

		// Token: 0x0400000B RID: 11
		private static object SyncLock;

		// Token: 0x0400000C RID: 12
		private static Thread pollingThread;

		// Token: 0x0400000D RID: 13
		private bool _stopMotorTimerActive;

		// Token: 0x0400000E RID: 14
		private DateTime _stopMotorTime;

		// Token: 0x0400000F RID: 15
		private XInputBatteryInformation _batteryInformationGamepad;

		// Token: 0x04000010 RID: 16
		private XInputBatteryInformation _batterInformationHeadset;

		// Token: 0x04000011 RID: 17
		private XInputState gamepadStatePrev = default(XInputState);

		// Token: 0x04000012 RID: 18
		private XInputState gamepadStateCurrent = default(XInputState);

		// Token: 0x04000013 RID: 19
		private static XboxController[] Controllers = new XboxController[4];

		// Token: 0x04000015 RID: 21
		private bool _isConnected;
	}
}
