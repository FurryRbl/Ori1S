using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200002D RID: 45
	internal class XPrintfFunctions
	{
		// Token: 0x060002FC RID: 764 RVA: 0x00009FAC File Offset: 0x000081AC
		static XPrintfFunctions()
		{
			CdeclFunction @object = new CdeclFunction("msvcrt", "printf", typeof(int));
			XPrintfFunctions.printf = new XPrintfFunctions.XPrintf(@object.Invoke);
			CdeclFunction object2 = new CdeclFunction("msvcrt", "fprintf", typeof(int));
			XPrintfFunctions.fprintf = new XPrintfFunctions.XPrintf(object2.Invoke);
			CdeclFunction object3 = new CdeclFunction("MonoPosixHelper", "Mono_Posix_Stdlib_snprintf", typeof(int));
			XPrintfFunctions.snprintf = new XPrintfFunctions.XPrintf(object3.Invoke);
			CdeclFunction object4 = new CdeclFunction("MonoPosixHelper", "Mono_Posix_Stdlib_syslog2", typeof(int));
			XPrintfFunctions.syslog = new XPrintfFunctions.XPrintf(object4.Invoke);
		}

		// Token: 0x04000120 RID: 288
		internal static XPrintfFunctions.XPrintf printf;

		// Token: 0x04000121 RID: 289
		internal static XPrintfFunctions.XPrintf fprintf;

		// Token: 0x04000122 RID: 290
		internal static XPrintfFunctions.XPrintf snprintf;

		// Token: 0x04000123 RID: 291
		internal static XPrintfFunctions.XPrintf syslog;

		// Token: 0x0200008D RID: 141
		// (Invoke) Token: 0x06000607 RID: 1543
		internal delegate object XPrintf(object[] parameters);
	}
}
