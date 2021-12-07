using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x0200017A RID: 378
	internal class TermInfoDriver : IConsoleDriver
	{
		// Token: 0x060013A6 RID: 5030 RVA: 0x0004E1E4 File Offset: 0x0004C3E4
		public TermInfoDriver() : this(Environment.GetEnvironmentVariable("TERM"))
		{
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0004E1F8 File Offset: 0x0004C3F8
		public TermInfoDriver(string term)
		{
			this.term = term;
			if (term == "xterm")
			{
				this.reader = new TermInfoReader(term, KnownTerminals.xterm);
				this.color16 = true;
			}
			else if (term == "linux")
			{
				this.reader = new TermInfoReader(term, KnownTerminals.linux);
				this.color16 = true;
			}
			else
			{
				string text = TermInfoDriver.SearchTerminfo(term);
				if (text != null)
				{
					this.reader = new TermInfoReader(term, text);
				}
			}
			if (this.reader == null)
			{
				this.reader = new TermInfoReader(term, KnownTerminals.ansi);
			}
			if (!(Console.stdout is CStreamWriter))
			{
				this.stdout = new CStreamWriter(Console.OpenStandardOutput(0), Console.OutputEncoding);
				this.stdout.AutoFlush = true;
			}
			else
			{
				this.stdout = (CStreamWriter)Console.stdout;
			}
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0004E530 File Offset: 0x0004C730
		private static string SearchTerminfo(string term)
		{
			if (term == null || term == string.Empty)
			{
				return null;
			}
			foreach (string text in TermInfoDriver.locations)
			{
				if (Directory.Exists(text))
				{
					string text2 = Path.Combine(text, term.Substring(0, 1));
					if (Directory.Exists(text))
					{
						text2 = Path.Combine(text2, term);
						if (File.Exists(text2))
						{
							return text2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0004E5C0 File Offset: 0x0004C7C0
		private void WriteConsole(string str)
		{
			if (str == null)
			{
				return;
			}
			this.stdout.InternalWriteString(str);
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x0004E5D8 File Offset: 0x0004C7D8
		public bool Initialized
		{
			get
			{
				return this.inited;
			}
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0004E5E0 File Offset: 0x0004C7E0
		public void Init()
		{
			if (this.inited)
			{
				return;
			}
			object obj = this.initLock;
			lock (obj)
			{
				if (!this.inited)
				{
					this.inited = true;
					if (!ConsoleDriver.IsConsole)
					{
						throw new IOException("Not a tty.");
					}
					ConsoleDriver.SetEcho(false);
					string text = null;
					this.keypadXmit = this.reader.Get(TermInfoStrings.KeypadXmit);
					this.keypadLocal = this.reader.Get(TermInfoStrings.KeypadLocal);
					if (this.keypadXmit != null)
					{
						this.WriteConsole(this.keypadXmit);
						if (this.keypadLocal != null)
						{
							text += this.keypadLocal;
						}
					}
					this.origPair = this.reader.Get(TermInfoStrings.OrigPair);
					this.origColors = this.reader.Get(TermInfoStrings.OrigColors);
					this.setfgcolor = TermInfoDriver.MangleParameters(this.reader.Get(TermInfoStrings.SetAForeground));
					this.setbgcolor = TermInfoDriver.MangleParameters(this.reader.Get(TermInfoStrings.SetABackground));
					this.setlfgcolor = ((!this.color16) ? this.setfgcolor : this.setfgcolor.Replace("[3", "[9"));
					this.setlbgcolor = ((!this.color16) ? this.setbgcolor : this.setbgcolor.Replace("[4", "[10"));
					string text2 = (this.origColors != null) ? this.origColors : this.origPair;
					if (text2 != null)
					{
						text += text2;
					}
					if (!ConsoleDriver.TtySetup(this.keypadXmit, text, out this.control_characters, out TermInfoDriver.native_terminal_size))
					{
						throw new IOException("Error initializing terminal.");
					}
					this.stdin = new StreamReader(Console.OpenStandardInput(0), Console.InputEncoding);
					this.clear = this.reader.Get(TermInfoStrings.ClearScreen);
					this.bell = this.reader.Get(TermInfoStrings.Bell);
					if (this.clear == null)
					{
						this.clear = this.reader.Get(TermInfoStrings.CursorHome);
						this.clear += this.reader.Get(TermInfoStrings.ClrEos);
					}
					this.csrVisible = this.reader.Get(TermInfoStrings.CursorNormal);
					if (this.csrVisible == null)
					{
						this.csrVisible = this.reader.Get(TermInfoStrings.CursorVisible);
					}
					this.csrInvisible = this.reader.Get(TermInfoStrings.CursorInvisible);
					if (this.term == "cygwin" || this.term == "linux" || (this.term != null && this.term.StartsWith("xterm")) || this.term == "rxvt" || this.term == "dtterm")
					{
						this.titleFormat = "\u001b]0;{0}\a";
					}
					else if (this.term == "iris-ansi")
					{
						this.titleFormat = "\u001bP1.y{0}\u001b\\";
					}
					else if (this.term == "sun-cmd")
					{
						this.titleFormat = "\u001b]l{0}\u001b\\";
					}
					this.cursorAddress = this.reader.Get(TermInfoStrings.CursorAddress);
					if (this.cursorAddress != null)
					{
						string text3 = this.cursorAddress.Replace("%i", string.Empty);
						this.home_1_1 = (this.cursorAddress != text3);
						this.cursorAddress = TermInfoDriver.MangleParameters(text3);
					}
					this.GetCursorPosition();
					if (this.noGetPosition)
					{
						this.WriteConsole(this.clear);
						this.cursorLeft = 0;
						this.cursorTop = 0;
					}
				}
			}
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0004E9D0 File Offset: 0x0004CBD0
		private static string MangleParameters(string str)
		{
			if (str == null)
			{
				return null;
			}
			str = str.Replace("{", "{{");
			str = str.Replace("}", "}}");
			str = str.Replace("%p1%d", "{0}");
			return str.Replace("%p2%d", "{1}");
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0004EA2C File Offset: 0x0004CC2C
		private static int TranslateColor(ConsoleColor desired, out bool light)
		{
			switch (desired)
			{
			case ConsoleColor.Black:
				light = false;
				return 0;
			case ConsoleColor.DarkBlue:
				light = false;
				return 4;
			case ConsoleColor.DarkGreen:
				light = false;
				return 2;
			case ConsoleColor.DarkCyan:
				light = false;
				return 6;
			case ConsoleColor.DarkRed:
				light = false;
				return 1;
			case ConsoleColor.DarkMagenta:
				light = false;
				return 5;
			case ConsoleColor.DarkYellow:
				light = false;
				return 3;
			case ConsoleColor.Gray:
				light = false;
				return 7;
			case ConsoleColor.DarkGray:
				light = true;
				return 0;
			case ConsoleColor.Blue:
				light = true;
				return 4;
			case ConsoleColor.Green:
				light = true;
				return 2;
			case ConsoleColor.Cyan:
				light = true;
				return 6;
			case ConsoleColor.Red:
				light = true;
				return 1;
			case ConsoleColor.Magenta:
				light = true;
				return 5;
			case ConsoleColor.Yellow:
				light = true;
				return 3;
			case ConsoleColor.White:
				light = true;
				return 7;
			default:
				light = false;
				return 0;
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0004EADC File Offset: 0x0004CCDC
		private void IncrementX()
		{
			this.cursorLeft++;
			if (this.cursorLeft >= this.WindowWidth)
			{
				this.cursorTop++;
				this.cursorLeft = 0;
				if (this.cursorTop >= this.WindowHeight)
				{
					if (this.rl_starty != -1)
					{
						this.rl_starty--;
					}
					this.cursorTop--;
				}
			}
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0004EB58 File Offset: 0x0004CD58
		public void WriteSpecialKey(ConsoleKeyInfo key)
		{
			switch (key.Key)
			{
			case ConsoleKey.Backspace:
				if (this.cursorLeft > 0)
				{
					if (this.cursorLeft > this.rl_startx || this.cursorTop != this.rl_starty)
					{
						this.cursorLeft--;
						this.SetCursorPosition(this.cursorLeft, this.cursorTop);
						this.WriteConsole(" ");
						this.SetCursorPosition(this.cursorLeft, this.cursorTop);
					}
				}
				break;
			case ConsoleKey.Tab:
			{
				int num = 8 - this.cursorLeft % 8;
				for (int i = 0; i < num; i++)
				{
					this.IncrementX();
				}
				this.WriteConsole("\t");
				break;
			}
			case ConsoleKey.Clear:
				this.WriteConsole(this.clear);
				this.cursorLeft = 0;
				this.cursorTop = 0;
				break;
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0004EC64 File Offset: 0x0004CE64
		public void WriteSpecialKey(char c)
		{
			this.WriteSpecialKey(this.CreateKeyInfoFromInt((int)c, false));
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0004EC74 File Offset: 0x0004CE74
		public bool IsSpecialKey(ConsoleKeyInfo key)
		{
			if (!this.inited)
			{
				return false;
			}
			switch (key.Key)
			{
			case ConsoleKey.Backspace:
				return true;
			case ConsoleKey.Tab:
				return true;
			case ConsoleKey.Clear:
				return true;
			case ConsoleKey.Enter:
				this.cursorLeft = 0;
				this.cursorTop++;
				if (this.cursorTop >= this.WindowHeight)
				{
					this.cursorTop--;
				}
				return false;
			}
			this.IncrementX();
			return false;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0004ED00 File Offset: 0x0004CF00
		public bool IsSpecialKey(char c)
		{
			return this.IsSpecialKey(this.CreateKeyInfoFromInt((int)c, false));
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x0004ED10 File Offset: 0x0004CF10
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x0004ED2C File Offset: 0x0004CF2C
		public ConsoleColor BackgroundColor
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.bgcolor;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.bgcolor = value;
				bool flag;
				int num = TermInfoDriver.TranslateColor(value, out flag);
				if (flag)
				{
					this.WriteConsole(string.Format(this.setlbgcolor, num));
				}
				else
				{
					this.WriteConsole(string.Format(this.setbgcolor, num));
				}
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x0004ED94 File Offset: 0x0004CF94
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x0004EDB0 File Offset: 0x0004CFB0
		public ConsoleColor ForegroundColor
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.fgcolor;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.fgcolor = value;
				bool flag;
				int num = TermInfoDriver.TranslateColor(value, out flag);
				if (flag)
				{
					this.WriteConsole(string.Format(this.setlfgcolor, num));
				}
				else
				{
					this.WriteConsole(string.Format(this.setfgcolor, num));
				}
			}
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0004EE18 File Offset: 0x0004D018
		private void GetCursorPosition()
		{
			int num = 0;
			int num2 = 0;
			int num3 = ConsoleDriver.InternalKeyAvailable(1000);
			int num4;
			while (num3-- > 0)
			{
				num4 = this.stdin.Read();
				this.AddToBuffer(num4);
			}
			this.WriteConsole("\u001b[6n");
			if (ConsoleDriver.InternalKeyAvailable(1000) <= 0)
			{
				this.noGetPosition = true;
				return;
			}
			for (num4 = this.stdin.Read(); num4 != 27; num4 = this.stdin.Read())
			{
				this.AddToBuffer(num4);
				if (ConsoleDriver.InternalKeyAvailable(100) <= 0)
				{
					return;
				}
			}
			num4 = this.stdin.Read();
			if (num4 != 91)
			{
				this.AddToBuffer(27);
				this.AddToBuffer(num4);
				return;
			}
			num4 = this.stdin.Read();
			if (num4 != 59)
			{
				num = num4 - 48;
				num4 = this.stdin.Read();
				while (num4 >= 48 && num4 <= 57)
				{
					num = num * 10 + num4 - 48;
					num4 = this.stdin.Read();
				}
				num--;
			}
			num4 = this.stdin.Read();
			if (num4 != 82)
			{
				num2 = num4 - 48;
				num4 = this.stdin.Read();
				while (num4 >= 48 && num4 <= 57)
				{
					num2 = num2 * 10 + num4 - 48;
					num4 = this.stdin.Read();
				}
				num2--;
			}
			this.cursorLeft = num2;
			this.cursorTop = num;
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0004EF90 File Offset: 0x0004D190
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x0004EFAC File Offset: 0x0004D1AC
		public int BufferHeight
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.bufferHeight;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0004EFC4 File Offset: 0x0004D1C4
		// (set) Token: 0x060013BC RID: 5052 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public int BufferWidth
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.bufferWidth;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0004EFF8 File Offset: 0x0004D1F8
		public bool CapsLock
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0004F010 File Offset: 0x0004D210
		// (set) Token: 0x060013BF RID: 5055 RVA: 0x0004F02C File Offset: 0x0004D22C
		public int CursorLeft
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.cursorLeft;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.SetCursorPosition(value, this.CursorTop);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0004F058 File Offset: 0x0004D258
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x0004F074 File Offset: 0x0004D274
		public int CursorTop
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.cursorTop;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.SetCursorPosition(this.CursorLeft, value);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0004F0A0 File Offset: 0x0004D2A0
		// (set) Token: 0x060013C3 RID: 5059 RVA: 0x0004F0BC File Offset: 0x0004D2BC
		public bool CursorVisible
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.cursorVisible;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.cursorVisible = value;
				this.WriteConsole((!value) ? this.csrInvisible : this.csrVisible);
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x0004F0F4 File Offset: 0x0004D2F4
		// (set) Token: 0x060013C5 RID: 5061 RVA: 0x0004F108 File Offset: 0x0004D308
		[MonoTODO]
		public int CursorSize
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return 1;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0004F11C File Offset: 0x0004D31C
		public bool KeyAvailable
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.writepos > this.readpos || ConsoleDriver.InternalKeyAvailable(0) > 0;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0004F158 File Offset: 0x0004D358
		public int LargestWindowHeight
		{
			get
			{
				return this.WindowHeight;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x0004F160 File Offset: 0x0004D360
		public int LargestWindowWidth
		{
			get
			{
				return this.WindowWidth;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x0004F168 File Offset: 0x0004D368
		public bool NumberLock
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x0004F180 File Offset: 0x0004D380
		// (set) Token: 0x060013CB RID: 5067 RVA: 0x0004F19C File Offset: 0x0004D39C
		public string Title
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.title;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.title = value;
				this.WriteConsole(string.Format(this.titleFormat, value));
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x0004F1D4 File Offset: 0x0004D3D4
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x0004F1F0 File Offset: 0x0004D3F0
		public bool TreatControlCAsInput
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.controlCAsInput;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				if (this.controlCAsInput == value)
				{
					return;
				}
				ConsoleDriver.SetBreak(value);
				this.controlCAsInput = value;
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0004F22C File Offset: 0x0004D42C
		private unsafe void CheckWindowDimensions()
		{
			if (TermInfoDriver.native_terminal_size == null || TermInfoDriver.terminal_size == *TermInfoDriver.native_terminal_size)
			{
				return;
			}
			if (*TermInfoDriver.native_terminal_size == -1)
			{
				int num = this.reader.Get(TermInfoNumbers.Columns);
				if (num != 0)
				{
					this.windowWidth = num;
				}
				num = this.reader.Get(TermInfoNumbers.Lines);
				if (num != 0)
				{
					this.windowHeight = num;
				}
			}
			else
			{
				TermInfoDriver.terminal_size = *TermInfoDriver.native_terminal_size;
				this.windowWidth = TermInfoDriver.terminal_size >> 16;
				this.windowHeight = (TermInfoDriver.terminal_size & 65535);
			}
			this.bufferHeight = this.windowHeight;
			this.bufferWidth = this.windowWidth;
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0004F2DC File Offset: 0x0004D4DC
		// (set) Token: 0x060013D0 RID: 5072 RVA: 0x0004F2FC File Offset: 0x0004D4FC
		public int WindowHeight
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.CheckWindowDimensions();
				return this.windowHeight;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0004F314 File Offset: 0x0004D514
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x0004F328 File Offset: 0x0004D528
		public int WindowLeft
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return 0;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0004F340 File Offset: 0x0004D540
		// (set) Token: 0x060013D4 RID: 5076 RVA: 0x0004F354 File Offset: 0x0004D554
		public int WindowTop
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return 0;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0004F36C File Offset: 0x0004D56C
		// (set) Token: 0x060013D6 RID: 5078 RVA: 0x0004F38C File Offset: 0x0004D58C
		public int WindowWidth
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.CheckWindowDimensions();
				return this.windowWidth;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0004F3A4 File Offset: 0x0004D5A4
		public void Clear()
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.WriteConsole(this.clear);
			this.cursorLeft = 0;
			this.cursorTop = 0;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0004F3D4 File Offset: 0x0004D5D4
		public void Beep(int frequency, int duration)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.WriteConsole(this.bell);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0004F3F4 File Offset: 0x0004D5F4
		public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			if (!this.inited)
			{
				this.Init();
			}
			throw new NotImplementedException();
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0004F40C File Offset: 0x0004D60C
		private void AddToBuffer(int b)
		{
			if (this.buffer == null)
			{
				this.buffer = new char[1024];
			}
			else if (this.writepos >= this.buffer.Length)
			{
				char[] dst = new char[this.buffer.Length * 2];
				Buffer.BlockCopy(this.buffer, 0, dst, 0, this.buffer.Length);
				this.buffer = dst;
			}
			this.buffer[this.writepos++] = (char)b;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0004F494 File Offset: 0x0004D694
		private void AdjustBuffer()
		{
			if (this.readpos >= this.writepos)
			{
				this.readpos = (this.writepos = 0);
			}
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0004F4C4 File Offset: 0x0004D6C4
		private ConsoleKeyInfo CreateKeyInfoFromInt(int n, bool alt)
		{
			char keyChar = (char)n;
			ConsoleKey key = (ConsoleKey)n;
			bool shift = false;
			bool control = false;
			switch (n)
			{
			case 8:
			case 9:
			case 12:
			case 13:
			case 19:
				break;
			case 10:
				key = ConsoleKey.Enter;
				break;
			default:
				switch (n)
				{
				case 42:
					key = ConsoleKey.Multiply;
					break;
				case 43:
					key = ConsoleKey.Add;
					break;
				default:
					if (n != 27)
					{
						if (n != 32)
						{
							if (n >= 1 && n <= 26)
							{
								control = true;
								key = ConsoleKey.A + n - 1;
							}
							else if (n >= 97 && n <= 122)
							{
								key = (ConsoleKey)(-32) + n;
							}
							else if (n >= 65 && n <= 90)
							{
								shift = true;
							}
							else if (n < 48 || n > 57)
							{
								key = (ConsoleKey)0;
							}
						}
						else
						{
							key = ConsoleKey.Spacebar;
						}
					}
					else
					{
						key = ConsoleKey.Escape;
					}
					break;
				case 45:
					key = ConsoleKey.Subtract;
					break;
				case 47:
					key = ConsoleKey.Divide;
					break;
				}
				break;
			}
			return new ConsoleKeyInfo(keyChar, key, shift, alt, control);
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0004F600 File Offset: 0x0004D800
		private object GetKeyFromBuffer(bool cooked)
		{
			if (this.readpos >= this.writepos)
			{
				return null;
			}
			int num = (int)this.buffer[this.readpos];
			if (!cooked || !this.rootmap.StartsWith(num))
			{
				this.readpos++;
				this.AdjustBuffer();
				return this.CreateKeyInfoFromInt(num, false);
			}
			int num2;
			TermInfoStrings termInfoStrings = this.rootmap.Match(this.buffer, this.readpos, this.writepos - this.readpos, out num2);
			if (termInfoStrings == (TermInfoStrings)(-1))
			{
				if (this.buffer[this.readpos] != '\u001b' || this.writepos - this.readpos < 2)
				{
					return null;
				}
				this.readpos += 2;
				this.AdjustBuffer();
				if (this.buffer[this.readpos + 1] == '\u007f')
				{
					return new ConsoleKeyInfo('\b', ConsoleKey.Backspace, false, true, false);
				}
				return this.CreateKeyInfoFromInt((int)this.buffer[this.readpos + 1], true);
			}
			else
			{
				if (this.keymap[termInfoStrings] != null)
				{
					ConsoleKeyInfo consoleKeyInfo = (ConsoleKeyInfo)this.keymap[termInfoStrings];
					this.readpos += num2;
					this.AdjustBuffer();
					return consoleKeyInfo;
				}
				this.readpos++;
				this.AdjustBuffer();
				return this.CreateKeyInfoFromInt(num, false);
			}
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0004F784 File Offset: 0x0004D984
		private ConsoleKeyInfo ReadKeyInternal(out bool fresh)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.InitKeys();
			object keyFromBuffer;
			if ((keyFromBuffer = this.GetKeyFromBuffer(true)) == null)
			{
				do
				{
					if (ConsoleDriver.InternalKeyAvailable(150) > 0)
					{
						do
						{
							this.AddToBuffer(this.stdin.Read());
						}
						while (ConsoleDriver.InternalKeyAvailable(0) > 0);
					}
					else if (this.stdin.DataAvailable())
					{
						do
						{
							this.AddToBuffer(this.stdin.Read());
						}
						while (this.stdin.DataAvailable());
					}
					else
					{
						if ((keyFromBuffer = this.GetKeyFromBuffer(false)) != null)
						{
							break;
						}
						this.AddToBuffer(this.stdin.Read());
					}
					keyFromBuffer = this.GetKeyFromBuffer(true);
				}
				while (keyFromBuffer == null);
				fresh = true;
			}
			else
			{
				fresh = false;
			}
			return (ConsoleKeyInfo)keyFromBuffer;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0004F864 File Offset: 0x0004DA64
		private bool InputPending()
		{
			return this.readpos < this.writepos || this.stdin.DataAvailable();
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0004F888 File Offset: 0x0004DA88
		private void QueueEcho(char c)
		{
			if (this.echobuf == null)
			{
				this.echobuf = new char[1024];
			}
			this.echobuf[this.echon++] = c;
			if (this.echon == this.echobuf.Length || !this.InputPending())
			{
				this.stdout.InternalWriteChars(this.echobuf, this.echon);
				this.echon = 0;
			}
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0004F908 File Offset: 0x0004DB08
		private void Echo(ConsoleKeyInfo key)
		{
			if (!this.IsSpecialKey(key))
			{
				this.QueueEcho(key.KeyChar);
				return;
			}
			this.EchoFlush();
			this.WriteSpecialKey(key);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0004F93C File Offset: 0x0004DB3C
		private void EchoFlush()
		{
			if (this.echon == 0)
			{
				return;
			}
			this.stdout.InternalWriteChars(this.echobuf, this.echon);
			this.echon = 0;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0004F974 File Offset: 0x0004DB74
		public int Read([In] [Out] char[] dest, int index, int count)
		{
			bool flag = false;
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			object keyFromBuffer;
			while ((keyFromBuffer = this.GetKeyFromBuffer(true)) != null)
			{
				ConsoleKeyInfo key = (ConsoleKeyInfo)keyFromBuffer;
				char keyChar = key.KeyChar;
				if (key.Key != ConsoleKey.Backspace)
				{
					if (key.Key == ConsoleKey.Enter)
					{
						num = stringBuilder.Length;
					}
					stringBuilder.Append(keyChar);
				}
				else if (stringBuilder.Length > num)
				{
					stringBuilder.Length--;
				}
			}
			this.rl_startx = this.cursorLeft;
			this.rl_starty = this.cursorTop;
			for (;;)
			{
				bool flag2;
				ConsoleKeyInfo key = this.ReadKeyInternal(out flag2);
				flag = (flag || flag2);
				char keyChar = key.KeyChar;
				if (key.Key != ConsoleKey.Backspace)
				{
					if (key.Key == ConsoleKey.Enter)
					{
						num = stringBuilder.Length;
					}
					stringBuilder.Append(keyChar);
					goto IL_10C;
				}
				if (stringBuilder.Length > num)
				{
					stringBuilder.Length--;
					goto IL_10C;
				}
				IL_119:
				if (key.Key == ConsoleKey.Enter)
				{
					break;
				}
				continue;
				IL_10C:
				if (flag)
				{
					this.Echo(key);
					goto IL_119;
				}
				goto IL_119;
			}
			this.EchoFlush();
			this.rl_startx = -1;
			this.rl_starty = -1;
			int num2 = 0;
			while (count > 0 && num2 < stringBuilder.Length)
			{
				dest[index + num2] = stringBuilder[num2];
				num2++;
				count--;
			}
			for (int i = num2; i < stringBuilder.Length; i++)
			{
				this.AddToBuffer((int)stringBuilder[i]);
			}
			return num2;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0004FB20 File Offset: 0x0004DD20
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			bool flag;
			ConsoleKeyInfo consoleKeyInfo = this.ReadKeyInternal(out flag);
			if (!intercept && flag)
			{
				this.Echo(consoleKeyInfo);
				this.EchoFlush();
			}
			return consoleKeyInfo;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0004FB50 File Offset: 0x0004DD50
		public string ReadLine()
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.GetCursorPosition();
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			this.rl_startx = this.cursorLeft;
			this.rl_starty = this.cursorTop;
			char c = (char)this.control_characters[4];
			for (;;)
			{
				bool flag2;
				ConsoleKeyInfo key = this.ReadKeyInternal(out flag2);
				flag = (flag || flag2);
				char keyChar = key.KeyChar;
				if (keyChar == c && keyChar != '\0' && stringBuilder.Length == 0)
				{
					break;
				}
				if (key.Key == ConsoleKey.Enter)
				{
					goto IL_C9;
				}
				if (key.Key != ConsoleKey.Backspace)
				{
					stringBuilder.Append(keyChar);
					goto IL_C9;
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Length--;
					goto IL_C9;
				}
				IL_D6:
				if (key.Key == ConsoleKey.Enter)
				{
					goto Block_10;
				}
				continue;
				IL_C9:
				if (flag)
				{
					this.Echo(key);
					goto IL_D6;
				}
				goto IL_D6;
			}
			return null;
			Block_10:
			this.EchoFlush();
			this.rl_startx = -1;
			this.rl_starty = -1;
			return stringBuilder.ToString();
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0004FC5C File Offset: 0x0004DE5C
		public void ResetColor()
		{
			if (!this.inited)
			{
				this.Init();
			}
			string str = (this.origPair == null) ? this.origColors : this.origPair;
			this.WriteConsole(str);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0004FCA0 File Offset: 0x0004DEA0
		public void SetBufferSize(int width, int height)
		{
			if (!this.inited)
			{
				this.Init();
			}
			throw new NotImplementedException(string.Empty);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0004FCC0 File Offset: 0x0004DEC0
		public void SetCursorPosition(int left, int top)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.CheckWindowDimensions();
			if (left < 0 || left >= this.bufferWidth)
			{
				throw new ArgumentOutOfRangeException("left", "Value must be positive and below the buffer width.");
			}
			if (top < 0 || top >= this.bufferHeight)
			{
				throw new ArgumentOutOfRangeException("top", "Value must be positive and below the buffer height.");
			}
			if (this.cursorAddress == null)
			{
				throw new NotSupportedException("This terminal does not suport setting the cursor position.");
			}
			int num = (!this.home_1_1) ? 0 : 1;
			this.WriteConsole(string.Format(this.cursorAddress, top + num, left + num));
			this.cursorLeft = left;
			this.cursorTop = top;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0004FD84 File Offset: 0x0004DF84
		public void SetWindowPosition(int left, int top)
		{
			if (!this.inited)
			{
				this.Init();
			}
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0004FD98 File Offset: 0x0004DF98
		public void SetWindowSize(int width, int height)
		{
			if (!this.inited)
			{
				this.Init();
			}
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0004FDAC File Offset: 0x0004DFAC
		private void CreateKeyMap()
		{
			this.keymap = new Hashtable();
			this.keymap[TermInfoStrings.KeyBackspace] = new ConsoleKeyInfo('\0', ConsoleKey.Backspace, false, false, false);
			this.keymap[TermInfoStrings.KeyClear] = new ConsoleKeyInfo('\0', ConsoleKey.Clear, false, false, false);
			this.keymap[TermInfoStrings.KeyDown] = new ConsoleKeyInfo('\0', ConsoleKey.DownArrow, false, false, false);
			this.keymap[TermInfoStrings.KeyF1] = new ConsoleKeyInfo('\0', ConsoleKey.F1, false, false, false);
			this.keymap[TermInfoStrings.KeyF10] = new ConsoleKeyInfo('\0', ConsoleKey.F10, false, false, false);
			this.keymap[TermInfoStrings.KeyF2] = new ConsoleKeyInfo('\0', ConsoleKey.F2, false, false, false);
			this.keymap[TermInfoStrings.KeyF3] = new ConsoleKeyInfo('\0', ConsoleKey.F3, false, false, false);
			this.keymap[TermInfoStrings.KeyF4] = new ConsoleKeyInfo('\0', ConsoleKey.F4, false, false, false);
			this.keymap[TermInfoStrings.KeyF5] = new ConsoleKeyInfo('\0', ConsoleKey.F5, false, false, false);
			this.keymap[TermInfoStrings.KeyF6] = new ConsoleKeyInfo('\0', ConsoleKey.F6, false, false, false);
			this.keymap[TermInfoStrings.KeyF7] = new ConsoleKeyInfo('\0', ConsoleKey.F7, false, false, false);
			this.keymap[TermInfoStrings.KeyF8] = new ConsoleKeyInfo('\0', ConsoleKey.F8, false, false, false);
			this.keymap[TermInfoStrings.KeyF9] = new ConsoleKeyInfo('\0', ConsoleKey.F9, false, false, false);
			this.keymap[TermInfoStrings.KeyHome] = new ConsoleKeyInfo('\0', ConsoleKey.Home, false, false, false);
			this.keymap[TermInfoStrings.KeyLeft] = new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, false, false, false);
			this.keymap[TermInfoStrings.KeyLl] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad1, false, false, false);
			this.keymap[TermInfoStrings.KeyNpage] = new ConsoleKeyInfo('\0', ConsoleKey.PageDown, false, false, false);
			this.keymap[TermInfoStrings.KeyPpage] = new ConsoleKeyInfo('\0', ConsoleKey.PageUp, false, false, false);
			this.keymap[TermInfoStrings.KeyRight] = new ConsoleKeyInfo('\0', ConsoleKey.RightArrow, false, false, false);
			this.keymap[TermInfoStrings.KeySf] = new ConsoleKeyInfo('\0', ConsoleKey.PageDown, false, false, false);
			this.keymap[TermInfoStrings.KeySr] = new ConsoleKeyInfo('\0', ConsoleKey.PageUp, false, false, false);
			this.keymap[TermInfoStrings.KeyUp] = new ConsoleKeyInfo('\0', ConsoleKey.UpArrow, false, false, false);
			this.keymap[TermInfoStrings.KeyA1] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad7, false, false, false);
			this.keymap[TermInfoStrings.KeyA3] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad9, false, false, false);
			this.keymap[TermInfoStrings.KeyB2] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad5, false, false, false);
			this.keymap[TermInfoStrings.KeyC1] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad1, false, false, false);
			this.keymap[TermInfoStrings.KeyC3] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad3, false, false, false);
			this.keymap[TermInfoStrings.KeyBtab] = new ConsoleKeyInfo('\0', ConsoleKey.Tab, true, false, false);
			this.keymap[TermInfoStrings.KeyBeg] = new ConsoleKeyInfo('\0', ConsoleKey.Home, false, false, false);
			this.keymap[TermInfoStrings.KeyCopy] = new ConsoleKeyInfo('C', ConsoleKey.C, false, true, false);
			this.keymap[TermInfoStrings.KeyEnd] = new ConsoleKeyInfo('\0', ConsoleKey.End, false, false, false);
			this.keymap[TermInfoStrings.KeyEnter] = new ConsoleKeyInfo('\n', ConsoleKey.Enter, false, false, false);
			this.keymap[TermInfoStrings.KeyHelp] = new ConsoleKeyInfo('\0', ConsoleKey.Help, false, false, false);
			this.keymap[TermInfoStrings.KeyPrint] = new ConsoleKeyInfo('\0', ConsoleKey.Print, false, false, false);
			this.keymap[TermInfoStrings.KeyUndo] = new ConsoleKeyInfo('Z', ConsoleKey.Z, false, true, false);
			this.keymap[TermInfoStrings.KeySbeg] = new ConsoleKeyInfo('\0', ConsoleKey.Home, true, false, false);
			this.keymap[TermInfoStrings.KeyScopy] = new ConsoleKeyInfo('C', ConsoleKey.C, true, true, false);
			this.keymap[TermInfoStrings.KeySdc] = new ConsoleKeyInfo('\t', ConsoleKey.Delete, true, false, false);
			this.keymap[TermInfoStrings.KeyShelp] = new ConsoleKeyInfo('\0', ConsoleKey.Help, true, false, false);
			this.keymap[TermInfoStrings.KeyShome] = new ConsoleKeyInfo('\0', ConsoleKey.Home, true, false, false);
			this.keymap[TermInfoStrings.KeySleft] = new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, true, false, false);
			this.keymap[TermInfoStrings.KeySprint] = new ConsoleKeyInfo('\0', ConsoleKey.Print, true, false, false);
			this.keymap[TermInfoStrings.KeySright] = new ConsoleKeyInfo('\0', ConsoleKey.RightArrow, true, false, false);
			this.keymap[TermInfoStrings.KeySundo] = new ConsoleKeyInfo('Z', ConsoleKey.Z, true, false, false);
			this.keymap[TermInfoStrings.KeyF11] = new ConsoleKeyInfo('\0', ConsoleKey.F11, false, false, false);
			this.keymap[TermInfoStrings.KeyF12] = new ConsoleKeyInfo('\0', ConsoleKey.F12, false, false, false);
			this.keymap[TermInfoStrings.KeyF13] = new ConsoleKeyInfo('\0', ConsoleKey.F13, false, false, false);
			this.keymap[TermInfoStrings.KeyF14] = new ConsoleKeyInfo('\0', ConsoleKey.F14, false, false, false);
			this.keymap[TermInfoStrings.KeyF15] = new ConsoleKeyInfo('\0', ConsoleKey.F15, false, false, false);
			this.keymap[TermInfoStrings.KeyF16] = new ConsoleKeyInfo('\0', ConsoleKey.F16, false, false, false);
			this.keymap[TermInfoStrings.KeyF17] = new ConsoleKeyInfo('\0', ConsoleKey.F17, false, false, false);
			this.keymap[TermInfoStrings.KeyF18] = new ConsoleKeyInfo('\0', ConsoleKey.F18, false, false, false);
			this.keymap[TermInfoStrings.KeyF19] = new ConsoleKeyInfo('\0', ConsoleKey.F19, false, false, false);
			this.keymap[TermInfoStrings.KeyF20] = new ConsoleKeyInfo('\0', ConsoleKey.F20, false, false, false);
			this.keymap[TermInfoStrings.KeyF21] = new ConsoleKeyInfo('\0', ConsoleKey.F21, false, false, false);
			this.keymap[TermInfoStrings.KeyF22] = new ConsoleKeyInfo('\0', ConsoleKey.F22, false, false, false);
			this.keymap[TermInfoStrings.KeyF23] = new ConsoleKeyInfo('\0', ConsoleKey.F23, false, false, false);
			this.keymap[TermInfoStrings.KeyF24] = new ConsoleKeyInfo('\0', ConsoleKey.F24, false, false, false);
			this.keymap[TermInfoStrings.KeyDc] = new ConsoleKeyInfo('\0', ConsoleKey.Delete, false, false, false);
			this.keymap[TermInfoStrings.KeyIc] = new ConsoleKeyInfo('\0', ConsoleKey.Insert, false, false, false);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00050648 File Offset: 0x0004E848
		private void InitKeys()
		{
			if (this.initKeys)
			{
				return;
			}
			this.CreateKeyMap();
			this.rootmap = new ByteMatcher();
			foreach (TermInfoStrings s in TermInfoDriver.UsedKeys)
			{
				this.AddStringMapping(s);
			}
			this.rootmap.AddMapping(TermInfoStrings.KeyBackspace, new byte[]
			{
				this.control_characters[2]
			});
			this.rootmap.Sort();
			this.initKeys = true;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x000506C8 File Offset: 0x0004E8C8
		private void AddStringMapping(TermInfoStrings s)
		{
			byte[] stringBytes = this.reader.GetStringBytes(s);
			if (stringBytes == null)
			{
				return;
			}
			this.rootmap.AddMapping(s, stringBytes);
		}

		// Token: 0x040005E2 RID: 1506
		private unsafe static int* native_terminal_size;

		// Token: 0x040005E3 RID: 1507
		private static int terminal_size;

		// Token: 0x040005E4 RID: 1508
		private static string[] locations = new string[]
		{
			"/etc/terminfo",
			"/usr/share/terminfo",
			"/usr/lib/terminfo"
		};

		// Token: 0x040005E5 RID: 1509
		private TermInfoReader reader;

		// Token: 0x040005E6 RID: 1510
		private int cursorLeft;

		// Token: 0x040005E7 RID: 1511
		private int cursorTop;

		// Token: 0x040005E8 RID: 1512
		private string title = string.Empty;

		// Token: 0x040005E9 RID: 1513
		private string titleFormat = string.Empty;

		// Token: 0x040005EA RID: 1514
		private bool cursorVisible = true;

		// Token: 0x040005EB RID: 1515
		private string csrVisible;

		// Token: 0x040005EC RID: 1516
		private string csrInvisible;

		// Token: 0x040005ED RID: 1517
		private string clear;

		// Token: 0x040005EE RID: 1518
		private string bell;

		// Token: 0x040005EF RID: 1519
		private string term;

		// Token: 0x040005F0 RID: 1520
		private StreamReader stdin;

		// Token: 0x040005F1 RID: 1521
		private CStreamWriter stdout;

		// Token: 0x040005F2 RID: 1522
		private int windowWidth;

		// Token: 0x040005F3 RID: 1523
		private int windowHeight;

		// Token: 0x040005F4 RID: 1524
		private int bufferHeight;

		// Token: 0x040005F5 RID: 1525
		private int bufferWidth;

		// Token: 0x040005F6 RID: 1526
		private char[] buffer;

		// Token: 0x040005F7 RID: 1527
		private int readpos;

		// Token: 0x040005F8 RID: 1528
		private int writepos;

		// Token: 0x040005F9 RID: 1529
		private string keypadXmit;

		// Token: 0x040005FA RID: 1530
		private string keypadLocal;

		// Token: 0x040005FB RID: 1531
		private bool controlCAsInput;

		// Token: 0x040005FC RID: 1532
		private bool inited;

		// Token: 0x040005FD RID: 1533
		private object initLock = new object();

		// Token: 0x040005FE RID: 1534
		private bool initKeys;

		// Token: 0x040005FF RID: 1535
		private string origPair;

		// Token: 0x04000600 RID: 1536
		private string origColors;

		// Token: 0x04000601 RID: 1537
		private string cursorAddress;

		// Token: 0x04000602 RID: 1538
		private ConsoleColor fgcolor = ConsoleColor.White;

		// Token: 0x04000603 RID: 1539
		private ConsoleColor bgcolor;

		// Token: 0x04000604 RID: 1540
		private bool color16;

		// Token: 0x04000605 RID: 1541
		private string setlfgcolor;

		// Token: 0x04000606 RID: 1542
		private string setlbgcolor;

		// Token: 0x04000607 RID: 1543
		private string setfgcolor;

		// Token: 0x04000608 RID: 1544
		private string setbgcolor;

		// Token: 0x04000609 RID: 1545
		private bool noGetPosition;

		// Token: 0x0400060A RID: 1546
		private Hashtable keymap;

		// Token: 0x0400060B RID: 1547
		private ByteMatcher rootmap;

		// Token: 0x0400060C RID: 1548
		private bool home_1_1;

		// Token: 0x0400060D RID: 1549
		private int rl_startx = -1;

		// Token: 0x0400060E RID: 1550
		private int rl_starty = -1;

		// Token: 0x0400060F RID: 1551
		private byte[] control_characters;

		// Token: 0x04000610 RID: 1552
		private char[] echobuf;

		// Token: 0x04000611 RID: 1553
		private int echon;

		// Token: 0x04000612 RID: 1554
		private static TermInfoStrings[] UsedKeys = new TermInfoStrings[]
		{
			TermInfoStrings.KeyBackspace,
			TermInfoStrings.KeyClear,
			TermInfoStrings.KeyDown,
			TermInfoStrings.KeyF1,
			TermInfoStrings.KeyF10,
			TermInfoStrings.KeyF2,
			TermInfoStrings.KeyF3,
			TermInfoStrings.KeyF4,
			TermInfoStrings.KeyF5,
			TermInfoStrings.KeyF6,
			TermInfoStrings.KeyF7,
			TermInfoStrings.KeyF8,
			TermInfoStrings.KeyF9,
			TermInfoStrings.KeyHome,
			TermInfoStrings.KeyLeft,
			TermInfoStrings.KeyLl,
			TermInfoStrings.KeyNpage,
			TermInfoStrings.KeyPpage,
			TermInfoStrings.KeyRight,
			TermInfoStrings.KeySf,
			TermInfoStrings.KeySr,
			TermInfoStrings.KeyUp,
			TermInfoStrings.KeyA1,
			TermInfoStrings.KeyA3,
			TermInfoStrings.KeyB2,
			TermInfoStrings.KeyC1,
			TermInfoStrings.KeyC3,
			TermInfoStrings.KeyBtab,
			TermInfoStrings.KeyBeg,
			TermInfoStrings.KeyCopy,
			TermInfoStrings.KeyEnd,
			TermInfoStrings.KeyEnter,
			TermInfoStrings.KeyHelp,
			TermInfoStrings.KeyPrint,
			TermInfoStrings.KeyUndo,
			TermInfoStrings.KeySbeg,
			TermInfoStrings.KeyScopy,
			TermInfoStrings.KeySdc,
			TermInfoStrings.KeyShelp,
			TermInfoStrings.KeyShome,
			TermInfoStrings.KeySleft,
			TermInfoStrings.KeySprint,
			TermInfoStrings.KeySright,
			TermInfoStrings.KeySundo,
			TermInfoStrings.KeyF11,
			TermInfoStrings.KeyF12,
			TermInfoStrings.KeyF13,
			TermInfoStrings.KeyF14,
			TermInfoStrings.KeyF15,
			TermInfoStrings.KeyF16,
			TermInfoStrings.KeyF17,
			TermInfoStrings.KeyF18,
			TermInfoStrings.KeyF19,
			TermInfoStrings.KeyF20,
			TermInfoStrings.KeyF21,
			TermInfoStrings.KeyF22,
			TermInfoStrings.KeyF23,
			TermInfoStrings.KeyF24,
			TermInfoStrings.KeyDc,
			TermInfoStrings.KeyIc
		};
	}
}
