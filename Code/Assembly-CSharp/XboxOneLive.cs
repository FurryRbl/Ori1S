using System;

// Token: 0x02000293 RID: 659
public class XboxOneLive
{
	// Token: 0x170003CD RID: 973
	// (get) Token: 0x0600155D RID: 5469 RVA: 0x0005F096 File Offset: 0x0005D296
	public static bool LiveOnline
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170003CE RID: 974
	// (get) Token: 0x0600155E RID: 5470 RVA: 0x0005F099 File Offset: 0x0005D299
	public static bool Online
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170003CF RID: 975
	// (get) Token: 0x0600155F RID: 5471 RVA: 0x0005F09C File Offset: 0x0005D29C
	// (set) Token: 0x06001560 RID: 5472 RVA: 0x0005F0A3 File Offset: 0x0005D2A3
	public static Action OnOnlineStateChanged { get; set; }
}
