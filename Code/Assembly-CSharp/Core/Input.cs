using System;
using Game;
using UnityEngine;

namespace Core
{
	// Token: 0x02000022 RID: 34
	public class Input
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007C59 File Offset: 0x00005E59
		public static int NormalizedHorizontal
		{
			get
			{
				if (Input.Horizontal < -0.4f)
				{
					return -1;
				}
				if (Input.Horizontal > 0.4f)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00007C7E File Offset: 0x00005E7E
		public static float NormalizedVertical
		{
			get
			{
				if (Input.Vertical < -0.6f)
				{
					return -1f;
				}
				if (Input.Vertical > 0.6f)
				{
					return 1f;
				}
				return 0f;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00007CAF File Offset: 0x00005EAF
		public static Vector2 Axis
		{
			get
			{
				return new Vector2(Input.Horizontal, Input.Vertical);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00007CC0 File Offset: 0x00005EC0
		public static Vector2 AnalogAxisLeft
		{
			get
			{
				return new Vector2(Input.HorizontalAnalogLeft, Input.VerticalAnalogLeft);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00007CD1 File Offset: 0x00005ED1
		public static Vector2 AnalogAxisRight
		{
			get
			{
				return new Vector2(Input.HorizontalAnalogRight, Input.VerticalAnalogRight);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00007CE2 File Offset: 0x00005EE2
		public static Vector2 DigiPadAxis
		{
			get
			{
				return new Vector2((float)Input.HorizontalDigiPad, (float)Input.VerticalDigiPad);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00007CF8 File Offset: 0x00005EF8
		public static Vector2 CursorPositionUI
		{
			get
			{
				Camera camera = UI.Cameras.System.GUICamera.Camera;
				Vector2 cursorPosition = Input.CursorPosition;
				return camera.ViewportToWorldPoint(cursorPosition);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00007D2C File Offset: 0x00005F2C
		public static bool OnAnyButtonPressed
		{
			get
			{
				for (int i = 0; i < Input.Buttons.Length; i++)
				{
					if (Input.Buttons[i].OnPressed)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00007D68 File Offset: 0x00005F68
		public static bool AnyButtonPressed
		{
			get
			{
				for (int i = 0; i < Input.Buttons.Length; i++)
				{
					if (Input.Buttons[i].IsPressed)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public static bool AnyButtonReleased
		{
			get
			{
				for (int i = 0; i < Input.Buttons.Length; i++)
				{
					if (Input.Buttons[i].Released)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00007DE0 File Offset: 0x00005FE0
		public static bool OnAnyButtonReleased
		{
			get
			{
				for (int i = 0; i < Input.Buttons.Length; i++)
				{
					if (Input.Buttons[i].OnReleased)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007E1C File Offset: 0x0000601C
		public static Input.InputButtonProcessor GetButton(Input.Button button)
		{
			switch (button)
			{
			case Input.Button.ButtonA:
				return Input.Jump;
			case Input.Button.ButtonX:
				return Input.SpiritFlame;
			case Input.Button.ButtonY:
				return Input.Bash;
			case Input.Button.ButtonB:
				return Input.SoulFlame;
			case Input.Button.LeftTrigger:
				return Input.ChargeJump;
			case Input.Button.RightTrigger:
				return Input.Glide;
			case Input.Button.LeftShoulder:
				return Input.LeftShoulder;
			case Input.Button.RightShoulder:
				return Input.RightShoulder;
			case Input.Button.Left:
				return Input.Left;
			case Input.Button.Right:
				return Input.Right;
			case Input.Button.Up:
				return Input.Up;
			case Input.Button.Down:
				return Input.Down;
			case Input.Button.LeftStick:
				return Input.LeftStick;
			case Input.Button.RightStick:
				return Input.RightStick;
			}
			return Input.Unassigned;
		}

		// Token: 0x0400014D RID: 333
		public static float Horizontal;

		// Token: 0x0400014E RID: 334
		public static float Vertical;

		// Token: 0x0400014F RID: 335
		public static int HorizontalDigiPad;

		// Token: 0x04000150 RID: 336
		public static int VerticalDigiPad;

		// Token: 0x04000151 RID: 337
		public static float HorizontalAnalogLeft;

		// Token: 0x04000152 RID: 338
		public static float VerticalAnalogLeft;

		// Token: 0x04000153 RID: 339
		public static float HorizontalAnalogRight;

		// Token: 0x04000154 RID: 340
		public static float VerticalAnalogRight;

		// Token: 0x04000155 RID: 341
		public static Input.InputButtonProcessor Down = new Input.InputButtonProcessor();

		// Token: 0x04000156 RID: 342
		public static Input.InputButtonProcessor Up = new Input.InputButtonProcessor();

		// Token: 0x04000157 RID: 343
		public static Input.InputButtonProcessor Left = new Input.InputButtonProcessor();

		// Token: 0x04000158 RID: 344
		public static Input.InputButtonProcessor Right = new Input.InputButtonProcessor();

		// Token: 0x04000159 RID: 345
		public static Input.InputButtonProcessor Jump = new Input.InputButtonProcessor();

		// Token: 0x0400015A RID: 346
		public static Input.InputButtonProcessor SpiritFlame = new Input.InputButtonProcessor();

		// Token: 0x0400015B RID: 347
		public static Input.InputButtonProcessor Bash = new Input.InputButtonProcessor();

		// Token: 0x0400015C RID: 348
		public static Input.InputButtonProcessor SoulFlame = new Input.InputButtonProcessor();

		// Token: 0x0400015D RID: 349
		public static Input.InputButtonProcessor ChargeJump = new Input.InputButtonProcessor();

		// Token: 0x0400015E RID: 350
		public static Input.InputButtonProcessor Glide = new Input.InputButtonProcessor();

		// Token: 0x0400015F RID: 351
		public static Input.InputButtonProcessor Grab = new Input.InputButtonProcessor();

		// Token: 0x04000160 RID: 352
		public static Input.InputButtonProcessor ZoomIn = new Input.InputButtonProcessor();

		// Token: 0x04000161 RID: 353
		public static Input.InputButtonProcessor ZoomOut = new Input.InputButtonProcessor();

		// Token: 0x04000162 RID: 354
		public static Input.InputButtonProcessor LeftShoulder = new Input.InputButtonProcessor();

		// Token: 0x04000163 RID: 355
		public static Input.InputButtonProcessor RightShoulder = new Input.InputButtonProcessor();

		// Token: 0x04000164 RID: 356
		public static Input.InputButtonProcessor Start = new Input.InputButtonProcessor();

		// Token: 0x04000165 RID: 357
		public static Input.InputButtonProcessor AnyStart = new Input.InputButtonProcessor();

		// Token: 0x04000166 RID: 358
		public static Input.InputButtonProcessor Select = new Input.InputButtonProcessor();

		// Token: 0x04000167 RID: 359
		public static Input.InputButtonProcessor Unassigned = new Input.InputButtonProcessor();

		// Token: 0x04000168 RID: 360
		public static Input.InputButtonProcessor LeftStick = new Input.InputButtonProcessor();

		// Token: 0x04000169 RID: 361
		public static Input.InputButtonProcessor RightStick = new Input.InputButtonProcessor();

		// Token: 0x0400016A RID: 362
		public static Input.InputButtonProcessor MenuDown = new Input.InputButtonProcessor();

		// Token: 0x0400016B RID: 363
		public static Input.InputButtonProcessor MenuUp = new Input.InputButtonProcessor();

		// Token: 0x0400016C RID: 364
		public static Input.InputButtonProcessor MenuLeft = new Input.InputButtonProcessor();

		// Token: 0x0400016D RID: 365
		public static Input.InputButtonProcessor MenuRight = new Input.InputButtonProcessor();

		// Token: 0x0400016E RID: 366
		public static Input.InputButtonProcessor MenuPageLeft = new Input.InputButtonProcessor();

		// Token: 0x0400016F RID: 367
		public static Input.InputButtonProcessor MenuPageRight = new Input.InputButtonProcessor();

		// Token: 0x04000170 RID: 368
		public static Input.InputButtonProcessor ActionButtonA = new Input.InputButtonProcessor();

		// Token: 0x04000171 RID: 369
		public static Input.InputButtonProcessor Cancel = new Input.InputButtonProcessor();

		// Token: 0x04000172 RID: 370
		public static Input.InputButtonProcessor LeftClick = new Input.InputButtonProcessor();

		// Token: 0x04000173 RID: 371
		public static Input.InputButtonProcessor RightClick = new Input.InputButtonProcessor();

		// Token: 0x04000174 RID: 372
		public static Input.InputButtonProcessor Copy = new Input.InputButtonProcessor();

		// Token: 0x04000175 RID: 373
		public static Input.InputButtonProcessor Delete = new Input.InputButtonProcessor();

		// Token: 0x04000176 RID: 374
		public static Input.InputButtonProcessor Focus = new Input.InputButtonProcessor();

		// Token: 0x04000177 RID: 375
		public static Input.InputButtonProcessor Filter = new Input.InputButtonProcessor();

		// Token: 0x04000178 RID: 376
		public static Input.InputButtonProcessor Legend = new Input.InputButtonProcessor();

		// Token: 0x04000179 RID: 377
		public static Vector2 CursorPosition;

		// Token: 0x0400017A RID: 378
		public static bool CursorMoved;

		// Token: 0x0400017B RID: 379
		public static Input.InputButtonProcessor[] Buttons = new Input.InputButtonProcessor[]
		{
			Input.Down,
			Input.Up,
			Input.Left,
			Input.Right,
			Input.Jump,
			Input.SpiritFlame,
			Input.Bash,
			Input.SoulFlame,
			Input.ChargeJump,
			Input.Glide,
			Input.Grab,
			Input.LeftShoulder,
			Input.RightShoulder,
			Input.Start,
			Input.AnyStart,
			Input.Select,
			Input.LeftStick,
			Input.RightStick,
			Input.MenuDown,
			Input.MenuUp,
			Input.MenuLeft,
			Input.MenuRight,
			Input.MenuPageLeft,
			Input.MenuPageRight,
			Input.ActionButtonA,
			Input.ZoomIn,
			Input.ZoomOut,
			Input.Cancel,
			Input.Copy,
			Input.Delete,
			Input.Focus,
			Input.Filter,
			Input.Legend
		};

		// Token: 0x02000024 RID: 36
		public class InputButtonProcessor
		{
			// Token: 0x060001CA RID: 458 RVA: 0x00007ED7 File Offset: 0x000060D7
			public void Update(bool isPressed)
			{
				this.WasPressed = this.IsPressed;
				this.IsPressed = isPressed;
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x060001CB RID: 459 RVA: 0x00007EEC File Offset: 0x000060EC
			public bool OnPressed
			{
				get
				{
					return this.IsPressed && !this.WasPressed;
				}
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x060001CC RID: 460 RVA: 0x00007F08 File Offset: 0x00006108
			public bool OnPressedNotUsed
			{
				get
				{
					return this.IsPressed && !this.WasPressed && !this.Used;
				}
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x060001CD RID: 461 RVA: 0x00007F37 File Offset: 0x00006137
			public bool OnReleased
			{
				get
				{
					return !this.IsPressed && this.WasPressed;
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x060001CE RID: 462 RVA: 0x00007F4D File Offset: 0x0000614D
			public bool Pressed
			{
				get
				{
					return this.IsPressed;
				}
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x060001CF RID: 463 RVA: 0x00007F55 File Offset: 0x00006155
			public bool Released
			{
				get
				{
					return !this.IsPressed;
				}
			}

			// Token: 0x0400017F RID: 383
			public bool WasPressed;

			// Token: 0x04000180 RID: 384
			public bool IsPressed;

			// Token: 0x04000181 RID: 385
			public bool Used;
		}

		// Token: 0x020000CC RID: 204
		public enum Button
		{
			// Token: 0x040006F3 RID: 1779
			ButtonA,
			// Token: 0x040006F4 RID: 1780
			ButtonX,
			// Token: 0x040006F5 RID: 1781
			ButtonY,
			// Token: 0x040006F6 RID: 1782
			ButtonB,
			// Token: 0x040006F7 RID: 1783
			LeftTrigger,
			// Token: 0x040006F8 RID: 1784
			RightTrigger,
			// Token: 0x040006F9 RID: 1785
			LeftShoulder,
			// Token: 0x040006FA RID: 1786
			RightShoulder,
			// Token: 0x040006FB RID: 1787
			Left,
			// Token: 0x040006FC RID: 1788
			Right,
			// Token: 0x040006FD RID: 1789
			Up,
			// Token: 0x040006FE RID: 1790
			Down,
			// Token: 0x040006FF RID: 1791
			Unassigned,
			// Token: 0x04000700 RID: 1792
			Any,
			// Token: 0x04000701 RID: 1793
			LeftStick,
			// Token: 0x04000702 RID: 1794
			RightStick
		}
	}
}
