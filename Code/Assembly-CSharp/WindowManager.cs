using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x020006CB RID: 1739
public class WindowManager : MonoBehaviour
{
	// Token: 0x060029AD RID: 10669
	[DllImport("USER32.DLL")]
	public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

	// Token: 0x060029AE RID: 10670
	[DllImport("USER32.DLL")]
	public static extern int GetWindowLong(IntPtr hWnd, int index);

	// Token: 0x060029AF RID: 10671
	[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
	private static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

	// Token: 0x060029B0 RID: 10672
	[DllImport("user32.dll")]
	private static extern bool DrawMenuBar(IntPtr hWnd);

	// Token: 0x060029B1 RID: 10673
	[DllImport("user32.dll")]
	public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

	// Token: 0x060029B2 RID: 10674
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

	// Token: 0x060029B3 RID: 10675
	[DllImport("user32.dll")]
	private static extern IntPtr GetForegroundWindow();

	// Token: 0x060029B4 RID: 10676
	[DllImport("user32.dll")]
	private static extern IntPtr GetActiveWindow();

	// Token: 0x060029B5 RID: 10677
	[DllImport("user32.dll")]
	private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

	// Token: 0x060029B6 RID: 10678
	[DllImport("user32.dll")]
	private static extern IntPtr GetFocus();

	// Token: 0x060029B7 RID: 10679 RVA: 0x000B3A96 File Offset: 0x000B1C96
	public void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x060029B8 RID: 10680 RVA: 0x000B3AA4 File Offset: 0x000B1CA4
	public int MakeBorderless()
	{
		IntPtr activeWindow = WindowManager.GetActiveWindow();
		RECT rect = default(RECT);
		WindowManager.GetWindowRect(activeWindow, out rect);
		int windowLong = WindowManager.GetWindowLong(WindowManager.GetActiveWindow(), -16);
		int num = 13565952;
		num = ~num;
		this.UpdateWindowProperties(windowLong & num);
		return windowLong;
	}

	// Token: 0x060029B9 RID: 10681 RVA: 0x000B3AE8 File Offset: 0x000B1CE8
	public void MakeBordered()
	{
		IntPtr activeWindow = WindowManager.GetActiveWindow();
		RECT rect = default(RECT);
		WindowManager.GetWindowRect(activeWindow, out rect);
		int num = Screen.width;
		int num2 = Screen.height;
		this.UpdateWindowProperties(13565952);
		RECT rect2 = default(RECT);
		rect2.X = rect.X;
		rect2.Y = rect.Y;
		rect2.Width = num;
		rect2.Height = num2;
		WindowManager.SetWindowPos(activeWindow, 0, rect2.X, rect2.Y, rect2.Width, rect2.Height, 64);
	}

	// Token: 0x060029BA RID: 10682 RVA: 0x000B3B80 File Offset: 0x000B1D80
	public void UpdateWindowProperties(int properties)
	{
		IntPtr activeWindow = WindowManager.GetActiveWindow();
		RECT rect = default(RECT);
		WindowManager.GetWindowRect(activeWindow, out rect);
		WindowManager.SetWindowLong(activeWindow, -16, properties);
		WindowManager.SetWindowPos(activeWindow, 0, rect.X, rect.Y, rect.Width, rect.Height, 64);
	}

	// Token: 0x060029BB RID: 10683 RVA: 0x000B3BD4 File Offset: 0x000B1DD4
	public void KickWindow()
	{
		IntPtr activeWindow = WindowManager.GetActiveWindow();
		RECT rect = default(RECT);
		WindowManager.GetWindowRect(activeWindow, out rect);
		WindowManager.SetWindowPos(activeWindow, 0, rect.X, rect.Y, rect.Width, rect.Height, 64);
	}

	// Token: 0x060029BC RID: 10684 RVA: 0x000B3C20 File Offset: 0x000B1E20
	public bool IsFullScreen()
	{
		IntPtr activeWindow = WindowManager.GetActiveWindow();
		int windowLong = WindowManager.GetWindowLong(activeWindow, -16);
		return Screen.fullScreen;
	}

	// Token: 0x060029BD RID: 10685 RVA: 0x000B3C44 File Offset: 0x000B1E44
	public bool IsMaximized()
	{
		IntPtr activeWindow = WindowManager.GetActiveWindow();
		int windowLong = WindowManager.GetWindowLong(activeWindow, -16);
		return (windowLong & 16777216) != 0;
	}

	// Token: 0x060029BE RID: 10686 RVA: 0x000B3C70 File Offset: 0x000B1E70
	public bool IsMinimized()
	{
		IntPtr activeWindow = WindowManager.GetActiveWindow();
		int windowLong = WindowManager.GetWindowLong(activeWindow, -16);
		return (windowLong & 536870912) != 0;
	}

	// Token: 0x060029BF RID: 10687 RVA: 0x000B3C9C File Offset: 0x000B1E9C
	public bool HasFocus()
	{
		IntPtr activeWindow = WindowManager.GetActiveWindow();
		IntPtr focus = WindowManager.GetFocus();
		return activeWindow == focus;
	}

	// Token: 0x060029C0 RID: 10688 RVA: 0x000B3CC4 File Offset: 0x000B1EC4
	public void Update()
	{
		if (this.m_lastTimeCheckedMinimized + 0.2f < Time.realtimeSinceStartup)
		{
			this.m_lastTimeCheckedMinimized = Time.realtimeSinceStartup;
			Application.runInBackground = !this.IsMinimized();
		}
	}

	// Token: 0x04002522 RID: 9506
	private const int GWL_STYLE = -16;

	// Token: 0x04002523 RID: 9507
	private const int WS_BORDER = 8388608;

	// Token: 0x04002524 RID: 9508
	private const int WS_CAPTION = 12582912;

	// Token: 0x04002525 RID: 9509
	private const int WS_SYSMENU = 524288;

	// Token: 0x04002526 RID: 9510
	private const int WS_MINIMIZEBOX = 131072;

	// Token: 0x04002527 RID: 9511
	private const int WS_MAXIMIZEBOX = 65536;

	// Token: 0x04002528 RID: 9512
	private const int SWP_SHOWWINDOW = 64;

	// Token: 0x04002529 RID: 9513
	private const int WS_SIZEBOX = 262144;

	// Token: 0x0400252A RID: 9514
	private const int WS_MINIMIZE = 536870912;

	// Token: 0x0400252B RID: 9515
	private const int WS_MAXIMIZE = 16777216;

	// Token: 0x0400252C RID: 9516
	private int m_borderlessSetCounter;

	// Token: 0x0400252D RID: 9517
	private float m_lastTimeCheckedMinimized;

	// Token: 0x0400252E RID: 9518
	private int lastProps;

	// Token: 0x0400252F RID: 9519
	private bool m_firstTime = true;

	// Token: 0x04002530 RID: 9520
	private bool wasFullScreen;

	// Token: 0x04002531 RID: 9521
	private bool m_wasFocused;

	// Token: 0x04002532 RID: 9522
	private bool m_borderless;

	// Token: 0x04002533 RID: 9523
	private int width = 1280;

	// Token: 0x04002534 RID: 9524
	private int height = 720;

	// Token: 0x04002535 RID: 9525
	private bool m_fullScreen;
}
