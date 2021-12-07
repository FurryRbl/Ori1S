using System;
using UnityEngine;

// Token: 0x020004D2 RID: 1234
public class LanguageToggler : MonoBehaviour, IDebugMenuToggleable
{
	// Token: 0x170005B1 RID: 1457
	// (get) Token: 0x0600216C RID: 8556 RVA: 0x00092D54 File Offset: 0x00090F54
	public string Name
	{
		get
		{
			return "Language";
		}
	}

	// Token: 0x170005B2 RID: 1458
	// (get) Token: 0x0600216D RID: 8557 RVA: 0x00092D5B File Offset: 0x00090F5B
	public string HelpText
	{
		get
		{
			return "Toggle differnt languages";
		}
	}

	// Token: 0x170005B3 RID: 1459
	// (get) Token: 0x0600216E RID: 8558 RVA: 0x00092D64 File Offset: 0x00090F64
	public string[] ToggleOptions
	{
		get
		{
			return new string[]
			{
				"English",
				"French",
				"Italian",
				"German",
				"Spanish",
				"Japanese",
				"Portuguese",
				"Chinese",
				"Russian"
			};
		}
	}

	// Token: 0x170005B4 RID: 1460
	// (get) Token: 0x0600216F RID: 8559 RVA: 0x00092DC0 File Offset: 0x00090FC0
	// (set) Token: 0x06002170 RID: 8560 RVA: 0x00092DCC File Offset: 0x00090FCC
	public int CurrentToggleOptionId
	{
		get
		{
			return (int)GameSettings.Instance.Language;
		}
		set
		{
			GameSettings.Instance.Language = (value % this.ToggleOptions.Length + (Language)this.ToggleOptions.Length) % (Language)this.ToggleOptions.Length;
		}
	}

	// Token: 0x04001C3E RID: 7230
	private int m_currentOption;
}
