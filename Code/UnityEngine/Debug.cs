using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000B5 RID: 181
	public sealed class Debug
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public static ILogger logger
		{
			get
			{
				return Debug.s_Logger;
			}
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0000EB1C File Offset: 0x0000CD1C
		public static void DrawLine(Vector3 start, Vector3 end, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
		{
			Debug.INTERNAL_CALL_DrawLine(ref start, ref end, ref color, duration, depthTest);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
		{
			bool depthTest = true;
			Debug.INTERNAL_CALL_DrawLine(ref start, ref end, ref color, duration, depthTest);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0000EB48 File Offset: 0x0000CD48
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color)
		{
			bool depthTest = true;
			float duration = 0f;
			Debug.INTERNAL_CALL_DrawLine(ref start, ref end, ref color, duration, depthTest);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end)
		{
			bool depthTest = true;
			float duration = 0f;
			Color white = Color.white;
			Debug.INTERNAL_CALL_DrawLine(ref start, ref end, ref white, duration, depthTest);
		}

		// Token: 0x06000AD1 RID: 2769
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawLine(ref Vector3 start, ref Vector3 end, ref Color color, float duration, bool depthTest);

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0000EB94 File Offset: 0x0000CD94
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
		{
			bool depthTest = true;
			Debug.DrawRay(start, dir, color, duration, depthTest);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color)
		{
			bool depthTest = true;
			float duration = 0f;
			Debug.DrawRay(start, dir, color, duration, depthTest);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir)
		{
			bool depthTest = true;
			float duration = 0f;
			Color white = Color.white;
			Debug.DrawRay(start, dir, white, duration, depthTest);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		public static void DrawRay(Vector3 start, Vector3 dir, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
		{
			Debug.DrawLine(start, start + dir, color, duration, depthTest);
		}

		// Token: 0x06000AD6 RID: 2774
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Break();

		// Token: 0x06000AD7 RID: 2775
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DebugBreak();

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0000EC0C File Offset: 0x0000CE0C
		public static void Log(object message)
		{
			Debug.logger.Log(LogType.Log, message);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		public static void Log(object message, Object context)
		{
			Debug.logger.Log(LogType.Log, message, context);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0000EC2C File Offset: 0x0000CE2C
		public static void LogFormat(string format, params object[] args)
		{
			Debug.logger.LogFormat(LogType.Log, format, args);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0000EC3C File Offset: 0x0000CE3C
		public static void LogFormat(Object context, string format, params object[] args)
		{
			Debug.logger.LogFormat(LogType.Log, context, format, args);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0000EC4C File Offset: 0x0000CE4C
		public static void LogError(object message)
		{
			Debug.logger.Log(LogType.Error, message);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0000EC5C File Offset: 0x0000CE5C
		public static void LogError(object message, Object context)
		{
			Debug.logger.Log(LogType.Error, message, context);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public static void LogErrorFormat(string format, params object[] args)
		{
			Debug.logger.LogFormat(LogType.Error, format, args);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0000EC7C File Offset: 0x0000CE7C
		public static void LogErrorFormat(Object context, string format, params object[] args)
		{
			Debug.logger.LogFormat(LogType.Error, context, format, args);
		}

		// Token: 0x06000AE0 RID: 2784
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearDeveloperConsole();

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000AE1 RID: 2785
		// (set) Token: 0x06000AE2 RID: 2786
		public static extern bool developerConsoleVisible { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0000EC8C File Offset: 0x0000CE8C
		public static void LogException(Exception exception)
		{
			Debug.logger.LogException(exception, null);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		public static void LogException(Exception exception, Object context)
		{
			Debug.logger.LogException(exception, context);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0000ECAC File Offset: 0x0000CEAC
		public static void LogWarning(object message)
		{
			Debug.logger.Log(LogType.Warning, message);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0000ECBC File Offset: 0x0000CEBC
		public static void LogWarning(object message, Object context)
		{
			Debug.logger.Log(LogType.Warning, message, context);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0000ECCC File Offset: 0x0000CECC
		public static void LogWarningFormat(string format, params object[] args)
		{
			Debug.logger.LogFormat(LogType.Warning, format, args);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		public static void LogWarningFormat(Object context, string format, params object[] args)
		{
			Debug.logger.LogFormat(LogType.Warning, context, format, args);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition)
		{
			if (!condition)
			{
				Debug.logger.Log(LogType.Assert, "Assertion failed");
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0000ED04 File Offset: 0x0000CF04
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, Object context)
		{
			if (!condition)
			{
				Debug.logger.Log(LogType.Assert, "Assertion failed", context);
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0000ED20 File Offset: 0x0000CF20
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, object message)
		{
			if (!condition)
			{
				Debug.logger.Log(LogType.Assert, message);
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0000ED34 File Offset: 0x0000CF34
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, string message)
		{
			if (!condition)
			{
				Debug.logger.Log(LogType.Assert, message);
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0000ED48 File Offset: 0x0000CF48
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, object message, Object context)
		{
			if (!condition)
			{
				Debug.logger.Log(LogType.Assert, message, context);
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0000ED60 File Offset: 0x0000CF60
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, string message, Object context)
		{
			if (!condition)
			{
				Debug.logger.Log(LogType.Assert, message, context);
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0000ED78 File Offset: 0x0000CF78
		[Conditional("UNITY_ASSERTIONS")]
		public static void AssertFormat(bool condition, string format, params object[] args)
		{
			if (!condition)
			{
				Debug.logger.LogFormat(LogType.Assert, format, args);
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0000ED90 File Offset: 0x0000CF90
		[Conditional("UNITY_ASSERTIONS")]
		public static void AssertFormat(bool condition, Object context, string format, params object[] args)
		{
			if (!condition)
			{
				Debug.logger.LogFormat(LogType.Assert, context, format, args);
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message)
		{
			Debug.logger.Log(LogType.Assert, message);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0000EDB8 File Offset: 0x0000CFB8
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message, Object context)
		{
			Debug.logger.Log(LogType.Assert, message, context);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertionFormat(string format, params object[] args)
		{
			Debug.logger.LogFormat(LogType.Assert, format, args);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertionFormat(Object context, string format, params object[] args)
		{
			Debug.logger.LogFormat(LogType.Assert, context, format, args);
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000AF5 RID: 2805
		public static extern bool isDebugBuild { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000AF6 RID: 2806
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void OpenConsoleFile();

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0000EDE8 File Offset: 0x0000CFE8
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Assert(bool, string, params object[]) is obsolete. Use AssertFormat(bool, string, params object[]) (UnityUpgradable) -> AssertFormat(*)", true)]
		public static void Assert(bool condition, string format, params object[] args)
		{
			if (!condition)
			{
				Debug.logger.LogFormat(LogType.Assert, format, args);
			}
		}

		// Token: 0x0400021D RID: 541
		internal static Logger s_Logger = new Logger(new DebugLogHandler());
	}
}
