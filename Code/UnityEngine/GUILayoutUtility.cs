using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020001FD RID: 509
	public class GUILayoutUtility
	{
		// Token: 0x06001F89 RID: 8073 RVA: 0x00021A94 File Offset: 0x0001FC94
		internal static GUILayoutUtility.LayoutCache SelectIDList(int instanceID, bool isWindow)
		{
			Dictionary<int, GUILayoutUtility.LayoutCache> dictionary = (!isWindow) ? GUILayoutUtility.s_StoredLayouts : GUILayoutUtility.s_StoredWindows;
			GUILayoutUtility.LayoutCache layoutCache;
			if (!dictionary.TryGetValue(instanceID, out layoutCache))
			{
				layoutCache = new GUILayoutUtility.LayoutCache();
				dictionary[instanceID] = layoutCache;
			}
			GUILayoutUtility.current.topLevel = layoutCache.topLevel;
			GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
			GUILayoutUtility.current.windows = layoutCache.windows;
			return layoutCache;
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x00021B0C File Offset: 0x0001FD0C
		internal static void Begin(int instanceID)
		{
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.SelectIDList(instanceID, false);
			if (Event.current.type == EventType.Layout)
			{
				GUILayoutUtility.current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (layoutCache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = layoutCache.topLevel;
				GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
				GUILayoutUtility.current.windows = layoutCache.windows;
			}
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x00021BC0 File Offset: 0x0001FDC0
		internal static void BeginWindow(int windowID, GUIStyle style, GUILayoutOption[] options)
		{
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.SelectIDList(windowID, true);
			if (Event.current.type == EventType.Layout)
			{
				GUILayoutUtility.current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.topLevel.style = style;
				GUILayoutUtility.current.topLevel.windowID = windowID;
				if (options != null)
				{
					GUILayoutUtility.current.topLevel.ApplyOptions(options);
				}
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (layoutCache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = layoutCache.topLevel;
				GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
				GUILayoutUtility.current.windows = layoutCache.windows;
			}
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x00021CA8 File Offset: 0x0001FEA8
		public static void BeginGroup(string GroupName)
		{
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x00021CAC File Offset: 0x0001FEAC
		public static void EndGroup(string groupName)
		{
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x00021CB0 File Offset: 0x0001FEB0
		internal static void Layout()
		{
			if (GUILayoutUtility.current.topLevel.windowID == -1)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, Mathf.Min((float)Screen.width / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxWidth));
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxHeight));
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				GUILayoutUtility.LayoutSingleGroup(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x00021D8C File Offset: 0x0001FF8C
		internal static void LayoutFromEditorWindow()
		{
			GUILayoutUtility.current.topLevel.CalcWidth();
			GUILayoutUtility.current.topLevel.SetHorizontal(0f, (float)Screen.width / GUIUtility.pixelsPerPoint);
			GUILayoutUtility.current.topLevel.CalcHeight();
			GUILayoutUtility.current.topLevel.SetVertical(0f, (float)Screen.height / GUIUtility.pixelsPerPoint);
			GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x00021E08 File Offset: 0x00020008
		internal static float LayoutFromInspector(float width)
		{
			if (GUILayoutUtility.current.topLevel != null && GUILayoutUtility.current.topLevel.windowID == -1)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, width);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxHeight));
				float minHeight = GUILayoutUtility.current.topLevel.minHeight;
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
				return minHeight;
			}
			if (GUILayoutUtility.current.topLevel != null)
			{
				GUILayoutUtility.LayoutSingleGroup(GUILayoutUtility.current.topLevel);
			}
			return 0f;
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x00021EE4 File Offset: 0x000200E4
		internal static void LayoutFreeGroup(GUILayoutGroup toplevel)
		{
			foreach (GUILayoutEntry guilayoutEntry in toplevel.entries)
			{
				GUILayoutGroup i = (GUILayoutGroup)guilayoutEntry;
				GUILayoutUtility.LayoutSingleGroup(i);
			}
			toplevel.ResetCursor();
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x00021F54 File Offset: 0x00020154
		private static void LayoutSingleGroup(GUILayoutGroup i)
		{
			if (!i.isWindow)
			{
				float minWidth = i.minWidth;
				float maxWidth = i.maxWidth;
				i.CalcWidth();
				i.SetHorizontal(i.rect.x, Mathf.Clamp(i.maxWidth, minWidth, maxWidth));
				float minHeight = i.minHeight;
				float maxHeight = i.maxHeight;
				i.CalcHeight();
				i.SetVertical(i.rect.y, Mathf.Clamp(i.maxHeight, minHeight, maxHeight));
			}
			else
			{
				i.CalcWidth();
				Rect rect = GUILayoutUtility.Internal_GetWindowRect(i.windowID);
				i.SetHorizontal(rect.x, Mathf.Clamp(rect.width, i.minWidth, i.maxWidth));
				i.CalcHeight();
				i.SetVertical(rect.y, Mathf.Clamp(rect.height, i.minHeight, i.maxHeight));
				GUILayoutUtility.Internal_MoveWindow(i.windowID, i.rect);
			}
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x0002204C File Offset: 0x0002024C
		[SecuritySafeCritical]
		private static GUILayoutGroup CreateGUILayoutGroupInstanceOfType(Type LayoutType)
		{
			if (!typeof(GUILayoutGroup).IsAssignableFrom(LayoutType))
			{
				throw new ArgumentException("LayoutType needs to be of type GUILayoutGroup");
			}
			return (GUILayoutGroup)Activator.CreateInstance(LayoutType);
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x0002207C File Offset: 0x0002027C
		internal static GUILayoutGroup BeginLayoutGroup(GUIStyle style, GUILayoutOption[] options, Type layoutType)
		{
			EventType type = Event.current.type;
			GUILayoutGroup guilayoutGroup;
			if (type != EventType.Layout && type != EventType.Used)
			{
				guilayoutGroup = (GUILayoutUtility.current.topLevel.GetNext() as GUILayoutGroup);
				if (guilayoutGroup == null)
				{
					throw new ArgumentException("GUILayout: Mismatched LayoutGroup." + Event.current.type);
				}
				guilayoutGroup.ResetCursor();
			}
			else
			{
				guilayoutGroup = GUILayoutUtility.CreateGUILayoutGroupInstanceOfType(layoutType);
				guilayoutGroup.style = style;
				if (options != null)
				{
					guilayoutGroup.ApplyOptions(options);
				}
				GUILayoutUtility.current.topLevel.Add(guilayoutGroup);
			}
			GUILayoutUtility.current.layoutGroups.Push(guilayoutGroup);
			GUILayoutUtility.current.topLevel = guilayoutGroup;
			return guilayoutGroup;
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0002213C File Offset: 0x0002033C
		internal static void EndLayoutGroup()
		{
			EventType type = Event.current.type;
			GUILayoutUtility.current.layoutGroups.Pop();
			GUILayoutUtility.current.topLevel = (GUILayoutGroup)GUILayoutUtility.current.layoutGroups.Peek();
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x00022184 File Offset: 0x00020384
		internal static GUILayoutGroup BeginLayoutArea(GUIStyle style, Type layoutType)
		{
			EventType type = Event.current.type;
			GUILayoutGroup guilayoutGroup;
			if (type != EventType.Layout && type != EventType.Used)
			{
				guilayoutGroup = (GUILayoutUtility.current.windows.GetNext() as GUILayoutGroup);
				if (guilayoutGroup == null)
				{
					throw new ArgumentException("GUILayout: Mismatched LayoutGroup." + Event.current.type);
				}
				guilayoutGroup.ResetCursor();
			}
			else
			{
				guilayoutGroup = GUILayoutUtility.CreateGUILayoutGroupInstanceOfType(layoutType);
				guilayoutGroup.style = style;
				GUILayoutUtility.current.windows.Add(guilayoutGroup);
			}
			GUILayoutUtility.current.layoutGroups.Push(guilayoutGroup);
			GUILayoutUtility.current.topLevel = guilayoutGroup;
			return guilayoutGroup;
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x00022234 File Offset: 0x00020434
		internal static GUILayoutGroup DoBeginLayoutArea(GUIStyle style, Type layoutType)
		{
			return GUILayoutUtility.BeginLayoutArea(style, layoutType);
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x00022240 File Offset: 0x00020440
		internal static GUILayoutGroup topLevel
		{
			get
			{
				return GUILayoutUtility.current.topLevel;
			}
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x0002224C File Offset: 0x0002044C
		public static Rect GetRect(GUIContent content, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(content, style, null);
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x00022258 File Offset: 0x00020458
		public static Rect GetRect(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(content, style, options);
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x00022264 File Offset: 0x00020464
		private static Rect DoGetRect(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			EventType type = Event.current.type;
			if (type == EventType.Layout)
			{
				if (style.isHeightDependantOnWidth)
				{
					GUILayoutUtility.current.topLevel.Add(new GUIWordWrapSizer(style, content, options));
				}
				else
				{
					Vector2 constraints = new Vector2(0f, 0f);
					if (options != null)
					{
						foreach (GUILayoutOption guilayoutOption in options)
						{
							switch (guilayoutOption.type)
							{
							case GUILayoutOption.Type.maxWidth:
								constraints.x = (float)guilayoutOption.value;
								break;
							case GUILayoutOption.Type.maxHeight:
								constraints.y = (float)guilayoutOption.value;
								break;
							}
						}
					}
					Vector2 vector = style.CalcSizeWithConstraints(content, constraints);
					GUILayoutUtility.current.topLevel.Add(new GUILayoutEntry(vector.x, vector.x, vector.y, vector.y, style, options));
				}
				return GUILayoutUtility.kDummyRect;
			}
			if (type != EventType.Used)
			{
				return GUILayoutUtility.current.topLevel.GetNext().rect;
			}
			return GUILayoutUtility.kDummyRect;
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x0002239C File Offset: 0x0002059C
		public static Rect GetRect(float width, float height)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, GUIStyle.none, null);
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x000223B0 File Offset: 0x000205B0
		public static Rect GetRect(float width, float height, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, style, null);
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x000223C0 File Offset: 0x000205C0
		public static Rect GetRect(float width, float height, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, GUIStyle.none, options);
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x000223D4 File Offset: 0x000205D4
		public static Rect GetRect(float width, float height, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, style, options);
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000223E4 File Offset: 0x000205E4
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, null);
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000223F8 File Offset: 0x000205F8
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, null);
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00022408 File Offset: 0x00020608
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, options);
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x0002241C File Offset: 0x0002061C
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, options);
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0002242C File Offset: 0x0002062C
		private static Rect DoGetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, GUILayoutOption[] options)
		{
			EventType type = Event.current.type;
			if (type == EventType.Layout)
			{
				GUILayoutUtility.current.topLevel.Add(new GUILayoutEntry(minWidth, maxWidth, minHeight, maxHeight, style, options));
				return GUILayoutUtility.kDummyRect;
			}
			if (type != EventType.Used)
			{
				return GUILayoutUtility.current.topLevel.GetNext().rect;
			}
			return GUILayoutUtility.kDummyRect;
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00022494 File Offset: 0x00020694
		public static Rect GetLastRect()
		{
			EventType type = Event.current.type;
			if (type == EventType.Layout)
			{
				return GUILayoutUtility.kDummyRect;
			}
			if (type != EventType.Used)
			{
				return GUILayoutUtility.current.topLevel.GetLast();
			}
			return GUILayoutUtility.kDummyRect;
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x000224DC File Offset: 0x000206DC
		public static Rect GetAspectRect(float aspect)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, GUIStyle.none, null);
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000224EC File Offset: 0x000206EC
		public static Rect GetAspectRect(float aspect, GUIStyle style)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, style, null);
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x000224F8 File Offset: 0x000206F8
		public static Rect GetAspectRect(float aspect, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, GUIStyle.none, options);
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x00022508 File Offset: 0x00020708
		public static Rect GetAspectRect(float aspect, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, GUIStyle.none, options);
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x00022518 File Offset: 0x00020718
		private static Rect DoGetAspectRect(float aspect, GUIStyle style, GUILayoutOption[] options)
		{
			EventType type = Event.current.type;
			if (type == EventType.Layout)
			{
				GUILayoutUtility.current.topLevel.Add(new GUIAspectSizer(aspect, options));
				return GUILayoutUtility.kDummyRect;
			}
			if (type != EventType.Used)
			{
				return GUILayoutUtility.current.topLevel.GetNext().rect;
			}
			return GUILayoutUtility.kDummyRect;
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001FAB RID: 8107 RVA: 0x0002257C File Offset: 0x0002077C
		internal static GUIStyle spaceStyle
		{
			get
			{
				if (GUILayoutUtility.s_SpaceStyle == null)
				{
					GUILayoutUtility.s_SpaceStyle = new GUIStyle();
				}
				GUILayoutUtility.s_SpaceStyle.stretchWidth = false;
				return GUILayoutUtility.s_SpaceStyle;
			}
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x000225B0 File Offset: 0x000207B0
		private static Rect Internal_GetWindowRect(int windowID)
		{
			Rect result;
			GUILayoutUtility.INTERNAL_CALL_Internal_GetWindowRect(windowID, out result);
			return result;
		}

		// Token: 0x06001FAD RID: 8109
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_GetWindowRect(int windowID, out Rect value);

		// Token: 0x06001FAE RID: 8110 RVA: 0x000225C8 File Offset: 0x000207C8
		private static void Internal_MoveWindow(int windowID, Rect r)
		{
			GUILayoutUtility.INTERNAL_CALL_Internal_MoveWindow(windowID, ref r);
		}

		// Token: 0x06001FAF RID: 8111
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_MoveWindow(int windowID, ref Rect r);

		// Token: 0x06001FB0 RID: 8112 RVA: 0x000225D4 File Offset: 0x000207D4
		internal static Rect GetWindowsBounds()
		{
			Rect result;
			GUILayoutUtility.INTERNAL_CALL_GetWindowsBounds(out result);
			return result;
		}

		// Token: 0x06001FB1 RID: 8113
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetWindowsBounds(out Rect value);

		// Token: 0x040007A2 RID: 1954
		private static readonly Dictionary<int, GUILayoutUtility.LayoutCache> s_StoredLayouts = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		// Token: 0x040007A3 RID: 1955
		private static readonly Dictionary<int, GUILayoutUtility.LayoutCache> s_StoredWindows = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		// Token: 0x040007A4 RID: 1956
		internal static GUILayoutUtility.LayoutCache current = new GUILayoutUtility.LayoutCache();

		// Token: 0x040007A5 RID: 1957
		private static readonly Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x040007A6 RID: 1958
		private static GUIStyle s_SpaceStyle;

		// Token: 0x020001FE RID: 510
		internal sealed class LayoutCache
		{
			// Token: 0x06001FB2 RID: 8114 RVA: 0x000225EC File Offset: 0x000207EC
			internal LayoutCache()
			{
				this.layoutGroups.Push(this.topLevel);
			}

			// Token: 0x06001FB3 RID: 8115 RVA: 0x00022634 File Offset: 0x00020834
			internal LayoutCache(GUILayoutUtility.LayoutCache other)
			{
				this.topLevel = other.topLevel;
				this.layoutGroups = other.layoutGroups;
				this.windows = other.windows;
			}

			// Token: 0x040007A7 RID: 1959
			internal GUILayoutGroup topLevel = new GUILayoutGroup();

			// Token: 0x040007A8 RID: 1960
			internal GenericStack layoutGroups = new GenericStack();

			// Token: 0x040007A9 RID: 1961
			internal GUILayoutGroup windows = new GUILayoutGroup();
		}
	}
}
