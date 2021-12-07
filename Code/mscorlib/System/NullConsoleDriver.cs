using System;

namespace System
{
	// Token: 0x02000160 RID: 352
	internal class NullConsoleDriver : IConsoleDriver
	{
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x00049678 File Offset: 0x00047878
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x0004967C File Offset: 0x0004787C
		public ConsoleColor BackgroundColor
		{
			get
			{
				return ConsoleColor.Black;
			}
			set
			{
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00049680 File Offset: 0x00047880
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x00049684 File Offset: 0x00047884
		public int BufferHeight
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x00049688 File Offset: 0x00047888
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x0004968C File Offset: 0x0004788C
		public int BufferWidth
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00049690 File Offset: 0x00047890
		public bool CapsLock
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x00049694 File Offset: 0x00047894
		// (set) Token: 0x060012AC RID: 4780 RVA: 0x00049698 File Offset: 0x00047898
		public int CursorLeft
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x0004969C File Offset: 0x0004789C
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x000496A0 File Offset: 0x000478A0
		public int CursorSize
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x000496A4 File Offset: 0x000478A4
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x000496A8 File Offset: 0x000478A8
		public int CursorTop
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x000496AC File Offset: 0x000478AC
		// (set) Token: 0x060012B2 RID: 4786 RVA: 0x000496B0 File Offset: 0x000478B0
		public bool CursorVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x000496B4 File Offset: 0x000478B4
		// (set) Token: 0x060012B4 RID: 4788 RVA: 0x000496B8 File Offset: 0x000478B8
		public ConsoleColor ForegroundColor
		{
			get
			{
				return ConsoleColor.Black;
			}
			set
			{
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x000496BC File Offset: 0x000478BC
		public bool KeyAvailable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x000496C0 File Offset: 0x000478C0
		public bool Initialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x000496C4 File Offset: 0x000478C4
		public int LargestWindowHeight
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x000496C8 File Offset: 0x000478C8
		public int LargestWindowWidth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x000496CC File Offset: 0x000478CC
		public bool NumberLock
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x000496D0 File Offset: 0x000478D0
		// (set) Token: 0x060012BB RID: 4795 RVA: 0x000496D8 File Offset: 0x000478D8
		public string Title
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x000496DC File Offset: 0x000478DC
		// (set) Token: 0x060012BD RID: 4797 RVA: 0x000496E0 File Offset: 0x000478E0
		public bool TreatControlCAsInput
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x000496E4 File Offset: 0x000478E4
		// (set) Token: 0x060012BF RID: 4799 RVA: 0x000496E8 File Offset: 0x000478E8
		public int WindowHeight
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x000496EC File Offset: 0x000478EC
		// (set) Token: 0x060012C1 RID: 4801 RVA: 0x000496F0 File Offset: 0x000478F0
		public int WindowLeft
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x000496F4 File Offset: 0x000478F4
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x000496F8 File Offset: 0x000478F8
		public int WindowTop
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x000496FC File Offset: 0x000478FC
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x00049700 File Offset: 0x00047900
		public int WindowWidth
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00049704 File Offset: 0x00047904
		public void Beep(int frequency, int duration)
		{
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00049708 File Offset: 0x00047908
		public void Clear()
		{
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0004970C File Offset: 0x0004790C
		public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00049710 File Offset: 0x00047910
		public void Init()
		{
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00049714 File Offset: 0x00047914
		public string ReadLine()
		{
			return null;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00049718 File Offset: 0x00047918
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			return ConsoleKeyInfo.Empty;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00049720 File Offset: 0x00047920
		public void ResetColor()
		{
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00049724 File Offset: 0x00047924
		public void SetBufferSize(int width, int height)
		{
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00049728 File Offset: 0x00047928
		public void SetCursorPosition(int left, int top)
		{
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0004972C File Offset: 0x0004792C
		public void SetWindowPosition(int left, int top)
		{
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00049730 File Offset: 0x00047930
		public void SetWindowSize(int width, int height)
		{
		}
	}
}
