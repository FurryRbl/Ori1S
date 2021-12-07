using System;

namespace UnityEngine
{
	// Token: 0x0200032C RID: 812
	public class Logger : ILogger, ILogHandler
	{
		// Token: 0x06002813 RID: 10259 RVA: 0x000392F4 File Offset: 0x000374F4
		private Logger()
		{
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x000392FC File Offset: 0x000374FC
		public Logger(ILogHandler logHandler)
		{
			this.logHandler = logHandler;
			this.logEnabled = true;
			this.filterLogType = LogType.Log;
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002815 RID: 10261 RVA: 0x00039324 File Offset: 0x00037524
		// (set) Token: 0x06002816 RID: 10262 RVA: 0x0003932C File Offset: 0x0003752C
		public ILogHandler logHandler { get; set; }

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002817 RID: 10263 RVA: 0x00039338 File Offset: 0x00037538
		// (set) Token: 0x06002818 RID: 10264 RVA: 0x00039340 File Offset: 0x00037540
		public bool logEnabled { get; set; }

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002819 RID: 10265 RVA: 0x0003934C File Offset: 0x0003754C
		// (set) Token: 0x0600281A RID: 10266 RVA: 0x00039354 File Offset: 0x00037554
		public LogType filterLogType { get; set; }

		// Token: 0x0600281B RID: 10267 RVA: 0x00039360 File Offset: 0x00037560
		public bool IsLogTypeAllowed(LogType logType)
		{
			return this.logEnabled && (logType <= this.filterLogType || logType == LogType.Exception);
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x00039390 File Offset: 0x00037590
		private static string GetString(object message)
		{
			return (message == null) ? "Null" : message.ToString();
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000393A8 File Offset: 0x000375A8
		public void Log(LogType logType, object message)
		{
			if (this.IsLogTypeAllowed(logType))
			{
				this.logHandler.LogFormat(logType, null, "{0}", new object[]
				{
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000393E4 File Offset: 0x000375E4
		public void Log(LogType logType, object message, Object context)
		{
			if (this.IsLogTypeAllowed(logType))
			{
				this.logHandler.LogFormat(logType, context, "{0}", new object[]
				{
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x00039420 File Offset: 0x00037620
		public void Log(LogType logType, string tag, object message)
		{
			if (this.IsLogTypeAllowed(logType))
			{
				this.logHandler.LogFormat(logType, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x00039460 File Offset: 0x00037660
		public void Log(LogType logType, string tag, object message, Object context)
		{
			if (this.IsLogTypeAllowed(logType))
			{
				this.logHandler.LogFormat(logType, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000394A0 File Offset: 0x000376A0
		public void Log(object message)
		{
			if (this.IsLogTypeAllowed(LogType.Log))
			{
				this.logHandler.LogFormat(LogType.Log, null, "{0}", new object[]
				{
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000394DC File Offset: 0x000376DC
		public void Log(string tag, object message)
		{
			if (this.IsLogTypeAllowed(LogType.Log))
			{
				this.logHandler.LogFormat(LogType.Log, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x0003951C File Offset: 0x0003771C
		public void Log(string tag, object message, Object context)
		{
			if (this.IsLogTypeAllowed(LogType.Log))
			{
				this.logHandler.LogFormat(LogType.Log, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x0003955C File Offset: 0x0003775C
		public void LogWarning(string tag, object message)
		{
			if (this.IsLogTypeAllowed(LogType.Warning))
			{
				this.logHandler.LogFormat(LogType.Warning, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x0003959C File Offset: 0x0003779C
		public void LogWarning(string tag, object message, Object context)
		{
			if (this.IsLogTypeAllowed(LogType.Warning))
			{
				this.logHandler.LogFormat(LogType.Warning, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x000395DC File Offset: 0x000377DC
		public void LogError(string tag, object message)
		{
			if (this.IsLogTypeAllowed(LogType.Error))
			{
				this.logHandler.LogFormat(LogType.Error, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x0003961C File Offset: 0x0003781C
		public void LogError(string tag, object message, Object context)
		{
			if (this.IsLogTypeAllowed(LogType.Error))
			{
				this.logHandler.LogFormat(LogType.Error, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x0003965C File Offset: 0x0003785C
		public void LogFormat(LogType logType, string format, params object[] args)
		{
			if (this.IsLogTypeAllowed(logType))
			{
				this.logHandler.LogFormat(logType, null, format, args);
			}
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x00039684 File Offset: 0x00037884
		public void LogException(Exception exception)
		{
			if (this.logEnabled)
			{
				this.logHandler.LogException(exception, null);
			}
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000396A0 File Offset: 0x000378A0
		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			if (this.IsLogTypeAllowed(logType))
			{
				this.logHandler.LogFormat(logType, context, format, args);
			}
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000396CC File Offset: 0x000378CC
		public void LogException(Exception exception, Object context)
		{
			if (this.logEnabled)
			{
				this.logHandler.LogException(exception, context);
			}
		}

		// Token: 0x04000C52 RID: 3154
		private const string kNoTagFormat = "{0}";

		// Token: 0x04000C53 RID: 3155
		private const string kTagFormat = "{0}: {1}";
	}
}
