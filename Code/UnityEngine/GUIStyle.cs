using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200020D RID: 525
	[RequiredByNativeCode]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GUIStyle
	{
		// Token: 0x06002030 RID: 8240 RVA: 0x000257AC File Offset: 0x000239AC
		public GUIStyle()
		{
			this.Init();
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x000257BC File Offset: 0x000239BC
		public GUIStyle(GUIStyle other)
		{
			this.InitCopy(other);
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x000257D4 File Offset: 0x000239D4
		~GUIStyle()
		{
			this.Cleanup();
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x00025810 File Offset: 0x00023A10
		internal void InternalOnAfterDeserialize()
		{
			this.m_FontInternal = this.GetFontInternalDuringLoadingThread();
			this.m_Normal = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(0));
			this.m_Hover = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(1));
			this.m_Active = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(2));
			this.m_Focused = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(3));
			this.m_OnNormal = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(4));
			this.m_OnHover = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(5));
			this.m_OnActive = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(6));
			this.m_OnFocused = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(7));
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x000258C4 File Offset: 0x00023AC4
		// (set) Token: 0x06002036 RID: 8246 RVA: 0x000258F8 File Offset: 0x00023AF8
		public GUIStyleState normal
		{
			get
			{
				if (this.m_Normal == null)
				{
					this.m_Normal = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(0));
				}
				return this.m_Normal;
			}
			set
			{
				this.AssignStyleState(0, value.m_Ptr);
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x00025908 File Offset: 0x00023B08
		// (set) Token: 0x06002038 RID: 8248 RVA: 0x0002593C File Offset: 0x00023B3C
		public GUIStyleState hover
		{
			get
			{
				if (this.m_Hover == null)
				{
					this.m_Hover = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(1));
				}
				return this.m_Hover;
			}
			set
			{
				this.AssignStyleState(1, value.m_Ptr);
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x0002594C File Offset: 0x00023B4C
		// (set) Token: 0x0600203A RID: 8250 RVA: 0x00025980 File Offset: 0x00023B80
		public GUIStyleState active
		{
			get
			{
				if (this.m_Active == null)
				{
					this.m_Active = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(2));
				}
				return this.m_Active;
			}
			set
			{
				this.AssignStyleState(2, value.m_Ptr);
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x00025990 File Offset: 0x00023B90
		// (set) Token: 0x0600203C RID: 8252 RVA: 0x000259C4 File Offset: 0x00023BC4
		public GUIStyleState onNormal
		{
			get
			{
				if (this.m_OnNormal == null)
				{
					this.m_OnNormal = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(4));
				}
				return this.m_OnNormal;
			}
			set
			{
				this.AssignStyleState(4, value.m_Ptr);
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x000259D4 File Offset: 0x00023BD4
		// (set) Token: 0x0600203E RID: 8254 RVA: 0x00025A08 File Offset: 0x00023C08
		public GUIStyleState onHover
		{
			get
			{
				if (this.m_OnHover == null)
				{
					this.m_OnHover = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(5));
				}
				return this.m_OnHover;
			}
			set
			{
				this.AssignStyleState(5, value.m_Ptr);
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x00025A18 File Offset: 0x00023C18
		// (set) Token: 0x06002040 RID: 8256 RVA: 0x00025A4C File Offset: 0x00023C4C
		public GUIStyleState onActive
		{
			get
			{
				if (this.m_OnActive == null)
				{
					this.m_OnActive = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(6));
				}
				return this.m_OnActive;
			}
			set
			{
				this.AssignStyleState(6, value.m_Ptr);
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x00025A5C File Offset: 0x00023C5C
		// (set) Token: 0x06002042 RID: 8258 RVA: 0x00025A90 File Offset: 0x00023C90
		public GUIStyleState focused
		{
			get
			{
				if (this.m_Focused == null)
				{
					this.m_Focused = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(3));
				}
				return this.m_Focused;
			}
			set
			{
				this.AssignStyleState(3, value.m_Ptr);
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x00025AA0 File Offset: 0x00023CA0
		// (set) Token: 0x06002044 RID: 8260 RVA: 0x00025AD4 File Offset: 0x00023CD4
		public GUIStyleState onFocused
		{
			get
			{
				if (this.m_OnFocused == null)
				{
					this.m_OnFocused = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(7));
				}
				return this.m_OnFocused;
			}
			set
			{
				this.AssignStyleState(7, value.m_Ptr);
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x00025AE4 File Offset: 0x00023CE4
		// (set) Token: 0x06002046 RID: 8262 RVA: 0x00025B18 File Offset: 0x00023D18
		public RectOffset border
		{
			get
			{
				if (this.m_Border == null)
				{
					this.m_Border = new RectOffset(this, this.GetRectOffsetPtr(0));
				}
				return this.m_Border;
			}
			set
			{
				this.AssignRectOffset(0, value.m_Ptr);
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x00025B28 File Offset: 0x00023D28
		// (set) Token: 0x06002048 RID: 8264 RVA: 0x00025B5C File Offset: 0x00023D5C
		public RectOffset margin
		{
			get
			{
				if (this.m_Margin == null)
				{
					this.m_Margin = new RectOffset(this, this.GetRectOffsetPtr(1));
				}
				return this.m_Margin;
			}
			set
			{
				this.AssignRectOffset(1, value.m_Ptr);
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002049 RID: 8265 RVA: 0x00025B6C File Offset: 0x00023D6C
		// (set) Token: 0x0600204A RID: 8266 RVA: 0x00025BA0 File Offset: 0x00023DA0
		public RectOffset padding
		{
			get
			{
				if (this.m_Padding == null)
				{
					this.m_Padding = new RectOffset(this, this.GetRectOffsetPtr(2));
				}
				return this.m_Padding;
			}
			set
			{
				this.AssignRectOffset(2, value.m_Ptr);
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x00025BB0 File Offset: 0x00023DB0
		// (set) Token: 0x0600204C RID: 8268 RVA: 0x00025BE4 File Offset: 0x00023DE4
		public RectOffset overflow
		{
			get
			{
				if (this.m_Overflow == null)
				{
					this.m_Overflow = new RectOffset(this, this.GetRectOffsetPtr(3));
				}
				return this.m_Overflow;
			}
			set
			{
				this.AssignRectOffset(3, value.m_Ptr);
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x00025BF4 File Offset: 0x00023DF4
		// (set) Token: 0x0600204E RID: 8270 RVA: 0x00025BFC File Offset: 0x00023DFC
		[Obsolete("warning Don't use clipOffset - put things inside BeginGroup instead. This functionality will be removed in a later version.")]
		public Vector2 clipOffset
		{
			get
			{
				return this.Internal_clipOffset;
			}
			set
			{
				this.Internal_clipOffset = value;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x00025C08 File Offset: 0x00023E08
		// (set) Token: 0x06002050 RID: 8272 RVA: 0x00025C10 File Offset: 0x00023E10
		public Font font
		{
			get
			{
				return this.GetFontInternal();
			}
			set
			{
				this.SetFontInternal(value);
				this.m_FontInternal = value;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x00025C20 File Offset: 0x00023E20
		public float lineHeight
		{
			get
			{
				return Mathf.Round(GUIStyle.Internal_GetLineHeight(this.m_Ptr));
			}
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00025C34 File Offset: 0x00023E34
		private static void Internal_Draw(IntPtr target, Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			Internal_DrawArguments internal_DrawArguments = default(Internal_DrawArguments);
			internal_DrawArguments.target = target;
			internal_DrawArguments.position = position;
			internal_DrawArguments.isHover = ((!isHover) ? 0 : 1);
			internal_DrawArguments.isActive = ((!isActive) ? 0 : 1);
			internal_DrawArguments.on = ((!on) ? 0 : 1);
			internal_DrawArguments.hasKeyboardFocus = ((!hasKeyboardFocus) ? 0 : 1);
			GUIStyle.Internal_Draw(content, ref internal_DrawArguments);
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x00025CB4 File Offset: 0x00023EB4
		public void Draw(Rect position, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			GUIStyle.Internal_Draw(this.m_Ptr, position, GUIContent.none, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x00025CD0 File Offset: 0x00023ED0
		public void Draw(Rect position, string text, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			GUIStyle.Internal_Draw(this.m_Ptr, position, GUIContent.Temp(text), isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x00025CF8 File Offset: 0x00023EF8
		public void Draw(Rect position, Texture image, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			GUIStyle.Internal_Draw(this.m_Ptr, position, GUIContent.Temp(image), isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x00025D20 File Offset: 0x00023F20
		public void Draw(Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			GUIStyle.Internal_Draw(this.m_Ptr, position, content, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x00025D38 File Offset: 0x00023F38
		public void Draw(Rect position, GUIContent content, int controlID)
		{
			this.Draw(position, content, controlID, false);
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x00025D44 File Offset: 0x00023F44
		public void Draw(Rect position, GUIContent content, int controlID, bool on)
		{
			if (content != null)
			{
				GUIStyle.Internal_Draw2(this.m_Ptr, position, content, controlID, on);
			}
			else
			{
				Debug.LogError("Style.Draw may not be called with GUIContent that is null.");
			}
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x00025D6C File Offset: 0x00023F6C
		public void DrawCursor(Rect position, GUIContent content, int controlID, int Character)
		{
			Event current = Event.current;
			if (current.type == EventType.Repaint)
			{
				Color cursorColor = new Color(0f, 0f, 0f, 0f);
				float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
				float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
				if (cursorFlashSpeed == 0f || num < 0.5f)
				{
					cursorColor = GUI.skin.settings.cursorColor;
				}
				GUIStyle.Internal_DrawCursor(this.m_Ptr, position, content, Character, cursorColor);
			}
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x00025DFC File Offset: 0x00023FFC
		internal void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition)
		{
			Event current = Event.current;
			Color cursorColor = new Color(0f, 0f, 0f, 0f);
			float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
			float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
			if (cursorFlashSpeed == 0f || num < 0.5f)
			{
				cursorColor = GUI.skin.settings.cursorColor;
			}
			Internal_DrawWithTextSelectionArguments internal_DrawWithTextSelectionArguments = default(Internal_DrawWithTextSelectionArguments);
			internal_DrawWithTextSelectionArguments.target = this.m_Ptr;
			internal_DrawWithTextSelectionArguments.position = position;
			internal_DrawWithTextSelectionArguments.firstPos = firstSelectedCharacter;
			internal_DrawWithTextSelectionArguments.lastPos = lastSelectedCharacter;
			internal_DrawWithTextSelectionArguments.cursorColor = cursorColor;
			internal_DrawWithTextSelectionArguments.selectionColor = GUI.skin.settings.selectionColor;
			internal_DrawWithTextSelectionArguments.isHover = ((!position.Contains(current.mousePosition)) ? 0 : 1);
			internal_DrawWithTextSelectionArguments.isActive = ((controlID != GUIUtility.hotControl) ? 0 : 1);
			internal_DrawWithTextSelectionArguments.on = 0;
			internal_DrawWithTextSelectionArguments.hasKeyboardFocus = ((controlID != GUIUtility.keyboardControl || !GUIStyle.showKeyboardFocus) ? 0 : 1);
			internal_DrawWithTextSelectionArguments.drawSelectionAsComposition = ((!drawSelectionAsComposition) ? 0 : 1);
			GUIStyle.Internal_DrawWithTextSelection(content, ref internal_DrawWithTextSelectionArguments);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x00025F40 File Offset: 0x00024140
		public void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter)
		{
			this.DrawWithTextSelection(position, content, controlID, firstSelectedCharacter, lastSelectedCharacter, false);
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x00025F50 File Offset: 0x00024150
		public static GUIStyle none
		{
			get
			{
				if (GUIStyle.s_None == null)
				{
					GUIStyle.s_None = new GUIStyle();
				}
				return GUIStyle.s_None;
			}
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x00025F6C File Offset: 0x0002416C
		public Vector2 GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			Vector2 result;
			GUIStyle.Internal_GetCursorPixelPosition(this.m_Ptr, position, content, cursorStringIndex, out result);
			return result;
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x00025F8C File Offset: 0x0002418C
		public int GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return GUIStyle.Internal_GetCursorStringIndex(this.m_Ptr, position, content, cursorPixelPosition);
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x00025F9C File Offset: 0x0002419C
		internal int GetNumCharactersThatFitWithinWidth(string text, float width)
		{
			return GUIStyle.Internal_GetNumCharactersThatFitWithinWidth(this.m_Ptr, text, width);
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x00025FAC File Offset: 0x000241AC
		public Vector2 CalcSize(GUIContent content)
		{
			Vector2 result;
			GUIStyle.Internal_CalcSize(this.m_Ptr, content, out result);
			return result;
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00025FC8 File Offset: 0x000241C8
		internal Vector2 CalcSizeWithConstraints(GUIContent content, Vector2 constraints)
		{
			Vector2 result;
			GUIStyle.Internal_CalcSizeWithConstraints(this.m_Ptr, content, constraints, out result);
			return result;
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00025FE8 File Offset: 0x000241E8
		public Vector2 CalcScreenSize(Vector2 contentSize)
		{
			return new Vector2((this.fixedWidth == 0f) ? Mathf.Ceil(contentSize.x + (float)this.padding.left + (float)this.padding.right) : this.fixedWidth, (this.fixedHeight == 0f) ? Mathf.Ceil(contentSize.y + (float)this.padding.top + (float)this.padding.bottom) : this.fixedHeight);
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x0002607C File Offset: 0x0002427C
		public float CalcHeight(GUIContent content, float width)
		{
			return GUIStyle.Internal_CalcHeight(this.m_Ptr, content, width);
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x0002608C File Offset: 0x0002428C
		public bool isHeightDependantOnWidth
		{
			get
			{
				return this.fixedHeight == 0f && this.wordWrap && this.imagePosition != ImagePosition.ImageOnly;
			}
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x000260C8 File Offset: 0x000242C8
		public void CalcMinMaxWidth(GUIContent content, out float minWidth, out float maxWidth)
		{
			GUIStyle.Internal_CalcMinMaxWidth(this.m_Ptr, content, out minWidth, out maxWidth);
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000260D8 File Offset: 0x000242D8
		public override string ToString()
		{
			return UnityString.Format("GUIStyle '{0}'", new object[]
			{
				this.name
			});
		}

		// Token: 0x06002067 RID: 8295
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init();

		// Token: 0x06002068 RID: 8296
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InitCopy(GUIStyle other);

		// Token: 0x06002069 RID: 8297
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600206A RID: 8298
		// (set) Token: 0x0600206B RID: 8299
		public extern string name { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600206C RID: 8300
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetStyleStatePtr(int idx);

		// Token: 0x0600206D RID: 8301
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AssignStyleState(int idx, IntPtr srcStyleState);

		// Token: 0x0600206E RID: 8302
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetRectOffsetPtr(int idx);

		// Token: 0x0600206F RID: 8303
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AssignRectOffset(int idx, IntPtr srcRectOffset);

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002070 RID: 8304
		// (set) Token: 0x06002071 RID: 8305
		public extern ImagePosition imagePosition { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002072 RID: 8306
		// (set) Token: 0x06002073 RID: 8307
		public extern TextAnchor alignment { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002074 RID: 8308
		// (set) Token: 0x06002075 RID: 8309
		public extern bool wordWrap { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002076 RID: 8310
		// (set) Token: 0x06002077 RID: 8311
		public extern TextClipping clipping { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x000260F4 File Offset: 0x000242F4
		// (set) Token: 0x06002079 RID: 8313 RVA: 0x0002610C File Offset: 0x0002430C
		public Vector2 contentOffset
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_contentOffset(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_contentOffset(ref value);
			}
		}

		// Token: 0x0600207A RID: 8314
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_contentOffset(out Vector2 value);

		// Token: 0x0600207B RID: 8315
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_contentOffset(ref Vector2 value);

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x00026118 File Offset: 0x00024318
		// (set) Token: 0x0600207D RID: 8317 RVA: 0x00026130 File Offset: 0x00024330
		internal Vector2 Internal_clipOffset
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_Internal_clipOffset(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_Internal_clipOffset(ref value);
			}
		}

		// Token: 0x0600207E RID: 8318
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_Internal_clipOffset(out Vector2 value);

		// Token: 0x0600207F RID: 8319
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_Internal_clipOffset(ref Vector2 value);

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002080 RID: 8320
		// (set) Token: 0x06002081 RID: 8321
		public extern float fixedWidth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002082 RID: 8322
		// (set) Token: 0x06002083 RID: 8323
		public extern float fixedHeight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002084 RID: 8324
		// (set) Token: 0x06002085 RID: 8325
		public extern bool stretchWidth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002086 RID: 8326
		// (set) Token: 0x06002087 RID: 8327
		public extern bool stretchHeight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06002088 RID: 8328
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetLineHeight(IntPtr target);

		// Token: 0x06002089 RID: 8329
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFontInternal(Font value);

		// Token: 0x0600208A RID: 8330
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Font GetFontInternalDuringLoadingThread();

		// Token: 0x0600208B RID: 8331
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Font GetFontInternal();

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x0600208C RID: 8332
		// (set) Token: 0x0600208D RID: 8333
		public extern int fontSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x0600208E RID: 8334
		// (set) Token: 0x0600208F RID: 8335
		public extern FontStyle fontStyle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002090 RID: 8336
		// (set) Token: 0x06002091 RID: 8337
		public extern bool richText { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06002092 RID: 8338
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Draw(GUIContent content, ref Internal_DrawArguments arguments);

		// Token: 0x06002093 RID: 8339 RVA: 0x0002613C File Offset: 0x0002433C
		private static void Internal_Draw2(IntPtr style, Rect position, GUIContent content, int controlID, bool on)
		{
			GUIStyle.INTERNAL_CALL_Internal_Draw2(style, ref position, content, controlID, on);
		}

		// Token: 0x06002094 RID: 8340
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_Draw2(IntPtr style, ref Rect position, GUIContent content, int controlID, bool on);

		// Token: 0x06002095 RID: 8341 RVA: 0x0002614C File Offset: 0x0002434C
		private static void Internal_DrawPrefixLabel(IntPtr style, Rect position, GUIContent content, int controlID, bool on)
		{
			GUIStyle.INTERNAL_CALL_Internal_DrawPrefixLabel(style, ref position, content, controlID, on);
		}

		// Token: 0x06002096 RID: 8342
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_DrawPrefixLabel(IntPtr style, ref Rect position, GUIContent content, int controlID, bool on);

		// Token: 0x06002097 RID: 8343
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetCursorFlashOffset();

		// Token: 0x06002098 RID: 8344 RVA: 0x0002615C File Offset: 0x0002435C
		private static void Internal_DrawCursor(IntPtr target, Rect position, GUIContent content, int pos, Color cursorColor)
		{
			GUIStyle.INTERNAL_CALL_Internal_DrawCursor(target, ref position, content, pos, ref cursorColor);
		}

		// Token: 0x06002099 RID: 8345
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_DrawCursor(IntPtr target, ref Rect position, GUIContent content, int pos, ref Color cursorColor);

		// Token: 0x0600209A RID: 8346
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawWithTextSelection(GUIContent content, ref Internal_DrawWithTextSelectionArguments arguments);

		// Token: 0x0600209B RID: 8347
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetDefaultFont(Font font);

		// Token: 0x0600209C RID: 8348 RVA: 0x0002616C File Offset: 0x0002436C
		internal static void Internal_GetCursorPixelPosition(IntPtr target, Rect position, GUIContent content, int cursorStringIndex, out Vector2 ret)
		{
			GUIStyle.INTERNAL_CALL_Internal_GetCursorPixelPosition(target, ref position, content, cursorStringIndex, out ret);
		}

		// Token: 0x0600209D RID: 8349
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_GetCursorPixelPosition(IntPtr target, ref Rect position, GUIContent content, int cursorStringIndex, out Vector2 ret);

		// Token: 0x0600209E RID: 8350 RVA: 0x0002617C File Offset: 0x0002437C
		internal static int Internal_GetCursorStringIndex(IntPtr target, Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return GUIStyle.INTERNAL_CALL_Internal_GetCursorStringIndex(target, ref position, content, ref cursorPixelPosition);
		}

		// Token: 0x0600209F RID: 8351
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_Internal_GetCursorStringIndex(IntPtr target, ref Rect position, GUIContent content, ref Vector2 cursorPixelPosition);

		// Token: 0x060020A0 RID: 8352
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Internal_GetNumCharactersThatFitWithinWidth(IntPtr target, string text, float width);

		// Token: 0x060020A1 RID: 8353
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_CalcSize(IntPtr target, GUIContent content, out Vector2 ret);

		// Token: 0x060020A2 RID: 8354 RVA: 0x0002618C File Offset: 0x0002438C
		internal static void Internal_CalcSizeWithConstraints(IntPtr target, GUIContent content, Vector2 maxSize, out Vector2 ret)
		{
			GUIStyle.INTERNAL_CALL_Internal_CalcSizeWithConstraints(target, content, ref maxSize, out ret);
		}

		// Token: 0x060020A3 RID: 8355
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_CalcSizeWithConstraints(IntPtr target, GUIContent content, ref Vector2 maxSize, out Vector2 ret);

		// Token: 0x060020A4 RID: 8356
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_CalcHeight(IntPtr target, GUIContent content, float width);

		// Token: 0x060020A5 RID: 8357
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CalcMinMaxWidth(IntPtr target, GUIContent content, out float minWidth, out float maxWidth);

		// Token: 0x060020A6 RID: 8358 RVA: 0x00026198 File Offset: 0x00024398
		public static implicit operator GUIStyle(string str)
		{
			if (GUISkin.current == null)
			{
				Debug.LogError("Unable to use a named GUIStyle without a current skin. Most likely you need to move your GUIStyle initialization code to OnGUI");
				return GUISkin.error;
			}
			return GUISkin.current.GetStyle(str);
		}

		// Token: 0x0400081D RID: 2077
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x0400081E RID: 2078
		[NonSerialized]
		private GUIStyleState m_Normal;

		// Token: 0x0400081F RID: 2079
		[NonSerialized]
		private GUIStyleState m_Hover;

		// Token: 0x04000820 RID: 2080
		[NonSerialized]
		private GUIStyleState m_Active;

		// Token: 0x04000821 RID: 2081
		[NonSerialized]
		private GUIStyleState m_Focused;

		// Token: 0x04000822 RID: 2082
		[NonSerialized]
		private GUIStyleState m_OnNormal;

		// Token: 0x04000823 RID: 2083
		[NonSerialized]
		private GUIStyleState m_OnHover;

		// Token: 0x04000824 RID: 2084
		[NonSerialized]
		private GUIStyleState m_OnActive;

		// Token: 0x04000825 RID: 2085
		[NonSerialized]
		private GUIStyleState m_OnFocused;

		// Token: 0x04000826 RID: 2086
		[NonSerialized]
		private RectOffset m_Border;

		// Token: 0x04000827 RID: 2087
		[NonSerialized]
		private RectOffset m_Padding;

		// Token: 0x04000828 RID: 2088
		[NonSerialized]
		private RectOffset m_Margin;

		// Token: 0x04000829 RID: 2089
		[NonSerialized]
		private RectOffset m_Overflow;

		// Token: 0x0400082A RID: 2090
		[NonSerialized]
		private Font m_FontInternal;

		// Token: 0x0400082B RID: 2091
		internal static bool showKeyboardFocus = true;

		// Token: 0x0400082C RID: 2092
		private static GUIStyle s_None;
	}
}
