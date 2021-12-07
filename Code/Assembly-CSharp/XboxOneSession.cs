using System;

// Token: 0x0200014C RID: 332
public class XboxOneSession
{
	// Token: 0x17000284 RID: 644
	// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0003E1CF File Offset: 0x0003C3CF
	// (set) Token: 0x06000D56 RID: 3414 RVA: 0x0003E1D6 File Offset: 0x0003C3D6
	public static Action OnSessionStarted { get; set; }

	// Token: 0x17000285 RID: 645
	// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0003E1DE File Offset: 0x0003C3DE
	// (set) Token: 0x06000D58 RID: 3416 RVA: 0x0003E1E5 File Offset: 0x0003C3E5
	public static Action OnSessionEnded { get; set; }

	// Token: 0x17000286 RID: 646
	// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0003E1ED File Offset: 0x0003C3ED
	// (set) Token: 0x06000D5A RID: 3418 RVA: 0x0003E1F4 File Offset: 0x0003C3F4
	public static Action OnWindowDeactivated { get; set; }

	// Token: 0x17000287 RID: 647
	// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0003E1FC File Offset: 0x0003C3FC
	// (set) Token: 0x06000D5C RID: 3420 RVA: 0x0003E203 File Offset: 0x0003C403
	public static Action OnWindowActivated { get; set; }

	// Token: 0x17000288 RID: 648
	// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0003E20B File Offset: 0x0003C40B
	// (set) Token: 0x06000D5E RID: 3422 RVA: 0x0003E212 File Offset: 0x0003C412
	public static Action OnSuspend { get; set; }

	// Token: 0x17000289 RID: 649
	// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0003E21A File Offset: 0x0003C41A
	// (set) Token: 0x06000D60 RID: 3424 RVA: 0x0003E221 File Offset: 0x0003C421
	public static Action OnResume { get; set; }

	// Token: 0x1700028A RID: 650
	// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0003E229 File Offset: 0x0003C429
	public static bool IsHighResources
	{
		get
		{
			return XboxOneSession.m_windowActivated;
		}
	}

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0003E230 File Offset: 0x0003C430
	public static bool SuspendDataAvailable
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700028C RID: 652
	// (get) Token: 0x06000D63 RID: 3427 RVA: 0x0003E233 File Offset: 0x0003C433
	public static Guid SessionID
	{
		get
		{
			return Guid.Empty;
		}
	}

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0003E23A File Offset: 0x0003C43A
	public static bool SessionActive
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x0003E23D File Offset: 0x0003C43D
	public static bool ClearSuspendData()
	{
		return false;
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x0003E240 File Offset: 0x0003C440
	public static bool RequireSession(Action success = null, Action failure = null)
	{
		return false;
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x0003E243 File Offset: 0x0003C443
	public static bool RestartSession(Action success = null, Action failure = null)
	{
		return false;
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x0003E246 File Offset: 0x0003C446
	public static void ClearGUID()
	{
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x0003E248 File Offset: 0x0003C448
	public static bool EndSession()
	{
		return false;
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x0003E24B File Offset: 0x0003C44B
	public static bool ResumeSession()
	{
		return false;
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x0003E24E File Offset: 0x0003C44E
	public static bool PauseSession()
	{
		return false;
	}

	// Token: 0x04000AE7 RID: 2791
	private static bool m_windowActivated = true;
}
