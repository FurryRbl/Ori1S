using System;

// Token: 0x0200049C RID: 1180
public static class TimeMeasurerHelper
{
	// Token: 0x06001FE6 RID: 8166 RVA: 0x0008BEFB File Offset: 0x0008A0FB
	public static void TakeTimestamp()
	{
		TimeMeasurerHelper.timestamp = DateTime.Now;
	}

	// Token: 0x06001FE7 RID: 8167 RVA: 0x0008BF08 File Offset: 0x0008A108
	public static double GetTimePast()
	{
		return DateTime.Now.Subtract(TimeMeasurerHelper.timestamp).TotalMilliseconds;
	}

	// Token: 0x04001B82 RID: 7042
	private static DateTime timestamp;
}
