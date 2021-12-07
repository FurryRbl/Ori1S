using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000212 RID: 530
	internal sealed class GUIClip
	{
		// Token: 0x060020D8 RID: 8408 RVA: 0x00026534 File Offset: 0x00024734
		public static Vector2 Unclip(Vector2 pos)
		{
			GUIClip.Unclip_Vector2(ref pos);
			return pos;
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x00026540 File Offset: 0x00024740
		public static Rect Unclip(Rect rect)
		{
			GUIClip.Unclip_Rect(ref rect);
			return rect;
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x0002654C File Offset: 0x0002474C
		public static Vector2 Clip(Vector2 absolutePos)
		{
			GUIClip.Clip_Vector2(ref absolutePos);
			return absolutePos;
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x00026558 File Offset: 0x00024758
		public static Rect Clip(Rect absoluteRect)
		{
			GUIClip.Internal_Clip_Rect(ref absoluteRect);
			return absoluteRect;
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x00026564 File Offset: 0x00024764
		public static Vector2 GetAbsoluteMousePosition()
		{
			Vector2 result;
			GUIClip.Internal_GetAbsoluteMousePosition(out result);
			return result;
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x0002657C File Offset: 0x0002477C
		internal static void Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIClip.INTERNAL_CALL_Push(ref screenRect, ref scrollOffset, ref renderOffset, resetOffset);
		}

		// Token: 0x060020DE RID: 8414
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Push(ref Rect screenRect, ref Vector2 scrollOffset, ref Vector2 renderOffset, bool resetOffset);

		// Token: 0x060020DF RID: 8415
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Pop();

		// Token: 0x060020E0 RID: 8416 RVA: 0x0002658C File Offset: 0x0002478C
		internal static Rect GetTopRect()
		{
			Rect result;
			GUIClip.INTERNAL_CALL_GetTopRect(out result);
			return result;
		}

		// Token: 0x060020E1 RID: 8417
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetTopRect(out Rect value);

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060020E2 RID: 8418
		public static extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060020E3 RID: 8419 RVA: 0x000265A4 File Offset: 0x000247A4
		private static void Unclip_Vector2(ref Vector2 pos)
		{
			GUIClip.INTERNAL_CALL_Unclip_Vector2(ref pos);
		}

		// Token: 0x060020E4 RID: 8420
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Unclip_Vector2(ref Vector2 pos);

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x000265AC File Offset: 0x000247AC
		public static Rect topmostRect
		{
			get
			{
				Rect result;
				GUIClip.INTERNAL_get_topmostRect(out result);
				return result;
			}
		}

		// Token: 0x060020E6 RID: 8422
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_topmostRect(out Rect value);

		// Token: 0x060020E7 RID: 8423 RVA: 0x000265C4 File Offset: 0x000247C4
		private static void Unclip_Rect(ref Rect rect)
		{
			GUIClip.INTERNAL_CALL_Unclip_Rect(ref rect);
		}

		// Token: 0x060020E8 RID: 8424
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Unclip_Rect(ref Rect rect);

		// Token: 0x060020E9 RID: 8425 RVA: 0x000265CC File Offset: 0x000247CC
		private static void Clip_Vector2(ref Vector2 absolutePos)
		{
			GUIClip.INTERNAL_CALL_Clip_Vector2(ref absolutePos);
		}

		// Token: 0x060020EA RID: 8426
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Clip_Vector2(ref Vector2 absolutePos);

		// Token: 0x060020EB RID: 8427 RVA: 0x000265D4 File Offset: 0x000247D4
		private static void Internal_Clip_Rect(ref Rect absoluteRect)
		{
			GUIClip.INTERNAL_CALL_Internal_Clip_Rect(ref absoluteRect);
		}

		// Token: 0x060020EC RID: 8428
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_Clip_Rect(ref Rect absoluteRect);

		// Token: 0x060020ED RID: 8429
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reapply();

		// Token: 0x060020EE RID: 8430 RVA: 0x000265DC File Offset: 0x000247DC
		internal static Matrix4x4 GetMatrix()
		{
			Matrix4x4 result;
			GUIClip.INTERNAL_CALL_GetMatrix(out result);
			return result;
		}

		// Token: 0x060020EF RID: 8431
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetMatrix(out Matrix4x4 value);

		// Token: 0x060020F0 RID: 8432 RVA: 0x000265F4 File Offset: 0x000247F4
		internal static void SetMatrix(Matrix4x4 m)
		{
			GUIClip.INTERNAL_CALL_SetMatrix(ref m);
		}

		// Token: 0x060020F1 RID: 8433
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetMatrix(ref Matrix4x4 m);

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060020F2 RID: 8434 RVA: 0x00026600 File Offset: 0x00024800
		public static Rect visibleRect
		{
			get
			{
				Rect result;
				GUIClip.INTERNAL_get_visibleRect(out result);
				return result;
			}
		}

		// Token: 0x060020F3 RID: 8435
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_visibleRect(out Rect value);

		// Token: 0x060020F4 RID: 8436
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetAbsoluteMousePosition(out Vector2 output);
	}
}
