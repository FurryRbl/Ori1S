using System;

// Token: 0x02000065 RID: 101
internal sealed class Locale
{
	// Token: 0x060006D9 RID: 1753 RVA: 0x00015384 File Offset: 0x00013584
	private Locale()
	{
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x0001538C File Offset: 0x0001358C
	public static string GetText(string msg)
	{
		return msg;
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00015390 File Offset: 0x00013590
	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
