using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000A8 RID: 168
	public sealed class ColorUtility
	{
		// Token: 0x06000996 RID: 2454
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool DoTryParseHtmlColor(string htmlString, out Color32 color);

		// Token: 0x06000997 RID: 2455 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		public static bool TryParseHtmlString(string htmlString, out Color color)
		{
			Color32 c;
			bool result = ColorUtility.DoTryParseHtmlColor(htmlString, out c);
			color = c;
			return result;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0000E000 File Offset: 0x0000C200
		public static string ToHtmlStringRGB(Color color)
		{
			Color32 color2 = color;
			return string.Format("{0:X2}{1:X2}{2:X2}", color2.r, color2.g, color2.b);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0000E044 File Offset: 0x0000C244
		public static string ToHtmlStringRGBA(Color color)
		{
			Color32 color2 = color;
			return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", new object[]
			{
				color2.r,
				color2.g,
				color2.b,
				color2.a
			});
		}
	}
}
