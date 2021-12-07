using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020001F0 RID: 496
	public class GUI
	{
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x0001D3B0 File Offset: 0x0001B5B0
		// (set) Token: 0x06001E03 RID: 7683 RVA: 0x0001D3B8 File Offset: 0x0001B5B8
		internal static DateTime nextScrollStepTime { get; set; } = DateTime.Now;

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x0001D3C0 File Offset: 0x0001B5C0
		// (set) Token: 0x06001E05 RID: 7685 RVA: 0x0001D3C8 File Offset: 0x0001B5C8
		internal static int scrollTroughSide { get; set; }

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x0001D3E0 File Offset: 0x0001B5E0
		// (set) Token: 0x06001E06 RID: 7686 RVA: 0x0001D3D0 File Offset: 0x0001B5D0
		public static GUISkin skin
		{
			get
			{
				GUIUtility.CheckOnGUI();
				return GUI.s_Skin;
			}
			set
			{
				GUIUtility.CheckOnGUI();
				GUI.DoSetSkin(value);
			}
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x0001D3EC File Offset: 0x0001B5EC
		internal static void DoSetSkin(GUISkin newSkin)
		{
			if (!newSkin)
			{
				newSkin = GUIUtility.GetDefaultSkin();
			}
			GUI.s_Skin = newSkin;
			newSkin.MakeCurrent();
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x0001D40C File Offset: 0x0001B60C
		// (set) Token: 0x06001E0A RID: 7690 RVA: 0x0001D414 File Offset: 0x0001B614
		public static Matrix4x4 matrix
		{
			get
			{
				return GUIClip.GetMatrix();
			}
			set
			{
				GUIClip.SetMatrix(value);
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x0001D41C File Offset: 0x0001B61C
		// (set) Token: 0x06001E0C RID: 7692 RVA: 0x0001D43C File Offset: 0x0001B63C
		public static string tooltip
		{
			get
			{
				string text = GUI.Internal_GetTooltip();
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				GUI.Internal_SetTooltip(value);
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x0001D444 File Offset: 0x0001B644
		protected static string mouseTooltip
		{
			get
			{
				return GUI.Internal_GetMouseTooltip();
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x0001D44C File Offset: 0x0001B64C
		// (set) Token: 0x06001E0F RID: 7695 RVA: 0x0001D454 File Offset: 0x0001B654
		protected static Rect tooltipRect
		{
			get
			{
				return GUI.s_ToolTipRect;
			}
			set
			{
				GUI.s_ToolTipRect = value;
			}
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x0001D45C File Offset: 0x0001B65C
		public static void Label(Rect position, string text)
		{
			GUI.Label(position, GUIContent.Temp(text), GUI.s_Skin.label);
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x0001D474 File Offset: 0x0001B674
		public static void Label(Rect position, Texture image)
		{
			GUI.Label(position, GUIContent.Temp(image), GUI.s_Skin.label);
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x0001D48C File Offset: 0x0001B68C
		public static void Label(Rect position, GUIContent content)
		{
			GUI.Label(position, content, GUI.s_Skin.label);
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x0001D4A0 File Offset: 0x0001B6A0
		public static void Label(Rect position, string text, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(text), style);
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x0001D4B0 File Offset: 0x0001B6B0
		public static void Label(Rect position, Texture image, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(image), style);
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x0001D4C0 File Offset: 0x0001B6C0
		public static void Label(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			GUI.DoLabel(position, content, style.m_Ptr);
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x0001D4D4 File Offset: 0x0001B6D4
		public static void DrawTexture(Rect position, Texture image)
		{
			GUI.DrawTexture(position, image, ScaleMode.StretchToFill);
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x0001D4E0 File Offset: 0x0001B6E0
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode)
		{
			GUI.DrawTexture(position, image, scaleMode, true);
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x0001D4EC File Offset: 0x0001B6EC
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, 0f);
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x0001D4FC File Offset: 0x0001B6FC
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect)
		{
			GUIUtility.CheckOnGUI();
			if (Event.current.type == EventType.Repaint)
			{
				if (image == null)
				{
					Debug.LogWarning("null texture passed to GUI.DrawTexture");
					return;
				}
				if (imageAspect == 0f)
				{
					imageAspect = (float)image.width / (float)image.height;
				}
				Material mat = (!alphaBlend) ? GUI.blitMaterial : GUI.blendMaterial;
				float num = position.width / position.height;
				InternalDrawTextureArguments internalDrawTextureArguments = default(InternalDrawTextureArguments);
				internalDrawTextureArguments.texture = image;
				internalDrawTextureArguments.leftBorder = 0;
				internalDrawTextureArguments.rightBorder = 0;
				internalDrawTextureArguments.topBorder = 0;
				internalDrawTextureArguments.bottomBorder = 0;
				internalDrawTextureArguments.color = GUI.color;
				internalDrawTextureArguments.mat = mat;
				switch (scaleMode)
				{
				case ScaleMode.StretchToFill:
					internalDrawTextureArguments.screenRect = position;
					internalDrawTextureArguments.sourceRect = new Rect(0f, 0f, 1f, 1f);
					Graphics.DrawTexture(ref internalDrawTextureArguments);
					break;
				case ScaleMode.ScaleAndCrop:
					if (num > imageAspect)
					{
						float num2 = imageAspect / num;
						internalDrawTextureArguments.screenRect = position;
						internalDrawTextureArguments.sourceRect = new Rect(0f, (1f - num2) * 0.5f, 1f, num2);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					else
					{
						float num3 = num / imageAspect;
						internalDrawTextureArguments.screenRect = position;
						internalDrawTextureArguments.sourceRect = new Rect(0.5f - num3 * 0.5f, 0f, num3, 1f);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					break;
				case ScaleMode.ScaleToFit:
					if (num > imageAspect)
					{
						float num4 = imageAspect / num;
						internalDrawTextureArguments.screenRect = new Rect(position.xMin + position.width * (1f - num4) * 0.5f, position.yMin, num4 * position.width, position.height);
						internalDrawTextureArguments.sourceRect = new Rect(0f, 0f, 1f, 1f);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					else
					{
						float num5 = num / imageAspect;
						internalDrawTextureArguments.screenRect = new Rect(position.xMin, position.yMin + position.height * (1f - num5) * 0.5f, position.width, num5 * position.height);
						internalDrawTextureArguments.sourceRect = new Rect(0f, 0f, 1f, 1f);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					break;
				}
			}
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x0001D788 File Offset: 0x0001B988
		internal static bool CalculateScaledTextureRects(Rect position, ScaleMode scaleMode, float imageAspect, ref Rect outScreenRect, ref Rect outSourceRect)
		{
			float num = position.width / position.height;
			bool result = false;
			switch (scaleMode)
			{
			case ScaleMode.StretchToFill:
				outScreenRect = position;
				outSourceRect = new Rect(0f, 0f, 1f, 1f);
				result = true;
				break;
			case ScaleMode.ScaleAndCrop:
				if (num > imageAspect)
				{
					float num2 = imageAspect / num;
					outScreenRect = position;
					outSourceRect = new Rect(0f, (1f - num2) * 0.5f, 1f, num2);
					result = true;
				}
				else
				{
					float num3 = num / imageAspect;
					outScreenRect = position;
					outSourceRect = new Rect(0.5f - num3 * 0.5f, 0f, num3, 1f);
					result = true;
				}
				break;
			case ScaleMode.ScaleToFit:
				if (num > imageAspect)
				{
					float num4 = imageAspect / num;
					outScreenRect = new Rect(position.xMin + position.width * (1f - num4) * 0.5f, position.yMin, num4 * position.width, position.height);
					outSourceRect = new Rect(0f, 0f, 1f, 1f);
					result = true;
				}
				else
				{
					float num5 = num / imageAspect;
					outScreenRect = new Rect(position.xMin, position.yMin + position.height * (1f - num5) * 0.5f, position.width, num5 * position.height);
					outSourceRect = new Rect(0f, 0f, 1f, 1f);
					result = true;
				}
				break;
			}
			return result;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0001D924 File Offset: 0x0001BB24
		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords)
		{
			GUI.DrawTextureWithTexCoords(position, image, texCoords, true);
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x0001D930 File Offset: 0x0001BB30
		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords, bool alphaBlend)
		{
			GUIUtility.CheckOnGUI();
			if (Event.current.type == EventType.Repaint)
			{
				Material mat = (!alphaBlend) ? GUI.blitMaterial : GUI.blendMaterial;
				InternalDrawTextureArguments internalDrawTextureArguments = default(InternalDrawTextureArguments);
				internalDrawTextureArguments.texture = image;
				internalDrawTextureArguments.leftBorder = 0;
				internalDrawTextureArguments.rightBorder = 0;
				internalDrawTextureArguments.topBorder = 0;
				internalDrawTextureArguments.bottomBorder = 0;
				internalDrawTextureArguments.color = GUI.color;
				internalDrawTextureArguments.mat = mat;
				internalDrawTextureArguments.screenRect = position;
				internalDrawTextureArguments.sourceRect = texCoords;
				Graphics.DrawTexture(ref internalDrawTextureArguments);
			}
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x0001D9C8 File Offset: 0x0001BBC8
		public static void Box(Rect position, string text)
		{
			GUI.Box(position, GUIContent.Temp(text), GUI.s_Skin.box);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x0001D9E0 File Offset: 0x0001BBE0
		public static void Box(Rect position, Texture image)
		{
			GUI.Box(position, GUIContent.Temp(image), GUI.s_Skin.box);
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x0001D9F8 File Offset: 0x0001BBF8
		public static void Box(Rect position, GUIContent content)
		{
			GUI.Box(position, content, GUI.s_Skin.box);
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0001DA0C File Offset: 0x0001BC0C
		public static void Box(Rect position, string text, GUIStyle style)
		{
			GUI.Box(position, GUIContent.Temp(text), style);
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x0001DA1C File Offset: 0x0001BC1C
		public static void Box(Rect position, Texture image, GUIStyle style)
		{
			GUI.Box(position, GUIContent.Temp(image), style);
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x0001DA2C File Offset: 0x0001BC2C
		public static void Box(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_BoxHash, FocusType.Passive);
			if (Event.current.type == EventType.Repaint)
			{
				style.Draw(position, content, controlID);
			}
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x0001DA64 File Offset: 0x0001BC64
		public static bool Button(Rect position, string text)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoButton(position, GUIContent.Temp(text), GUI.s_Skin.button.m_Ptr);
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0001DA94 File Offset: 0x0001BC94
		public static bool Button(Rect position, Texture image)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoButton(position, GUIContent.Temp(image), GUI.s_Skin.button.m_Ptr);
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0001DAC4 File Offset: 0x0001BCC4
		public static bool Button(Rect position, GUIContent content)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoButton(position, content, GUI.s_Skin.button.m_Ptr);
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x0001DAE4 File Offset: 0x0001BCE4
		public static bool Button(Rect position, string text, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoButton(position, GUIContent.Temp(text), style.m_Ptr);
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0001DB00 File Offset: 0x0001BD00
		public static bool Button(Rect position, Texture image, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoButton(position, GUIContent.Temp(image), style.m_Ptr);
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0001DB1C File Offset: 0x0001BD1C
		public static bool Button(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoButton(position, content, style.m_Ptr);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0001DB30 File Offset: 0x0001BD30
		public static bool RepeatButton(Rect position, string text)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(text), GUI.s_Skin.button, FocusType.Native);
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0001DB4C File Offset: 0x0001BD4C
		public static bool RepeatButton(Rect position, Texture image)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(image), GUI.s_Skin.button, FocusType.Native);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x0001DB68 File Offset: 0x0001BD68
		public static bool RepeatButton(Rect position, GUIContent content)
		{
			return GUI.DoRepeatButton(position, content, GUI.s_Skin.button, FocusType.Native);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0001DB7C File Offset: 0x0001BD7C
		public static bool RepeatButton(Rect position, string text, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(text), style, FocusType.Native);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0001DB8C File Offset: 0x0001BD8C
		public static bool RepeatButton(Rect position, Texture image, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(image), style, FocusType.Native);
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0001DB9C File Offset: 0x0001BD9C
		public static bool RepeatButton(Rect position, GUIContent content, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, content, style, FocusType.Native);
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x0001DBA8 File Offset: 0x0001BDA8
		private static bool DoRepeatButton(Rect position, GUIContent content, GUIStyle style, FocusType focusType)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_RepeatButtonHash, focusType, position);
			EventType typeForControl = Event.current.GetTypeForControl(controlID);
			if (typeForControl == EventType.MouseDown)
			{
				if (position.Contains(Event.current.mousePosition))
				{
					GUIUtility.hotControl = controlID;
					Event.current.Use();
				}
				return false;
			}
			if (typeForControl != EventType.MouseUp)
			{
				if (typeForControl != EventType.Repaint)
				{
					return false;
				}
				style.Draw(position, content, controlID);
				return controlID == GUIUtility.hotControl && position.Contains(Event.current.mousePosition);
			}
			else
			{
				if (GUIUtility.hotControl == controlID)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
					return position.Contains(Event.current.mousePosition);
				}
				return false;
			}
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0001DC70 File Offset: 0x0001BE70
		public static string TextField(Rect position, string text)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, -1, GUI.skin.textField);
			return guicontent.text;
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x0001DCA4 File Offset: 0x0001BEA4
		public static string TextField(Rect position, string text, int maxLength)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, GUI.skin.textField);
			return guicontent.text;
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0001DCD8 File Offset: 0x0001BED8
		public static string TextField(Rect position, string text, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, -1, style);
			return guicontent.text;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0001DD04 File Offset: 0x0001BF04
		public static string TextField(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style);
			return guicontent.text;
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0001DD30 File Offset: 0x0001BF30
		public static string PasswordField(Rect position, string password, char maskChar)
		{
			return GUI.PasswordField(position, password, maskChar, -1, GUI.skin.textField);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x0001DD50 File Offset: 0x0001BF50
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength)
		{
			return GUI.PasswordField(position, password, maskChar, maxLength, GUI.skin.textField);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0001DD70 File Offset: 0x0001BF70
		public static string PasswordField(Rect position, string password, char maskChar, GUIStyle style)
		{
			return GUI.PasswordField(position, password, maskChar, -1, style);
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0001DD7C File Offset: 0x0001BF7C
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			string text = GUI.PasswordFieldGetStrToShow(password, maskChar);
			GUIContent guicontent = GUIContent.Temp(text);
			bool changed = GUI.changed;
			GUI.changed = false;
			if (TouchScreenKeyboard.isSupported)
			{
				GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard), guicontent, false, maxLength, style, password, maskChar);
			}
			else
			{
				GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style);
			}
			text = ((!GUI.changed) ? password : guicontent.text);
			GUI.changed = (GUI.changed || changed);
			return text;
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x0001DE04 File Offset: 0x0001C004
		internal static string PasswordFieldGetStrToShow(string password, char maskChar)
		{
			return (Event.current.type != EventType.Repaint && Event.current.type != EventType.MouseDown) ? password : string.Empty.PadRight(password.Length, maskChar);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x0001DE48 File Offset: 0x0001C048
		public static string TextArea(Rect position, string text)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, -1, GUI.skin.textArea);
			return guicontent.text;
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x0001DE7C File Offset: 0x0001C07C
		public static string TextArea(Rect position, string text, int maxLength)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, maxLength, GUI.skin.textArea);
			return guicontent.text;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
		public static string TextArea(Rect position, string text, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, -1, style);
			return guicontent.text;
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0001DEDC File Offset: 0x0001C0DC
		public static string TextArea(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style);
			return guicontent.text;
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0001DF08 File Offset: 0x0001C108
		private static string TextArea(Rect position, GUIContent content, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(content.text, content.image);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style);
			return guicontent.text;
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x0001DF40 File Offset: 0x0001C140
		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style)
		{
			GUI.DoTextField(position, id, content, multiline, maxLength, style, null);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0001DF50 File Offset: 0x0001C150
		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText)
		{
			GUI.DoTextField(position, id, content, multiline, maxLength, style, secureText, '\0');
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0001DF70 File Offset: 0x0001C170
		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText, char maskChar)
		{
			if (maxLength >= 0 && content.text.Length > maxLength)
			{
				content.text = content.text.Substring(0, maxLength);
			}
			GUIUtility.CheckOnGUI();
			TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), id);
			textEditor.text = content.text;
			textEditor.SaveBackup();
			textEditor.position = position;
			textEditor.style = style;
			textEditor.multiline = multiline;
			textEditor.controlID = id;
			textEditor.DetectFocusChange();
			if (TouchScreenKeyboard.isSupported)
			{
				GUI.HandleTextFieldEventForTouchscreen(position, id, content, multiline, maxLength, style, secureText, maskChar, textEditor);
			}
			else
			{
				GUI.HandleTextFieldEventForDesktop(position, id, content, multiline, maxLength, style, textEditor);
			}
			textEditor.UpdateScrollOffsetIfNeeded();
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x0001E030 File Offset: 0x0001C230
		private static void HandleTextFieldEventForTouchscreen(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText, char maskChar, TextEditor editor)
		{
			Event current = Event.current;
			EventType type = current.type;
			if (type != EventType.MouseDown)
			{
				if (type == EventType.Repaint)
				{
					if (editor.keyboardOnScreen != null)
					{
						content.text = editor.keyboardOnScreen.text;
						if (maxLength >= 0 && content.text.Length > maxLength)
						{
							content.text = content.text.Substring(0, maxLength);
						}
						if (editor.keyboardOnScreen.done)
						{
							editor.keyboardOnScreen = null;
							GUI.changed = true;
						}
					}
					string text = content.text;
					if (secureText != null)
					{
						content.text = GUI.PasswordFieldGetStrToShow(text, maskChar);
					}
					style.Draw(position, content, id, false);
					content.text = text;
				}
			}
			else if (position.Contains(current.mousePosition))
			{
				GUIUtility.hotControl = id;
				if (GUI.s_HotTextField != -1 && GUI.s_HotTextField != id)
				{
					TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUI.s_HotTextField);
					textEditor.keyboardOnScreen = null;
				}
				GUI.s_HotTextField = id;
				if (GUIUtility.keyboardControl != id)
				{
					GUIUtility.keyboardControl = id;
				}
				editor.keyboardOnScreen = TouchScreenKeyboard.Open((secureText == null) ? content.text : secureText, TouchScreenKeyboardType.Default, true, multiline, secureText != null);
				current.Use();
			}
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0001E198 File Offset: 0x0001C398
		private static void HandleTextFieldEventForDesktop(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, TextEditor editor)
		{
			Event current = Event.current;
			bool flag = false;
			switch (current.type)
			{
			case EventType.MouseDown:
				if (position.Contains(current.mousePosition))
				{
					GUIUtility.hotControl = id;
					GUIUtility.keyboardControl = id;
					editor.m_HasFocus = true;
					editor.MoveCursorToPosition(Event.current.mousePosition);
					if (Event.current.clickCount == 2 && GUI.skin.settings.doubleClickSelectsWord)
					{
						editor.SelectCurrentWord();
						editor.DblClickSnap(TextEditor.DblClickSnapping.WORDS);
						editor.MouseDragSelectsWholeWords(true);
					}
					if (Event.current.clickCount == 3 && GUI.skin.settings.tripleClickSelectsLine)
					{
						editor.SelectCurrentParagraph();
						editor.MouseDragSelectsWholeWords(true);
						editor.DblClickSnap(TextEditor.DblClickSnapping.PARAGRAPHS);
					}
					current.Use();
				}
				break;
			case EventType.MouseUp:
				if (GUIUtility.hotControl == id)
				{
					editor.MouseDragSelectsWholeWords(false);
					GUIUtility.hotControl = 0;
					current.Use();
				}
				break;
			case EventType.MouseDrag:
				if (GUIUtility.hotControl == id)
				{
					if (current.shift)
					{
						editor.MoveCursorToPosition(Event.current.mousePosition);
					}
					else
					{
						editor.SelectToPosition(Event.current.mousePosition);
					}
					current.Use();
				}
				break;
			case EventType.KeyDown:
				if (GUIUtility.keyboardControl != id)
				{
					return;
				}
				if (editor.HandleKeyEvent(current))
				{
					current.Use();
					flag = true;
					content.text = editor.text;
				}
				else
				{
					if (current.keyCode == KeyCode.Tab || current.character == '\t')
					{
						return;
					}
					char character = current.character;
					if (character == '\n' && !multiline && !current.alt)
					{
						return;
					}
					Font font = style.font;
					if (!font)
					{
						font = GUI.skin.font;
					}
					if (font.HasCharacter(character) || character == '\n')
					{
						editor.Insert(character);
						flag = true;
					}
					else if (character == '\0')
					{
						if (Input.compositionString.Length > 0)
						{
							editor.ReplaceSelection(string.Empty);
							flag = true;
						}
						current.Use();
					}
				}
				break;
			case EventType.Repaint:
				if (GUIUtility.keyboardControl != id)
				{
					style.Draw(position, content, id, false);
				}
				else
				{
					editor.DrawCursor(content.text);
				}
				break;
			}
			if (GUIUtility.keyboardControl == id)
			{
				GUIUtility.textFieldInput = true;
			}
			if (flag)
			{
				GUI.changed = true;
				content.text = editor.text;
				if (maxLength >= 0 && content.text.Length > maxLength)
				{
					content.text = content.text.Substring(0, maxLength);
				}
				current.Use();
			}
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x0001E470 File Offset: 0x0001C670
		public static bool Toggle(Rect position, bool value, string text)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(text), GUI.s_Skin.toggle);
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0001E48C File Offset: 0x0001C68C
		public static bool Toggle(Rect position, bool value, Texture image)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(image), GUI.s_Skin.toggle);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0001E4A8 File Offset: 0x0001C6A8
		public static bool Toggle(Rect position, bool value, GUIContent content)
		{
			return GUI.Toggle(position, value, content, GUI.s_Skin.toggle);
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x0001E4BC File Offset: 0x0001C6BC
		public static bool Toggle(Rect position, bool value, string text, GUIStyle style)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(text), style);
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0001E4CC File Offset: 0x0001C6CC
		public static bool Toggle(Rect position, bool value, Texture image, GUIStyle style)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(image), style);
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x0001E4DC File Offset: 0x0001C6DC
		public static bool Toggle(Rect position, bool value, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoToggle(position, GUIUtility.GetControlID(GUI.s_ToggleHash, FocusType.Native, position), value, content, style.m_Ptr);
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x0001E500 File Offset: 0x0001C700
		public static bool Toggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoToggle(position, id, value, content, style.m_Ptr);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0001E518 File Offset: 0x0001C718
		public static int Toolbar(Rect position, int selected, string[] texts)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(texts), GUI.s_Skin.button);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x0001E534 File Offset: 0x0001C734
		public static int Toolbar(Rect position, int selected, Texture[] images)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(images), GUI.s_Skin.button);
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0001E550 File Offset: 0x0001C750
		public static int Toolbar(Rect position, int selected, GUIContent[] content)
		{
			return GUI.Toolbar(position, selected, content, GUI.s_Skin.button);
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0001E564 File Offset: 0x0001C764
		public static int Toolbar(Rect position, int selected, string[] texts, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(texts), style);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0001E574 File Offset: 0x0001C774
		public static int Toolbar(Rect position, int selected, Texture[] images, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(images), style);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0001E584 File Offset: 0x0001C784
		public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			GUIStyle firstStyle;
			GUIStyle midStyle;
			GUIStyle lastStyle;
			GUI.FindStyles(ref style, out firstStyle, out midStyle, out lastStyle, "left", "mid", "right");
			return GUI.DoButtonGrid(position, selected, contents, contents.Length, style, firstStyle, midStyle, lastStyle);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0001E5C4 File Offset: 0x0001C7C4
		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, null);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0001E5D8 File Offset: 0x0001C7D8
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(images), xCount, null);
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0001E5EC File Offset: 0x0001C7EC
		public static int SelectionGrid(Rect position, int selected, GUIContent[] content, int xCount)
		{
			return GUI.SelectionGrid(position, selected, content, xCount, null);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x0001E5F8 File Offset: 0x0001C7F8
		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount, GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, style);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0001E60C File Offset: 0x0001C80C
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount, GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(images), xCount, style);
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0001E620 File Offset: 0x0001C820
		public static int SelectionGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style)
		{
			if (style == null)
			{
				style = GUI.s_Skin.button;
			}
			return GUI.DoButtonGrid(position, selected, contents, xCount, style, style, style, style);
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0001E654 File Offset: 0x0001C854
		internal static void FindStyles(ref GUIStyle style, out GUIStyle firstStyle, out GUIStyle midStyle, out GUIStyle lastStyle, string first, string mid, string last)
		{
			if (style == null)
			{
				style = GUI.skin.button;
			}
			string name = style.name;
			midStyle = GUI.skin.FindStyle(name + mid);
			if (midStyle == null)
			{
				midStyle = style;
			}
			firstStyle = GUI.skin.FindStyle(name + first);
			if (firstStyle == null)
			{
				firstStyle = midStyle;
			}
			lastStyle = GUI.skin.FindStyle(name + last);
			if (lastStyle == null)
			{
				lastStyle = midStyle;
			}
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x0001E6DC File Offset: 0x0001C8DC
		internal static int CalcTotalHorizSpacing(int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
		{
			if (xCount < 2)
			{
				return 0;
			}
			if (xCount == 2)
			{
				return Mathf.Max(firstStyle.margin.right, lastStyle.margin.left);
			}
			int num = Mathf.Max(midStyle.margin.left, midStyle.margin.right);
			return Mathf.Max(firstStyle.margin.right, midStyle.margin.left) + Mathf.Max(midStyle.margin.right, lastStyle.margin.left) + num * (xCount - 3);
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x0001E770 File Offset: 0x0001C970
		private static int DoButtonGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
		{
			GUIUtility.CheckOnGUI();
			int num = contents.Length;
			if (num == 0)
			{
				return selected;
			}
			if (xCount <= 0)
			{
				Debug.LogWarning("You are trying to create a SelectionGrid with zero or less elements to be displayed in the horizontal direction. Set xCount to a positive value.");
				return selected;
			}
			int controlID = GUIUtility.GetControlID(GUI.s_ButtonGridHash, FocusType.Native, position);
			int num2 = num / xCount;
			if (num % xCount != 0)
			{
				num2++;
			}
			float num3 = (float)GUI.CalcTotalHorizSpacing(xCount, style, firstStyle, midStyle, lastStyle);
			float num4 = (float)(Mathf.Max(style.margin.top, style.margin.bottom) * (num2 - 1));
			float elemWidth = (position.width - num3) / (float)xCount;
			float elemHeight = (position.height - num4) / (float)num2;
			if (style.fixedWidth != 0f)
			{
				elemWidth = style.fixedWidth;
			}
			if (style.fixedHeight != 0f)
			{
				elemHeight = style.fixedHeight;
			}
			switch (Event.current.GetTypeForControl(controlID))
			{
			case EventType.MouseDown:
				if (position.Contains(Event.current.mousePosition))
				{
					Rect[] array = GUI.CalcMouseRects(position, num, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false);
					if (GUI.GetButtonGridMouseSelection(array, Event.current.mousePosition, true) != -1)
					{
						GUIUtility.hotControl = controlID;
						Event.current.Use();
					}
				}
				break;
			case EventType.MouseUp:
				if (GUIUtility.hotControl == controlID)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
					Rect[] array = GUI.CalcMouseRects(position, num, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false);
					int buttonGridMouseSelection = GUI.GetButtonGridMouseSelection(array, Event.current.mousePosition, true);
					GUI.changed = true;
					return buttonGridMouseSelection;
				}
				break;
			case EventType.MouseDrag:
				if (GUIUtility.hotControl == controlID)
				{
					Event.current.Use();
				}
				break;
			case EventType.Repaint:
			{
				GUIStyle guistyle = null;
				GUIClip.Push(position, Vector2.zero, Vector2.zero, false);
				position = new Rect(0f, 0f, position.width, position.height);
				Rect[] array = GUI.CalcMouseRects(position, num, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false);
				int buttonGridMouseSelection2 = GUI.GetButtonGridMouseSelection(array, Event.current.mousePosition, controlID == GUIUtility.hotControl);
				bool flag = position.Contains(Event.current.mousePosition);
				GUIUtility.mouseUsed = (GUIUtility.mouseUsed || flag);
				for (int i = 0; i < num; i++)
				{
					GUIStyle guistyle2;
					if (i != 0)
					{
						guistyle2 = midStyle;
					}
					else
					{
						guistyle2 = firstStyle;
					}
					if (i == num - 1)
					{
						guistyle2 = lastStyle;
					}
					if (num == 1)
					{
						guistyle2 = style;
					}
					if (i != selected)
					{
						guistyle2.Draw(array[i], contents[i], i == buttonGridMouseSelection2 && (GUI.enabled || controlID == GUIUtility.hotControl) && (controlID == GUIUtility.hotControl || GUIUtility.hotControl == 0), controlID == GUIUtility.hotControl && GUI.enabled, false, false);
					}
					else
					{
						guistyle = guistyle2;
					}
				}
				if (selected < num && selected > -1)
				{
					guistyle.Draw(array[selected], contents[selected], selected == buttonGridMouseSelection2 && (GUI.enabled || controlID == GUIUtility.hotControl) && (controlID == GUIUtility.hotControl || GUIUtility.hotControl == 0), controlID == GUIUtility.hotControl, true, false);
				}
				if (buttonGridMouseSelection2 >= 0)
				{
					GUI.tooltip = contents[buttonGridMouseSelection2].tooltip;
				}
				GUIClip.Pop();
				break;
			}
			}
			return selected;
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x0001EB0C File Offset: 0x0001CD0C
		private static Rect[] CalcMouseRects(Rect position, int count, int xCount, float elemWidth, float elemHeight, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, bool addBorders)
		{
			int num = 0;
			int num2 = 0;
			float num3 = position.xMin;
			float num4 = position.yMin;
			GUIStyle guistyle = style;
			Rect[] array = new Rect[count];
			if (count > 1)
			{
				guistyle = firstStyle;
			}
			for (int i = 0; i < count; i++)
			{
				if (!addBorders)
				{
					array[i] = new Rect(num3, num4, elemWidth, elemHeight);
				}
				else
				{
					array[i] = guistyle.margin.Add(new Rect(num3, num4, elemWidth, elemHeight));
				}
				array[i].width = Mathf.Round(array[i].xMax) - Mathf.Round(array[i].x);
				array[i].x = Mathf.Round(array[i].x);
				GUIStyle guistyle2 = midStyle;
				if (i == count - 2)
				{
					guistyle2 = lastStyle;
				}
				num3 += elemWidth + (float)Mathf.Max(guistyle.margin.right, guistyle2.margin.left);
				num2++;
				if (num2 >= xCount)
				{
					num++;
					num2 = 0;
					num4 += elemHeight + (float)Mathf.Max(style.margin.top, style.margin.bottom);
					num3 = position.xMin;
				}
			}
			return array;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0001EC6C File Offset: 0x0001CE6C
		private static int GetButtonGridMouseSelection(Rect[] buttonRects, Vector2 mousePos, bool findNearest)
		{
			for (int i = 0; i < buttonRects.Length; i++)
			{
				if (buttonRects[i].Contains(mousePos))
				{
					return i;
				}
			}
			if (!findNearest)
			{
				return -1;
			}
			float num = 10000000f;
			int result = -1;
			for (int j = 0; j < buttonRects.Length; j++)
			{
				Rect rect = buttonRects[j];
				Vector2 b = new Vector2(Mathf.Clamp(mousePos.x, rect.xMin, rect.xMax), Mathf.Clamp(mousePos.y, rect.yMin, rect.yMax));
				float sqrMagnitude = (mousePos - b).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					result = j;
					num = sqrMagnitude;
				}
			}
			return result;
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x0001ED30 File Offset: 0x0001CF30
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, true, GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Native, position));
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x0001ED6C File Offset: 0x0001CF6C
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, slider, thumb, true, GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Native, position));
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x0001ED98 File Offset: 0x0001CF98
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, GUI.skin.verticalSlider, GUI.skin.verticalSliderThumb, false, GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Native, position));
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x0001EDD4 File Offset: 0x0001CFD4
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, slider, thumb, false, GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Native, position));
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0001EE00 File Offset: 0x0001D000
		public static float Slider(Rect position, float value, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id)
		{
			GUIUtility.CheckOnGUI();
			SliderHandler sliderHandler = new SliderHandler(position, value, size, start, end, slider, thumb, horiz, id);
			return sliderHandler.Handle();
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0001EE30 File Offset: 0x0001D030
		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue)
		{
			return GUI.Scroller(position, value, size, leftValue, rightValue, GUI.skin.horizontalScrollbar, GUI.skin.horizontalScrollbarThumb, GUI.skin.horizontalScrollbarLeftButton, GUI.skin.horizontalScrollbarRightButton, true);
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0001EE74 File Offset: 0x0001D074
		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle style)
		{
			return GUI.Scroller(position, value, size, leftValue, rightValue, style, GUI.skin.GetStyle(style.name + "thumb"), GUI.skin.GetStyle(style.name + "leftbutton"), GUI.skin.GetStyle(style.name + "rightbutton"), true);
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x0001EEE0 File Offset: 0x0001D0E0
		internal static bool ScrollerRepeatButton(int scrollerID, Rect rect, GUIStyle style)
		{
			bool result = false;
			if (GUI.DoRepeatButton(rect, GUIContent.none, style, FocusType.Passive))
			{
				bool flag = GUI.s_ScrollControlId != scrollerID;
				GUI.s_ScrollControlId = scrollerID;
				if (flag)
				{
					result = true;
					GUI.nextScrollStepTime = DateTime.Now.AddMilliseconds(250.0);
				}
				else if (DateTime.Now >= GUI.nextScrollStepTime)
				{
					result = true;
					GUI.nextScrollStepTime = DateTime.Now.AddMilliseconds(30.0);
				}
				if (Event.current.type == EventType.Repaint)
				{
					GUI.InternalRepaintEditorWindow();
				}
			}
			return result;
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x0001EF84 File Offset: 0x0001D184
		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue)
		{
			return GUI.Scroller(position, value, size, topValue, bottomValue, GUI.skin.verticalScrollbar, GUI.skin.verticalScrollbarThumb, GUI.skin.verticalScrollbarUpButton, GUI.skin.verticalScrollbarDownButton, false);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x0001EFC8 File Offset: 0x0001D1C8
		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue, GUIStyle style)
		{
			return GUI.Scroller(position, value, size, topValue, bottomValue, style, GUI.skin.GetStyle(style.name + "thumb"), GUI.skin.GetStyle(style.name + "upbutton"), GUI.skin.GetStyle(style.name + "downbutton"), false);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0001F034 File Offset: 0x0001D234
		private static float Scroller(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUIStyle leftButton, GUIStyle rightButton, bool horiz)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive, position);
			Rect position2;
			Rect rect;
			Rect rect2;
			if (horiz)
			{
				position2 = new Rect(position.x + leftButton.fixedWidth, position.y, position.width - leftButton.fixedWidth - rightButton.fixedWidth, position.height);
				rect = new Rect(position.x, position.y, leftButton.fixedWidth, position.height);
				rect2 = new Rect(position.xMax - rightButton.fixedWidth, position.y, rightButton.fixedWidth, position.height);
			}
			else
			{
				position2 = new Rect(position.x, position.y + leftButton.fixedHeight, position.width, position.height - leftButton.fixedHeight - rightButton.fixedHeight);
				rect = new Rect(position.x, position.y, position.width, leftButton.fixedHeight);
				rect2 = new Rect(position.x, position.yMax - rightButton.fixedHeight, position.width, rightButton.fixedHeight);
			}
			value = GUI.Slider(position2, value, size, leftValue, rightValue, slider, thumb, horiz, controlID);
			bool flag = false;
			if (Event.current.type == EventType.MouseUp)
			{
				flag = true;
			}
			if (GUI.ScrollerRepeatButton(controlID, rect, leftButton))
			{
				value -= GUI.s_ScrollStepSize * ((leftValue >= rightValue) ? -1f : 1f);
			}
			if (GUI.ScrollerRepeatButton(controlID, rect2, rightButton))
			{
				value += GUI.s_ScrollStepSize * ((leftValue >= rightValue) ? -1f : 1f);
			}
			if (flag && Event.current.type == EventType.Used)
			{
				GUI.s_ScrollControlId = 0;
			}
			if (leftValue < rightValue)
			{
				value = Mathf.Clamp(value, leftValue, rightValue - size);
			}
			else
			{
				value = Mathf.Clamp(value, rightValue, leftValue - size);
			}
			return value;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0001F240 File Offset: 0x0001D440
		public static void BeginClip(Rect position, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Push(position, scrollOffset, renderOffset, resetOffset);
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0001F250 File Offset: 0x0001D450
		public static void BeginGroup(Rect position)
		{
			GUI.BeginGroup(position, GUIContent.none, GUIStyle.none);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x0001F264 File Offset: 0x0001D464
		public static void BeginGroup(Rect position, string text)
		{
			GUI.BeginGroup(position, GUIContent.Temp(text), GUIStyle.none);
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0001F278 File Offset: 0x0001D478
		public static void BeginGroup(Rect position, Texture image)
		{
			GUI.BeginGroup(position, GUIContent.Temp(image), GUIStyle.none);
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0001F28C File Offset: 0x0001D48C
		public static void BeginGroup(Rect position, GUIContent content)
		{
			GUI.BeginGroup(position, content, GUIStyle.none);
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0001F29C File Offset: 0x0001D49C
		public static void BeginGroup(Rect position, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.none, style);
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0001F2AC File Offset: 0x0001D4AC
		public static void BeginGroup(Rect position, string text, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.Temp(text), style);
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x0001F2BC File Offset: 0x0001D4BC
		public static void BeginGroup(Rect position, Texture image, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.Temp(image), style);
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0001F2CC File Offset: 0x0001D4CC
		public static void BeginGroup(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_BeginGroupHash, FocusType.Passive);
			if (content != GUIContent.none || style != GUIStyle.none)
			{
				EventType type = Event.current.type;
				if (type != EventType.Repaint)
				{
					if (position.Contains(Event.current.mousePosition))
					{
						GUIUtility.mouseUsed = true;
					}
				}
				else
				{
					style.Draw(position, content, controlID);
				}
			}
			GUIClip.Push(position, Vector2.zero, Vector2.zero, false);
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0001F358 File Offset: 0x0001D558
		public static void EndGroup()
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Pop();
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x0001F364 File Offset: 0x0001D564
		public static void BeginClip(Rect position)
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Push(position, Vector2.zero, Vector2.zero, false);
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0001F37C File Offset: 0x0001D57C
		public static void EndClip()
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Pop();
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x0001F388 File Offset: 0x0001D588
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0001F3C0 File Offset: 0x0001D5C0
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x0001F3F8 File Offset: 0x0001D5F8
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, false, false, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x0001F41C File Offset: 0x0001D61C
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0001F444 File Offset: 0x0001D644
		protected static Vector2 DoBeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0001F464 File Offset: 0x0001D664
		internal static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_ScrollviewHash, FocusType.Passive);
			GUI.ScrollViewState scrollViewState = (GUI.ScrollViewState)GUIUtility.GetStateObject(typeof(GUI.ScrollViewState), controlID);
			if (scrollViewState.apply)
			{
				scrollPosition = scrollViewState.scrollPosition;
				scrollViewState.apply = false;
			}
			scrollViewState.position = position;
			scrollViewState.scrollPosition = scrollPosition;
			scrollViewState.visibleRect = (scrollViewState.viewRect = viewRect);
			scrollViewState.visibleRect.width = position.width;
			scrollViewState.visibleRect.height = position.height;
			GUI.s_ScrollViewStates.Push(scrollViewState);
			Rect screenRect = new Rect(position);
			EventType type = Event.current.type;
			if (type != EventType.Layout)
			{
				if (type != EventType.Used)
				{
					bool flag = alwaysShowVertical;
					bool flag2 = alwaysShowHorizontal;
					if (flag2 || viewRect.width > screenRect.width)
					{
						scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						screenRect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						flag2 = true;
					}
					if (flag || viewRect.height > screenRect.height)
					{
						scrollViewState.visibleRect.width = position.width - verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
						screenRect.width -= verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
						flag = true;
						if (!flag2 && viewRect.width > screenRect.width)
						{
							scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
							screenRect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
							flag2 = true;
						}
					}
					if (Event.current.type == EventType.Repaint && background != GUIStyle.none)
					{
						background.Draw(position, position.Contains(Event.current.mousePosition), false, flag2 && flag, false);
					}
					if (flag2 && horizontalScrollbar != GUIStyle.none)
					{
						scrollPosition.x = GUI.HorizontalScrollbar(new Rect(position.x, position.yMax - horizontalScrollbar.fixedHeight, screenRect.width, horizontalScrollbar.fixedHeight), scrollPosition.x, Mathf.Min(screenRect.width, viewRect.width), 0f, viewRect.width, horizontalScrollbar);
					}
					else
					{
						GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						if (horizontalScrollbar != GUIStyle.none)
						{
							scrollPosition.x = 0f;
						}
						else
						{
							scrollPosition.x = Mathf.Clamp(scrollPosition.x, 0f, Mathf.Max(viewRect.width - position.width, 0f));
						}
					}
					if (flag && verticalScrollbar != GUIStyle.none)
					{
						scrollPosition.y = GUI.VerticalScrollbar(new Rect(screenRect.xMax + (float)verticalScrollbar.margin.left, screenRect.y, verticalScrollbar.fixedWidth, screenRect.height), scrollPosition.y, Mathf.Min(screenRect.height, viewRect.height), 0f, viewRect.height, verticalScrollbar);
					}
					else
					{
						GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						if (verticalScrollbar != GUIStyle.none)
						{
							scrollPosition.y = 0f;
						}
						else
						{
							scrollPosition.y = Mathf.Clamp(scrollPosition.y, 0f, Mathf.Max(viewRect.height - position.height, 0f));
						}
					}
				}
			}
			else
			{
				GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
			}
			GUIClip.Push(screenRect, new Vector2(Mathf.Round(-scrollPosition.x - viewRect.x), Mathf.Round(-scrollPosition.y - viewRect.y)), Vector2.zero, false);
			return scrollPosition;
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0001F910 File Offset: 0x0001DB10
		public static void EndScrollView()
		{
			GUI.EndScrollView(true);
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x0001F918 File Offset: 0x0001DB18
		public static void EndScrollView(bool handleScrollWheel)
		{
			GUIUtility.CheckOnGUI();
			GUI.ScrollViewState scrollViewState = (GUI.ScrollViewState)GUI.s_ScrollViewStates.Peek();
			GUIClip.Pop();
			GUI.s_ScrollViewStates.Pop();
			if (handleScrollWheel && Event.current.type == EventType.ScrollWheel && scrollViewState.position.Contains(Event.current.mousePosition))
			{
				scrollViewState.scrollPosition.x = Mathf.Clamp(scrollViewState.scrollPosition.x + Event.current.delta.x * 20f, 0f, scrollViewState.viewRect.width - scrollViewState.visibleRect.width);
				scrollViewState.scrollPosition.y = Mathf.Clamp(scrollViewState.scrollPosition.y + Event.current.delta.y * 20f, 0f, scrollViewState.viewRect.height - scrollViewState.visibleRect.height);
				scrollViewState.apply = true;
				Event.current.Use();
			}
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0001FA2C File Offset: 0x0001DC2C
		internal static GUI.ScrollViewState GetTopScrollView()
		{
			if (GUI.s_ScrollViewStates.Count != 0)
			{
				return (GUI.ScrollViewState)GUI.s_ScrollViewStates.Peek();
			}
			return null;
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x0001FA5C File Offset: 0x0001DC5C
		public static void ScrollTo(Rect position)
		{
			GUI.ScrollViewState topScrollView = GUI.GetTopScrollView();
			if (topScrollView != null)
			{
				topScrollView.ScrollTo(position);
			}
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x0001FA7C File Offset: 0x0001DC7C
		public static bool ScrollTowards(Rect position, float maxDelta)
		{
			GUI.ScrollViewState topScrollView = GUI.GetTopScrollView();
			return topScrollView != null && topScrollView.ScrollTowards(position, maxDelta);
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x0001FAA0 File Offset: 0x0001DCA0
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(text), GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x0001FAD0 File Offset: 0x0001DCD0
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, Texture image)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(image), GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0001FB00 File Offset: 0x0001DD00
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, content, GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x0001FB2C File Offset: 0x0001DD2C
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, string text, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(text), style, GUI.skin, true);
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x0001FB54 File Offset: 0x0001DD54
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, Texture image, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(image), style, GUI.skin, true);
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x0001FB7C File Offset: 0x0001DD7C
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, title, style, GUI.skin, true);
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x0001FBA0 File Offset: 0x0001DDA0
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(text), GUI.skin.window, GUI.skin);
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x0001FBD0 File Offset: 0x0001DDD0
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, Texture image)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(image), GUI.skin.window, GUI.skin);
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x0001FC00 File Offset: 0x0001DE00
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, content, GUI.skin.window, GUI.skin);
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x0001FC2C File Offset: 0x0001DE2C
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, string text, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(text), style, GUI.skin);
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x0001FC54 File Offset: 0x0001DE54
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, Texture image, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(image), style, GUI.skin);
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x0001FC7C File Offset: 0x0001DE7C
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, content, style, GUI.skin);
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x0001FCA0 File Offset: 0x0001DEA0
		[RequiredByNativeCode]
		internal static void CallWindowDelegate(GUI.WindowFunction func, int id, GUISkin _skin, int forceRect, float width, float height, GUIStyle style)
		{
			GUILayoutUtility.SelectIDList(id, true);
			GUISkin skin = GUI.skin;
			if (Event.current.type == EventType.Layout)
			{
				if (forceRect != 0)
				{
					GUILayoutOption[] options = new GUILayoutOption[]
					{
						GUILayout.Width(width),
						GUILayout.Height(height)
					};
					GUILayoutUtility.BeginWindow(id, style, options);
				}
				else
				{
					GUILayoutUtility.BeginWindow(id, style, null);
				}
			}
			GUI.skin = _skin;
			func(id);
			if (Event.current.type == EventType.Layout)
			{
				GUILayoutUtility.Layout();
			}
			GUI.skin = skin;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x0001FD2C File Offset: 0x0001DF2C
		public static void DragWindow()
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x0001FD4C File Offset: 0x0001DF4C
		internal static void BeginWindows(int skinMode, int editorWindowInstanceID)
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
			GUILayoutGroup windows = GUILayoutUtility.current.windows;
			Matrix4x4 matrix = GUI.matrix;
			GUI.Internal_BeginWindows();
			GUI.matrix = matrix;
			GUILayoutUtility.current.topLevel = topLevel;
			GUILayoutUtility.current.layoutGroups = layoutGroups;
			GUILayoutUtility.current.windows = windows;
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x0001FDAC File Offset: 0x0001DFAC
		internal static void EndWindows()
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
			GUILayoutGroup windows = GUILayoutUtility.current.windows;
			GUI.Internal_EndWindows();
			GUILayoutUtility.current.topLevel = topLevel;
			GUILayoutUtility.current.layoutGroups = layoutGroups;
			GUILayoutUtility.current.windows = windows;
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x0001FE00 File Offset: 0x0001E000
		// (set) Token: 0x06001E8E RID: 7822 RVA: 0x0001FE18 File Offset: 0x0001E018
		public static Color color
		{
			get
			{
				Color result;
				GUI.INTERNAL_get_color(out result);
				return result;
			}
			set
			{
				GUI.INTERNAL_set_color(ref value);
			}
		}

		// Token: 0x06001E8F RID: 7823
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_color(out Color value);

		// Token: 0x06001E90 RID: 7824
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_color(ref Color value);

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x0001FE24 File Offset: 0x0001E024
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x0001FE3C File Offset: 0x0001E03C
		public static Color backgroundColor
		{
			get
			{
				Color result;
				GUI.INTERNAL_get_backgroundColor(out result);
				return result;
			}
			set
			{
				GUI.INTERNAL_set_backgroundColor(ref value);
			}
		}

		// Token: 0x06001E93 RID: 7827
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_backgroundColor(out Color value);

		// Token: 0x06001E94 RID: 7828
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_backgroundColor(ref Color value);

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x0001FE48 File Offset: 0x0001E048
		// (set) Token: 0x06001E96 RID: 7830 RVA: 0x0001FE60 File Offset: 0x0001E060
		public static Color contentColor
		{
			get
			{
				Color result;
				GUI.INTERNAL_get_contentColor(out result);
				return result;
			}
			set
			{
				GUI.INTERNAL_set_contentColor(ref value);
			}
		}

		// Token: 0x06001E97 RID: 7831
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_contentColor(out Color value);

		// Token: 0x06001E98 RID: 7832
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_contentColor(ref Color value);

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001E99 RID: 7833
		// (set) Token: 0x06001E9A RID: 7834
		public static extern bool changed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001E9B RID: 7835
		// (set) Token: 0x06001E9C RID: 7836
		public static extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001E9D RID: 7837
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetTooltip();

		// Token: 0x06001E9E RID: 7838
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetTooltip(string value);

		// Token: 0x06001E9F RID: 7839
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetMouseTooltip();

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001EA0 RID: 7840
		// (set) Token: 0x06001EA1 RID: 7841
		public static extern int depth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001EA2 RID: 7842 RVA: 0x0001FE6C File Offset: 0x0001E06C
		private static void DoLabel(Rect position, GUIContent content, IntPtr style)
		{
			GUI.INTERNAL_CALL_DoLabel(ref position, content, style);
		}

		// Token: 0x06001EA3 RID: 7843
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DoLabel(ref Rect position, GUIContent content, IntPtr style);

		// Token: 0x06001EA4 RID: 7844
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeGUIClipTexture();

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001EA5 RID: 7845
		private static extern Material blendMaterial { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001EA6 RID: 7846
		private static extern Material blitMaterial { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001EA7 RID: 7847 RVA: 0x0001FE78 File Offset: 0x0001E078
		private static bool DoButton(Rect position, GUIContent content, IntPtr style)
		{
			return GUI.INTERNAL_CALL_DoButton(ref position, content, style);
		}

		// Token: 0x06001EA8 RID: 7848
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_DoButton(ref Rect position, GUIContent content, IntPtr style);

		// Token: 0x06001EA9 RID: 7849
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetNextControlName(string name);

		// Token: 0x06001EAA RID: 7850
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetNameOfFocusedControl();

		// Token: 0x06001EAB RID: 7851
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FocusControl(string name);

		// Token: 0x06001EAC RID: 7852 RVA: 0x0001FE84 File Offset: 0x0001E084
		internal static bool DoToggle(Rect position, int id, bool value, GUIContent content, IntPtr style)
		{
			return GUI.INTERNAL_CALL_DoToggle(ref position, id, value, content, style);
		}

		// Token: 0x06001EAD RID: 7853
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_DoToggle(ref Rect position, int id, bool value, GUIContent content, IntPtr style);

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001EAE RID: 7854
		internal static extern bool usePageScrollbars { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001EAF RID: 7855
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalRepaintEditorWindow();

		// Token: 0x06001EB0 RID: 7856 RVA: 0x0001FE94 File Offset: 0x0001E094
		private static Rect DoModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUISkin skin)
		{
			Rect result;
			GUI.INTERNAL_CALL_DoModalWindow(id, ref clientRect, func, content, style, skin, out result);
			return result;
		}

		// Token: 0x06001EB1 RID: 7857
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DoModalWindow(int id, ref Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUISkin skin, out Rect value);

		// Token: 0x06001EB2 RID: 7858 RVA: 0x0001FEB4 File Offset: 0x0001E0B4
		private static Rect DoWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, GUISkin skin, bool forceRectOnLayout)
		{
			Rect result;
			GUI.INTERNAL_CALL_DoWindow(id, ref clientRect, func, title, style, skin, forceRectOnLayout, out result);
			return result;
		}

		// Token: 0x06001EB3 RID: 7859
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DoWindow(int id, ref Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, GUISkin skin, bool forceRectOnLayout, out Rect value);

		// Token: 0x06001EB4 RID: 7860 RVA: 0x0001FED4 File Offset: 0x0001E0D4
		public static void DragWindow(Rect position)
		{
			GUI.INTERNAL_CALL_DragWindow(ref position);
		}

		// Token: 0x06001EB5 RID: 7861
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DragWindow(ref Rect position);

		// Token: 0x06001EB6 RID: 7862
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BringWindowToFront(int windowID);

		// Token: 0x06001EB7 RID: 7863
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BringWindowToBack(int windowID);

		// Token: 0x06001EB8 RID: 7864
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FocusWindow(int windowID);

		// Token: 0x06001EB9 RID: 7865
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnfocusWindow();

		// Token: 0x06001EBA RID: 7866
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BeginWindows();

		// Token: 0x06001EBB RID: 7867
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_EndWindows();

		// Token: 0x0400077D RID: 1917
		private static float s_ScrollStepSize = 10f;

		// Token: 0x0400077E RID: 1918
		private static int s_ScrollControlId;

		// Token: 0x0400077F RID: 1919
		private static int s_HotTextField = -1;

		// Token: 0x04000780 RID: 1920
		private static readonly int s_BoxHash = "Box".GetHashCode();

		// Token: 0x04000781 RID: 1921
		private static readonly int s_RepeatButtonHash = "repeatButton".GetHashCode();

		// Token: 0x04000782 RID: 1922
		private static readonly int s_ToggleHash = "Toggle".GetHashCode();

		// Token: 0x04000783 RID: 1923
		private static readonly int s_ButtonGridHash = "ButtonGrid".GetHashCode();

		// Token: 0x04000784 RID: 1924
		private static readonly int s_SliderHash = "Slider".GetHashCode();

		// Token: 0x04000785 RID: 1925
		private static readonly int s_BeginGroupHash = "BeginGroup".GetHashCode();

		// Token: 0x04000786 RID: 1926
		private static readonly int s_ScrollviewHash = "scrollView".GetHashCode();

		// Token: 0x04000787 RID: 1927
		private static GUISkin s_Skin;

		// Token: 0x04000788 RID: 1928
		internal static Rect s_ToolTipRect;

		// Token: 0x04000789 RID: 1929
		private static GenericStack s_ScrollViewStates = new GenericStack();

		// Token: 0x020001F1 RID: 497
		internal sealed class ScrollViewState
		{
			// Token: 0x06001EBC RID: 7868 RVA: 0x0001FEE0 File Offset: 0x0001E0E0
			[RequiredByNativeCode]
			public ScrollViewState()
			{
			}

			// Token: 0x06001EBD RID: 7869 RVA: 0x0001FEE8 File Offset: 0x0001E0E8
			internal void ScrollTo(Rect position)
			{
				this.ScrollTowards(position, float.PositiveInfinity);
			}

			// Token: 0x06001EBE RID: 7870 RVA: 0x0001FEF8 File Offset: 0x0001E0F8
			internal bool ScrollTowards(Rect position, float maxDelta)
			{
				Vector2 b = this.ScrollNeeded(position);
				if (b.sqrMagnitude < 0.0001f)
				{
					return false;
				}
				if (maxDelta == 0f)
				{
					return true;
				}
				if (b.magnitude > maxDelta)
				{
					b = b.normalized * maxDelta;
				}
				this.scrollPosition += b;
				this.apply = true;
				return true;
			}

			// Token: 0x06001EBF RID: 7871 RVA: 0x0001FF64 File Offset: 0x0001E164
			internal Vector2 ScrollNeeded(Rect position)
			{
				Rect rect = this.visibleRect;
				rect.x += this.scrollPosition.x;
				rect.y += this.scrollPosition.y;
				float num = position.width - this.visibleRect.width;
				if (num > 0f)
				{
					position.width -= num;
					position.x += num * 0.5f;
				}
				num = position.height - this.visibleRect.height;
				if (num > 0f)
				{
					position.height -= num;
					position.y += num * 0.5f;
				}
				Vector2 zero = Vector2.zero;
				if (position.xMax > rect.xMax)
				{
					zero.x += position.xMax - rect.xMax;
				}
				else if (position.xMin < rect.xMin)
				{
					zero.x -= rect.xMin - position.xMin;
				}
				if (position.yMax > rect.yMax)
				{
					zero.y += position.yMax - rect.yMax;
				}
				else if (position.yMin < rect.yMin)
				{
					zero.y -= rect.yMin - position.yMin;
				}
				Rect rect2 = this.viewRect;
				rect2.width = Mathf.Max(rect2.width, this.visibleRect.width);
				rect2.height = Mathf.Max(rect2.height, this.visibleRect.height);
				zero.x = Mathf.Clamp(zero.x, rect2.xMin - this.scrollPosition.x, rect2.xMax - this.visibleRect.width - this.scrollPosition.x);
				zero.y = Mathf.Clamp(zero.y, rect2.yMin - this.scrollPosition.y, rect2.yMax - this.visibleRect.height - this.scrollPosition.y);
				return zero;
			}

			// Token: 0x0400078C RID: 1932
			public Rect position;

			// Token: 0x0400078D RID: 1933
			public Rect visibleRect;

			// Token: 0x0400078E RID: 1934
			public Rect viewRect;

			// Token: 0x0400078F RID: 1935
			public Vector2 scrollPosition;

			// Token: 0x04000790 RID: 1936
			public bool apply;

			// Token: 0x04000791 RID: 1937
			public bool hasScrollTo;
		}

		// Token: 0x020001F2 RID: 498
		public abstract class Scope : IDisposable
		{
			// Token: 0x06001EC1 RID: 7873
			protected abstract void CloseScope();

			// Token: 0x06001EC2 RID: 7874 RVA: 0x000201D8 File Offset: 0x0001E3D8
			~Scope()
			{
				if (!this.m_Disposed)
				{
					Debug.LogError("Scope was not disposed! You should use the 'using' keyword or manually call Dispose.");
					this.Dispose();
				}
			}

			// Token: 0x06001EC3 RID: 7875 RVA: 0x00020228 File Offset: 0x0001E428
			public void Dispose()
			{
				if (this.m_Disposed)
				{
					return;
				}
				this.m_Disposed = true;
				this.CloseScope();
			}

			// Token: 0x04000792 RID: 1938
			private bool m_Disposed;
		}

		// Token: 0x020001F3 RID: 499
		public class GroupScope : GUI.Scope
		{
			// Token: 0x06001EC4 RID: 7876 RVA: 0x00020244 File Offset: 0x0001E444
			public GroupScope(Rect position)
			{
				GUI.BeginGroup(position);
			}

			// Token: 0x06001EC5 RID: 7877 RVA: 0x00020254 File Offset: 0x0001E454
			public GroupScope(Rect position, string text)
			{
				GUI.BeginGroup(position, text);
			}

			// Token: 0x06001EC6 RID: 7878 RVA: 0x00020264 File Offset: 0x0001E464
			public GroupScope(Rect position, Texture image)
			{
				GUI.BeginGroup(position, image);
			}

			// Token: 0x06001EC7 RID: 7879 RVA: 0x00020274 File Offset: 0x0001E474
			public GroupScope(Rect position, GUIContent content)
			{
				GUI.BeginGroup(position, content);
			}

			// Token: 0x06001EC8 RID: 7880 RVA: 0x00020284 File Offset: 0x0001E484
			public GroupScope(Rect position, GUIStyle style)
			{
				GUI.BeginGroup(position, style);
			}

			// Token: 0x06001EC9 RID: 7881 RVA: 0x00020294 File Offset: 0x0001E494
			public GroupScope(Rect position, string text, GUIStyle style)
			{
				GUI.BeginGroup(position, text, style);
			}

			// Token: 0x06001ECA RID: 7882 RVA: 0x000202A4 File Offset: 0x0001E4A4
			public GroupScope(Rect position, Texture image, GUIStyle style)
			{
				GUI.BeginGroup(position, image, style);
			}

			// Token: 0x06001ECB RID: 7883 RVA: 0x000202B4 File Offset: 0x0001E4B4
			protected override void CloseScope()
			{
				GUI.EndGroup();
			}
		}

		// Token: 0x020001F4 RID: 500
		public class ScrollViewScope : GUI.Scope
		{
			// Token: 0x06001ECC RID: 7884 RVA: 0x000202BC File Offset: 0x0001E4BC
			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect);
			}

			// Token: 0x06001ECD RID: 7885 RVA: 0x000202E4 File Offset: 0x0001E4E4
			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical);
			}

			// Token: 0x06001ECE RID: 7886 RVA: 0x00020310 File Offset: 0x0001E510
			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, horizontalScrollbar, verticalScrollbar);
			}

			// Token: 0x06001ECF RID: 7887 RVA: 0x0002033C File Offset: 0x0001E53C
			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar);
			}

			// Token: 0x06001ED0 RID: 7888 RVA: 0x0002036C File Offset: 0x0001E56C
			internal ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
			}

			// Token: 0x1700080B RID: 2059
			// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x000203A0 File Offset: 0x0001E5A0
			// (set) Token: 0x06001ED2 RID: 7890 RVA: 0x000203A8 File Offset: 0x0001E5A8
			public Vector2 scrollPosition { get; private set; }

			// Token: 0x1700080C RID: 2060
			// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x000203B4 File Offset: 0x0001E5B4
			// (set) Token: 0x06001ED4 RID: 7892 RVA: 0x000203BC File Offset: 0x0001E5BC
			public bool handleScrollWheel { get; set; }

			// Token: 0x06001ED5 RID: 7893 RVA: 0x000203C8 File Offset: 0x0001E5C8
			protected override void CloseScope()
			{
				GUI.EndScrollView(this.handleScrollWheel);
			}
		}

		// Token: 0x020001F5 RID: 501
		public class ClipScope : GUI.Scope
		{
			// Token: 0x06001ED6 RID: 7894 RVA: 0x000203D8 File Offset: 0x0001E5D8
			public ClipScope(Rect position)
			{
				GUI.BeginClip(position);
			}

			// Token: 0x06001ED7 RID: 7895 RVA: 0x000203E8 File Offset: 0x0001E5E8
			protected override void CloseScope()
			{
				GUI.EndClip();
			}
		}

		// Token: 0x0200034A RID: 842
		// (Invoke) Token: 0x06002882 RID: 10370
		public delegate void WindowFunction(int id);
	}
}
