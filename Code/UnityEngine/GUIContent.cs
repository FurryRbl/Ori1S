using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020001F6 RID: 502
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class GUIContent
	{
		// Token: 0x06001ED8 RID: 7896 RVA: 0x000203F0 File Offset: 0x0001E5F0
		public GUIContent()
		{
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00020410 File Offset: 0x0001E610
		public GUIContent(string text)
		{
			this.m_Text = text;
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x00020438 File Offset: 0x0001E638
		public GUIContent(Texture image)
		{
			this.m_Image = image;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x00020460 File Offset: 0x0001E660
		public GUIContent(string text, Texture image)
		{
			this.m_Text = text;
			this.m_Image = image;
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x00020498 File Offset: 0x0001E698
		public GUIContent(string text, string tooltip)
		{
			this.m_Text = text;
			this.m_Tooltip = tooltip;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x000204D0 File Offset: 0x0001E6D0
		public GUIContent(Texture image, string tooltip)
		{
			this.m_Image = image;
			this.m_Tooltip = tooltip;
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x00020508 File Offset: 0x0001E708
		public GUIContent(string text, Texture image, string tooltip)
		{
			this.m_Text = text;
			this.m_Image = image;
			this.m_Tooltip = tooltip;
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x0002053C File Offset: 0x0001E73C
		public GUIContent(GUIContent src)
		{
			this.m_Text = src.m_Text;
			this.m_Image = src.m_Image;
			this.m_Tooltip = src.m_Tooltip;
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x000205BC File Offset: 0x0001E7BC
		// (set) Token: 0x06001EE2 RID: 7906 RVA: 0x000205C4 File Offset: 0x0001E7C4
		public string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				this.m_Text = value;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x000205D0 File Offset: 0x0001E7D0
		// (set) Token: 0x06001EE4 RID: 7908 RVA: 0x000205D8 File Offset: 0x0001E7D8
		public Texture image
		{
			get
			{
				return this.m_Image;
			}
			set
			{
				this.m_Image = value;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x000205E4 File Offset: 0x0001E7E4
		// (set) Token: 0x06001EE6 RID: 7910 RVA: 0x000205EC File Offset: 0x0001E7EC
		public string tooltip
		{
			get
			{
				return this.m_Tooltip;
			}
			set
			{
				this.m_Tooltip = value;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x000205F8 File Offset: 0x0001E7F8
		internal int hash
		{
			get
			{
				int result = 0;
				if (!string.IsNullOrEmpty(this.m_Text))
				{
					result = this.m_Text.GetHashCode() * 37;
				}
				return result;
			}
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x00020628 File Offset: 0x0001E828
		internal static GUIContent Temp(string t)
		{
			GUIContent.s_Text.m_Text = t;
			GUIContent.s_Text.m_Tooltip = string.Empty;
			return GUIContent.s_Text;
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0002064C File Offset: 0x0001E84C
		internal static GUIContent Temp(string t, string tooltip)
		{
			GUIContent.s_Text.m_Text = t;
			GUIContent.s_Text.m_Tooltip = tooltip;
			return GUIContent.s_Text;
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x0002066C File Offset: 0x0001E86C
		internal static GUIContent Temp(Texture i)
		{
			GUIContent.s_Image.m_Image = i;
			GUIContent.s_Image.m_Tooltip = string.Empty;
			return GUIContent.s_Image;
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x00020690 File Offset: 0x0001E890
		internal static GUIContent Temp(Texture i, string tooltip)
		{
			GUIContent.s_Image.m_Image = i;
			GUIContent.s_Image.m_Tooltip = tooltip;
			return GUIContent.s_Image;
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x000206B0 File Offset: 0x0001E8B0
		internal static GUIContent Temp(string t, Texture i)
		{
			GUIContent.s_TextImage.m_Text = t;
			GUIContent.s_TextImage.m_Image = i;
			return GUIContent.s_TextImage;
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x000206D0 File Offset: 0x0001E8D0
		internal static void ClearStaticCache()
		{
			GUIContent.s_Text.m_Text = null;
			GUIContent.s_Text.m_Tooltip = string.Empty;
			GUIContent.s_Image.m_Image = null;
			GUIContent.s_Image.m_Tooltip = string.Empty;
			GUIContent.s_TextImage.m_Text = null;
			GUIContent.s_TextImage.m_Image = null;
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x00020728 File Offset: 0x0001E928
		internal static GUIContent[] Temp(string[] texts)
		{
			GUIContent[] array = new GUIContent[texts.Length];
			for (int i = 0; i < texts.Length; i++)
			{
				array[i] = new GUIContent(texts[i]);
			}
			return array;
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00020760 File Offset: 0x0001E960
		internal static GUIContent[] Temp(Texture[] images)
		{
			GUIContent[] array = new GUIContent[images.Length];
			for (int i = 0; i < images.Length; i++)
			{
				array[i] = new GUIContent(images[i]);
			}
			return array;
		}

		// Token: 0x04000795 RID: 1941
		[SerializeField]
		private string m_Text = string.Empty;

		// Token: 0x04000796 RID: 1942
		[SerializeField]
		private Texture m_Image;

		// Token: 0x04000797 RID: 1943
		[SerializeField]
		private string m_Tooltip = string.Empty;

		// Token: 0x04000798 RID: 1944
		private static readonly GUIContent s_Text = new GUIContent();

		// Token: 0x04000799 RID: 1945
		private static readonly GUIContent s_Image = new GUIContent();

		// Token: 0x0400079A RID: 1946
		private static readonly GUIContent s_TextImage = new GUIContent();

		// Token: 0x0400079B RID: 1947
		public static GUIContent none = new GUIContent(string.Empty);
	}
}
