using System;
using System.Collections.Generic;
using CatlikeCoding.TextBox;
using UnityEngine;

// Token: 0x02000765 RID: 1893
public class TextBoxIconsFontGenerator : ScriptableObject
{
	// Token: 0x06002C35 RID: 11317 RVA: 0x000BDA3C File Offset: 0x000BBC3C
	public TextBoxIconsFontGenerator.IconData FindIcon(int id)
	{
		foreach (TextBoxIconsFontGenerator.IconData iconData in this.Icons)
		{
			if ((int)iconData.Character[0] == id)
			{
				return iconData;
			}
		}
		return null;
	}

	// Token: 0x040027F1 RID: 10225
	public List<TextBoxIconsFontGenerator.IconData> Icons = new List<TextBoxIconsFontGenerator.IconData>();

	// Token: 0x040027F2 RID: 10226
	public BitmapFont BitmapFont;

	// Token: 0x02000766 RID: 1894
	[Serializable]
	public class IconData
	{
		// Token: 0x040027F3 RID: 10227
		public string Character = "x";

		// Token: 0x040027F4 RID: 10228
		public GameObject Icon;

		// Token: 0x040027F5 RID: 10229
		public float Width;
	}
}
