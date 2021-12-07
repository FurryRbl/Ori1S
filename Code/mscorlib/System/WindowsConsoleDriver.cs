using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x020001A0 RID: 416
	internal class WindowsConsoleDriver : IConsoleDriver
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x000532AC File Offset: 0x000514AC
		public WindowsConsoleDriver()
		{
			this.outputHandle = WindowsConsoleDriver.GetStdHandle(Handles.STD_OUTPUT);
			this.inputHandle = WindowsConsoleDriver.GetStdHandle(Handles.STD_INPUT);
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			this.defaultAttribute = consoleScreenBufferInfo.Attribute;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000532FC File Offset: 0x000514FC
		private static ConsoleColor GetForeground(short attr)
		{
			attr &= 15;
			return (ConsoleColor)attr;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00053308 File Offset: 0x00051508
		private static ConsoleColor GetBackground(short attr)
		{
			attr &= 240;
			attr = (short)(attr >> 4);
			return (ConsoleColor)attr;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0005331C File Offset: 0x0005151C
		private static short GetAttrForeground(int attr, ConsoleColor color)
		{
			attr &= -16;
			return (short)(attr | (int)color);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00053328 File Offset: 0x00051528
		private static short GetAttrBackground(int attr, ConsoleColor color)
		{
			attr &= -241;
			int num = (int)((int)color << 4);
			return (short)(attr | num);
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x00053348 File Offset: 0x00051548
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x00053378 File Offset: 0x00051578
		public ConsoleColor BackgroundColor
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return WindowsConsoleDriver.GetBackground(consoleScreenBufferInfo.Attribute);
			}
			set
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				short attrBackground = WindowsConsoleDriver.GetAttrBackground((int)consoleScreenBufferInfo.Attribute, value);
				WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, attrBackground);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x000533B8 File Offset: 0x000515B8
		// (set) Token: 0x060014AC RID: 5292 RVA: 0x000533E8 File Offset: 0x000515E8
		public int BufferHeight
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Size.Y;
			}
			set
			{
				this.SetBufferSize(this.BufferWidth, value);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x000533F8 File Offset: 0x000515F8
		// (set) Token: 0x060014AE RID: 5294 RVA: 0x00053428 File Offset: 0x00051628
		public int BufferWidth
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Size.X;
			}
			set
			{
				this.SetBufferSize(value, this.BufferHeight);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x00053438 File Offset: 0x00051638
		public bool CapsLock
		{
			get
			{
				short keyState = WindowsConsoleDriver.GetKeyState(20);
				return (keyState & 1) == 1;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00053454 File Offset: 0x00051654
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x00053484 File Offset: 0x00051684
		public int CursorLeft
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.CursorPosition.X;
			}
			set
			{
				this.SetCursorPosition(value, this.CursorTop);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x00053494 File Offset: 0x00051694
		// (set) Token: 0x060014B3 RID: 5299 RVA: 0x000534C0 File Offset: 0x000516C0
		public int CursorSize
		{
			get
			{
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				return consoleCursorInfo.Size;
			}
			set
			{
				if (value < 1 || value > 100)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				consoleCursorInfo.Size = value;
				if (!WindowsConsoleDriver.SetConsoleCursorInfo(this.outputHandle, ref consoleCursorInfo))
				{
					throw new Exception("SetConsoleCursorInfo failed");
				}
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00053524 File Offset: 0x00051724
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x00053554 File Offset: 0x00051754
		public int CursorTop
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.CursorPosition.Y;
			}
			set
			{
				this.SetCursorPosition(this.CursorLeft, value);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x00053564 File Offset: 0x00051764
		// (set) Token: 0x060014B7 RID: 5303 RVA: 0x00053590 File Offset: 0x00051790
		public bool CursorVisible
		{
			get
			{
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				return consoleCursorInfo.Visible;
			}
			set
			{
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				if (consoleCursorInfo.Visible == value)
				{
					return;
				}
				consoleCursorInfo.Visible = value;
				if (!WindowsConsoleDriver.SetConsoleCursorInfo(this.outputHandle, ref consoleCursorInfo))
				{
					throw new Exception("SetConsoleCursorInfo failed");
				}
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x000535E8 File Offset: 0x000517E8
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x00053618 File Offset: 0x00051818
		public ConsoleColor ForegroundColor
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return WindowsConsoleDriver.GetForeground(consoleScreenBufferInfo.Attribute);
			}
			set
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				short attrForeground = WindowsConsoleDriver.GetAttrForeground((int)consoleScreenBufferInfo.Attribute, value);
				WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, attrForeground);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x00053658 File Offset: 0x00051858
		public bool KeyAvailable
		{
			get
			{
				InputRecord inputRecord = default(InputRecord);
				int num;
				while (WindowsConsoleDriver.PeekConsoleInput(this.inputHandle, out inputRecord, 1, out num))
				{
					if (num == 0)
					{
						return false;
					}
					if (inputRecord.EventType == 1 && inputRecord.KeyDown)
					{
						return true;
					}
					if (!WindowsConsoleDriver.ReadConsoleInput(this.inputHandle, out inputRecord, 1, out num))
					{
						throw new InvalidOperationException("Error in ReadConsoleInput " + Marshal.GetLastWin32Error());
					}
				}
				throw new InvalidOperationException("Error in PeekConsoleInput " + Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x000536F4 File Offset: 0x000518F4
		public bool Initialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x000536F8 File Offset: 0x000518F8
		public int LargestWindowHeight
		{
			get
			{
				Coord largestConsoleWindowSize = WindowsConsoleDriver.GetLargestConsoleWindowSize(this.outputHandle);
				if (largestConsoleWindowSize.X == 0 && largestConsoleWindowSize.Y == 0)
				{
					throw new Exception("GetLargestConsoleWindowSize" + Marshal.GetLastWin32Error());
				}
				return (int)largestConsoleWindowSize.Y;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x0005374C File Offset: 0x0005194C
		public int LargestWindowWidth
		{
			get
			{
				Coord largestConsoleWindowSize = WindowsConsoleDriver.GetLargestConsoleWindowSize(this.outputHandle);
				if (largestConsoleWindowSize.X == 0 && largestConsoleWindowSize.Y == 0)
				{
					throw new Exception("GetLargestConsoleWindowSize" + Marshal.GetLastWin32Error());
				}
				return (int)largestConsoleWindowSize.X;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x000537A0 File Offset: 0x000519A0
		public bool NumberLock
		{
			get
			{
				short keyState = WindowsConsoleDriver.GetKeyState(144);
				return (keyState & 1) == 1;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x000537C0 File Offset: 0x000519C0
		// (set) Token: 0x060014C0 RID: 5312 RVA: 0x00053824 File Offset: 0x00051A24
		public string Title
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				if (WindowsConsoleDriver.GetConsoleTitle(stringBuilder, 1024) == 0)
				{
					stringBuilder = new StringBuilder(26001);
					if (WindowsConsoleDriver.GetConsoleTitle(stringBuilder, 26000) == 0)
					{
						throw new Exception("Got " + Marshal.GetLastWin32Error());
					}
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!WindowsConsoleDriver.SetConsoleTitle(value))
				{
					throw new Exception("Got " + Marshal.GetLastWin32Error());
				}
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x00053868 File Offset: 0x00051A68
		// (set) Token: 0x060014C2 RID: 5314 RVA: 0x000538A8 File Offset: 0x00051AA8
		public bool TreatControlCAsInput
		{
			get
			{
				int num;
				if (!WindowsConsoleDriver.GetConsoleMode(this.outputHandle, out num))
				{
					throw new Exception("Failed in GetConsoleMode: " + Marshal.GetLastWin32Error());
				}
				return (num & 1) == 0;
			}
			set
			{
				int num;
				if (!WindowsConsoleDriver.GetConsoleMode(this.outputHandle, out num))
				{
					throw new Exception("Failed in GetConsoleMode: " + Marshal.GetLastWin32Error());
				}
				bool flag = (num & 1) == 0;
				if (flag == value)
				{
					return;
				}
				if (value)
				{
					num &= -2;
				}
				else
				{
					num++;
				}
				if (!WindowsConsoleDriver.SetConsoleMode(this.outputHandle, num))
				{
					throw new Exception("Failed in SetConsoleMode: " + Marshal.GetLastWin32Error());
				}
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00053930 File Offset: 0x00051B30
		// (set) Token: 0x060014C4 RID: 5316 RVA: 0x00053970 File Offset: 0x00051B70
		public int WindowHeight
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)(consoleScreenBufferInfo.Window.Bottom - consoleScreenBufferInfo.Window.Top + 1);
			}
			set
			{
				this.SetWindowSize(this.WindowWidth, value);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x00053980 File Offset: 0x00051B80
		// (set) Token: 0x060014C6 RID: 5318 RVA: 0x000539B0 File Offset: 0x00051BB0
		public int WindowLeft
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Window.Left;
			}
			set
			{
				this.SetWindowPosition(value, this.WindowTop);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x000539C0 File Offset: 0x00051BC0
		// (set) Token: 0x060014C8 RID: 5320 RVA: 0x000539F0 File Offset: 0x00051BF0
		public int WindowTop
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Window.Top;
			}
			set
			{
				this.SetWindowPosition(this.WindowLeft, value);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00053A00 File Offset: 0x00051C00
		// (set) Token: 0x060014CA RID: 5322 RVA: 0x00053A40 File Offset: 0x00051C40
		public int WindowWidth
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)(consoleScreenBufferInfo.Window.Right - consoleScreenBufferInfo.Window.Left + 1);
			}
			set
			{
				this.SetWindowSize(value, this.WindowHeight);
			}
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00053A50 File Offset: 0x00051C50
		public void Beep(int frequency, int duration)
		{
			WindowsConsoleDriver._Beep(frequency, duration);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00053A5C File Offset: 0x00051C5C
		public void Clear()
		{
			Coord coord = default(Coord);
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			int size = (int)(consoleScreenBufferInfo.Size.X * consoleScreenBufferInfo.Size.Y);
			int num;
			WindowsConsoleDriver.FillConsoleOutputCharacter(this.outputHandle, ' ', size, coord, out num);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			WindowsConsoleDriver.FillConsoleOutputAttribute(this.outputHandle, consoleScreenBufferInfo.Attribute, size, coord, out num);
			WindowsConsoleDriver.SetConsoleCursorPosition(this.outputHandle, coord);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00053AE8 File Offset: 0x00051CE8
		public unsafe void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			if (sourceForeColor < ConsoleColor.Black)
			{
				throw new ArgumentException("Cannot be less than 0.", "sourceForeColor");
			}
			if (sourceBackColor < ConsoleColor.Black)
			{
				throw new ArgumentException("Cannot be less than 0.", "sourceBackColor");
			}
			if (sourceWidth == 0 || sourceHeight == 0)
			{
				return;
			}
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			CharInfo[] array = new CharInfo[sourceWidth * sourceHeight];
			Coord bsize = new Coord(sourceWidth, sourceHeight);
			Coord coord = new Coord(0, 0);
			SmallRect smallRect = new SmallRect(sourceLeft, sourceTop, sourceLeft + sourceWidth - 1, sourceTop + sourceHeight - 1);
			fixed (void* buffer = (void*)(&array[0]))
			{
				if (!WindowsConsoleDriver.ReadConsoleOutput(this.outputHandle, buffer, bsize, coord, ref smallRect))
				{
					throw new ArgumentException(string.Empty, "Cannot read from the specified coordinates.");
				}
			}
			short num = WindowsConsoleDriver.GetAttrForeground(0, sourceForeColor);
			num = WindowsConsoleDriver.GetAttrBackground((int)num, sourceBackColor);
			coord = new Coord(sourceLeft, sourceTop);
			int i = 0;
			while (i < sourceHeight)
			{
				int num2;
				WindowsConsoleDriver.FillConsoleOutputCharacter(this.outputHandle, sourceChar, sourceWidth, coord, out num2);
				WindowsConsoleDriver.FillConsoleOutputAttribute(this.outputHandle, num, sourceWidth, coord, out num2);
				i++;
				coord.Y += 1;
			}
			coord = new Coord(0, 0);
			smallRect = new SmallRect(targetLeft, targetTop, targetLeft + sourceWidth - 1, targetTop + sourceHeight - 1);
			if (!WindowsConsoleDriver.WriteConsoleOutput(this.outputHandle, array, bsize, coord, ref smallRect))
			{
				throw new ArgumentException(string.Empty, "Cannot write to the specified coordinates.");
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00053C5C File Offset: 0x00051E5C
		public void Init()
		{
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00053C60 File Offset: 0x00051E60
		public string ReadLine()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag;
			do
			{
				ConsoleKeyInfo consoleKeyInfo = this.ReadKey(false);
				char keyChar = consoleKeyInfo.KeyChar;
				flag = (keyChar == '\n');
				if (!flag)
				{
					stringBuilder.Append(consoleKeyInfo.KeyChar);
				}
			}
			while (!flag);
			return stringBuilder.ToString();
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00053CAC File Offset: 0x00051EAC
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			InputRecord inputRecord = default(InputRecord);
			int num;
			while (WindowsConsoleDriver.ReadConsoleInput(this.inputHandle, out inputRecord, 1, out num))
			{
				if (inputRecord.EventType == 1 || inputRecord.KeyDown)
				{
					bool alt = (inputRecord.ControlKeyState & 3) != 0;
					bool control = (inputRecord.ControlKeyState & 12) != 0;
					bool shift = (inputRecord.ControlKeyState & 16) != 0;
					return new ConsoleKeyInfo(inputRecord.Character, (ConsoleKey)inputRecord.VirtualKeyCode, shift, alt, control);
				}
			}
			throw new InvalidOperationException("Error in ReadConsoleInput " + Marshal.GetLastWin32Error());
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00053D54 File Offset: 0x00051F54
		public void ResetColor()
		{
			WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, this.defaultAttribute);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00053D68 File Offset: 0x00051F68
		public void SetBufferSize(int width, int height)
		{
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			if (width - 1 > (int)consoleScreenBufferInfo.Window.Right)
			{
				throw new ArgumentOutOfRangeException("width");
			}
			if (height - 1 > (int)consoleScreenBufferInfo.Window.Bottom)
			{
				throw new ArgumentOutOfRangeException("height");
			}
			Coord newSize = new Coord(width, height);
			if (!WindowsConsoleDriver.SetConsoleScreenBufferSize(this.outputHandle, newSize))
			{
				throw new ArgumentOutOfRangeException("height/width", "Cannot be smaller than the window size.");
			}
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00053DF4 File Offset: 0x00051FF4
		public void SetCursorPosition(int left, int top)
		{
			Coord coord = new Coord(left, top);
			WindowsConsoleDriver.SetConsoleCursorPosition(this.outputHandle, coord);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00053E18 File Offset: 0x00052018
		public void SetWindowPosition(int left, int top)
		{
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			SmallRect window = consoleScreenBufferInfo.Window;
			window.Left = (short)left;
			window.Top = (short)top;
			if (!WindowsConsoleDriver.SetConsoleWindowInfo(this.outputHandle, true, ref window))
			{
				throw new ArgumentOutOfRangeException("left/top", "Windows error " + Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00053E88 File Offset: 0x00052088
		public void SetWindowSize(int width, int height)
		{
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			SmallRect window = consoleScreenBufferInfo.Window;
			window.Right = (short)((int)window.Left + width - 1);
			window.Bottom = (short)((int)window.Top + height - 1);
			if (!WindowsConsoleDriver.SetConsoleWindowInfo(this.outputHandle, true, ref window))
			{
				throw new ArgumentOutOfRangeException("left/top", "Windows error " + Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060014D6 RID: 5334
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr GetStdHandle(Handles handle);

		// Token: 0x060014D7 RID: 5335
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "Beep", SetLastError = true)]
		private static extern void _Beep(int frequency, int duration);

		// Token: 0x060014D8 RID: 5336
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetConsoleScreenBufferInfo(IntPtr handle, out ConsoleScreenBufferInfo info);

		// Token: 0x060014D9 RID: 5337
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool FillConsoleOutputCharacter(IntPtr handle, char c, int size, Coord coord, out int written);

		// Token: 0x060014DA RID: 5338
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool FillConsoleOutputAttribute(IntPtr handle, short c, int size, Coord coord, out int written);

		// Token: 0x060014DB RID: 5339
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleCursorPosition(IntPtr handle, Coord coord);

		// Token: 0x060014DC RID: 5340
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleTextAttribute(IntPtr handle, short attribute);

		// Token: 0x060014DD RID: 5341
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleScreenBufferSize(IntPtr handle, Coord newSize);

		// Token: 0x060014DE RID: 5342
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleWindowInfo(IntPtr handle, bool absolute, ref SmallRect rect);

		// Token: 0x060014DF RID: 5343
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int GetConsoleTitle(StringBuilder sb, int size);

		// Token: 0x060014E0 RID: 5344
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleTitle(string title);

		// Token: 0x060014E1 RID: 5345
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetConsoleCursorInfo(IntPtr handle, out ConsoleCursorInfo info);

		// Token: 0x060014E2 RID: 5346
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleCursorInfo(IntPtr handle, ref ConsoleCursorInfo info);

		// Token: 0x060014E3 RID: 5347
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern short GetKeyState(int virtKey);

		// Token: 0x060014E4 RID: 5348
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetConsoleMode(IntPtr handle, out int mode);

		// Token: 0x060014E5 RID: 5349
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleMode(IntPtr handle, int mode);

		// Token: 0x060014E6 RID: 5350
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool PeekConsoleInput(IntPtr handle, out InputRecord record, int length, out int eventsRead);

		// Token: 0x060014E7 RID: 5351
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool ReadConsoleInput(IntPtr handle, out InputRecord record, int length, out int nread);

		// Token: 0x060014E8 RID: 5352
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern Coord GetLargestConsoleWindowSize(IntPtr handle);

		// Token: 0x060014E9 RID: 5353
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private unsafe static extern bool ReadConsoleOutput(IntPtr handle, void* buffer, Coord bsize, Coord bpos, ref SmallRect region);

		// Token: 0x060014EA RID: 5354
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool WriteConsoleOutput(IntPtr handle, CharInfo[] buffer, Coord bsize, Coord bpos, ref SmallRect region);

		// Token: 0x04000844 RID: 2116
		private IntPtr inputHandle;

		// Token: 0x04000845 RID: 2117
		private IntPtr outputHandle;

		// Token: 0x04000846 RID: 2118
		private short defaultAttribute;
	}
}
