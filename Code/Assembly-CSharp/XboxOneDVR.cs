using System;

// Token: 0x02000249 RID: 585
public class XboxOneDVR
{
	// Token: 0x1700038A RID: 906
	// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0005B105 File Offset: 0x00059305
	public static bool CanUseDVR
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700038B RID: 907
	// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0005B108 File Offset: 0x00059308
	// (set) Token: 0x060013E8 RID: 5096 RVA: 0x0005B10F File Offset: 0x0005930F
	public static bool EnableDVR { get; set; }

	// Token: 0x1700038C RID: 908
	// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0005B117 File Offset: 0x00059317
	// (set) Token: 0x060013EA RID: 5098 RVA: 0x0005B11A File Offset: 0x0005931A
	public static bool WaitingForClip
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	// Token: 0x060013EB RID: 5099 RVA: 0x0005B11C File Offset: 0x0005931C
	public static bool RecordPast(float seconds, string clipName, bool cancelExisting = false)
	{
		return false;
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x0005B11F File Offset: 0x0005931F
	public static bool RecordFuture(float seconds, string clipName, bool cancelExisting = false)
	{
		return false;
	}

	// Token: 0x060013ED RID: 5101 RVA: 0x0005B122 File Offset: 0x00059322
	public static bool StartRecording(bool cancelExisting = false)
	{
		return false;
	}

	// Token: 0x060013EE RID: 5102 RVA: 0x0005B125 File Offset: 0x00059325
	public static bool StopRecording(string clipName)
	{
		return false;
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x0005B128 File Offset: 0x00059328
	public static bool CancelRecording()
	{
		return false;
	}

	// Token: 0x04001184 RID: 4484
	public const float kMinRecordTime = 6f;

	// Token: 0x04001185 RID: 4485
	public const float kMaxRecordTime = 300f;

	// Token: 0x04001186 RID: 4486
	public static string AIRSTRIKE_ID = "3";

	// Token: 0x04001187 RID: 4487
	public static string BASHING_SPREE_ID = "2";

	// Token: 0x04001188 RID: 4488
	public static string CRUSHED_RAM_ID = "6";

	// Token: 0x04001189 RID: 4489
	public static string PROJECTILE_TAKEDOWN_ID = "4";

	// Token: 0x0400118A RID: 4490
	public static string QUINTUPLE_JUMP_ID = "1";

	// Token: 0x0400118B RID: 4491
	public static string SUPER_JUMP_ID = "5";

	// Token: 0x0400118C RID: 4492
	public static string TRICKED_ENEMY_ID = "7";

	// Token: 0x0400118D RID: 4493
	public static string ESCAPED_BOULDER_ID = "8";
}
