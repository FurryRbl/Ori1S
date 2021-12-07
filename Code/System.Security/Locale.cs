using System;

// Token: 0x02000003 RID: 3
internal sealed class Locale
{
	// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
	private Locale()
	{
	}

	// Token: 0x06000002 RID: 2 RVA: 0x000020F4 File Offset: 0x000002F4
	public static string GetText(string msg)
	{
		return msg;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000020F8 File Offset: 0x000002F8
	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
