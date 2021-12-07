using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000113 RID: 275
	internal class ConsoleDriver
	{
		// Token: 0x06000E4D RID: 3661 RVA: 0x0003C990 File Offset: 0x0003AB90
		static ConsoleDriver()
		{
			if (!ConsoleDriver.IsConsole)
			{
				ConsoleDriver.driver = ConsoleDriver.CreateNullConsoleDriver();
			}
			else if (Environment.IsRunningOnWindows)
			{
				ConsoleDriver.driver = ConsoleDriver.CreateWindowsConsoleDriver();
			}
			else
			{
				string environmentVariable = Environment.GetEnvironmentVariable("TERM");
				if (environmentVariable == "dumb")
				{
					ConsoleDriver.is_console = false;
					ConsoleDriver.driver = ConsoleDriver.CreateNullConsoleDriver();
				}
				else
				{
					ConsoleDriver.driver = ConsoleDriver.CreateTermInfoDriver(environmentVariable);
				}
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0003CA0C File Offset: 0x0003AC0C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateNullConsoleDriver()
		{
			return new NullConsoleDriver();
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003CA14 File Offset: 0x0003AC14
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateWindowsConsoleDriver()
		{
			return new WindowsConsoleDriver();
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0003CA1C File Offset: 0x0003AC1C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateTermInfoDriver(string term)
		{
			return new TermInfoDriver(term);
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0003CA24 File Offset: 0x0003AC24
		public static bool Initialized
		{
			get
			{
				return ConsoleDriver.driver.Initialized;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0003CA30 File Offset: 0x0003AC30
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x0003CA3C File Offset: 0x0003AC3C
		public static ConsoleColor BackgroundColor
		{
			get
			{
				return ConsoleDriver.driver.BackgroundColor;
			}
			set
			{
				if (value < ConsoleColor.Black || value > ConsoleColor.White)
				{
					throw new ArgumentOutOfRangeException("value", "Not a ConsoleColor value.");
				}
				ConsoleDriver.driver.BackgroundColor = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0003CA74 File Offset: 0x0003AC74
		// (set) Token: 0x06000E55 RID: 3669 RVA: 0x0003CA80 File Offset: 0x0003AC80
		public static int BufferHeight
		{
			get
			{
				return ConsoleDriver.driver.BufferHeight;
			}
			set
			{
				ConsoleDriver.driver.BufferHeight = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0003CA90 File Offset: 0x0003AC90
		// (set) Token: 0x06000E57 RID: 3671 RVA: 0x0003CA9C File Offset: 0x0003AC9C
		public static int BufferWidth
		{
			get
			{
				return ConsoleDriver.driver.BufferWidth;
			}
			set
			{
				ConsoleDriver.driver.BufferWidth = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0003CAAC File Offset: 0x0003ACAC
		public static bool CapsLock
		{
			get
			{
				return ConsoleDriver.driver.CapsLock;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0003CAB8 File Offset: 0x0003ACB8
		// (set) Token: 0x06000E5A RID: 3674 RVA: 0x0003CAC4 File Offset: 0x0003ACC4
		public static int CursorLeft
		{
			get
			{
				return ConsoleDriver.driver.CursorLeft;
			}
			set
			{
				ConsoleDriver.driver.CursorLeft = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x0003CAD4 File Offset: 0x0003ACD4
		// (set) Token: 0x06000E5C RID: 3676 RVA: 0x0003CAE0 File Offset: 0x0003ACE0
		public static int CursorSize
		{
			get
			{
				return ConsoleDriver.driver.CursorSize;
			}
			set
			{
				ConsoleDriver.driver.CursorSize = value;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0003CAF0 File Offset: 0x0003ACF0
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x0003CAFC File Offset: 0x0003ACFC
		public static int CursorTop
		{
			get
			{
				return ConsoleDriver.driver.CursorTop;
			}
			set
			{
				ConsoleDriver.driver.CursorTop = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0003CB0C File Offset: 0x0003AD0C
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x0003CB18 File Offset: 0x0003AD18
		public static bool CursorVisible
		{
			get
			{
				return ConsoleDriver.driver.CursorVisible;
			}
			set
			{
				ConsoleDriver.driver.CursorVisible = value;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0003CB28 File Offset: 0x0003AD28
		public static bool KeyAvailable
		{
			get
			{
				return ConsoleDriver.driver.KeyAvailable;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0003CB34 File Offset: 0x0003AD34
		// (set) Token: 0x06000E63 RID: 3683 RVA: 0x0003CB40 File Offset: 0x0003AD40
		public static ConsoleColor ForegroundColor
		{
			get
			{
				return ConsoleDriver.driver.ForegroundColor;
			}
			set
			{
				if (value < ConsoleColor.Black || value > ConsoleColor.White)
				{
					throw new ArgumentOutOfRangeException("value", "Not a ConsoleColor value.");
				}
				ConsoleDriver.driver.ForegroundColor = value;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0003CB78 File Offset: 0x0003AD78
		public static int LargestWindowHeight
		{
			get
			{
				return ConsoleDriver.driver.LargestWindowHeight;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0003CB84 File Offset: 0x0003AD84
		public static int LargestWindowWidth
		{
			get
			{
				return ConsoleDriver.driver.LargestWindowWidth;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0003CB90 File Offset: 0x0003AD90
		public static bool NumberLock
		{
			get
			{
				return ConsoleDriver.driver.NumberLock;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0003CB9C File Offset: 0x0003AD9C
		// (set) Token: 0x06000E68 RID: 3688 RVA: 0x0003CBA8 File Offset: 0x0003ADA8
		public static string Title
		{
			get
			{
				return ConsoleDriver.driver.Title;
			}
			set
			{
				ConsoleDriver.driver.Title = value;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0003CBB8 File Offset: 0x0003ADB8
		// (set) Token: 0x06000E6A RID: 3690 RVA: 0x0003CBC4 File Offset: 0x0003ADC4
		public static bool TreatControlCAsInput
		{
			get
			{
				return ConsoleDriver.driver.TreatControlCAsInput;
			}
			set
			{
				ConsoleDriver.driver.TreatControlCAsInput = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0003CBD4 File Offset: 0x0003ADD4
		// (set) Token: 0x06000E6C RID: 3692 RVA: 0x0003CBE0 File Offset: 0x0003ADE0
		public static int WindowHeight
		{
			get
			{
				return ConsoleDriver.driver.WindowHeight;
			}
			set
			{
				ConsoleDriver.driver.WindowHeight = value;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0003CBF0 File Offset: 0x0003ADF0
		// (set) Token: 0x06000E6E RID: 3694 RVA: 0x0003CBFC File Offset: 0x0003ADFC
		public static int WindowLeft
		{
			get
			{
				return ConsoleDriver.driver.WindowLeft;
			}
			set
			{
				ConsoleDriver.driver.WindowLeft = value;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0003CC0C File Offset: 0x0003AE0C
		// (set) Token: 0x06000E70 RID: 3696 RVA: 0x0003CC18 File Offset: 0x0003AE18
		public static int WindowTop
		{
			get
			{
				return ConsoleDriver.driver.WindowTop;
			}
			set
			{
				ConsoleDriver.driver.WindowTop = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0003CC28 File Offset: 0x0003AE28
		// (set) Token: 0x06000E72 RID: 3698 RVA: 0x0003CC34 File Offset: 0x0003AE34
		public static int WindowWidth
		{
			get
			{
				return ConsoleDriver.driver.WindowWidth;
			}
			set
			{
				ConsoleDriver.driver.WindowWidth = value;
			}
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0003CC44 File Offset: 0x0003AE44
		public static void Beep(int frequency, int duration)
		{
			ConsoleDriver.driver.Beep(frequency, duration);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0003CC54 File Offset: 0x0003AE54
		public static void Clear()
		{
			ConsoleDriver.driver.Clear();
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0003CC60 File Offset: 0x0003AE60
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
		{
			ConsoleDriver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, ' ', ConsoleColor.Black, ConsoleColor.Black);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003CC80 File Offset: 0x0003AE80
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			ConsoleDriver.driver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003CCA8 File Offset: 0x0003AEA8
		public static void Init()
		{
			ConsoleDriver.driver.Init();
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0003CCB4 File Offset: 0x0003AEB4
		public static int Read()
		{
			return (int)ConsoleDriver.ReadKey(false).KeyChar;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0003CCD0 File Offset: 0x0003AED0
		public static string ReadLine()
		{
			return ConsoleDriver.driver.ReadLine();
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0003CCDC File Offset: 0x0003AEDC
		public static ConsoleKeyInfo ReadKey(bool intercept)
		{
			return ConsoleDriver.driver.ReadKey(intercept);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0003CCEC File Offset: 0x0003AEEC
		public static void ResetColor()
		{
			ConsoleDriver.driver.ResetColor();
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0003CCF8 File Offset: 0x0003AEF8
		public static void SetBufferSize(int width, int height)
		{
			ConsoleDriver.driver.SetBufferSize(width, height);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0003CD08 File Offset: 0x0003AF08
		public static void SetCursorPosition(int left, int top)
		{
			ConsoleDriver.driver.SetCursorPosition(left, top);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0003CD18 File Offset: 0x0003AF18
		public static void SetWindowPosition(int left, int top)
		{
			ConsoleDriver.driver.SetWindowPosition(left, top);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003CD28 File Offset: 0x0003AF28
		public static void SetWindowSize(int width, int height)
		{
			ConsoleDriver.driver.SetWindowSize(width, height);
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0003CD38 File Offset: 0x0003AF38
		public static bool IsConsole
		{
			get
			{
				if (ConsoleDriver.called_isatty)
				{
					return ConsoleDriver.is_console;
				}
				ConsoleDriver.is_console = (ConsoleDriver.Isatty(MonoIO.ConsoleOutput) && ConsoleDriver.Isatty(MonoIO.ConsoleInput));
				ConsoleDriver.called_isatty = true;
				return ConsoleDriver.is_console;
			}
		}

		// Token: 0x06000E81 RID: 3713
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Isatty(IntPtr handle);

		// Token: 0x06000E82 RID: 3714
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalKeyAvailable(int ms_timeout);

		// Token: 0x06000E83 RID: 3715
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool TtySetup(string keypadXmit, string teardown, out byte[] control_characters, out int* address);

		// Token: 0x06000E84 RID: 3716
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SetEcho(bool wantEcho);

		// Token: 0x06000E85 RID: 3717
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SetBreak(bool wantBreak);

		// Token: 0x040003DF RID: 991
		internal static IConsoleDriver driver;

		// Token: 0x040003E0 RID: 992
		private static bool is_console;

		// Token: 0x040003E1 RID: 993
		private static bool called_isatty;
	}
}
