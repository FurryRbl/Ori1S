using System;

namespace System
{
	// Token: 0x02000140 RID: 320
	internal interface IConsoleDriver
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06001183 RID: 4483
		// (set) Token: 0x06001184 RID: 4484
		ConsoleColor BackgroundColor { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001185 RID: 4485
		// (set) Token: 0x06001186 RID: 4486
		int BufferHeight { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06001187 RID: 4487
		// (set) Token: 0x06001188 RID: 4488
		int BufferWidth { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06001189 RID: 4489
		bool CapsLock { get; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600118A RID: 4490
		// (set) Token: 0x0600118B RID: 4491
		int CursorLeft { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600118C RID: 4492
		// (set) Token: 0x0600118D RID: 4493
		int CursorSize { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600118E RID: 4494
		// (set) Token: 0x0600118F RID: 4495
		int CursorTop { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001190 RID: 4496
		// (set) Token: 0x06001191 RID: 4497
		bool CursorVisible { get; set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001192 RID: 4498
		// (set) Token: 0x06001193 RID: 4499
		ConsoleColor ForegroundColor { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06001194 RID: 4500
		bool KeyAvailable { get; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001195 RID: 4501
		bool Initialized { get; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001196 RID: 4502
		int LargestWindowHeight { get; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001197 RID: 4503
		int LargestWindowWidth { get; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001198 RID: 4504
		bool NumberLock { get; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001199 RID: 4505
		// (set) Token: 0x0600119A RID: 4506
		string Title { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600119B RID: 4507
		// (set) Token: 0x0600119C RID: 4508
		bool TreatControlCAsInput { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600119D RID: 4509
		// (set) Token: 0x0600119E RID: 4510
		int WindowHeight { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600119F RID: 4511
		// (set) Token: 0x060011A0 RID: 4512
		int WindowLeft { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060011A1 RID: 4513
		// (set) Token: 0x060011A2 RID: 4514
		int WindowTop { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060011A3 RID: 4515
		// (set) Token: 0x060011A4 RID: 4516
		int WindowWidth { get; set; }

		// Token: 0x060011A5 RID: 4517
		void Init();

		// Token: 0x060011A6 RID: 4518
		void Beep(int frequency, int duration);

		// Token: 0x060011A7 RID: 4519
		void Clear();

		// Token: 0x060011A8 RID: 4520
		void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor);

		// Token: 0x060011A9 RID: 4521
		ConsoleKeyInfo ReadKey(bool intercept);

		// Token: 0x060011AA RID: 4522
		void ResetColor();

		// Token: 0x060011AB RID: 4523
		void SetBufferSize(int width, int height);

		// Token: 0x060011AC RID: 4524
		void SetCursorPosition(int left, int top);

		// Token: 0x060011AD RID: 4525
		void SetWindowPosition(int left, int top);

		// Token: 0x060011AE RID: 4526
		void SetWindowSize(int width, int height);

		// Token: 0x060011AF RID: 4527
		string ReadLine();
	}
}
