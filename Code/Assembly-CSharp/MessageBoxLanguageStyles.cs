using System;
using CatlikeCoding.TextBox;
using UnityEngine;

// Token: 0x0200067E RID: 1662
public class MessageBoxLanguageStyles : ScriptableObject
{
	// Token: 0x1700066E RID: 1646
	// (get) Token: 0x06002861 RID: 10337 RVA: 0x000AF0AF File Offset: 0x000AD2AF
	public TextStyleCollection Current
	{
		get
		{
			if (Application.isPlaying)
			{
				return this.GetStyle(GameSettings.Instance.Language);
			}
			return this.English;
		}
	}

	// Token: 0x06002862 RID: 10338 RVA: 0x000AF0D4 File Offset: 0x000AD2D4
	public TextStyleCollection GetStyle(Language language)
	{
		switch (language)
		{
		case Language.English:
			return this.English;
		case Language.French:
			return this.French;
		case Language.Italian:
			return this.Italian;
		case Language.German:
			return this.German;
		case Language.Spanish:
			return this.Spanish;
		case Language.Japanese:
			return this.Japanese;
		case Language.Portuguese:
			return this.Portuguese;
		case Language.Chinese:
			return this.Chinese;
		case Language.Russian:
			return this.Russian;
		default:
			return this.English;
		}
	}

	// Token: 0x040023E5 RID: 9189
	public TextStyleCollection English;

	// Token: 0x040023E6 RID: 9190
	public TextStyleCollection German;

	// Token: 0x040023E7 RID: 9191
	public TextStyleCollection Spanish;

	// Token: 0x040023E8 RID: 9192
	public TextStyleCollection French;

	// Token: 0x040023E9 RID: 9193
	public TextStyleCollection Italian;

	// Token: 0x040023EA RID: 9194
	public TextStyleCollection Portuguese;

	// Token: 0x040023EB RID: 9195
	public TextStyleCollection Russian;

	// Token: 0x040023EC RID: 9196
	public int Space;

	// Token: 0x040023ED RID: 9197
	public TextStyleCollection Japanese;

	// Token: 0x040023EE RID: 9198
	public TextStyleCollection Chinese;
}
