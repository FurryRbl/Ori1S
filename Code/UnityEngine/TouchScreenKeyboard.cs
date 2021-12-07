using System;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000057 RID: 87
	public sealed class TouchScreenKeyboard
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x000049C8 File Offset: 0x00002BC8
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert)
		{
			string empty = string.Empty;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, empty);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000049EC File Offset: 0x00002BEC
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure)
		{
			string empty = string.Empty;
			bool alert = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, empty);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00004A10 File Offset: 0x00002C10
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline)
		{
			string empty = string.Empty;
			bool alert = false;
			bool secure = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, empty);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00004A34 File Offset: 0x00002C34
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection)
		{
			string empty = string.Empty;
			bool alert = false;
			bool secure = false;
			bool multiline = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, empty);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00004A5C File Offset: 0x00002C5C
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType)
		{
			string empty = string.Empty;
			bool alert = false;
			bool secure = false;
			bool multiline = false;
			bool autocorrection = true;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, empty);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00004A88 File Offset: 0x00002C88
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text)
		{
			string empty = string.Empty;
			bool alert = false;
			bool secure = false;
			bool multiline = false;
			bool autocorrection = true;
			TouchScreenKeyboardType keyboardType = TouchScreenKeyboardType.Default;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, empty);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public static TouchScreenKeyboard Open(string text, [DefaultValue("TouchScreenKeyboardType.Default")] TouchScreenKeyboardType keyboardType, [DefaultValue("true")] bool autocorrection, [DefaultValue("false")] bool multiline, [DefaultValue("false")] bool secure, [DefaultValue("false")] bool alert, [DefaultValue("\"\"")] string textPlaceholder)
		{
			return null;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00004ABC File Offset: 0x00002CBC
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public string text
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00004AC8 File Offset: 0x00002CC8
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x00004ACC File Offset: 0x00002CCC
		public static bool hideInput
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00004AD0 File Offset: 0x00002CD0
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public bool active
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public bool done
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00004ADC File Offset: 0x00002CDC
		public bool wasCanceled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00004AE0 File Offset: 0x00002CE0
		private static Rect area
		{
			get
			{
				return default(Rect);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00004AF8 File Offset: 0x00002CF8
		private static bool visible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00004AFC File Offset: 0x00002CFC
		public static bool isSupported
		{
			get
			{
				return false;
			}
		}
	}
}
