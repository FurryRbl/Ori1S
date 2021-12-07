using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B4 RID: 180
	internal sealed class DebugLogHandler : ILogHandler
	{
		// Token: 0x06000AC6 RID: 2758
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Log(LogType level, string msg, [Writable] Object obj);

		// Token: 0x06000AC7 RID: 2759
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_LogException(Exception exception, [Writable] Object obj);

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0000EAD8 File Offset: 0x0000CCD8
		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			DebugLogHandler.Internal_Log(logType, string.Format(format, args), context);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		public void LogException(Exception exception, Object context)
		{
			DebugLogHandler.Internal_LogException(exception, context);
		}
	}
}
