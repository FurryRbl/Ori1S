using System;

namespace UWPCompat
{
	// Token: 0x02000868 RID: 2152
	public static class Environment
	{
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x000CEB91 File Offset: 0x000CCD91
		public static string MachineName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x000CEB98 File Offset: 0x000CCD98
		public static string[] GetCommandLineArgs()
		{
			return Environment.GetCommandLineArgs();
		}
	}
}
