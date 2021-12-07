using System;

namespace UnityEngine
{
	// Token: 0x0200032B RID: 811
	public interface ILogHandler
	{
		// Token: 0x06002811 RID: 10257
		void LogFormat(LogType logType, Object context, string format, params object[] args);

		// Token: 0x06002812 RID: 10258
		void LogException(Exception exception, Object context);
	}
}
