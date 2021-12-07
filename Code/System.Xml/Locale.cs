using System;

// Token: 0x02000014 RID: 20
internal sealed class Locale
{
	// Token: 0x06000072 RID: 114 RVA: 0x000069B4 File Offset: 0x00004BB4
	private Locale()
	{
	}

	// Token: 0x06000073 RID: 115 RVA: 0x000069BC File Offset: 0x00004BBC
	public static string GetText(string msg)
	{
		return msg;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x000069C0 File Offset: 0x00004BC0
	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
