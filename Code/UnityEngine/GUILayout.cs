using System;

namespace UnityEngine
{
	// Token: 0x020001F7 RID: 503
	public class GUILayout
	{
		// Token: 0x06001EF1 RID: 7921 RVA: 0x000207A0 File Offset: 0x0001E9A0
		public static void Label(Texture image, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(image), GUI.skin.label, options);
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x000207B8 File Offset: 0x0001E9B8
		public static void Label(string text, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(text), GUI.skin.label, options);
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x000207D0 File Offset: 0x0001E9D0
		public static void Label(GUIContent content, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(content, GUI.skin.label, options);
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000207E4 File Offset: 0x0001E9E4
		public static void Label(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(image), style, options);
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x000207F4 File Offset: 0x0001E9F4
		public static void Label(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(text), style, options);
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00020804 File Offset: 0x0001EA04
		public static void Label(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(content, style, options);
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00020810 File Offset: 0x0001EA10
		private static void DoLabel(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUI.Label(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00020824 File Offset: 0x0001EA24
		public static void Box(Texture image, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(image), GUI.skin.box, options);
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0002083C File Offset: 0x0001EA3C
		public static void Box(string text, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(text), GUI.skin.box, options);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00020854 File Offset: 0x0001EA54
		public static void Box(GUIContent content, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(content, GUI.skin.box, options);
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x00020868 File Offset: 0x0001EA68
		public static void Box(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(image), style, options);
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00020878 File Offset: 0x0001EA78
		public static void Box(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(text), style, options);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x00020888 File Offset: 0x0001EA88
		public static void Box(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(content, style, options);
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x00020894 File Offset: 0x0001EA94
		private static void DoBox(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUI.Box(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x000208A8 File Offset: 0x0001EAA8
		public static bool Button(Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(image), GUI.skin.button, options);
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x000208C0 File Offset: 0x0001EAC0
		public static bool Button(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(text), GUI.skin.button, options);
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000208D8 File Offset: 0x0001EAD8
		public static bool Button(GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(content, GUI.skin.button, options);
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x000208EC File Offset: 0x0001EAEC
		public static bool Button(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(image), style, options);
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x000208FC File Offset: 0x0001EAFC
		public static bool Button(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(text), style, options);
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x0002090C File Offset: 0x0001EB0C
		public static bool Button(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(content, style, options);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x00020918 File Offset: 0x0001EB18
		private static bool DoButton(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.Button(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x0002092C File Offset: 0x0001EB2C
		public static bool RepeatButton(Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(image), GUI.skin.button, options);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x00020944 File Offset: 0x0001EB44
		public static bool RepeatButton(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(text), GUI.skin.button, options);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0002095C File Offset: 0x0001EB5C
		public static bool RepeatButton(GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(content, GUI.skin.button, options);
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x00020970 File Offset: 0x0001EB70
		public static bool RepeatButton(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(image), style, options);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x00020980 File Offset: 0x0001EB80
		public static bool RepeatButton(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(text), style, options);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x00020990 File Offset: 0x0001EB90
		public static bool RepeatButton(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(content, style, options);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0002099C File Offset: 0x0001EB9C
		private static bool DoRepeatButton(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.RepeatButton(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x000209B0 File Offset: 0x0001EBB0
		public static string TextField(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, false, GUI.skin.textField, options);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x000209D0 File Offset: 0x0001EBD0
		public static string TextField(string text, int maxLength, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, false, GUI.skin.textField, options);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x000209F0 File Offset: 0x0001EBF0
		public static string TextField(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, false, style, options);
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000209FC File Offset: 0x0001EBFC
		public static string TextField(string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, true, style, options);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x00020A08 File Offset: 0x0001EC08
		public static string PasswordField(string password, char maskChar, params GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(password, maskChar, -1, GUI.skin.textField, options);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x00020A28 File Offset: 0x0001EC28
		public static string PasswordField(string password, char maskChar, int maxLength, params GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(password, maskChar, maxLength, GUI.skin.textField, options);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x00020A48 File Offset: 0x0001EC48
		public static string PasswordField(string password, char maskChar, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(password, maskChar, -1, style, options);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x00020A54 File Offset: 0x0001EC54
		public static string PasswordField(string password, char maskChar, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			GUIContent content = GUIContent.Temp(GUI.PasswordFieldGetStrToShow(password, maskChar));
			return GUI.PasswordField(GUILayoutUtility.GetRect(content, GUI.skin.textField, options), password, maskChar, maxLength, style);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00020A8C File Offset: 0x0001EC8C
		public static string TextArea(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, true, GUI.skin.textArea, options);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x00020AAC File Offset: 0x0001ECAC
		public static string TextArea(string text, int maxLength, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, true, GUI.skin.textArea, options);
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x00020ACC File Offset: 0x0001ECCC
		public static string TextArea(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, true, style, options);
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x00020AD8 File Offset: 0x0001ECD8
		public static string TextArea(string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, true, style, options);
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00020AE4 File Offset: 0x0001ECE4
		private static string DoTextField(string text, int maxLength, bool multiline, GUIStyle style, GUILayoutOption[] options)
		{
			int controlID = GUIUtility.GetControlID(FocusType.Keyboard);
			GUIContent guicontent = GUIContent.Temp(text);
			if (GUIUtility.keyboardControl != controlID)
			{
				guicontent = GUIContent.Temp(text);
			}
			else
			{
				guicontent = GUIContent.Temp(text + Input.compositionString);
			}
			Rect rect = GUILayoutUtility.GetRect(guicontent, style, options);
			if (GUIUtility.keyboardControl == controlID)
			{
				guicontent = GUIContent.Temp(text);
			}
			GUI.DoTextField(rect, controlID, guicontent, multiline, maxLength, style);
			return guicontent.text;
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x00020B54 File Offset: 0x0001ED54
		public static bool Toggle(bool value, Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(image), GUI.skin.toggle, options);
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x00020B78 File Offset: 0x0001ED78
		public static bool Toggle(bool value, string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(text), GUI.skin.toggle, options);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x00020B9C File Offset: 0x0001ED9C
		public static bool Toggle(bool value, GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, content, GUI.skin.toggle, options);
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x00020BB0 File Offset: 0x0001EDB0
		public static bool Toggle(bool value, Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(image), style, options);
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x00020BC0 File Offset: 0x0001EDC0
		public static bool Toggle(bool value, string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(text), style, options);
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x00020BD0 File Offset: 0x0001EDD0
		public static bool Toggle(bool value, GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, content, style, options);
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x00020BDC File Offset: 0x0001EDDC
		private static bool DoToggle(bool value, GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.Toggle(GUILayoutUtility.GetRect(content, style, options), value, content, style);
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x00020BF0 File Offset: 0x0001EDF0
		public static int Toolbar(int selected, string[] texts, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(texts), GUI.skin.button, options);
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00020C14 File Offset: 0x0001EE14
		public static int Toolbar(int selected, Texture[] images, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(images), GUI.skin.button, options);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x00020C38 File Offset: 0x0001EE38
		public static int Toolbar(int selected, GUIContent[] content, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, content, GUI.skin.button, options);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x00020C4C File Offset: 0x0001EE4C
		public static int Toolbar(int selected, string[] texts, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(texts), style, options);
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x00020C5C File Offset: 0x0001EE5C
		public static int Toolbar(int selected, Texture[] images, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(images), style, options);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00020C6C File Offset: 0x0001EE6C
		public static int Toolbar(int selected, GUIContent[] contents, GUIStyle style, params GUILayoutOption[] options)
		{
			GUIStyle guistyle;
			GUIStyle guistyle2;
			GUIStyle guistyle3;
			GUI.FindStyles(ref style, out guistyle, out guistyle2, out guistyle3, "left", "mid", "right");
			Vector2 vector = default(Vector2);
			int num = contents.Length;
			GUIStyle guistyle4 = (num <= 1) ? style : guistyle;
			GUIStyle guistyle5 = (num <= 1) ? style : guistyle2;
			GUIStyle guistyle6 = (num <= 1) ? style : guistyle3;
			int num2 = guistyle4.margin.left;
			for (int i = 0; i < contents.Length; i++)
			{
				if (i == num - 2)
				{
					guistyle4 = guistyle5;
					guistyle5 = guistyle6;
				}
				if (i == num - 1)
				{
					guistyle4 = guistyle6;
				}
				Vector2 vector2 = guistyle4.CalcSize(contents[i]);
				if (vector2.x > vector.x)
				{
					vector.x = vector2.x;
				}
				if (vector2.y > vector.y)
				{
					vector.y = vector2.y;
				}
				if (i == num - 1)
				{
					num2 += guistyle4.margin.right;
				}
				else
				{
					num2 += Mathf.Max(guistyle4.margin.right, guistyle5.margin.left);
				}
			}
			vector.x = vector.x * (float)contents.Length + (float)num2;
			return GUI.Toolbar(GUILayoutUtility.GetRect(vector.x, vector.y, style, options), selected, contents, style);
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x00020DE4 File Offset: 0x0001EFE4
		public static int SelectionGrid(int selected, string[] texts, int xCount, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(texts), xCount, GUI.skin.button, options);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x00020E0C File Offset: 0x0001F00C
		public static int SelectionGrid(int selected, Texture[] images, int xCount, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(images), xCount, GUI.skin.button, options);
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x00020E34 File Offset: 0x0001F034
		public static int SelectionGrid(int selected, GUIContent[] content, int xCount, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, content, xCount, GUI.skin.button, options);
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x00020E54 File Offset: 0x0001F054
		public static int SelectionGrid(int selected, string[] texts, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(texts), xCount, style, options);
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00020E68 File Offset: 0x0001F068
		public static int SelectionGrid(int selected, Texture[] images, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(images), xCount, style, options);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x00020E7C File Offset: 0x0001F07C
		public static int SelectionGrid(int selected, GUIContent[] contents, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.SelectionGrid(GUIGridSizer.GetRect(contents, xCount, style, options), selected, contents, xCount, style);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x00020E94 File Offset: 0x0001F094
		public static float HorizontalSlider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return GUILayout.DoHorizontalSlider(value, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, options);
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x00020EC0 File Offset: 0x0001F0C0
		public static float HorizontalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return GUILayout.DoHorizontalSlider(value, leftValue, rightValue, slider, thumb, options);
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x00020ED0 File Offset: 0x0001F0D0
		private static float DoHorizontalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUILayoutOption[] options)
		{
			return GUI.HorizontalSlider(GUILayoutUtility.GetRect(GUIContent.Temp("mmmm"), slider, options), value, leftValue, rightValue, slider, thumb);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x00020EFC File Offset: 0x0001F0FC
		public static float VerticalSlider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return GUILayout.DoVerticalSlider(value, leftValue, rightValue, GUI.skin.verticalSlider, GUI.skin.verticalSliderThumb, options);
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x00020F28 File Offset: 0x0001F128
		public static float VerticalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return GUILayout.DoVerticalSlider(value, leftValue, rightValue, slider, thumb, options);
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x00020F38 File Offset: 0x0001F138
		private static float DoVerticalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return GUI.VerticalSlider(GUILayoutUtility.GetRect(GUIContent.Temp("\n\n\n\n\n"), slider, options), value, leftValue, rightValue, slider, thumb);
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00020F64 File Offset: 0x0001F164
		public static float HorizontalScrollbar(float value, float size, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return GUILayout.HorizontalScrollbar(value, size, leftValue, rightValue, GUI.skin.horizontalScrollbar, options);
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00020F88 File Offset: 0x0001F188
		public static float HorizontalScrollbar(float value, float size, float leftValue, float rightValue, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.HorizontalScrollbar(GUILayoutUtility.GetRect(GUIContent.Temp("mmmm"), style, options), value, size, leftValue, rightValue, style);
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00020FB4 File Offset: 0x0001F1B4
		public static float VerticalScrollbar(float value, float size, float topValue, float bottomValue, params GUILayoutOption[] options)
		{
			return GUILayout.VerticalScrollbar(value, size, topValue, bottomValue, GUI.skin.verticalScrollbar, options);
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00020FD8 File Offset: 0x0001F1D8
		public static float VerticalScrollbar(float value, float size, float topValue, float bottomValue, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.VerticalScrollbar(GUILayoutUtility.GetRect(GUIContent.Temp("\n\n\n\n"), style, options), value, size, topValue, bottomValue, style);
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x00021004 File Offset: 0x0001F204
		public static void Space(float pixels)
		{
			GUIUtility.CheckOnGUI();
			if (GUILayoutUtility.current.topLevel.isVertical)
			{
				GUILayoutUtility.GetRect(0f, pixels, GUILayoutUtility.spaceStyle, new GUILayoutOption[]
				{
					GUILayout.Height(pixels)
				});
			}
			else
			{
				GUILayoutUtility.GetRect(pixels, 0f, GUILayoutUtility.spaceStyle, new GUILayoutOption[]
				{
					GUILayout.Width(pixels)
				});
			}
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x00021070 File Offset: 0x0001F270
		public static void FlexibleSpace()
		{
			GUIUtility.CheckOnGUI();
			GUILayoutOption guilayoutOption;
			if (GUILayoutUtility.current.topLevel.isVertical)
			{
				guilayoutOption = GUILayout.ExpandHeight(true);
			}
			else
			{
				guilayoutOption = GUILayout.ExpandWidth(true);
			}
			guilayoutOption.value = 10000;
			GUILayoutUtility.GetRect(0f, 0f, GUILayoutUtility.spaceStyle, new GUILayoutOption[]
			{
				guilayoutOption
			});
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x000210D8 File Offset: 0x0001F2D8
		public static void BeginHorizontal(params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.none, GUIStyle.none, options);
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x000210EC File Offset: 0x0001F2EC
		public static void BeginHorizontal(GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.none, style, options);
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x000210FC File Offset: 0x0001F2FC
		public static void BeginHorizontal(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.Temp(text), style, options);
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x0002110C File Offset: 0x0001F30C
		public static void BeginHorizontal(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.Temp(image), style, options);
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x0002111C File Offset: 0x0001F31C
		public static void BeginHorizontal(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
			guilayoutGroup.isVertical = false;
			if (style != GUIStyle.none || content != GUIContent.none)
			{
				GUI.Box(guilayoutGroup.rect, content, style);
			}
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00021168 File Offset: 0x0001F368
		public static void EndHorizontal()
		{
			GUILayoutUtility.EndGroup("GUILayout.EndHorizontal");
			GUILayoutUtility.EndLayoutGroup();
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x0002117C File Offset: 0x0001F37C
		public static void BeginVertical(params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.none, GUIStyle.none, options);
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00021190 File Offset: 0x0001F390
		public static void BeginVertical(GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.none, style, options);
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x000211A0 File Offset: 0x0001F3A0
		public static void BeginVertical(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.Temp(text), style, options);
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x000211B0 File Offset: 0x0001F3B0
		public static void BeginVertical(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.Temp(image), style, options);
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x000211C0 File Offset: 0x0001F3C0
		public static void BeginVertical(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
			guilayoutGroup.isVertical = true;
			if (style != GUIStyle.none)
			{
				GUI.Box(guilayoutGroup.rect, content, style);
			}
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00021200 File Offset: 0x0001F400
		public static void EndVertical()
		{
			GUILayoutUtility.EndGroup("GUILayout.EndVertical");
			GUILayoutUtility.EndLayoutGroup();
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00021214 File Offset: 0x0001F414
		public static void BeginArea(Rect screenRect)
		{
			GUILayout.BeginArea(screenRect, GUIContent.none, GUIStyle.none);
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00021228 File Offset: 0x0001F428
		public static void BeginArea(Rect screenRect, string text)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(text), GUIStyle.none);
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x0002123C File Offset: 0x0001F43C
		public static void BeginArea(Rect screenRect, Texture image)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(image), GUIStyle.none);
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00021250 File Offset: 0x0001F450
		public static void BeginArea(Rect screenRect, GUIContent content)
		{
			GUILayout.BeginArea(screenRect, GUIContent.none, GUIStyle.none);
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x00021264 File Offset: 0x0001F464
		public static void BeginArea(Rect screenRect, GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, GUIContent.none, style);
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x00021274 File Offset: 0x0001F474
		public static void BeginArea(Rect screenRect, string text, GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(text), style);
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00021284 File Offset: 0x0001F484
		public static void BeginArea(Rect screenRect, Texture image, GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(image), style);
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x00021294 File Offset: 0x0001F494
		public static void BeginArea(Rect screenRect, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutArea(style, typeof(GUILayoutGroup));
			if (Event.current.type == EventType.Layout)
			{
				guilayoutGroup.resetCoords = true;
				guilayoutGroup.minWidth = (guilayoutGroup.maxWidth = screenRect.width);
				guilayoutGroup.minHeight = (guilayoutGroup.maxHeight = screenRect.height);
				guilayoutGroup.rect = Rect.MinMaxRect(screenRect.xMin, screenRect.yMin, guilayoutGroup.rect.xMax, guilayoutGroup.rect.yMax);
			}
			GUI.BeginGroup(guilayoutGroup.rect, content, style);
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x00021338 File Offset: 0x0001F538
		public static void EndArea()
		{
			GUIUtility.CheckOnGUI();
			if (Event.current.type == EventType.Used)
			{
				return;
			}
			GUILayoutUtility.current.layoutGroups.Pop();
			GUILayoutUtility.current.topLevel = (GUILayoutGroup)GUILayoutUtility.current.layoutGroups.Peek();
			GUI.EndGroup();
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00021390 File Offset: 0x0001F590
		public static Vector2 BeginScrollView(Vector2 scrollPosition, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x000213C4 File Offset: 0x0001F5C4
		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x000213F8 File Offset: 0x0001F5F8
		public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, false, false, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0002141C File Offset: 0x0001F61C
		public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style)
		{
			GUILayoutOption[] options = null;
			return GUILayout.BeginScrollView(scrollPosition, style, options);
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x00021434 File Offset: 0x0001F634
		public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options)
		{
			string name = style.name;
			GUIStyle guistyle = GUI.skin.FindStyle(name + "VerticalScrollbar");
			if (guistyle == null)
			{
				guistyle = GUI.skin.verticalScrollbar;
			}
			GUIStyle guistyle2 = GUI.skin.FindStyle(name + "HorizontalScrollbar");
			if (guistyle2 == null)
			{
				guistyle2 = GUI.skin.horizontalScrollbar;
			}
			return GUILayout.BeginScrollView(scrollPosition, false, false, guistyle2, guistyle, style, options);
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x000214A4 File Offset: 0x0001F6A4
		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x000214C8 File Offset: 0x0001F6C8
		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background, params GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			GUIScrollGroup guiscrollGroup = (GUIScrollGroup)GUILayoutUtility.BeginLayoutGroup(background, null, typeof(GUIScrollGroup));
			EventType type = Event.current.type;
			if (type == EventType.Layout)
			{
				guiscrollGroup.resetCoords = true;
				guiscrollGroup.isVertical = true;
				guiscrollGroup.stretchWidth = 1;
				guiscrollGroup.stretchHeight = 1;
				guiscrollGroup.verticalScrollbar = verticalScrollbar;
				guiscrollGroup.horizontalScrollbar = horizontalScrollbar;
				guiscrollGroup.needsVerticalScrollbar = alwaysShowVertical;
				guiscrollGroup.needsHorizontalScrollbar = alwaysShowHorizontal;
				guiscrollGroup.ApplyOptions(options);
			}
			return GUI.BeginScrollView(guiscrollGroup.rect, scrollPosition, new Rect(0f, 0f, guiscrollGroup.clientWidth, guiscrollGroup.clientHeight), alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00021584 File Offset: 0x0001F784
		public static void EndScrollView()
		{
			GUILayout.EndScrollView(true);
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x0002158C File Offset: 0x0001F78C
		internal static void EndScrollView(bool handleScrollWheel)
		{
			GUILayoutUtility.EndGroup("GUILayout.EndScrollView");
			GUILayoutUtility.EndLayoutGroup();
			GUI.EndScrollView(handleScrollWheel);
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x000215A4 File Offset: 0x0001F7A4
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(text), GUI.skin.window, options);
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x000215CC File Offset: 0x0001F7CC
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(image), GUI.skin.window, options);
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x000215F4 File Offset: 0x0001F7F4
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, content, GUI.skin.window, options);
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x00021618 File Offset: 0x0001F818
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(text), style, options);
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x0002162C File Offset: 0x0001F82C
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(image), style, options);
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x00021640 File Offset: 0x0001F840
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, content, style, options);
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x00021650 File Offset: 0x0001F850
		private static Rect DoWindow(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			GUILayout.LayoutedWindow @object = new GUILayout.LayoutedWindow(func, screenRect, content, options, style);
			return GUI.Window(id, screenRect, new GUI.WindowFunction(@object.DoWindow), content, style);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00021688 File Offset: 0x0001F888
		public static GUILayoutOption Width(float width)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedWidth, width);
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x00021698 File Offset: 0x0001F898
		public static GUILayoutOption MinWidth(float minWidth)
		{
			return new GUILayoutOption(GUILayoutOption.Type.minWidth, minWidth);
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x000216A8 File Offset: 0x0001F8A8
		public static GUILayoutOption MaxWidth(float maxWidth)
		{
			return new GUILayoutOption(GUILayoutOption.Type.maxWidth, maxWidth);
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x000216B8 File Offset: 0x0001F8B8
		public static GUILayoutOption Height(float height)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedHeight, height);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x000216C8 File Offset: 0x0001F8C8
		public static GUILayoutOption MinHeight(float minHeight)
		{
			return new GUILayoutOption(GUILayoutOption.Type.minHeight, minHeight);
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x000216D8 File Offset: 0x0001F8D8
		public static GUILayoutOption MaxHeight(float maxHeight)
		{
			return new GUILayoutOption(GUILayoutOption.Type.maxHeight, maxHeight);
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x000216E8 File Offset: 0x0001F8E8
		public static GUILayoutOption ExpandWidth(bool expand)
		{
			return new GUILayoutOption(GUILayoutOption.Type.stretchWidth, (!expand) ? 0 : 1);
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x00021704 File Offset: 0x0001F904
		public static GUILayoutOption ExpandHeight(bool expand)
		{
			return new GUILayoutOption(GUILayoutOption.Type.stretchHeight, (!expand) ? 0 : 1);
		}

		// Token: 0x020001F8 RID: 504
		private sealed class LayoutedWindow
		{
			// Token: 0x06001F66 RID: 8038 RVA: 0x00021720 File Offset: 0x0001F920
			internal LayoutedWindow(GUI.WindowFunction f, Rect screenRect, GUIContent content, GUILayoutOption[] options, GUIStyle style)
			{
				this.m_Func = f;
				this.m_ScreenRect = screenRect;
				this.m_Options = options;
				this.m_Style = style;
			}

			// Token: 0x06001F67 RID: 8039 RVA: 0x00021754 File Offset: 0x0001F954
			public void DoWindow(int windowID)
			{
				GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
				EventType type = Event.current.type;
				if (type != EventType.Layout)
				{
					topLevel.ResetCursor();
				}
				else
				{
					topLevel.resetCoords = true;
					topLevel.rect = this.m_ScreenRect;
					if (this.m_Options != null)
					{
						topLevel.ApplyOptions(this.m_Options);
					}
					topLevel.isWindow = true;
					topLevel.windowID = windowID;
					topLevel.style = this.m_Style;
				}
				this.m_Func(windowID);
			}

			// Token: 0x0400079C RID: 1948
			private readonly GUI.WindowFunction m_Func;

			// Token: 0x0400079D RID: 1949
			private readonly Rect m_ScreenRect;

			// Token: 0x0400079E RID: 1950
			private readonly GUILayoutOption[] m_Options;

			// Token: 0x0400079F RID: 1951
			private readonly GUIStyle m_Style;
		}

		// Token: 0x020001F9 RID: 505
		public class HorizontalScope : GUI.Scope
		{
			// Token: 0x06001F68 RID: 8040 RVA: 0x000217E4 File Offset: 0x0001F9E4
			public HorizontalScope(params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(options);
			}

			// Token: 0x06001F69 RID: 8041 RVA: 0x000217F4 File Offset: 0x0001F9F4
			public HorizontalScope(GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(style, options);
			}

			// Token: 0x06001F6A RID: 8042 RVA: 0x00021804 File Offset: 0x0001FA04
			public HorizontalScope(string text, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(text, style, options);
			}

			// Token: 0x06001F6B RID: 8043 RVA: 0x00021814 File Offset: 0x0001FA14
			public HorizontalScope(Texture image, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(image, style, options);
			}

			// Token: 0x06001F6C RID: 8044 RVA: 0x00021824 File Offset: 0x0001FA24
			public HorizontalScope(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginHorizontal(content, style, options);
			}

			// Token: 0x06001F6D RID: 8045 RVA: 0x00021834 File Offset: 0x0001FA34
			protected override void CloseScope()
			{
				GUILayout.EndHorizontal();
			}
		}

		// Token: 0x020001FA RID: 506
		public class VerticalScope : GUI.Scope
		{
			// Token: 0x06001F6E RID: 8046 RVA: 0x0002183C File Offset: 0x0001FA3C
			public VerticalScope(params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(options);
			}

			// Token: 0x06001F6F RID: 8047 RVA: 0x0002184C File Offset: 0x0001FA4C
			public VerticalScope(GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(style, options);
			}

			// Token: 0x06001F70 RID: 8048 RVA: 0x0002185C File Offset: 0x0001FA5C
			public VerticalScope(string text, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(text, style, options);
			}

			// Token: 0x06001F71 RID: 8049 RVA: 0x0002186C File Offset: 0x0001FA6C
			public VerticalScope(Texture image, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(image, style, options);
			}

			// Token: 0x06001F72 RID: 8050 RVA: 0x0002187C File Offset: 0x0001FA7C
			public VerticalScope(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
			{
				GUILayout.BeginVertical(content, style, options);
			}

			// Token: 0x06001F73 RID: 8051 RVA: 0x0002188C File Offset: 0x0001FA8C
			protected override void CloseScope()
			{
				GUILayout.EndVertical();
			}
		}

		// Token: 0x020001FB RID: 507
		public class AreaScope : GUI.Scope
		{
			// Token: 0x06001F74 RID: 8052 RVA: 0x00021894 File Offset: 0x0001FA94
			public AreaScope(Rect screenRect)
			{
				GUILayout.BeginArea(screenRect);
			}

			// Token: 0x06001F75 RID: 8053 RVA: 0x000218A4 File Offset: 0x0001FAA4
			public AreaScope(Rect screenRect, string text)
			{
				GUILayout.BeginArea(screenRect, text);
			}

			// Token: 0x06001F76 RID: 8054 RVA: 0x000218B4 File Offset: 0x0001FAB4
			public AreaScope(Rect screenRect, Texture image)
			{
				GUILayout.BeginArea(screenRect, image);
			}

			// Token: 0x06001F77 RID: 8055 RVA: 0x000218C4 File Offset: 0x0001FAC4
			public AreaScope(Rect screenRect, GUIContent content)
			{
				GUILayout.BeginArea(screenRect, content);
			}

			// Token: 0x06001F78 RID: 8056 RVA: 0x000218D4 File Offset: 0x0001FAD4
			public AreaScope(Rect screenRect, string text, GUIStyle style)
			{
				GUILayout.BeginArea(screenRect, text, style);
			}

			// Token: 0x06001F79 RID: 8057 RVA: 0x000218E4 File Offset: 0x0001FAE4
			public AreaScope(Rect screenRect, Texture image, GUIStyle style)
			{
				GUILayout.BeginArea(screenRect, image, style);
			}

			// Token: 0x06001F7A RID: 8058 RVA: 0x000218F4 File Offset: 0x0001FAF4
			public AreaScope(Rect screenRect, GUIContent content, GUIStyle style)
			{
				GUILayout.BeginArea(screenRect, content, style);
			}

			// Token: 0x06001F7B RID: 8059 RVA: 0x00021904 File Offset: 0x0001FB04
			protected override void CloseScope()
			{
				GUILayout.EndArea();
			}
		}

		// Token: 0x020001FC RID: 508
		public class ScrollViewScope : GUI.Scope
		{
			// Token: 0x06001F7C RID: 8060 RVA: 0x0002190C File Offset: 0x0001FB0C
			public ScrollViewScope(Vector2 scrollPosition, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, options);
			}

			// Token: 0x06001F7D RID: 8061 RVA: 0x00021928 File Offset: 0x0001FB28
			public ScrollViewScope(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, options);
			}

			// Token: 0x06001F7E RID: 8062 RVA: 0x00021954 File Offset: 0x0001FB54
			public ScrollViewScope(Vector2 scrollPosition, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, horizontalScrollbar, verticalScrollbar, options);
			}

			// Token: 0x06001F7F RID: 8063 RVA: 0x00021980 File Offset: 0x0001FB80
			public ScrollViewScope(Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, style, options);
			}

			// Token: 0x06001F80 RID: 8064 RVA: 0x000219A8 File Offset: 0x0001FBA8
			public ScrollViewScope(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, options);
			}

			// Token: 0x06001F81 RID: 8065 RVA: 0x000219D8 File Offset: 0x0001FBD8
			public ScrollViewScope(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background, params GUILayoutOption[] options)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background, options);
			}

			// Token: 0x17000811 RID: 2065
			// (get) Token: 0x06001F82 RID: 8066 RVA: 0x00021A08 File Offset: 0x0001FC08
			// (set) Token: 0x06001F83 RID: 8067 RVA: 0x00021A10 File Offset: 0x0001FC10
			public Vector2 scrollPosition { get; private set; }

			// Token: 0x17000812 RID: 2066
			// (get) Token: 0x06001F84 RID: 8068 RVA: 0x00021A1C File Offset: 0x0001FC1C
			// (set) Token: 0x06001F85 RID: 8069 RVA: 0x00021A24 File Offset: 0x0001FC24
			public bool handleScrollWheel { get; set; }

			// Token: 0x06001F86 RID: 8070 RVA: 0x00021A30 File Offset: 0x0001FC30
			protected override void CloseScope()
			{
				GUILayout.EndScrollView(this.handleScrollWheel);
			}
		}
	}
}
