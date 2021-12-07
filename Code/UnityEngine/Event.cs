using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001EB RID: 491
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Event
	{
		// Token: 0x06001DBD RID: 7613 RVA: 0x0001C288 File Offset: 0x0001A488
		public Event()
		{
			this.Init(0);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0001C298 File Offset: 0x0001A498
		public Event(int displayIndex)
		{
			this.Init(displayIndex);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0001C2A8 File Offset: 0x0001A4A8
		public Event(Event other)
		{
			if (other == null)
			{
				throw new ArgumentException("Event to copy from is null.");
			}
			this.InitCopy(other);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x0001C2C8 File Offset: 0x0001A4C8
		private Event(IntPtr ptr)
		{
			this.InitPtr(ptr);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0001C2D8 File Offset: 0x0001A4D8
		~Event()
		{
			this.Cleanup();
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x0001C314 File Offset: 0x0001A514
		// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x0001C32C File Offset: 0x0001A52C
		public Vector2 mousePosition
		{
			get
			{
				Vector2 result;
				this.Internal_GetMousePosition(out result);
				return result;
			}
			set
			{
				this.Internal_SetMousePosition(value);
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x0001C338 File Offset: 0x0001A538
		// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x0001C350 File Offset: 0x0001A550
		public Vector2 delta
		{
			get
			{
				Vector2 result;
				this.Internal_GetMouseDelta(out result);
				return result;
			}
			set
			{
				this.Internal_SetMouseDelta(value);
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x0001C35C File Offset: 0x0001A55C
		// (set) Token: 0x06001DC7 RID: 7623 RVA: 0x0001C370 File Offset: 0x0001A570
		[Obsolete("Use HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);", true)]
		public Ray mouseRay
		{
			get
			{
				return new Ray(Vector3.up, Vector3.up);
			}
			set
			{
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0001C374 File Offset: 0x0001A574
		// (set) Token: 0x06001DC9 RID: 7625 RVA: 0x0001C384 File Offset: 0x0001A584
		public bool shift
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Shift;
				}
				else
				{
					this.modifiers |= EventModifiers.Shift;
				}
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x0001C3BC File Offset: 0x0001A5BC
		// (set) Token: 0x06001DCB RID: 7627 RVA: 0x0001C3CC File Offset: 0x0001A5CC
		public bool control
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Control;
				}
				else
				{
					this.modifiers |= EventModifiers.Control;
				}
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x0001C404 File Offset: 0x0001A604
		// (set) Token: 0x06001DCD RID: 7629 RVA: 0x0001C414 File Offset: 0x0001A614
		public bool alt
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Alt;
				}
				else
				{
					this.modifiers |= EventModifiers.Alt;
				}
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x0001C44C File Offset: 0x0001A64C
		// (set) Token: 0x06001DCF RID: 7631 RVA: 0x0001C45C File Offset: 0x0001A65C
		public bool command
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Command;
				}
				else
				{
					this.modifiers |= EventModifiers.Command;
				}
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x0001C494 File Offset: 0x0001A694
		// (set) Token: 0x06001DD1 RID: 7633 RVA: 0x0001C4A8 File Offset: 0x0001A6A8
		public bool capsLock
		{
			get
			{
				return (this.modifiers & EventModifiers.CapsLock) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.CapsLock;
				}
				else
				{
					this.modifiers |= EventModifiers.CapsLock;
				}
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x0001C4E0 File Offset: 0x0001A6E0
		// (set) Token: 0x06001DD3 RID: 7635 RVA: 0x0001C4F4 File Offset: 0x0001A6F4
		public bool numeric
		{
			get
			{
				return (this.modifiers & EventModifiers.Numeric) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Shift;
				}
				else
				{
					this.modifiers |= EventModifiers.Shift;
				}
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x0001C52C File Offset: 0x0001A72C
		public bool functionKey
		{
			get
			{
				return (this.modifiers & EventModifiers.FunctionKey) != EventModifiers.None;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x0001C540 File Offset: 0x0001A740
		// (set) Token: 0x06001DD6 RID: 7638 RVA: 0x0001C548 File Offset: 0x0001A748
		public static Event current
		{
			get
			{
				return Event.s_Current;
			}
			set
			{
				if (value != null)
				{
					Event.s_Current = value;
				}
				else
				{
					Event.s_Current = Event.s_MasterEvent;
				}
				Event.Internal_SetNativeEvent(Event.s_Current.m_Ptr);
			}
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0001C580 File Offset: 0x0001A780
		[RequiredByNativeCode]
		private static void Internal_MakeMasterEventCurrent(int displayIndex)
		{
			if (Event.s_MasterEvent == null)
			{
				Event.s_MasterEvent = new Event(displayIndex);
			}
			Event.s_MasterEvent.displayIndex = displayIndex;
			Event.s_Current = Event.s_MasterEvent;
			Event.Internal_SetNativeEvent(Event.s_MasterEvent.m_Ptr);
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x0001C5BC File Offset: 0x0001A7BC
		public bool isKey
		{
			get
			{
				EventType type = this.type;
				return type == EventType.KeyDown || type == EventType.KeyUp;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
		public bool isMouse
		{
			get
			{
				EventType type = this.type;
				return type == EventType.MouseMove || type == EventType.MouseDown || type == EventType.MouseUp || type == EventType.MouseDrag;
			}
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0001C610 File Offset: 0x0001A810
		public static Event KeyboardEvent(string key)
		{
			Event @event = new Event(0);
			@event.type = EventType.KeyDown;
			if (string.IsNullOrEmpty(key))
			{
				return @event;
			}
			int num = 0;
			bool flag;
			do
			{
				flag = true;
				if (num >= key.Length)
				{
					break;
				}
				char c = key[num];
				switch (c)
				{
				case '#':
					@event.modifiers |= EventModifiers.Shift;
					num++;
					break;
				default:
					if (c != '^')
					{
						flag = false;
					}
					else
					{
						@event.modifiers |= EventModifiers.Control;
						num++;
					}
					break;
				case '%':
					@event.modifiers |= EventModifiers.Command;
					num++;
					break;
				case '&':
					@event.modifiers |= EventModifiers.Alt;
					num++;
					break;
				}
			}
			while (flag);
			string text = key.Substring(num, key.Length - num).ToLower();
			string text2 = text;
			switch (text2)
			{
			case "[0]":
				@event.character = '0';
				@event.keyCode = KeyCode.Keypad0;
				return @event;
			case "[1]":
				@event.character = '1';
				@event.keyCode = KeyCode.Keypad1;
				return @event;
			case "[2]":
				@event.character = '2';
				@event.keyCode = KeyCode.Keypad2;
				return @event;
			case "[3]":
				@event.character = '3';
				@event.keyCode = KeyCode.Keypad3;
				return @event;
			case "[4]":
				@event.character = '4';
				@event.keyCode = KeyCode.Keypad4;
				return @event;
			case "[5]":
				@event.character = '5';
				@event.keyCode = KeyCode.Keypad5;
				return @event;
			case "[6]":
				@event.character = '6';
				@event.keyCode = KeyCode.Keypad6;
				return @event;
			case "[7]":
				@event.character = '7';
				@event.keyCode = KeyCode.Keypad7;
				return @event;
			case "[8]":
				@event.character = '8';
				@event.keyCode = KeyCode.Keypad8;
				return @event;
			case "[9]":
				@event.character = '9';
				@event.keyCode = KeyCode.Keypad9;
				return @event;
			case "[.]":
				@event.character = '.';
				@event.keyCode = KeyCode.KeypadPeriod;
				return @event;
			case "[/]":
				@event.character = '/';
				@event.keyCode = KeyCode.KeypadDivide;
				return @event;
			case "[-]":
				@event.character = '-';
				@event.keyCode = KeyCode.KeypadMinus;
				return @event;
			case "[+]":
				@event.character = '+';
				@event.keyCode = KeyCode.KeypadPlus;
				return @event;
			case "[=]":
				@event.character = '=';
				@event.keyCode = KeyCode.KeypadEquals;
				return @event;
			case "[equals]":
				@event.character = '=';
				@event.keyCode = KeyCode.KeypadEquals;
				return @event;
			case "[enter]":
				@event.character = '\n';
				@event.keyCode = KeyCode.KeypadEnter;
				return @event;
			case "up":
				@event.keyCode = KeyCode.UpArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "down":
				@event.keyCode = KeyCode.DownArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "left":
				@event.keyCode = KeyCode.LeftArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "right":
				@event.keyCode = KeyCode.RightArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "insert":
				@event.keyCode = KeyCode.Insert;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "home":
				@event.keyCode = KeyCode.Home;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "end":
				@event.keyCode = KeyCode.End;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "pgup":
				@event.keyCode = KeyCode.PageDown;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "page up":
				@event.keyCode = KeyCode.PageUp;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "pgdown":
				@event.keyCode = KeyCode.PageUp;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "page down":
				@event.keyCode = KeyCode.PageDown;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "backspace":
				@event.keyCode = KeyCode.Backspace;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "delete":
				@event.keyCode = KeyCode.Delete;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "tab":
				@event.keyCode = KeyCode.Tab;
				return @event;
			case "f1":
				@event.keyCode = KeyCode.F1;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f2":
				@event.keyCode = KeyCode.F2;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f3":
				@event.keyCode = KeyCode.F3;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f4":
				@event.keyCode = KeyCode.F4;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f5":
				@event.keyCode = KeyCode.F5;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f6":
				@event.keyCode = KeyCode.F6;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f7":
				@event.keyCode = KeyCode.F7;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f8":
				@event.keyCode = KeyCode.F8;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f9":
				@event.keyCode = KeyCode.F9;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f10":
				@event.keyCode = KeyCode.F10;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f11":
				@event.keyCode = KeyCode.F11;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f12":
				@event.keyCode = KeyCode.F12;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f13":
				@event.keyCode = KeyCode.F13;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f14":
				@event.keyCode = KeyCode.F14;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f15":
				@event.keyCode = KeyCode.F15;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "[esc]":
				@event.keyCode = KeyCode.Escape;
				return @event;
			case "return":
				@event.character = '\n';
				@event.keyCode = KeyCode.Return;
				@event.modifiers &= ~EventModifiers.FunctionKey;
				return @event;
			case "space":
				@event.keyCode = KeyCode.Space;
				@event.character = ' ';
				@event.modifiers &= ~EventModifiers.FunctionKey;
				return @event;
			}
			if (text.Length != 1)
			{
				try
				{
					@event.keyCode = (KeyCode)((int)Enum.Parse(typeof(KeyCode), text, true));
				}
				catch (ArgumentException)
				{
					Debug.LogError(UnityString.Format("Unable to find key name that matches '{0}'", new object[]
					{
						text
					}));
				}
			}
			else
			{
				@event.character = text.ToLower()[0];
				@event.keyCode = (KeyCode)@event.character;
				if (@event.modifiers != EventModifiers.None)
				{
					@event.character = '\0';
				}
			}
			return @event;
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x0001D0A8 File Offset: 0x0001B2A8
		public override int GetHashCode()
		{
			int num = 1;
			if (this.isKey)
			{
				num = (int)((ushort)this.keyCode);
			}
			if (this.isMouse)
			{
				num = this.mousePosition.GetHashCode();
			}
			return num * 37 | (int)this.modifiers;
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x0001D0F4 File Offset: 0x0001B2F4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != base.GetType())
			{
				return false;
			}
			Event @event = (Event)obj;
			if (this.type != @event.type || (this.modifiers & ~EventModifiers.CapsLock) != (@event.modifiers & ~EventModifiers.CapsLock))
			{
				return false;
			}
			if (this.isKey)
			{
				return this.keyCode == @event.keyCode;
			}
			return this.isMouse && this.mousePosition == @event.mousePosition;
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x0001D194 File Offset: 0x0001B394
		public override string ToString()
		{
			if (this.isKey)
			{
				if (this.character == '\0')
				{
					return UnityString.Format("Event:{0}   Character:\\0   Modifiers:{1}   KeyCode:{2}", new object[]
					{
						this.type,
						this.modifiers,
						this.keyCode
					});
				}
				return string.Concat(new object[]
				{
					"Event:",
					this.type,
					"   Character:",
					(int)this.character,
					"   Modifiers:",
					this.modifiers,
					"   KeyCode:",
					this.keyCode
				});
			}
			else
			{
				if (this.isMouse)
				{
					return UnityString.Format("Event: {0}   Position: {1} Modifiers: {2}", new object[]
					{
						this.type,
						this.mousePosition,
						this.modifiers
					});
				}
				if (this.type == EventType.ExecuteCommand || this.type == EventType.ValidateCommand)
				{
					return UnityString.Format("Event: {0}  \"{1}\"", new object[]
					{
						this.type,
						this.commandName
					});
				}
				return string.Empty + this.type;
			}
		}

		// Token: 0x06001DDE RID: 7646
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init(int displayIndex);

		// Token: 0x06001DDF RID: 7647
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		// Token: 0x06001DE0 RID: 7648
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InitCopy(Event other);

		// Token: 0x06001DE1 RID: 7649
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InitPtr(IntPtr ptr);

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001DE2 RID: 7650
		public extern EventType rawType { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001DE3 RID: 7651
		// (set) Token: 0x06001DE4 RID: 7652
		public extern EventType type { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001DE5 RID: 7653
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern EventType GetTypeForControl(int controlID);

		// Token: 0x06001DE6 RID: 7654 RVA: 0x0001D2F4 File Offset: 0x0001B4F4
		private void Internal_SetMousePosition(Vector2 value)
		{
			Event.INTERNAL_CALL_Internal_SetMousePosition(this, ref value);
		}

		// Token: 0x06001DE7 RID: 7655
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_SetMousePosition(Event self, ref Vector2 value);

		// Token: 0x06001DE8 RID: 7656
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetMousePosition(out Vector2 value);

		// Token: 0x06001DE9 RID: 7657 RVA: 0x0001D300 File Offset: 0x0001B500
		private void Internal_SetMouseDelta(Vector2 value)
		{
			Event.INTERNAL_CALL_Internal_SetMouseDelta(this, ref value);
		}

		// Token: 0x06001DEA RID: 7658
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_SetMouseDelta(Event self, ref Vector2 value);

		// Token: 0x06001DEB RID: 7659
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetMouseDelta(out Vector2 value);

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001DEC RID: 7660
		// (set) Token: 0x06001DED RID: 7661
		public extern int button { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001DEE RID: 7662
		// (set) Token: 0x06001DEF RID: 7663
		public extern EventModifiers modifiers { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001DF0 RID: 7664
		// (set) Token: 0x06001DF1 RID: 7665
		public extern float pressure { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001DF2 RID: 7666
		// (set) Token: 0x06001DF3 RID: 7667
		public extern int clickCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001DF4 RID: 7668
		// (set) Token: 0x06001DF5 RID: 7669
		public extern char character { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001DF6 RID: 7670
		// (set) Token: 0x06001DF7 RID: 7671
		public extern string commandName { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001DF8 RID: 7672
		// (set) Token: 0x06001DF9 RID: 7673
		public extern KeyCode keyCode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001DFA RID: 7674
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetNativeEvent(IntPtr ptr);

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001DFB RID: 7675
		// (set) Token: 0x06001DFC RID: 7676
		public extern int displayIndex { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001DFD RID: 7677
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Use();

		// Token: 0x06001DFE RID: 7678
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool PopEvent(Event outEvent);

		// Token: 0x06001DFF RID: 7679
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetEventCount();

		// Token: 0x0400060B RID: 1547
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x0400060C RID: 1548
		private static Event s_Current;

		// Token: 0x0400060D RID: 1549
		private static Event s_MasterEvent;
	}
}
