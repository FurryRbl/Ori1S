using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000211 RID: 529
	public class GUIUtility
	{
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x000261EC File Offset: 0x000243EC
		internal static float pixelsPerPoint
		{
			get
			{
				return GUIUtility.Internal_GetPixelsPerPoint();
			}
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000261F4 File Offset: 0x000243F4
		public static int GetControlID(FocusType focus)
		{
			return GUIUtility.GetControlID(0, focus);
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x00026200 File Offset: 0x00024400
		public static int GetControlID(GUIContent contents, FocusType focus)
		{
			return GUIUtility.GetControlID(contents.hash, focus);
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x00026210 File Offset: 0x00024410
		public static int GetControlID(FocusType focus, Rect position)
		{
			return GUIUtility.Internal_GetNextControlID2(0, focus, position);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0002621C File Offset: 0x0002441C
		public static int GetControlID(int hint, FocusType focus, Rect position)
		{
			return GUIUtility.Internal_GetNextControlID2(hint, focus, position);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x00026228 File Offset: 0x00024428
		public static int GetControlID(GUIContent contents, FocusType focus, Rect position)
		{
			return GUIUtility.Internal_GetNextControlID2(contents.hash, focus, position);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00026238 File Offset: 0x00024438
		public static object GetStateObject(Type t, int controlID)
		{
			return GUIStateObjects.GetStateObject(t, controlID);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x00026244 File Offset: 0x00024444
		public static object QueryStateObject(Type t, int controlID)
		{
			return GUIStateObjects.QueryStateObject(t, controlID);
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x00026250 File Offset: 0x00024450
		// (set) Token: 0x060020B3 RID: 8371 RVA: 0x00026258 File Offset: 0x00024458
		public static int hotControl
		{
			get
			{
				return GUIUtility.Internal_GetHotControl();
			}
			set
			{
				GUIUtility.Internal_SetHotControl(value);
			}
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x00026260 File Offset: 0x00024460
		public static void ExitGUI()
		{
			throw new ExitGUIException();
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x00026268 File Offset: 0x00024468
		internal static GUISkin GetDefaultSkin()
		{
			return GUIUtility.Internal_GetDefaultSkin(GUIUtility.s_SkinMode);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x00026274 File Offset: 0x00024474
		internal static GUISkin GetBuiltinSkin(int skin)
		{
			return GUIUtility.Internal_GetBuiltinSkin(skin) as GUISkin;
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00026284 File Offset: 0x00024484
		[RequiredByNativeCode]
		internal static void BeginGUI(int skinMode, int instanceID, int useGUILayout)
		{
			GUIUtility.s_SkinMode = skinMode;
			GUIUtility.s_OriginalID = instanceID;
			GUI.skin = null;
			if (useGUILayout != 0)
			{
				GUILayoutUtility.SelectIDList(instanceID, false);
				GUILayoutUtility.Begin(instanceID);
			}
			GUI.changed = false;
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x000262C0 File Offset: 0x000244C0
		[RequiredByNativeCode]
		internal static void EndGUI(int layoutType)
		{
			try
			{
				if (Event.current.type == EventType.Layout)
				{
					switch (layoutType)
					{
					case 1:
						GUILayoutUtility.Layout();
						break;
					case 2:
						GUILayoutUtility.LayoutFromEditorWindow();
						break;
					}
				}
				GUILayoutUtility.SelectIDList(GUIUtility.s_OriginalID, false);
				GUIContent.ClearStaticCache();
			}
			finally
			{
				GUIUtility.Internal_ExitGUI();
			}
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x00026348 File Offset: 0x00024548
		[RequiredByNativeCode]
		internal static bool EndGUIFromException(Exception exception)
		{
			if (exception == null)
			{
				return false;
			}
			if (!(exception is ExitGUIException) && !(exception.InnerException is ExitGUIException))
			{
				return false;
			}
			GUIUtility.Internal_ExitGUI();
			return true;
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x00026378 File Offset: 0x00024578
		internal static void CheckOnGUI()
		{
			if (GUIUtility.Internal_GetGUIDepth() <= 0)
			{
				throw new ArgumentException("You can only call GUI functions from inside OnGUI.");
			}
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x00026390 File Offset: 0x00024590
		public static Vector2 GUIToScreenPoint(Vector2 guiPoint)
		{
			return GUIClip.Unclip(guiPoint) + GUIUtility.s_EditorScreenPointOffset;
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000263A4 File Offset: 0x000245A4
		internal static Rect GUIToScreenRect(Rect guiRect)
		{
			Vector2 vector = GUIUtility.GUIToScreenPoint(new Vector2(guiRect.x, guiRect.y));
			guiRect.x = vector.x;
			guiRect.y = vector.y;
			return guiRect;
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x000263E8 File Offset: 0x000245E8
		public static Vector2 ScreenToGUIPoint(Vector2 screenPoint)
		{
			return GUIClip.Clip(screenPoint) - GUIUtility.s_EditorScreenPointOffset;
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000263FC File Offset: 0x000245FC
		public static Rect ScreenToGUIRect(Rect screenRect)
		{
			Vector2 vector = GUIUtility.ScreenToGUIPoint(new Vector2(screenRect.x, screenRect.y));
			screenRect.x = vector.x;
			screenRect.y = vector.y;
			return screenRect;
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x00026440 File Offset: 0x00024640
		public static void RotateAroundPivot(float angle, Vector2 pivotPoint)
		{
			Matrix4x4 matrix = GUI.matrix;
			GUI.matrix = Matrix4x4.identity;
			Vector2 vector = GUIClip.Unclip(pivotPoint);
			Matrix4x4 lhs = Matrix4x4.TRS(vector, Quaternion.Euler(0f, 0f, angle), Vector3.one) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.matrix = lhs * matrix;
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x000264B0 File Offset: 0x000246B0
		public static void ScaleAroundPivot(Vector2 scale, Vector2 pivotPoint)
		{
			Matrix4x4 matrix = GUI.matrix;
			Vector2 vector = GUIClip.Unclip(pivotPoint);
			Matrix4x4 lhs = Matrix4x4.TRS(vector, Quaternion.identity, new Vector3(scale.x, scale.y, 1f)) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.matrix = lhs * matrix;
		}

		// Token: 0x060020C1 RID: 8385
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetPixelsPerPoint();

		// Token: 0x060020C2 RID: 8386
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetControlID(int hint, FocusType focus);

		// Token: 0x060020C3 RID: 8387 RVA: 0x00026520 File Offset: 0x00024720
		private static int Internal_GetNextControlID2(int hint, FocusType focusType, Rect rect)
		{
			return GUIUtility.INTERNAL_CALL_Internal_GetNextControlID2(hint, focusType, ref rect);
		}

		// Token: 0x060020C4 RID: 8388
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_Internal_GetNextControlID2(int hint, FocusType focusType, ref Rect rect);

		// Token: 0x060020C5 RID: 8389
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetPermanentControlID();

		// Token: 0x060020C6 RID: 8390
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetHotControl();

		// Token: 0x060020C7 RID: 8391
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetHotControl(int value);

		// Token: 0x060020C8 RID: 8392
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UpdateUndoName();

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060020C9 RID: 8393
		// (set) Token: 0x060020CA RID: 8394
		public static extern int keyboardControl { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060020CB RID: 8395
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetDidGUIWindowsEatLastEvent(bool value);

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060020CC RID: 8396
		// (set) Token: 0x060020CD RID: 8397
		public static extern string systemCopyBuffer { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060020CE RID: 8398
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GUISkin Internal_GetDefaultSkin(int skinMode);

		// Token: 0x060020CF RID: 8399
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_GetBuiltinSkin(int skin);

		// Token: 0x060020D0 RID: 8400
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ExitGUI();

		// Token: 0x060020D1 RID: 8401
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Internal_GetGUIDepth();

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060020D2 RID: 8402
		// (set) Token: 0x060020D3 RID: 8403
		internal static extern bool mouseUsed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060020D4 RID: 8404
		public static extern bool hasModalWindow { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060020D5 RID: 8405
		// (set) Token: 0x060020D6 RID: 8406
		internal static extern bool textFieldInput { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x04000834 RID: 2100
		internal static int s_SkinMode;

		// Token: 0x04000835 RID: 2101
		internal static int s_OriginalID;

		// Token: 0x04000836 RID: 2102
		internal static Vector2 s_EditorScreenPointOffset = Vector2.zero;

		// Token: 0x04000837 RID: 2103
		internal static bool s_HasKeyboardFocus = false;
	}
}
