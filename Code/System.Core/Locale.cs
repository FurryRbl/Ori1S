using System;

// Token: 0x02000007 RID: 7
internal sealed class Locale
{
	// Token: 0x060000CE RID: 206 RVA: 0x00005840 File Offset: 0x00003A40
	private Locale()
	{
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00005848 File Offset: 0x00003A48
	public static string GetText(string msg)
	{
		return msg;
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x0000584C File Offset: 0x00003A4C
	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
