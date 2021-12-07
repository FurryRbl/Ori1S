using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000014 RID: 20
	public class LogFilter
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004A0C File Offset: 0x00002C0C
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00004A14 File Offset: 0x00002C14
		public static int currentLogLevel
		{
			get
			{
				return LogFilter.s_CurrentLogLevel;
			}
			set
			{
				LogFilter.s_CurrentLogLevel = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004A1C File Offset: 0x00002C1C
		internal static bool logDev
		{
			get
			{
				return LogFilter.s_CurrentLogLevel <= 0;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004A2C File Offset: 0x00002C2C
		public static bool logDebug
		{
			get
			{
				return LogFilter.s_CurrentLogLevel <= 1;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004A3C File Offset: 0x00002C3C
		public static bool logInfo
		{
			get
			{
				return LogFilter.s_CurrentLogLevel <= 2;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004A4C File Offset: 0x00002C4C
		public static bool logWarn
		{
			get
			{
				return LogFilter.s_CurrentLogLevel <= 3;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004A5C File Offset: 0x00002C5C
		public static bool logError
		{
			get
			{
				return LogFilter.s_CurrentLogLevel <= 4;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004A6C File Offset: 0x00002C6C
		public static bool logFatal
		{
			get
			{
				return LogFilter.s_CurrentLogLevel <= 5;
			}
		}

		// Token: 0x04000043 RID: 67
		internal const int Developer = 0;

		// Token: 0x04000044 RID: 68
		public const int Debug = 1;

		// Token: 0x04000045 RID: 69
		public const int Info = 2;

		// Token: 0x04000046 RID: 70
		public const int Warn = 3;

		// Token: 0x04000047 RID: 71
		public const int Error = 4;

		// Token: 0x04000048 RID: 72
		public const int Fatal = 5;

		// Token: 0x04000049 RID: 73
		public static LogFilter.FilterLevel current = LogFilter.FilterLevel.Info;

		// Token: 0x0400004A RID: 74
		private static int s_CurrentLogLevel = 2;

		// Token: 0x02000015 RID: 21
		public enum FilterLevel
		{
			// Token: 0x0400004C RID: 76
			Developer,
			// Token: 0x0400004D RID: 77
			Debug,
			// Token: 0x0400004E RID: 78
			Info,
			// Token: 0x0400004F RID: 79
			Warn,
			// Token: 0x04000050 RID: 80
			Error,
			// Token: 0x04000051 RID: 81
			Fatal
		}
	}
}
