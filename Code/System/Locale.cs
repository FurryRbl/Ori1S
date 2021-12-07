using System;

// Token: 0x02000004 RID: 4
internal sealed class Locale
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002104 File Offset: 0x00000304
	private Locale()
	{
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000210C File Offset: 0x0000030C
	public static string GetText(string msg)
	{
		return msg;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002110 File Offset: 0x00000310
	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
