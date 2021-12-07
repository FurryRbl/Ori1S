using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200001D RID: 29
	public sealed class Cursor
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00002898 File Offset: 0x00000A98
		private static void SetCursor(Texture2D texture, CursorMode cursorMode)
		{
			Cursor.SetCursor(texture, Vector2.zero, cursorMode);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000028A8 File Offset: 0x00000AA8
		public static void SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode)
		{
			Cursor.INTERNAL_CALL_SetCursor(texture, ref hotspot, cursorMode);
		}

		// Token: 0x060000BA RID: 186
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetCursor(Texture2D texture, ref Vector2 hotspot, CursorMode cursorMode);

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000BB RID: 187
		// (set) Token: 0x060000BC RID: 188
		public static extern bool visible { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000BD RID: 189
		// (set) Token: 0x060000BE RID: 190
		public static extern CursorLockMode lockState { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
