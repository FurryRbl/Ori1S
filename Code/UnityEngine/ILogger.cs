using System;

namespace UnityEngine
{
	// Token: 0x0200032A RID: 810
	public interface ILogger : ILogHandler
	{
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060027FD RID: 10237
		// (set) Token: 0x060027FE RID: 10238
		ILogHandler logHandler { get; set; }

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x060027FF RID: 10239
		// (set) Token: 0x06002800 RID: 10240
		bool logEnabled { get; set; }

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002801 RID: 10241
		// (set) Token: 0x06002802 RID: 10242
		LogType filterLogType { get; set; }

		// Token: 0x06002803 RID: 10243
		bool IsLogTypeAllowed(LogType logType);

		// Token: 0x06002804 RID: 10244
		void Log(LogType logType, object message);

		// Token: 0x06002805 RID: 10245
		void Log(LogType logType, object message, Object context);

		// Token: 0x06002806 RID: 10246
		void Log(LogType logType, string tag, object message);

		// Token: 0x06002807 RID: 10247
		void Log(LogType logType, string tag, object message, Object context);

		// Token: 0x06002808 RID: 10248
		void Log(object message);

		// Token: 0x06002809 RID: 10249
		void Log(string tag, object message);

		// Token: 0x0600280A RID: 10250
		void Log(string tag, object message, Object context);

		// Token: 0x0600280B RID: 10251
		void LogWarning(string tag, object message);

		// Token: 0x0600280C RID: 10252
		void LogWarning(string tag, object message, Object context);

		// Token: 0x0600280D RID: 10253
		void LogError(string tag, object message);

		// Token: 0x0600280E RID: 10254
		void LogError(string tag, object message, Object context);

		// Token: 0x0600280F RID: 10255
		void LogFormat(LogType logType, string format, params object[] args);

		// Token: 0x06002810 RID: 10256
		void LogException(Exception exception);
	}
}
